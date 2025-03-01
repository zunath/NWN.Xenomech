using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(DeadeyeStatusEffect))]
    public class DeadeyeStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Deadeye;
        public override EffectIconType Icon => EffectIconType.Deadeye;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 30f;

        public DeadeyeStatusEffect()
        {
            StatGroup.Stats[StatType.CriticalRate] = 5;
        }

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpImproveAbilityScore), creature);
        }
    }
}
