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
    internal class GreatswordAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();
        private readonly Lazy<StatusEffectService> _status;
        private readonly SpellService _spell;

        public GreatswordAbilities(
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
            HardSlash();
            Frostbite();
            SickleMoon();
            SpinningSlash();
            ShockSlash();
            GroundStrike();
            Scourge();

            return _builder.Build();
        }

        private void HardSlash()
        {
            _builder.Create(FeatType.HardSlash)
                .Name(LocaleString.HardSlash)
                .Description(LocaleString.HardSlashDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.GreatSword, 160)
                .RequirementTP(500)
                .ResistType(ResistType.Earth)
                .IncreasesStat(StatType.QueuedDMGBonus, 12);
        }

        private void Frostbite()
        {
            const int DMG = 16;
            const ResistType Resist = ResistType.Ice;
            const DamageType DamageType = DamageType.Ice;
            const VisualEffectType Vfx = VisualEffectType.ImpFrostSmall;

            _builder.Create(FeatType.Frostbite)
                .Name(LocaleString.Frostbite)
                .Description(LocaleString.Frostbite)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.GreatSword, 240)
                .RequirementTP(1000)
                .ResistType(Resist)
                .IncreasesStat(StatType.QueuedDMGBonus, DMG)
                .TelegraphSize(6f, 2f)
                .HasTelegraphConeAction((activator, targets, location) =>
                {
                    DamageEffectImpact<SlowStatusEffect>(
                        activator,
                        targets,
                        DMG,
                        Resist,
                        DamageType,
                        Vfx,
                        8);
                });
        }

        private void SickleMoon()
        {
            const int DMG = 19;
            const ResistType Resist = ResistType.Wind;
            const DamageType DamageType = DamageType.Wind;
            const VisualEffectType Vfx = VisualEffectType.ImpAcidSmall;

            _builder.Create(FeatType.SickleMoon)
                .Name(LocaleString.SickleMoon)
                .Description(LocaleString.SickleMoonDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.GreatSword, 540)
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

        private void SpinningSlash()
        {
            const int DMG = 24;
            const ResistType Resist = ResistType.Darkness;
            const DamageType DamageType = DamageType.Darkness;
            const VisualEffectType Vfx = VisualEffectType.ImpAcidSmall;

            _builder.Create(FeatType.SpinningSlash)
                .Name(LocaleString.SpinningSlash)
                .Description(LocaleString.SpinningSlashDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.GreatSword, 860)
                .RequirementTP(1500)
                .ResistType(Resist)
                .IncreasesStat(StatType.QueuedDMGBonus, DMG)
                .TelegraphSize(2f, 2f)
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

        private void ShockSlash()
        {
            _builder.Create(FeatType.ShockSlash)
                .Name(LocaleString.ShockSlash)
                .Description(LocaleString.ShockSlashDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.GreatSword, 1130)
                .RequirementTP(1350)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Lightning, 6);
                    _status.Value.ApplyStatusEffect<ShockSlashStatusEffect>(activator, target, duration);
                });
        }

        private void GroundStrike()
        {
            _builder.Create(FeatType.GroundStrike)
                .Name(LocaleString.GroundStrike)
                .Description(LocaleString.GroundStrikeDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.GreatSword, 1390)
                .RequirementTP(2000)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Earth, 10);
                    _status.Value.ApplyStatusEffect<GroundStrikeStatusEffect>(activator, target, duration);
                });
        }

        private void Scourge()
        {
            _builder.Create(FeatType.Scourge)
                .Name(LocaleString.Scourge)
                .Description(LocaleString.ScourgeDescription)
                .HasPassiveWeaponSkill<ScourgeStatusEffect>(SkillType.GreatSword);
        }
    }
}
