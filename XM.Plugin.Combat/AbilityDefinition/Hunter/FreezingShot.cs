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
    internal class FreezingShot: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();
        private readonly SpellService _spell;
        private readonly StatusEffectService _status;

        public FreezingShot(
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
            FreezingShot1();
            FreezingShot2();
            FreezingShot3();

            return _builder.Build();
        }

        private void FreezingShot1()
        {
            _builder.Create(FeatType.FreezingShot1)
                .Name(LocaleString.FreezingShotI)
                .Description(LocaleString.FreezingShotIDescription)
                .HasRecastDelay(RecastGroup.FreezingShot, 60f * 3f)
                .IsQueuedAttack()
                .RequirementEP(16)
                .ResonanceCost(1)
                .ResistType(ResistType.Ice)
                .IncreasesStat(StatType.QueuedDMGBonus, 18)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Ice, 40);
                    _status.ApplyStatusEffect<SlowStatusEffect>(activator, target, duration);
                    AssignCommand(activator, () => ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpFrostSmall), target));
                });
        }

        private void FreezingShot2()
        {
            _builder.Create(FeatType.FreezingShot2)
                .Name(LocaleString.FreezingShotII)
                .Description(LocaleString.FreezingShotIIDescription)
                .HasRecastDelay(RecastGroup.FreezingShot, 60f * 3f)
                .IsQueuedAttack()
                .RequirementEP(38)
                .ResonanceCost(2)
                .ResistType(ResistType.Ice)
                .IncreasesStat(StatType.QueuedDMGBonus, 26)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Ice, 40);
                    _status.ApplyStatusEffect<SlowStatusEffect>(activator, target, duration);
                    AssignCommand(activator, () => ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpFrostSmall), target));
                });
        }

        private void FreezingShot3()
        {
            _builder.Create(FeatType.FreezingShot3)
                .Name(LocaleString.FreezingShotIII)
                .Description(LocaleString.FreezingShotIIIDescription)
                .HasRecastDelay(RecastGroup.FreezingShot, 60f * 3f)
                .IsQueuedAttack()
                .RequirementEP(73)
                .ResonanceCost(3)
                .ResistType(ResistType.Ice)
                .IncreasesStat(StatType.QueuedDMGBonus, 76)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Ice, 40);
                    _status.ApplyStatusEffect<SlowStatusEffect>(activator, target, duration);
                    AssignCommand(activator, () => ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpFrostSmall), target));
                });
        }
    }
}
