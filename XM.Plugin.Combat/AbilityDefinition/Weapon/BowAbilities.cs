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
    internal class BowAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();
        private readonly Lazy<StatusEffectService> _status;
        private readonly SpellService _spell;
        private readonly StatService _stat;

        public BowAbilities(
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
            FlamingArrow();
            PiercingArrow();
            DullingArrow();
            Sidewinder();
            BlastArrow();
            ArchingArrow();
            ApexArrow();

            return _builder.Build();
        }

        private void FlamingArrow()
        {
            _builder.Create(FeatType.FlamingArrow)
                .Name(LocaleString.FlamingArrow)
                .Description(LocaleString.FlamingArrowDescription)
                .IsWeaponSkill(SkillType.Bow, 160)
                .RequirementTP(500)
                .ResistType(ResistType.Fire)
                .IncreasesStat(StatType.QueuedDMGBonus, 10);
        }

        private void PiercingArrow()
        {
            const int DMG = 13;
            const ResistType Resist = ResistType.Ice;
            const DamageType DamageType = DamageType.Ice;
            const VisualEffectType Vfx = VisualEffectType.ImpFrostSmall;

            _builder.Create(FeatType.PiercingArrow)
                .Name(LocaleString.PiercingArrow)
                .Description(LocaleString.PiercingArrowDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.Bow, 240)
                .RequirementTP(1000)
                .ResistType(Resist)
                .IncreasesStat(StatType.QueuedDMGBonus, DMG)
                .TelegraphSize(5f, 2f)
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

        private void DullingArrow()
        {
            const int DMG = 16;
            const ResistType Resist = ResistType.Mind;
            const DamageType DamageType = DamageType.Mind;
            const VisualEffectType Vfx = VisualEffectType.ImpDestruction;

            _builder.Create(FeatType.DullingArrow)
                .Name(LocaleString.DullingArrow)
                .Description(LocaleString.DullingArrowDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.Bow, 540)
                .RequirementTP(1250)
                .ResistType(Resist)
                .IncreasesStat(StatType.QueuedDMGBonus, DMG)
                .TelegraphSize(5f, 2f)
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

        private void Sidewinder()
        {
            const int DMG = 20;
            const ResistType Resist = ResistType.Wind;
            const DamageType DamageType = DamageType.Wind;
            const VisualEffectType Vfx = VisualEffectType.ImpDeathWard;

            _builder.Create(FeatType.Sidewinder)
                .Name(LocaleString.Sidewinder)
                .Description(LocaleString.SidewinderDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.Bow, 860)
                .RequirementTP(1500)
                .ResistType(Resist)
                .IncreasesStat(StatType.QueuedDMGBonus, DMG)
                .TelegraphSize(5f, 2f)
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

        private void BlastArrow()
        {
            _builder.Create(FeatType.BlastArrow)
                .Name(LocaleString.BlastArrow)
                .Description(LocaleString.BlastArrowDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.Bow, 1130)
                .RequirementTP(1350)
                .HasImpactAction((activator, target, location) =>
                {
                    var amount = _spell.CalculateResistedTicks(target, ResistType.Mind, 50);
                    if (amount <= 0)
                    {
                        SendMessageToPC(activator, LocaleString.YourSpellWasResisted.ToLocalizedString());
                        return;
                    }

                    var percent = amount * 0.01f;
                    var tp = (int)(_stat.GetCurrentTP(target) * percent);

                    _stat.SetTP(target, tp);
                    SendMessageToPC(activator, LocaleString.XTPHasBeenReducedByYPercent.ToLocalizedString(GetName(target), amount));

                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpReduceAbilityScore), target);
                });
        }

        private void ArchingArrow()
        {
            _builder.Create(FeatType.ArchingArrow)
                .Name(LocaleString.ArchingArrow)
                .Description(LocaleString.ArchingArrowDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.Bow, 1390)
                .RequirementTP(2000)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Mind, 40);
                    _status.Value.ApplyStatusEffect<BlindStatusEffect>(activator, target, duration);
                });
        }

        private void ApexArrow()
        {
            _builder.Create(FeatType.ApexArrow)
                .Name(LocaleString.ApexArrow)
                .Description(LocaleString.ApexArrowDescription)
                .HasPassiveWeaponSkill<ApexArrowStatusEffect>(SkillType.Bow);
        }
    }
}
