using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Keeper
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class ShieldMastery: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ShieldMastery1();
            ShieldMastery2();

            return _builder.Build();
        }

        private void ShieldMastery1()
        {
            _builder.Create(FeatType.ShieldMastery1)
                .Name(LocaleString.ShieldMasteryI)
                .Description(LocaleString.ShieldMasteryIDescription)
                .ResonanceCost(1)
                .IncreasesStat(StatType.ShieldDeflection, 10);
        }
        private void ShieldMastery2()
        {
            _builder.Create(FeatType.ShieldMastery1)
                .Name(LocaleString.ShieldMasteryII)
                .Description(LocaleString.ShieldMasteryIIDescription)
                .ResonanceCost(2)
                .IncreasesStat(StatType.ShieldDeflection, 20);
        }
    }
}
