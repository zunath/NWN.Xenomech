using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.WeaponSkill;
using XM.Progression.Ability;
using XM.Progression.Skill;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Weapon
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class ThrowingAbilities : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            StoneToss();
            WindSlash();
            ShurikenStorm();
            PhantomHurl();
            FlameToss();
            FrostDart();
            StarStrike();

            return _builder.Build();
        }

        private void StoneToss()
        {

        }

        private void WindSlash()
        {

        }

        private void ShurikenStorm()
        {

        }

        private void PhantomHurl()
        {

        }

        private void FlameToss()
        {

        }

        private void FrostDart()
        {

        }

        private void StarStrike()
        {
            _builder.Create(FeatType.StarStrike)
                .Name(LocaleString.StarStrike)
                .Description(LocaleString.StarStrikeDescription)
                .HasPassiveWeaponSkill<StarStrikeStatusEffect>(SkillType.Throwing);
        }
    }
}
