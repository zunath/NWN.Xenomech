using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(AtonementStatusEffect))]
    public class AtonementStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Atonement;
        public override EffectIconType Icon => EffectIconType.Atonement;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        public AtonementStatusEffect()
        {
            StatGroup.Stats[StatType.TPGainModifier] = 10;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpImproveAbilityScore), creature);
        }
    }
}
