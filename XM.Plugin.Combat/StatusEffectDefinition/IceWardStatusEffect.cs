using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(IceWardStatusEffect))]
    public class IceWardStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.IceWard;
        public override EffectIconType Icon => EffectIconType.IceWard;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        public IceWardStatusEffect()
        {
            Stats.Resists[ResistType.Ice] = 50;
        }
        protected override void Apply(uint creature)
        {
            ApplyEffectToObject(DurationType.Temporary, EffectVisualEffect(VisualEffectType.DurAuraPulseCyanBlue), creature, 2f);
        }
    }
}
