using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Hunter
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class BowLore : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            BowLoreAbility();

            return _builder.Build();
        }

        private void BowLoreAbility()
        {
            _builder.Create(FeatType.BowLore)
                .Name(LocaleString.BowLore)
                .Description(LocaleString.BowLoreDescription)
                .ResonanceCost(1);
        }
    }
}