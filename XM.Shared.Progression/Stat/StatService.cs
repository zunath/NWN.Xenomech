using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using XM.Progression.Event;
using XM.Progression.Job;
using XM.Progression.Job.Entity;
using XM.Progression.Job.JobDefinition;
using XM.Progression.Stat.Entity;
using XM.Progression.Stat.ResistDefinition;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.API.NWNX.CreaturePlugin;
using XM.Shared.API.NWNX.ObjectPlugin;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;
using XM.UI.Event;

namespace XM.Progression.Stat
{
    [ServiceBinding(typeof(StatService))]
    [ServiceBinding(typeof(IInitializable))]
    public class StatService: IInitializable
    {
        private readonly DBService _db;
        private const string NPCEPStatVariable = "EP";
        private const string NPCTPStatVariable = "TP";
        private readonly XMEventService _event;
        private readonly StatusEffectService _status;

        private readonly IList<IResistDefinition> _resists;
        private readonly Dictionary<ResistType, IResistDefinition> _resistDefinitions = new();

        public const int MaxTP = 3000;

        private readonly Dictionary<GradeType, int> _baseHPByGrade = new()
        {
            { GradeType.A, 19},
            { GradeType.B, 17},
            { GradeType.C, 16},
            { GradeType.D, 14},
            { GradeType.E, 13},
            { GradeType.F, 11},
            { GradeType.G, 10},
        };

        private readonly Dictionary<GradeType, int> _growthHPByGrade = new()
        {
            { GradeType.A, 9},
            { GradeType.B, 8},
            { GradeType.C, 7},
            { GradeType.D, 6},
            { GradeType.E, 5},
            { GradeType.F, 4},
            { GradeType.G, 3},
        };

        private readonly Dictionary<GradeType, int> _bonusHPByGrade = new()
        {
            { GradeType.A, 1},
            { GradeType.B, 1},
            { GradeType.C, 1},
            { GradeType.D, 0},
            { GradeType.E, 0},
            { GradeType.F, 0},
            { GradeType.G, 0},
        };

        private readonly Dictionary<GradeType, int> _baseStatsByGrade = new()
        {
            { GradeType.A, 5},
            { GradeType.B, 4},
            { GradeType.C, 4},
            { GradeType.D, 3},
            { GradeType.E, 3},
            { GradeType.F, 2},
            { GradeType.G, 2},
        };

        private readonly Dictionary<GradeType, int> _baseEPByGrade = new()
        {
            { GradeType.A, 16},
            { GradeType.B, 14},
            { GradeType.C, 12},
            { GradeType.D, 10},
            { GradeType.E, 8},
            { GradeType.F, 6},
            { GradeType.G, 4},
        };

        private readonly Dictionary<GradeType, float> _growthEPByGrade = new()
        {
            { GradeType.A, 6f},
            { GradeType.B, 5f},
            { GradeType.C, 4f},
            { GradeType.D, 3f},
            { GradeType.E, 2f},
            { GradeType.F, 1f},
            { GradeType.G, 0.5f},
        };

        private readonly Dictionary<GradeType, float> _growthStatsByGrade = new()
        {
            { GradeType.A , 0.5f},
            { GradeType.B , 0.45f},
            { GradeType.C , 0.40f},
            { GradeType.D , 0.35f},
            { GradeType.E , 0.30f},
            { GradeType.F , 0.25f},
            { GradeType.G , 0.20f},
        };

        public StatService(
            DBService db, 
            XMEventService @event,
            StatusEffectService status,
            IList<IResistDefinition> resistDefinitions)
        {
            _db = db;
            _event = @event;
            _status = status;
            _resists = resistDefinitions;

            RegisterEvents();
            SubscribeEvents();
        }

        public void Init()
        {
            LoadResistDefinitions();
        }

        private void LoadResistDefinitions()
        {
            foreach (var definition in _resists)
            {
                _resistDefinitions[definition.Type] = definition;
            }
        }

