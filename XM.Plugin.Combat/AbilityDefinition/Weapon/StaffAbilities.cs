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
    internal class StaffAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();

        public StaffAbilities(
            Lazy<CombatService> combat,
            Lazy<StatusEffectService> status)
            : base(combat, status)
        {
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            HeavySwing();
            RockCrusher();
            EarthCrusher();
            Starburst();
            Omniscience();
            SpiritTaker();
            Shattersoul();

            return _builder.Build();
        }

        private void HeavySwing()
        {
            _builder.Create(FeatType.HeavySwing)
                .Name(LocaleString.HeavySwing)
                .Description(LocaleString.HeavySwingDescription)
                .IsWeaponSkill(SkillType.Staff, 160)
                .RequirementTP(500)
                .ResistType(ResistType.Earth)
                .IncreasesStat(StatType.QueuedDMGBonus, 7);
        }

        private void RockCrusher()
        {
            const int DMG = 9;
            const ResistType Resist = ResistType.Earth;
            const DamageType DamageType = DamageType.Earth;
            const VisualEffectType Vfx = VisualEffectType.ImpAcidSmall;

            _builder.Create(FeatType.RockCrusher)
                .Name(LocaleString.RockCrusher)
                .Description(LocaleString.RockCrusherDescription)
                .IsWeaponSkill(SkillType.Staff, 240)
                .RequirementTP(1000)
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

        private void EarthCrusher()
        {
            const int DMG = 11;
            const ResistType Resist = ResistType.Earth;
            const DamageType DamageType = DamageType.Earth;
            const VisualEffectType Vfx = VisualEffectType.ImpAcidSmall;

            _builder.Create(FeatType.EarthCrusher)
                .Name(LocaleString.EarthCrusher)
                .Description(LocaleString.EarthCrusherDescription)
                .IsWeaponSkill(SkillType.Staff, 540)
                .RequirementTP(1250)
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

        private void Starburst()
        {
            _builder.Create(FeatType.Starburst)
                .Name(LocaleString.Starburst)
                .Description(LocaleString.StarburstDescription)
                .IsWeaponSkill(SkillType.Staff, 860)
                .RequirementTP(1500)
                .ResistType(ResistType.Mind)
                .IncreasesStat(StatType.QueuedDMGBonus, 14);
        }

        private void Omniscience()
        {

        }

        private void SpiritTaker()
        {

        }

        private void Shattersoul()
        {
            _builder.Create(FeatType.Shattersoul)
                .Name(LocaleString.Shattersoul)
                .Description(LocaleString.ShattersoulDescription)
                .HasPassiveWeaponSkill<ShattersoulStatusEffect>(SkillType.Staff);
        }
    }
}
