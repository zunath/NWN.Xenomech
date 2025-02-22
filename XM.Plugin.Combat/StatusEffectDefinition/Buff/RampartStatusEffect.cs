using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(RampartStatusEffect))]
    public class RampartStatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.Rampart;
        public override EffectIconType Icon => EffectIconType.Rampart;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 60f;

        public RampartStatusEffect()
        {
            Stats[StatType.Defense] = 30;
        }

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Temporary, EffectVisualEffect(VisualEffectType.ImpSpellMantleUse), creature, 2f);
        }
    }
}
