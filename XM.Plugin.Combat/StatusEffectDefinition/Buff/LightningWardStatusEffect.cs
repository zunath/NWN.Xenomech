using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(LightningWardStatusEffect))]
    public class LightningWardStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.LightningWard;
        public override EffectIconType Icon => EffectIconType.LightningWard;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        public LightningWardStatusEffect()
        {
            StatGroup.Resists[ResistType.Lightning] = 50;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Temporary, EffectVisualEffect(VisualEffectType.DurAuraCyan), creature, 2f);
        }
    }
}
