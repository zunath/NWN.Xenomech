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
    internal class ElectricFist: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();

        public ElectricFist(
            PartyService party,
            StatusEffectService status)
            : base(party, status)
        {
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ElectricFist1();
            ElectricFist2();
            ElectricFist3();

            return _builder.Build();
        }

        private void ElectricFist1()
        {
            _builder.Create(FeatType.ElectricFist1)
                .Name(LocaleString.ElectricFistI)
                .Description(LocaleString.ElectricFistIDescription)
                .HasRecastDelay(RecastGroup.ElectricFist, 12f)
                .IsQueuedAttack()
                .RequirementEP(6)
                .ResonanceCost(1)
                .ResistType(ResistType.Lightning)
                .IncreasesStat(StatType.QueuedDMGBonus, 12)
                .HasImpactAction((activator, target, location) =>
                {
                    AssignCommand(activator, () => ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpLightningSmall), target));
                });
        }

        private void ElectricFist2()
        {
            _builder.Create(FeatType.ElectricFist2)
                .Name(LocaleString.ElectricFistII)
                .Description(LocaleString.ElectricFistIIDescription)
                .HasRecastDelay(RecastGroup.ElectricFist, 12f)
                .IsQueuedAttack()
                .RequirementEP(15)
                .ResonanceCost(2)
                .ResistType(ResistType.Lightning)
                .IncreasesStat(StatType.QueuedDMGBonus, 22)
                .HasImpactAction((activator, target, location) =>
                {
                    AssignCommand(activator, () => ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpLightningSmall), target));
                });
        }

        private void ElectricFist3()
        {
            _builder.Create(FeatType.ElectricFist3)
                .Name(LocaleString.ElectricFistIII)
                .Description(LocaleString.ElectricFistIIIDescription)
                .HasRecastDelay(RecastGroup.ElectricFist, 12f)
                .IsQueuedAttack()
                .RequirementEP(35)
                .ResonanceCost(3)
                .ResistType(ResistType.Lightning)
                .IncreasesStat(StatType.QueuedDMGBonus, 52)
                .HasImpactAction((activator, target, location) =>
                {
                    AssignCommand(activator, () => ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpLightningSmall), target));
                });
        }
    }
}
