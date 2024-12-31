namespace XM.Core.EventManagement.NWNXEvent
{
    public interface IWebhookFailureEvent: IXMEvent
    {
        void OnWebhookFailure();
    }
}
