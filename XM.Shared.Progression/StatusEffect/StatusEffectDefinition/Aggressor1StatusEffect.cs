using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Progression.StatusEffect.StatusEffectDefinition
{
    [ServiceBinding(typeof(Aggressor1StatusEffect))]
    public class Aggressor1StatusEffect: StatusEffectBase
    {
        public override LocaleString Name => LocaleString.AggressorI;
        public override EffectIconType Icon => EffectIconType.Aggressor1;
        public override bool IsStackable => false;
        public override bool IsFlaggedForRemoval { get; protected set; }
        public override bool SendsApplicationMessage => true;
        public override bool SendsWornOffMessage => true;
        public override float Frequency => 100f;

        public override List<Type> MorePowerfulEffectTypes { get; } = 
        [
            typeof(Aggressor2StatusEffect)
        ];

        public Aggressor1StatusEffect()
        {
            Stats[StatType.Enmity] = 5;
        }
    }
}
