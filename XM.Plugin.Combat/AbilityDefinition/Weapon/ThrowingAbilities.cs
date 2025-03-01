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
    internal class ThrowingAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();
        private readonly Lazy<StatusEffectService> _status;
        private readonly SpellService _spell;

        public ThrowingAbilities(
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
            StoneToss();
            WindSlash();
            ShurikenStorm();
            PhantomHurl();
            FlameToss();
            FrostDart();
            StarStrike();

            return _builder.Build();
        }

        private void StoneToss()
        {
            _builder.Create(FeatType.StoneToss)
                .Name(LocaleString.StoneToss)
                .Description(LocaleString.StoneTossDescription)
                .IsWeaponSkill(SkillType.Throwing, 160)
                .RequirementTP(500)
                .ResistType(ResistType.Earth)
                .IncreasesStat(StatType.QueuedDMGBonus, 8);
        }

        private void WindSlash()
        {
            const int DMG = 10;
            const ResistType Resist = ResistType.Wind;
            const DamageType DamageType = DamageType.Wind;
            const VisualEffectType Vfx = VisualEffectType.ImpDeathWard;

            _builder.Create(FeatType.WindSlash)
                .Name(LocaleString.WindSlash)
                .Description(LocaleString.WindSlashDescription)
                .IsWeaponSkill(SkillType.Throwing, 240)
                .RequirementTP(1000)
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

        private void ShurikenStorm()
        {
            const int DMG = 13;
            const ResistType Resist = ResistType.Lightning;
            const DamageType DamageType = DamageType.Lightning;
            const VisualEffectType Vfx = VisualEffectType.ImpLightningSmall;

            _builder.Create(FeatType.ShurikenStorm)
                .Name(LocaleString.ShurikenStorm)
                .Description(LocaleString.ShurikenStormDescription)
                .IsWeaponSkill(SkillType.Throwing, 540)
                .RequirementTP(1250)
                .ResistType(Resist)
                .IncreasesStat(StatType.QueuedDMGBonus, DMG)
                .TelegraphSize(3f, 3f)
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

        private void PhantomHurl()
        {
            const int DMG = 16;
            const ResistType Resist = ResistType.Darkness;
            const DamageType DamageType = DamageType.Darkness;
            const VisualEffectType Vfx = VisualEffectType.ImpDoom;

            _builder.Create(FeatType.PhantomHurl)
                .Name(LocaleString.PhantomHurl)
                .Description(LocaleString.PhantomHurlDescription)
                .IsWeaponSkill(SkillType.Throwing, 860)
                .RequirementTP(1500)
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

        private void FlameToss()
        {
            _builder.Create(FeatType.FlameToss)
                .Name(LocaleString.FlameToss)
                .Description(LocaleString.FlameTossDescription)
                .IsWeaponSkill(SkillType.Throwing, 1130)
                .RequirementTP(1350)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Fire, 20);
                    _status.Value.ApplyStatusEffect<BurnStatusEffect>(activator, target, duration);
                });
        }

        private void FrostDart()
        {
            _builder.Create(FeatType.FrostDart)
                .Name(LocaleString.FrostDart)
                .Description(LocaleString.FrostDartDescription)
                .IsWeaponSkill(SkillType.Throwing, 1390)
                .RequirementTP(2000)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Ice, 10);
                    _status.Value.ApplyStatusEffect<FrostDartStatusEffect>(activator, target, duration);
                });
        }

        private void StarStrike()
        {
            _builder.Create(FeatType.StarStrike)
                .Name(LocaleString.StarStrike)
                .Description(LocaleString.StarStrikeDescription)
                .HasPassiveWeaponSkill<StarStrikeStatusEffect>(SkillType.Throwing);
        }
    }
}
