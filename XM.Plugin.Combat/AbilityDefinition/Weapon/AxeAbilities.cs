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
    internal class AxeAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();
        private readonly Lazy<StatusEffectService> _status;
        private readonly SpellService _spell;
        private readonly StatService _stat;

        public AxeAbilities(
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
            RagingAxe();
            SmashAxe();
            GaleAxe();
            AvalancheAxe();
            SpinningAxe();
            Rampage();
            PrimalRend();

            return _builder.Build();
        }

        private void RagingAxe()
        {
            _builder.Create(FeatType.RagingAxe)
                .Name(LocaleString.RagingAxe)
                .Description(LocaleString.RagingAxeDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.Axe, 160)
                .RequirementTP(500)
                .ResistType(ResistType.Fire)
                .IncreasesStat(StatType.QueuedDMGBonus, 13);
        }

        private void SmashAxe()
        {
            const int DMG = 17;
            const ResistType Resist = ResistType.Earth;
            const DamageType DamageType = DamageType.Earth;
            const VisualEffectType Vfx = VisualEffectType.ImpAcidSmall;

            _builder.Create(FeatType.SmashAxe)
                .Name(LocaleString.SmashAxe)
                .Description(LocaleString.SmashAxeDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.Axe, 240)
                .RequirementTP(1000)
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

        private void GaleAxe()
        {
            const int DMG = 21;
            const ResistType Resist = ResistType.Wind;
            const DamageType DamageType = DamageType.Wind;
            const VisualEffectType Vfx = VisualEffectType.ImpDeathWard;

            _builder.Create(FeatType.GaleAxe)
                .Name(LocaleString.GaleAxe)
                .Description(LocaleString.GaleAxeDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.Axe, 540)
                .RequirementTP(1250)
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

        private void AvalancheAxe()
        {
            const int DMG = 26;
            const ResistType Resist = ResistType.Ice;
            const DamageType DamageType = DamageType.Ice;
            const VisualEffectType Vfx = VisualEffectType.ImpFrostSmall;

            _builder.Create(FeatType.AvalancheAxe)
                .Name(LocaleString.AvalancheAxe)
                .Description(LocaleString.AvalancheAxeDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.Axe, 860)
                .RequirementTP(1500)
                .ResistType(Resist)
                .IncreasesStat(StatType.QueuedDMGBonus, DMG)
                .TelegraphSize(3f, 1f)
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

        private void SpinningAxe()
        {
            _builder.Create(FeatType.SpinningAxe)
                .Name(LocaleString.SpinningAxe)
                .Description(LocaleString.SpinningAxeDescription)
                .Classification(AbilityCategoryType.Offensive)
                .IsWeaponSkill(SkillType.Axe, 1130)
                .RequirementTP(1350)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Ice, 6);
                    _status.Value.ApplyStatusEffect<SpinningAxeStatusEffect>(activator, target, duration);
                });
        }

        private void Rampage()
        {
            _builder.Create(FeatType.Rampage)
                .Name(LocaleString.Rampage)
                .Description(LocaleString.RampageDescription)
                .Classification(AbilityCategoryType.Defensive)
                .IsWeaponSkill(SkillType.Axe, 1390)
                .RequirementTP(2000)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.Value.ApplyStatusEffect<RampageStatusEffect>(activator, activator, 1);
                });
        }

        private void PrimalRend()
        {
            _builder.Create(FeatType.PrimalRend)
                .Name(LocaleString.PrimalRend)
                .Description(LocaleString.PrimalRendDescription)
                .HasPassiveWeaponSkill<PrimalRendStatusEffect>(SkillType.Axe);
        }
    }
}
