using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Beastmaster
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class EtherLink: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            EtherLinkAbility();

            return _builder.Build();
        }

        private void EtherLinkAbility()
        {
            _builder.Create(FeatType.EtherLink)
                .Name(LocaleString.EtherLink)
                .Description(LocaleString.EtherLinkDescription)
                .ResonanceCost(2);
        }
    }
}
