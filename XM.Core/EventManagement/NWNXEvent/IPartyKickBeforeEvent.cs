namespace XM.Core.EventManagement.NWNXEvent
{
    public interface IPartyKickBeforeEvent: IXMEvent
    {
        void OnPartyKickBefore();
    }
}
