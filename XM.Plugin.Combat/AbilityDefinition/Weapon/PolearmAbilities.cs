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
    internal class PolearmAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();

        public PolearmAbilities(
            Lazy<CombatService> combat,
            Lazy<StatusEffectService> status)
            : base(combat, status)
        {
        }
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
            _builder.Create(FeatType.DoubleThrust)
                .Name(LocaleString.DoubleThrust)
                .Description(LocaleString.DoubleThrustDescription)
                .IsWeaponSkill(SkillType.Polearm, 160)
                .RequirementTP(500)
                .ResistType(ResistType.Lightning)
                .IncreasesStat(StatType.QueuedDMGBonus, 11);
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
