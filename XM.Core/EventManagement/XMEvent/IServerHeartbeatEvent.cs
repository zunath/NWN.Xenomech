namespace XM.Core.EventManagement.XMEvent
{
    public interface IServerHeartbeatEvent: IXMEvent
    {
        void OnXMServerHeartbeat();
    }
}
