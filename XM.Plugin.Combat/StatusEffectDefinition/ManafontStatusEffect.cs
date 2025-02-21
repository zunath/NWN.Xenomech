using Anvil.Services;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(ManafontStatusEffect))]
    public class ManafontStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Manafont;
        public override EffectIconType Icon => EffectIconType.Manafont;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpGoodHelp), creature);
            SetLocalBool(creature, "MANAFONT", true);
        }

        protected override void Remove(uint creature)
        {
            DeleteLocalBool(creature, "MANAFONT");
        }
    }
}
