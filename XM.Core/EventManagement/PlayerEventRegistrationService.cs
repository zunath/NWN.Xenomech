using System.Collections.Generic;
using Anvil.API;
using Anvil.API.Events;
using Anvil.Services;
using NLog;
using XM.Core.EventManagement.ModuleEvent;
using XM.Core.EventManagement.PlayerEvent;
using EventScriptType = XM.API.Constants.EventScriptType;

namespace XM.Core.EventManagement
{
    [ServiceBinding(typeof(PlayerEventRegistrationService))]
    internal class PlayerEventRegistrationService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly XMEventService _event;

        public PlayerEventRegistrationService(XMEventService @event)
        {
            _event = @event;

            _logger.Info($"Registering player events...");
            RegisterEvents();
            SubscribeEvents();
        }

        private void RegisterEvents()
        {
            _event.RegisterEvent<PlayerOnHeartbeatEvent>(EventScript.PlayerOnHeartbeatScript);
            _event.RegisterEvent<PlayerOnNoticeEvent>(EventScript.PlayerOnNoticeScript);
            _event.RegisterEvent<PlayerOnSpellCastAtEvent>(EventScript.PlayerOnSpellCastAtScript);
            _event.RegisterEvent<PlayerOnMeleeAttackedEvent>(EventScript.PlayerOnMeleeAttackedScript);
            _event.RegisterEvent<PlayerOnDamagedEvent>(EventScript.PlayerOnDamagedScript);
            _event.RegisterEvent<PlayerOnDisturbedEvent>(EventScript.PlayerOnDisturbedScript);
            _event.RegisterEvent<PlayerOnEndCombatRoundEvent>(EventScript.PlayerOnEndCombatRoundScript);
            _event.RegisterEvent<PlayerOnSpawnInEvent>(EventScript.PlayerOnSpawnInScript);
            _event.RegisterEvent<PlayerOnRestedEvent>(EventScript.PlayerOnRestedScript);
            _event.RegisterEvent<PlayerOnDeathEvent>(EventScript.PlayerOnDeathScript);
            _event.RegisterEvent<PlayerOnUserDefinedEvent>(EventScript.PlayerOnUserDefinedScript);
            _event.RegisterEvent<PlayerOnBlockedByDoorEvent>(EventScript.PlayerOnBlockedByDoorScript);
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ModuleOnPlayerEnterEvent>(OnClientEnter);
        }

        private void OnClientEnter()
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

    }
}
