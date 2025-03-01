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
    internal class AxeAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();

        public AxeAbilities(
            Lazy<CombatService> combat,
            Lazy<StatusEffectService> status)
            : base(combat, status)
        {
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
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
                .IsWeaponSkill(SkillType.Axe, 160)
                .RequirementTP(500)
                .ResistType(ResistType.Fire)
                .IncreasesStat(StatType.QueuedDMGBonus, 13);
        }

        private void SmashAxe()
        {

        }

        private void GaleAxe()
        {

        }

        private void AvalancheAxe()
        {

        }

        private void SpinningAxe()
        {

        }

        private void Rampage()
        {

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
