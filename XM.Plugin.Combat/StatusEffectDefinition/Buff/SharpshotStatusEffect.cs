using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(SharpshotStatusEffect))]
    public class SharpshotStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Sharpshot;
        public override EffectIconType Icon => EffectIconType.Sharpshot;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 120f;

        public SharpshotStatusEffect()
        {
            StatGroup.Stats[StatType.Accuracy] = 20;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpImproveAbilityScore), creature);
        }
    }
}
