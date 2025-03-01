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
    internal class ShortSwordAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();

        public ShortSwordAbilities(
            Lazy<CombatService> combat,
            Lazy<StatusEffectService> status)
            : base(combat, status)
        {
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

        }

        private void EmberFang()
        {

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
