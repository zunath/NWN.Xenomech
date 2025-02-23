using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(SneakAttack3StatusEffect))]
    public class SneakAttack3StatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.SneakAttackIII;
        public override EffectIconType Icon => EffectIconType.SneakAttack3;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        public override List<Type> LessPowerfulEffectTypes { get; } =
        [
            typeof(SneakAttack1StatusEffect),
            typeof(SneakAttack2StatusEffect),
        ];

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpImproveAbilityScore), creature);
        }
    }
}
