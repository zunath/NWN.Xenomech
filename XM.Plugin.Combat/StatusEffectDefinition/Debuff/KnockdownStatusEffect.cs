using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(KnockdownStatusEffect))]
    public class KnockdownStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Knockdown;
        public override EffectIconType Icon => EffectIconType.Knockdown;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 0.25f;

        private const string EffectTag = "KNOCKDOWN_EFFECT";

        protected override void Apply(uint creature, int durationTicks)
        {
            var effect = EffectKnockdown();
            effect = TagEffect(effect, EffectTag);
            ApplyEffectToObject(DurationType.Temporary, effect, creature, Frequency * durationTicks);
        }

        protected override void Remove(uint creature)
        {
            RemoveEffectByTag(creature, EffectTag);
        }
    }
}
