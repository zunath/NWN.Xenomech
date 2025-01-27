using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using XM.Progression.Event;
using XM.Progression.Job;
using XM.Progression.Job.Entity;
using XM.Progression.Stat.Entity;
using XM.Progression.Stat.ResistDefinition;
using XM.Shared.API.Constants;
using XM.Shared.API.NWNX.CreaturePlugin;
using XM.Shared.API.NWNX.ObjectPlugin;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;
using XM.UI;
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

        private readonly IList<IResistDefinition> _resists;
        private readonly Dictionary<ResistType, IResistDefinition> _resistDefinitions = new();

        public const int MaxTP = 3000;

        public StatService(
            DBService db, 
            XMEventService @event,
            IList<IResistDefinition> resistDefinitions)
        {
            _db = db;
            _event = @event;
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
            _event.Subscribe<CreatureEvent.OnSpawn>(OnSpawnCreature);
            _event.Subscribe<ModuleEvent.OnEquipItem>(OnEquipItem);
            _event.Subscribe<ModuleEvent.OnUnequipItem>(OnUnequipItem);
            _event.Subscribe<ModuleEvent.OnPlayerDeath>(OnPlayerDeath);
            _event.Subscribe<ModuleEvent.OnPlayerLeave>(OnPlayerLeave);
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

        private ItemStat BuildItemStat(uint item)
        {
            var itemStat = new ItemStat();
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
                        itemStat.HP += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.EP:
                        itemStat.EP += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.EPRegen:
                        itemStat.EPRegen += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.AbilityRecastReduction:
                        itemStat.RecastReduction += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.Defense:
                        itemStat.Defense += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.Evasion:
                        itemStat.Evasion += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.Accuracy:
                        itemStat.Accuracy += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.Attack:
                        itemStat.Attack += GetItemPropertyCostTableValue(ip);
                        break;
                    case ItemPropertyType.EtherAttack:
                        itemStat.EtherAttack += GetItemPropertyCostTableValue(ip);
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
                dbPlayerStat.EquippedItemStats[slot] = new ItemStat();
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
            return GetMaxHitPoints(creature);
        }

        public Dictionary<ResistType, IResistDefinition> GetAllResistDefinitions()
        {
            return _resistDefinitions.ToDictionary(x => x.Key, y => y.Value);
        }

        public void SetPlayerMaxHP(uint player, int amount)
        {
            const int MaxHPPerLevel = 254;

            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            dbPlayerStat.MaxHP = amount;
            var nwnLevelCount = GetLevelByPosition(1, player) +
                                GetLevelByPosition(2, player) +
                                GetLevelByPosition(3, player);

            var hpToApply = dbPlayerStat.MaxHP;

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

            _db.Set(dbPlayerStat);
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
            // Players
            if (GetIsPC(creature) && !GetIsDM(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                return dbPlayerStat.MaxEP;
            }
            // NPCs
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.EP;
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

        public int GetHPRegen(uint creature)
        {
            if (GetIsPC(creature) && !GetIsDM(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemHPRegen = dbPlayerStat.EquippedItemStats.CalculateHPRegen();
                var hpRegen = itemHPRegen;

                return hpRegen;
            }
            else
            {
                return 0;
            }
        }
        public int GetEPRegen(uint creature)
        {
            if (GetIsPC(creature) && !GetIsDM(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemEPRegen = dbPlayerStat.EquippedItemStats.CalculateEPRegen();
                var epRegen = itemEPRegen;

                return epRegen;
            }
            else
            {
                return 0;
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
            var itemRecastReduction = dbPlayerStat.EquippedItemStats.CalculateRecastReduction();
            var recastReduction = itemRecastReduction;

            return recastReduction;
        }

        public void SetPlayerMaxEP(uint player, int amount)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            dbPlayerStat.MaxEP = amount;

            if (dbPlayerStat.EP > dbPlayerStat.MaxEP)
                dbPlayerStat.EP = dbPlayerStat.MaxEP;

            _db.Set(dbPlayerStat);
        }

        public int GetAttribute(uint creature, AbilityType type)
        {
            return GetAbilityScore(creature, type);
        }

        public void SetPlayerAttribute(uint player, AbilityType type, int amount)
        {
            CreaturePlugin.SetRawAbilityScore(player, type, amount);
        }

        public int GetAttack(uint creature)
        {
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemAttack = dbPlayerStat.EquippedItemStats.CalculateAttack();
                var attack = itemAttack;

                return attack;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.Attack;
            }
        }
        public int GetEtherAttack(uint creature)
        {
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemEtherAttack = dbPlayerStat.EquippedItemStats.CalculateEtherAttack();
                var etherAttack = itemEtherAttack;

                return etherAttack;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.EtherAttack;
            }
        }

        public int GetAccuracy(uint creature)
        {
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemAccuracy = dbPlayerStat.EquippedItemStats.CalculateAccuracy();
                var accuracy = itemAccuracy;

                return accuracy;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.Accuracy;
            }
        }
        public int GetEvasion(uint creature)
        {
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemEvasion = dbPlayerStat.EquippedItemStats.CalculateEvasion();
                var evasion = itemEvasion;

                return evasion;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.Evasion;
            }
        }
        public int GetDefense(uint creature)
        {
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                var itemDefense = dbPlayerStat.EquippedItemStats.CalculateDefense();
                var defense = itemDefense;

                return defense;
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.Defense;
            }
        }

        public int GetTPGain(uint creature)
        {
            if (!GetIsPC(creature))
                return 0;

            var playerId = PlayerId.Get(creature);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            var itemTPGain = dbPlayerStat.EquippedItemStats.CalculateTPGain();
            var tpGain = itemTPGain;

            return tpGain;
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

        public int GetResist(uint player, ResistType resist)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            var equipmentResist = dbPlayerStat.EquippedItemStats.CalculateResist(resist);
            
            return Math.Clamp(equipmentResist, 0, 100);
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
                    npcStats.Defense = GetItemPropertyCostTableValue(ip);
                }
                else if (type == ItemPropertyType.Attack)
                {
                    npcStats.Attack = GetItemPropertyCostTableValue(ip);
                }
                else if (type == ItemPropertyType.EtherAttack)
                {
                    npcStats.EtherAttack = GetItemPropertyCostTableValue(ip);
                }
                else if (type == ItemPropertyType.Evasion)
                {
                    npcStats.Evasion = GetItemPropertyCostTableValue(ip);
                }
                else if (type == ItemPropertyType.EP)
                {
                    npcStats.EP = GetItemPropertyCostTableValue(ip);
                }
                else if (type == ItemPropertyType.Accuracy)
                {
                    npcStats.Accuracy = GetItemPropertyCostTableValue(ip);
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
    }
}
