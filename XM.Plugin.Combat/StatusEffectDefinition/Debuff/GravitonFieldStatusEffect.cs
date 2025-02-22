using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(GravitonFieldStatusEffect))]
    public class GravitonFieldStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.GravitonField;
        public override EffectIconType Icon => EffectIconType.GravitonField;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 1f;

        private const string EffectTag = "GRAVITON_FIELD_EFFECT";

        public GravitonFieldStatusEffect()
        {
            Stats[StatType.Evasion] = -25;
        }

        protected override void Apply(uint creature, int durationTicks)
        {
            var effect = EffectMovementSpeedDecrease(25);
            effect = TagEffect(effect, EffectTag);
            ApplyEffectToObject(DurationType.Temporary, effect, creature, durationTicks * Frequency);

            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpReduceAbilityScore), creature);
        }

        protected override void Remove(uint creature)
        {
            RemoveEffectByTag(creature, EffectTag);
        }
    }
}
