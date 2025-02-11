using System.Collections.Generic;
using XM.AI.Enmity;
using XM.Progression.Ability;
using XM.Progression.Stat;
using XM.Shared.API.Constants;

namespace XM.Plugin.Combat.AbilityDefinition.Keeper
{
    internal class DefenseBonus: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly EnmityService _enmity;
        private readonly StatService _stat;

        public DefenseBonus(
            EnmityService enmity,
            StatService stat)
        {
            _enmity = enmity;
            _stat = stat;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            DefenseBonus1();
            DefenseBonus2();
            DefenseBonus3();
            DefenseBonus4();

            return _builder.Build();
        }

        private void DefenseBonus1()
        {

        }
        private void DefenseBonus2()
        {

        }
        private void DefenseBonus3()
        {

        }
        private void DefenseBonus4()
        {

        }
    }
}
