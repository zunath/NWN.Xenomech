using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(StarStrikeDebuffStatusEffect))]
    public class StarStrikeDebuffStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.StarStrike;
        public override EffectIconType Icon => EffectIconType.StarStrike;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 3f;

        public StarStrikeDebuffStatusEffect()
        {
            Stats[StatType.AccuracyModifier] = -20;
        }
    }
}
