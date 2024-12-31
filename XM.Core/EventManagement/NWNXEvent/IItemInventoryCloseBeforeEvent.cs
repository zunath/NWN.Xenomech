namespace XM.Core.EventManagement.NWNXEvent
{
    public interface IItemInventoryCloseBeforeEvent: IXMEvent
    {
        void OnItemInventoryCloseBefore();
    }
}
