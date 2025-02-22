using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using XM.Shared.Core.Party;

namespace XM.Plugin.Combat.AbilityDefinition.Beastmaster
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class CrescentMoon : AbilityBase
    {
        private readonly AbilityBuilder _builder = new();
        private readonly SpellService _spell;

        public CrescentMoon(
            PartyService party,
            StatusEffectService status,
            SpellService spell)
            : base(party, status)
        {
            _spell = spell;
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            CrescentMoon1();
            CrescentMoon2();
            CrescentMoon3();

            return _builder.Build();
        }

        private void Impact(uint activator, uint target)
        {
            var ticks = _spell.CalculateResistedTicks(target, ResistType.Wind, 16);
            if (ticks > 0)
            {
                var duration = ticks * 0.25f;
                AssignCommand(activator, () => ApplyEffectToObject(DurationType.Temporary, EffectStunned(), target, duration)); 
                AssignCommand(activator, () => ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpReduceAbilityScore), target));
            }
        }

        private void CrescentMoon1()
        {
            _builder.Create(FeatType.CrescentMoon1)
                .Name(LocaleString.CrescentMoonI)
                .Description(LocaleString.CrescentMoonIDescription)
                .HasRecastDelay(RecastGroup.CrescentMoon, 30f)
                .IsQueuedAttack()
                .RequirementEP(12)
                .ResonanceCost(1)
                .ResistType(ResistType.Wind)
                .IncreasesStat(StatType.QueuedDMGBonus, 18)
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, target);
                });
        }

        private void CrescentMoon2()
        {
            _builder.Create(FeatType.CrescentMoon2)
                .Name(LocaleString.CrescentMoonII)
                .Description(LocaleString.CrescentMoonIIDescription)
                .HasRecastDelay(RecastGroup.CrescentMoon, 30f)
                .IsQueuedAttack()
                .RequirementEP(36)
                .ResonanceCost(2)
                .ResistType(ResistType.Wind)
                .IncreasesStat(StatType.QueuedDMGBonus, 32)
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, target);
                });
        }

        private void CrescentMoon3()
        {
            _builder.Create(FeatType.CrescentMoon3)
                .Name(LocaleString.CrescentMoonIII)
                .Description(LocaleString.CrescentMoonIIIDescription)
                .HasRecastDelay(RecastGroup.CrescentMoon, 30f)
                .IsQueuedAttack()
                .RequirementEP(54)
                .ResonanceCost(3)
                .ResistType(ResistType.Wind)
                .IncreasesStat(StatType.QueuedDMGBonus, 50)
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, target);
                });
        }
    }
}
