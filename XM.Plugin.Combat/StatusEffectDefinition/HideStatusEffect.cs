using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(HideStatusEffect))]
    public class HideStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Hide;
        public override EffectIconType Icon => EffectIconType.Hide;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        private const string EffectTag = "HIDE_EFFECT";

        protected override void Apply(uint creature, int durationTicks)
        {
            var effect = EffectInvisibility(InvisibilityType.Normal);
            effect = TagEffect(effect, EffectTag);
            ApplyEffectToObject(DurationType.Temporary, effect, creature, 60f);
        }

        protected override void Remove(uint creature)
        {
            RemoveEffectByTag(creature, EffectTag);
        }
    }
}
