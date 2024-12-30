using System.Collections.Generic;
using Anvil.API;
using Anvil.API.Events;
using Anvil.Services;
using XM.Core.EventManagement.CreatureEvent;
using EventScriptType = XM.API.Constants.EventScriptType;

namespace XM.Core.EventManagement
{
    [ServiceBinding(typeof(PlayerEventRegistrationService))]
    internal class PlayerEventRegistrationService: EventRegistrationServiceBase
    {
        [Inject]
        public IList<ICreatureOnHeartbeatBefore> PlayerOnHeartbeatSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnNoticeBefore> PlayerOnNoticeSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnSpellCastAtBefore> PlayerOnSpellCastAtSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnMeleeAttackedBefore> PlayerOnMeleeAttackedSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnDamagedBefore> PlayerOnDamagedSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnDisturbedBefore> PlayerOnDisturbedSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnEndCombatRoundBefore> PlayerOnEndCombatRoundSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnSpawnInBefore> PlayerOnSpawnInSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnRestedBefore> PlayerOnRestedSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnDeathBefore> PlayerOnDeathSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnUserDefinedBefore> PlayerOnUserDefinedSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnBlockedByDoorBefore> PlayerOnBlockedByDoorSubscriptions { get; set; }


        [ScriptHandler(EventScript.PlayerOnHeartbeatScript)]
        public void HandlePlayerOnHeartbeat() => HandleEvent(PlayerOnHeartbeatSubscriptions, (subscription) => subscription.CreatureOnHeartbeatBefore());

        [ScriptHandler(EventScript.PlayerOnNoticeScript)]
        public void HandlePlayerOnNotice() => HandleEvent(PlayerOnNoticeSubscriptions, (subscription) => subscription.CreatureOnNoticeBefore());

        [ScriptHandler(EventScript.PlayerOnSpellCastAtScript)]
        public void HandlePlayerOnSpellCastAt() => HandleEvent(PlayerOnSpellCastAtSubscriptions, (subscription) => subscription.CreatureOnSpellCastAtBefore());

        [ScriptHandler(EventScript.PlayerOnMeleeAttackedScript)]
        public void HandlePlayerOnMeleeAttacked() => HandleEvent(PlayerOnMeleeAttackedSubscriptions, (subscription) => subscription.CreatureOnMeleeAttackedBefore());

        [ScriptHandler(EventScript.PlayerOnDamagedScript)]
        public void HandlePlayerOnDamaged() => HandleEvent(PlayerOnDamagedSubscriptions, (subscription) => subscription.CreatureOnDamagedBefore());

        [ScriptHandler(EventScript.PlayerOnDisturbedScript)]
        public void HandlePlayerOnDisturbed() => HandleEvent(PlayerOnDisturbedSubscriptions, (subscription) => subscription.CreatureOnDisturbedBefore());

        [ScriptHandler(EventScript.PlayerOnEndCombatRoundScript)]
        public void HandlePlayerOnEndCombatRound() => HandleEvent(PlayerOnEndCombatRoundSubscriptions, (subscription) => subscription.CreatureOnEndCombatRoundBefore());

        [ScriptHandler(EventScript.PlayerOnSpawnInScript)]
        public void HandlePlayerOnSpawnIn() => HandleEvent(PlayerOnSpawnInSubscriptions, (subscription) => subscription.CreatureOnSpawnInBefore());

        [ScriptHandler(EventScript.PlayerOnRestedScript)]
        public void HandlePlayerOnRested() => HandleEvent(PlayerOnRestedSubscriptions, (subscription) => subscription.CreatureOnRestedBefore());

        [ScriptHandler(EventScript.PlayerOnDeathScript)]
        public void HandlePlayerOnDeath() => HandleEvent(PlayerOnDeathSubscriptions, (subscription) => subscription.CreatureOnDeathBefore());

        [ScriptHandler(EventScript.PlayerOnUserDefinedScript)]
        public void HandlePlayerOnUserDefined() => HandleEvent(PlayerOnUserDefinedSubscriptions, (subscription) => subscription.CreatureOnUserDefinedBefore());

        [ScriptHandler(EventScript.PlayerOnBlockedByDoorScript)]
        public void HandlePlayerOnBlockedByDoor() => HandleEvent(PlayerOnBlockedByDoorSubscriptions, (subscription) => subscription.CreatureOnBlockedByDoorBefore());

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
