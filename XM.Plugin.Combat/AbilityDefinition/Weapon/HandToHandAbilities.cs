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
    internal class HandToHandAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();
        private readonly Lazy<StatusEffectService> _status;
        private readonly SpellService _spell;

        public HandToHandAbilities(
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
            Combo();
            RagingFists();
            HowlingFist();
            FinalHeaven();
            DragonBlow();
            OneInchPunch();
            AsuranFists();

            return _builder.Build();
        }

        private void Combo()
        {
            _builder.Create(FeatType.Combo)
                .Name(LocaleString.Combo)
                .Description(LocaleString.ComboDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.HandToHand, 160)
                .RequirementTP(500)
                .ResistType(ResistType.Mind)
                .IncreasesStat(StatType.QueuedDMGBonus, 11);
        }

        private void RagingFists()
        {
            const int DMG = 14;
            const ResistType Resist = ResistType.Fire;
            const DamageType DamageType = DamageType.Fire;
            const VisualEffectType Vfx = VisualEffectType.ComHitFire;

            _builder.Create(FeatType.RagingFists)
                .Name(LocaleString.RagingFists)
                .Description(LocaleString.RagingFistsDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.HandToHand, 240)
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

        private void HowlingFist()
        {
            const int DMG = 18;
            const ResistType Resist = ResistType.Wind;
            const DamageType DamageType = DamageType.Wind;
            const VisualEffectType Vfx = VisualEffectType.ImpAcidSmall;

            _builder.Create(FeatType.HowlingFist)
                .Name(LocaleString.HowlingFist)
                .Description(LocaleString.HowlingFistDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.HandToHand, 540)
                .RequirementTP(1250)
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

        private void FinalHeaven()
        {
            const int DMG = 22;
            const ResistType Resist = ResistType.Light;
            const DamageType DamageType = DamageType.Light;
            const VisualEffectType Vfx = VisualEffectType.ImpDivineStrikeHoly;

            _builder.Create(FeatType.FinalHeaven)
                .Name(LocaleString.FinalHeaven)
                .Description(LocaleString.FinalHeavenDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.HandToHand, 860)
                .RequirementTP(1500)
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

        private void DragonBlow()
        {
            _builder.Create(FeatType.DragonBlow)
                .Name(LocaleString.DragonBlow)
                .Description(LocaleString.DragonBlowDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.HandToHand, 1130)
                .RequirementTP(1350)
                .TelegraphSize(4f, 2f)
                .HasTelegraphLineAction((activator, targets, location) =>
                {
                    foreach (var target in targets)
                    {
                        var duration = _spell.CalculateResistedTicks(target, ResistType.Fire, 20);
                        _status.Value.ApplyStatusEffect<BurnStatusEffect>(activator, target, duration);
                    }
                });
        }

        private void OneInchPunch()
        {
            _builder.Create(FeatType.OneInchPunch)
                .Name(LocaleString.OneInchPunch)
                .Description(LocaleString.OneInchPunchDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.HandToHand, 1390)
                .RequirementTP(2000)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Water, 10);
                    _status.Value.ApplyStatusEffect<OneInchPunchStatusEffect>(activator, target, duration);
                });
        }

        private void AsuranFists()
        {
            _builder.Create(FeatType.AsuranFists)
                .Name(LocaleString.AsuranFists)
                .Description(LocaleString.AsuranFistsDescription)
                .HasPassiveWeaponSkill<AsuranFistsStatusEffect>(SkillType.HandToHand);
        }
    }
}
