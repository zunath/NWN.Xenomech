using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.WeaponSkill;
using XM.Progression.Ability;
using XM.Progression.Skill;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Weapon
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class BowAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();

        public BowAbilities(
            Lazy<CombatService> combat,
            Lazy<StatusEffectService> status)
            : base(combat, status)
        {
        }
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            FlamingArrow();
            PiercingArrow();
            DullingArrow();
            Sidewinder();
            BlastArrow();
            ArchingArrow();
            ApexArrow();

            return _builder.Build();
        }

        private void FlamingArrow()
        {
            _builder.Create(FeatType.FlamingArrow)
                .Name(LocaleString.FlamingArrow)
                .Description(LocaleString.FlamingArrowDescription)
                .IsWeaponSkill(SkillType.Bow, 160)
                .RequirementTP(500)
                .ResistType(ResistType.Fire)
                .IncreasesStat(StatType.QueuedDMGBonus, 10);
        }

        private void PiercingArrow()
        {

        }

        private void DullingArrow()
        {

        }

        private void Sidewinder()
        {

        }

        private void BlastArrow()
        {

        }

        private void ArchingArrow()
        {

        }

        private void ApexArrow()
        {
            _builder.Create(FeatType.ApexArrow)
                .Name(LocaleString.ApexArrow)
                .Description(LocaleString.ApexArrowDescription)
                .HasPassiveWeaponSkill<ApexArrowStatusEffect>(SkillType.Bow);
        }
    }
}
