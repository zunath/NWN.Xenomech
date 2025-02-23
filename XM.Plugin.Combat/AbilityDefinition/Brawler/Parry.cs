using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Brawler
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Parry : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Parry1();
            Parry2();
            Parry3();

            return _builder.Build();
        }

        private void Parry1()
        {
            _builder.Create(FeatType.Parry1)
                .Name(LocaleString.ParryI)
                .Description(LocaleString.ParryIDescription)
                .ResonanceCost(1)
                .IncreasesStat(StatType.AttackDeflection, 5);
        }
        private void Parry2()
        {
            _builder.Create(FeatType.Parry2)
                .Name(LocaleString.ParryII)
                .Description(LocaleString.ParryIIDescription)
                .ResonanceCost(2)
                .IncreasesStat(StatType.AttackDeflection, 10);
        }
        private void Parry3()
        {
            _builder.Create(FeatType.Parry3)
                .Name(LocaleString.ParryIII)
                .Description(LocaleString.ParryIIIDescription)
                .ResonanceCost(3)
                .IncreasesStat(StatType.AttackDeflection, 15);
        }
    }
}