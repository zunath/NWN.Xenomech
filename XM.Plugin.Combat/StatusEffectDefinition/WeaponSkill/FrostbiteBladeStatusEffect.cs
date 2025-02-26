using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.WeaponSkill
{
    [ServiceBinding(typeof(FrostbiteBladeStatusEffect))]
    public class FrostbiteBladeStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.FrostbiteBlade;
        public override EffectIconType Icon => EffectIconType.FrostbiteBlade;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override StatusEffectActivationType ActivationType => StatusEffectActivationType.Passive;
        public override StatusEffectSourceType SourceType => StatusEffectSourceType.WeaponSkill;
        public override float Frequency => -1;

        public FrostbiteBladeStatusEffect()
        {
            Stats[StatType.Haste] = 10;
        }
    }
}
