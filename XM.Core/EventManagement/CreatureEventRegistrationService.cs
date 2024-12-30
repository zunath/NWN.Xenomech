using System;
using System.Collections.Generic;
using Anvil.Services;
using NLog;
using XM.API.Constants;
using XM.Core.EventManagement.CreatureEvent;

namespace XM.Core.EventManagement
{
    [ServiceBinding(typeof(CreatureEventRegistrationService))]
    internal class CreatureEventRegistrationService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        [Inject]
        public IList<ICreatureOnHeartbeat> CreatureOnHeartbeatSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnNotice> CreatureOnNoticeSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnSpellCastAt> CreatureOnSpellCastAtSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnMeleeAttacked> CreatureOnMeleeAttackedSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnDamaged> CreatureOnDamagedSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnDisturbed> CreatureOnDisturbedSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnEndCombatRound> CreatureOnEndCombatRoundSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnSpawnIn> CreatureOnSpawnInSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnRested> CreatureOnRestedSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnDeath> CreatureOnDeathSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnUserDefined> CreatureOnUserDefinedSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnBlockedByDoor> CreatureOnBlockedByDoorSubscriptions { get; set; }


        [ScriptHandler(EventScript.CreatureOnHeartbeatScript)]
        public void HandleCreatureOnHeartbeat()
        {
            foreach (var handler in CreatureOnHeartbeatSubscriptions)
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

        [ScriptHandler(EventScript.CreatureOnNoticeScript)]
        public void HandleCreatureOnNotice()
        {
            foreach (var handler in CreatureOnNoticeSubscriptions)
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

        [ScriptHandler(EventScript.CreatureOnSpellCastAtScript)]
        public void HandleCreatureOnSpellCastAt()
        {
            foreach (var handler in CreatureOnSpellCastAtSubscriptions)
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

        [ScriptHandler(EventScript.CreatureOnMeleeAttackedScript)]
        public void HandleCreatureOnMeleeAttacked()
        {
            foreach (var handler in CreatureOnMeleeAttackedSubscriptions)
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

        [ScriptHandler(EventScript.CreatureOnDamagedScript)]
        public void HandleCreatureOnDamaged()
        {
            foreach (var handler in CreatureOnDamagedSubscriptions)
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

        [ScriptHandler(EventScript.CreatureOnDisturbedScript)]
        public void HandleCreatureOnDisturbed()
        {
            foreach (var handler in CreatureOnDisturbedSubscriptions)
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

        [ScriptHandler(EventScript.CreatureOnEndCombatRoundScript)]
        public void HandleCreatureOnEndCombatRound()
        {
            foreach (var handler in CreatureOnEndCombatRoundSubscriptions)
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

        [ScriptHandler(EventScript.CreatureOnSpawnInScript)]
        public void HandleCreatureOnSpawnIn()
        {
            foreach (var handler in CreatureOnSpawnInSubscriptions)
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

        [ScriptHandler(EventScript.CreatureOnRestedScript)]
        public void HandleCreatureOnRested()
        {
            foreach (var handler in CreatureOnRestedSubscriptions)
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

        [ScriptHandler(EventScript.CreatureOnDeathScript)]
        public void HandleCreatureOnDeath()
        {
            foreach (var handler in CreatureOnDeathSubscriptions)
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

        [ScriptHandler(EventScript.CreatureOnUserDefinedScript)]
        public void HandleCreatureOnUserDefined()
        {
            foreach (var handler in CreatureOnUserDefinedSubscriptions)
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

        [ScriptHandler(EventScript.CreatureOnBlockedByDoorScript)]
        public void HandleCreatureOnBlockedByDoor()
        {
            foreach (var handler in CreatureOnBlockedByDoorSubscriptions)
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

        /// <summary>
        /// Assigns the necessary scripts to the targeted creature.
        /// This is most useful in the spawn system.
        /// </summary>
        /// <param name="spawn">The targeted creature that will have their scripts updated</param>
        public void SetCreatureScripts(uint spawn)
        {
            SetEventScript(spawn, EventScriptType.CreatureOnBlockedByDoor, EventScript.CreatureOnBlockedByDoorScript);
            SetEventScript(spawn, EventScriptType.CreatureOnEndCombatRound, EventScript.CreatureOnEndCombatRoundScript);
            SetEventScript(spawn, EventScriptType.CreatureOnDamaged, EventScript.CreatureOnDamagedScript);
            SetEventScript(spawn, EventScriptType.CreatureOnDeath, EventScript.CreatureOnDeathScript);
            SetEventScript(spawn, EventScriptType.CreatureOnDisturbed, EventScript.CreatureOnDisturbedScript);
            SetEventScript(spawn, EventScriptType.CreatureOnHeartbeat, EventScript.CreatureOnHeartbeatScript);
            SetEventScript(spawn, EventScriptType.CreatureOnNotice, EventScript.CreatureOnNoticeScript);
            SetEventScript(spawn, EventScriptType.CreatureOnMeleeAttacked, EventScript.CreatureOnMeleeAttackedScript);
            SetEventScript(spawn, EventScriptType.CreatureOnRested, EventScript.CreatureOnRestedScript);
            SetEventScript(spawn, EventScriptType.CreatureOnSpawnIn, EventScript.CreatureOnSpawnInScript);
            SetEventScript(spawn, EventScriptType.CreatureOnSpellCastAt, EventScript.CreatureOnSpellCastAtScript);
            SetEventScript(spawn, EventScriptType.CreatureOnUserDefinedEvent, EventScript.CreatureOnUserDefinedScript);

        }
    }
}
