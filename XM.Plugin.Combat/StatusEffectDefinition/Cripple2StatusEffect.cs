using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(Cripple2StatusEffect))]
    public class Cripple2StatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.CrippleII;
        public override EffectIconType Icon => EffectIconType.Cripple2;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 3f;

        public override List<Type> LessPowerfulEffectTypes { get; } =
        [
            typeof(Cripple1StatusEffect)
        ];

        public Cripple2StatusEffect()
        {
            Stats[StatType.Defense] = 40;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpReduceAbilityScore), creature);
        }
    }
}