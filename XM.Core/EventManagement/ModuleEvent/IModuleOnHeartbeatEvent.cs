namespace XM.Core.EventManagement.ModuleEvent
{
    public interface IModuleOnHeartbeatEvent : IXMEvent
    {
        void OnModuleHeartbeat();
    }
}
