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
    internal class LongswordAbilities : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            FastBlade();
            BurningBlade();
            RedLotusBlade();
            VorpalBlade();
            FlatBlade();
            Atonement();
            ShiningBlade();

            return _builder.Build();
        }

        private void FastBlade()
        {

        }

        private void BurningBlade()
        {

        }

        private void RedLotusBlade()
        {

        }

        private void VorpalBlade()
        {

        }

        private void FlatBlade()
        {

        }

        private void Atonement()
        {

        }

        private void ShiningBlade()
        {
            _builder.Create(FeatType.ShiningBlade)
                .Name(LocaleString.ShiningBlade)
                .Description(LocaleString.ShiningBladeDescription)
                .HasPassiveWeaponSkill<ShiningBladeStatusEffect>(SkillType.Longsword);
        }
    }
}
