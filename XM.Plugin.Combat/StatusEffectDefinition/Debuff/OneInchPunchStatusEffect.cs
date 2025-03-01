using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(OneInchPunchStatusEffect))]
    public class OneInchPunchStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.OneInchPunch;
        public override EffectIconType Icon => EffectIconType.OneInchPunch;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 3f;

        public OneInchPunchStatusEffect()
        {
            StatGroup.Stats[StatType.AttackModifier] = -15;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpReduceAbilityScore), creature);
        }
    }
}