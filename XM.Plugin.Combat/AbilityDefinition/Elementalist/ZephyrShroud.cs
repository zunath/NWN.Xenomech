using System.Collections.Generic;
using Anvil.Services;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using XM.Shared.Core.Party;

namespace XM.Plugin.Combat.AbilityDefinition.Elementalist
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class ZephyrShroud: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();

        public ZephyrShroud(
            StatusEffectService status,
            PartyService party)
            : base(party, status)
        {
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ZephyrShroudAbility();

            return _builder.Build();
        }

        private void ZephyrShroudAbility()
        {
            _builder.Create(FeatType.ZephyrShroud)
                .Name(LocaleString.ZephyrShroud)
                .Description(LocaleString.ZephyrShroudDescription)
                .HasRecastDelay(RecastGroup.ZephyrShroud, 60f * 5f)
                .IsCastedAbility()
                .RequirementEP(40)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .HasActivationDelay(2f)
                .ResonanceCost(2)
                .HasImpactAction((activator, target, location) =>
                {
                    ApplyPartyStatusAOE<ZephyrShroudStatusEffect>(activator, activator, 10f, 1);
                });
        }
    }
}
