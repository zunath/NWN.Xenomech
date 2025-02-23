using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Nightstalker
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class TreasureHunter : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            TreasureHunter1();
            TreasureHunter2();

            return _builder.Build();
        }

        private void TreasureHunter1()
        {
            _builder.Create(FeatType.TreasureHunter1)
                .Name(LocaleString.TreasureHunterI)
                .Description(LocaleString.TreasureHunterIDescription)
                .ResonanceCost(2);
        }
        private void TreasureHunter2()
        {
            _builder.Create(FeatType.TreasureHunter2)
                .Name(LocaleString.TreasureHunterII)
                .Description(LocaleString.TreasureHunterIIDescription)
                .ResonanceCost(3);
        }
    }
}