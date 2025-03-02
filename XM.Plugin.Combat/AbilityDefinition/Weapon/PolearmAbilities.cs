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
    internal class PolearmAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();
        private readonly Lazy<StatusEffectService> _status;
        private readonly SpellService _spell;

        public PolearmAbilities(
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
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
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
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
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
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
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
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
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
            _builder.Create(FeatType.VorpalThrust)
                .Name(LocaleString.VorpalThrust)
                .Description(LocaleString.VorpalThrustDescription)
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
                .IsWeaponSkill(SkillType.Polearm, 1130)
                .RequirementTP(1350)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Water, 20);
                    _status.Value.ApplyStatusEffect<ParalyzeStatusEffect>(activator, target, duration);
                });
        }

        private void SonicThrust()
        {
            _builder.Create(FeatType.SonicThrust)
                .Name(LocaleString.SonicThrust)
                .Description(LocaleString.SonicThrustDescription)
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
                .IsWeaponSkill(SkillType.Polearm, 1390)
                .RequirementTP(2000)
                .HasActivationDelay(2f)
                .TelegraphSize(4f, 2f)
                .HasTelegraphConeAction((activator, targets, location) =>
                {
                    foreach (var target in targets)
                    {
                        if (GetFactionEqual(target, activator))
                            continue;

                        _status.Value.ApplyStatusEffect<SonicThrustStatusEffect>(activator, target, 1);
                    }
                });
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
