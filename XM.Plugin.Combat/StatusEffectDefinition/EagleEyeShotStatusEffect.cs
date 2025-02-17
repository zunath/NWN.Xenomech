using Anvil.Services;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(EagleEyeShotStatusEffect))]
    public class EagleEyeShotStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.EagleEyeShot;
        public override EffectIconType Icon => EffectIconType.EagleEyeShot;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        protected override void Apply(uint creature)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpImproveAbilityScore), creature);
        }
    }
}
