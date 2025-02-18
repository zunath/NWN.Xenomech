using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(ZephyrShroudStatusEffect))]
    public class ZephyrShroudStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.ZephyrShroud;
        public override EffectIconType Icon => EffectIconType.ZephyrShroud;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 120f;

        public ZephyrShroudStatusEffect()
        {
            Stats[StatType.Evasion] = 30;
        }
        protected override void Apply(uint creature)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpElementalProtection), creature);
        }
    }
}
