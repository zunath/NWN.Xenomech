namespace XM.Core.EventManagement.ModuleEvent
{
    public interface IModuleOnUnacquireItemEvent : IXMEvent
    {
        void OnModuleLoseItem();
    }
}
