using Anvil.Services;
using System;
using XM.Inventory;
using XM.Progression.Job;
using XM.Progression.Stat;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.Localization;
using CreaturePlugin = XM.Shared.API.NWNX.CreaturePlugin.CreaturePlugin;
using XM.Shared.API.NWNX.FeedbackPlugin;
using FeedbackPlugin = XM.Shared.API.NWNX.FeedbackPlugin.FeedbackPlugin;

namespace XM.Combat
{
    [ServiceBinding(typeof(CombatService))]
    internal class CombatService: IInitializable
    {
        private readonly DBService _db;
        private readonly JobService _job;
        private readonly StatService _stat;
        private readonly ItemTypeService _itemType;

        public CombatService(
            DBService db,
            JobService job,
            StatService stat,
            ItemTypeService itemType)
        {
            _db = db;
            _job = job;
            _stat = stat;
            _itemType = itemType;
        }
        public void Init()
        {
            DisableDefaultCombatMessages();
        }

        private void DisableDefaultCombatMessages()
        {
            FeedbackPlugin.SetCombatLogMessageHidden(FeedbackCombatLogType.Initiative, true);
            FeedbackPlugin.SetCombatLogMessageHidden(FeedbackCombatLogType.ComplexAttack, true);
        }

        public int CalculateHitRate(
            int attackerAccuracy,
            int defenderEvasion)
        {
            const int BaseHitRate = 75;

            var hitRate = BaseHitRate + (int)Math.Floor((attackerAccuracy - defenderEvasion) / 2.0f);

            if (hitRate < 20)
                hitRate = 20;
            else if (hitRate > 95)
                hitRate = 95;

            return hitRate;
        }

        private int CalculateCriticalRate(
            int attackerStat, 
            int defenderStat)
        {
            const int BaseCriticalRate = 5;
            var delta = attackerStat - defenderStat;

            if (delta < 0)
                delta = 0;
            else if (delta > 15)
                delta = 15;

            return BaseCriticalRate + delta;
        }

        private int CalculateAccuracy(
            uint attacker, 
            uint defender, 
            AttackType attackType, 
            CombatModeType combatMode)
        {
            var bonus = _stat.GetAccuracy(attacker);
            var perception = _stat.GetAttribute(attacker, AbilityType.Perception);
            var level = _job.GetLevel(attacker);
            var modifiers = 0;

            if (attackType == AttackType.Ranged)
            {
                modifiers += CalculateRangeModifier(attacker, defender);
            }
            else if (attackType == AttackType.Melee)
            {
                modifiers += CalculateDualWieldAccuracyModifier(attacker);
                modifiers += CalculateCombatModeAccuracyModifier(combatMode);
            }

            return perception * 3 + level + bonus + modifiers;
        }

        private int CalculateEvasion(uint creature)
        {
            var agility = _stat.GetAttribute(creature, AbilityType.Agility);
            var baseEvasion = CreaturePlugin.GetBaseAC(creature) * 5;
            var evasionBonus = _stat.GetEvasion(creature);
            var level = _stat.GetLevel(creature);

            return agility * 3 + level + baseEvasion + evasionBonus;
        }

        private int CalculateRangeModifier(uint attacker, uint defender)
        {
            var distance = GetDistanceBetween(attacker, defender);

            if (distance >= 40f)
                return -20;

            return 0;
        }

        private int CalculateDualWieldAccuracyModifier(uint attacker)
        {
            var mainHand = GetItemInSlot(InventorySlotType.RightHand, attacker);
            var offHand = GetItemInSlot(InventorySlotType.LeftHand, attacker);
            var isShield = _itemType.IsShield(offHand);
            
            if (!isShield && GetIsObjectValid(mainHand) && GetIsObjectValid(offHand))
            {
                return -10;
            }

            return 0;
        }

        private int CalculateCombatModeAccuracyModifier(CombatModeType type)
        {
            switch (type)
            {
                case CombatModeType.PowerAttack:
                    return -5;
                case CombatModeType.ImprovedPowerAttack:
                    return -10;
                default:
                    return 0;
            }
        }

        private int CalculateDeflectionChance(uint defender)
        {
            var offHand = GetItemInSlot(InventorySlotType.LeftHand, defender);
            var isShield = _itemType.IsShield(offHand);

            return isShield ? 10 : 0;
        }

        public (HitResultType, int) DetermineHitType(
            uint attacker, 
            uint defender, 
            AttackType attackType, 
            CombatModeType combatMode)
        {
            var accuracy = CalculateAccuracy(attacker, defender, attackType, combatMode);
            var evasion = CalculateEvasion(defender);
            var roll = XMRandom.D100(1);
            var hitRate = CalculateHitRate(accuracy, evasion);
            var result = HitResultType.Miss;

            if (roll <= hitRate)
            {
                var isDeflect = DetermineAttackDeflection(defender);
                var isCritical = DetermineCriticalHit(attacker, defender, attackType);

                if (isDeflect)
                {
                    result = HitResultType.Deflect;
                }
                else if (isCritical)
                {
                    result = HitResultType.Critical;
                }
                else
                {
                    result = HitResultType.Hit;
                }
            }

            return (result, hitRate);
        }

