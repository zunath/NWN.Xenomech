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
    internal class NeuralCascade: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly StatusEffectService _status;
        private readonly SpellService _spell;

        public NeuralCascade(
            StatusEffectService status,
            SpellService spell)
        {
            _status = status;
            _spell = spell;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            NeuralCascade1();
            NeuralCascade2();

            return _builder.Build();
        }

        private void NeuralCascade1()
        {
            _builder.Create(FeatType.NeuralCascade1)
                .Name(LocaleString.NeuralCascadeI)
                .Description(LocaleString.NeuralCascadeIDescription)
                .Classification(AbilityCategoryType.Special)
                .HasRecastDelay(RecastGroup.NeuralCascade, 4f)
                .HasActivationDelay(2f)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .RequirementEP(6)
                .ResonanceCost(2)
                .HasImpactAction((activator, target, location) =>
                {
                    var duration = _spell.CalculateResistedTicks(target, ResistType.Mind, 40);
                    _status.ApplyStatusEffect<PacifiedStatusEffect>(activator, target, duration);
                });
        }

        private void NeuralCascade2()
        {
            _builder.Create(FeatType.NeuralCascade2)
                .Name(LocaleString.NeuralCascadeII)
                .Description(LocaleString.NeuralCascadeIIDescription)
                .Classification(AbilityCategoryType.Special)
                .HasRecastDelay(RecastGroup.NeuralCascade, 16f)
                .HasActivationDelay(2f)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .RequirementEP(72)
                .ResonanceCost(3)
                .TelegraphSize(5f, 5f)
                .HasTelegraphSphereAction((activator, targets, location) =>
                {
                    ApplyEffectAtLocation(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpPdkGenericPulse), location);
                    foreach (var target in targets)
                    {
                        if (!GetIsReactionTypeHostile(target, activator))
                            continue;

                        var duration = _spell.CalculateResistedTicks(target, ResistType.Mind, 40);
                        _status.ApplyStatusEffect<PacifiedStatusEffect>(activator, target, duration);
                    }
                });
        }
    }
}
