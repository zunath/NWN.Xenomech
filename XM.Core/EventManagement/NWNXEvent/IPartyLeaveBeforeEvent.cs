namespace XM.Core.EventManagement.NWNXEvent
{
    public interface IPartyLeaveBeforeEvent: IXMEvent
    {
        void OnPartyLeaveBefore();
    }
}
