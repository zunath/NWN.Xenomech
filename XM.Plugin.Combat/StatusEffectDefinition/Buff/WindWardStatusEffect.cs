using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(WindWardStatusEffect))]
    public class WindWardStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.WindWard;
        public override EffectIconType Icon => EffectIconType.WindWard;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        public WindWardStatusEffect()
        {
            Stats.Resists[ResistType.Wind] = 50;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Temporary, EffectVisualEffect(VisualEffectType.DurAuraGreen), creature, 2f);
        }
    }
}
