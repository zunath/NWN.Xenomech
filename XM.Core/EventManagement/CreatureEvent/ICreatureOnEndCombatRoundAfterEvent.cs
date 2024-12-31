namespace XM.Core.EventManagement.CreatureEvent
{
    public interface ICreatureOnEndCombatRoundAfterEvent : IXMEvent
    {
        void CreatureOnEndCombatRoundAfter();
    }
}