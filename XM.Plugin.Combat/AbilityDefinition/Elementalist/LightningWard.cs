using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using XM.Shared.Core.Party;

namespace XM.Plugin.Combat.AbilityDefinition.Elementalist
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class LightningWard: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();

        public LightningWard(
            PartyService party,
            StatusEffectService status)
            : base(party, status)
        {
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            LightningWardAbility();

            return _builder.Build();
        }

        private void LightningWardAbility()
        {
            _builder.Create(FeatType.LightningWard)
                .Name(LocaleString.LightningWard)
                .Description(LocaleString.LightningWardDescription)
                .HasRecastDelay(RecastGroup.Ward, 10f)
                .HasActivationDelay(4f)
                .RequirementEP(30)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    ApplyPartyStatusAOE<LightningWardStatusEffect>(activator, activator, 15f, 15);
                });
        }
    }
}
