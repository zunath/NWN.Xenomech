namespace XM.Core.EventManagement.NWNXEvent
{
    public interface ICastSpellBeforeEvent: IXMEvent
    {
        void OnCastSpellBefore();
    }
}
