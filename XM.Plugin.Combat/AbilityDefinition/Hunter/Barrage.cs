using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Hunter
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Barrage : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            BarrageAbility();

            return _builder.Build();
        }

        private void BarrageAbility()
        {
            _builder.Create(FeatType.Barrage)
                .Name(LocaleString.Barrage)
                .Description(LocaleString.BarrageDescription)
                .ResonanceCost(4);
        }
    }
}