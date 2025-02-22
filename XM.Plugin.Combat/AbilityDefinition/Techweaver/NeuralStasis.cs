using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Debuff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Techweaver
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class NeuralStasis: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly StatusEffectService _status;
        private readonly SpellService _spell;

        public NeuralStasis(
            StatusEffectService status,
            SpellService spell)
        {
            _status = status;
            _spell = spell;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            NeuralStasisAbility();

            return _builder.Build();
        }

        private void NeuralStasisAbility()
        {
            _builder.Create(FeatType.NeuralStasis)
                .Name(LocaleString.NeuralStasis)
                .Description(LocaleString.NeuralStasisDescription)
                .HasRecastDelay(RecastGroup.NeuralStasis, 4f)
                .HasActivationDelay(2f)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .RequirementEP(22)
                .ResonanceCost(2)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Mind, 60);
                    _status.ApplyStatusEffect<NeuralStasisStatusEffect>(activator, target, duration);
                });
        }
    }
}
