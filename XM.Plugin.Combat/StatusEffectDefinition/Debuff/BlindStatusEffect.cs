using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(BlindStatusEffect))]
    public class BlindStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Blind;
        public override EffectIconType Icon => EffectIconType.XMBlind;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 3f;

        public BlindStatusEffect()
        {
            StatGroup.Stats[StatType.AccuracyModifier] = -25;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpBlindDeafMedium), creature);
        }
    }
}