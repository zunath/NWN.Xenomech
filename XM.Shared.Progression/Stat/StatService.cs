using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using XM.Inventory.Event;
using XM.Progression.Event;
using XM.Progression.Job;
using XM.Shared.Core.Entity;
using XM.Progression.Job.JobDefinition;
using XM.Progression.Stat.ResistDefinition;
using XM.Progression.StatusEffect;
using XM.Shared.API.Constants;
using XM.Shared.API.NWNX.CreaturePlugin;
using XM.Shared.API.NWNX.ObjectPlugin;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;
using XM.UI.Event;
using XM.Shared.Progression.Stat;

namespace XM.Progression.Stat
{
    [ServiceBinding(typeof(StatService))]
    [ServiceBinding(typeof(IInitializable))]
    public class StatService: IInitializable
    {
        private const int PassiveWeaponSkillTPThreshold = 2000;

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

        private readonly Dictionary<GradeType, int> _baseDMGByGrade = new()
        {
            { GradeType.A , 10},
            { GradeType.B , 9},
            { GradeType.C , 8},
            { GradeType.D , 7},
            { GradeType.E , 6},
            { GradeType.F , 5},
            { GradeType.G , 4},
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

        private readonly Dictionary<GradeType, float> _growthDMGByGrade = new()
        {
            { GradeType.A , 1.22f},
            { GradeType.B , 1.14f},
            { GradeType.C , 1.06f},
            { GradeType.D , 0.98f},
            { GradeType.E , 0.9f},
            { GradeType.F , 0.82f},
            { GradeType.G , 0.73f},
        };

        private readonly Dictionary<ItemPropertyType, StatType> _itemPropertyToStat = new()
        {
            { ItemPropertyType.HP, StatType.MaxHP},
            { ItemPropertyType.EP, StatType.MaxEP},
            { ItemPropertyType.HPRegen, StatType.HPRegen},
            { ItemPropertyType.EPRegen, StatType.EPRegen},
            { ItemPropertyType.AbilityRecastReduction, StatType.RecastReduction},
            { ItemPropertyType.Defense, StatType.Defense},
            { ItemPropertyType.Evasion, StatType.Evasion},
            { ItemPropertyType.Accuracy, StatType.Accuracy},
            { ItemPropertyType.Attack, StatType.Attack},
            { ItemPropertyType.EtherAttack, StatType.EtherAttack},
            { ItemPropertyType.TPGain, StatType.TPGain},
            { ItemPropertyType.Might, StatType.Might},
            { ItemPropertyType.Perception, StatType.Perception},
            { ItemPropertyType.Vitality, StatType.Vitality},
            { ItemPropertyType.Agility, StatType.Agility},
            { ItemPropertyType.Willpower, StatType.Willpower},
            { ItemPropertyType.Social, StatType.Social},
            { ItemPropertyType.ShieldDeflection, StatType.ShieldDeflection},
            { ItemPropertyType.AttackDeflection, StatType.AttackDeflection},
            { ItemPropertyType.SubtleBlow, StatType.SubtleBlow},
            { ItemPropertyType.CriticalRate, StatType.CriticalRate},
            { ItemPropertyType.Enmity, StatType.Enmity},
            { ItemPropertyType.Haste, StatType.Haste},
            { ItemPropertyType.Slow, StatType.Slow},
            { ItemPropertyType.DamageReduction, StatType.DamageReduction},
            { ItemPropertyType.EtherDefense, StatType.EtherDefense},
            { ItemPropertyType.EtherLink, StatType.EtherLink},
            { ItemPropertyType.AccuracyModifier, StatType.AccuracyModifier},
            { ItemPropertyType.TPGainModifier, StatType.TPGainModifier},
            { ItemPropertyType.RecastReductionModifier, StatType.RecastReductionModifier},
            { ItemPropertyType.TPCostModifier, StatType.TPCostModifier},
            { ItemPropertyType.DefenseBypassModifier, StatType.DefenseBypassModifier},
            { ItemPropertyType.HealingModifier, StatType.HealingModifier},
            { ItemPropertyType.EPRestoreOnHit, StatType.EPRestoreOnHit},
            { ItemPropertyType.DefenseModifier, StatType.DefenseModifier},
            { ItemPropertyType.EtherDefenseModifier, StatType.EtherDefenseModifier},
            { ItemPropertyType.ExtraAttackModifier, StatType.ExtraAttackModifier},
            { ItemPropertyType.AttackModifier, StatType.AttackModifier},
            { ItemPropertyType.EtherAttackModifier, StatType.EtherAttackModifier},
            { ItemPropertyType.EvasionModifier, StatType.EvasionModifier},
            { ItemPropertyType.XPModifier, StatType.XPModifier},
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
            LoadMappings();
        }

        private void LoadResistDefinitions()
        {
            foreach (var definition in _resists)
            {
                _resistDefinitions[definition.Type] = definition;
            }
        }

        private void LoadMappings()
        {
            foreach (var (ipType, statType) in _itemPropertyToStat)
            {
                _statsToItemProperty[statType] = ipType;
            }
        }

        private void RegisterEvents()
        {
            _event.RegisterEvent<StatEvent.PlayerHPAdjustedEvent>(ProgressionEventScript.PlayerHPAdjustedScript);
            _event.RegisterEvent<StatEvent.PlayerEPAdjustedEvent>(ProgressionEventScript.PlayerEPAdjustedScript);
            _event.RegisterEvent<StatEvent.PlayerTPAdjustedEvent>(ProgressionEventScript.PlayerTPAdjustedScript);
            _event.RegisterEvent<StatEvent.PassiveTPBonusAcquiredEvent>(ProgressionEventScript.PlayerPassiveTPBonusAcquiredScript);
            _event.RegisterEvent<StatEvent.PassiveTPBonusRemovedEvent>(ProgressionEventScript.PlayerPassiveTPBonusRemovedScript);
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
            _event.Subscribe<InventoryEvent.ItemDurabilityChangedEvent>(ItemDurabilityChanged);
        }


        private void OnInitializePlayer(uint player)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId);

