using System;
using Anvil.Services;
using System.Collections.Generic;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(SneakAttack1StatusEffect))]
    public class SneakAttack1StatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.SneakAttackI;
        public override EffectIconType Icon => EffectIconType.SneakAttack1;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        public override List<Type> MorePowerfulEffectTypes { get; } =
        [
            typeof(SneakAttack2StatusEffect),
            typeof(SneakAttack3StatusEffect),
        ];
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpImproveAbilityScore), creature);
        }
    }
}
