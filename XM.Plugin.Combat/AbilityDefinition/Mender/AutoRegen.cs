using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Mender
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class AutoRegen : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            AutoRegen1();
            AutoRegen2();

            return _builder.Build();
        }

        private void AutoRegen1()
        {
            _builder.Create(FeatType.AutoRegen1)
                .Name(LocaleString.AutoRegenI)
                .Description(LocaleString.AutoRegenIDescription)
                .ResonanceCost(1)
                .IncreasesStat(StatType.HPRegen, 1);
        }
        private void AutoRegen2()
        {
            _builder.Create(FeatType.AutoRegen2)
                .Name(LocaleString.AutoRegenII)
                .Description(LocaleString.AutoRegenIIDescription)
                .ResonanceCost(2)
                .IncreasesStat(StatType.HPRegen, 2);
        }
    }
}