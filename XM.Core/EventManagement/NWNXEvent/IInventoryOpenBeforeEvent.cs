namespace XM.Core.EventManagement.NWNXEvent
{
    public interface IInventoryOpenBeforeEvent: IXMEvent
    {
        void OnInventoryOpenBefore();
    }
}
