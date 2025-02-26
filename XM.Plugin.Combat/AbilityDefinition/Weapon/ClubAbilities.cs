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
    internal class ClubAbilities : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ShiningStrike();
            SeraphStrike();
            Brainshaker();
            BlackHalo();
            FlashNova();
            Judgment();
            HexaStrike();

            return _builder.Build();
        }

        private void ShiningStrike()
        {

        }

        private void SeraphStrike()
        {

        }

        private void Brainshaker()
        {

        }

        private void BlackHalo()
        {

        }

        private void FlashNova()
        {

        }

        private void Judgment()
        {

        }

        private void HexaStrike()
        {
            _builder.Create(FeatType.HexaStrike)
                .Name(LocaleString.HexaStrike)
                .Description(LocaleString.HexaStrikeDescription)
                .HasPassiveWeaponSkill<HexaStrikeStatusEffect>(SkillType.Club);
        }
    }
}
