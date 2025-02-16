using Anvil.Services;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(HundredFistsStatusEffect))]
    public class HundredFistsStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.HundredFists;
        public override EffectIconType Icon => EffectIconType.HundredFists;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        protected override void Apply(uint creature)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpImproveAbilityScore), creature);
        }
    }
}
