using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(OmniscienceStatusEffect))]
    public class OmniscienceStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Omniscience;
        public override EffectIconType Icon => EffectIconType.Omniscience;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        public OmniscienceStatusEffect()
        {
            StatGroup.Stats[StatType.EtherDefenseModifier] = 10;
        }

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpImproveAbilityScore), creature);
        }
    }
}
