using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Progression.StatusEffect.StatusEffectDefinition;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using XM.Shared.Core.Party;

namespace XM.Plugin.Combat.AbilityDefinition.Nightstalker
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class LightWard: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();

        public LightWard(
            PartyService party,
            StatusEffectService status)
            : base(party, status)
        {
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            LightWardAbility();

            return _builder.Build();
        }

        private void LightWardAbility()
        {
            _builder.Create(FeatType.LightWard)
                .Name(LocaleString.LightWard)
                .Description(LocaleString.LightWardDescription)
                .HasRecastDelay(RecastGroup.Ward, 10f)
                .HasActivationDelay(4f)
                .RequirementEP(30)
                .UsesAnimation(AnimationType.LoopingConjure2)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    ApplyPartyAOE<LightWardStatusEffect>(activator, activator, 15f, 15);
                });
        }
    }
}
