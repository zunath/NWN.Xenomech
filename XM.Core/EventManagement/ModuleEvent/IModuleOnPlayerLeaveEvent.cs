namespace XM.Core.EventManagement.ModuleEvent
{
    public interface IModuleOnPlayerLeaveEvent : IXMEvent
    {
        void OnModuleClientExit();
    }
}
