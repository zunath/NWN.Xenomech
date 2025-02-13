using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Nightstalker
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class CriticalBonus : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            CriticalBonus1();
            CriticalBonus2();

            return _builder.Build();
        }

        private void CriticalBonus1()
        {
            _builder.Create(FeatType.CriticalBonus1)
                .Name(LocaleString.CriticalBonusI)
                .Description(LocaleString.CriticalBonusIDescription)
                .ResonanceCost(1)
                .IncreasesStat(StatType.CriticalRate, 5);
        }
        private void CriticalBonus2()
        {
            _builder.Create(FeatType.CriticalBonus2)
                .Name(LocaleString.CriticalBonusII)
                .Description(LocaleString.CriticalBonusIIDescription)
                .ResonanceCost(2)
                .IncreasesStat(StatType.CriticalRate, 10);
        }
    }
}