using Anvil.Services;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.StatusEffect.StatusEffectDefinition
{
    [ServiceBinding(typeof(IceWardStatusEffect))]
    public class IceWardStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.IceWard;
        public override EffectIconType Icon => EffectIconType.IceWard;
        public override bool IsStackable => false;
        public override float Frequency => 1000f;

        public IceWardStatusEffect()
        {
            Stats.Resists[ResistType.Ice] = 50;
        }
    }
}
