using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Plugin.Combat.StatusEffectDefinition;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.Localization;
using XM.Shared.Core.Party;

namespace XM.Plugin.Combat.AbilityDefinition.Mender
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class EtherBlast : AbilityBase
    {
        private readonly AbilityBuilder _builder = new();

        private readonly EnmityService _enmity;
        private readonly StatService _stat;
        private readonly StatusEffectService _status;

        public EtherBlast(
            EnmityService enmity,
            StatService stat,
            StatusEffectService status,
            PartyService party)
        : base(party, status)
        {
            _enmity = enmity;
            _stat = stat;
            _status = status;
        }

        public override Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            EtherBlast1();
            EtherBlast2();

            return _builder.Build();
        }

        private void Impact(
            uint activator,
            VisualEffectType impactVFX,
            Func<int, float> rateAction,
            Func<int, int> constAction,
            int minCap)
        {
            var willpower = _stat.GetAttribute(activator, AbilityType.Willpower);
            var vitality = _stat.GetAttribute(activator, AbilityType.Vitality);

            var power = 3 * willpower + vitality;
            var rate = rateAction(power);
            var hpConst = constAction(power);

            var healAmount = (int)Math.Floor((Math.Floor(power / 2.0) / rate)) + hpConst;

            if (healAmount < minCap)
            {
                healAmount = minCap;
            }

            if (_status.HasEffect<DivineSealStatusEffect>(activator))
            {
                healAmount *= 2;
                _status.RemoveStatusEffect<DivineSealStatusEffect>(activator);
            }

            ApplyPartyAOE(activator, 10f, target =>
            {
                _enmity.ApplyHealingEnmity(activator, healAmount);
                ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(impactVFX), target);
                ApplyEffectToObject(DurationType.Instant, EffectHeal(healAmount), target);
                Messaging.SendMessageNearbyToPlayers(activator, LocaleString.PlayerRestoresXHPToTarget.ToLocalizedString(GetName(activator), healAmount, GetName(target)));
            });
        }

        private void EtherBlast1()
        {
            float GetRate(int power)
            {
                return Math.Max(1 + (power / 100.0f), 1);
            }

            int GetConst(int power)
            {
                return 60 + (power / 10);
            }

            _builder.Create(FeatType.EtherBlast1)
                .Name(LocaleString.EtherBlastI)
                .Description(LocaleString.EtherBlastIDescription)
                .Classification(AbilityCategoryType.Healing)
                .HasRecastDelay(RecastGroup.EtherBlast, 10f)
                .HasActivationDelay(4f)
                .RequirementEP(60)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(2)
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, VisualEffectType.ImpHealingSmall, GetRate, GetConst, 20);
                });
        }

        private void EtherBlast2()
        {
            float GetRate(int power)
            {
                return Math.Max(1 + (power / 150.0f), 1);
            }

            int GetConst(int power)
            {
                return 130 + (power / 8);
            }

            _builder.Create(FeatType.EtherBlast2)
                .Name(LocaleString.EtherBlastII)
                .Description(LocaleString.EtherBlastIIDescription)
                .Classification(AbilityCategoryType.Healing)
                .HasRecastDelay(RecastGroup.EtherBloom, 10f)
                .HasActivationDelay(5f)
                .RequirementEP(95)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(3)
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, VisualEffectType.ImpHealingMedium, GetRate, GetConst, 70);
                });
        }
    }
}
