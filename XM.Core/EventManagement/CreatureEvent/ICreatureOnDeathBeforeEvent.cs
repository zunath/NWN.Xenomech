namespace XM.Core.EventManagement.CreatureEvent
{
    public interface ICreatureOnDeathBeforeEvent : IXMEvent
    {
        void CreatureOnDeathBefore();
    }
}