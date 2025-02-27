using System;
using Anvil.Services;
using System.Collections.Generic;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(Drown2StatusEffect))]
    public class Drown2StatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.DrownII;
        public override EffectIconType Icon => EffectIconType.Drown2;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 1f;

        public override List<Type> LessPowerfulEffectTypes { get; } =
        [
            typeof(Drown1StatusEffect)
        ];

        public Drown2StatusEffect()
        {
            StatGroup.Stats[StatType.Might] = -15;
        }
    }
}
