namespace XM.Core.EventManagement.NWNXEvent
{
    public interface IHealBeforeEvent: IXMEvent
    {
        void OnHealBefore();
    }
}
