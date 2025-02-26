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
    internal class StaffAbilities : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            HeavySwing();
            RockCrusher();
            EarthCrusher();
            Starburst();
            Omniscience();
            SpiritTaker();
            Shattersoul();

            return _builder.Build();
        }

        private void HeavySwing()
        {

        }

        private void RockCrusher()
        {

        }

        private void EarthCrusher()
        {

        }

        private void Starburst()
        {

        }

        private void Omniscience()
        {

        }

        private void SpiritTaker()
        {

        }

        private void Shattersoul()
        {
            _builder.Create(FeatType.Shattersoul)
                .Name(LocaleString.Shattersoul)
                .Description(LocaleString.ShattersoulDescription)
                .HasPassiveWeaponSkill<ShattersoulStatusEffect>(SkillType.Staff);
        }
    }
}
