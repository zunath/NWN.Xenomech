using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.WeaponSkill;
using XM.Progression.Ability;
using XM.Progression.Skill;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Weapon
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class GreatAxeAbilities : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ShieldBreak();
            IronTempest();
            FellCleave();
            GrandSlash();
            Knockout();
            FurySlash();
            Upheaval();

            return _builder.Build();
        }

        private void ShieldBreak()
        {

        }

        private void IronTempest()
        {

        }

        private void FellCleave()
        {

        }

        private void GrandSlash()
        {
            _builder.Create(FeatType.GrandSlash)
                .Name(LocaleString.GrandSlash)
                .Description(LocaleString.GrandSlashDescription)
                .IsWeaponSkill(SkillType.GreatAxe, 860)
                .RequirementTP(1500)
                .ResistType(ResistType.Lightning)
                .IncreasesStat(StatType.QueuedDMGBonus, 28);
        }

        private void Knockout()
        {

        }

        private void FurySlash()
        {

        }

        private void Upheaval()
        {
            _builder.Create(FeatType.Upheaval)
                .Name(LocaleString.Upheaval)
                .Description(LocaleString.UpheavalDescription)
                .HasPassiveWeaponSkill<UpheavalStatusEffect>(SkillType.GreatAxe);
        }
    }
}
