namespace XM.Core.EventManagement.NWNXEvent
{
    public interface IClientDisconnectBeforeEvent: IXMEvent
    {
        void OnClientDisconnectBefore();
    }
}
