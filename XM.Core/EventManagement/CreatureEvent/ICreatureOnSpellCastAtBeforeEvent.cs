namespace XM.Core.EventManagement.CreatureEvent
{
    public interface ICreatureOnSpellCastAtBeforeEvent : IXMEvent
    {
        void CreatureOnSpellCastAtBefore();
    }
}