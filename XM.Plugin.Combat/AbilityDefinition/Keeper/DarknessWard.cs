using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using XM.Shared.Core.Party;

namespace XM.Plugin.Combat.AbilityDefinition.Keeper
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class DarknessWard: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();

        public DarknessWard(
            PartyService party,
            StatusEffectService status)
            : base(party, status)
        {
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            DarknessWardAbility();

            return _builder.Build();
        }

        private void DarknessWardAbility()
        {
            _builder.Create(FeatType.DarknessWard)
                .Name(LocaleString.DarknessWard)
                .Description(LocaleString.DarknessWardDescription)
                .HasRecastDelay(RecastGroup.Ward, 10f)
                .HasActivationDelay(4f)
                .RequirementEP(30)
                .UsesAnimation(AnimationType.LoopingConjure2)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    ApplyPartyStatusAOE<DarknessWardStatusEffect>(activator, activator, 15f, 15);
                });
        }
    }
}
