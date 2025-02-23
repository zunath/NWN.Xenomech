using Anvil.Services;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(PacifiedStatusEffect))]
    public class PacifiedStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Pacified;
        public override EffectIconType Icon => EffectIconType.Pacified;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 3f;

        private const string EffectTag = "PACIFIED_EFFECT";

        protected override void Apply(uint creature, int durationTicks)
        {
            var duration = durationTicks * Frequency;
            var effect = EffectSleep();
            effect = TagEffect(effect, EffectTag);

            ApplyEffectToObject(DurationType.Temporary, effect, creature, duration);
        }

        protected override void Remove(uint creature)
        {
            RemoveEffectByTag(creature, EffectTag);
        }
    }
}
