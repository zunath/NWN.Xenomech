using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Brawler
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class HandToHandLore : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            HandToHandLoreAbility();

            return _builder.Build();
        }

        private void HandToHandLoreAbility()
        {
            _builder.Create(FeatType.HandToHandLore)
                .Name(LocaleString.HandToHandLore)
                .Description(LocaleString.HandToHandLoreDescription)
                .ResonanceCost(1);
        }
    }
}