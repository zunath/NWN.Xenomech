using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.WeaponSkill
{
    [ServiceBinding(typeof(AsuranFistsStatusEffect))]
    public class AsuranFistsStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Upheaval;
        public override EffectIconType Icon => EffectIconType.Upheaval;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override StatusEffectActivationType ActivationType => StatusEffectActivationType.Passive;
        public override StatusEffectSourceType SourceType => StatusEffectSourceType.WeaponSkill;
        public override float Frequency => -1;

        public AsuranFistsStatusEffect()
        {
            StatGroup.Stats[StatType.TPCostModifier] = -20;
        }
    }
}
