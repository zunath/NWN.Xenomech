using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(ParalyzeStatusEffect))]
    public class ParalyzeStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Paralyze;
        public override EffectIconType Icon => EffectIconType.XMParalyze;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 1f;

        public ParalyzeStatusEffect()
        {
            StatGroup.Stats[StatType.Paralysis] = 25;
        }
    }
}
