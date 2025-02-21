using System.Collections.Generic;
using Anvil.API;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using XM.Shared.Core.Party;

namespace XM.Plugin.Combat.AbilityDefinition.Hunter
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class ChokingShot: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();
        private readonly SpellService _spell;
        private readonly StatusEffectService _status;

        public ChokingShot(
            PartyService party,
            StatusEffectService status,
            SpellService spell)
            : base(party, status)
        {
            _spell = spell;
            _status = status;
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ChokingShot1();
            ChokingShot2();

            return _builder.Build();
        }

        private void Impact(uint activator, List<uint> targets, Location targetLocation, int dmg)
        {
            foreach (var target in targets)
            {
                var duration = _spell.CalculateResistedTicks(target, ResistType.Darkness, 20);
                _status.ApplyStatusEffect<GraspingDeathStatusEffect>(activator, target, duration);
            }
        }

        private void ChokingShot1()
        {
            _builder.Create(FeatType.ChokingShot1)
                .Name(LocaleString.ChokingShotI)
                .Description(LocaleString.ChokingShotIDescription)
                .HasRecastDelay(RecastGroup.ChokingShot, 24f)
                .HasActivationDelay(2f)
                .DisplaysVisualEffectWhenActivating()
                .IsCastedAbility()
                .RequirementEP(12)
                .ResonanceCost(1)
                .HasMaxRange(10f)
                .IsHostile()
                .ResistType(ResistType.Darkness)
                .TelegraphSize(3f, 3f)
                .HasTelegraphSphereAction((activator, targets, targetLocation) =>
                {
                    Impact(activator, targets, targetLocation, 18);
                });
        }

        private void ChokingShot2()
        {
            _builder.Create(FeatType.ChokingShot2)
                .Name(LocaleString.ChokingShotII)
                .Description(LocaleString.ChokingShotIIDescription)
                .HasRecastDelay(RecastGroup.ChokingShot, 24f)
                .HasActivationDelay(2f)
                .DisplaysVisualEffectWhenActivating()
                .IsCastedAbility()
                .RequirementEP(38)
                .ResonanceCost(2)
                .HasMaxRange(10f)
                .IsHostile()
                .ResistType(ResistType.Darkness)
                .TelegraphSize(3f, 3f)
                .HasTelegraphSphereAction((activator, targets, targetLocation) =>
                {
                    Impact(activator, targets, targetLocation, 30);
                });
        }
    }
}
