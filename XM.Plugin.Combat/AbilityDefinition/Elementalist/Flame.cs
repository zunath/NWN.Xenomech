using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using DamageType = XM.Shared.API.Constants.DamageType;

namespace XM.Plugin.Combat.AbilityDefinition.Elementalist
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Flame: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly SpellService _spell;

        public Flame(
            SpellService spell)
        {
            _spell = spell;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Flame1();
            Flame2();
            Flame3();

            return _builder.Build();
        }

        private void Impact(uint activator, uint target, int dmg)
        {
            var damage = _spell.CalculateSpellDamage(activator, target, dmg, ResistType.Fire);
            damage = _spell.CalculateElementalSealBonus(activator, damage);
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpFlameSmall), target);
            AssignCommand(activator, () =>
            {
                ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Fire), target);
            });
        }

        private void Flame1()
        {
            _builder.Create(FeatType.Flame1)
                .Name(LocaleString.FlameI)
                .Description(LocaleString.FlameIDescription)
                .Classification(AbilityCategoryType.Offensive)
                .HasRecastDelay(RecastGroup.Flame, 2f)
                .HasActivationDelay(1f)
                .DisplaysVisualEffectWhenActivating()
                .UsesAnimation(AnimationType.LoopingConjure1)
                .IsCastedAbility()
                .RequirementEP(6)
                .ResonanceCost(1)
                .HasMaxRange(10f)
                .IsHostile()
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, target, 10);
                });
        }

        private void Flame2()
        {
            _builder.Create(FeatType.Flame2)
                .Name(LocaleString.FlameII)
                .Description(LocaleString.FlameIIDescription)
                .Classification(AbilityCategoryType.Offensive)
                .HasRecastDelay(RecastGroup.Flame, 3f)
                .HasActivationDelay(2f)
                .DisplaysVisualEffectWhenActivating()
                .UsesAnimation(AnimationType.LoopingConjure1)
                .IsCastedAbility()
                .RequirementEP(22)
                .ResonanceCost(2)
                .HasMaxRange(10f)
                .IsHostile()
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, target, 60);
                });
        }

        private void Flame3()
        {
            _builder.Create(FeatType.Flame3)
                .Name(LocaleString.FlameIII)
                .Description(LocaleString.FlameIIIDescription)
                .Classification(AbilityCategoryType.Offensive)
                .HasRecastDelay(RecastGroup.Flame, 4f)
                .HasActivationDelay(3f)
                .DisplaysVisualEffectWhenActivating()
                .UsesAnimation(AnimationType.LoopingConjure1)
                .IsCastedAbility()
                .RequirementEP(65)
                .ResonanceCost(3)
                .HasMaxRange(10f)
                .IsHostile()
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, target, 120);
                });
        }
    }
}
