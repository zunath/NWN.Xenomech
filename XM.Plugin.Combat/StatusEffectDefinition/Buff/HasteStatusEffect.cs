using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(HasteStatusEffect))]
    public class HasteStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Haste;
        public override EffectIconType Icon => EffectIconType.XMHaste;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 180f;

        public HasteStatusEffect()
        {
            StatGroup.Stats[StatType.Haste] = 15;
        }

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpHaste), creature);
        }
    }
}
