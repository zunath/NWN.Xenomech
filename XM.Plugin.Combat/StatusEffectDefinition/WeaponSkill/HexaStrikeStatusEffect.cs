using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.WeaponSkill
{
    [ServiceBinding(typeof(HexaStrikeStatusEffect))]
    public class HexaStrikeStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.HexaStrike;
        public override EffectIconType Icon => EffectIconType.HexaStrike;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override StatusEffectActivationType ActivationType => StatusEffectActivationType.Passive;
        public override StatusEffectSourceType SourceType => StatusEffectSourceType.WeaponSkill;
        public override float Frequency => -1;

        public HexaStrikeStatusEffect()
        {
            StatGroup.Stats[StatType.HealingModifier] = 20;
        }
    }
}
