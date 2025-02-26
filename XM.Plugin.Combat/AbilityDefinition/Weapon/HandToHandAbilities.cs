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
    internal class HandToHandAbilities : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

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
