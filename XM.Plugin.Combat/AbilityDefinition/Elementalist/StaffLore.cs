using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Elementalist
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class StaffLore : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            StaffLoreAbility();

            return _builder.Build();
        }

        private void StaffLoreAbility()
        {
            _builder.Create(FeatType.StaffLore)
                .Name(LocaleString.StaffLore)
                .Description(LocaleString.StaffLoreDescription)
                .ResonanceCost(1);
        }
    }
}