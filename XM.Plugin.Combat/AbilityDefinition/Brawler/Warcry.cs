using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using XM.Shared.Core.Party;

namespace XM.Plugin.Combat.AbilityDefinition.Brawler
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Warcry: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();

        public Warcry(
            PartyService party,
            StatusEffectService status)
            : base(party, status)
        {
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            WarcryAbility();

            return _builder.Build();
        }

        private void WarcryAbility()
        {
            _builder.Create(FeatType.Warcry)
                .Name(LocaleString.Warcry)
                .Description(LocaleString.WarcryDescription)
                .HasRecastDelay(RecastGroup.Warcry, 15f)
                .HasActivationDelay(2f)
                .RequirementEP(14)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(2)
                .HasImpactAction((activator, target, location) =>
                {
                    ApplyPartyStatusAOE<WarcryStatusEffect>(activator, activator, 10f, 1);
                });
        }
    }
}
