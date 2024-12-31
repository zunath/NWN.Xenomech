using System.Collections.Generic;
using Anvil.API;
using Anvil.API.Events;
using Anvil.Services;
using NLog;
using EventScriptType = XM.API.Constants.EventScriptType;

namespace XM.Core.EventManagement.Registration
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
            _event.RegisterEvent<PlayerEvent.OnHeartbeat>(EventScript.PlayerOnHeartbeatScript);
            _event.RegisterEvent<PlayerEvent.OnPerception>(EventScript.PlayerOnNoticeScript);
            _event.RegisterEvent<PlayerEvent.OnSpellCastAt>(EventScript.PlayerOnSpellCastAtScript);
            _event.RegisterEvent<PlayerEvent.OnMeleeAttacked>(EventScript.PlayerOnMeleeAttackedScript);
            _event.RegisterEvent<PlayerEvent.OnDamaged>(EventScript.PlayerOnDamagedScript);
            _event.RegisterEvent<PlayerEvent.OnDisturbed>(EventScript.PlayerOnDisturbedScript);
            _event.RegisterEvent<PlayerEvent.OnEndCombatRound>(EventScript.PlayerOnEndCombatRoundScript);
            _event.RegisterEvent<PlayerEvent.OnSpawnIn>(EventScript.PlayerOnSpawnInScript);
            _event.RegisterEvent<PlayerEvent.OnRested>(EventScript.PlayerOnRestedScript);
            _event.RegisterEvent<PlayerEvent.OnDeath>(EventScript.PlayerOnDeathScript);
            _event.RegisterEvent<PlayerEvent.OnUserDefined>(EventScript.PlayerOnUserDefinedScript);
            _event.RegisterEvent<PlayerEvent.OnBlockedByDoor>(EventScript.PlayerOnBlockedByDoorScript);
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ModuleEvent.OnPlayerEnter>(OnClientEnter);
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
