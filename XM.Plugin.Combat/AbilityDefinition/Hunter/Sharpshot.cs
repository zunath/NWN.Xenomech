using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using XM.Shared.Core.Party;

namespace XM.Plugin.Combat.AbilityDefinition.Hunter
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Sharpshot: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();
        private readonly EnmityService _enmity;

        public Sharpshot(
            StatusEffectService status,
            EnmityService enmity,
            PartyService party)
            : base(party, status)
        {
            _enmity = enmity;
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            SharpshotAbility();

            return _builder.Build();
        }

        private void SharpshotAbility()
        {
            _builder.Create(FeatType.Sharpshot)
                .Name(LocaleString.Sharpshot)
                .Description(LocaleString.SharpshotDescription)
                .HasRecastDelay(RecastGroup.Sharpshot, 60f * 5)
                .IsCastedAbility()
                .RequirementEP(40)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .HasActivationDelay(2f)
                .ResonanceCost(2)
                .HasImpactAction((activator, target, location) =>
                {
                    ApplyPartyStatusAOE<SharpshotStatusEffect>(activator, activator, 10f, 1);
                    _enmity.ModifyEnmityOnAll(activator, EnmityType.Volatile, 800);
                });
        }
    }
}
