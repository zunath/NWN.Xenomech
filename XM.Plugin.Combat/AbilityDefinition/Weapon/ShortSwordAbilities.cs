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
    internal class ShortSwordAbilities : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            PiercingBlade();
            BurningEdge();
            SoulBlade();
            IceFang();
            SonicSlash();
            EmberFang();
            FrostbiteBlade();

            return _builder.Build();
        }

        private void PiercingBlade()
        {
            _builder.Create(FeatType.PiercingBlade)
                .Name(LocaleString.PiercingBlade)
                .Description(LocaleString.PiercingBladeDescription)
                .IsWeaponSkill(SkillType.ShortSword, 160)
                .RequirementTP(500)
                .ResistType(ResistType.Wind)
                .IncreasesStat(StatType.QueuedDMGBonus, 9);
        }

        private void BurningEdge()
        {

        }

        private void SoulBlade()
        {
            _builder.Create(FeatType.SoulBlade)
                .Name(LocaleString.SoulBlade)
                .Description(LocaleString.SoulBladeDescription)
                .IsWeaponSkill(SkillType.ShortSword, 540)
                .RequirementTP(1250)
                .ResistType(ResistType.Darkness)
                .IncreasesStat(StatType.QueuedDMGBonus, 14);
        }

        private void IceFang()
        {

        }

        private void SonicSlash()
        {

        }

        private void EmberFang()
        {

        }

        private void FrostbiteBlade()
        {
            _builder.Create(FeatType.FrostbiteBlade)
                .Name(LocaleString.FrostbiteBlade)
                .Description(LocaleString.FrostbiteBladeDescription)
                .HasPassiveWeaponSkill<FrostbiteBladeStatusEffect>(SkillType.ShortSword);
        }
    }
}
