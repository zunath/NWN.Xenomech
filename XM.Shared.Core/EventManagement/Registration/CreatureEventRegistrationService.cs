using Anvil.Services;
using XM.Shared.API.Constants;

namespace XM.Shared.Core.EventManagement.Registration
{
    [ServiceBinding(typeof(CreatureEventRegistrationService))]
    internal class CreatureEventRegistrationService
    {
        private readonly XMEventService _event;

        public CreatureEventRegistrationService(XMEventService @event)
        {
            _event = @event;
            RegisterEvents();

            @event.Subscribe<XMEvent.OnSpawnCreated>(OnSpawnCreated);
        }

        private void RegisterEvents()
        {
            _event.RegisterEvent<CreatureEvent.OnHeartbeat>(EventScript.CreatureOnHeartbeatScript);
            _event.RegisterEvent<CreatureEvent.OnPerception>(EventScript.CreatureOnNoticeScript);
            _event.RegisterEvent<CreatureEvent.OnSpellCastAt>(EventScript.CreatureOnSpellCastAtScript);
            _event.RegisterEvent<CreatureEvent.OnMeleeAttacked>(EventScript.CreatureOnMeleeAttackedScript);
            _event.RegisterEvent<CreatureEvent.OnDamaged>(EventScript.CreatureOnDamagedScript);
            _event.RegisterEvent<CreatureEvent.OnDisturbed>(EventScript.CreatureOnDisturbedScript);
            _event.RegisterEvent<CreatureEvent.OnEndCombatRound>(EventScript.CreatureOnEndCombatRoundScript);
            _event.RegisterEvent<CreatureEvent.OnSpawn>(EventScript.CreatureOnSpawnInScript);
            _event.RegisterEvent<CreatureEvent.OnRested>(EventScript.CreatureOnRestedScript);
            _event.RegisterEvent<CreatureEvent.OnDeath>(EventScript.CreatureOnDeathScript);
            _event.RegisterEvent<CreatureEvent.OnUserDefined>(EventScript.CreatureOnUserDefinedScript);
            _event.RegisterEvent<CreatureEvent.OnBlockedByDoor>(EventScript.CreatureOnBlockedByDoorScript);
        }

        /// <summary>
        /// Assigns the necessary scripts to the targeted creature.
        /// This is most useful in the spawn system.
        /// </summary>
        /// <param name="spawn">The targeted creature that will have their scripts updated</param>
        private void SetCreatureScripts(uint spawn)
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

        private void OnSpawnCreated(uint objectSelf)
        {
            SetCreatureScripts(objectSelf);
        }
    }
}
