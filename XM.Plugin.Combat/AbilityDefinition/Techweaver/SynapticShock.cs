using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Techweaver
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class SynapticShock : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly SpellService _spell;
        private readonly EnmityService _enmity;

        public SynapticShock(
            SpellService spell,
            EnmityService enmity)
        {
            _spell = spell;
            _enmity = enmity;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            SynapticShockAbility();

            return _builder.Build();
        }

        private void Impact(uint activator, uint target)
        {
            var ticks = _spell.CalculateResistedTicks(target, ResistType.Mind, 32);
            if (ticks > 0)
            {
                var duration = ticks * 0.25f;
                AssignCommand(activator, () => ApplyEffectToObject(DurationType.Temporary, EffectStunned(), target, duration)); 
                AssignCommand(activator, () => ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpReduceAbilityScore), target));
            }

            _enmity.ModifyEnmity(activator, target, EnmityType.Volatile, 850);
        }

        private void SynapticShockAbility()
        {
            _builder.Create(FeatType.SynapticShock)
                .Name(LocaleString.SynapticShock)
                .Description(LocaleString.SynapticShockDescription)
                .HasRecastDelay(RecastGroup.SynapticShock, 60f)
                .IsCastedAbility()
                .HasActivationDelay(1f)
                .RequirementEP(12)
                .ResonanceCost(1)
                .HasMaxRange(12f)
                .IsHostile()
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, target);
                });
        }
    }
}
