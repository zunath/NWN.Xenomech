using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Nightstalker
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class BackAttack : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            BackAttack1();
            BackAttack2();
            BackAttack3();
            BackAttack4();

            return _builder.Build();
        }

        private void BackAttack1()
        {
            _builder.Create(FeatType.BackAttack1)
                .Name(LocaleString.BackAttackI)
                .Description(LocaleString.BackAttackIDescription)
                .ResonanceCost(1);
        }
        private void BackAttack2()
        {
            _builder.Create(FeatType.BackAttack2)
                .Name(LocaleString.BackAttackII)
                .Description(LocaleString.BackAttackIIDescription)
                .ResonanceCost(2);
        }
        private void BackAttack3()
        {
            _builder.Create(FeatType.BackAttack3)
                .Name(LocaleString.BackAttackIII)
                .Description(LocaleString.BackAttackIIIDescription)
                .ResonanceCost(3);
        }
        private void BackAttack4()
        {
            _builder.Create(FeatType.BackAttack4)
                .Name(LocaleString.BackAttackIV)
                .Description(LocaleString.BackAttackIVDescription)
                .ResonanceCost(4);
        }
    }
}