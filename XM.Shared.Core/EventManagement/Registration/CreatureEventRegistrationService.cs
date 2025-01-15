using Anvil.Services;
using XM.Shared.API.Constants;

namespace XM.Shared.Core.EventManagement.Registration
{
    [ServiceBinding(typeof(CreatureEventRegistrationService))]
    internal class CreatureEventRegistrationService
    {
        public CreatureEventRegistrationService(XMEventService @event)
        {
            @event.Subscribe<XMEvent.OnSpawnCreated>(OnSpawnCreated);
        }

        /// <summary>
        /// Assigns the necessary scripts to the targeted creature.
        /// This is most useful in the spawn system.
        /// </summary>
        /// <param name="spawn">The targeted creature that will have their scripts updated</param>
        private void SetCreatureScripts(uint spawn)
        {
            SetEventScript(spawn, EventScriptType.CreatureOnBlockedByDoor, string.Empty);
            SetEventScript(spawn, EventScriptType.CreatureOnEndCombatRound, string.Empty);
            SetEventScript(spawn, EventScriptType.CreatureOnDamaged, string.Empty);
            SetEventScript(spawn, EventScriptType.CreatureOnDeath, string.Empty);
            SetEventScript(spawn, EventScriptType.CreatureOnDisturbed, string.Empty);
            SetEventScript(spawn, EventScriptType.CreatureOnHeartbeat, string.Empty);
            SetEventScript(spawn, EventScriptType.CreatureOnNotice, string.Empty);
            SetEventScript(spawn, EventScriptType.CreatureOnMeleeAttacked, string.Empty);
            SetEventScript(spawn, EventScriptType.CreatureOnRested, string.Empty);
            SetEventScript(spawn, EventScriptType.CreatureOnSpawnIn, string.Empty);
            SetEventScript(spawn, EventScriptType.CreatureOnSpellCastAt, string.Empty);
            SetEventScript(spawn, EventScriptType.CreatureOnUserDefinedEvent, string.Empty);
        }

        private void OnSpawnCreated(uint objectSelf)
        {
            SetCreatureScripts(objectSelf);
        }
    }
}
