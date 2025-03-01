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
    internal class PistolAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();

        public PistolAbilities(
            Lazy<CombatService> combat,
            Lazy<StatusEffectService> status)
            : base(combat, status)
        {
        }
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            QuickDraw();
            BurningShot();
            Ricochet();
            PiercingShot();
            ShadowBarrage();
            Deadeye();
            TrueShot();

            return _builder.Build();
        }

        private void QuickDraw()
        {
            _builder.Create(FeatType.QuickDraw)
                .Name(LocaleString.QuickDraw)
                .Description(LocaleString.QuickDrawDescription)
                .IsWeaponSkill(SkillType.Pistol, 160)
                .RequirementTP(500)
                .ResistType(ResistType.Lightning)
                .IncreasesStat(StatType.QueuedDMGBonus, 8);
        }

        private void BurningShot()
        {
            const int DMG = 10;
            const ResistType Resist = ResistType.Fire;
            const DamageType DamageType = DamageType.Fire;
            const VisualEffectType Vfx = VisualEffectType.ComHitFire;

            _builder.Create(FeatType.BurningShot)
                .Name(LocaleString.BurningShot)
                .Description(LocaleString.BurningShotDescription)
                .IsWeaponSkill(SkillType.Pistol, 240)
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

        private void Ricochet()
        {
            const int DMG = 13;
            const ResistType Resist = ResistType.Water;
            const DamageType DamageType = DamageType.Water;
            const VisualEffectType Vfx = VisualEffectType.ImpDeathWard;

            _builder.Create(FeatType.Ricochet)
                .Name(LocaleString.Ricochet)
                .Description(LocaleString.RicochetDescription)
                .IsWeaponSkill(SkillType.Pistol, 540)
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

        private void PiercingShot()
        {
            const int DMG = 16;
            const ResistType Resist = ResistType.Mind;
            const DamageType DamageType = DamageType.Mind;
            const VisualEffectType Vfx = VisualEffectType.ImpDeathWard;

            _builder.Create(FeatType.PiercingShot)
                .Name(LocaleString.PiercingShot)
                .Description(LocaleString.PiercingShotDescription)
                .IsWeaponSkill(SkillType.Pistol, 860)
                .RequirementTP(1500)
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

        private void ShadowBarrage()
        {

        }

        private void Deadeye()
        {

        }

        private void TrueShot()
        {
            _builder.Create(FeatType.TrueShot)
                .Name(LocaleString.TrueShot)
                .Description(LocaleString.TrueShotDescription)
                .HasPassiveWeaponSkill<TrueShotStatusEffect>(SkillType.Pistol);
        }
    }
}
