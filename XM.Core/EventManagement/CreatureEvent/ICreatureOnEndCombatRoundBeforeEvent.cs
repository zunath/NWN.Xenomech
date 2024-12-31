namespace XM.Core.EventManagement.CreatureEvent
{
    public interface ICreatureOnEndCombatRoundBeforeEvent : IXMEvent
    {
        void CreatureOnEndCombatRoundBefore();
    }
}