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
        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
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
            const int DMG = 14;
            const ResistType Resist = ResistType.Lightning;
            const DamageType DamageType = DamageType.Lightning;
            const VisualEffectType Vfx = VisualEffectType.ImpLightningSmall;

            _builder.Create(FeatType.ThunderThrust)
                .Name(LocaleString.ThunderThrust)
                .Description(LocaleString.ThunderThrustDescription)
                .IsWeaponSkill(SkillType.Polearm, 240)
                .RequirementTP(1000)
                .ResistType(Resist)
                .IncreasesStat(StatType.QueuedDMGBonus, DMG)
                .TelegraphSize(4f, 2f)
                .HasTelegraphLineAction((activator, targets, location) =>
                {
                    DamageImpact(
                        activator,
                        targets,
                        DMG,
                        Resist,
                        DamageType,
                        Vfx);
                });
        }

        private void RaidenThrust()
        {
            const int DMG = 18;
            const ResistType Resist = ResistType.Lightning;
            const DamageType DamageType = DamageType.Lightning;
            const VisualEffectType Vfx = VisualEffectType.ImpLightningSmall;

            _builder.Create(FeatType.RaidenThrust)
                .Name(LocaleString.RaidenThrust)
                .Description(LocaleString.RaidenThrustDescription)
                .IsWeaponSkill(SkillType.Polearm, 540)
                .RequirementTP(1250)
                .ResistType(Resist)
                .IncreasesStat(StatType.QueuedDMGBonus, DMG)
                .TelegraphSize(4f, 2f)
                .HasTelegraphConeAction((activator, targets, location) =>
                {
                    DamageImpact(
                        activator,
                        targets,
                        DMG,
                        Resist,
                        DamageType,
                        Vfx);
                });
        }

        private void PentaThrust()
        {
            const int DMG = 22;
            const ResistType Resist = ResistType.Lightning;
            const DamageType DamageType = DamageType.Lightning;
            const VisualEffectType Vfx = VisualEffectType.ImpLightningSmall;

            _builder.Create(FeatType.PentaThrust)
                .Name(LocaleString.PentaThrust)
                .Description(LocaleString.PentaThrustDescription)
                .IsWeaponSkill(SkillType.Polearm, 860)
                .RequirementTP(1500)
                .ResistType(Resist)
                .IncreasesStat(StatType.QueuedDMGBonus, DMG)
                .TelegraphSize(2f, 2f)
                .HasTelegraphSphereAction((activator, targets, location) =>
                {
                    DamageImpact(
                        activator,
                        targets,
                        DMG,
                        Resist,
                        DamageType,
                        Vfx);
                });
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
