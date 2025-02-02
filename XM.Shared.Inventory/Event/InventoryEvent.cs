using XM.Inventory.KeyItem;
using XM.Shared.Core.EventManagement;

namespace XM.Inventory.Event
{
    public class InventoryEvent
    {
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
