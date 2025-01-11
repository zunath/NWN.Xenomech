using System;
using Anvil.Services;
using XM.Progression.Stat.Entity;
using XM.Progression.Stat.Event;
using XM.Shared.API.Constants;
using XM.Shared.API.NWNX.CreaturePlugin;
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

        public StatService(DBService db, XMEventService @event)
        {
            _db = db;
            _event = @event;

            RegisterEvents();
        }

        private void RegisterEvents()
        {
            _event.RegisterEvent<PlayerHPAdjustedEvent>(ProgressionEventScript.OnPlayerHPAdjustedScript);
            _event.RegisterEvent<PlayerEPAdjustedEvent>(ProgressionEventScript.OnPlayerEPAdjustedScript);
        }

        public int GetCurrentHP(uint creature)
        {
            return GetCurrentHitPoints(creature);
        }

        public int GetMaxHP(uint creature)
        {
            return GetMaxHitPoints(creature);
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
            var dbPlayerStat = _db.Get<PlayerStat>(playerId);
            dbPlayerStat.MaxHP += adjustBy;

            SetPlayerMaxHP(player, dbPlayerStat.MaxHP);
            _db.Set(dbPlayerStat);
        }

        public void SetPlayerMaxHP(uint player, int amount)
        {
            const int MaxHPPerLevel = 254;

            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId);
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
                return GetLocalInt(creature, NPCEPStatVariable);
            }
        }

        public void ReduceEP(uint creature, int reduceBy)
        {
            if (reduceBy <= 0) 
                return;

            if (GetIsPC(creature) && !GetIsDM(creature))
            {
                var playerId = PlayerId.Get(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId);

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

            ExecuteScript(ProgressionEventScript.OnPlayerEPAdjustedScript, creature);
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
                var dbPlayerStat = _db.Get<PlayerStat>(playerId);

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

            ExecuteScript(ProgressionEventScript.OnPlayerEPAdjustedScript, creature);
        }

        public void AdjustHPRegen(uint player, int adjustBy)
        {
            // Note: It's possible for HP Regen to drop to a negative number. This is expected to ensure calculations stay in sync.
            // If there are any visual indicators (GUI elements for example) be sure to account for this scenario.
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId);
            dbPlayerStat.HPRegen += adjustBy;
            _db.Set(dbPlayerStat);
        }

        public void AdjustEPRegen(uint player, int adjustBy)
        {
            // Note: It's possible for EP Regen to drop to a negative number. This is expected to ensure calculations stay in sync.
            // If there are any visual indicators (GUI elements for example) be sure to account for this scenario.
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId);
            dbPlayerStat.EPRegen += adjustBy;
            _db.Set(dbPlayerStat);
        }

        public void AdjustDefense(uint player, int adjustBy)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId);
            dbPlayerStat.Defense += adjustBy;
            _db.Set(dbPlayerStat);
        }
        public void AdjustEvasion(uint player, int adjustBy)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId);
            dbPlayerStat.Evasion += adjustBy;
            _db.Set(dbPlayerStat);
        }

        public void AdjustAttack(uint player, int adjustBy)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId);
            dbPlayerStat.Attack += adjustBy;
            _db.Set(dbPlayerStat);
        }
        public void AdjustEtherAttack(uint player, int adjustBy)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId);
            dbPlayerStat.EtherAttack += adjustBy;
            _db.Set(dbPlayerStat);
        }

        public void AdjustAccuracy(uint player, int adjustBy)
        {
            var playerId = PlayerId.Get(player);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId);
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
            var dbPlayerStat = _db.Get<PlayerStat>(playerId);
            dbPlayerStat.MaxEP = amount;

            if (dbPlayerStat.EP > dbPlayerStat.MaxEP)
                dbPlayerStat.EP = dbPlayerStat.MaxEP;

            _db.Set(dbPlayerStat);
        }

        public void SetPlayerAttribute(uint player, AbilityType type, int amount)
        {
            CreaturePlugin.SetRawAbilityScore(player, type, amount);
        }
    }
}
