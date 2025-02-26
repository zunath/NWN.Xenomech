using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(DrakesbaneDebuffStatusEffect))]
    public class DrakesbaneDebuffStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Drakesbane;
        public override EffectIconType Icon => EffectIconType.Drakesbane;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 3f;

        public DrakesbaneDebuffStatusEffect()
        {
            Stats[StatType.EtherDefenseModifier] = -20;
        }
    }
}
