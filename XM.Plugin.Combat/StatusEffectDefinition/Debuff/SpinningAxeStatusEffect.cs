using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(SpinningAxeStatusEffect))]
    public class SpinningAxeStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.SpinningAxe;
        public override EffectIconType Icon => EffectIconType.SpinningAxe;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 3f;

        public SpinningAxeStatusEffect()
        {
            StatGroup.Stats[StatType.EvasionModifier] = -15;
        }

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpReduceAbilityScore), creature);
        }
    }
}
