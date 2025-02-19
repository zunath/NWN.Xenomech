using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using XM.Shared.Core.Party;

namespace XM.Plugin.Combat.AbilityDefinition.Brawler
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class StrikingCobra: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();
        private readonly SpellService _spell;
        private readonly StatusEffectService _status;

        public StrikingCobra(
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
            StrikingCobra1();
            StrikingCobra2();

            return _builder.Build();
        }

        private void StrikingCobra1()
        {
            _builder.Create(FeatType.StrikingCobra1)
                .Name(LocaleString.StrikingCobraI)
                .Description(LocaleString.StrikingCobraIDescription)
                .HasRecastDelay(RecastGroup.StrikingCobra, 30f)
                .IsQueuedAttack()
                .RequirementEP(10)
                .ResonanceCost(1)
                .ResistType(ResistType.Darkness)
                .IncreasesStat(StatType.QueuedDMGBonus, 14)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Darkness, 20);
                    _status.ApplyStatusEffect<PoisonStatusEffect>(activator, target, duration);
                    AssignCommand(activator, () => ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpDiseaseSmall), target));
                });
        }

        private void StrikingCobra2()
        {
            _builder.Create(FeatType.StrikingCobra2)
                .Name(LocaleString.StrikingCobraII)
                .Description(LocaleString.StrikingCobraIIDescription)
                .HasRecastDelay(RecastGroup.StrikingCobra, 30f)
                .IsQueuedAttack()
                .RequirementEP(28)
                .ResonanceCost(2)
                .ResistType(ResistType.Darkness)
                .IncreasesStat(StatType.QueuedDMGBonus, 32)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Darkness, 20);
                    _status.ApplyStatusEffect<PoisonStatusEffect>(activator, target, duration);
                    AssignCommand(activator, () => ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpDiseaseSmall), target));
                });
        }
    }
}
