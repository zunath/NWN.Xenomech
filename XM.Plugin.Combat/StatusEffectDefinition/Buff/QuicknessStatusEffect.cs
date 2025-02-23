using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(QuicknessStatusEffect))]
    public class QuicknessStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Quickness;
        public override EffectIconType Icon => EffectIconType.Quickness;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f * 5f;

        public QuicknessStatusEffect()
        {
            Stats[StatType.Haste] = 10;
        }

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpHaste), creature);
        }
    }
}
