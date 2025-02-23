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
    internal class ShockwaveSurge: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly SpellService _spell;

        public ShockwaveSurge(
            SpellService spell)
        {
            _spell = spell;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ShockwaveSurge1();
            ShockwaveSurge2();

            return _builder.Build();
        }

        private void Impact(uint activator, List<uint> targets, int dmg)
        {
            foreach (var target in targets)
            {
                if (!GetIsReactionTypeHostile(target, activator))
                    continue;

                var damage = _spell.CalculateSpellDamage(activator, target, dmg, ResistType.Lightning);
                damage = _spell.CalculateElementalSealBonus(activator, damage);

                AssignCommand(activator, () => ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpLightningMedium), target));
                AssignCommand(activator, () =>
                {
                    ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Lightning), target);
                });
            }
        }

        private void ShockwaveSurge1()
        {
            _builder.Create(FeatType.ShockwaveSurge1)
                .Name(LocaleString.ShockwaveSurgeI)
                .Description(LocaleString.ShockwaveSurgeIDescription)
                .HasRecastDelay(RecastGroup.ShockwaveSurge, 16f)
                .HasActivationDelay(4f)
                .DisplaysVisualEffectWhenActivating()
                .UsesAnimation(AnimationType.LoopingConjure1)
                .IsCastedAbility()
                .RequirementEP(21)
                .ResonanceCost(1)
                .HasMaxRange(12f)
                .IsHostile()
                .ResistType(ResistType.Lightning)
                .TelegraphSize(5f, 5f)
                .HasTelegraphSphereAction((activator, targets, targetLocation) =>
                {
                    Impact(activator, targets, 24);
                });
        }

        private void ShockwaveSurge2()
        {
            _builder.Create(FeatType.ShockwaveSurge2)
                .Name(LocaleString.ShockwaveSurgeII)
                .Description(LocaleString.ShockwaveSurgeIIDescription)
                .HasRecastDelay(RecastGroup.ShockwaveSurge, 16f)
                .HasActivationDelay(4f)
                .DisplaysVisualEffectWhenActivating()
                .UsesAnimation(AnimationType.LoopingConjure1)
                .IsCastedAbility()
                .RequirementEP(48)
                .ResonanceCost(2)
                .HasMaxRange(12f)
                .IsHostile()
                .ResistType(ResistType.Lightning)
                .TelegraphSize(5f, 5f)
                .HasTelegraphSphereAction((activator, targets, targetLocation) =>
                {
                    Impact(activator, targets, 82);
                });
        }
    }
}
