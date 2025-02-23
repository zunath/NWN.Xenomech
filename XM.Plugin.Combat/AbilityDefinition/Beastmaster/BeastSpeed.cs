using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Beastmaster
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class BeastSpeed: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            BeastSpeedAbility();

            return _builder.Build();
        }

        private void BeastSpeedAbility()
        {
            _builder.Create(FeatType.BeastSpeed)
                .Name(LocaleString.BeastSpeed)
                .Description(LocaleString.BeastSpeedDescription)
                .ResonanceCost(1);
        }
    }
}