        private bool DetermineCriticalHit(uint attacker, uint defender, AttackType attackType)
        {
            if (GetIsImmune(defender, ImmunityType.CriticalHit, attacker))
                return false;

            var attackerStat = attackType == AttackType.Melee
                ? _stat.GetAttribute(attacker, AbilityType.Perception)
                : _stat.GetAttribute(attacker, AbilityType.Agility);
            var defenderStat = _stat.GetAttribute(defender, AbilityType.Agility);
            var roll = XMRandom.D100(1);
            var criticalRate = CalculateCriticalRate(attackerStat, defenderStat);

            return roll <= criticalRate;
        }

        private bool DetermineAttackDeflection(uint defender)
        {
            var roll = XMRandom.D100(1);
            var deflectionChance = CalculateDeflectionChance(defender);

            return roll <= deflectionChance;
        }

        public string BuildCombatLogMessage(
            uint attacker, 
            uint defender, 
            HitResultType hitType,
            int chanceToHit)
        {
            var attackerName = GetIsPC(attacker) 
                ? ColorToken.GetNamePCColor(attacker) 
                : ColorToken.GetNameNPCColor(attacker);
            var defenderName = GetIsPC(defender)
                ? ColorToken.GetNamePCColor(defender)
                : ColorToken.GetNameNPCColor(defender);

            var type = string.Empty;

            switch (hitType)
            {
                case HitResultType.Hit:
                    type = LocaleString.Hit.ToLocalizedString();
                    break;
                case HitResultType.Miss:
                    type = LocaleString.Miss.ToLocalizedString();
                    break;
                case HitResultType.Deflect:
                    type = LocaleString.Deflect.ToLocalizedString();
                    break;
                case HitResultType.Critical:
                    type = LocaleString.Critical.ToLocalizedString();
                    break;
            }

            var message = LocaleString.AttackerAttacksDefender.ToLocalizedString(attackerName, defenderName, type, chanceToHit);
            return ColorToken.Combat(message);
        }

        private int CalculateAttack(uint attacker, AttackType attackType)
        {
            var attack = _stat.GetAttack(attacker);
            var stat = attackType == AttackType.Melee
                ? _stat.GetAttribute(attacker, AbilityType.Might)
                : _stat.GetAttribute(attacker, AbilityType.Agility);
            var level = _stat.GetLevel(attacker);

            return 8 + (2 * level) + stat + attack;
        }

        private int CalculateDefense(uint defender)
        {
            var defense = _stat.GetDefense(defender);
            var stat = _stat.GetAttribute(defender, AbilityType.Vitality);
            var level = _stat.GetLevel(defender);

            return (int)(8 + (stat * 1.5f) + level + defense);
        }

        private float CalculateDamageRatio(uint attacker, uint defender, AttackType attackType)
        {
            const float RatioMax = 3.625f;
            const float RatioMin = 0.01f;

            var attackerAttack = CalculateAttack(attacker, attackType);
            var defenderDefense = CalculateDefense(defender);
            if (defenderDefense < 1)
                defenderDefense = 1;

            var ratio = (float)attackerAttack / (float)defenderDefense;

            if (ratio > RatioMax)
                ratio = RatioMax;
            else if (ratio < RatioMin)
                ratio = RatioMin;

            return ratio;
        }

        private int CalculateDamageStatDelta(uint attacker, uint defender, AttackType attackType)
        {
            var attackerStat = attackType == AttackType.Melee
                ? _stat.GetAttribute(attacker, AbilityType.Might)
                : _stat.GetAttribute(attacker, AbilityType.Agility);
            var defenderStat = _stat.GetAttribute(defender, AbilityType.Vitality);
            var delta = attackerStat - defenderStat;

            if (delta >= 12)
                return (delta + 4) / 4;
            else if (delta >= 6)
                return (delta + 6) / 4;
            else if (delta >= 1)
                return (delta + 7) / 4;
            else if (delta >= -2)
                return (delta + 8) / 4;
            else if (delta >= -7)
                return (delta + 9) / 4;
            else if (delta >= -15)
                return (delta + 10) / 4;
            else if (delta >= -21)
                return (delta + 12) / 4;
            else
                return (delta + 13) / 4;
        }

        private (int, int) CalculateDamageRange(uint attacker, uint defender, uint attackerWeapon, AttackType attackType)
        {
            var delta = CalculateDamageStatDelta(attacker, defender, attackType);
            var ratio = CalculateDamageRatio(attacker, defender, attackType);
            var attackerDMG = _stat.GetDMG(attackerWeapon);
            var baseDMG = attackerDMG + delta;

            var maxDamage = baseDMG * ratio;
            var minDamage = maxDamage * 0.7f;

            return ((int)minDamage, (int)maxDamage);
        }

        public int DetermineDamage(
            uint attacker, 
            uint defender,
            uint attackerWeapon,
            AttackType attackType,
            HitResultType hitResult)
        {
            var (minDamage, maxDamage) = CalculateDamageRange(attacker, defender, attackerWeapon, attackType);
            var damage = XMRandom.Next(minDamage, maxDamage);

            if (hitResult == HitResultType.Critical)
                damage = (int)(damage * 0.25f);

            return damage;
        }

    }
}
