using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Mender
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Regen : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Regen1();
            Regen2();

            return _builder.Build();
        }

        private readonly StatusEffectService _status;
        public Regen(StatusEffectService status)
        {
            _status = status;
        }

        private void Regen1()
        {
            _builder.Create(FeatType.Regen1)
                .Name(LocaleString.RegenI)
                .Description(LocaleString.RegenIDescription)
                .Classification(AbilityCategoryType.HPRestoration)
                .HasRecastDelay(RecastGroup.Regen, 12f)
                .HasActivationDelay(2f)
                .RequirementEP(25)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<Regen1StatusEffect>(activator, target, 10);
                });
        }

        private void Regen2()
        {
            _builder.Create(FeatType.Regen2)
                .Name(LocaleString.RegenII)
                .Description(LocaleString.RegenIIDescription)
                .Classification(AbilityCategoryType.HPRestoration)
                .HasRecastDelay(RecastGroup.Regen, 18f)
                .HasActivationDelay(2f)
                .RequirementEP(63)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(2)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<Regen2StatusEffect>(activator, target, 10);
                });
        }
    }
}