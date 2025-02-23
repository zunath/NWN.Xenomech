using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Nightstalker
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Hide : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();
        private readonly StatusEffectService _status;
        public Hide(StatusEffectService status)
        {
            _status = status;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            HideAbility();

            return _builder.Build();
        }

        private void HideAbility()
        {
            _builder.Create(FeatType.Hide)
                .Name(LocaleString.Hide)
                .Description(LocaleString.HideDescription)
                .HasRecastDelay(RecastGroup.Hide, 60f * 3f)
                .HasActivationDelay(2f)
                .RequirementEP(52)
                .ResonanceCost(2)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<HideStatusEffect>(activator, activator, 1);
                });
        }
    }
}