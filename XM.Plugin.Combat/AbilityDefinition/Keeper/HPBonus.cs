using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Keeper
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class HPBonus: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            HPBonus1();
            HPBonus2();

            return _builder.Build();
        }

        private void HPBonus1()
        {
            _builder.Create(FeatType.HPBonus1)
                .Name(LocaleString.HPBonusI)
                .Description(LocaleString.HPBonusIDescription)
                .ResonanceCost(1)
                .IncreasesStat(StatType.MaxHP, 60);
        }
        private void HPBonus2()
        {
            _builder.Create(FeatType.HPBonus2)
                .Name(LocaleString.HPBonusII)
                .Description(LocaleString.HPBonusIIDescription)
                .ResonanceCost(2)
                .IncreasesStat(StatType.MaxHP, 120);
        }
    }
}
