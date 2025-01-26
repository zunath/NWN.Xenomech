using System;
using System.Collections.Generic;
using System.Numerics;
using Anvil.API;
using Anvil.Services;
using NWN.Core.NWNX;
using XM.Progression.Job;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.Activity;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;

namespace XM.Progression.Ability
{
    [ServiceBinding(typeof(AbilityService))]
    internal class AbilityService: IInitializable
    {
        private static readonly Dictionary<FeatType, AbilityDetail> _abilities = new();

        private readonly ActivityService _activity;
        private readonly StatService _stat;
        private readonly RecastService _recast;
        private readonly JobService _job;
        private readonly IList<IAbilityListDefinition> _abilityDefinitions;

        public AbilityService(
            ActivityService activity,
            StatService stat,
            RecastService recast,
            JobService job,
            IList<IAbilityListDefinition> abilityDefinitions,
            XMEventService @event)
        {
            _activity = activity;
            _stat = stat;
            _recast = recast;
            _job = job;
            _abilityDefinitions = abilityDefinitions;

            @event.Subscribe<NWNXEvent.OnUseFeatBefore>(UseAbility);
        }

        public void Init()
        {
            CacheAbilities();
        }

        private void CacheAbilities()
        {
            foreach (var definition in _abilityDefinitions)
            {
                var abilities = definition.BuildAbilities();

                foreach (var (feat, ability) in abilities)
                {
                    _abilities[feat] = ability;
                }
            }
        }
        private AbilityDetail GetAbilityDetail(FeatType featType)
        {
            if (!_abilities.ContainsKey(featType))
                throw new KeyNotFoundException($"Feat '{featType}' is not registered to an ability.");

            return _abilities[featType];
        }

        private bool CanUseAbility(
            uint activator,
            uint target,
            FeatType feat,
            Location targetLocation)
        {
            var ability = GetAbilityDetail(feat);

            if (GetIsPC(activator) && 
                !GetIsDM(activator) && 
                !GetIsDMPossessed(activator))
            {
                var level = _stat.GetLevel(activator);
                var job = _job.GetActiveJob(activator);
                var levelAcquired = job.GetFeatAcquiredLevel(feat);

                if (level < levelAcquired)
                {
                    SendMessageToPC(activator, LocaleString.InsufficientLevel.ToLocalizedString());
                    return false;
                }
            }

            // Activator is dead.
            if (GetCurrentHitPoints(activator) <= 0)
            {
                SendMessageToPC(activator, LocaleString.YouAreDead.ToLocalizedString());
                return false;
            }

            // Not commandable
            if (!GetCommandable(activator))
            {
                SendMessageToPC(activator, LocaleString.YouCannotTakeActionsAtThisTime.ToLocalizedString());
                return false;
            }

            // Must be within line of sight.
            if (GetIsObjectValid(target) && !LineOfSightObject(activator, target))
            {
                SendMessageToPC(activator, LocaleString.YouCannotSeeYourTarget.ToLocalizedString());
                return false;
            }

            // Must not be busy
            if (_activity.IsBusy(activator))
            {
                SendMessageToPC(activator, LocaleString.YouAreBusy.ToLocalizedString());
                return false;
            }

            // Range check.
            if (GetDistanceBetween(activator, target) > ability.MaxRange)
            {
                SendMessageToPC(activator, LocaleString.YouAreOutOfRangeThisAbilityHasARangeOfXMeters.ToLocalizedString(ability.MaxRange));
                return false;
            }

            // Hostility check
            if (GetIsObjectValid(target) && !GetIsReactionTypeHostile(target, activator) && ability.IsHostileAbility)
            {
                SendMessageToPC(activator, LocaleString.YouMayOnlyUseThisAbilityOnEnemies.ToLocalizedString());
                return false;
            }

            // EP check
            if (_stat.GetCurrentEP(activator) < ability.EPRequired)
            {
                SendMessageToPC(activator, LocaleString.InsufficientEP.ToLocalizedString());
                return false;
            }

            // Perk-specific custom validation logic.
            var customValidationResult = ability.CustomValidation == null 
                ? string.Empty 
                : ability.CustomValidation(activator, target, targetLocation);
            if (!string.IsNullOrWhiteSpace(customValidationResult))
            {
                SendMessageToPC(activator, customValidationResult);
                return false;
            }

            // Check if ability is on a recast timer still.
            var (isOnRecast, timeToWait) = _recast.IsOnRecastDelay(activator, ability.RecastGroup);
            if (isOnRecast)
            {
                SendMessageToPC(activator, LocaleString.ThisAbilityCanBeUsedIn.ToLocalizedString(timeToWait));
                return false;
            }

            return true;
        }
        private bool IsFeatRegistered(FeatType featType)
        {
            return _abilities.ContainsKey(featType);
        }

