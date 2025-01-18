using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using XM.Progression.Job.Entity;
using XM.Progression.Stat.Entity;
using XM.Progression.Stat.Event;
using XM.Progression.Stat.ResistDefinition;
using XM.Shared.API.Constants;
using XM.Shared.API.NWNX.CreaturePlugin;
using XM.Shared.API.NWNX.ObjectPlugin;
using XM.Shared.Core;
using XM.Shared.Core.Data;
using XM.Shared.Core.EventManagement;

namespace XM.Progression.Stat
{
    [ServiceBinding(typeof(StatService))]
    public class StatService
    {
        private readonly DBService _db;
        private const string NPCEPStatVariable = "EP";
        private readonly XMEventService _event;

        private readonly Dictionary<ResistType, IResistDefinition> _resistDefinitions = new()
        {
            { ResistType.Darkness, new DarknessResistDefinition()},
            { ResistType.Earth, new EarthResistDefinition()},
            { ResistType.Fire, new FireResistDefinition()},
            { ResistType.Ice, new IceResistDefinition()},
            { ResistType.Lightning, new LightningResistDefinition()},
            { ResistType.Light, new LightResistDefinition()},
            { ResistType.Mind, new MindResistDefinition()},
            { ResistType.Water, new WaterResistDefinition()},
            { ResistType.Wind, new WindResistDefinition()},
        };

        public StatService(DBService db, XMEventService @event)
        {
            _db = db;
            _event = @event;

            RegisterEvents();
            SubscribeEvents();
        }

        private void RegisterEvents()
        {
            _event.RegisterEvent<PlayerHPAdjustedEvent>(ProgressionEventScript.OnPlayerHPAdjustedScript);
            _event.RegisterEvent<PlayerEPAdjustedEvent>(ProgressionEventScript.OnPlayerEPAdjustedScript);
        }

        private void SubscribeEvents()
        {
            Console.WriteLine($"subscribing creature event stat service");
            _event.Subscribe<CreatureEvent.OnSpawn>(OnSpawnCreature);
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

        /// <summary>
        /// Increases or decreases a player's HP by a specified amount.
        /// There is a cap of 255 HP per NWN level. Players are auto-leveled to 40 by default, so this
        /// gives 255 * 40 = 10,200 HP maximum. If the player's HP would go over this amount, it will be set to 10,200.
        /// </summary>
        /// <param name="player">The player to adjust</param>
        /// <param name="adjustBy">The amount to adjust by.</param>
        public void AdjustPlayerMaxHP(uint player, int adjustBy)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            dbPlayerStat.MaxHP += adjustBy;

            SetPlayerMaxHP(player, dbPlayerStat.MaxHP);
            _db.Set(dbPlayerStat);
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

        public int GetHPRegen(uint creature)
        {
            if (GetIsPC(creature) && !GetIsDM(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
                return dbPlayerStat.HPRegen;
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
                return dbPlayerStat.EPRegen;
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

            _event.ExecuteScript(ProgressionEventScript.OnPlayerEPAdjustedScript, creature);
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

            _event.ExecuteScript(ProgressionEventScript.OnPlayerEPAdjustedScript, creature);
        }

        public void AdjustHPRegen(uint player, int adjustBy)
        {
            // Note: It's possible for HP Regen to drop to a negative number. This is expected to ensure calculations stay in sync.
            // If there are any visual indicators (GUI elements for example) be sure to account for this scenario.
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            dbPlayerStat.HPRegen += adjustBy;
            _db.Set(dbPlayerStat);
        }

        public void AdjustEPRegen(uint player, int adjustBy)
        {
            // Note: It's possible for EP Regen to drop to a negative number. This is expected to ensure calculations stay in sync.
            // If there are any visual indicators (GUI elements for example) be sure to account for this scenario.
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            dbPlayerStat.EPRegen += adjustBy;
            _db.Set(dbPlayerStat);
        }

        public void AdjustDefense(uint player, int adjustBy)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            dbPlayerStat.Defense += adjustBy;
            _db.Set(dbPlayerStat);
        }
        public void AdjustEvasion(uint player, int adjustBy)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            dbPlayerStat.Evasion += adjustBy;
            _db.Set(dbPlayerStat);
        }

        public void AdjustAttack(uint player, int adjustBy)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            dbPlayerStat.Attack += adjustBy;
            _db.Set(dbPlayerStat);
        }
        public void AdjustEtherAttack(uint player, int adjustBy)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            dbPlayerStat.EtherAttack += adjustBy;
            _db.Set(dbPlayerStat);
        }

        public void AdjustAccuracy(uint player, int adjustBy)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            dbPlayerStat.Accuracy += adjustBy;
            _db.Set(dbPlayerStat);
        }

        public int GetAbilityRecastReduction(uint creature)
        {
            if (!GetIsPC(creature) || GetIsDMPossessed(creature))
                throw new Exception($"Only PCs have ability recast reduction.");

            var playerId = PlayerId.Get(creature);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            return dbPlayerStat.AbilityRecastReduction;
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

        public int GetAttribute(uint player, AbilityType type)
        {
            return GetAbilityScore(player, type);
        }

        public void SetPlayerAttribute(uint player, AbilityType type, int amount)
        {
            CreaturePlugin.SetRawAbilityScore(player, type, amount);
        }

        public int GetAttack(uint player)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            
            return dbPlayerStat.Attack;
        }
        public int GetEtherAttack(uint player)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            return dbPlayerStat.EtherAttack;
        }

        public int GetAccuracy(uint player)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            return dbPlayerStat.Accuracy;
        }
        public int GetEvasion(uint player)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            return dbPlayerStat.Evasion;
        }
        public int GetDefense(uint player)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);
            return dbPlayerStat.Defense;
        }
        public int GetMainHandDMG(uint player)
        {
            return 0; // todo
        }
        public int GetOffHandDMG(uint player)
        {
            return 0; // todo
        }

        public int GetResist(uint player, ResistType resist)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId) ?? new PlayerStat(playerId);

            if (!dbPlayerStat.Resists.ContainsKey(resist))
                return 0;

            var value = dbPlayerStat.Resists[resist];
            return Math.Clamp(value, 0, 100);
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
            }

            return npcStats;
        }

        public int GetLevel(uint creature)
        {
            if (GetIsPC(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerJob = _db.Get<PlayerJob>(playerId);
                var activeJob = dbPlayerJob.ActiveJob;

                return dbPlayerJob.JobLevels[activeJob];
            }
            else
            {
                var npcStats = GetNPCStats(creature);
                return npcStats.Level;
            }
        }
    }
}
