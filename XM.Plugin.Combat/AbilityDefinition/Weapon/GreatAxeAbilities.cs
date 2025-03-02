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
    internal class GreatAxeAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();
        private readonly Lazy<StatusEffectService> _status;
        private readonly SpellService _spell;

        public GreatAxeAbilities(
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
            ShieldBreak();
            IronTempest();
            FellCleave();
            GrandSlash();
            Knockout();
            FurySlash();
            Upheaval();

            return _builder.Build();
        }

        private void ShieldBreak()
        {
            const int DMG = 14;
            const ResistType Resist = ResistType.Earth;

            _builder.Create(FeatType.ShieldBreak)
                .Name(LocaleString.ShieldBreak)
                .Description(LocaleString.ShieldBreakDescription)
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
                .IsWeaponSkill(SkillType.GreatAxe, 160)
                .RequirementTP(500)
                .ResistType(Resist)
                .IncreasesStat(StatType.QueuedDMGBonus, DMG);
        }

        private void IronTempest()
        {
            const int DMG = 18;
            const ResistType Resist = ResistType.Wind;
            const DamageType DamageType = DamageType.Wind;
            const VisualEffectType Vfx = VisualEffectType.ImpAcidSmall;

            _builder.Create(FeatType.IronTempest)
                .Name(LocaleString.IronTempest)
                .Description(LocaleString.IronTempestDescription)
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
                .IsWeaponSkill(SkillType.GreatAxe, 240)
                .RequirementTP(1000)
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

        private void FellCleave()
        {
            const int DMG = 22;
            const ResistType Resist = ResistType.Darkness;
            const DamageType DamageType = DamageType.Darkness;
            const VisualEffectType Vfx = VisualEffectType.ImpDoom;

            _builder.Create(FeatType.FellCleave)
                .Name(LocaleString.FellCleave)
                .Description(LocaleString.FellCleaveDescription)
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
                .IsWeaponSkill(SkillType.GreatAxe, 540)
                .RequirementTP(1250)
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

        private void GrandSlash()
        {
            _builder.Create(FeatType.GrandSlash)
                .Name(LocaleString.GrandSlash)
                .Description(LocaleString.GrandSlashDescription)
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
                .IsWeaponSkill(SkillType.GreatAxe, 860)
                .RequirementTP(1500)
                .ResistType(ResistType.Lightning)
                .IncreasesStat(StatType.QueuedDMGBonus, 28);
        }

        private void Knockout()
        {
            _builder.Create(FeatType.Knockout)
                .Name(LocaleString.Knockout)
                .Description(LocaleString.KnockoutDescription)
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
                .IsWeaponSkill(SkillType.GreatAxe, 1130)
                .RequirementTP(1350)
                .HasActivationDelay(2f)
                .TelegraphSize(2f, 2f)
                .HasTelegraphSphereAction((activator, targets, location) =>
                {
                    foreach (var target in targets)
                    {
                        if (GetFactionEqual(target, activator))
                            continue;

                        var duration = _spell.CalculateResistedTicks(target, ResistType.Mind, 32);
                        _status.Value.ApplyStatusEffect<KnockdownStatusEffect>(activator, target, duration);
                    }
                });
        }

        private void FurySlash()
        {
            _builder.Create(FeatType.FurySlash)
                .Name(LocaleString.FurySlash)
                .Description(LocaleString.FurySlashDescription)
                .Classification(AbilityCategoryType.Defensive)
                .TargetingType(AbilityTargetingType.SelfTargetsParty)
                .IsWeaponSkill(SkillType.GreatAxe, 1390)
                .RequirementTP(2000)
                .HasActivationDelay(2f)
                .TelegraphSize(2f, 2f)
                .HasTelegraphSphereAction((activator, targets, location) =>
                {
                    foreach (var target in targets)
                    {
                        if (!GetFactionEqual(target, activator))
                            continue;

                        _status.Value.ApplyStatusEffect<FurySlashStatusEffect>(activator, target, 1);
                    }
                });
        }

        private void Upheaval()
        {
            _builder.Create(FeatType.Upheaval)
                .Name(LocaleString.Upheaval)
                .Description(LocaleString.UpheavalDescription)
                .HasPassiveWeaponSkill<UpheavalStatusEffect>(SkillType.GreatAxe);
        }
    }
}
