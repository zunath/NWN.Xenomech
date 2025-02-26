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
    internal class PistolAbilities : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            QuickDraw();
            BurningShot();
            Ricochet();
            PiercingShot();
            ShadowBarrage();
            Deadeye();
            TrueShot();

            return _builder.Build();
        }

        private void QuickDraw()
        {

        }

        private void BurningShot()
        {

        }

        private void Ricochet()
        {

        }

        private void PiercingShot()
        {

        }

        private void ShadowBarrage()
        {

        }

        private void Deadeye()
        {

        }

        private void TrueShot()
        {
            _builder.Create(FeatType.TrueShot)
                .Name(LocaleString.TrueShot)
                .Description(LocaleString.TrueShotDescription)
                .HasPassiveWeaponSkill<TrueShotStatusEffect>(SkillType.Pistol);
        }
    }
}
