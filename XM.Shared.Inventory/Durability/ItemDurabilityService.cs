using Anvil.API;
using Anvil.Services;
using XM.Inventory.Durability.UI;
using XM.Inventory.Event;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;
using XM.UI;

namespace XM.Inventory.Durability
{
    [ServiceBinding(typeof(ItemDurabilityService))]
    internal class ItemDurabilityService
    {
        private readonly XMEventService _event;
        private readonly ItemTypeService _itemType;
        private readonly GuiService _gui;

        private const int DecayLossChance = 1;

        public ItemDurabilityService(
            XMEventService @event,
            ItemTypeService itemType,
            GuiService gui)
        {
            _event = @event;
            _itemType = itemType;
            _gui = gui;

            RegisterEvents();
            SubscribeEvents();
        }

        private void RegisterEvents()
        {
            _event.RegisterEvent<InventoryEvent.ItemDurabilityChangedEvent>(InventoryEventScript.DurabilityChangedScript);
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<XMEvent.OnDamageDealt>(OnDamageDealt);
        }

        private void OnDamageDealt(uint attacker)
        {
            var data = _event.GetEventData<XMEvent.OnDamageDealt>();
            var defender = data.Target;

            if (GetIsPC(attacker))
            {
                RunDecayLossChance(attacker, GetItemInSlot(InventorySlotType.RightHand, attacker));

                var leftHand = GetItemInSlot(InventorySlotType.LeftHand, attacker);
                if(!_itemType.IsShield(leftHand))
                    RunDecayLossChance(attacker, leftHand);
            }

            if (GetIsPC(defender))
            {
                var rightHand = GetItemInSlot(InventorySlotType.RightHand, defender);
                if(_itemType.IsShield(rightHand))
                    RunDecayLossChance(defender, rightHand);

                RunDecayLossChance(defender, GetItemInSlot(InventorySlotType.Head, defender));
                RunDecayLossChance(defender, GetItemInSlot(InventorySlotType.Chest, defender));
                RunDecayLossChance(defender, GetItemInSlot(InventorySlotType.Arms, defender));
                RunDecayLossChance(defender, GetItemInSlot(InventorySlotType.Boots, defender));
                RunDecayLossChance(defender, GetItemInSlot(InventorySlotType.Neck, defender));
                RunDecayLossChance(defender, GetItemInSlot(InventorySlotType.RightRing, defender));
                RunDecayLossChance(defender, GetItemInSlot(InventorySlotType.LeftRing, defender));
                RunDecayLossChance(defender, GetItemInSlot(InventorySlotType.Cloak, defender));
                RunDecayLossChance(defender, GetItemInSlot(InventorySlotType.Belt, defender));
            }
        }

        private void RunDecayLossChance(uint creature, uint item)
        {
            if (!GetIsObjectValid(item))
                return;

            if (XMRandom.D100(1) > DecayLossChance)
                return;

            ReduceDurability(creature, item);
        }

        private void ReduceDurability(uint creature, uint item)
        {
            var durability = new ItemDurability(item);
            if (durability.MaxDurability <= 0)
                return;

            durability.CurrentDurability--;
            durability.SaveProperties();

            var itemName = GetName(item);
            var message = LocaleString.DurabilityMessage.ToLocalizedString(itemName, durability.CurrentDurability, durability.MaxDurability);
            SendMessageToPC(creature, ColorToken.Red(message));

            var slot = GetInventorySlot(creature, item);
            _event.PublishEvent(creature, new InventoryEvent.ItemDurabilityChangedEvent(item, slot, durability.Condition));
        }

        internal void RestoreDurability(uint creature, uint item, int amount)
        {
            var durability = new ItemDurability(item);
            durability.CurrentDurability += amount;
            durability.SaveProperties();

            var itemName = GetName(item);
            var message = LocaleString.DurabilityMessage.ToLocalizedString(itemName, durability.CurrentDurability, durability.MaxDurability);
            SendMessageToPC(creature, ColorToken.Green(message));

            var slot = GetInventorySlot(creature, item);
            _event.PublishEvent(creature, new InventoryEvent.ItemDurabilityChangedEvent(item, slot, durability.Condition));
        }

        private InventorySlotType GetInventorySlot(uint creature, uint item)
        {
            for (var index = 0; index < GeneralConstants.NumberOfInventorySlots; index++)
            {
                var itemSlot = (InventorySlotType)index;
                if (GetItemInSlot(itemSlot, creature) == item)
                {
                    return itemSlot;
                }
            }

            return InventorySlotType.Invalid;
        }

        [ScriptHandler("repair_terminal")]
        public void OpenRepairWindow()
        {
            var player = GetLastUsedBy();
            if (!GetIsPC(player) || GetIsDM(player))
            {
                var message = LocaleString.OnlyPlayersMayUseThisItem.ToLocalizedString();
                SendMessageToPC(player, message);
                return;
            }

            _gui.ShowWindow<ItemRepairView>(player, null, OBJECT_SELF);
        }

        [ScriptHandler("bread_test3")]
        public void DurabilityReduceTest()
        {
            var player = GetLastUsedBy();
            var item = GetItemInSlot(InventorySlotType.Chest, player);

            ReduceDurability(player, item);
        }
    }
}
