using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.WeaponSkill;
using XM.Progression.Ability;
using XM.Progression.Skill;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Weapon
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class PistolAbilities : WeaponSkillBaseAbility
    {
        private readonly AbilityBuilder _builder = new();

        public PistolAbilities(
            Lazy<CombatService> combat,
            Lazy<StatusEffectService> status)
            : base(combat, status)
        {
        }
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
            _builder.Create(FeatType.QuickDraw)
                .Name(LocaleString.QuickDraw)
                .Description(LocaleString.QuickDrawDescription)
                .IsWeaponSkill(SkillType.Pistol, 160)
                .RequirementTP(500)
                .ResistType(ResistType.Lightning)
                .IncreasesStat(StatType.QueuedDMGBonus, 8);
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
