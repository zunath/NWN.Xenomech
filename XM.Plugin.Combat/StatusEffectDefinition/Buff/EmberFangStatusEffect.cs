using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(EmberFangStatusEffect))]
    public class EmberFangStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.EmberFang;
        public override EffectIconType Icon => EffectIconType.EmberFang;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 30f;

        public EmberFangStatusEffect()
        {
            StatGroup.Stats[StatType.Haste] = 20;
        }

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpImproveAbilityScore), creature);
        }
    }
}
