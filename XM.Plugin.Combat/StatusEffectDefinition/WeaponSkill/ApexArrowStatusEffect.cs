using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.WeaponSkill
{
    [ServiceBinding(typeof(ApexArrowStatusEffect))]
    public class ApexArrowStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.ApexArrow;
        public override EffectIconType Icon => EffectIconType.ApexArrow;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override StatusEffectActivationType ActivationType => StatusEffectActivationType.Passive;
        public override StatusEffectSourceType SourceType => StatusEffectSourceType.WeaponSkill;
        public override float Frequency => -1;

        public ApexArrowStatusEffect()
        {
            StatGroup.Stats[StatType.Haste] = 15;
        }
    }
}
