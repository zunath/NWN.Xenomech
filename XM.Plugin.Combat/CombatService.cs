using System;
using System.Numerics;
using Anvil.Services;
using NWN.Core.NWNX;
using XM.Inventory;
using XM.Plugin.Combat.StatusEffectDefinition.Buff;
using XM.Plugin.Combat.StatusEffectDefinition.Debuff;
using XM.Progression.Ability;
using XM.Progression.Beast;
using XM.Progression.Event;
using XM.Progression.Skill;
using XM.Progression.Stat;
using XM.Progression.Stat.Entity;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.API.NWNX.FeedbackPlugin;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;
using FeedbackPlugin = XM.Shared.API.NWNX.FeedbackPlugin.FeedbackPlugin;
using SkillType = XM.Progression.Skill.SkillType;


namespace XM.Plugin.Combat
{
    [ServiceBinding(typeof(CombatService))]
    internal class CombatService: IInitializable
    {
        private readonly XMEventService _event;
        private readonly SkillService _skill;
        private readonly StatService _stat;
        private readonly ItemTypeService _itemType;
        private readonly StatusEffectService _statusEffect;
        private readonly DBService _db;
        private readonly AbilityService _ability;
        private readonly SpellService _spell;
        private readonly BeastService _beast;

        public CombatService(
            SkillService skill,
            XMEventService @event,
            StatService stat,
            ItemTypeService itemType,
            StatusEffectService statusEffect,
            DBService db,
            AbilityService ability,
            SpellService spell,
            BeastService beast)
        {
            _skill = skill;
            _event = @event;
            _stat = stat;
            _itemType = itemType;
            _statusEffect = statusEffect;
            _db = db;
            _ability = ability;
            _spell = spell;
            _beast = beast;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<NWNXEvent.OnBroadcastAttackOfOpportunityBefore>(DisableAttacksOfOpportunity);
            _event.Subscribe<PlayerEvent.OnDamaged>(RemoveEffectsOnDamaged);
            _event.Subscribe<CreatureEvent.OnDamaged>(RemoveEffectsOnDamaged);
            _event.Subscribe<StatEvent.PassiveTPBonusAcquiredEvent>(ApplyPassiveTPBonus);
            _event.Subscribe<StatEvent.PassiveTPBonusRemovedEvent>(RemovePassiveTPBonus);
            _event.Subscribe<XMEvent.OnDamageDealt>(OnDamageDealt);
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

        private void DisableAttacksOfOpportunity(uint objectSelf)
        {
            EventsPlugin.SkipEvent();
        }

        internal int CalculateHitRate(
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

        private int CalculateCriticalDelta(
            int attackerStat, 
            int defenderStat)
        {
            var delta = attackerStat - defenderStat;

            if (delta < 0)
                delta = 0;
            else if (delta > 15)
                delta = 15;

            return delta;
        }

        private int CalculateAccuracy(
            uint attacker, 
            uint defender, 
            AttackType attackType, 
            CombatModeType combatMode)
        {
            var bonus = _stat.GetAccuracy(attacker);
            var perception = _stat.GetAttribute(attacker, AbilityType.Perception);
            var attackerLevel = _stat.GetLevel(attacker);
            var defenderLevel = _stat.GetLevel(defender);
            var levelDelta = attackerLevel - defenderLevel;
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

            modifiers += levelDelta * 4;

            return perception * 3 + attackerLevel + bonus + modifiers;
        }

        private int CalculateEvasion(uint creature)
        {
            var agility = _stat.GetAttribute(creature, AbilityType.Agility);
            var baseEvasion = _skill.GetEvasionSkill(creature) / 10;
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
            var hasShield = _itemType.IsShield(offHand);
            var deflection = 0;

            if (hasShield)
            {
                deflection += 10 + _stat.GetShieldDeflection(defender);
            }
            else
            {
                deflection += _stat.GetAttackDeflection(defender);
            }

            return deflection;
        }

        internal (HitResultType, int) DetermineHitType(
            uint attacker, 
            uint defender, 
            AttackType attackType, 
            CombatModeType combatMode)
        {
            if (_statusEffect.HasEffect<PerfectDodgeStatusEffect>(defender))
            {
                return (HitResultType.Miss, 0);
            }

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

            if (_statusEffect.HasEffect<MightyStrikesStatusEffect>(attacker))
                return true;

            var attackerStat = attackType == AttackType.Melee
                ? _stat.GetAttribute(attacker, AbilityType.Perception)
                : _stat.GetAttribute(attacker, AbilityType.Agility);
            var defenderStat = _stat.GetAttribute(defender, AbilityType.Agility);
            var roll = XMRandom.D100(1);
            var criticalRate = _stat.GetCriticalRate(attacker);
            var criticalDelta = CalculateCriticalDelta(attackerStat, defenderStat);
            criticalRate += criticalDelta;

            if (criticalRate > 35)
                criticalRate = 35;

            return roll <= criticalRate;
        }

        private bool DetermineAttackDeflection(uint defender)
        {
            var roll = XMRandom.D100(1);
            var deflectionChance = CalculateDeflectionChance(defender);

            return roll <= deflectionChance;
        }

        internal string BuildCombatLogMessage(
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

        private int CalculateAttack(uint attacker, uint weapon, AttackType attackType)
        {
            var attack = _stat.GetAttack(attacker);
            var stat = attackType == AttackType.Melee
                ? _stat.GetAttribute(attacker, AbilityType.Might)
                : _stat.GetAttribute(attacker, AbilityType.Agility);
            var level = _stat.GetLevel(attacker);
            var skillType = _skill.GetSkillOfWeapon(weapon);
            var skillLevel = _skill.GetSkillLevel(attacker, skillType) / 10;

            return 8 + (2 * level) + stat + (attack + skillLevel);
        }

        private int CalculateDefense(uint defender)
        {
            var defense = _stat.GetDefense(defender);
            var stat = _stat.GetAttribute(defender, AbilityType.Vitality);
            var level = _stat.GetLevel(defender);

            return (int)(8 + (stat * 1.5f) + level + defense);
        }

        private float CalculateDamageRatio(uint attacker, uint defender, uint weapon, AttackType attackType)
        {
            const float RatioMax = 3.625f;
            const float RatioMin = 0.01f;

            var attackerAttack = CalculateAttack(attacker, weapon, attackType);
            var defenderDefense = CalculateDefense(defender);

            var defenseModifier = 1 + _stat.GetDefenseModifier(defender);
            defenderDefense = (int)(defenderDefense * defenseModifier);

            var defenseBypassModifier = 1 - _stat.GetDefenseBypassModifier(attacker);
            defenderDefense = (int)(defenderDefense * defenseBypassModifier);

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

        private (int, int) CalculateDamageRange(uint attacker, uint defender, uint weapon, AttackType attackType)
        {
            var delta = CalculateDamageStatDelta(attacker, defender, attackType);
            var ratio = CalculateDamageRatio(attacker, defender, weapon, attackType);
            var attackerDMG = _stat.GetMainHandDMG(attacker) + _stat.GetOffHandDMG(attacker);
            var backAttackDMG = CalculateBackAttackBonus(attacker, defender);
            var queuedDMG = CalculateQueuedAbilityDamageBonus(attacker, defender);
            var baseDMG = attackerDMG + delta + backAttackDMG + queuedDMG;
            var maxDamage = baseDMG * ratio;
            var minDamage = maxDamage * 0.7f;

            return ((int)minDamage, (int)maxDamage);
        }

        private int CalculateBackAttackBonus(uint attacker, uint defender)
        {
            var isBehind = IsBehind(attacker, defender);
            if (!isBehind)
                return 0;

            var bonusDMG = 0;
            if (GetHasFeat(FeatType.BackAttack4, attacker))
                bonusDMG += 16;
            else if (GetHasFeat(FeatType.BackAttack3, attacker))
                bonusDMG += 12;
            else if (GetHasFeat(FeatType.BackAttack2, attacker))
                bonusDMG += 8;
            else if (GetHasFeat(FeatType.BackAttack1, attacker))
                bonusDMG += 4;

            if (_statusEffect.HasEffect<SneakAttack3StatusEffect>(attacker))
            {
                bonusDMG += 30;
                _statusEffect.RemoveStatusEffect<SneakAttack3StatusEffect>(attacker);
            }

            if (_statusEffect.HasEffect<SneakAttack2StatusEffect>(attacker))
            {
                bonusDMG += 20;
                _statusEffect.RemoveStatusEffect<SneakAttack2StatusEffect>(attacker);
            }

            if (_statusEffect.HasEffect<SneakAttack1StatusEffect>(attacker))
            {
                bonusDMG += 10;
                _statusEffect.RemoveStatusEffect<SneakAttack1StatusEffect>(attacker);
            }

            return bonusDMG;
        }

        private int CalculateQueuedAbilityDamageBonus(uint attacker, uint defender)
        {
            var ability = _ability.GetQueuedAbility(attacker);
            if (ability == null)
                return 0;

            var dmg = ability.Stats[StatType.QueuedDMGBonus];

            if (ability.ResistType != ResistType.Invalid)
            {
                var resist = _spell.CalculateResistDamageReduction(defender, ability.ResistType);
                dmg = (int)(dmg * resist);
            }

            if (dmg <= 0)
                dmg = 0;

            return dmg;
        }

        internal int DetermineDamage(
            uint attacker, 
            uint defender,
            uint weapon,
            AttackType attackType,
            HitResultType hitResult)
        {
            var (minDamage, maxDamage) = CalculateDamageRange(attacker, defender, weapon, attackType);
            var damage = XMRandom.Next(minDamage, maxDamage);

            if (hitResult == HitResultType.Critical)
            {
                damage += (int)(damage * 0.25f);
            }

            damage -= (int)(damage * (_stat.GetDamageReduction(defender) * 0.01f));

            // Eagle Eye Shot - Multiply Damage by 5
            if (_statusEffect.HasEffect<EagleEyeShotStatusEffect>(attacker))
            {
                damage *= 5;
                _statusEffect.RemoveStatusEffect<EagleEyeShotStatusEffect>(attacker);
            }

            var halfDamage = damage / 2;
            // Ether Wall - 50% of damage applied to EP. If no EP is remaining, remove effect. Remainder is normal damage.
            if (_statusEffect.HasEffect<EtherWallStatusEffect>(defender))
            {
                var ep = _stat.GetCurrentEP(defender);
                if (ep < halfDamage)
                {
                    damage -= ep;
                    _stat.ReduceEP(defender, ep);
                    _statusEffect.RemoveStatusEffect<EtherWallStatusEffect>(defender);
                }
                else if (ep > halfDamage)
                {
                    _stat.ReduceEP(defender, halfDamage);
                    damage -= halfDamage;
                }
                else if (ep == halfDamage)
                {
                    _stat.ReduceEP(defender, halfDamage);
                    damage -= halfDamage;
                    _statusEffect.RemoveStatusEffect<EtherWallStatusEffect>(defender);
                }
            }

            return damage;
        }

        internal int CalculateAttackDelay(uint attacker)
        {
            if (_statusEffect.HasEffect<HundredFistsStatusEffect>(attacker))
                return 1;

            var weapon = GetItemInSlot(InventorySlotType.RightHand, attacker);
            var skillType = _skill.GetSkillOfWeapon(weapon);

            float delay;
            if (GetIsPC(attacker) && !GetIsDMPossessed(attacker))
            {
                var playerId = PlayerId.Get(attacker);
                var dbPlayerCombat = _db.Get<PlayerStat>(playerId);
                delay = dbPlayerCombat.EquippedItemStats[InventorySlotType.RightHand].Delay +
                        dbPlayerCombat.EquippedItemStats[InventorySlotType.LeftHand].Delay;
            }
            else
            {
                var npcStats = _stat.GetNPCStats(attacker);
                delay = npcStats.MainHandDelay + npcStats.OffHandDelay;
            }

            var delayPercentAdjustment = 0f;

            if (skillType == SkillType.Bow && 
                GetHasFeat(FeatType.Barrage, attacker))
            {
                delayPercentAdjustment = -0.2f;
            }
            else if (skillType == SkillType.HandToHand && 
                     GetHasFeat(FeatType.MartialArts, attacker))
            {
                delayPercentAdjustment = -0.2f;
            }

            var haste = _stat.GetHaste(attacker) * 0.01f;
            delayPercentAdjustment -= haste;

            if (delayPercentAdjustment < -0.5f)
                delayPercentAdjustment = -0.5f;

            var finalDelay = (int)(delay / 60f * 1000);
            return (int)(finalDelay + finalDelay * delayPercentAdjustment);
        }

        internal int CalculateTPGainPlayer(uint player, bool useSubtleBlow)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId);
            var main = dbPlayerStat.EquippedItemStats[InventorySlotType.RightHand];
            var off = dbPlayerStat.EquippedItemStats[InventorySlotType.LeftHand];
            var mainDelay = main.Delay;
            var offDelay = off.Delay;
            var totalDelay = mainDelay + offDelay;
            var equipmentBonus = _stat.GetTPGain(player);

            if (main.IsEquipped && off.IsEquipped)
                totalDelay /= 2;

            int tpGain;
            if (totalDelay <= 180)
                tpGain = 61 + ((totalDelay - 180) * 63 / 360);
            else if (totalDelay <= 540)
                tpGain = 61 + ((totalDelay - 180) * 88 / 360);
            else if (totalDelay <= 630)
                tpGain = 149 + ((totalDelay - 540) * 22 / 360);
            else if (totalDelay <= 720)
                tpGain = 154 + ((totalDelay - 630) * 28 / 360);
            else if (totalDelay <= 900)
                tpGain = 161 + ((totalDelay - 720) * 24 / 360);
            else
                tpGain = 173 + ((totalDelay - 900) * 28 / 360);

            var totalTP = dbPlayerStat.TP + tpGain + equipmentBonus;

            if (useSubtleBlow)
            {
                var subtleBlow = _stat.GetSubtleBlow(player) * 0.01f;
                if (subtleBlow > 0.75f)
                    subtleBlow = 0.75f;

                totalTP -= (int)(subtleBlow * totalTP);
            }

            return totalTP;
        }

        internal int CalculateTPGainNPC(uint npc, bool useSubtleBlow)
        {
            var npcStats = _stat.GetNPCStats(npc);
            var totalDelay = npcStats.MainHandDelay + npcStats.OffHandDelay;

            if (npcStats.MainHandDelay > 0 && npcStats.OffHandDelay > 0)
                totalDelay /= 2;

            int tpGain;

            if (totalDelay <= 180)
                tpGain = 80 + ((totalDelay - 180) * 15) / 180;
            else if (totalDelay <= 450)
                tpGain = 80 + ((totalDelay - 180) * 65) / 270;
            else if (totalDelay <= 480)
                tpGain = 145 + ((totalDelay - 250) * 15) / 30;
            else if (totalDelay <= 530)
                tpGain = 160 + ((totalDelay - 480) * 15) / 30;
            else 
                tpGain = 175 + ((totalDelay - 530) * 35) / 470;

            var totalTP = _stat.GetCurrentTP(npc) + tpGain;

            if (useSubtleBlow)
            {
                var subtleBlow = _stat.GetSubtleBlow(npc) * 0.01f;
                if (subtleBlow > 0.75f)
                    subtleBlow = 0.75f;

                totalTP -= (int)(subtleBlow * totalTP);
            }

            return totalTP;
        }

        internal void UpdateTP(uint target, int tp)
        {
            _stat.SetTP(target, tp);
        }

        private void ApplyPassiveTPBonus(uint creature)
        {
            var weapon = GetItemInSlot(InventorySlotType.RightHand, creature);
            var skill = _skill.GetSkillOfWeapon(weapon);

            if (skill == SkillType.Invalid)
                return;

            var definition = _skill.GetSkillDefinition(skill);
            if (!_ability.IsFeatRegistered(definition.PassiveFeat))
                return;

            if (!GetHasFeat(definition.PassiveFeat, creature))
                return;

            var ability = _ability.GetAbilityDetail(definition.PassiveFeat);
            _statusEffect.ApplyPermanentStatusEffect(ability.PassiveWeaponSkillStatusEffectType, creature, creature);
        }
        private void RemovePassiveTPBonus(uint creature)
        {
            _statusEffect.RemoveStatusEffectBySourceType(creature, StatusEffectSourceType.WeaponSkill);
        }

        private void RemoveEffectsOnDamaged(uint creature)
        {
            _statusEffect.RemoveStatusEffect<ThirdEyeStatusEffect>(creature);
            _statusEffect.RemoveStatusEffect<HideStatusEffect>(creature);
        }

        private bool IsBehind(uint attacker, uint defender)
        {
            if (_statusEffect.HasEffect<HideStatusEffect>(attacker))
            {
                _statusEffect.RemoveStatusEffect<HideStatusEffect>(attacker);
                return true;
            }

            var attackerPosition = GetPosition(attacker);
            var defenderPosition = GetPosition(defender);
            var defenderFacing = GetFacing(defender);

            // Adjust facing to account for NWN's 0.0 being East instead of North
            var defenderDirection = new Vector3(cos(defenderFacing + (float)Math.PI / 2), sin(defenderFacing + (float)Math.PI / 2), 0);

            var toAttacker = Vector3.Normalize(attackerPosition - defenderPosition);

            // Attacker is behind the defender if the direction from the defender to the attacker 
            // is opposite to the defender's facing direction.
            var isBehind = Vector3.Dot(toAttacker, defenderDirection) < -0.5f;

            return isBehind;
        }

        internal bool HandleParalyze(uint attacker)
        {
            if (!_statusEffect.HasEffect<ParalyzeStatusEffect>(attacker))
                return false;

            var hasParalysis = XMRandom.D100(1) <= _stat.GetParalysis(attacker);

            if (hasParalysis)
            {
                Messaging.SendMessageNearbyToPlayers(attacker, LocaleString.XIsParalyzed.ToLocalizedString(GetName(attacker)));
            }

            return hasParalysis;
        }

        private void HandleEtherLink(uint attacker)
        {
            if (!_beast.IsBeast(attacker))
                return;

            if (!GetHasFeat(FeatType.EtherLink, attacker))
                return;

            var npcStats = _stat.GetNPCStats(attacker);

            if (XMRandom.D100(1) <= npcStats.Stats[StatType.EtherLink])
            {
                var owner = GetMaster(attacker);
                _stat.RestoreEP(owner, 5);
            }
        }

        private void HandleRestoreEPOnHit(uint attacker)
        {
            var epRestore = _stat.GetEPRestoreOnHit(attacker);
            if (epRestore <= 0)
                return;

            _stat.RestoreEP(attacker, epRestore);
        }

        private void OnDamageDealt(uint attacker)
        {
            HandleEtherLink(attacker);
            HandleRestoreEPOnHit(attacker);
        }
    }
}
