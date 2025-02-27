using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.WeaponSkill
{
    [ServiceBinding(typeof(TrueShotStatusEffect))]
    public class TrueShotStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.TrueShot;
        public override EffectIconType Icon => EffectIconType.TrueShot;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override StatusEffectActivationType ActivationType => StatusEffectActivationType.Passive;
        public override StatusEffectSourceType SourceType => StatusEffectSourceType.WeaponSkill;
        public override float Frequency => -1;

        public TrueShotStatusEffect()
        {
            StatGroup.Stats[StatType.AccuracyModifier] = 20;
        }
    }
}
