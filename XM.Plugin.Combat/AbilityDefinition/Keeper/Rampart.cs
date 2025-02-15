using System.Collections.Generic;
using Anvil.Services;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Progression.StatusEffect.StatusEffectDefinition;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using XM.Shared.Core.Party;

namespace XM.Plugin.Combat.AbilityDefinition.Keeper
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Rampart: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();

        public Rampart(
            PartyService party,
            StatusEffectService status)
            : base(party, status)
        {
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            RampartAbility();

            return _builder.Build();
        }

        private void RampartAbility()
        {
            _builder.Create(FeatType.Rampart)
                .Name(LocaleString.Rampart)
                .Description(LocaleString.RampartDescription)
                .HasRecastDelay(RecastGroup.Rampart, 90f)
                .HasActivationDelay(2f)
                .RequirementEP(8)
                .UsesAnimation(AnimationType.LoopingConjure2)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(2)
                .HasImpactAction((activator, target, location) =>
                {
                    ApplyPartyAOE<RampartStatusEffect>(activator, activator, 15f, 2);
                });
        }
    }
}
