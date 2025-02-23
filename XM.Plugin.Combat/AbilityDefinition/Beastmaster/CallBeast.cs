using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Beastmaster
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class CallBeast: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            CallBeastAbility();

            return _builder.Build();
        }

        private void CallBeastAbility()
        {
            _builder.Create(FeatType.CallBeast)
                .Name(LocaleString.CallBeast)
                .Description(LocaleString.CallBeastDescription)
                .ResonanceCost(1);
        }
    }
}
