using Anvil.Services;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.StatusEffect.StatusEffectDefinition
{
    [ServiceBinding(typeof(LightWardStatusEffect))]
    public class LightWardStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.LightWard;
        public override EffectIconType Icon => EffectIconType.LightWard;
        public override bool IsStackable => false;
        public override float Frequency => 1000f;

        public LightWardStatusEffect()
        {
            Stats.Resists[ResistType.Light] = 50;
        }
    }
}
