using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Progression.Ability;
using XM.Progression.Beast;
using XM.Progression.Recast;
using XM.Progression.Stat;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.Localization;

namespace XM.Plugin.Combat.AbilityDefinition.Beastmaster
{
    [ServiceBinding(typeof(IAbilityListDefinition))]
    internal class Reward: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        private readonly StatService _stat;
        private readonly StatusEffectService _status;
        private readonly EnmityService _enmity;
        private readonly BeastService _beast;

        public Reward(
            StatService stat,
            StatusEffectService status,
            EnmityService enmity,
            BeastService beast)
        {
            _stat = stat;
            _status = status;
            _enmity = enmity;
            _beast = beast;
        }

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            Reward1();
            Reward2();
            Reward3();

            return _builder.Build();
        }

        private string Validation(uint activator)
        {
            var beast = _beast.GetBeast(activator);

            return beast == OBJECT_INVALID 
                ? LocaleString.YouDoNotHaveAnActiveBeast.ToLocalizedString() 
                : string.Empty;
        }

        private void Impact(
            uint activator,
            VisualEffectType impactVFX,
            Func<int, int> powerFloorAction,
            Func<int, float> rateAction,
            Func<int, int> hpFloorAction)
        {
            var beast = _beast.GetBeast(activator);
            var level = _stat.GetLevel(activator);
            var social = _stat.GetAttribute(activator, AbilityType.Social);
            var agility = _stat.GetAttribute(activator, AbilityType.Agility);

            var power = (social / 2) + (agility / 4) + (level * 2);
            var powerFloor = powerFloorAction(power);
            var rate = rateAction(power);
            var hpFloor = hpFloorAction(power);

            var healModifier = 1 + _stat.GetHealingModifier(activator);
            var healAmount = (int)Math.Floor((power - powerFloor) / rate) + hpFloor;
            healAmount *= (int)healModifier;

            if (_status.HasEffect<DivineSealStatusEffect>(activator))
            {
                healAmount *= 2;
                _status.RemoveStatusEffect<DivineSealStatusEffect>(activator);
            }

            _enmity.ApplyHealingEnmity(activator, healAmount);
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(impactVFX), beast);
            ApplyEffectToObject(DurationType.Instant, EffectHeal(healAmount), beast);
            Messaging.SendMessageNearbyToPlayers(activator, LocaleString.PlayerRestoresXHPToTarget.ToLocalizedString(GetName(activator), healAmount, GetName(beast)));
        }

        private void Reward1()
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

            _builder.Create(FeatType.Reward1)
                .Name(LocaleString.RewardI)
                .Description(LocaleString.RewardIDescription)
                .Classification(AbilityCategoryType.HPRestoration)
                .TargetingType(AbilityTargetingType.SelfTargetsParty)
                .HasRecastDelay(RecastGroup.Reward, 4f)
                .HasActivationDelay(2f)
                .RequirementEP(4)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(1)
                .HasCustomValidation((activator, target, location) => Validation(activator))
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, VisualEffectType.ImpHealingSmall, GetPowerFloor, GetRate, GetHPFloor);
                });
        }

        private void Reward2()
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


            _builder.Create(FeatType.Reward2)
                .Name(LocaleString.RewardII)
                .Description(LocaleString.RewardIIDescription)
                .Classification(AbilityCategoryType.HPRestoration)
                .TargetingType(AbilityTargetingType.SelfTargetsParty)
                .HasRecastDelay(RecastGroup.Reward, 4f)
                .HasActivationDelay(2f)
                .RequirementEP(16)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(2)
                .HasCustomValidation((activator, target, location) => Validation(activator))
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, VisualEffectType.ImpHealingSmall, GetPowerFloor, GetRate, GetHPFloor);
                });
        }

        private void Reward3()
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
            _builder.Create(FeatType.Reward3)
                .Name(LocaleString.RewardIII)
                .Description(LocaleString.RewardIIIDescription)
                .Classification(AbilityCategoryType.HPRestoration)
                .TargetingType(AbilityTargetingType.SelfTargetsParty)
                .HasRecastDelay(RecastGroup.Reward, 4f)
                .HasActivationDelay(2f)
                .RequirementEP(28)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(3)
                .HasCustomValidation((activator, target, location) => Validation(activator))
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, VisualEffectType.ImpHealingSmall, GetPowerFloor, GetRate, GetHPFloor);
                });
        }
    }
}
