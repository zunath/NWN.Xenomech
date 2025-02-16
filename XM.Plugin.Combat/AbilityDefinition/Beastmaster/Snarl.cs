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
    internal class Snarl: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();

        private readonly StatusEffectService _status;

        public Snarl(
            PartyService party,
            StatusEffectService status)
            : base(party, status)
        {
            _status = status;
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            SnarlAbility();

            return _builder.Build();
        }

        private void SnarlAbility()
        {
            _builder.Create(FeatType.Snarl)
                .Name(LocaleString.Snarl)
                .Description(LocaleString.SnarlDescription)
                .HasRecastDelay(RecastGroup.Snarl, 30f)
                .HasActivationDelay(2f)
                .RequirementEP(20)
                .UsesAnimation(AnimationType.LoopingConjure2)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    ApplyEnemyAOE(activator, 8f, enemy =>
                    {
                        _status.ApplyStatusEffect<SnarlStatusEffect>(activator, enemy, 1);
                    });
                });
        }
    }
}
