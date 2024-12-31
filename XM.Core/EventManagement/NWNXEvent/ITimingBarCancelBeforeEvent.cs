namespace XM.Core.EventManagement.NWNXEvent
{
    public interface ITimingBarCancelBeforeEvent: IXMEvent
    {
        void OnTimingBarCancelBefore();
    }
}
