using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(SonicSlashStatusEffect))]
    public class SonicSlashStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.SonicSlash;
        public override EffectIconType Icon => EffectIconType.SonicSlash;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 3f;

        public SonicSlashStatusEffect()
        {
            StatGroup.Stats[StatType.EvasionModifier] = -10;
        }
    }
}
