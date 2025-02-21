using System;
using System.Collections.Generic;
using Anvil.API;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using XM.Shared.Core.Party;
using DamageType = XM.Shared.API.Constants.DamageType;

namespace XM.Plugin.Combat.AbilityDefinition.Hunter
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class IceVolley: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();
        private readonly SpellService _spell;

        public IceVolley(
            PartyService party,
            StatusEffectService status,
            SpellService spell)
            : base(party, status)
        {
            _spell = spell;
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            IceVolley1();
            IceVolley2();
            IceVolley3();

            return _builder.Build();
        }

        private void Impact(uint activator, List<uint> targets, Location targetLocation, int dmg)
        {
            AssignCommand(activator, () => ApplyEffectAtLocation(DurationType.Instant, EffectVisualEffect(VisualEffectType.FnfIcelstorm), targetLocation));
            foreach (var target in targets)
            {
                var damage = _spell.CalculateSpellDamage(activator, target, dmg, ResistType.Ice, AbilityType.Perception, AbilityType.Perception);
                ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Ice), target);
            }
        }

        private void IceVolley1()
        {
            _builder.Create(FeatType.IceVolley1)
                .Name(LocaleString.IceVolleyI)
                .Description(LocaleString.IceVolleyIDescription)
                .HasRecastDelay(RecastGroup.IceVolley, 60f)
                .HasActivationDelay(2f)
                .DisplaysVisualEffectWhenActivating()
                .IsCastedAbility()
                .RequirementEP(22)
                .ResonanceCost(1)
                .HasMaxRange(10f)
                .IsHostile()
                .ResistType(ResistType.Ice)
                .TelegraphSize(4f, 4f)
                .HasTelegraphSphereAction((activator, targets, targetLocation) =>
                {
                    Impact(activator, targets, targetLocation, 18);
                });
        }

        private void IceVolley2()
        {
            _builder.Create(FeatType.IceVolley2)
                .Name(LocaleString.IceVolleyII)
                .Description(LocaleString.IceVolleyIIDescription)
                .HasRecastDelay(RecastGroup.IceVolley, 60f)
                .HasActivationDelay(2f)
                .DisplaysVisualEffectWhenActivating()
                .IsCastedAbility()
                .RequirementEP(44)
                .ResonanceCost(2)
                .HasMaxRange(10f)
                .IsHostile()
                .ResistType(ResistType.Ice)
                .TelegraphSize(4f, 4f)
                .HasTelegraphSphereAction((activator, targets, targetLocation) =>
                {
                    Impact(activator, targets, targetLocation, 30);
                });
        }

        private void IceVolley3()
        {
            _builder.Create(FeatType.IceVolley3)
                .Name(LocaleString.IceVolleyIII)
                .Description(LocaleString.IceVolleyIIIDescription)
                .HasRecastDelay(RecastGroup.IceVolley, 60f)
                .HasActivationDelay(2f)
                .DisplaysVisualEffectWhenActivating()
                .IsCastedAbility()
                .RequirementEP(66)
                .ResonanceCost(3)
                .HasMaxRange(10f)
                .IsHostile()
                .ResistType(ResistType.Ice)
                .TelegraphSize(4f, 4f)
                .HasTelegraphSphereAction((activator, targets, targetLocation) =>
                {
                    Impact(activator, targets, targetLocation, 50);
                });
        }
    }
}
