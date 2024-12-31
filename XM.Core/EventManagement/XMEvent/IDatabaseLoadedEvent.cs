namespace XM.Core.EventManagement.XMEvent
{
    public interface IDatabaseLoadedEvent: IXMEvent
    {
        void OnDatabaseLoaded();
    }
}
