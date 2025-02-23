using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Techweaver
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class PolearmLore : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            PolearmLoreAbility();

            return _builder.Build();
        }

        private void PolearmLoreAbility()
        {
            _builder.Create(FeatType.PolearmLore)
                .Name(LocaleString.PolearmLore)
                .Description(LocaleString.PolearmLoreDescription)
                .ResonanceCost(1);
        }
    }
}