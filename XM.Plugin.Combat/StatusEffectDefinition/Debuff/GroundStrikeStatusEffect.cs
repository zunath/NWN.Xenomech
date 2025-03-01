using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(GroundStrikeStatusEffect))]
    public class GroundStrikeStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.GroundStrike;
        public override EffectIconType Icon => EffectIconType.GroundStrike;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 3f;

        public GroundStrikeStatusEffect()
        {
            StatGroup.Stats[StatType.DefenseModifier] = -15;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpReduceAbilityScore), creature);
        }
    }
}