using System;
using System.Collections.Generic;
using System.Numerics;
using Anvil.Services;
using NWN.Core.NWNX;
using XM.Progression.Job;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Activity;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;
using PlayerPlugin = XM.Shared.API.NWNX.PlayerPlugin.PlayerPlugin;

namespace XM.Plugin.Item
{
    [ServiceBinding(typeof(ItemUseService))]
    [ServiceBinding(typeof(IInitializable))]
    internal class ItemUseService: IInitializable
    {
        private static readonly Dictionary<string, ItemDetail> _items = new();

        private readonly XMEventService _event;
        private readonly ActivityService _activity;
        private readonly JobService _job;
        private readonly StatService _stat;
        private readonly RecastService _recast;

        private readonly IList<IItemListDefinition> _itemListDefinitions;

        public ItemUseService(
            XMEventService @event,
            ActivityService activity,
            JobService job,
            StatService stat,
            RecastService recast,
            IList<IItemListDefinition> itemListDefinitions)
        {
            _event = @event;
            _activity = activity;
            _job = job;
            _stat = stat;
            _recast = recast;
            _itemListDefinitions = itemListDefinitions;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<NWNXEvent.OnUseItemBefore>(UseItem);
        }
        public void Init()
        {
            LoadItemDefinitions();
        }

        private void LoadItemDefinitions()
        {
            foreach (var list in _itemListDefinitions)
            {
                foreach (var (tag, detail) in list.BuildItems())
                {
                    _items[tag] = detail;
                }
            }
        }

