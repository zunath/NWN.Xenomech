using XM.Shared.Core.EventManagement;

namespace XM.Progression.Event
{
    internal class StatusEffectEvent
    {
        public struct OnApplyStatusEffect : IXMEvent
        {
        }

        public struct OnRemoveStatusEffect : IXMEvent
        {
        }

        public struct OnStatusEffectInterval : IXMEvent
        {
        }
    }
}
