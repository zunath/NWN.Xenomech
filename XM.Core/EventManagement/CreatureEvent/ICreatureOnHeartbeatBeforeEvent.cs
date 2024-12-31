namespace XM.Core.EventManagement.CreatureEvent
{
    public interface ICreatureOnHeartbeatBeforeEvent : IXMEvent
    {
        void CreatureOnHeartbeatBefore();
    }
}