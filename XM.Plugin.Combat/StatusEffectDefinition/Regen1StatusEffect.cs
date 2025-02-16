using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(Regen1StatusEffect))]
    public class Regen1StatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.RegenI;
        public override EffectIconType Icon => EffectIconType.Regen1;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 6f;

        public override List<Type> MorePowerfulEffectTypes { get; } =
        [
            typeof(Regen2StatusEffect)
        ];

        public Regen1StatusEffect()
        {
            Stats[StatType.HPRegen] = 5;
        }

        protected override void Apply(uint creature)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpHeadHoly), creature);
        }
    }
}