        // Variable names for queued abilities.
        private const string ActiveAbilityIdName = "ACTIVE_ABILITY_ID";
        private const string ActiveAbilityFeatIdName = "ACTIVE_ABILITY_FEAT_ID";
        private const string ActiveAbilityEffectivePerkLevelName = "ACTIVE_ABILITY_EFFECTIVE_PERK_LEVEL";

        private void UseAbility(uint activator)
        {
            var target = StringToObject(EventsPlugin.GetEventData("TARGET_OBJECT_ID"));
            var targetArea = StringToObject(EventsPlugin.GetEventData("AREA_OBJECT_ID"));
            var targetPosition = Vector(
                (float)Convert.ToDouble(EventsPlugin.GetEventData("TARGET_POSITION_X")),
                (float)Convert.ToDouble(EventsPlugin.GetEventData("TARGET_POSITION_Y")),
                (float)Convert.ToDouble(EventsPlugin.GetEventData("TARGET_POSITION_Z"))
            );

            // If we have a valid target, use its position
            if (GetIsObjectValid(target))
            {
                targetPosition = GetPosition(target);
            }

            var targetLocation = Location(targetArea, targetPosition, 0.0f);

            var feat = (FeatType)Convert.ToInt32(EventsPlugin.GetEventData("FEAT_ID"));
            if (!IsFeatRegistered(feat)) return;
            var ability = GetAbilityDetail(feat);

            // Weapon abilities are queued for the next time the activator's attack lands on an enemy.
            if (ability.ActivationType == AbilityActivationType.Weapon)
            {
                if (CanUseAbility(activator, target, feat, targetLocation))
                {
                    if (ability.DisplaysActivationMessage)
                        Messaging.SendMessageNearbyToPlayers(activator, LocaleString.PlayerQueuesAbilityForTheNextAttack.ToLocalizedString(GetName(activator), ability.Name.ToLocalizedString()));
                    QueueWeaponAbility(activator, ability, feat);
                }
            }
            // All other abilities are funneled through the same process.
            else
            {
                if (CanUseAbility(activator, target, feat, targetLocation))
                {
                    if (GetIsObjectValid(target))
                    {
                        if (ability.DisplaysActivationMessage)
                            Messaging.SendMessageNearbyToPlayers(activator, LocaleString.PlayerReadiesAbilityOnTarget.ToLocalizedString(GetName(activator), ability.Name.ToLocalizedString(), GetName(target)));
                    }
                    else
                    {
                        if (ability.DisplaysActivationMessage)
                            Messaging.SendMessageNearbyToPlayers(activator, LocaleString.PlayerReadiesAbility.ToLocalizedString(GetName(activator), ability.Name.ToLocalizedString()));
                    }

                    ActivateAbility(activator, target, feat, ability, targetLocation);
                }
            }
        }

