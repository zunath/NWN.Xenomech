using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(ShockSlashStatusEffect))]
    public class ShockSlashStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.ShockSlash;
        public override EffectIconType Icon => EffectIconType.ShockSlash;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 3f;

        public ShockSlashStatusEffect()
        {
            StatGroup.Stats[StatType.EtherDefenseModifier] = -10;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpReduceAbilityScore), creature);
        }
    }
}