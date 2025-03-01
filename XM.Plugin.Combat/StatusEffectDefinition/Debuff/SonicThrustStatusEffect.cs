using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(SonicThrustStatusEffect))]
    public class SonicThrustStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.SonicThrust;
        public override EffectIconType Icon => EffectIconType.SonicThrust;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 3f;

        public SonicThrustStatusEffect()
        {
            StatGroup.Stats[StatType.EtherDefenseModifier] = -20;
        }
    }
}
