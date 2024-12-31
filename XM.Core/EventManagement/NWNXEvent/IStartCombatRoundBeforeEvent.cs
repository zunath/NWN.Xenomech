namespace XM.Core.EventManagement.NWNXEvent
{
    public interface IStartCombatRoundBeforeEvent: IXMEvent
    {
        void OnStartCombatRoundBefore();
    }
}
