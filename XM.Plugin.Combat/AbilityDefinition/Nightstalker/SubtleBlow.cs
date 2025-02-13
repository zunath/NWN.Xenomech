using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Nightstalker
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class SubtleBlow : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            SubtleBlow1();
            SubtleBlow2();

            return _builder.Build();
        }

        private void SubtleBlow1()
        {
            _builder.Create(FeatType.SubtleBlow1)
                .Name(LocaleString.SubtleBlowI)
                .Description(LocaleString.SubtleBlowIDescription)
                .ResonanceCost(1)
                .IncreasesStat(StatType.SubtleBlow, 10);
        }
        private void SubtleBlow2()
        {
            _builder.Create(FeatType.SubtleBlow2)
                .Name(LocaleString.SubtleBlowII)
                .Description(LocaleString.SubtleBlowIIDescription)
                .ResonanceCost(2)
                .IncreasesStat(StatType.SubtleBlow, 20);
        }
    }
}