        private void RegisterEvents()
        {
            _event.RegisterEvent<StatEvent.PlayerHPAdjustedEvent>(ProgressionEventScript.OnPlayerHPAdjustedScript);
            _event.RegisterEvent<StatEvent.PlayerEPAdjustedEvent>(ProgressionEventScript.OnPlayerEPAdjustedScript);
            _event.RegisterEvent<StatEvent.PlayerTPAdjustedEvent>(ProgressionEventScript.OnPlayerTPAdjustedScript);
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<XMEvent.OnPCInitialized>(OnInitializePlayer);
            _event.Subscribe<CreatureEvent.OnSpawn>(OnSpawnCreature);
            _event.Subscribe<ModuleEvent.OnEquipItem>(OnEquipItem);
            _event.Subscribe<ModuleEvent.OnUnequipItem>(OnUnequipItem);
            _event.Subscribe<ModuleEvent.OnPlayerDeath>(OnPlayerDeath);
            _event.Subscribe<ModuleEvent.OnPlayerLeave>(OnPlayerLeave);
            _event.Subscribe<JobEvent.PlayerChangedJobEvent>(OnPlayerChangeJob);
            _event.Subscribe<JobEvent.PlayerLeveledUpEvent>(OnPlayerLevelUp);
        }

        private void OnInitializePlayer(uint player)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId);

