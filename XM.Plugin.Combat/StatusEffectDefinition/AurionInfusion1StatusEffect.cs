using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(AurionInfusion1StatusEffect))]
    public class AurionInfusion1StatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.AurionInfusionI;
        public override EffectIconType Icon => EffectIconType.AurionInfusion1;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 60f * 30f;

        public override List<Type> MorePowerfulEffectTypes { get; } =
        [
            typeof(AurionInfusion2StatusEffect)
        ];

        public AurionInfusion1StatusEffect()
        {
            Stats[StatType.EtherAttack] = 15;
        }
        protected override void Apply(uint creature)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpImproveAbilityScore), creature);
        }
    }
}