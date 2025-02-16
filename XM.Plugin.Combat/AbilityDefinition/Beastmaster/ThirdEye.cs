using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using XM.Shared.Core.Party;

namespace XM.Plugin.Combat.AbilityDefinition.Beastmaster
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class ThirdEye: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();

        private readonly StatusEffectService _status;

        public ThirdEye(
            PartyService party,
            StatusEffectService status)
            : base(party, status)
        {
            _status = status;
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ThirdEyeAbility();

            return _builder.Build();
        }

        private void ThirdEyeAbility()
        {
            _builder.Create(FeatType.ThirdEye)
                .Name(LocaleString.ThirdEye)
                .Description(LocaleString.ThirdEyeDescription)
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
