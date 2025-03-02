using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Debuff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Mender
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Antidote : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            AntidoteAbility();

            return _builder.Build();
        }

        private readonly StatusEffectService _status;
        public Antidote(StatusEffectService status)
        {
            _status = status;
        }

        private void AntidoteAbility()
        {
            _builder.Create(FeatType.Antidote)
                .Name(LocaleString.Antidote)
                .Description(LocaleString.AntidoteDescription)
                .Classification(AbilityCategoryType.Special)
                .HasRecastDelay(RecastGroup.Antidote, 4f)
                .HasActivationDelay(2f)
                .RequirementEP(6)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(1)
                .HasCustomValidation((activator, target, location) =>
                {
                    if (!_status.HasEffect<PoisonStatusEffect>(target))
                    {
                        return LocaleString.YourTargetIsNotPoisoned.ToLocalizedString();
                    }

                    return string.Empty;
                })
                .HasImpactAction((activator, target, location) =>
                {
                    _status.RemoveStatusEffect<PoisonStatusEffect>(target);
                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpHolyAid), target);
                });
        }
    }
}