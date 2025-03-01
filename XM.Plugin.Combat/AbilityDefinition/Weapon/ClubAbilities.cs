﻿using System;
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
    internal class ClubAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();
        private readonly Lazy<StatusEffectService> _status;
        private readonly SpellService _spell;
        private readonly StatService _stat;

        public ClubAbilities(
            Lazy<CombatService> combat,
            Lazy<StatusEffectService> status,
            SpellService spell,
            StatService stat)
            : base(combat, status)
        {
            _status = status;
            _spell = spell;
            _stat = stat;
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ShiningStrike();
            SeraphStrike();
            Brainshaker();
            BlackHalo();
            FlashNova();
            Judgment();
            HexaStrike();

            return _builder.Build();
        }

        private void ShiningStrike()
        {
            _builder.Create(FeatType.ShiningStrike)
                .Name(LocaleString.ShiningStrike)
                .Description(LocaleString.ShiningStrikeDescription)
                .IsWeaponSkill(SkillType.Club, 160)
                .RequirementTP(500)
                .ResistType(ResistType.Light)
                .IncreasesStat(StatType.QueuedDMGBonus, 7);
        }

        private void SeraphStrike()
        {
            const int DMG = 9;
            const ResistType Resist = ResistType.Light;
            const DamageType DamageType = DamageType.Light;
            const VisualEffectType Vfx = VisualEffectType.ImpDivineStrikeHoly;

            _builder.Create(FeatType.SeraphStrike)
                .Name(LocaleString.SeraphStrike)
                .Description(LocaleString.SeraphStrikeDescription)
                .IsWeaponSkill(SkillType.Club, 240)
                .RequirementTP(1000)
                .ResistType(Resist)
                .IncreasesStat(StatType.QueuedDMGBonus, DMG)
                .TelegraphSize(3f, 2f)
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

        private void Brainshaker()
        {
            _builder.Create(FeatType.Brainshaker)
                .Name(LocaleString.Brainshaker)
                .Description(LocaleString.BrainshakerDescription)
                .IsWeaponSkill(SkillType.Club, 540)
                .RequirementTP(1250)
                .ResistType(ResistType.Mind)
                .IncreasesStat(StatType.QueuedDMGBonus, 11);
        }

        private void BlackHalo()
        {
            const int DMG = 14;
            const ResistType Resist = ResistType.Darkness;
            const DamageType DamageType = DamageType.Darkness;
            const VisualEffectType Vfx = VisualEffectType.ImpDoom;

            _builder.Create(FeatType.BlackHalo)
                .Name(LocaleString.BlackHalo)
                .Description(LocaleString.BlackHaloDescription)
                .IsWeaponSkill(SkillType.Club, 860)
                .RequirementTP(1500)
                .ResistType(Resist)
                .IncreasesStat(StatType.QueuedDMGBonus, DMG)
                .TelegraphSize(5f, 5f)
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

        private void FlashNova()
        {
            _builder.Create(FeatType.FlashNova)
                .Name(LocaleString.FlashNova)
                .Description(LocaleString.FlashNovaDescription)
                .IsWeaponSkill(SkillType.Club, 1130)
                .RequirementTP(1350)
                .TelegraphSize(4f, 2f)
                .HasTelegraphConeAction((activator, targets, location) =>
                {
                    foreach (var target in targets)
                    {
                        var duration = _spell.CalculateResistedTicks(target, ResistType.Light, 6);
                        _status.Value.ApplyStatusEffect<BlindStatusEffect>(activator, target, duration);
                    }
                });
        }

        private void Judgment()
        {
            _builder.Create(FeatType.Judgment)
                .Name(LocaleString.Judgment)
                .Description(LocaleString.JudgmentDescription)
                .IsWeaponSkill(SkillType.Club, 1390)
                .RequirementTP(2000)
                .HasImpactAction((activator, target, location) =>
                {
                    var maxHP = _stat.GetMaxHP(activator);
                    var healing = (int)(maxHP * 0.4f);

                    AssignCommand(activator, () =>
                    {
                        ApplyEffectToObject(DurationType.Instant, EffectHeal(healing), activator);
                    });

                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpHealingExtra), activator);
                });
        }

        private void HexaStrike()
        {
            _builder.Create(FeatType.HexaStrike)
                .Name(LocaleString.HexaStrike)
                .Description(LocaleString.HexaStrikeDescription)
                .HasPassiveWeaponSkill<HexaStrikeStatusEffect>(SkillType.Club);
        }
    }
}
