using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Techweaver
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class AurionInfusion: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly StatusEffectService _status;

        public AurionInfusion(StatusEffectService status)
        {
            _status = status;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            AurionInfusion1();
            AurionInfusion2();

            return _builder.Build();
        }

        private void AurionInfusion1()
        {
            _builder.Create(FeatType.AurionInfusion1)
                .Name(LocaleString.AurionInfusionI)
                .Description(LocaleString.AurionInfusionIDescription)
                .HasRecastDelay(RecastGroup.AurionInfusion, 12f)
                .HasActivationDelay(4f)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .RequirementEP(32)
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<AurionInfusion1StatusEffect>(activator, target, 1);
                });
        }

        private void AurionInfusion2()
        {
            _builder.Create(FeatType.AurionInfusion2)
                .Name(LocaleString.AurionInfusionII)
                .Description(LocaleString.AurionInfusionIIDescription)
                .HasRecastDelay(RecastGroup.AurionInfusion, 12f)
                .HasActivationDelay(4f)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .RequirementEP(98)
                .ResonanceCost(2)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<AurionInfusion2StatusEffect>(activator, target, 1);
                });
        }
    }
}
