using System;
using Anvil.Services;
using System.Collections.Generic;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(SneakAttack2StatusEffect))]
    public class SneakAttack2StatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.SneakAttackII;
        public override EffectIconType Icon => EffectIconType.SneakAttack2;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        public override List<Type> LessPowerfulEffectTypes { get; } =
        [
            typeof(SneakAttack1StatusEffect),
        ];
        public override List<Type> MorePowerfulEffectTypes { get; } =
        [
            typeof(SneakAttack3StatusEffect),
        ];

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpImproveAbilityScore), creature);
        }
    }
}
