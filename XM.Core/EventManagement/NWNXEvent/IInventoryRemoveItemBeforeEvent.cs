namespace XM.Core.EventManagement.NWNXEvent
{
    public interface IInventoryRemoveItemBeforeEvent: IXMEvent
    {
        void OnInventoryRemoveItemBefore();
    }
}
