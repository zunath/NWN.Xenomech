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
            _builder.Create(FeatType.ShiningStrike)
                .Name(LocaleString.ShiningStrike)
                .Description(LocaleString.ShiningStrikeDescription)
                .IsWeaponSkill(SkillType.Club, 160)
                .RequirementTP(500)
                .ResistType(ResistType.Light)
                .IncreasesStat(StatType.QueuedDMGBonus, 7);
        }

        private void SeraphStrike()
        {

        }

        private void Brainshaker()
        {
            _builder.Create(FeatType.Brainshaker)
                .Name(LocaleString.Brainshaker)
                .Description(LocaleString.BrainshakerDescription)
                .IsWeaponSkill(SkillType.Club, 540)
                .RequirementTP(1250)
                .ResistType(ResistType.Mind)
                .IncreasesStat(StatType.QueuedDMGBonus, 11);
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
