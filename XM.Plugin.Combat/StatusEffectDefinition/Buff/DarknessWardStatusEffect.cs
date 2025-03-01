using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(DarknessWardStatusEffect))]
    public class DarknessWardStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.DarknessWard;
        public override EffectIconType Icon => EffectIconType.DarknessWard;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        public DarknessWardStatusEffect()
        {
            StatGroup.Resists[ResistType.Darkness] = 50;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Temporary, EffectVisualEffect(VisualEffectType.DurAuraPulseBlueBlack), creature, 2f);
        }
    }
}
