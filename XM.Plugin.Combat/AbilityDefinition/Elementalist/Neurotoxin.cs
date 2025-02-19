using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Elementalist
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Neurotoxin: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly StatusEffectService _status;
        private readonly SpellService _spell;

        public Neurotoxin(
            StatusEffectService status,
            SpellService spell)
        {
            _status = status;
            _spell = spell;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            NeurotoxinAbility();

            return _builder.Build();
        }

        private void NeurotoxinAbility()
        {
            _builder.Create(FeatType.Neurotoxin)
                .Name(LocaleString.Neurotoxin)
                .Description(LocaleString.NeurotoxinDescription)
                .HasRecastDelay(RecastGroup.Neurotoxin, 5f)
                .HasActivationDelay(1f)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .RequirementEP(10)
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Wind, 40);
                    _status.ApplyStatusEffect<PoisonStatusEffect>(activator, target, duration);
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpDiseaseSmall), target);
                });
        }
    }
}
