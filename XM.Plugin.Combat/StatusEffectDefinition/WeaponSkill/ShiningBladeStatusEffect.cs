using Anvil.Services;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.WeaponSkill
{
    [ServiceBinding(typeof(ShiningBladeStatusEffect))]
    public class ShiningBladeStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.ShiningBlade;
        public override EffectIconType Icon => EffectIconType.ShiningBlade;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override StatusEffectActivationType ActivationType => StatusEffectActivationType.OnHit;
        public override StatusEffectSourceType SourceType => StatusEffectSourceType.WeaponSkill;
        public override float Frequency => -1;
    }
}
