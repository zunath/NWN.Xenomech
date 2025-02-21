using System;
using Anvil.Services;
using System.Collections.Generic;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(Regen2StatusEffect))]
    public class Regen2StatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.RegenII;
        public override EffectIconType Icon => EffectIconType.Regen2;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 6f;

        public override List<Type> LessPowerfulEffectTypes { get; } =
        [
            typeof(Regen1StatusEffect)
        ];

        public Regen2StatusEffect()
        {
            Stats[StatType.HPRegen] = 12;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpHeadHoly), creature);
        }
    }
}
