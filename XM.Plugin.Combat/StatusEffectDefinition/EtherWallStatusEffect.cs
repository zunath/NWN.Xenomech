using Anvil.Services;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(EtherWallStatusEffect))]
    public class EtherWallStatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.EtherWall;
        public override EffectIconType Icon => EffectIconType.EtherWall;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        protected override void Apply(uint creature)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpGlobeUse), creature);
        }
    }
}
