using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Beastmaster
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class ThirdEye: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly StatusEffectService _status;

        public ThirdEye(StatusEffectService status)
        {
            _status = status;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ThirdEyeAbility();

            return _builder.Build();
        }

        private void ThirdEyeAbility()
        {
            _builder.Create(FeatType.ThirdEye)
                .Name(LocaleString.ThirdEye)
                .Description(LocaleString.ThirdEyeDescription)
                .Classification(AbilityCategoryType.Defensive)
                .HasRecastDelay(RecastGroup.ThirdEye, 15f)
                .HasActivationDelay(1f)
                .RequirementEP(6)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    _status.ApplyStatusEffect<ThirdEyeStatusEffect>(activator, activator, 1);
                });
        }
    }
}
