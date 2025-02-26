using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.WeaponSkill;
using XM.Progression.Ability;
using XM.Progression.Skill;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Weapon
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class AxeAbilities : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

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
