using Anvil.Services;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(PerfectDodgeStatusEffect))]
    public class PerfectDodgeStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.PerfectDodge;
        public override EffectIconType Icon => EffectIconType.PerfectDodge;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpPdkRallyingCry), creature);
        }
    }
}
