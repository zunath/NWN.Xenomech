using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(ParalyzeStatusEffect))]
    public class ParalyzeStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Paralyze;
        public override EffectIconType Icon => EffectIconType.XMParalyze;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 1f;

        public ParalyzeStatusEffect()
        {
            Stats[StatType.Paralysis] = 25;
        }
    }
}
