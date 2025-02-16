using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(MindWardStatusEffect))]
    public class MindWardStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.MindWard;
        public override EffectIconType Icon => EffectIconType.MindWard;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        public MindWardStatusEffect()
        {
            Stats.Resists[ResistType.Mind] = 50;
        }
        protected override void Apply(uint creature)
        {
            ApplyEffectToObject(DurationType.Temporary, EffectVisualEffect(VisualEffectType.DurAuraRedLight), creature, 2f);
        }
    }
}
