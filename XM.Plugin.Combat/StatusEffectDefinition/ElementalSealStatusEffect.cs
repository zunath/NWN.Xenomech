using Anvil.Services;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(ElementalSealStatusEffect))]
    public class ElementalSealStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.ElementalSeal;
        public override EffectIconType Icon => EffectIconType.ElementalSeal;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpGoodHelp), creature);
        }
    }
}
