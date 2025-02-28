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
    internal class DaggerAbilities : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            WaspSting();
            GustSlash();
            Cyclone();
            SharkBite();
            Shadowstitch();
            EnergyDrain();
            DancingEdge();

            return _builder.Build();
        }

        private void WaspSting()
        {

        }

        private void GustSlash()
        {

        }

        private void Cyclone()
        {

        }

        private void SharkBite()
        {
            _builder.Create(FeatType.SharkBite)
                .Name(LocaleString.SharkBite)
                .Description(LocaleString.SharkBiteDescription)
                .IsWeaponSkill(SkillType.Dagger, 860)
                .RequirementTP(1500)
                .ResistType(ResistType.Water)
                .IncreasesStat(StatType.QueuedDMGBonus, 18);
        }

        private void Shadowstitch()
        {

        }

        private void EnergyDrain()
        {

        }

        private void DancingEdge()
        {
            _builder.Create(FeatType.DancingEdge)
                .Name(LocaleString.DancingEdge)
                .Description(LocaleString.DancingEdgeDescription)
                .HasPassiveWeaponSkill<DancingEdgeStatusEffect>(SkillType.Dagger);
        }
    }
}
