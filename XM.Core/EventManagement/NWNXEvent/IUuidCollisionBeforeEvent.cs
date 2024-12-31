namespace XM.Core.EventManagement.NWNXEvent
{
    public interface IUuidCollisionBeforeEvent: IXMEvent
    {
        void OnUuidCollisionBefore();
    }
}
