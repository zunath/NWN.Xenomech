using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Elementalist
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class ClearMind : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ClearMindAbility();

            return _builder.Build();
        }

        private void ClearMindAbility()
        {
            _builder.Create(FeatType.ClearMind)
                .Name(LocaleString.ClearMind)
                .Description(LocaleString.ClearMindDescription)
                .ResonanceCost(1);
        }
    }
}