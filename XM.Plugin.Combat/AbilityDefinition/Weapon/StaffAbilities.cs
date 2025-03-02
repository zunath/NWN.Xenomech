using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
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
        private readonly Lazy<StatusEffectService> _status;
        private readonly SpellService _spell;
        private readonly StatService _stat;

        public StaffAbilities(
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
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
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
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
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
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
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
                .Classification(AbilityCategoryType.Offensive)
                .TargetingType(AbilityTargetingType.SelfTargetsEnemy)
                .IsWeaponSkill(SkillType.Staff, 860)
                .RequirementTP(1500)
                .ResistType(ResistType.Mind)
                .IncreasesStat(StatType.QueuedDMGBonus, 14);
        }

        private void Omniscience()
        {
            _builder.Create(FeatType.Omniscience)
                .Name(LocaleString.Omniscience)
                .Description(LocaleString.OmniscienceDescription)
                .Classification(AbilityCategoryType.Defensive)
                .TargetingType(AbilityTargetingType.SelfTargetsParty)
                .IsWeaponSkill(SkillType.Staff, 1130)
                .RequirementTP(1350)
                .TelegraphSize(4f, 4f)
                .HasTelegraphSphereAction((activator, targets, location) =>
                {
                    foreach (var target in targets)
                    {
                        if (!GetFactionEqual(target, activator))
                            continue;

                        _status.Value.ApplyStatusEffect<OmniscienceStatusEffect>(activator, target, 1);
                    }
                });
        }

        private void SpiritTaker()
        {
            _builder.Create(FeatType.SpiritTaker)
                .Name(LocaleString.SpiritTaker)
                .Description(LocaleString.SpiritTakerDescription)
                .Classification(AbilityCategoryType.EPRestoration)
                .IsWeaponSkill(SkillType.Staff, 1390)
                .RequirementTP(2000)
                .HasImpactAction((activator, target, location) =>
                {
                    var maxEP = _stat.GetMaxEP(activator);
                    var restore = (int)(maxEP * 0.4f);

                    _stat.RestoreEP(activator, restore);
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpRestorationGreater), activator);
                });
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
