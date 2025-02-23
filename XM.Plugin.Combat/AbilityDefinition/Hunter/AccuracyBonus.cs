using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Hunter
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class AccuracyBonus : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            AccuracyBonus1();
            AccuracyBonus2();

            return _builder.Build();
        }

        private void AccuracyBonus1()
        {
            _builder.Create(FeatType.AccuracyBonus1)
                .Name(LocaleString.AccuracyBonusI)
                .Description(LocaleString.AccuracyBonusIDescription)
                .ResonanceCost(1)
                .IncreasesStat(StatType.Accuracy, 10);
        }
        private void AccuracyBonus2()
        {
            _builder.Create(FeatType.AccuracyBonus2)
                .Name(LocaleString.AccuracyBonusII)
                .Description(LocaleString.AccuracyBonusIIDescription)
                .ResonanceCost(2)
                .IncreasesStat(StatType.Accuracy, 30);
        }
    }
}