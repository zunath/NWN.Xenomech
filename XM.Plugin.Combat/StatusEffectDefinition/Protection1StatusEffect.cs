using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(Protection1StatusEffect))]
    public class Protection1StatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.ProtectionI;
        public override EffectIconType Icon => EffectIconType.Protection1;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        public override List<Type> MorePowerfulEffectTypes { get; } =
        [
            typeof(Protection2StatusEffect),
            typeof(Protection3StatusEffect)
        ];

        public Protection1StatusEffect()
        {
            Stats[StatType.Defense] = 20;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpACBonus), creature);
        }
    }
}