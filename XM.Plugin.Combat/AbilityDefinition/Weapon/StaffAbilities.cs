using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.WeaponSkill;
using XM.Progression.Ability;
using XM.Progression.Skill;
using XM.Progression.Stat;
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
            _builder.Create(FeatType.HeavySwing)
                .Name(LocaleString.HeavySwing)
                .Description(LocaleString.HeavySwingDescription)
                .IsWeaponSkill(SkillType.Club, 160)
                .RequirementTP(500)
                .ResistType(ResistType.Earth)
                .IncreasesStat(StatType.QueuedDMGBonus, 7);
        }

        private void RockCrusher()
        {

        }

        private void EarthCrusher()
        {

        }

        private void Starburst()
        {
            _builder.Create(FeatType.Starburst)
                .Name(LocaleString.Starburst)
                .Description(LocaleString.StarburstDescription)
                .IsWeaponSkill(SkillType.Club, 860)
                .RequirementTP(1500)
                .ResistType(ResistType.Mind)
                .IncreasesStat(StatType.QueuedDMGBonus, 14);
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
