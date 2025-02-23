using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.StatusEffectDefinition.Debuff
{
    [ServiceBinding(typeof(Drown1StatusEffect))]
    public class Drown1StatusEffect : StatusEffectBase
    {
        public override LocaleString Name => LocaleString.DrownI;
        public override EffectIconType Icon => EffectIconType.Drown1;
        public override StatusEffectStackType StackingType => StatusEffectStackType.StackFromMultipleSources;
        public override float Frequency => 1f;

        public override List<Type> MorePowerfulEffectTypes { get; } =
        [
            typeof(Drown2StatusEffect)
        ];

        public Drown1StatusEffect()
        {
            Stats[StatType.Might] = -6;
        }
    }
}
