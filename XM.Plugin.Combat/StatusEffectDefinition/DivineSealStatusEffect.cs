using Anvil.Services;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(DivineSealStatusEffect))]
    public class DivineSealStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.DivineSeal;
        public override EffectIconType Icon => EffectIconType.DivineSeal;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpGoodHelp), creature);
        }
    }
}
