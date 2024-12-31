namespace XM.Core.EventManagement.NWNXEvent
{
    public interface IStoreRequestSellBeforeEvent: IXMEvent
    {
        void OnStoreRequestSellBefore();
    }
}
