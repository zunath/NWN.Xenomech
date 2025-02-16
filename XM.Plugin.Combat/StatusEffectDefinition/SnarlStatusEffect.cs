using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(SnarlStatusEffect))]
    public class SnarlStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Snarl;
        public override EffectIconType Icon => EffectIconType.Snarl;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 60f;

        public SnarlStatusEffect()
        {
            Stats[StatType.Accuracy] = -15;
        }
        protected override void Apply(uint creature)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpReduceAbilityScore), creature);
        }
    }
}
