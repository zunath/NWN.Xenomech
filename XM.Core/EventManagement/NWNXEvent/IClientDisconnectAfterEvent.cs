namespace XM.Core.EventManagement.NWNXEvent
{
    public interface IClientDisconnectAfterEvent: IXMEvent
    {
        void OnClientDisconnectAfter();
    }
}
