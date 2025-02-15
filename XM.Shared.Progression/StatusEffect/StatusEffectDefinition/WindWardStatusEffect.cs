using Anvil.Services;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.StatusEffect.StatusEffectDefinition
{
    [ServiceBinding(typeof(WindWardStatusEffect))]
    public class WindWardStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.WindWard;
        public override EffectIconType Icon => EffectIconType.WindWard;
        public override bool IsStackable => false;
        public override float Frequency => 1000f;

        public WindWardStatusEffect()
        {
            Stats.Resists[ResistType.Wind] = 50;
        }
    }
}
