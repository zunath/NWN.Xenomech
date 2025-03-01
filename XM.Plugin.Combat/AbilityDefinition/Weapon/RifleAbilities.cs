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
    internal class RifleAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();

        public RifleAbilities(
            Lazy<CombatService> combat,
            Lazy<StatusEffectService> status)
            : base(combat, status)
        {
        }
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            HotShot();
            SplitShot();
            SniperShot();
            SlugShot();
            BlastShot();
            HeavyShot();
            Trueflight();

            return _builder.Build();
        }

        private void HotShot()
        {
            _builder.Create(FeatType.HotShot)
                .Name(LocaleString.HotShot)
                .Description(LocaleString.HotShotDescription)
                .IsWeaponSkill(SkillType.Rifle, 160)
                .RequirementTP(500)
                .ResistType(ResistType.Fire)
                .IncreasesStat(StatType.QueuedDMGBonus, 9);
        }

        private void SplitShot()
        {
            const int DMG = 12;
            const ResistType Resist = ResistType.Water;
            const DamageType DamageType = DamageType.Water;
            const VisualEffectType Vfx = VisualEffectType.ImpDeathWard;

            _builder.Create(FeatType.SplitShot)
                .Name(LocaleString.SplitShot)
                .Description(LocaleString.SplitShotDescription)
                .IsWeaponSkill(SkillType.Rifle, 240)
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

        private void SniperShot()
        {
            _builder.Create(FeatType.SniperShot)
                .Name(LocaleString.SniperShot)
                .Description(LocaleString.SniperShotDescription)
                .IsWeaponSkill(SkillType.Rifle, 540)
                .RequirementTP(1250)
                .ResistType(ResistType.Wind)
                .IncreasesStat(StatType.QueuedDMGBonus, 14);
        }

        private void SlugShot()
        {
            const int DMG = 18;
            const ResistType Resist = ResistType.Mind;
            const DamageType DamageType = DamageType.Mind;
            const VisualEffectType Vfx = VisualEffectType.ImpDestruction;

            _builder.Create(FeatType.SlugShot)
                .Name(LocaleString.SlugShot)
                .Description(LocaleString.SlugShotDescription)
                .IsWeaponSkill(SkillType.Rifle, 860)
                .RequirementTP(1500)
                .ResistType(Resist)
                .IncreasesStat(StatType.QueuedDMGBonus, DMG)
                .TelegraphSize(4f, 4f)
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

        private void BlastShot()
        {

        }

        private void HeavyShot()
        {

        }

        private void Trueflight()
        {
            _builder.Create(FeatType.Trueflight)
                .Name(LocaleString.Trueflight)
                .Description(LocaleString.TrueflightDescription)
                .HasPassiveWeaponSkill<TrueFlightStatusEffect>(SkillType.Rifle);
        }
    }
}
