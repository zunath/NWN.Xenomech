using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(Protection3StatusEffect))]
    public class Protection3StatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.ProtectionIII;
        public override EffectIconType Icon => EffectIconType.Protection3;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        public override List<Type> LessPowerfulEffectTypes { get; } =
        [
            typeof(Protection1StatusEffect),
            typeof(Protection2StatusEffect)
        ];

        public Protection3StatusEffect()
        {
            StatGroup.Stats[StatType.Defense] = 90;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpACBonus), creature);
        }
    }
}