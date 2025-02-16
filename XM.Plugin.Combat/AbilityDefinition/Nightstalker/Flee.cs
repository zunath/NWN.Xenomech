using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Nightstalker
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Flee : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly StatusEffectService _status;
        public Flee(StatusEffectService status)
        {
            _status = status;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            FleeAbility();

            return _builder.Build();
        }

        private void FleeAbility()
        {
            _builder.Create(FeatType.Flee)
                .Name(LocaleString.Flee)
                .Description(LocaleString.FleeDescription)
                .HasRecastDelay(RecastGroup.Flee, 60f * 5f)
                .HasActivationDelay(2f)
                .RequirementEP(12)
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<FleeStatusEffect>(activator, activator, 1);
                });
        }
    }
}