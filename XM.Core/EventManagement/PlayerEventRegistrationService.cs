using System.Collections.Generic;
using Anvil.API;
using Anvil.API.Events;
using Anvil.Services;
using XM.Core.EventManagement.PlayerEvent;
using EventScriptType = XM.API.Constants.EventScriptType;

namespace XM.Core.EventManagement
{
    [ServiceBinding(typeof(PlayerEventRegistrationService))]
    internal class PlayerEventRegistrationService: EventRegistrationServiceBase
    {
        [Inject]
        public IList<IPlayerOnHeartbeatEvent> PlayerOnHeartbeatSubscriptions { get; set; }

        [Inject]
        public IList<IPlayerOnNoticeEvent> PlayerOnNoticeSubscriptions { get; set; }

        [Inject]
        public IList<IPlayerOnSpellCastAtEvent> PlayerOnSpellCastAtSubscriptions { get; set; }

        [Inject]
        public IList<IPlayerOnMeleeAttackedEvent> PlayerOnMeleeAttackedSubscriptions { get; set; }

        [Inject]
        public IList<IPlayerOnDamagedEvent> PlayerOnDamagedSubscriptions { get; set; }

        [Inject]
        public IList<IPlayerOnDisturbedEvent> PlayerOnDisturbedSubscriptions { get; set; }

        [Inject]
        public IList<IPlayerOnEndCombatRoundEvent> PlayerOnEndCombatRoundSubscriptions { get; set; }

        [Inject]
        public IList<IPlayerOnSpawnInEvent> PlayerOnSpawnInSubscriptions { get; set; }

        [Inject]
        public IList<IPlayerOnRestedEvent> PlayerOnRestedSubscriptions { get; set; }

        [Inject]
        public IList<IPlayerOnDeathEvent> PlayerOnDeathSubscriptions { get; set; }

        [Inject]
        public IList<IPlayerOnUserDefinedEvent> PlayerOnUserDefinedSubscriptions { get; set; }

        [Inject]
        public IList<IPlayerOnBlockedByDoorEvent> PlayerOnBlockedByDoorSubscriptions { get; set; }


        [ScriptHandler(EventScript.PlayerOnHeartbeatScript)]
        public void HandlePlayerOnHeartbeat() => HandleEvent(PlayerOnHeartbeatSubscriptions, (subscription) => subscription.PlayerOnHeartbeat());

        [ScriptHandler(EventScript.PlayerOnNoticeScript)]
        public void HandlePlayerOnNotice() => HandleEvent(PlayerOnNoticeSubscriptions, (subscription) => subscription.PlayerOnNotice());

        [ScriptHandler(EventScript.PlayerOnSpellCastAtScript)]
        public void HandlePlayerOnSpellCastAt() => HandleEvent(PlayerOnSpellCastAtSubscriptions, (subscription) => subscription.PlayerOnSpellCastAt());

        [ScriptHandler(EventScript.PlayerOnMeleeAttackedScript)]
        public void HandlePlayerOnMeleeAttacked() => HandleEvent(PlayerOnMeleeAttackedSubscriptions, (subscription) => subscription.PlayerOnMeleeAttacked());

        [ScriptHandler(EventScript.PlayerOnDamagedScript)]
        public void HandlePlayerOnDamaged() => HandleEvent(PlayerOnDamagedSubscriptions, (subscription) => subscription.PlayerOnDamaged());

        [ScriptHandler(EventScript.PlayerOnDisturbedScript)]
        public void HandlePlayerOnDisturbed() => HandleEvent(PlayerOnDisturbedSubscriptions, (subscription) => subscription.PlayerOnDisturbed());

        [ScriptHandler(EventScript.PlayerOnEndCombatRoundScript)]
        public void HandlePlayerOnEndCombatRound() => HandleEvent(PlayerOnEndCombatRoundSubscriptions, (subscription) => subscription.PlayerOnEndCombatRound());

        [ScriptHandler(EventScript.PlayerOnSpawnInScript)]
        public void HandlePlayerOnSpawnIn() => HandleEvent(PlayerOnSpawnInSubscriptions, (subscription) => subscription.PlayerOnSpawnIn());

        [ScriptHandler(EventScript.PlayerOnRestedScript)]
        public void HandlePlayerOnRested() => HandleEvent(PlayerOnRestedSubscriptions, (subscription) => subscription.PlayerOnRested());

        [ScriptHandler(EventScript.PlayerOnDeathScript)]
        public void HandlePlayerOnDeath() => HandleEvent(PlayerOnDeathSubscriptions, (subscription) => subscription.PlayerOnDeath());

        [ScriptHandler(EventScript.PlayerOnUserDefinedScript)]
        public void HandlePlayerOnUserDefined() => HandleEvent(PlayerOnUserDefinedSubscriptions, (subscription) => subscription.PlayerOnUserDefined());

        [ScriptHandler(EventScript.PlayerOnBlockedByDoorScript)]
        public void HandlePlayerOnBlockedByDoor() => HandleEvent(PlayerOnBlockedByDoorSubscriptions, (subscription) => subscription.PlayerOnBlockedByDoor());

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

    }
}
