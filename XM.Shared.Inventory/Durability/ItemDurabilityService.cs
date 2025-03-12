using Anvil.API;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;

namespace XM.Inventory.Durability
{
    [ServiceBinding(typeof(ItemDurabilityService))]
    internal class ItemDurabilityService
    {
        private readonly XMEventService _event;
        private readonly ItemTypeService _itemType;
        private const int DecayLossChance = 1;

        public ItemDurabilityService(
            XMEventService @event,
            ItemTypeService itemType)
        {
            _event = @event;
            _itemType = itemType;

            SubscribeEvents();
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
        }

        private void RestoreDurability(uint creature, uint item, int amount)
        {
            var durability = new ItemDurability(item);
            durability.CurrentDurability += amount;
            durability.SaveProperties();

            var itemName = GetName(item);
            var message = LocaleString.DurabilityMessage.ToLocalizedString(itemName, durability.CurrentDurability, durability.MaxDurability);
            SendMessageToPC(creature, ColorToken.Green(message));
        }

        [ScriptHandler("bread_test3")]
        public void DurabilityReduceTest()
        {
            var player = GetLastUsedBy();
            var item = GetItemInSlot(InventorySlotType.RightHand, player);

            ReduceDurability(player, item);
        }
    }
}
