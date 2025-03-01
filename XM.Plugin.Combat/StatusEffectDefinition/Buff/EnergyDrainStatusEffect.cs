using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(EnergyDrainStatusEffect))]
    public class EnergyDrainStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.EnergyDrain;
        public override EffectIconType Icon => EffectIconType.EnergyDrain;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        public EnergyDrainStatusEffect()
        {
            StatGroup.Stats[StatType.EPRestoreOnHit] = 1;
        }

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpImproveAbilityScore), creature);
        }
    }
}
