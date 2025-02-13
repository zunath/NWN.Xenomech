using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Techweaver
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class EPBonus : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Parry1();
            Parry2();

            return _builder.Build();
        }

        private void Parry1()
        {
            _builder.Create(FeatType.EPBonus1)
                .Name(LocaleString.EPBonusI)
                .Description(LocaleString.EPBonusIDescription)
                .ResonanceCost(1)
                .IncreasesStat(StatType.MaxEP, 40);
        }
        private void Parry2()
        {
            _builder.Create(FeatType.EPBonus2)
                .Name(LocaleString.EPBonusII)
                .Description(LocaleString.EPBonusIIDescription)
                .ResonanceCost(2)
                .IncreasesStat(StatType.MaxEP, 80);
        }
    }
}