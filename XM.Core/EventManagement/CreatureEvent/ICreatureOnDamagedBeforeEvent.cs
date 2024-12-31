namespace XM.Core.EventManagement.CreatureEvent
{
    public interface ICreatureOnDamagedBeforeEvent : IXMEvent
    {
        void CreatureOnDamagedBefore();
    }
}