        private void UseItem(uint user)
        {
            var item = StringToObject(EventsPlugin.GetEventData("ITEM_OBJECT_ID"));
            var itemTag = GetTag(item);

            // Not in the cache. Skip.
            if (!_items.ContainsKey(itemTag))
                return;

            var target = StringToObject(EventsPlugin.GetEventData("TARGET_OBJECT_ID"));
            var area = GetArea(user);
            var targetPositionX = (float)Convert.ToDouble(EventsPlugin.GetEventData("TARGET_POSITION_X"));
            var targetPositionY = (float)Convert.ToDouble(EventsPlugin.GetEventData("TARGET_POSITION_Y"));
            var targetPositionZ = (float)Convert.ToDouble(EventsPlugin.GetEventData("TARGET_POSITION_Z"));
            var targetPosition = GetIsObjectValid(target) ? GetPosition(target) : Vector(targetPositionX, targetPositionY, targetPositionZ);
            var targetLocation = GetIsObjectValid(target) ? GetLocation(target) : Location(area, targetPosition, 0.0f);
            var userPosition = GetPosition(user);
            var propertyIndex = Convert.ToInt32(EventsPlugin.GetEventData("ITEM_PROPERTY_INDEX"));
            var itemDetail = _items[itemTag];

            // Bypass the NWN "item use" animation.
            EventsPlugin.SkipEvent();

            // Check item property requirements.
            if (!CanCreatureUseItem(user, item))
            {
                SendMessageToPC(user, "You do not meet the requirements to use this item.");
                return;
            }

            // User is busy
            if (_activity.IsBusy(user))
            {
                SendMessageToPC(user, "You are busy.");
                return;
            }

            // Check recast cooldown
            if (itemDetail.RecastGroup != null && itemDetail.RecastCooldown != null)
            {
                var (isOnRecast, timeToWait) = _recast.IsOnRecastDelay(user, (RecastGroup)itemDetail.RecastGroup);
                if (isOnRecast)
                {
                    SendMessageToPC(user, $"This item can be used in {timeToWait}.");
                    return;
                }
            }


            var validationMessage = itemDetail.ValidateAction == null ? string.Empty : itemDetail.ValidateAction(user, item, target, targetLocation, propertyIndex);

            // Failed validation.
            if (!string.IsNullOrWhiteSpace(validationMessage))
            {
                SendMessageToPC(user, validationMessage);
                return;
            }

            // Send the initialization message, if there is one.
            var initializationMessage = itemDetail.InitializationMessageAction == null
                ? string.Empty
                : itemDetail.InitializationMessageAction(user, item, target, targetLocation, propertyIndex);
            if (!string.IsNullOrWhiteSpace(initializationMessage))
            {
                SendMessageToPC(user, initializationMessage);
            }

            var maxDistance = itemDetail.CalculateDistanceAction?.Invoke(user, item, target, targetLocation, propertyIndex) ?? 3.5f;
            // Distance checks, if necessary for this item.
            if (GetItemPossessor(target) != user && maxDistance > 0.0f)
            {
                // Target is valid - check distance between objects.
                if (GetIsObjectValid(target) &&
                    (GetDistanceBetween(user, target) > maxDistance ||
                     area != GetArea(target)))
                {
                    SendMessageToPC(user, "Your target is too far away.");
                    return;
                }
                // Target is invalid - check distance between locations.
                else if (!GetIsObjectValid(target) &&
                         (GetDistanceBetweenLocations(GetLocation(user), targetLocation) > maxDistance ||
                          area != GetAreaFromLocation(targetLocation)))
                {
                    SendMessageToPC(user, "That location is too far away.");
                    return;
                }
            }

            // Make the user turn to face the target if configured.
            if (itemDetail.UserFacesTarget)
            {
                AssignCommand(user, () => SetFacingPoint(targetPosition));
            }

            var delay = itemDetail.DelayAction?.Invoke(user, item, target, targetLocation, propertyIndex) ?? 0.0f;
            // Play an animation if configured.
            if (itemDetail.ActivationAnimation != AnimationType.Invalid)
            {
                AssignCommand(user, () => ActionPlayAnimation(itemDetail.ActivationAnimation, 1.0f, delay));
            }

            // Play the timing bar for a player user.
            if (delay > 0.0f &&
                GetIsPC(user))
            {
                PlayerPlugin.StartGuiTimingBar(user, delay);
            }

            // Apply the item's action if specified.
            if (itemDetail.ApplyAction != null)
            {
                var actionId = Guid.NewGuid().ToString();
                _activity.SetBusy(user, ActivityStatusType.UseItem);
                SetLocalBool(user, actionId, true);
                CheckPosition(user, actionId, userPosition);

                DelayCommand(delay + 0.1f, () =>
                {
                    DeleteLocalBool(user, actionId);
                    _activity.ClearBusy(user);

                    var updatedPosition = GetPosition(user);

                    // Check if user has moved.
                    if (userPosition.X != updatedPosition.X ||
                        userPosition.Y != updatedPosition.Y ||
                        userPosition.Z != updatedPosition.Z)
                    {
                        return;
                    }

                    // Rerun validation since things may have changed since the user started the action.
                    validationMessage = itemDetail.ValidateAction == null ? string.Empty : itemDetail.ValidateAction(user, item, target, targetLocation, propertyIndex);
                    if (!string.IsNullOrWhiteSpace(validationMessage))
                    {
                        SendMessageToPC(user, validationMessage);
                        return;
                    }

                    itemDetail.ApplyAction(user, item, target, targetLocation, propertyIndex);

                    // Reduce item charge if specified.
                    var reducesItemCharge = itemDetail.ReducesItemChargeAction?.Invoke(user, item, target, targetLocation, propertyIndex) ?? false;
                    if (reducesItemCharge)
                    {
                        var charges = GetItemCharges(item) - 1;

                        if (charges <= 0)
                        {
                            DestroyObject(item);
                        }
                        else
                        {
                            SetItemCharges(item, charges);
                        }
                    }
                });
            }
        }


        private void CheckPosition(uint actionUser, string actionId, Vector3 originalPosition)
        {
            // Action ended, no need to continue checking.
            if (!GetLocalBool(actionUser, actionId)) return;

            var position = GetPosition(actionUser);

            if (position.X != originalPosition.X ||
                position.Y != originalPosition.Y ||
                position.Z != originalPosition.Z)
            {
                _activity.ClearBusy(actionUser);
                SendMessageToPC(actionUser, LocaleString.YouMoveAndInterruptYourAction.ToLocalizedString());
                PlayerPlugin.StopGuiTimingBar(actionUser, string.Empty);
                return;
            }

            DelayCommand(0.1f, () => CheckPosition(actionUser, actionId, originalPosition));
        }
        private bool CanCreatureUseItem(uint creature, uint item)
        {
            var job = _job.GetActiveJob(creature);
            var level = _stat.GetLevel(creature);

            for (var ip = GetFirstItemProperty(item); GetIsItemPropertyValid(ip); ip = GetNextItemProperty(item))
            {
                var type = GetItemPropertyType(ip);

                if (type == ItemPropertyType.UseLimitationJob)
                {
                    var jobType = (JobType)GetItemPropertySubType(ip);

                    if (job.Type != jobType)
                        return false;
                }
                else if (type == ItemPropertyType.LevelRequired)
                {
                    var levelRequired = GetItemPropertyCostTableValue(ip);

                    if (level < levelRequired)
                        return false;
                }
            }

            return true;
        }
    }
}
