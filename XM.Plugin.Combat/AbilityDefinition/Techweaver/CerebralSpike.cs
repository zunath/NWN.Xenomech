using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using DamageType = XM.Shared.API.Constants.DamageType;

namespace XM.Plugin.Combat.AbilityDefinition.Techweaver
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class CerebralSpike: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly SpellService _spell;

        public CerebralSpike(
            SpellService spell)
        {
            _spell = spell;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            CerebralSpike1();
            CerebralSpike2();

            return _builder.Build();
        }

        private void Impact(uint activator, uint target, int dmg)
        {
            var damage = _spell.CalculateSpellDamage(activator, target, dmg, ResistType.Mind);
            damage = _spell.CalculateElementalSealBonus(activator, damage);

            AssignCommand(activator, () =>
            {
                ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.BeamMind), target);
            });
            AssignCommand(activator, () =>
            {
                ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Mind), target);
            });

        }

        private void CerebralSpike1()
        {
            _builder.Create(FeatType.CerebralSpike1)
                .Name(LocaleString.CerebralSpikeI)
                .Description(LocaleString.CerebralSpikeIDescription)
                .HasRecastDelay(RecastGroup.CerebralSpike, 4f)
                .HasActivationDelay(2f)
                .DisplaysVisualEffectWhenActivating()
                .UsesAnimation(AnimationType.LoopingConjure1)
                .IsCastedAbility()
                .RequirementEP(22)
                .ResonanceCost(1)
                .HasMaxRange(10f)
                .IsHostile()
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, target, 30);
                });
        }

        private void CerebralSpike2()
        {
            _builder.Create(FeatType.CerebralSpike2)
                .Name(LocaleString.CerebralSpikeII)
                .Description(LocaleString.CerebralSpikeIIDescription)
                .HasRecastDelay(RecastGroup.CerebralSpike, 4f)
                .HasActivationDelay(2f)
                .DisplaysVisualEffectWhenActivating()
                .UsesAnimation(AnimationType.LoopingConjure1)
                .IsCastedAbility()
                .RequirementEP(66)
                .ResonanceCost(2)
                .HasMaxRange(10f)
                .IsHostile()
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, target, 60);
                });
        }
    }
}
