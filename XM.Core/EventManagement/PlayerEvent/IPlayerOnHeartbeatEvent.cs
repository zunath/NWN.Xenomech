namespace XM.Core.EventManagement.PlayerEvent
{
    public interface IPlayerOnHeartbeatEvent: IXMEvent
    {
        void PlayerOnHeartbeat();
    }
}