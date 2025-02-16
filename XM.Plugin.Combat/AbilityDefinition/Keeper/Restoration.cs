using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
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

namespace XM.Plugin.Combat.AbilityDefinition.Keeper
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Restoration: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly EnmityService _enmity;
        private readonly StatService _stat;
        private readonly StatusEffectService _status;

        public Restoration(
            EnmityService enmity, 
            StatService stat,
            StatusEffectService status)
        {
            _enmity = enmity;
            _stat = stat;
            _status = status;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Restoration1();
            Restoration2();
            Restoration3();

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
            var vitality = _stat.GetAttribute(activator, AbilityType.Vitality);
            var willpower = _stat.GetAttribute(activator, AbilityType.Willpower);

            var power = (vitality / 2) + (willpower / 4) + (level * 2);
            var powerFloor = powerFloorAction(power); 
            var rate = rateAction(power);
            var hpFloor = hpFloorAction(power);

            var healAmount = (int)Math.Floor((power - powerFloor) / rate) + hpFloor;

            if (_status.HasEffect<DivineSealStatusEffect>(activator))
            {
                healAmount *= 2;
                _status.RemoveStatusEffect<DivineSealStatusEffect>(activator);
            }

            _enmity.ApplyHealingEnmity(activator, healAmount);
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(impactVFX), target);
            ApplyEffectToObject(DurationType.Instant, EffectHeal(healAmount), target);
            Messaging.SendMessageNearbyToPlayers(activator, LocaleString.PlayerRestoresXHPToTarget.ToLocalizedString(GetName(activator), healAmount, GetName(target)));
        }

        private void Restoration1()
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
                    return 20f / 0.45f;
                else if (power >= 200)
                    return 20f / 0.45f;
                else if (power >= 125)
                    return 15f / 0.45f;
                else if (power >= 40)
                    return 8.5f / 0.45f;
                else if (power >= 20)
                    return 1.33f / 0.45f;
                else
                    return 4f / 0.45f;
            }

            int GetHPFloor(int power)
            {
                if (power >= 600)
                    return 29;
                else if (power >= 200)
                    return 20;
                else if (power >= 125)
                    return 18;
                else if (power >= 40)
                    return 14;
                else if (power >= 20)
                    return 7;
                else
                    return 4;
            }


            _builder.Create(FeatType.Restoration1)
                .Name(LocaleString.RestorationI)
                .Description(LocaleString.RestorationIDescription)
                .Classification(AbilityCategoryType.Healing)
                .HasRecastDelay(RecastGroup.Restoration, 4f)
                .HasActivationDelay(2f)
                .RequirementEP(4)
                .UsesAnimation(AnimationType.LoopingConjure2)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(1)
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, activator, VisualEffectType.ImpHealingSmall, GetPowerFloor, GetRate, GetHPFloor);
                });
        }


        private void Restoration2()
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
                    return 20f / 0.45f;
                else if (power >= 400)
                    return 20f / 0.45f;
                else if (power >= 200)
                    return 10f / 0.45f;
                else if (power >= 125)
                    return 7.5f / 0.45f;
                else if (power >= 70)
                    return 5.5f / 0.45f;
                else
                    return 1f / 0.45f;
            }

            int GetHPFloor(int power)
            {
                if (power >= 700)
                    return 65;
                else if (power >= 400)
                    return 58;
                else if (power >= 200)
                    return 49;
                else if (power >= 125)
                    return 45;
                else if (power >= 70)
                    return 40;
                else
                    return 27;
            }

            _builder.Create(FeatType.Restoration2)
                    .Name(LocaleString.RestorationII)
                    .Description(LocaleString.RestorationIIDescription)
                    .Classification(AbilityCategoryType.Healing)
                    .HasRecastDelay(RecastGroup.Restoration, 4f)
                    .HasActivationDelay(2f)
                    .RequirementEP(10)
                    .UsesAnimation(AnimationType.LoopingConjure2)
                    .DisplaysVisualEffectWhenActivating()
                    .ResonanceCost(2)
                    .HasImpactAction((activator, target, location) =>
                    {
                        Impact(activator, activator, VisualEffectType.ImpHealingMedium, GetPowerFloor, GetRate, GetHPFloor);
                    });
        }


        private void Restoration3()
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
                    return 11.11f;
                else if (power >= 300)
                    return 11.11f;
                else if (power >= 200)
                    return 5.56f;
                else if (power >= 125)
                    return 2.56f;
                else
                    return 4.89f;
            }

            int GetHPFloor(int power)
            {
                if (power >= 700)
                    return 153;
                else if (power >= 300)
                    return 117;
                else if (power >= 200)
                    return 99;
                else if (power >= 125)
                    return 70;
                else
                    return 59;
            }

            _builder.Create(FeatType.Restoration3)
                    .Name(LocaleString.RestorationIII)
                    .Description(LocaleString.RestorationIIIDescription)
                    .Classification(AbilityCategoryType.Healing)
                    .HasRecastDelay(RecastGroup.Restoration, 5f)
                    .HasActivationDelay(2f)
                    .RequirementEP(15)
                    .UsesAnimation(AnimationType.LoopingConjure2)
                    .DisplaysVisualEffectWhenActivating()
                    .ResonanceCost(3)
                    .HasImpactAction((activator, target, location) =>
                    {
                        Impact(activator, activator, VisualEffectType.ImpHealingLarge, GetPowerFloor, GetRate, GetHPFloor);
                    });
        }
    }
}
