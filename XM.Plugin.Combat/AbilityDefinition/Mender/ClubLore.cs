using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Mender
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class ClubLore : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ClubLoreAbility();

            return _builder.Build();
        }

        private void ClubLoreAbility()
        {
            _builder.Create(FeatType.ClubLore)
                .Name(LocaleString.ClubLore)
                .Description(LocaleString.ClubLoreDescription)
                .ResonanceCost(1);
        }
    }
}