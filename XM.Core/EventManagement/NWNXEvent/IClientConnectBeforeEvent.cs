namespace XM.Core.EventManagement.NWNXEvent
{
    public interface IClientConnectBeforeEvent: IXMEvent
    {
        void OnClientConnectBefore();
    }
}
