using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(Protection2StatusEffect))]
    public class Protection2StatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.ProtectionII;
        public override EffectIconType Icon => EffectIconType.Protection2;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        public override List<Type> LessPowerfulEffectTypes { get; } =
        [
            typeof(Protection1StatusEffect)
        ];

        public override List<Type> MorePowerfulEffectTypes { get; } =
        [
            typeof(Protection3StatusEffect)
        ];

        public Protection2StatusEffect()
        {
            Stats[StatType.Defense] = 50;
        }
        protected override void Apply(uint creature)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpACBonus), creature);
        }
    }
}