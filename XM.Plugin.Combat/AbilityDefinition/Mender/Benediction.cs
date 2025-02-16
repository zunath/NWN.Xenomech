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

namespace XM.Plugin.Combat.AbilityDefinition.Mender
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Benediction: AbilityBase
    {
        private readonly AbilityBuilder _builder = new();

        private readonly EnmityService _enmity;
        private readonly StatService _stat;

        public Benediction(
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
            BenedictionAbility();

            return _builder.Build();
        }

        private void BenedictionAbility()
        {
            _builder.Create(FeatType.Benediction)
                .Name(LocaleString.Benediction)
                .Description(LocaleString.BenedictionDescription)
                .HasRecastDelay(RecastGroup.JobCapstone, 60f * 30f)
                .IsCastedAbility()
                .RequirementEP(150)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .HasActivationDelay(4f)
                .ResonanceCost(3)
                .HasImpactAction((activator, target, location) =>
                {
                    ApplyPartyAOE(activator, 15f, member =>
                    {
                        var maxHP = _stat.GetMaxHP(member);
                        ApplyEffectToObject(DurationType.Instant, EffectHeal(maxHP), member);
                        ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffectType.ImpHealingExtra), member);
                        _enmity.ModifyEnmityOnAll(activator, EnmityType.Volatile, maxHP);
                    });

                    _enmity.ModifyEnmityOnAll(activator, EnmityType.Volatile, 4500);
                });
        }
    }
}
