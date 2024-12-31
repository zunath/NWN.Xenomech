namespace XM.Core.EventManagement.NWNXEvent
{
    public interface IDmDumpLocalsBeforeEvent: IXMEvent
    {
        void OnDmDumpLocalsBefore();
    }
}
