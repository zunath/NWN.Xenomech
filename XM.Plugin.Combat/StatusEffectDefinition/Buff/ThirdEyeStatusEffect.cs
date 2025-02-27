using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(ThirdEyeStatusEffect))]
    public class ThirdEyeStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.ThirdEye;
        public override EffectIconType Icon => EffectIconType.ThirdEye;
        public override StatusEffectStackType StackingType => StatusEffectStackType.UnlimitedStacking;
        public override float Frequency => 60f;

        public ThirdEyeStatusEffect()
        {
            StatGroup.Stats[StatType.DamageReduction] = 10;
        }

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Temporary, EffectVisualEffect(VisualEffectType.DurAuraWhite), creature, 2f);
        }
    }
}
