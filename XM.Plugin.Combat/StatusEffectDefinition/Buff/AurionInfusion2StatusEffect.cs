using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(AurionInfusion2StatusEffect))]
    public class AurionInfusion2StatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.AurionInfusionII;
        public override EffectIconType Icon => EffectIconType.AurionInfusion2;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 60f * 30f;

        public override List<Type> LessPowerfulEffectTypes { get; } =
        [
            typeof(AurionInfusion1StatusEffect)
        ];

        public AurionInfusion2StatusEffect()
        {
            Stats[StatType.EtherAttack] = 30;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpImproveAbilityScore), creature);
        }
    }
}