using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Progression.StatusEffect.StatusEffectDefinition;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using XM.Shared.Core.Party;

namespace XM.Plugin.Combat.AbilityDefinition.Hunter
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class IceWard: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();

        public IceWard(
            PartyService party,
            StatusEffectService status)
            : base(party, status)
        {
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            IceWardAbility();

            return _builder.Build();
        }

        private void IceWardAbility()
        {
            _builder.Create(FeatType.IceWard)
                .Name(LocaleString.IceWard)
                .Description(LocaleString.IceWardDescription)
                .HasRecastDelay(RecastGroup.Ward, 10f)
                .HasActivationDelay(4f)
                .RequirementEP(30)
                .UsesAnimation(AnimationType.LoopingConjure2)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    ApplyPartyAOE<IceWardStatusEffect>(activator, activator, 15f, 15);
                });
        }
    }
}
