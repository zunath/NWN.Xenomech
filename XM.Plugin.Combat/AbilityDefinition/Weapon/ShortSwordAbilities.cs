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
    internal class ShortSwordAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();
        private readonly Lazy<StatusEffectService> _status;
        private readonly SpellService _spell;

        public ShortSwordAbilities(
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
            PiercingBlade();
            BurningEdge();
            SoulBlade();
            IceFang();
            SonicSlash();
            EmberFang();
            FrostbiteBlade();

            return _builder.Build();
        }

        private void PiercingBlade()
        {
            _builder.Create(FeatType.PiercingBlade)
                .Name(LocaleString.PiercingBlade)
                .Description(LocaleString.PiercingBladeDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.ShortSword, 160)
                .RequirementTP(500)
                .ResistType(ResistType.Wind)
                .IncreasesStat(StatType.QueuedDMGBonus, 9);
        }

        private void BurningEdge()
        {
            const int DMG = 12;
            const ResistType Resist = ResistType.Fire;
            const DamageType DamageType = DamageType.Fire;
            const VisualEffectType Vfx = VisualEffectType.ComHitFire;

            _builder.Create(FeatType.BurningEdge)
                .Name(LocaleString.BurningEdge)
                .Description(LocaleString.BurningEdgeDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.ShortSword, 240)
                .RequirementTP(1000)
                .ResistType(Resist)
                .IncreasesStat(StatType.QueuedDMGBonus, DMG)
                .TelegraphSize(3f, 2f)
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

        private void SoulBlade()
        {
            _builder.Create(FeatType.SoulBlade)
                .Name(LocaleString.SoulBlade)
                .Description(LocaleString.SoulBladeDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.ShortSword, 540)
                .RequirementTP(1250)
                .ResistType(ResistType.Darkness)
                .IncreasesStat(StatType.QueuedDMGBonus, 14);
        }

        private void IceFang()
        {
            const int DMG = 18;
            const ResistType Resist = ResistType.Ice;
            const DamageType DamageType = DamageType.Ice;
            const VisualEffectType Vfx = VisualEffectType.ImpFrostSmall;

            _builder.Create(FeatType.IceFang)
                .Name(LocaleString.IceFang)
                .Description(LocaleString.IceFangDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.ShortSword, 860)
                .RequirementTP(1500)
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

        private void SonicSlash()
        {
            _builder.Create(FeatType.SonicSlash)
                .Name(LocaleString.SonicSlash)
                .Description(LocaleString.SonicSlashDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.ShortSword, 1130)
                .RequirementTP(1350)
                .TelegraphSize(4f, 2f)
                .HasTelegraphSphereAction((activator, targets, location) =>
                {
                    foreach (var target in targets)
                    {
                        if (GetFactionEqual(target, activator))
                            continue;

                        var duration = _spell.CalculateResistedTicks(target, ResistType.Wind, 6);
                        _status.Value.ApplyStatusEffect<SonicSlashStatusEffect>(activator, target, duration);
                    }
                });
        }

        private void EmberFang()
        {
            _builder.Create(FeatType.EmberFang)
                .Name(LocaleString.EmberFang)
                .Description(LocaleString.EmberFangDescription)
                .Classification(AbilityCategoryType.Defensive)
                .IsWeaponSkill(SkillType.ShortSword, 1390)
                .RequirementTP(2000)
                .HasActivationDelay(2f)
                .TelegraphSize(3f, 3f)
                .HasTelegraphSphereAction((activator, targets, location) =>
                {
                    foreach (var target in targets)
                    {
                        if (!GetFactionEqual(target, activator))
                            continue;

                        _status.Value.ApplyStatusEffect<EmberFangStatusEffect>(activator, target, 1);
                    }
                });
        }

        private void FrostbiteBlade()
        {
            _builder.Create(FeatType.FrostbiteBlade)
                .Name(LocaleString.FrostbiteBlade)
                .Description(LocaleString.FrostbiteBladeDescription)
                .HasPassiveWeaponSkill<FrostbiteBladeStatusEffect>(SkillType.ShortSword);
        }
    }
}
