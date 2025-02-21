using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(StasisFieldStatusEffect))]
    public class StasisFieldStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.StasisField;
        public override EffectIconType Icon => EffectIconType.StasisField;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        public StasisFieldStatusEffect()
        {
            Stats[StatType.Evasion] = 40;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpImproveAbilityScore), creature);
        }
    }
}
