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
    internal class LongswordAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();
        private readonly Lazy<StatusEffectService> _status;
        private readonly SpellService _spell;

        public LongswordAbilities(
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
            FastBlade();
            BurningBlade();
            RedLotusBlade();
            VorpalBlade();
            FlatBlade();
            Atonement();
            ShiningBlade();

            return _builder.Build();
        }

        private void FastBlade()
        {
            _builder.Create(FeatType.FastBlade)
                .Name(LocaleString.FastBlade)
                .Description(LocaleString.FastBladeDescription)
                .IsWeaponSkill(SkillType.Longsword, 160)
                .RequirementTP(500)
                .ResistType(ResistType.Darkness)
                .IncreasesStat(StatType.QueuedDMGBonus, 10);
        }

        private void BurningBlade()
        {
            const int DMG = 13;
            const ResistType Resist = ResistType.Fire;
            const DamageType DamageType = DamageType.Fire;
            const VisualEffectType Vfx = VisualEffectType.ComHitFire;

            _builder.Create(FeatType.BurningBlade)
                .Name(LocaleString.BurningBlade)
                .Description(LocaleString.BurningBladeDescription)
                .IsWeaponSkill(SkillType.Longsword, 240)
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

        private void RedLotusBlade()
        {
            const int DMG = 16;
            const ResistType Resist = ResistType.Fire;
            const DamageType DamageType = DamageType.Fire;
            const VisualEffectType Vfx = VisualEffectType.ComHitFire;

            _builder.Create(FeatType.RedLotusBlade)
                .Name(LocaleString.RedLotusBlade)
                .Description(LocaleString.RedLotusBladeDescription)
                .IsWeaponSkill(SkillType.Longsword, 540)
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

        private void VorpalBlade()
        {
            const int DMG = 20;
            const ResistType Resist = ResistType.Water;
            const DamageType DamageType = DamageType.Water;
            const VisualEffectType Vfx = VisualEffectType.None;
            const int DurationTicks = 10;

            _builder.Create(FeatType.VorpalBlade)
                .Name(LocaleString.VorpalBlade)
                .Description(LocaleString.VorpalBladeDescription)
                .IsWeaponSkill(SkillType.Longsword, 860)
                .RequirementTP(1500)
                .ResistType(Resist)
                .IncreasesStat(StatType.QueuedDMGBonus, DMG)
                .HasImpactAction((activator, target, location) =>
                {
                    DamageEffectImpact<SlowStatusEffect>(
                        activator,
                        target,
                        DMG,
                        Resist,
                        DamageType,
                        Vfx,
                        DurationTicks);
                });
        }

        private void FlatBlade()
        {
            _builder.Create(FeatType.FlatBlade)
                .Name(LocaleString.FlatBlade)
                .Description(LocaleString.FlatBladeDescription)
                .IsWeaponSkill(SkillType.Longsword, 1130)
                .RequirementTP(1350)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Mind, 20);
                    _status.Value.ApplyStatusEffect<BlindStatusEffect>(activator, target, duration);
                });
        }

        private void Atonement()
        {

        }

        private void ShiningBlade()
        {
            _builder.Create(FeatType.ShiningBlade)
                .Name(LocaleString.ShiningBlade)
                .Description(LocaleString.ShiningBladeDescription)
                .HasPassiveWeaponSkill<ShiningBladeStatusEffect>(SkillType.Longsword);
        }
    }
}
