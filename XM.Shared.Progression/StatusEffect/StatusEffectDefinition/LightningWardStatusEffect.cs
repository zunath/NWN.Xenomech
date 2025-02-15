using Anvil.Services;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.StatusEffect.StatusEffectDefinition
{
    [ServiceBinding(typeof(LightningWardStatusEffect))]
    public class LightningWardStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.LightningWard;
        public override EffectIconType Icon => EffectIconType.LightningWard;
        public override bool IsStackable => false;
        public override float Frequency => 1000f;

        public LightningWardStatusEffect()
        {
            Stats.Resists[ResistType.Lightning] = 50;
        }
    }
}
