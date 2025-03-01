using System;
using System.Collections.Generic;
using XM.Progression.Ability;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;

namespace XM.Plugin.Combat.AbilityDefinition.Weapon
{
    internal abstract class WeaponSkillBaseAbility: IAbilityListDefinition
    {
        private readonly Lazy<CombatService> _combat;
        private readonly Lazy<StatusEffectService> _status;

        protected WeaponSkillBaseAbility(
            Lazy<CombatService> combat,
            Lazy<StatusEffectService> status)
        {
            _combat = combat;
            _status = status;
        }

        protected void DamageImpact(
            uint activator, 
            List<uint> targets,
            int bonusDMG,
            ResistType resistType,
            DamageType damageType,
            VisualEffectType vfx)
        {
            foreach (var target in targets)
            {
                if (!GetIsReactionTypeHostile(target, activator))
                    continue;

                var damage = _combat.Value.CalculateWeaponSkillDamage(
                    activator,
                    target,
                    bonusDMG, 
                    resistType);

                AssignCommand(activator, () =>
                {
                    ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, damageType), target);
                });

                if (vfx != VisualEffectType.None)
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(vfx), target);
            }
        }

        protected void DamageEffectImpact<T>(
            uint activator,
            List<uint> targets,
            int bonusDMG,
            ResistType resistType,
            DamageType damageType,
            VisualEffectType vfx,
            int durationTicks)
            where T: IStatusEffect
        {
            foreach (var target in targets)
            {
                if (!GetIsReactionTypeHostile(target, activator))
                    continue;

                var damage = _combat.Value.CalculateWeaponSkillDamage(
                    activator,
                    target,
                    bonusDMG,
                    resistType);

                AssignCommand(activator, () =>
                {
                    ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, damageType), target);
                });

                if (vfx != VisualEffectType.None)
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(vfx), target);

                _status.Value.ApplyStatusEffect<T>(activator, target, durationTicks);
            }
        }

        protected void DamageEffectImpact<T>(
            uint activator, 
            uint target,
            int bonusDMG,
            ResistType resistType,
            DamageType damageType,
            VisualEffectType vfx,
            int durationTicks)
            where T : IStatusEffect
        {
            var damage = _combat.Value.CalculateWeaponSkillDamage(
                activator,
                target,
                bonusDMG,
                resistType);

            AssignCommand(activator, () =>
            {
                ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, damageType), target);
            });

            if(vfx != VisualEffectType.None)
                ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(vfx), target);

            _status.Value.ApplyStatusEffect<T>(activator, target, durationTicks);
        }

        public abstract Dictionary<FeatType, AbilityDetail> BuildAbilities();
    }
}
