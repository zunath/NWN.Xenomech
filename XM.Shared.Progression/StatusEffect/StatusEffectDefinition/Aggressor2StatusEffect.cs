using System;
using Anvil.Services;
using System.Collections.Generic;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.StatusEffect.StatusEffectDefinition
{
    [ServiceBinding(typeof(Aggressor2StatusEffect))]
    public class Aggressor2StatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.AggressorII;
        public override EffectIconType Icon => EffectIconType.Aggressor2;
        public override StatusEffectStackType StackingType => StatusEffectStackType.Disabled;
        public override float Frequency => 60f;

        public override List<Type> LessPowerfulEffectTypes { get; } =
        [
            typeof(Aggressor1StatusEffect)
        ];

        public Aggressor2StatusEffect()
        {
            Stats[StatType.Enmity] = 10;
        }
        protected override void Apply(uint creature)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpHeadEvil), creature);
        }
    }
}
