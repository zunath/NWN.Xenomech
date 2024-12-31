namespace XM.Core.EventManagement.CreatureEvent
{
    public interface ICreatureOnMeleeAttackedBeforeEvent : IXMEvent
    {
        void CreatureOnMeleeAttackedBefore();
    }
}