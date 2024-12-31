namespace XM.Core.EventManagement.XMEvent
{
    public interface ICacheDataBeforeEvent: IXMEvent
    {
        void OnCacheDataBefore();
    }
}
