using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Elementalist
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class EtherAttackBonus : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            EtherAttackBonus1();
            EtherAttackBonus2();

            return _builder.Build();
        }

        private void EtherAttackBonus1()
        {
            _builder.Create(FeatType.EtherAttackBonus1)
                .Name(LocaleString.EtherAttackBonusI)
                .Description(LocaleString.EtherAttackBonusIDescription)
                .ResonanceCost(1)
                .IncreasesStat(StatType.EtherAttack, 10);
        }
        private void EtherAttackBonus2()
        {
            _builder.Create(FeatType.EtherAttackBonus2)
                .Name(LocaleString.EtherAttackBonusII)
                .Description(LocaleString.EtherAttackBonusIIDescription)
                .ResonanceCost(2)
                .IncreasesStat(StatType.EtherAttack, 30);
        }
    }
}