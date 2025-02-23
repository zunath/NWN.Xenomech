using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Beastmaster
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class AxeLore : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            AxeLoreAbility();

            return _builder.Build();
        }

        private void AxeLoreAbility()
        {
            _builder.Create(FeatType.AxeLore)
                .Name(LocaleString.AxeLore)
                .Description(LocaleString.AxeLoreDescription)
                .ResonanceCost(1);
        }
    }
}