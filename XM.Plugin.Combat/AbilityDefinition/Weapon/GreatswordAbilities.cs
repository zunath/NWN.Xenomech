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
    internal class GreatswordAbilities : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            HardSlash();
            Frostbite();
            SickleMoon();
            SpinningSlash();
            ShockSlash();
            GroundStrike();
            Scourge();

            return _builder.Build();
        }

        private void HardSlash()
        {

        }

        private void Frostbite()
        {

        }

        private void SickleMoon()
        {

        }

        private void SpinningSlash()
        {

        }

        private void ShockSlash()
        {

        }

        private void GroundStrike()
        {

        }

        private void Scourge()
        {
            _builder.Create(FeatType.Scourge)
                .Name(LocaleString.Scourge)
                .Description(LocaleString.ScourgeDescription)
                .HasPassiveWeaponSkill<ScourgeStatusEffect>(SkillType.GreatSword);
        }
    }
}
