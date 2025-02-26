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
    internal class RifleAbilities : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            HotShot();
            SplitShot();
            SniperShot();
            SlugShot();
            BlastShot();
            HeavyShot();
            Trueflight();

            return _builder.Build();
        }

        private void HotShot()
        {

        }

        private void SplitShot()
        {

        }

        private void SniperShot()
        {

        }

        private void SlugShot()
        {

        }

        private void BlastShot()
        {

        }

        private void HeavyShot()
        {

        }

        private void Trueflight()
        {
            _builder.Create(FeatType.Trueflight)
                .Name(LocaleString.Trueflight)
                .Description(LocaleString.TrueflightDescription)
                .HasPassiveWeaponSkill<TrueFlightStatusEffect>(SkillType.Rifle);
        }
    }
}
