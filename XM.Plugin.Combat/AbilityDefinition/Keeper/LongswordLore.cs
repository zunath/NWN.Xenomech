using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Keeper
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class LongswordLore : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            LongswordLoreAbility();

            return _builder.Build();
        }

        private void LongswordLoreAbility()
        {
            _builder.Create(FeatType.LongswordLore)
                .Name(LocaleString.LongswordLore)
                .Description(LocaleString.LongswordLoreDescription)
                .ResonanceCost(1);
        }
    }
}