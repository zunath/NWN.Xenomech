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
    internal class GreatAxeAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();

        public GreatAxeAbilities(
            Lazy<CombatService> combat,
            Lazy<StatusEffectService> status)
            : base(combat, status)
        {
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
                .IsWeaponSkill(SkillType.GreatAxe, 860)
                .RequirementTP(1500)
                .ResistType(ResistType.Lightning)
                .IncreasesStat(StatType.QueuedDMGBonus, 28);
        }

        private void Knockout()
        {

        }

        private void FurySlash()
        {

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
