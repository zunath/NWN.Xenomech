using System;
using System.Collections.Generic;
using Anvil.Services;
using XM.AI.Enmity;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
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
    internal class Restoration : IAbilityListDefinition
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

            var power = (int)((vitality / 2f) + (willpower / 4f) + (level * 2.5f));
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
                    return 44.44f;
                else if (power >= 200)
                    return 44.44f;
                else if (power >= 125)
                    return 33.33f;
                else if (power >= 40)
                    return 18.88f;
                else if (power >= 20)
                    return 2.96f;
                else
                    return 8.88f;
            }

            int GetHPFloor(int power)
            {
                if (power >= 600)
                    return 38;
                else if (power >= 200)
                    return 27;
                else if (power >= 125)
                    return 24;
                else if (power >= 40)
                    return 19;
                else if (power >= 20)
                    return 11;
                else
                    return 7;
            }

            _builder.Create(FeatType.Restoration1)
                .Name(LocaleString.RestorationI)
                .Description(LocaleString.RestorationIDescription)
                .Classification(AbilityCategoryType.Healing)
                .HasRecastDelay(RecastGroup.Restoration, 4f)
                .HasActivationDelay(2f)
                .RequirementEP(4)
                .UsesAnimation(AnimationType.LoopingConjure1)
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
                    return 44.44f;
                else if (power >= 400)
                    return 44.44f;
                else if (power >= 200)
                    return 22.22f;
                else if (power >= 125)
                    return 16.66f;
                else if (power >= 70)
                    return 12.22f;
                else
                    return 2.22f;
            }

            int GetHPFloor(int power)
            {
                if (power >= 700)
                    return 78;
                else if (power >= 400)
                    return 68;
                else if (power >= 200)
                    return 59;
                else if (power >= 125)
                    return 55;
                else if (power >= 70)
                    return 48;
                else
                    return 35;
            }

            _builder.Create(FeatType.Restoration2)
                .Name(LocaleString.RestorationII)
                .Description(LocaleString.RestorationIIDescription)
                .Classification(AbilityCategoryType.Healing)
                .HasRecastDelay(RecastGroup.Restoration, 4f)
                .HasActivationDelay(2f)
                .RequirementEP(10)
                .UsesAnimation(AnimationType.LoopingConjure1)
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
                    return 10.55f;
                else if (power >= 300)
                    return 10.55f;
                else if (power >= 200)
                    return 5.28f;
                else if (power >= 125)
                    return 2.45f;
                else
                    return 4.67f;
            }

            int GetHPFloor(int power)
            {
                if (power >= 700)
                    return 165;
                else if (power >= 300)
                    return 132;
                else if (power >= 200)
                    return 115;
                else if (power >= 125)
                    return 85;
                else
                    return 72;
            }

            _builder.Create(FeatType.Restoration3)
                .Name(LocaleString.RestorationIII)
                .Description(LocaleString.RestorationIIIDescription)
                .Classification(AbilityCategoryType.Healing)
                .HasRecastDelay(RecastGroup.Restoration, 5f)
                .HasActivationDelay(2f)
                .RequirementEP(15)
                .UsesAnimation(AnimationType.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .ResonanceCost(3)
                .HasImpactAction((activator, target, location) =>
                {
                    Impact(activator, activator, VisualEffectType.ImpHealingLarge, GetPowerFloor, GetRate, GetHPFloor);
                });
        }
    }
}
