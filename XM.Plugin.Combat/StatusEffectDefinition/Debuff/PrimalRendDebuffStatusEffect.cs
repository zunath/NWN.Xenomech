using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(PrimalRendDebuffStatusEffect))]
    public class PrimalRendDebuffStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.PrimalRend;
        public override EffectIconType Icon => EffectIconType.PrimalRend;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 3f;

        public PrimalRendDebuffStatusEffect()
        {
            StatGroup.Stats[StatType.DefenseModifier] = -20;
        }
    }
}
