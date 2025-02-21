using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Mender
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Erase : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            EraseAbility();

            return _builder.Build();
        }

        private static readonly List<Type> _effectsToRemoveInOrder =
        [
            typeof(SlowStatusEffect),
            typeof(PoisonStatusEffect),
            typeof(GraspingDeathStatusEffect)
        ];

        private readonly StatusEffectService _status;
        public Erase(StatusEffectService status)
        {
            _status = status;
        }

        private void EraseAbility()
        {
            _builder.Create(FeatType.Erase)
                .Name(LocaleString.Erase)
                .Description(LocaleString.EraseDescription)
                .HasRecastDelay(RecastGroup.Erase, 15f)
                .HasActivationDelay(3f)
                .RequirementEP(18)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(1)
                .HasCustomValidation((activator, target, location) =>
                {
                    foreach (var effect in _effectsToRemoveInOrder)
                    {
                        if (_status.HasEffect(effect, target))
                            return string.Empty;
                    }

                    return LocaleString.YourTargetIsNotAfflictedByAnyDetrimentalEffects.ToLocalizedString();
                })
                .HasImpactAction((activator, target, location) =>
                {
                    foreach (var effect in _effectsToRemoveInOrder)
                    {
                        if (_status.HasEffect(effect, target))
                        {
                            _status.RemoveStatusEffect(effect, target);
                            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpRemoveCondition), target);
                            return;
                        }
                    }
                });
        }
    }
}