using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.WeaponSkill
{
    [ServiceBinding(typeof(DancingEdgeStatusEffect))]
    public class DancingEdgeStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.DancingEdge;
        public override EffectIconType Icon => EffectIconType.DancingEdge;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override StatusEffectActivationType ActivationType => StatusEffectActivationType.Passive;
        public override StatusEffectSourceType SourceType => StatusEffectSourceType.WeaponSkill;
        public override float Frequency => -1;

        public DancingEdgeStatusEffect()
        {
            Stats[StatType.TPGainModifier] = 20;
        }
    }
}
