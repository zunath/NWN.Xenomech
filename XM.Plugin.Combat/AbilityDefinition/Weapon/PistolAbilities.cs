using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
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
    internal class PistolAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();
        private readonly Lazy<StatusEffectService> _status;
        private readonly SpellService _spell;

        public PistolAbilities(
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
            _builder.Create(FeatType.ShadowBarrage)
                .Name(LocaleString.ShadowBarrage)
                .Description(LocaleString.ShadowBarrageDescription)
                .IsWeaponSkill(SkillType.Pistol, 1130)
                .RequirementTP(1350)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Darkness, 6);
                    _status.Value.ApplyStatusEffect<ShadowBarrageStatusEffect>(activator, target, duration);
                });
        }

        private void Deadeye()
        {
            _builder.Create(FeatType.Deadeye)
                .Name(LocaleString.Deadeye)
                .Description(LocaleString.DeadeyeDescription)
                .IsWeaponSkill(SkillType.Pistol, 1390)
                .RequirementTP(2000)
                .HasActivationDelay(2f)
                .TelegraphSize(3f, 3f)
                .HasTelegraphSphereAction((activator, targets, location) =>
                {
                    foreach (var target in targets)
                    {
                        if (!GetFactionEqual(target, activator))
                            continue;

                        _status.Value.ApplyStatusEffect<DeadeyeStatusEffect>(activator, target, 1);
                    }
                });
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
