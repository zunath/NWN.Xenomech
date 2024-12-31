namespace XM.Core.EventManagement.NWNXEvent
{
    public interface IBroadcastCastSpellBeforeEvent: IXMEvent
    {
        void OnBroadcastCastSpellBefore();
    }
}
