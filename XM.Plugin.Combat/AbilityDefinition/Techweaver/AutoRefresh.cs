using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Techweaver
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class AutoRefresh: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            AutoRefresh1();
            AutoRefresh2();

            return _builder.Build();
        }

        private void AutoRefresh1()
        {
            _builder.Create(FeatType.AutoRefresh1)
                .Name(LocaleString.AutoRefreshI)
                .Description(LocaleString.AutoRefreshIDescription)
                .ResonanceCost(2)
                .IncreasesStat(StatType.EPRegen, 1);
        }
        private void AutoRefresh2()
        {
            _builder.Create(FeatType.AutoRefresh2)
                .Name(LocaleString.AutoRefreshII)
                .Description(LocaleString.AutoRefreshIIDescription)
                .ResonanceCost(3)
                .IncreasesStat(StatType.EPRegen, 2);
        }
    }
}