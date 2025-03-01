using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(LightWardStatusEffect))]
    public class LightWardStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.LightWard;
        public override EffectIconType Icon => EffectIconType.LightWard;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        public LightWardStatusEffect()
        {
            StatGroup.Resists[ResistType.Light] = 50;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Temporary, EffectVisualEffect(VisualEffectType.DurAuraPulseRedWhite), creature, 2f);
        }
    }
}
