namespace XM.Core.EventManagement.NWNXEvent
{
    public interface IWebhookSuccessEvent: IXMEvent
    {
        void OnWebhookSuccess();
    }
}
