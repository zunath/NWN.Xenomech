using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(Mindstream2StatusEffect))]
    public class Mindstream2StatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.MindstreamII;
        public override EffectIconType Icon => EffectIconType.Mindstream2;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 150f;

        public override List<Type> MorePowerfulEffectTypes { get; } =
        [
            typeof(Mindstream1StatusEffect)
        ];

        public Mindstream2StatusEffect()
        {
            Stats[StatType.EPRegen] = 6;
        }
        protected override void Apply(uint creature)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpImproveAbilityScore), creature);
        }
    }
}