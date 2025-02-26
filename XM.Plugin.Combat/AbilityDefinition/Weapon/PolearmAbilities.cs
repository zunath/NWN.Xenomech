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
    internal class PolearmAbilities : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            DoubleThrust();
            ThunderThrust();
            RaidenThrust();
            PentaThrust();
            VorpalThrust();
            SonicThrust();
            Drakesbane();

            return _builder.Build();
        }

        private void DoubleThrust()
        {

        }

        private void ThunderThrust()
        {

        }

        private void RaidenThrust()
        {

        }

        private void PentaThrust()
        {

        }

        private void VorpalThrust()
        {

        }

        private void SonicThrust()
        {

        }

        private void Drakesbane()
        {
            _builder.Create(FeatType.Drakesbane)
                .Name(LocaleString.Drakesbane)
                .Description(LocaleString.DrakesbaneDescription)
                .HasPassiveWeaponSkill<DrakesbaneStatusEffect>(SkillType.Polearm);
        }
    }
}
