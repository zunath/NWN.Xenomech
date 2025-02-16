using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Nightstalker
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Creditfinder : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            CreditfinderAbility();

            return _builder.Build();
        }

        private void CreditfinderAbility()
        {
            _builder.Create(FeatType.Creditfinder)
                .Name(LocaleString.Creditfinder)
                .Description(LocaleString.CreditfinderDescription)
                .ResonanceCost(1);
        }
    }
}