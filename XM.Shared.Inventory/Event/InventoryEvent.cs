using XM.Inventory.KeyItem;
using XM.Shared.API.Constants;
using XM.Shared.Core.EventManagement;

namespace XM.Inventory.Event
{
    public class InventoryEvent
    {
        public struct ItemDurabilityChangedEvent : IXMEvent
        {
            public uint Item { get; }
            public InventorySlotType Slot { get; }
            public float NewCondition { get; }

            public ItemDurabilityChangedEvent(uint item, InventorySlotType slot, float newCondition)
            {
                Item = item;
                Slot = slot;
                NewCondition = newCondition;
            }
        }

        public struct GiveKeyItemEvent: IXMEvent
        {
            public KeyItemType KeyItem { get; }
            public GiveKeyItemEvent(KeyItemType keyItem)
            {
                KeyItem = keyItem;
            }
        }
        public struct RemoveKeyItemEvent: IXMEvent
        {
            public KeyItemType KeyItem { get; }
            public RemoveKeyItemEvent(KeyItemType keyItem)
            {
                KeyItem = keyItem;
            }
        }
    }
}
