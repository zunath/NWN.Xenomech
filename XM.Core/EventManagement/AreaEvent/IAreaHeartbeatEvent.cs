namespace XM.Core.EventManagement.AreaEvent
{
    public interface IAreaHeartbeatEvent : IXMEvent
    {
        void OnAreaHeartbeat();
    }
}