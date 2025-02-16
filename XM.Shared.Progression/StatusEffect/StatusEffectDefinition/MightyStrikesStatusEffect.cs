using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.StatusEffect.StatusEffectDefinition
{
    [ServiceBinding(typeof(MightyStrikesStatusEffect))]
    public class MightyStrikesStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.MightyStrikes;
        public override EffectIconType Icon => EffectIconType.MightyStrikes;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        protected override void Apply(uint creature)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpPdkRallyingCry), creature);
        }
    }
}
