using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(ShadowstitchStatusEffect))]
    public class ShadowstitchStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Shadowstitch;
        public override EffectIconType Icon => EffectIconType.Shadowstitch;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 3f;

        public ShadowstitchStatusEffect()
        {
            StatGroup.Stats[StatType.Slow] = 40;
        }

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpSlow), creature);
        }
    }
}
