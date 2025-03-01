using System;
using Anvil.Services;
using System.Collections.Generic;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(Chi2StatusEffect))]
    public class Chi2StatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.ChiII;
        public override EffectIconType Icon => EffectIconType.Chi2;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 6f;

        public override List<Type> LessPowerfulEffectTypes { get; } =
        [
            typeof(Chi1StatusEffect)
        ];

        public Chi2StatusEffect()
        {
            StatGroup.Stats[StatType.HPRegen] = 8;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpHeadHoly), creature);
        }
    }
}
