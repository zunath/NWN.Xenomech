using System;
using System.Collections.Generic;
using Anvil.API;
using Anvil.API.Events;
using Anvil.Services;
using NLog;
using XM.Core.EventManagement.CreatureEvent;
using EventScriptType = XM.API.Constants.EventScriptType;

namespace XM.Core.EventManagement
{
    [ServiceBinding(typeof(PlayerEventRegistrationService))]
    internal class PlayerEventRegistrationService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        [Inject]
        public IList<ICreatureOnHeartbeat> PlayerOnHeartbeatSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnNotice> PlayerOnNoticeSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnSpellCastAt> PlayerOnSpellCastAtSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnMeleeAttacked> PlayerOnMeleeAttackedSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnDamaged> PlayerOnDamagedSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnDisturbed> PlayerOnDisturbedSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnEndCombatRound> PlayerOnEndCombatRoundSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnSpawnIn> PlayerOnSpawnInSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnRested> PlayerOnRestedSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnDeath> PlayerOnDeathSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnUserDefined> PlayerOnUserDefinedSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnBlockedByDoor> PlayerOnBlockedByDoorSubscriptions { get; set; }

        public PlayerEventRegistrationService()
        {
            NwModule.Instance.OnClientEnter += OnClientEnter;
        }

        private void OnClientEnter(ModuleEvents.OnClientEnter obj)
        {
            var player = GetEnteringObject();
            SetPlayerScripts(player);
        }
        private void SetPlayerScripts(uint player)
        {
            if (!GetIsPC(player) || GetIsDM(player))
                return;

            SetEventScript(player, EventScriptType.CreatureOnHeartbeat, EventScript.PlayerOnHeartbeatScript);
            SetEventScript(player, EventScriptType.CreatureOnNotice, EventScript.PlayerOnNoticeScript);
            SetEventScript(player, EventScriptType.CreatureOnSpellCastAt, EventScript.PlayerOnSpellCastAtScript);
            SetEventScript(player, EventScriptType.CreatureOnMeleeAttacked, EventScript.PlayerOnMeleeAttackedScript);
            SetEventScript(player, EventScriptType.CreatureOnDamaged, EventScript.PlayerOnDamagedScript);
            SetEventScript(player, EventScriptType.CreatureOnDisturbed, EventScript.PlayerOnDisturbedScript);
            SetEventScript(player, EventScriptType.CreatureOnEndCombatRound, EventScript.PlayerOnEndCombatRoundScript);
            SetEventScript(player, EventScriptType.CreatureOnSpawnIn, EventScript.PlayerOnSpawnInScript);
            SetEventScript(player, EventScriptType.CreatureOnRested, EventScript.PlayerOnRestedScript);
            SetEventScript(player, EventScriptType.CreatureOnDeath, EventScript.PlayerOnDeathScript);
            SetEventScript(player, EventScriptType.CreatureOnUserDefinedEvent, EventScript.PlayerOnUserDefinedScript);
            SetEventScript(player, EventScriptType.CreatureOnBlockedByDoor, EventScript.PlayerOnBlockedByDoorScript);
        }

        [ScriptHandler(EventScript.PlayerOnHeartbeatScript)]
        public void HandlePlayerOnHeartbeat()
        {
            foreach (var handler in PlayerOnHeartbeatSubscriptions)
            {
                try
                {
                    handler.CreatureOnHeartbeat();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.PlayerOnNoticeScript)]
        public void HandlePlayerOnNotice()
        {
            foreach (var handler in PlayerOnNoticeSubscriptions)
            {
                try
                {
                    handler.CreatureOnNotice();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.PlayerOnSpellCastAtScript)]
        public void HandlePlayerOnSpellCastAt()
        {
            foreach (var handler in PlayerOnSpellCastAtSubscriptions)
            {
                try
                {
                    handler.CreatureOnSpellCastAt();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.PlayerOnMeleeAttackedScript)]
        public void HandlePlayerOnMeleeAttacked()
        {
            foreach (var handler in PlayerOnMeleeAttackedSubscriptions)
            {
                try
                {
                    handler.CreatureOnMeleeAttacked();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.PlayerOnDamagedScript)]
        public void HandlePlayerOnDamaged()
        {
            foreach (var handler in PlayerOnDamagedSubscriptions)
            {
                try
                {
                    handler.CreatureOnDamaged();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.PlayerOnDisturbedScript)]
        public void HandlePlayerOnDisturbed()
        {
            foreach (var handler in PlayerOnDisturbedSubscriptions)
            {
                try
                {
                    handler.CreatureOnDisturbed();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.PlayerOnEndCombatRoundScript)]
        public void HandlePlayerOnEndCombatRound()
        {
            foreach (var handler in PlayerOnEndCombatRoundSubscriptions)
            {
                try
                {
                    handler.CreatureOnEndCombatRound();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.PlayerOnSpawnInScript)]
        public void HandlePlayerOnSpawnIn()
        {
            foreach (var handler in PlayerOnSpawnInSubscriptions)
            {
                try
                {
                    handler.CreatureOnSpawnIn();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.PlayerOnRestedScript)]
        public void HandlePlayerOnRested()
        {
            foreach (var handler in PlayerOnRestedSubscriptions)
            {
                try
                {
                    handler.CreatureOnRested();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.PlayerOnDeathScript)]
        public void HandlePlayerOnDeath()
        {
            foreach (var handler in PlayerOnDeathSubscriptions)
            {
                try
                {
                    handler.CreatureOnDeath();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.PlayerOnUserDefinedScript)]
        public void HandlePlayerOnUserDefined()
        {
            foreach (var handler in PlayerOnUserDefinedSubscriptions)
            {
                try
                {
                    handler.CreatureOnUserDefined();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }

        [ScriptHandler(EventScript.PlayerOnBlockedByDoorScript)]
        public void HandlePlayerOnBlockedByDoor()
        {
            foreach (var handler in PlayerOnBlockedByDoorSubscriptions)
            {
                try
                {
                    handler.CreatureOnBlockedByDoor();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }
    }
}
