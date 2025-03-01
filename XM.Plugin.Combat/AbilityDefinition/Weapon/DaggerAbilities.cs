using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
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
        private readonly Lazy<StatusEffectService> _status;
        private readonly SpellService _spell;

        public DaggerAbilities(
            Lazy<CombatService> combat,
            Lazy<StatusEffectService> status,
            SpellService spell)
            : base(combat, status)
        {
            _status = status;
            _spell = spell;
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
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
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.Dagger, 160)
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
                .Classification(AbilityCategoryType.Offensive)
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
                .Classification(AbilityCategoryType.Offensive)
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
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.Dagger, 860)
                .RequirementTP(1500)
                .ResistType(ResistType.Water)
                .IncreasesStat(StatType.QueuedDMGBonus, 18);
        }

        private void Shadowstitch()
        {
            _builder.Create(FeatType.Shadowstitch)
                .Name(LocaleString.Shadowstitch)
                .Description(LocaleString.ShadowstitchDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.Dagger, 1130)
                .RequirementTP(1350)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Darkness, 6);
                    _status.Value.ApplyStatusEffect<ShadowstitchStatusEffect>(activator, target, duration);
                });
        }

        private void EnergyDrain()
        {
            _builder.Create(FeatType.EnergyDrain)
                .Name(LocaleString.EnergyDrain)
                .Description(LocaleString.EnergyDrainDescription)
                .Classification(AbilityCategoryType.Defensive)
                .IsWeaponSkill(SkillType.Dagger, 1390)
                .RequirementTP(2000)
                .HasActivationDelay(2f)
                .TelegraphSize(3f, 3f)
                .HasTelegraphSphereAction((activator, targets, location) =>
                {
                    foreach (var target in targets)
                    {
                        if (!GetFactionEqual(target, activator))
                            continue;

                        _status.Value.ApplyStatusEffect<EnergyDrainStatusEffect>(activator, target, 1);
                    }
                });
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
