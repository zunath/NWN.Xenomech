using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Hunter
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Recycle : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Recycle1();
            Recycle2();

            return _builder.Build();
        }

        private void Recycle1()
        {
            _builder.Create(FeatType.Recycle1)
                .Name(LocaleString.RecycleI)
                .Description(LocaleString.RecycleIDescription)
                .ResonanceCost(1);
        }
        private void Recycle2()
        {
            _builder.Create(FeatType.Recycle2)
                .Name(LocaleString.RecycleII)
                .Description(LocaleString.RecycleIIDescription)
                .ResonanceCost(2);
        }
    }
}