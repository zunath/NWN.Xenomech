using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Nightstalker
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class DaggerLore : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            DaggerLoreAbility();

            return _builder.Build();
        }

        private void DaggerLoreAbility()
        {
            _builder.Create(FeatType.DaggerLore)
                .Name(LocaleString.DaggerLore)
                .Description(LocaleString.DaggerLoreDescription)
                .ResonanceCost(1);
        }
    }
}