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

namespace XM.Plugin.Combat.AbilityDefinition.Mender
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
            EtherBloom2();
            EtherBloom3();
            EtherBloom4();

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
                .Classification(AbilityCategoryType.Healing)
                .HasRecastDelay(RecastGroup.EtherBloom, 4f)
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


        private void EtherBloom2()
        {
            int GetPowerFloor(int power)
            {
                if (power >= 700)
                    return 700;
                else if (power >= 400)
                    return 400;
                else if (power >= 200)
                    return 200;
                else if (power >= 125)
                    return 125;
                else if (power >= 70)
                    return 70;
                else
                    return 40;
            }

            float GetRate(int power)
            {
                if (power >= 700)
                    return 20;
                else if (power >= 400)
                    return 20;
                else if (power >= 200)
                    return 10;
                else if (power >= 125)
                    return 7.5f;
                else if (power >= 70)
                    return 5.5f;
                else
                    return 1;
            }

            int GetHPFloor(int power)
            {
                if (power >= 700)
                    return 145;
                else if (power >= 400)
                    return 130;
                else if (power >= 200)
                    return 110;
                else if (power >= 125)
                    return 100;
                else if (power >= 70)
                    return 90;
                else
                    return 60;
            }

            _builder.Create(FeatType.EtherBloom2)
                    .Name(LocaleString.EtherBloomII)
                    .Description(LocaleString.EtherBloomIIDescription)
                    .Classification(AbilityCategoryType.Healing)
                    .HasRecastDelay(RecastGroup.EtherBloom, 4f)
                    .HasActivationDelay(2f)
                    .RequirementEP(24)
                    .HasMaxRange(15f)
                    .UsesAnimation(AnimationType.LoopingConjure2)
                    .DisplaysVisualEffectWhenActivating()
                    .ResonanceCost(2)
                    .HasImpactAction((activator, target, location) =>
                    {
                        Impact(activator, target, VisualEffectType.ImpHealingMedium, GetPowerFloor, GetRate, GetHPFloor);
                    });
        }


        private void EtherBloom3()
        {
            int GetPowerFloor(int power)
            {
                if (power >= 700)
                    return 700;
                else if (power >= 300)
                    return 300;
                else if (power >= 200)
                    return 200;
                else if (power >= 125)
                    return 125;
                else
                    return 70;
            }

            float GetRate(int power)
            {
                if (power >= 700)
                    return 5f;
                else if (power >= 300)
                    return 5f;
                else if (power >= 200)
                    return 2.5f;
                else if (power >= 125)
                    return 1.15f;
                else
                    return 2.2f;
            }

            int GetHPFloor(int power)
            {
                if (power >= 700)
                    return 340;
                else if (power >= 300)
                    return 260;
                else if (power >= 200)
                    return 220;
                else if (power >= 125)
                    return 155;
                else
                    return 130;
            }

            _builder.Create(FeatType.EtherBloom3)
                    .Name(LocaleString.EtherBloomIII)
                    .Description(LocaleString.EtherBloomIIIDescription)
                    .Classification(AbilityCategoryType.Healing)
                    .HasRecastDelay(RecastGroup.EtherBloom, 5f)
                    .HasActivationDelay(3f)
                    .RequirementEP(46)
                    .HasMaxRange(15f)
                    .UsesAnimation(AnimationType.LoopingConjure2)
                    .DisplaysVisualEffectWhenActivating()
                    .ResonanceCost(3)
                    .HasImpactAction((activator, target, location) =>
                    {
                        Impact(activator, target, VisualEffectType.ImpHealingLarge, GetPowerFloor, GetRate, GetHPFloor);
                    });
        }

        private void EtherBloom4()
        {
            int GetPowerFloor(int power)
            {
                if (power >= 700)
                    return 700;
                else if (power >= 400)
                    return 400;
                else if (power >= 300)
                    return 300;
                else if (power >= 200)
                    return 200;
                else
                    return 70;
            }

            float GetRate(int power)
            {
                if (power >= 700)
                    return 2.5f;
                else if (power >= 400)
                    return 2.5f;
                else if (power >= 300)
                    return 1.43f;
                else if (power >= 200)
                    return 2f;
                else
                    return 1f;
            }

            int GetHPFloor(int power)
            {
                if (power >= 700)
                    return 640;
                else if (power >= 400)
                    return 520;
                else if (power >= 300)
                    return 450;
                else if (power >= 200)
                    return 400;
                else
                    return 270;
            }

            _builder.Create(FeatType.EtherBloom4)
                    .Name(LocaleString.EtherBloomIV)
                    .Description(LocaleString.EtherBloomIVDescription)
                    .Classification(AbilityCategoryType.Healing)
                    .HasRecastDelay(RecastGroup.EtherBloom, 10f)
                    .HasActivationDelay(5f)
                    .RequirementEP(88)
                    .HasMaxRange(15f)
                    .UsesAnimation(AnimationType.LoopingConjure2)
                    .DisplaysVisualEffectWhenActivating()
                    .ResonanceCost(4)
                    .HasImpactAction((activator, target, location) =>
                    {
                        Impact(activator, target, VisualEffectType.ImpHealingGreat, GetPowerFloor, GetRate, GetHPFloor);
                    });
        }
    }
}
