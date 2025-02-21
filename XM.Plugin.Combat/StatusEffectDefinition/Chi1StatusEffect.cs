using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(Chi1StatusEffect))]
    public class Chi1StatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.ChiI;
        public override EffectIconType Icon => EffectIconType.Chi1;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 6f;

        public override List<Type> MorePowerfulEffectTypes { get; } =
        [
            typeof(Chi2StatusEffect)
        ];

        public Chi1StatusEffect()
        {
            Stats[StatType.HPRegen] = 3;
        }

        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpHeadHoly), creature);
        }
    }
}
