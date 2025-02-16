using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(FlashStatusEffect))]
    public class FlashStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Flash;
        public override EffectIconType Icon => EffectIconType.Flash;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 1f;

        public FlashStatusEffect()
        {
            Stats[StatType.Accuracy] = -15;
        }
    }
}