            dbPlayerStat.BaseStats[StatType.Might] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Might);
            dbPlayerStat.BaseStats[StatType.Perception] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Perception);
            dbPlayerStat.BaseStats[StatType.Vitality] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Vitality);
            dbPlayerStat.BaseStats[StatType.Willpower] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Willpower);
            dbPlayerStat.BaseStats[StatType.Agility] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Agility);
            dbPlayerStat.BaseStats[StatType.Social] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Social);

            _db.Set(dbPlayerStat);
        }

        private void OnSpawnCreature(uint creature)
        {
            LoadNPCStats(creature);
        }

        private void LoadNPCStats(uint creature)
        {
            var skin = GetItemInSlot(InventorySlotType.CreatureArmor, creature);

            var maxHP = 0;
            for (var ip = GetFirstItemProperty(skin); GetIsItemPropertyValid(ip); ip = GetNextItemProperty(skin))
            {
                if (GetItemPropertyType(ip) == ItemPropertyType.NPCHP)
                {
                    maxHP += GetItemPropertyCostTableValue(ip);
                }
            }

            if (maxHP > 30000)
                maxHP = 30000;

            if (maxHP > 0)
            {
                ObjectPlugin.SetMaxHitPoints(creature, maxHP);
                ObjectPlugin.SetCurrentHitPoints(creature, maxHP);
            }

            SetLocalInt(creature, NPCEPStatVariable, GetMaxEP(creature));
        }

        private ItemStatGroup BuildItemStat(uint item)
        {
            var itemStat = new ItemStatGroup();
            for (var ip = GetFirstItemProperty(item); GetIsItemPropertyValid(ip); ip = GetNextItemProperty(item))
            {
                var type = GetItemPropertyType(ip);

                switch (type)
                {
                    case ItemPropertyType.DMG:
                        itemStat.DMG += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.Delay:
                        itemStat.Delay += GetItemPropertyCostTableValue(ip) * 10;
                        break;
                    case ItemPropertyType.HPBonus:
                        itemStat[StatType.MaxHP] += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.EP:
                        itemStat[StatType.MaxEP] += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.EPRegen:
                        itemStat[StatType.EPRegen] += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.AbilityRecastReduction:
                        itemStat[StatType.RecastReduction] += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.Defense:
                        itemStat[StatType.Defense] += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.Evasion:
                        itemStat[StatType.Evasion] += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.Accuracy:
                        itemStat[StatType.Accuracy] += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.Attack:
                        itemStat[StatType.Attack] += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.EtherAttack:
                        itemStat[StatType.EtherAttack] += GetItemPropertyCostTableValue(ip);
                        break;
                }
            }

            return itemStat;
        }

        private void OnEquipItem(uint module)
        {
            UpdateItemStatOnEquip();
        }
        private void UpdateItemStatOnEquip()
        {
            var player = GetPCItemLastEquippedBy();
            var item = GetPCItemLastEquipped();
            var slot = GetPCItemLastEquippedSlot();
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId);

            var itemStat = BuildItemStat(item);
            itemStat.IsEquipped = true;
            dbPlayerStat.EquippedItemStats[slot] = itemStat;

            _db.Set(dbPlayerStat);
        }

        private void OnUnequipItem(uint module)
        {
            UpdateItemStatOnUnequip();
            ClearTPOnUnequipWeapon();
        }

        private void UpdateItemStatOnUnequip()
        {
            var player = GetPCItemLastUnequippedBy();
            var slot = GetPCItemLastUnequippedSlot();
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId);

            if (dbPlayerStat.EquippedItemStats.ContainsKey(slot))
            {
                dbPlayerStat.EquippedItemStats[slot] = new ItemStatGroup();
            }

            _db.Set(dbPlayerStat);
        }

        private void ClearTPOnUnequipWeapon()
        {
            var player = GetPCItemLastUnequippedBy();
            var slot = GetPCItemLastUnequippedSlot();
            if (slot != InventorySlotType.RightHand &&
                slot != InventorySlotType.LeftHand)
                return;

            SetTP(player, 0);
        }

        public int GetCurrentHP(uint creature)
        {
            return GetCurrentHitPoints(creature);
        }

        public int GetMaxHP(uint creature)
        {
            if (GetIsPC(creature))
            {
                const int MaxHPBase = 40;
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId);
                var jobMaxHP = dbPlayerStat.JobStats[StatType.MaxHP];
                var itemMaxHP = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.MaxHP);
                var abilityMaxHP = dbPlayerStat.AbilityStats.CalculateStat(StatType.MaxHP);
                var statusMaxHP = _status.GetCreatureStatusEffects(creature).Stats[StatType.MaxHP];

                return MaxHPBase + jobMaxHP + itemMaxHP + abilityMaxHP + statusMaxHP;
            }
            else
            {
                return GetMaxHitPoints(creature);
            }
        }

        public Dictionary<ResistType, IResistDefinition> GetAllResistDefinitions()
        {
            return _resistDefinitions.ToDictionary(x => x.Key, y => y.Value);
        }

        private void ApplyPlayerMaxHP(uint player, int hpToApply)
        {
            const int MaxHPPerLevel = 254;

            var nwnLevelCount = GetLevelByPosition(1, player) +
                                GetLevelByPosition(2, player) +
                                GetLevelByPosition(3, player);

            // All levels must have at least 1 HP, so apply those right now.
            for (var nwnLevel = 1; nwnLevel <= nwnLevelCount; nwnLevel++)
            {
                hpToApply--;
                CreaturePlugin.SetMaxHitPointsByLevel(player, nwnLevel, 1);
            }

            // It's possible for the MaxHP value to be a negative if builders misuse item properties, etc.
            // Players cannot go under 'nwnLevel' HP, so we apply that first. If our HP to apply is zero, we don't want to
            // do any more logic with HP application.
            if (hpToApply > 0)
            {
                // Apply the remaining HP.
                for (var nwnLevel = 1; nwnLevel <= nwnLevelCount; nwnLevel++)
                {
                    if (hpToApply > MaxHPPerLevel) // Levels can only contain a max of 255 HP
                    {
                        CreaturePlugin.SetMaxHitPointsByLevel(player, nwnLevel, 255);
                        hpToApply -= 254;
                    }
                    else // Remaining value gets set to the level. (<255 hp)
                    {
                        CreaturePlugin.SetMaxHitPointsByLevel(player, nwnLevel, hpToApply + 1);
                        break;
                    }
                }
            }

            // If player's current HP is higher than max, deal the difference in damage to bring them back down to their new maximum.
            var currentHP = GetCurrentHitPoints(player);
            var maxHP = GetMaxHitPoints(player);
            if (currentHP > maxHP)
            {
                SetCurrentHitPoints(player, maxHP);
            }
        }

        public int GetCurrentEP(uint creature)
        {
            // Players
            if (GetIsPC(creature) && !GetIsDM(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                return dbPlayerStat.EP;
            }
            // NPCs
            else
            {
                return GetLocalInt(creature, NPCEPStatVariable);
            }
        }

        public int GetMaxEP(uint creature)
        {
            var statusMaxEP = _status.GetCreatureStatusEffects(creature).Stats[StatType.MaxEP];
            // Players
            if (GetIsPC(creature) && !GetIsDM(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var jobMaxEP = dbPlayerStat.JobStats[StatType.MaxEP];
                var abilityEP = dbPlayerStat.AbilityStats.CalculateStat(StatType.MaxEP);
                var itemEP = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.MaxEP);

                return jobMaxEP + abilityEP + itemEP + statusMaxEP;
            }
            // NPCs
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.Stats[StatType.MaxEP] + statusMaxEP;
            }
        }

        public int GetCurrentTP(uint creature)
        {
            // Players
            if (GetIsPC(creature) && !GetIsDM(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                return dbPlayerStat.TP;
            }
            // NPCs
            else
            {
                return GetLocalInt(creature, NPCTPStatVariable);
            }
        }

        public int GetSubtleBlow(uint creature)
        {
            var statusSubtleBlow = _status.GetCreatureStatusEffects(creature).Stats[StatType.SubtleBlow];
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var abilitySubtleBlow = dbPlayerStat.AbilityStats.CalculateStat(StatType.SubtleBlow);
                var itemSubtleBlow = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.SubtleBlow);

                return abilitySubtleBlow + itemSubtleBlow + statusSubtleBlow;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.Stats[StatType.SubtleBlow] + statusSubtleBlow;
            }
        }
        public int GetCriticalRate(uint creature)
        {
            const int BaseCriticalRate = 5;

            var statusCriticalRate = _status.GetCreatureStatusEffects(creature).Stats[StatType.CriticalRate];
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var abilityCriticalRate = dbPlayerStat.AbilityStats.CalculateStat(StatType.CriticalRate);
                var itemCriticalRate = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.CriticalRate);

                return BaseCriticalRate + abilityCriticalRate + itemCriticalRate + statusCriticalRate;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return BaseCriticalRate + npcStats.Stats[StatType.CriticalRate] + statusCriticalRate;
            }
        }

        public int GetHPRegen(uint creature)
        {
            var statusHPRegen = _status.GetCreatureStatusEffects(creature).Stats[StatType.HPRegen];
            if (GetIsPC(creature) && !GetIsDM(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemHPRegen = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.HPRegen);
                var abilityHPRegen = dbPlayerStat.AbilityStats.CalculateStat(StatType.HPRegen);

                return itemHPRegen + abilityHPRegen + statusHPRegen;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.Stats[StatType.HPRegen] + statusHPRegen;
            }
        }
        public int GetEPRegen(uint creature)
        {
            var statusEPRegen = _status.GetCreatureStatusEffects(creature).Stats[StatType.EPRegen];
            if (GetIsPC(creature) && !GetIsDM(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemEPRegen = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.EPRegen);
                var abilityEPRegen = dbPlayerStat.AbilityStats.CalculateStat(StatType.EPRegen);

                return itemEPRegen + abilityEPRegen + statusEPRegen;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.Stats[StatType.EPRegen] + statusEPRegen;
            }
        }

        public int GetHaste(uint creature)
        {
            var effects = _status.GetCreatureStatusEffects(creature);
            var statusHaste = effects.Stats[StatType.Haste] - effects.Stats[StatType.Slow];
            if (GetIsPC(creature) && !GetIsDM(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemHaste = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.Haste) - dbPlayerStat.EquippedItemStats.CalculateStat(StatType.Slow);
                var abilityHaste = dbPlayerStat.AbilityStats.CalculateStat(StatType.Haste) - dbPlayerStat.AbilityStats.CalculateStat(StatType.Slow);

                return itemHaste + abilityHaste + statusHaste;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.Stats[StatType.Haste] + statusHaste;
            }
        }
        public int GetDamageReduction(uint creature)
        {
            if (GetIsPC(creature) && !GetIsDM(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemDamageReduction = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.DamageReduction);
                var abilityDamageReduction = dbPlayerStat.AbilityStats.CalculateStat(StatType.DamageReduction);
                var statusDamageReduction = _status.GetCreatureStatusEffects(creature).Stats[StatType.DamageReduction];

                return itemDamageReduction + abilityDamageReduction + statusDamageReduction;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.Stats[StatType.DamageReduction];
            }
        }

        public void ReduceEP(uint creature, int reduceBy)
        {
            if (reduceBy <= 0) 
                return;

            if (GetIsPC(creature) && !GetIsDM(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);

                dbPlayerStat.EP -= reduceBy;

                if (dbPlayerStat.EP < 0)
                    dbPlayerStat.EP = 0;

                _db.Set(dbPlayerStat);
            }
            else
            {
                var ep = GetLocalInt(creature, NPCEPStatVariable);
                ep -= reduceBy;
                if (ep < 0)
                    ep = 0;

                SetLocalInt(creature, NPCEPStatVariable, ep);
            }

            _event.PublishEvent<StatEvent.PlayerEPAdjustedEvent>(creature);
        }

        public void RestoreEP(uint creature, int amount)
        {
            if (amount <= 0) 
                return;

            var maxEP = GetMaxEP(creature);

            // Players
            if (GetIsPC(creature) && !GetIsDM(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);

                dbPlayerStat.EP += amount;

                if (dbPlayerStat.EP > maxEP)
                    dbPlayerStat.EP = maxEP;

                _db.Set(dbPlayerStat);
            }
            // NPCs
            else
            {
                var fp = GetLocalInt(creature, NPCEPStatVariable);
                fp += amount;

                if (fp > maxEP)
                    fp = maxEP;

                SetLocalInt(creature, NPCEPStatVariable, fp);
            }

            _event.PublishEvent<StatEvent.PlayerEPAdjustedEvent>(creature);
        }

        public int GetAbilityRecastReduction(uint creature)
        {
            if (!GetIsPC(creature) || GetIsDMPossessed(creature))
                throw new Exception($"Only PCs have ability recast reduction.");

            var playerId = PlayerId.Get(creature);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            var itemRecastReduction = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.RecastReduction);
            var abilityRecastReduction = dbPlayerStat.AbilityStats.CalculateStat(StatType.RecastReduction);
            var statusRecastReduction = _status.GetCreatureStatusEffects(creature).Stats[StatType.RecastReduction];

            return itemRecastReduction + abilityRecastReduction + statusRecastReduction;
        }

        public int GetAttribute(uint creature, AbilityType type)
        {
            var statType = StatType.Invalid;
            switch (type)
            {
                case AbilityType.Might:
                    statType = StatType.Might;
                    break;
                case AbilityType.Willpower:
                    statType = StatType.Willpower;
                    break;
                case AbilityType.Perception:
                    statType = StatType.Perception;
                    break;
                case AbilityType.Vitality:
                    statType = StatType.Vitality;
                    break;
                case AbilityType.Agility:
                    statType = StatType.Agility;
                    break;
                case AbilityType.Social:
                    statType = StatType.Social;
                    break;
            }

            var statusAbilityBonus = _status.GetCreatureStatusEffects(creature).Stats[statType];
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId);

                var baseStat = dbPlayerStat.BaseStats[statType];
                var jobBonus = dbPlayerStat.JobStats[statType];
                var itemBonus = dbPlayerStat.EquippedItemStats.CalculateStat(statType);
                var abilityBonus = dbPlayerStat.AbilityStats.CalculateStat(statType);

                return Math.Clamp(baseStat + jobBonus + itemBonus + abilityBonus + statusAbilityBonus, 3, 127);
            }
            else
            {
                return Math.Clamp(GetAbilityScore(creature, type) + statusAbilityBonus, 3, 127);
            }
        }

        public void SetPlayerAttribute(uint player, AbilityType type, int amount)
        {
            CreaturePlugin.SetRawAbilityScore(player, type, amount);
        }

        public int GetAttack(uint creature)
        {
            var statusAttack = _status.GetCreatureStatusEffects(creature).Stats[StatType.Attack];
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemAttack = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.Attack);
                var abilityAttack = dbPlayerStat.AbilityStats.CalculateStat(StatType.Attack);

                return itemAttack + abilityAttack + statusAttack;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.Stats[StatType.Attack] + statusAttack;
            }
        }
        public int GetEtherAttack(uint creature)
        {
            var statusEtherAttack = _status.GetCreatureStatusEffects(creature).Stats[StatType.EtherAttack];
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemEtherAttack = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.EtherAttack);
                var abilityEtherAttack = dbPlayerStat.AbilityStats.CalculateStat(StatType.EtherAttack);

                return itemEtherAttack + abilityEtherAttack + statusEtherAttack;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.Stats[StatType.EtherAttack] + statusEtherAttack;
            }
        }
        public int GetEtherDefense(uint creature)
        {
            var statusEtherDefense = _status.GetCreatureStatusEffects(creature).Stats[StatType.EtherDefense];
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemEtherDefense = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.EtherDefense);
                var abilityEtherDefense = dbPlayerStat.AbilityStats.CalculateStat(StatType.EtherDefense);

                return itemEtherDefense + abilityEtherDefense + statusEtherDefense;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.Stats[StatType.EtherDefense] + statusEtherDefense;
            }
        }

        public int GetAccuracy(uint creature)
        {
            var statusAccuracy = _status.GetCreatureStatusEffects(creature).Stats[StatType.Accuracy];
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemAccuracy = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.Accuracy);
                var abilityAccuracy = dbPlayerStat.AbilityStats.CalculateStat(StatType.Accuracy);

                return itemAccuracy + abilityAccuracy + statusAccuracy;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.Stats[StatType.Accuracy] + statusAccuracy;
            }
        }
        public int GetEvasion(uint creature)
        {
            var statusEvasion = _status.GetCreatureStatusEffects(creature).Stats[StatType.Evasion];
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemEvasion = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.Evasion);
                var abilityEvasion = dbPlayerStat.AbilityStats.CalculateStat(StatType.Evasion);

                return itemEvasion + abilityEvasion + statusEvasion;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.Stats[StatType.Evasion] + statusEvasion;
            }
        }
        public int GetDefense(uint creature)
        {
            var statusDefense = _status.GetCreatureStatusEffects(creature).Stats[StatType.Defense];
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemDefense = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.Defense);
                var abilityDefense = dbPlayerStat.AbilityStats.CalculateStat(StatType.Defense);

                return itemDefense + abilityDefense + statusDefense;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.Stats[StatType.Defense] + statusDefense;
            }
        }

        public int GetParalysis(uint creature)
        {
            var statusParalysis = _status.GetCreatureStatusEffects(creature).Stats[StatType.Paralysis];
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemParalysis = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.Paralysis);
                var abilityParalysis = dbPlayerStat.AbilityStats.CalculateStat(StatType.Paralysis);

                return Math.Clamp(itemParalysis + abilityParalysis + statusParalysis, 0, 100);
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return Math.Clamp(npcStats.Stats[StatType.Paralysis] + statusParalysis, 0, 100);
            }
        }

        public int GetShieldDeflection(uint creature)
        {
            var statusShieldDeflection = _status.GetCreatureStatusEffects(creature).Stats[StatType.ShieldDeflection];
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemShieldDeflection = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.ShieldDeflection);
                var abilityShieldDeflection = dbPlayerStat.AbilityStats.CalculateStat(StatType.ShieldDeflection);
                
                return itemShieldDeflection + abilityShieldDeflection + statusShieldDeflection;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.Stats[StatType.ShieldDeflection] + statusShieldDeflection;
            }
        }
        public int GetAttackDeflection(uint creature)
        {
            var statusAttackDeflection = _status.GetCreatureStatusEffects(creature).Stats[StatType.AttackDeflection];
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemAttackDeflection = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.AttackDeflection);
                var abilityAttackDeflection = dbPlayerStat.AbilityStats.CalculateStat(StatType.AttackDeflection);

                return itemAttackDeflection + abilityAttackDeflection + statusAttackDeflection;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.Stats[StatType.AttackDeflection] + statusAttackDeflection;
            }
        }

        public int GetTPGain(uint creature)
        {
            if (!GetIsPC(creature))
                return 0;

            var playerId = PlayerId.Get(creature);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            var itemTPGain = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.TPGain);
            var abilityTPGain = dbPlayerStat.AbilityStats.CalculateStat(StatType.TPGain);
            var statusTPGain = _status.GetCreatureStatusEffects(creature).Stats[StatType.TPGain];
            
            return itemTPGain + abilityTPGain + statusTPGain;
        }
        public int GetEnmityAdjustment(uint creature)
        {
            if (!GetIsPC(creature))
                return 0;

            var playerId = PlayerId.Get(creature);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            var itemEnmity = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.Enmity);
            var abilityEnmity = dbPlayerStat.AbilityStats.CalculateStat(StatType.Enmity);
            var statusEnmity = _status.GetCreatureStatusEffects(creature).Stats[StatType.Enmity];

            var enmity = itemEnmity + abilityEnmity + statusEnmity;
            if (enmity > 200)
                enmity = 200;
            else if (enmity < -50)
                enmity = -50;

            return enmity;
        }

        public void SetTP(uint creature, int amount)
        {
            if (amount > MaxTP)
                amount = MaxTP;

            if (GetIsPC(creature) && !GetIsDMPossessed(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId);
                dbPlayerStat.TP = amount;

                _db.Set(dbPlayerStat);

                _event.PublishEvent<UIEvent.UIRefreshEvent>(creature);
            }
            else
            {
                SetLocalInt(creature, NPCTPStatVariable, amount);
            }
        }

        private int GetDMG(uint item)
        {
            if (!GetIsObjectValid(item))
                return 0;

            var dmg = 0;
            for (var ip = GetFirstItemProperty(item); GetIsItemPropertyValid(ip); ip = GetNextItemProperty(item))
            {
                if (GetItemPropertyType(ip) == ItemPropertyType.DMG)
                {
                    dmg += GetItemPropertyCostTableValue(ip);
                }
            }

            return dmg;
        }

        public int GetMainHandDMG(uint creature)
        {
            var item = GetItemInSlot(InventorySlotType.RightHand, creature);
            if (!GetIsObjectValid(item))
                return 3; // Base DMG of 3 for unarmed

            var dmg =  GetDMG(item);
            if (dmg < 1)
                dmg = 1;

            return dmg;
        }

        public int GetOffHandDMG(uint creature)
        {
            var item = GetItemInSlot(InventorySlotType.LeftHand, creature);
            if (!GetIsObjectValid(item))
                return 0;

            return GetDMG(item);
        }

        public int GetResist(uint creature, ResistType resist)
        {
            var statusResist = _status.GetCreatureStatusEffects(creature).Stats.Resists[resist];
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var equipmentResist = dbPlayerStat.EquippedItemStats.CalculateResist(resist);
                var abilityResist = dbPlayerStat.AbilityStats.CalculateResist(resist);
                var resistPercent = equipmentResist + abilityResist + statusResist;

                return Math.Clamp(resistPercent, 0, 100);
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                var resistPercent = npcStats.Stats.Resists[resist] + statusResist;

                return Math.Clamp(resistPercent, 0, 100);
            }
        }

        public NPCStats GetNPCStats(uint npc)
        {
            var npcStats = new NPCStats();

            var skin = GetItemInSlot(InventorySlotType.CreatureArmor, npc);
            if (!GetIsObjectValid(skin))
                return npcStats;

            for (var ip = GetFirstItemProperty(skin); GetIsItemPropertyValid(ip); ip = GetNextItemProperty(skin))
            {
                var type = GetItemPropertyType(ip);
                if (type == ItemPropertyType.NPCLevel)
                {
                    npcStats.Level = GetItemPropertyCostTableValue(ip);
                }
                else if (type == ItemPropertyType.Defense)
                {
                    npcStats.Stats[StatType.Defense] = GetItemPropertyCostTableValue(ip);
                }
                else if (type == ItemPropertyType.Attack)
                {
                    npcStats.Stats[StatType.Attack] = GetItemPropertyCostTableValue(ip);
                }
                else if (type == ItemPropertyType.EtherAttack)
                {
                    npcStats.Stats[StatType.EtherAttack] = GetItemPropertyCostTableValue(ip);
                }
                else if (type == ItemPropertyType.Evasion)
                {
                    npcStats.Stats[StatType.Evasion] = GetItemPropertyCostTableValue(ip);
                }
                else if (type == ItemPropertyType.EP)
                {
                    npcStats.Stats[StatType.MaxEP] = GetItemPropertyCostTableValue(ip);
                }
                else if (type == ItemPropertyType.Accuracy)
                {
                    npcStats.Stats[StatType.Accuracy] = GetItemPropertyCostTableValue(ip);
                }
                else if (type == ItemPropertyType.NPCEvasionGrade)
                {
                    var gradeId = GetItemPropertyCostTableValue(ip);
                    switch (gradeId)
                    {
                        case 1:
                            npcStats.EvasionGrade = GradeType.A;
                            break;
                        case 2:
                            npcStats.EvasionGrade = GradeType.B;
                            break;
                        case 3:
                            npcStats.EvasionGrade = GradeType.C;
                            break;
                        case 4:
                            npcStats.EvasionGrade = GradeType.D;
                            break;
                        case 5:
                            npcStats.EvasionGrade = GradeType.E;
                            break;
                        case 6:
                            npcStats.EvasionGrade = GradeType.F;
                            break;
                    }
                }
            }

            var clawRight = GetItemInSlot(InventorySlotType.CreatureWeaponRight, npc);
            var clawLeft = GetItemInSlot(InventorySlotType.CreatureWeaponLeft, npc);
            var rightHand = GetItemInSlot(InventorySlotType.RightHand, npc);
            var leftHand = GetItemInSlot(InventorySlotType.LeftHand, npc);

            var mainHand = GetIsObjectValid(rightHand)
                ? rightHand
                : clawRight;
            var offHand = GetIsObjectValid(leftHand)
                ? leftHand
                : clawLeft;

            for (var ip = GetFirstItemProperty(mainHand); GetIsItemPropertyValid(ip); ip = GetNextItemProperty(mainHand))
            {
                var type = GetItemPropertyType(ip);
                if (type == ItemPropertyType.Delay)
                {
                    npcStats.MainHandDelay += GetItemPropertyCostTableValue(ip) * 10;
                }
            }

            for (var ip = GetFirstItemProperty(offHand); GetIsItemPropertyValid(ip); ip = GetNextItemProperty(offHand))
            {
                var type = GetItemPropertyType(ip);
                if (type == ItemPropertyType.Delay)
                {
                    npcStats.OffHandDelay += GetItemPropertyCostTableValue(ip) * 10;
                }
            }


            return npcStats;
        }

        public int GetLevel(uint creature)
        {
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerJob = _db.Get<PlayerJob>(playerId) ?? new PlayerJob(playerId);
                var activeJob = dbPlayerJob.ActiveJob;

                return activeJob == JobType.Invalid 
                    ? 1 
                    : dbPlayerJob.JobLevels[activeJob];
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.Level;
            }
        }

        private void OnPlayerDeath(uint module)
        {
            var player = GetLastPlayerDied();
            SetTP(player, 0);
        }

        private void OnPlayerLeave(uint module)
        {
            var player = GetExitingObject();
            SetTP(player, 0);
        }

        private int CalculateJobHP(int level, GradeType grade)
        {
            var hpScale = _growthHPByGrade[grade];
            var hpBase = _baseHPByGrade[grade];
            var hpBonus = _bonusHPByGrade[grade];

            return hpScale * (level - 1) + hpBase + hpBonus * level;
        }

        private int CalculateJobEP(int level, GradeType grade)
        {
            var epScale = _growthEPByGrade[grade];
            var epBase = _baseEPByGrade[grade];

            return (int)(epScale * (level - 1) + epBase);
        }

        private int CalculateJobStat(int level, GradeType grade)
        {
            var statScale = _growthStatsByGrade[grade];
            var statBase = _baseStatsByGrade[grade];

            return (int)(statScale * (level - 1) + statBase);
        }

        private void RecalculateJobStats(uint player, IJobDefinition definition, int level)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId);

            dbPlayerStat.JobStats[StatType.MaxHP] = CalculateJobHP(level, definition.Grades.MaxHP);
            dbPlayerStat.JobStats[StatType.MaxEP] = CalculateJobEP(level, definition.Grades.MaxEP);

            dbPlayerStat.JobStats[StatType.Might] = CalculateJobStat(level, definition.Grades.Might);
            dbPlayerStat.JobStats[StatType.Perception] = CalculateJobStat(level, definition.Grades.Perception);
            dbPlayerStat.JobStats[StatType.Vitality] = CalculateJobStat(level, definition.Grades.Vitality);
            dbPlayerStat.JobStats[StatType.Willpower] = CalculateJobStat(level, definition.Grades.Willpower);
            dbPlayerStat.JobStats[StatType.Agility] = CalculateJobStat(level, definition.Grades.Agility);
            dbPlayerStat.JobStats[StatType.Social] = CalculateJobStat(level, definition.Grades.Social);

            _db.Set(dbPlayerStat);
        }

        private void OnPlayerChangeJob(uint player)
        {
            var data = _event.GetEventData<JobEvent.PlayerChangedJobEvent>();
            RecalculateJobStats(player, data.Definition, data.Level);
            ApplyStats(player);
            SetTP(player, 0);
        }

        private void OnPlayerLevelUp(uint player)
        {
            var data = _event.GetEventData<JobEvent.PlayerLeveledUpEvent>();
            RecalculateJobStats(player, data.Definition, data.Level);
            ApplyStats(player);
        }

        public void ApplyStats(uint player)
        {
            if (!GetIsPC(player))
                return;

            var maxHP = GetMaxHP(player);

            var might = GetAttribute(player, AbilityType.Might);
            var perception = GetAttribute(player, AbilityType.Perception);
            var vitality = GetAttribute(player, AbilityType.Vitality);
            var willpower = GetAttribute(player, AbilityType.Willpower);
            var agility = GetAttribute(player, AbilityType.Agility);
            var social = GetAttribute(player, AbilityType.Social);

            ApplyPlayerMaxHP(player, maxHP);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Might, might);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Perception, perception);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Vitality, vitality);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Willpower, willpower);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Agility, agility);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Social, social);

            _event.PublishEvent<UIEvent.UIRefreshEvent>(player);
        }

    }
}
