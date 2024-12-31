namespace XM.Core.EventManagement.NWNXEvent
{
    public interface IStoreRequestBuyBeforeEvent: IXMEvent
    {
        void OnStoreRequestBuyBefore();
    }
}
