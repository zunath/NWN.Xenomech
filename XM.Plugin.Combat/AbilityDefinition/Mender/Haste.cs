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
    internal class Haste : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            HasteAbility();

            return _builder.Build();
        }

        private readonly StatusEffectService _status;
        public Haste(StatusEffectService status)
        {
            _status = status;
        }

        private void HasteAbility()
        {
            _builder.Create(FeatType.Haste)
                .Name(LocaleString.Haste)
                .Description(LocaleString.HasteDescription)
                .HasRecastDelay(RecastGroup.Haste, 20f)
                .HasActivationDelay(6f)
                .RequirementEP(40)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(3)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.RemoveStatusEffect<SlowStatusEffect>(target);
                    _status.ApplyStatusEffect<HasteStatusEffect>(activator, target, 1);
                });
        }
    }
}