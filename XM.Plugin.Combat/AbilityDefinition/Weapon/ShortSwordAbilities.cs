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

        }

        private void BurningEdge()
        {

        }

        private void SoulBlade()
        {

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
