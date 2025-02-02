using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Progression.Ability;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.Localization;

namespace XM.Combat.AbilityDefinition.Mender
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class EtherBloom: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly EnmityService _enmity;
        private readonly StatService _stat;

        public EtherBloom(
            EnmityService enmity, 
            StatService stat)
        {
            _enmity = enmity;
            _stat = stat;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            EtherBloom1();

            return _builder.Build();
        }


        private void Impact(
            uint activator, 
            uint target,
            VisualEffectType impactVFX,
            Func<int, int> powerFloorAction,
            Func<int, float> rateAction,
            Func<int, int> hpFloorAction)
        {
            var level = _stat.GetLevel(activator);
            var willpower = _stat.GetAttribute(activator, AbilityType.Willpower);
            var vitality = _stat.GetAttribute(activator, AbilityType.Vitality);

            var power = (willpower / 2) + (vitality / 4) + (level * 2);
            var powerFloor = powerFloorAction(power); 
            var rate = rateAction(power);
            var hpFloor = hpFloorAction(power);

            var healAmount = (int)Math.Floor((power - powerFloor) / rate) + hpFloor;

            _enmity.ApplyHealingEnmity(activator, healAmount);
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(impactVFX), target);
            ApplyEffectToObject(DurationType.Instant, EffectHeal(healAmount), target);
            Messaging.SendMessageNearbyToPlayers(activator, LocaleString.PlayerRestoresXHPToTarget.ToLocalizedString(GetName(activator), healAmount, GetName(target)));
        }

        private void EtherBloom1()
        {
            int GetPowerFloor(int power)
            {
                if (power >= 600)
                    return 600;
                else if (power >= 200)
                    return 200;
                else if (power >= 125)
                    return 125;
                else if (power >= 40)
                    return 40;
                else if (power >= 20)
                    return 20;
                else
                    return 0;
            }

            float GetRate(int power)
            {
                if (power >= 600)
                    return 20;
                else if (power >= 200)
                    return 20;
                else if (power >= 125)
                    return 15;
                else if (power >= 40)
                    return 8.5f;
                else if (power >= 20)
                    return 1.33f;
                else
                    return 4;
            }

            int GetHPFloor(int power)
            {
                if (power >= 600)
                    return 65;
                else if (power >= 200)
                    return 45;
                else if (power >= 125)
                    return 40;
                else if (power >= 40)
                    return 30;
                else if (power >= 20)
                    return 15;
                else
                    return 10;
            }

        _builder.Create(FeatType.EtherBloom1)
                .Name(LocaleString.EtherBloomI)
                .Description(LocaleString.EtherBloomIDescription)
                .HasRecastDelay(RecastGroup.EtherBloom, 3f)
                .HasActivationDelay(2f)
                .RequirementEP(8)
                .HasMaxRange(15f)
                .UsesAnimation(AnimationType.LoopingConjure2)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, target, VisualEffectType.ImpHealingSmall, GetPowerFloor, GetRate, GetHPFloor);
                });
        }
    }
}
