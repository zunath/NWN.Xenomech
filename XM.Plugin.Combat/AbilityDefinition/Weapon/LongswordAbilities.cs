using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.WeaponSkill;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Skill;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Weapon
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class LongswordAbilities : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            FastBlade();
            BurningBlade();
            RedLotusBlade();
            VorpalBlade();
            FlatBlade();
            Atonement();
            ShiningBlade();

            return _builder.Build();
        }

        private void FastBlade()
        {
            _builder.Create(FeatType.FastBlade)
                .Name(LocaleString.FastBlade)
                .Description(LocaleString.FastBladeDescription)
                .IsWeaponSkill(SkillType.Longsword, 160)
                .RequirementTP(500)
                .ResistType(ResistType.Darkness)
                .IncreasesStat(StatType.QueuedDMGBonus, 10);
        }

        private void BurningBlade()
        {
            _builder.Create(FeatType.BurningBlade)
                .Name(LocaleString.BurningBlade)
                .Description(LocaleString.BurningBladeDescription)
                .IsWeaponSkill(SkillType.Longsword, 240)
                .RequirementTP(1000)
                .ResistType(ResistType.Fire)
                .IncreasesStat(StatType.QueuedDMGBonus, 13)
                .TelegraphSize(4f, 2f)
                .HasTelegraphLineAction((activator, targets, location) =>
                {
                    foreach (var target in targets)
                    {
                        SendMessageToPC(activator, "Target = " + GetName(target));
                    }
                });
        }

        private void RedLotusBlade()
        {

        }

        private void VorpalBlade()
        {

        }

        private void FlatBlade()
        {

        }

        private void Atonement()
        {

        }

        private void ShiningBlade()
        {
            _builder.Create(FeatType.ShiningBlade)
                .Name(LocaleString.ShiningBlade)
                .Description(LocaleString.ShiningBladeDescription)
                .HasPassiveWeaponSkill<ShiningBladeStatusEffect>(SkillType.Longsword);
        }
    }
}