        private void ActivateAbility(
            uint activator,
            uint target,
            FeatType feat,
            AbilityDetail ability,
            Location targetLocation)
        {
            float CalculateActivationDelay()
            {
                var abilityDelay = ability.ActivationDelay?.Invoke(activator, target) ?? 0.0f;
                return abilityDelay;
            }

            // Handles displaying animation and visual effects.
            void ProcessAnimationAndVisualEffects(float delay)
            {
                // Force out of stealth
                if (GetActionMode(activator, ActionModeType.Stealth))
                    SetActionMode(activator, ActionModeType.Stealth, false);

                AssignCommand(activator, () => ClearAllActions());
                TurnToFaceObject(target, activator);

                // Display a casting visual effect if one has been specified.
                if (ability.ActivationVisualEffect != VisualEffectType.None)
                {
                    var vfx = TagEffect(EffectVisualEffect(ability.ActivationVisualEffect), "ACTIVATION_VFX");
                    ApplyEffectToObject(DurationType.Temporary, vfx, activator, delay + 0.2f);
                }

                // Casted types play an animation of casting.
                if (ability.ActivationType == AbilityActivationType.Casted &&
                    ability.AnimationType != AnimationType.Invalid)
                {
                    var animationLength = delay - 0.2f;
                    if (animationLength < 0f)
                        animationLength = 0f;

                    AssignCommand(activator, () => ActionPlayAnimation(ability.AnimationType, 1.0f, animationLength));
                }
            }

            // Recursive function which checks if player has moved since starting the casting.
            void CheckForActivationInterruption(string activationId, Vector3 originalPosition)
            {
                if (!GetIsPC(activator)) return;

                // Completed abilities should no longer run.
                var status = GetLocalInt(activator, activationId);
                if (status == (int)ActivationStatus.Completed || status == (int)ActivationStatus.Invalid) return;

                var currentPosition = GetPosition(activator);

                if (currentPosition.X != originalPosition.X ||
                    currentPosition.Y != originalPosition.Y ||
                    currentPosition.Z != originalPosition.Z)
                {
                    RemoveEffectByTag(activator, "ACTIVATION_VFX");
                    PlayerPlugin.StopGuiTimingBar(activator, string.Empty);
                    Messaging.SendMessageNearbyToPlayers(activator, LocaleString.PlayersAbilityHasBeenInterrupted.ToLocalizedString(GetName(activator)));
                    SetLocalInt(activator, activationId, (int)ActivationStatus.Interrupted);
                    return;
                }

                DelayCommand(0.5f, () => CheckForActivationInterruption(activationId, originalPosition));
            }

            // This method is called after the delay of the ability has finished.
            void CompleteActivation(string activationId, float abilityRecastDelay)
            {
                _activity.ClearBusy(activator);

                // Moved during casting or activator died. Cancel the activation.
                if (GetLocalInt(activator, activationId) == (int)ActivationStatus.Interrupted || GetCurrentHitPoints(activator) <= 0)
                    return;

                if (!CanUseAbility(activator, target, feat, targetLocation))
                    return;

                DeleteLocalInt(activator, activationId);

                ApplyRequirementEffects(activator, ability);
                ability.ImpactAction?.Invoke(activator, target, targetLocation);
                _recast.ApplyRecastDelay(activator, ability.RecastGroup, abilityRecastDelay, false);
            }

            // Begin the main process
            var activationId = Guid.NewGuid().ToString();
            var activationDelay = CalculateActivationDelay();
            var recastDelay = ability.RecastDelay?.Invoke(activator) ?? 0f;
            var position = GetPosition(activator);
            ProcessAnimationAndVisualEffects(activationDelay);
            SetLocalInt(activator, activationId, (int)ActivationStatus.Started);
            CheckForActivationInterruption(activationId, position);

            var executeImpact = ability.ActivationAction == null
                ? true
                : ability.ActivationAction?.Invoke(activator, target, targetLocation);

            if (executeImpact == true)
            {
                if (GetIsPC(activator))
                {
                    if (activationDelay > 0.0f)
                    {
                        PlayerPlugin.StartGuiTimingBar(activator, activationDelay, string.Empty);
                    }
                }

                _activity.SetBusy(activator, ActivityStatusType.AbilityActivation);
                DelayCommand(activationDelay, () => CompleteActivation(activationId, recastDelay));

                // If currently attacking a target, re-attack it after the end of the activation period.
                // This mitigates the issue where a melee fighter's combat is disrupted for using an ability.
                if (GetCurrentAction(activator) == ActionType.AttackObject)
                {
                    var attackTarget = GetAttackTarget(activator);
                    DelayCommand(activationDelay + 0.1f, () =>
                    {
                        AssignCommand(activator, () => ActionAttack(attackTarget));
                    });
                }
            }
        }

        private void QueueWeaponAbility(uint activator, AbilityDetail ability, FeatType feat)
        {
            var abilityId = Guid.NewGuid().ToString();
            // Assign local variables which will be picked up on the next weapon OnHit event by this player.
            SetLocalString(activator, ActiveAbilityIdName, abilityId);
            SetLocalInt(activator, ActiveAbilityFeatIdName, (int)feat);

            ApplyRequirementEffects(activator, ability);

            var abilityRecastDelay = ability.RecastDelay?.Invoke(activator) ?? 0.0f;
            _recast.ApplyRecastDelay(activator, ability.RecastGroup, abilityRecastDelay, false);

            // Activator must attack within 30 seconds after queueing or else it wears off.
            DelayCommand(30.0f, () =>
            {
                DequeueWeaponAbility(activator, ability.DisplaysActivationMessage);
            });
        }

        public void DequeueWeaponAbility(uint target, bool sendMessage = true)
        {
            var abilityId = GetLocalString(target, ActiveAbilityIdName);
            if (string.IsNullOrWhiteSpace(abilityId))
                return;

            var featId = GetLocalInt(target, ActiveAbilityFeatIdName);
            if (featId == 0)
                return;

            var featType = (FeatType)featId;
            var abilityDetail = GetAbilityDetail(featType);

            // Remove the local variables.
            DeleteLocalString(target, ActiveAbilityIdName);
            DeleteLocalInt(target, ActiveAbilityFeatIdName);
            DeleteLocalInt(target, ActiveAbilityEffectivePerkLevelName);

            // Notify the activator and nearby players
            SendMessageToPC(target, LocaleString.YourWeaponAbilityXIsNoLongerQueued.ToLocalizedString(abilityDetail.Name.ToLocalizedString()));

            if (sendMessage)
                Messaging.SendMessageNearbyToPlayers(target, LocaleString.PlayerNoLongerHasWeaponAbilityXReadied.ToLocalizedString(GetName(target), abilityDetail.Name.ToLocalizedString()));
        }

        private void ApplyRequirementEffects(uint activator, AbilityDetail ability)
        {
            _stat.ReduceEP(activator, ability.EPRequired);
        }
    }
}
