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
    internal class HandToHandAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();

        public HandToHandAbilities(
            Lazy<CombatService> combat,
            Lazy<StatusEffectService> status)
            : base(combat, status)
        {
        }
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Combo();
            RagingFists();
            HowlingFist();
            FinalHeaven();
            DragonBlow();
            OneInchPunch();
            AsuranFists();

            return _builder.Build();
        }

        private void Combo()
        {
            _builder.Create(FeatType.Combo)
                .Name(LocaleString.Combo)
                .Description(LocaleString.ComboDescription)
                .IsWeaponSkill(SkillType.HandToHand, 160)
                .RequirementTP(500)
                .ResistType(ResistType.Mind)
                .IncreasesStat(StatType.QueuedDMGBonus, 11);
        }

        private void RagingFists()
        {

        }

        private void HowlingFist()
        {

        }

        private void FinalHeaven()
        {

        }

        private void DragonBlow()
        {

        }

        private void OneInchPunch()
        {

        }

        private void AsuranFists()
        {
            _builder.Create(FeatType.AsuranFists)
                .Name(LocaleString.AsuranFists)
                .Description(LocaleString.AsuranFistsDescription)
                .HasPassiveWeaponSkill<AsuranFistsStatusEffect>(SkillType.HandToHand);
        }
    }
}
