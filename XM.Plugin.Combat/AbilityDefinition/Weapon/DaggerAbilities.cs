using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Debuff;
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
    internal class DaggerAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();

        public DaggerAbilities(
            Lazy<CombatService> combat,
            Lazy<StatusEffectService> status)
            : base(combat, status)
        {
        }
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            WaspSting();
            GustSlash();
            Cyclone();
            SharkBite();
            Shadowstitch();
            EnergyDrain();
            DancingEdge();

            return _builder.Build();
        }

        private void WaspSting()
        {
            const int DMG = 9;
            const ResistType Resist = ResistType.Earth;
            const DamageType DamageType = DamageType.Earth;
            const VisualEffectType Vfx = VisualEffectType.None;
            const int DurationTicks = 10;

            _builder.Create(FeatType.WaspSting)
                .Name(LocaleString.WaspSting)
                .Description(LocaleString.WaspStingDescription)
                .IsWeaponSkill(SkillType.Dagger, 240)
                .RequirementTP(500)
                .ResistType(Resist)
                .IncreasesStat(StatType.QueuedDMGBonus, DMG)
                .HasImpactAction((activator, target, location) =>
                {
                    DamageEffectImpact<PoisonStatusEffect>(
                        activator,
                        target,
                        DMG,
                        Resist,
                        DamageType,
                        Vfx,
                        DurationTicks);
                });
        }

        private void GustSlash()
        {
            const int DMG = 12;
            const ResistType Resist = ResistType.Wind;
            const DamageType DamageType = DamageType.Wind;
            const VisualEffectType Vfx = VisualEffectType.ImpDeathWard;

            _builder.Create(FeatType.GustSlash)
                .Name(LocaleString.GustSlash)
                .Description(LocaleString.GustSlashDescription)
                .IsWeaponSkill(SkillType.Dagger, 240)
                .RequirementTP(1000)
                .ResistType(Resist)
                .IncreasesStat(StatType.QueuedDMGBonus, DMG)
                .TelegraphSize(5f, 2f)
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

        private void Cyclone()
        {
            const int DMG = 14;
            const ResistType Resist = ResistType.Wind;
            const DamageType DamageType = DamageType.Wind;
            const VisualEffectType Vfx = VisualEffectType.ImpDeathWard;

            _builder.Create(FeatType.Cyclone)
                .Name(LocaleString.Cyclone)
                .Description(LocaleString.CycloneDescription)
                .IsWeaponSkill(SkillType.Dagger, 540)
                .RequirementTP(1250)
                .ResistType(Resist)
                .IncreasesStat(StatType.QueuedDMGBonus, DMG)
                .TelegraphSize(4f, 4f)
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

        private void SharkBite()
        {
            _builder.Create(FeatType.SharkBite)
                .Name(LocaleString.SharkBite)
                .Description(LocaleString.SharkBiteDescription)
                .IsWeaponSkill(SkillType.Dagger, 860)
                .RequirementTP(1500)
                .ResistType(ResistType.Water)
                .IncreasesStat(StatType.QueuedDMGBonus, 18);
        }

        private void Shadowstitch()
        {

        }

        private void EnergyDrain()
        {

        }

        private void DancingEdge()
        {
            _builder.Create(FeatType.DancingEdge)
                .Name(LocaleString.DancingEdge)
                .Description(LocaleString.DancingEdgeDescription)
                .HasPassiveWeaponSkill<DancingEdgeStatusEffect>(SkillType.Dagger);
        }
    }
}
