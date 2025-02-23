using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Keeper
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class DefenseBonus: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            DefenseBonus1();
            DefenseBonus2();
            DefenseBonus3();
            DefenseBonus4();

            return _builder.Build();
        }

        private void DefenseBonus1()
        {
            _builder.Create(FeatType.DefenseBonus1)
                .Name(LocaleString.DefenseBonusI)
                .Description(LocaleString.DefenseBonusIDescription)
                .ResonanceCost(1)
                .IncreasesStat(StatType.Defense, 10);
        }
        private void DefenseBonus2()
        {
            _builder.Create(FeatType.DefenseBonus2)
                .Name(LocaleString.DefenseBonusII)
                .Description(LocaleString.DefenseBonusIIDescription)
                .ResonanceCost(2)
                .IncreasesStat(StatType.Defense, 30);
        }
        private void DefenseBonus3()
        {
            _builder.Create(FeatType.DefenseBonus3)
                .Name(LocaleString.DefenseBonusIII)
                .Description(LocaleString.DefenseBonusIIIDescription)
                .ResonanceCost(3)
                .IncreasesStat(StatType.Defense, 50);
        }
        private void DefenseBonus4()
        {
            _builder.Create(FeatType.DefenseBonus4)
                .Name(LocaleString.DefenseBonusIV)
                .Description(LocaleString.DefenseBonusIVDescription)
                .ResonanceCost(4)
                .IncreasesStat(StatType.Defense, 70);
        }
    }
}