            dbPlayerStat.BaseStats.SetStat(StatType.Might, CreaturePlugin.GetRawAbilityScore(player, AbilityType.Might));
            dbPlayerStat.BaseStats.SetStat(StatType.Perception, CreaturePlugin.GetRawAbilityScore(player, AbilityType.Perception));
            dbPlayerStat.BaseStats.SetStat(StatType.Vitality, CreaturePlugin.GetRawAbilityScore(player, AbilityType.Vitality));
            dbPlayerStat.BaseStats.SetStat(StatType.Willpower, CreaturePlugin.GetRawAbilityScore(player, AbilityType.Willpower));
            dbPlayerStat.BaseStats.SetStat(StatType.Agility, CreaturePlugin.GetRawAbilityScore(player, AbilityType.Agility));
            dbPlayerStat.BaseStats.SetStat(StatType.Social, CreaturePlugin.GetRawAbilityScore(player, AbilityType.Social));

            _db.Set(dbPlayerStat);
        }

        private void OnSpawnCreature(uint creature)
        {
            ApplyStats(creature);
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
                    case ItemPropertyType.HP:
                        itemStat.Stats[StatType.MaxHP] += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.EP:
                        itemStat.Stats[StatType.MaxEP] += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.EPRegen:
                        itemStat.Stats[StatType.EPRegen] += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.AbilityRecastReduction:
                        itemStat.Stats[StatType.RecastReduction] += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.Defense:
                        itemStat.Stats[StatType.Defense] += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.Evasion:
                        itemStat.Stats[StatType.Evasion] += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.Accuracy:
                        itemStat.Stats[StatType.Accuracy] += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.Attack:
                        itemStat.Stats[StatType.Attack] += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.EtherAttack:
                        itemStat.Stats[StatType.EtherAttack] += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.Condition:
                        itemStat.Condition = 1f - (GetItemPropertyCostTableValue(ip) * 0.01f);
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
            // Map Progression ItemStatGroup into Core CoreItemStatGroup
            var coreItem = new XM.Shared.Core.Entity.Stat.ItemStatGroup
            {
                IsEquipped = itemStat.IsEquipped,
                DMG = itemStat.DMG,
                Delay = itemStat.Delay,
                Condition = itemStat.Condition
            };
            foreach (var (k, v) in itemStat.Stats)
                coreItem.Stats[(int)k] = v;
            foreach (var (k, v) in itemStat.Resists)
                coreItem.Resists[(int)k] = v;
            dbPlayerStat.EquippedItemStats[slot.GetHashCode()] = coreItem;

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

            if (dbPlayerStat.EquippedItemStats.ContainsKey(slot.GetHashCode()))
            {
                dbPlayerStat.EquippedItemStats[slot.GetHashCode()] = new XM.Shared.Core.Entity.Stat.ItemStatGroup();
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
                var jobMaxHP = dbPlayerStat.JobStats.GetStat(StatType.MaxHP);
                var itemMaxHP = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.MaxHP);
                var abilityMaxHP = dbPlayerStat.AbilityStats.CalculateStat(StatType.MaxHP);
                var statusMaxHP = _status.GetCreatureStatusEffects(creature).StatGroup.Stats[StatType.MaxHP];

                return MaxHPBase + jobMaxHP + itemMaxHP + abilityMaxHP + statusMaxHP;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.StatGroup.Stats[StatType.MaxHP];
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
            var statusMaxEP = _status.GetCreatureStatusEffects(creature).StatGroup.Stats[StatType.MaxEP];
            // Players
            if (GetIsPC(creature) && !GetIsDM(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var jobMaxEP = dbPlayerStat.JobStats.GetStat(StatType.MaxEP);
                var abilityEP = dbPlayerStat.AbilityStats.CalculateStat(StatType.MaxEP);
                var itemEP = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.MaxEP);

                return jobMaxEP + abilityEP + itemEP + statusMaxEP;
            }
            // NPCs
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.StatGroup.Stats[StatType.MaxEP] + statusMaxEP;
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
            var statusSubtleBlow = _status.GetCreatureStatusEffects(creature).StatGroup.Stats[StatType.SubtleBlow];
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
                return npcStats.StatGroup.Stats[StatType.SubtleBlow] + statusSubtleBlow;
            }
        }
        public int GetCriticalRate(uint creature)
        {
            const int BaseCriticalRate = 5;

            var statusCriticalRate = _status.GetCreatureStatusEffects(creature).StatGroup.Stats[StatType.CriticalRate];
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
                return BaseCriticalRate + npcStats.StatGroup.Stats[StatType.CriticalRate] + statusCriticalRate;
            }
        }

        public int GetHPRegen(uint creature)
        {
            var statusHPRegen = _status.GetCreatureStatusEffects(creature).StatGroup.Stats[StatType.HPRegen];
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
                return npcStats.StatGroup.Stats[StatType.HPRegen] + statusHPRegen;
            }
        }
        public int GetEPRegen(uint creature)
        {
            var statusEPRegen = _status.GetCreatureStatusEffects(creature).StatGroup.Stats[StatType.EPRegen];
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
                return npcStats.StatGroup.Stats[StatType.EPRegen] + statusEPRegen;
            }
        }

        public int GetHaste(uint creature)
        {
            var effects = _status.GetCreatureStatusEffects(creature);
            var statusHaste = effects.StatGroup.Stats[StatType.Haste] - effects.StatGroup.Stats[StatType.Slow];
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
                return npcStats.StatGroup.Stats[StatType.Haste] + statusHaste;
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
                var statusDamageReduction = _status.GetCreatureStatusEffects(creature).StatGroup.Stats[StatType.DamageReduction];

                return itemDamageReduction + abilityDamageReduction + statusDamageReduction;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.StatGroup.Stats[StatType.DamageReduction];
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

        public void ReduceTP(uint creature, int reduceBy)
        {
            if (reduceBy <= 0)
                return;

            if (GetIsPC(creature) && !GetIsDM(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);

                dbPlayerStat.TP -= reduceBy;

                if (dbPlayerStat.TP < 0)
                    dbPlayerStat.TP = 0;

                _db.Set(dbPlayerStat);
            }
            else
            {
                var tp = GetLocalInt(creature, NPCTPStatVariable);
                tp -= reduceBy;
                if (tp < 0)
                    tp = 0;

                SetLocalInt(creature, NPCTPStatVariable, tp);
            }

            _event.PublishEvent<StatEvent.PlayerTPAdjustedEvent>(creature);
        }

        public int GetRecastReduction(uint creature)
        {
            var effects = _status.GetCreatureStatusEffects(creature);
            var statusRecastReduction = effects.StatGroup.Stats[StatType.RecastReduction];
            var statusRecastReductionModifier = 1 - effects.StatGroup.Stats[StatType.RecastReductionModifier] * 0.01f;

            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemRecastReduction = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.RecastReduction);
                var abilityRecastReduction = dbPlayerStat.AbilityStats.CalculateStat(StatType.RecastReduction);

                return (int)((itemRecastReduction + abilityRecastReduction + statusRecastReduction) * statusRecastReductionModifier);
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                var recastReduction = npcStats.StatGroup.Stats[StatType.RecastReduction];

                return (int)((recastReduction + statusRecastReduction) * statusRecastReductionModifier);
            }
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

            var statusAbilityBonus = _status.GetCreatureStatusEffects(creature).StatGroup.Stats[statType];
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId);

                var baseStat = dbPlayerStat.BaseStats.GetStat(statType);
                var jobBonus = dbPlayerStat.JobStats.GetStat(statType);
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
            var effects = _status.GetCreatureStatusEffects(creature);
            var statusAttack = effects.StatGroup.Stats[StatType.Attack];
            var statusAttackModifier = 1 + effects.StatGroup.Stats[StatType.AttackModifier] * 0.01f;

            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemAttack = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.Attack);
                var abilityAttack = dbPlayerStat.AbilityStats.CalculateStat(StatType.Attack);

                return (int)((itemAttack + abilityAttack + statusAttack) * statusAttackModifier);
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return (int)((npcStats.StatGroup.Stats[StatType.Attack] + statusAttack) * statusAttackModifier);
            }
        }

        public int GetEtherAttack(uint creature)
        {
            var effects = _status.GetCreatureStatusEffects(creature);
            var statusEtherAttack = effects.StatGroup.Stats[StatType.EtherAttack];
            var statusEtherAttackModifier = 1 + effects.StatGroup.Stats[StatType.EtherAttackModifier] * 0.01f;

            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemEtherAttack = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.EtherAttack);
                var abilityEtherAttack = dbPlayerStat.AbilityStats.CalculateStat(StatType.EtherAttack);

                return (int)((itemEtherAttack + abilityEtherAttack + statusEtherAttack) * statusEtherAttackModifier);
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return (int)((npcStats.StatGroup.Stats[StatType.EtherAttack] + statusEtherAttack) * statusEtherAttackModifier);
            }
        }

        public int GetEtherDefense(uint creature)
        {
            var statusEtherDefense = _status.GetCreatureStatusEffects(creature).StatGroup.Stats[StatType.EtherDefense];
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
                return npcStats.StatGroup.Stats[StatType.EtherDefense] + statusEtherDefense;
            }
        }

        public int GetAccuracy(uint creature)
        {
            var effects = _status.GetCreatureStatusEffects(creature);
            var statusAccuracy = effects.StatGroup.Stats[StatType.Accuracy];
            var statusAccuracyModifier = 1 + effects.StatGroup.Stats[StatType.AccuracyModifier] * 0.01f;

            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemAccuracy = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.Accuracy);
                var abilityAccuracy = dbPlayerStat.AbilityStats.CalculateStat(StatType.Accuracy);

                return (int)((itemAccuracy + abilityAccuracy + statusAccuracy) * statusAccuracyModifier);
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return (int)((npcStats.StatGroup.Stats[StatType.Accuracy] + statusAccuracy) * statusAccuracyModifier);
            }
        }
        public int GetEvasion(uint creature)
        {
            var effects = _status.GetCreatureStatusEffects(creature);
            var statusEvasion = effects.StatGroup.Stats[StatType.Evasion];
            var statusEvasionModifier = 1 + effects.StatGroup.Stats[StatType.EvasionModifier] * 0.01f;

            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemEvasion = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.Evasion);
                var abilityEvasion = dbPlayerStat.AbilityStats.CalculateStat(StatType.Evasion);

                return (int)((itemEvasion + abilityEvasion + statusEvasion) * statusEvasionModifier);
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return (int)((npcStats.StatGroup.Stats[StatType.Evasion] + statusEvasion) * statusEvasionModifier);
            }
        }
        public int GetDefense(uint creature)
        {
            var statusDefense = _status.GetCreatureStatusEffects(creature).StatGroup.Stats[StatType.Defense];
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
                return npcStats.StatGroup.Stats[StatType.Defense] + statusDefense;
            }
        }

        public int GetParalysis(uint creature)
        {
            var statusParalysis = _status.GetCreatureStatusEffects(creature).StatGroup.Stats[StatType.Paralysis];
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
                return Math.Clamp(npcStats.StatGroup.Stats[StatType.Paralysis] + statusParalysis, 0, 100);
            }
        }

        public int GetShieldDeflection(uint creature)
        {
            var statusShieldDeflection = _status.GetCreatureStatusEffects(creature).StatGroup.Stats[StatType.ShieldDeflection];
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
                return npcStats.StatGroup.Stats[StatType.ShieldDeflection] + statusShieldDeflection;
            }
        }
        public int GetAttackDeflection(uint creature)
        {
            var statusAttackDeflection = _status.GetCreatureStatusEffects(creature).StatGroup.Stats[StatType.AttackDeflection];
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
                return npcStats.StatGroup.Stats[StatType.AttackDeflection] + statusAttackDeflection;
            }
        }

        public float GetTPCostModifier(uint creature)
        {
            var effects = _status.GetCreatureStatusEffects(creature);
            var statusTPCostModifier = effects.StatGroup.Stats[StatType.TPCostModifier];

            if (GetIsPC(creature))
            {
                return statusTPCostModifier * 0.01f;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                var tpCostModifier = npcStats.StatGroup.Stats[StatType.TPCostModifier];

                return (tpCostModifier + statusTPCostModifier) * 0.01f;
            }
        }

        public int GetTPGain(uint creature)
        {
            if (!GetIsPC(creature))
                return 0;

            var effects = _status.GetCreatureStatusEffects(creature);
            var playerId = PlayerId.Get(creature);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            var itemTPGain = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.TPGain);
            var abilityTPGain = dbPlayerStat.AbilityStats.CalculateStat(StatType.TPGain);
            var statusTPGain = effects.StatGroup.Stats[StatType.TPGain];
            var statusTPGainModifier = 1 + effects.StatGroup.Stats[StatType.TPGainModifier] * 0.01f;
            
            return (int)((itemTPGain + abilityTPGain + statusTPGain) * statusTPGainModifier);
        }


        public float GetDefenseBypassModifier(uint creature)
        {
            var effects = _status.GetCreatureStatusEffects(creature);
            var statusDefenseBypassModifier = effects.StatGroup.Stats[StatType.DefenseBypassModifier];

            if (GetIsPC(creature))
            {
                return statusDefenseBypassModifier * 0.01f;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                var defenseBypass = npcStats.StatGroup.Stats[StatType.DefenseBypassModifier];

                return (defenseBypass + statusDefenseBypassModifier) * 0.01f;
            }
        }

        public float GetHealingModifier(uint creature)
        {
            var effects = _status.GetCreatureStatusEffects(creature);
            var statusHealingModifier = effects.StatGroup.Stats[StatType.HealingModifier];

            if (GetIsPC(creature))
            {
                return statusHealingModifier * 0.01f;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                var healingModifier = npcStats.StatGroup.Stats[StatType.HealingModifier];

                return (healingModifier + statusHealingModifier) * 0.01f;
            }
        }

        public int GetEPRestoreOnHit(uint creature)
        {
            var effects = _status.GetCreatureStatusEffects(creature);
            var statusEPRestoreOnHit = effects.StatGroup.Stats[StatType.EPRestoreOnHit];

            if (GetIsPC(creature))
            {
                return statusEPRestoreOnHit;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                var epRestoreOnHit = npcStats.StatGroup.Stats[StatType.EPRestoreOnHit];

                return epRestoreOnHit + statusEPRestoreOnHit;
            }
        }

        public float GetDefenseModifier(uint creature)
        {
            var effects = _status.GetCreatureStatusEffects(creature);
            var statusDefenseModifier = effects.StatGroup.Stats[StatType.DefenseModifier];

            if (GetIsPC(creature))
            {
                return statusDefenseModifier * 0.01f;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                var defenseModifier = npcStats.StatGroup.Stats[StatType.DefenseModifier];

                return (defenseModifier + statusDefenseModifier) * 0.01f;
            }
        }
        public float GetEtherDefenseModifier(uint creature)
        {
            var effects = _status.GetCreatureStatusEffects(creature);
            var statusEtherDefenseModifier = effects.StatGroup.Stats[StatType.EtherDefenseModifier];

            if (GetIsPC(creature))
            {
                return statusEtherDefenseModifier * 0.01f;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                var etherDefenseModifier = npcStats.StatGroup.Stats[StatType.EtherDefenseModifier];

                return (etherDefenseModifier + statusEtherDefenseModifier) * 0.01f;
            }
        }
        public int GetExtraAttackModifier(uint creature)
        {
            var effects = _status.GetCreatureStatusEffects(creature);
            var statusExtraAttackModifier = effects.StatGroup.Stats[StatType.ExtraAttackModifier];

            if (GetIsPC(creature))
            {
                return statusExtraAttackModifier;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                var extraAttackModifier = npcStats.StatGroup.Stats[StatType.ExtraAttackModifier];

                return extraAttackModifier + statusExtraAttackModifier;
            }
        }

        public int GetEnmityAdjustment(uint creature)
        {
            if (!GetIsPC(creature))
                return 0;

            var playerId = PlayerId.Get(creature);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            var itemEnmity = dbPlayerStat.EquippedItemStats.CalculateStat(StatType.Enmity);
            var abilityEnmity = dbPlayerStat.AbilityStats.CalculateStat(StatType.Enmity);
            var statusEnmity = _status.GetCreatureStatusEffects(creature).StatGroup.Stats[StatType.Enmity];

            var enmity = itemEnmity + abilityEnmity + statusEnmity;
            if (enmity > 200)
                enmity = 200;
            else if (enmity < -50)
                enmity = -50;

            return enmity;
        }

        public float GetXPModifier(uint creature)
        {
            var effects = _status.GetCreatureStatusEffects(creature);
            var xpModifier = effects.StatGroup.Stats[StatType.XPModifier];

            if (GetIsPC(creature))
            {
                return 1 + xpModifier * 0.01f;
            }
            else
            {
                return 0f;
            }
        }

        public void SetTP(uint creature, int amount)
        {
            if (amount > MaxTP)
                amount = MaxTP;

            if (GetIsPC(creature) && !GetIsDMPossessed(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId);
                var oldTP = dbPlayerStat.TP;
                dbPlayerStat.TP = amount;

                _db.Set(dbPlayerStat);

                _event.PublishEvent<UIEvent.UIRefreshEvent>(creature);

                // Below the threshold before this change but now we're at or above it.
                if (oldTP < PassiveWeaponSkillTPThreshold && 
                    dbPlayerStat.TP >= PassiveWeaponSkillTPThreshold)
                {
                    _event.PublishEvent<StatEvent.PassiveTPBonusAcquiredEvent>(creature);
                }
                // Above the threshold before this change but now we're below it.
                else if (oldTP >= PassiveWeaponSkillTPThreshold &&
                         dbPlayerStat.TP < PassiveWeaponSkillTPThreshold)
                {
                    _event.PublishEvent<StatEvent.PassiveTPBonusRemovedEvent>(creature);
                }
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
            if (GetIsPC(creature))
            {
                var item = GetItemInSlot(InventorySlotType.RightHand, creature);
                if (!GetIsObjectValid(item))
                    return 3; // Base DMG of 3 for unarmed

                var dmg = GetDMG(item);
                if (dmg < 1)
                    dmg = 1;

                return dmg;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.MainHandDMG;
            }
        }

        public int GetOffHandDMG(uint creature)
        {
            if (GetIsPC(creature))
            {
                var item = GetItemInSlot(InventorySlotType.LeftHand, creature);
                if (!GetIsObjectValid(item))
                    return 0;

                return GetDMG(item);
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.OffHandDMG;
            }
        }

        public int GetResist(uint creature, ResistType resist)
        {
            var statusResist = _status.GetCreatureStatusEffects(creature).StatGroup.Resists[resist];
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
                var resistPercent = npcStats.StatGroup.Resists[resist] + statusResist;

                return Math.Clamp(resistPercent, 0, 100);
            }
        }


        private readonly Dictionary<StatType, ItemPropertyType> _statsToItemProperty = new();

        public NPCStats GetNPCStats(uint npc)
        {
            var npcStats = new NPCStats();

            var skin = GetItemInSlot(InventorySlotType.CreatureArmor, npc);
            if (!GetIsObjectValid(skin))
                return npcStats;

            npcStats.StatGroup.Stats[StatType.MaxHP] += GetMaxHitPoints(npc);
            for (var ip = GetFirstItemProperty(skin); GetIsItemPropertyValid(ip); ip = GetNextItemProperty(skin))
            {
                var type = GetItemPropertyType(ip);
                if (type == ItemPropertyType.NPCLevel)
                {
                    npcStats.Level = GetItemPropertyCostTableValue(ip);
                }
                else if (type == ItemPropertyType.NPCEvasionGrade)
                {
                    var gradeId = GetItemPropertyCostTableValue(ip);
                    npcStats.EvasionGrade = (GradeType)gradeId;
                    if (npcStats.EvasionGrade < GradeType.F)
                        npcStats.EvasionGrade = GradeType.F;
                }
                else if(_itemPropertyToStat.ContainsKey(type))
                {
                    var costTable = GetItemPropertyCostTable(ip);
                    var value = GetItemPropertyCostTableValue(ip);

                    // Cost Table ID 42 has positive values between 1-100 
                    // Starting at 101 the values become negative.
                    // We need to adjust for that here.
                    if (costTable == 42 && value > 100)
                    {
                        value = -(value - 100);
                    }

                    npcStats.StatGroup.Stats[_itemPropertyToStat[type]] += value;
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

        private void SetIP(uint item, ItemPropertyType type, int value)
        {
            BiowareXP2.IPSafeAddItemProperty(item, ItemPropertyCustom(type, -1, value), 0f, AddItemPropertyPolicy.ReplaceExisting, false, false);
        }

        public void SetNPCStats(
            uint npc, 
            NPCStats npcStats)
        {
            if (GetIsPC(npc) || GetIsDM(npc))
                return;

            var skin = GetItemInSlot(InventorySlotType.CreatureArmor, npc);
            var clawRight = GetItemInSlot(InventorySlotType.CreatureWeaponRight, npc);
            var clawLeft = GetItemInSlot(InventorySlotType.CreatureWeaponLeft, npc);

            SetIP(skin, ItemPropertyType.NPCLevel, npcStats.Level);

            if (npcStats.MainHandDMG > 0)
            {
                SetIP(clawRight, ItemPropertyType.DMG, npcStats.MainHandDMG);
            }

            if (npcStats.MainHandDelay > 0)
            {
                SetIP(clawRight, ItemPropertyType.Delay, npcStats.MainHandDelay / 10);
            }

            if (npcStats.OffHandDMG > 0)
            {
                SetIP(clawLeft, ItemPropertyType.DMG, npcStats.OffHandDMG);
            }

            if (npcStats.OffHandDelay > 0)
            {
                SetIP(clawLeft, ItemPropertyType.Delay, npcStats.OffHandDelay / 10);
            }

            if (npcStats.EvasionGrade != GradeType.Invalid)
            {
                SetIP(skin, ItemPropertyType.NPCEvasionGrade, (int)npcStats.EvasionGrade);
            }

            foreach (var (stat, ipType) in _statsToItemProperty)
            {
                SetIP(skin, ipType, npcStats.StatGroup.Stats[stat]);
            }

            ApplyStats(npc);
        }

        public int GetLevel(uint creature)
        {
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerJob = _db.Get<PlayerJob>(playerId) ?? new PlayerJob(playerId);
                var activeJob = (JobType)dbPlayerJob.ActiveJobCode;

                return activeJob == JobType.Invalid 
                    ? 1 
                    : dbPlayerJob.JobLevels[(int)activeJob];
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

        internal int CalculateHP(int level, GradeType grade)
        {
            var hpScale = _growthHPByGrade[grade];
            var hpBase = _baseHPByGrade[grade];
            var hpBonus = _bonusHPByGrade[grade];

            return hpScale * (level - 1) + hpBase + hpBonus * level;
        }

        internal int CalculateEP(int level, GradeType grade)
        {
            var epScale = _growthEPByGrade[grade];
            var epBase = _baseEPByGrade[grade];

            return (int)(epScale * (level - 1) + epBase);
        }

        internal int CalculateStat(int level, GradeType grade)
        {
            var statScale = _growthStatsByGrade[grade];
            var statBase = _baseStatsByGrade[grade];

            return (int)(statScale * (level - 1) + statBase);
        }

        internal int CalculateDMG(int level, GradeType grade)
        {
            var dmgScale = _growthDMGByGrade[grade];
            var dmgBase = _baseDMGByGrade[grade];

            return (int)(dmgScale * (level - 1) + dmgBase);
        }

        private void RecalculateJobStats(uint player, IJobDefinition definition, int level)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId);

            dbPlayerStat.JobStats.SetStat(StatType.MaxHP, CalculateHP(level, definition.Grades.MaxHP));
            dbPlayerStat.JobStats.SetStat(StatType.MaxEP, CalculateEP(level, definition.Grades.MaxEP));

            dbPlayerStat.JobStats.SetStat(StatType.Might, CalculateStat(level, definition.Grades.Might));
            dbPlayerStat.JobStats.SetStat(StatType.Perception, CalculateStat(level, definition.Grades.Perception));
            dbPlayerStat.JobStats.SetStat(StatType.Vitality, CalculateStat(level, definition.Grades.Vitality));
            dbPlayerStat.JobStats.SetStat(StatType.Willpower, CalculateStat(level, definition.Grades.Willpower));
            dbPlayerStat.JobStats.SetStat(StatType.Agility, CalculateStat(level, definition.Grades.Agility));
            dbPlayerStat.JobStats.SetStat(StatType.Social, CalculateStat(level, definition.Grades.Social));

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

        private void ApplyStats(uint creature)
        {
            var maxHP = GetMaxHP(creature);

            var might = GetAttribute(creature, AbilityType.Might);
            var perception = GetAttribute(creature, AbilityType.Perception);
            var vitality = GetAttribute(creature, AbilityType.Vitality);
            var willpower = GetAttribute(creature, AbilityType.Willpower);
            var agility = GetAttribute(creature, AbilityType.Agility);
            var social = GetAttribute(creature, AbilityType.Social);

            CreaturePlugin.SetRawAbilityScore(creature, AbilityType.Might, might);
            CreaturePlugin.SetRawAbilityScore(creature, AbilityType.Perception, perception);
            CreaturePlugin.SetRawAbilityScore(creature, AbilityType.Vitality, vitality);
            CreaturePlugin.SetRawAbilityScore(creature, AbilityType.Willpower, willpower);
            CreaturePlugin.SetRawAbilityScore(creature, AbilityType.Agility, agility);
            CreaturePlugin.SetRawAbilityScore(creature, AbilityType.Social, social);

            if (GetIsPC(creature))
            {
                ApplyPlayerMaxHP(creature, maxHP);
                _event.PublishEvent<UIEvent.UIRefreshEvent>(creature);
            }
            else
            {
                if (maxHP > 30000)
                    maxHP = 30000;

                if (maxHP > 0)
                {
                    ObjectPlugin.SetMaxHitPoints(creature, maxHP);
                    ObjectPlugin.SetCurrentHitPoints(creature, maxHP);
                }

                SetLocalInt(creature, NPCEPStatVariable, GetMaxEP(creature));
            }
        }

        private void ItemDurabilityChanged(uint player)
        {
            if (!GetIsPC(player) || GetIsDM(player))
                return;

            var data = _event.GetEventData<InventoryEvent.ItemDurabilityChangedEvent>();
            var slot = data.Slot;

            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId);
            if (!dbPlayerStat.EquippedItemStats.ContainsKey(slot.GetHashCode()))
                return;

            if (dbPlayerStat.EquippedItemStats.ContainsKey(slot.GetHashCode()))
            {
                dbPlayerStat.EquippedItemStats[slot.GetHashCode()].Condition = data.NewCondition;
            }
            _db.Set(dbPlayerStat);
        }

        [ScriptHandler("bread_test6")]
        public void DebugTP()
        {
            var player = GetLastUsedBy();
            var tp = GetCurrentTP(player) + 500;
            SetTP(player, tp);
        }
    }
}
