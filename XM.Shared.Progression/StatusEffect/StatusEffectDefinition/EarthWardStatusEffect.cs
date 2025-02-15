using Anvil.Services;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.StatusEffect.StatusEffectDefinition
{
    [ServiceBinding(typeof(EarthWardStatusEffect))]
    public class EarthWardStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.EarthWard;
        public override EffectIconType Icon => EffectIconType.EarthWard;
        public override bool IsStackable => false;
        public override float Frequency => 1000f;

        public EarthWardStatusEffect()
        {
            Stats.Resists[ResistType.Earth] = 50;
        }
    }
}
