using XM.Shared.Core.EventManagement;

namespace XM.Plugin.Combat.Event
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
