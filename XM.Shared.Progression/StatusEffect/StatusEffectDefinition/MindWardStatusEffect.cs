using Anvil.Services;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.StatusEffect.StatusEffectDefinition
{
    [ServiceBinding(typeof(MindWardStatusEffect))]
    public class MindWardStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.MindWard;
        public override EffectIconType Icon => EffectIconType.MindWard;
        public override bool IsStackable => false;
        public override float Frequency => 1000f;

        public MindWardStatusEffect()
        {
            Stats.Resists[ResistType.Mind] = 50;
        }
    }
}
