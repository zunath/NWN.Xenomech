using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(Mindstream1StatusEffect))]
    public class Mindstream1StatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.MindstreamI;
        public override EffectIconType Icon => EffectIconType.Mindstream1;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 150f;

        public override List<Type> MorePowerfulEffectTypes { get; } =
        [
            typeof(Mindstream2StatusEffect)
        ];

        public Mindstream1StatusEffect()
        {
            Stats[StatType.EPRegen] = 3;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpImproveAbilityScore), creature);
        }
    }
}