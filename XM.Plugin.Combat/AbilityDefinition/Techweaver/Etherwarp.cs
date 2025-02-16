using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using XM.Shared.Core.Party;

namespace XM.Plugin.Combat.AbilityDefinition.Techweaver
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Etherwarp: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();

        private readonly EnmityService _enmity;
        private readonly StatService _stat;

        public Etherwarp(
            EnmityService enmity,
            PartyService party,
            StatusEffectService status,
            StatService stat)
        :base(party, status)
        {
            _enmity = enmity;
            _stat = stat;
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            EtherwarpAbility();

            return _builder.Build();
        }

        private void EtherwarpAbility()
        {
            _builder.Create(FeatType.Etherwarp)
                .Name(LocaleString.Etherwarp)
                .Description(LocaleString.EtherwarpDescription)
                .HasRecastDelay(RecastGroup.JobCapstone, 60f * 30f)
                .IsCastedAbility()
                .RequirementEP(150)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .HasActivationDelay(4f)
                .ResonanceCost(3)
                .HasImpactAction((activator, target, location) =>
                {
                    ApplyPartyAOE(activator, 13f, member =>
                    {
                        var maxEP = _stat.GetMaxEP(member);
                        _stat.RestoreEP(member, maxEP);
                        ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpHealingExtra), member);
                        _enmity.ModifyEnmityOnAll(activator, EnmityType.Volatile, maxEP);
                    });

                    _enmity.ModifyEnmityOnAll(activator, EnmityType.Volatile, 4500);
                });
        }
    }
}
