using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(Cripple1StatusEffect))]
    public class Cripple1StatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.CrippleI;
        public override EffectIconType Icon => EffectIconType.Cripple1;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 3f;

        public override List<Type> MorePowerfulEffectTypes { get; } =
        [
            typeof(Cripple2StatusEffect)
        ];

        public Cripple1StatusEffect()
        {
            Stats[StatType.Defense] = 20;
        }
        protected override void Apply(uint creature)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpReduceAbilityScore), creature);
        }
    }
}