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
    internal class EarthWard: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();

        public EarthWard(
            PartyService party,
            StatusEffectService status)
            : base(party, status)
        {
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            EarthWardAbility();

            return _builder.Build();
        }

        private void EarthWardAbility()
        {
            _builder.Create(FeatType.EarthWard)
                .Name(LocaleString.EarthWard)
                .Description(LocaleString.EarthWardDescription)
                .HasRecastDelay(RecastGroup.Ward, 10f)
                .HasActivationDelay(4f)
                .RequirementEP(30)
                .UsesAnimation(AnimationType.LoopingConjure2)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    ApplyPartyAOE<EarthWardStatusEffect>(activator, activator, 15f, 15);
                });
        }
    }
}
