using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition
{
    [ServiceBinding(typeof(Aggressor1StatusEffect))]
    public class Aggressor1StatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.AggressorI;
        public override EffectIconType Icon => EffectIconType.Aggressor1;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        public override List<Type> MorePowerfulEffectTypes { get; } = 
        [
            typeof(Aggressor2StatusEffect)
        ];

        public Aggressor1StatusEffect()
        {
            Stats[StatType.Enmity] = 5;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpHeadEvil), creature);
        }
    }
}
