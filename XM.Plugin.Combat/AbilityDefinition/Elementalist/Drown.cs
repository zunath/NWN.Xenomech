using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Debuff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using DamageType = XM.Shared.API.Constants.DamageType;

namespace XM.Plugin.Combat.AbilityDefinition.Elementalist
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Drown: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly SpellService _spell;
        private readonly StatusEffectService _status;

        public Drown(
            StatusEffectService status,
            SpellService spell)
        {
            _status = status;
            _spell = spell;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Drown1();
            Drown2();

            return _builder.Build();
        }

        private void Impact<T>(uint activator, uint target, int dmg)
            where T: IStatusEffect
        {
            var damage = _spell.CalculateSpellDamage(activator, target, dmg, ResistType.Water);
            damage = _spell.CalculateElementalSealBonus(activator, damage);
            var duration = _spell.CalculateResistedTicks(target, ResistType.Water, 120);
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpPulseWater), target);
            AssignCommand(activator, () =>
            {
                ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Water), target);
            });

            _status.ApplyStatusEffect<T>(activator, target, duration);
        }

        private void Drown1()
        {
            _builder.Create(FeatType.Drown1)
                .Name(LocaleString.DrownI)
                .Description(LocaleString.DrownIDescription)
                .HasRecastDelay(RecastGroup.Drown, 10f)
                .HasActivationDelay(3f)
                .DisplaysVisualEffectWhenActivating()
                .UsesAnimation(AnimationType.LoopingConjure1)
                .IsCastedAbility()
                .RequirementEP(16)
                .ResonanceCost(1)
                .HasMaxRange(10f)
                .IsHostile()
                .HasImpactAction((activator, target, location) =>
                {
                    Impact<Drown1StatusEffect>(activator, target, 16);
                });
        }

        private void Drown2()
        {
            _builder.Create(FeatType.Drown2)
                .Name(LocaleString.DrownII)
                .Description(LocaleString.DrownIIDescription)
                .HasRecastDelay(RecastGroup.Drown, 30f)
                .HasActivationDelay(2f)
                .DisplaysVisualEffectWhenActivating()
                .UsesAnimation(AnimationType.LoopingConjure1)
                .IsCastedAbility()
                .RequirementEP(84)
                .ResonanceCost(2)
                .HasMaxRange(10f)
                .IsHostile()
                .HasImpactAction((activator, target, location) =>
                {
                    Impact<Drown2StatusEffect>(activator, target, 50);
                });
        }
    }
}
