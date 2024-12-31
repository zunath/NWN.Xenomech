namespace XM.Core.EventManagement.NWNXEvent
{
    public interface IEffectAppliedBeforeEvent: IXMEvent
    {
        void OnEffectAppliedBefore();
    }
}
