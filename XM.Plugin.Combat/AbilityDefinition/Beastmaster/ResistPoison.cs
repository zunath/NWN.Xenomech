using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Beastmaster
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class ResistPoison : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ResistPoisonAbility();

            return _builder.Build();
        }

        private void ResistPoisonAbility()
        {
            _builder.Create(FeatType.ResistPoison)
                .Name(LocaleString.ResistPoison)
                .Description(LocaleString.ResistPoisonDescription)
                .ResonanceCost(1)
                .IncreasesResist(ResistType.Poison, 25);
        }
    }
}