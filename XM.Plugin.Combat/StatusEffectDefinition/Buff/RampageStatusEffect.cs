using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(RampageStatusEffect))]
    public class RampageStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Rampage;
        public override EffectIconType Icon => EffectIconType.Rampage;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 30f;

        public RampageStatusEffect()
        {
            StatGroup.Stats[StatType.Haste] = 20;
        }

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpImproveAbilityScore), creature);
        }
    }
}
