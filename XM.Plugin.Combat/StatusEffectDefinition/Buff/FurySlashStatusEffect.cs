using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(FurySlashStatusEffect))]
    public class FurySlashStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.FurySlash;
        public override EffectIconType Icon => EffectIconType.FurySlash;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 30f;

        public FurySlashStatusEffect()
        {
            StatGroup.Stats[StatType.AttackModifier] = 10;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpImproveAbilityScore), creature);
        }
    }
}