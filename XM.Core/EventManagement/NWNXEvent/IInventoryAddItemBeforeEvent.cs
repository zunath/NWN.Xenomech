namespace XM.Core.EventManagement.NWNXEvent
{
    public interface IInventoryAddItemBeforeEvent: IXMEvent
    {
        void OnInventoryAddItemBefore();
    }
}
