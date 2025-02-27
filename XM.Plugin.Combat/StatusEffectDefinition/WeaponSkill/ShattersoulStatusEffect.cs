using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.WeaponSkill
{
    [ServiceBinding(typeof(ShattersoulStatusEffect))]
    public class ShattersoulStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Shattersoul;
        public override EffectIconType Icon => EffectIconType.Shattersoul;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override StatusEffectActivationType ActivationType => StatusEffectActivationType.Passive;
        public override StatusEffectSourceType SourceType => StatusEffectSourceType.WeaponSkill;
        public override float Frequency => -1;

        public ShattersoulStatusEffect()
        {
            StatGroup.Stats[StatType.EPRestoreOnHit] = 2;
        }
    }
}
