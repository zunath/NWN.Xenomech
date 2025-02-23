using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Beastmaster
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class KillerInstinct : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            KillerInstinctAbility();

            return _builder.Build();
        }

        private void KillerInstinctAbility()
        {
            _builder.Create(FeatType.KillerInstinct)
                .Name(LocaleString.KillerInstinct)
                .Description(LocaleString.KillerInstinctDescription)
                .ResonanceCost(1)
                .IncreasesStat(StatType.Attack, 15);
        }
    }
}