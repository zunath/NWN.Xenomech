using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(FrostDartStatusEffect))]
    public class FrostDartStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.FrostDart;
        public override EffectIconType Icon => EffectIconType.FrostDart;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 3f;

        public FrostDartStatusEffect()
        {
            StatGroup.Stats[StatType.Slow] = 25;
        }

        private const string EffectTag = "FROST_DART_EFFECT";

        protected override void Apply(uint creature, int durationTicks)
        {
            var effect = EffectMovementSpeedDecrease(25);
            effect = TagEffect(effect, EffectTag);

            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpSlow), creature);
            AssignCommand(Source, () =>
            {
                ApplyEffectToObject(DurationType.Temporary, effect, creature, durationTicks * Frequency);
            });
        }

        protected override void Remove(uint creature)
        {
            RemoveEffectByTag(creature, EffectTag);
        }
    }
}
