using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(FeralHowlStatusEffect))]
    public class FeralHowlStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.FeralHowl;
        public override EffectIconType Icon => EffectIconType.FeralHowl;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 60f;

        public FeralHowlStatusEffect()
        {
            Stats[StatType.Accuracy] = 30;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpImproveAbilityScore), creature);
        }
    }
}
