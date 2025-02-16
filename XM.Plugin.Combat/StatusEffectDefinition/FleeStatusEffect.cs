using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(FleeStatusEffect))]
    public class FleeStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Flee;
        public override EffectIconType Icon => EffectIconType.Flee;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 30f;

        private const string EffectTag = "FLEE_EFFECT";

        public FleeStatusEffect()
        {
            Stats[StatType.Defense] = 30;
        }

        protected override void Apply(uint creature)
        {
            var effect = EffectMovementSpeedIncrease(60);
            effect = TagEffect(effect, EffectTag);

            ApplyEffectToObject(DurationType.Temporary, effect, creature, 30f);
        }
    }
}
