namespace XM.Core.EventManagement.NWNXEvent
{
    public interface ILevelUpBeforeEvent: IXMEvent
    {
        void OnLevelUpBefore();
    }
}
