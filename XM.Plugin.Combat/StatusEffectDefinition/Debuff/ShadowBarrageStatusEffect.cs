using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(ShadowBarrageStatusEffect))]
    public class ShadowBarrageStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.ShadowBarrage;
        public override EffectIconType Icon => EffectIconType.ShadowBarrage;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 3f;

        public ShadowBarrageStatusEffect()
        {
            StatGroup.Stats[StatType.AccuracyModifier] = -10;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpBlindDeafMedium), creature);
        }
    }
}