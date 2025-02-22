using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Debuff;
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
    internal class CryoVenom: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();
        private readonly SpellService _spell;
        private readonly StatusEffectService _status;

        public CryoVenom(
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
            CryoVenom1();
            CryoVenom2();

            return _builder.Build();
        }

        private void CryoVenom1()
        {
            _builder.Create(FeatType.CryoVenom1)
                .Name(LocaleString.CryoVenomI)
                .Description(LocaleString.CryoVenomIDescription)
                .HasRecastDelay(RecastGroup.CryoVenom, 60f)
                .IsQueuedAttack()
                .RequirementEP(12)
                .ResonanceCost(1)
                .ResistType(ResistType.Ice)
                .IncreasesStat(StatType.QueuedDMGBonus, 12)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Ice, 20);
                    _status.ApplyStatusEffect<PoisonStatusEffect>(activator, target, duration);
                    AssignCommand(activator, () => ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpDiseaseSmall), target));
                });
        }

        private void CryoVenom2()
        {
            _builder.Create(FeatType.CryoVenom2)
                .Name(LocaleString.CryoVenomII)
                .Description(LocaleString.CryoVenomIIDescription)
                .HasRecastDelay(RecastGroup.CryoVenom, 60f)
                .IsQueuedAttack()
                .RequirementEP(45)
                .ResonanceCost(2)
                .ResistType(ResistType.Ice)
                .IncreasesStat(StatType.QueuedDMGBonus, 36)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Ice, 20);
                    _status.ApplyStatusEffect<PoisonStatusEffect>(activator, target, duration);
                    AssignCommand(activator, () => ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpDiseaseSmall), target));
                });
        }
    }
}
