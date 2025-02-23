using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Techweaver
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class NullLance : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly SpellService _spell;

        public NullLance(
            SpellService spell)
        {
            _spell = spell;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            NullLance1();
            NullLance2();
            NullLance3();

            return _builder.Build();
        }

        private void Impact(uint activator, uint target)
        {
            var ticks = _spell.CalculateResistedTicks(target, ResistType.Mind, 16);
            if (ticks > 0)
            {
                var duration = ticks * 0.25f;
                AssignCommand(activator, () => ApplyEffectToObject(DurationType.Temporary, EffectStunned(), target, duration)); 
                AssignCommand(activator, () => ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpReduceAbilityScore), target));
            }
        }

        private void NullLance1()
        {
            _builder.Create(FeatType.NullLance1)
                .Name(LocaleString.NullLanceI)
                .Description(LocaleString.NullLanceIDescription)
                .HasRecastDelay(RecastGroup.NullLance, 18f)
                .IsQueuedAttack()
                .RequirementEP(8)
                .ResonanceCost(1)
                .ResistType(ResistType.Mind)
                .IncreasesStat(StatType.QueuedDMGBonus, 12)
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, target);
                });
        }

        private void NullLance2()
        {
            _builder.Create(FeatType.NullLance2)
                .Name(LocaleString.NullLanceII)
                .Description(LocaleString.NullLanceIIDescription)
                .HasRecastDelay(RecastGroup.NullLance, 18f)
                .IsQueuedAttack()
                .RequirementEP(25)
                .ResonanceCost(2)
                .ResistType(ResistType.Mind)
                .IncreasesStat(StatType.QueuedDMGBonus, 22)
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, target);
                });
        }

        private void NullLance3()
        {
            _builder.Create(FeatType.NullLance3)
                .Name(LocaleString.NullLanceIII)
                .Description(LocaleString.NullLanceIIIDescription)
                .HasRecastDelay(RecastGroup.NullLance, 18f)
                .IsQueuedAttack()
                .RequirementEP(52)
                .ResonanceCost(3)
                .ResistType(ResistType.Mind)
                .IncreasesStat(StatType.QueuedDMGBonus, 52)
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, target);
                });
        }
    }
}
