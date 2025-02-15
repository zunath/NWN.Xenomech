using Anvil.Services;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.StatusEffect.StatusEffectDefinition
{
    [ServiceBinding(typeof(DarknessWardStatusEffect))]
    public class DarknessWardStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.DarknessWard;
        public override EffectIconType Icon => EffectIconType.DarknessWard;
        public override bool IsStackable => false;
        public override float Frequency => 60f;

        public DarknessWardStatusEffect()
        {
            Stats.Resists[ResistType.Darkness] = 50;
        }
    }
}
