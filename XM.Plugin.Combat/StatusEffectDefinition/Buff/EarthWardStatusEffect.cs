using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(EarthWardStatusEffect))]
    public class EarthWardStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.EarthWard;
        public override EffectIconType Icon => EffectIconType.EarthWard;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        public EarthWardStatusEffect()
        {
            StatGroup.Resists[ResistType.Earth] = 50;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Temporary, EffectVisualEffect(VisualEffectType.DurAuraOrange), creature, 2f);
        }
    }
}
