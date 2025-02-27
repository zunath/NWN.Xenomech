using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Buff
{
    [ServiceBinding(typeof(Aggressor2StatusEffect))]
    public class Aggressor2StatusEffect : StatusEffectBase
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
            StatGroup.Stats[StatType.Enmity] = 10;
        }
        protected override void Apply(uint creature, int durationTicks)
        {
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpHeadEvil), creature);
        }
    }
}
