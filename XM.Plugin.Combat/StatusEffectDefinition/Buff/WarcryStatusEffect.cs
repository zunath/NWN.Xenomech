using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(WarcryStatusEffect))]
    public class WarcryStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Warcry;
        public override EffectIconType Icon => EffectIconType.Warcry;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 60f;

        public WarcryStatusEffect()
        {
            StatGroup.Stats[StatType.Attack] = 25;
        }

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpImproveAbilityScore), creature);
        }
    }
}
