﻿using System;
using Anvil.Services;
using XM.Core.EventManagement;
using XM.Data;
using XM.Progression.Stat.Entity;
using XM.Progression.Stat.Event;

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

        public int GetCurrentEP(uint creature)
        {
            // Players
            if (GetIsPC(creature) && !GetIsDM(creature))
            {
                var playerId = GetObjectUUID(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId);
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
                var playerId = GetObjectUUID(creature);
                var dbPlayerStat = _db.Get<PlayerStat>(playerId);
                return dbPlayerStat.MaxEP;
            }
            // NPCs
            else
            {
                return GetLocalInt(creature, NPCEPStatVariable);
            }
        }

        public int GetAbilityRecastReduction(uint creature)
        {
            if (!GetIsPC(creature) || GetIsDMPossessed(creature))
                throw new Exception($"Only PCs have ability recast reduction.");

            var playerId = GetObjectUUID(creature);
            var dbPlayerStat = _db.Get<PlayerStat>(playerId);
            return dbPlayerStat.AbilityRecastReduction;
        }
    }
}
