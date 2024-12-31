namespace XM.Core.EventManagement.XMEvent
{
    public interface IModuleContentChangedEvent: IXMEvent
    {
        void OnModuleContentChanged();
    }
}
