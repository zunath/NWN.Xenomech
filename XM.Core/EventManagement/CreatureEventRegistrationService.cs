using Anvil.Services;
using XM.API.Constants;
using XM.Core.EventManagement.CreatureEvent;
using XM.Core.EventManagement.XMEvent;

namespace XM.Core.EventManagement
{
    [ServiceBinding(typeof(CreatureEventRegistrationService))]
    internal class CreatureEventRegistrationService
    {
        private readonly XMEventService _event;

        public CreatureEventRegistrationService(XMEventService @event)
        {
            _event = @event;
            RegisterEvents();

            @event.Subscribe<SpawnCreatedEvent>(OnSpawnCreated);
        }

        private void RegisterEvents()
        {
            _event.RegisterEvent<CreatureOnHeartbeatBeforeEvent>(EventScript.CreatureOnHeartbeatBeforeScript);
            _event.RegisterEvent<CreatureOnPerceptionBeforeEvent>(EventScript.CreatureOnNoticeBeforeScript);
            _event.RegisterEvent<CreatureOnSpellCastAtBeforeEvent>(EventScript.CreatureOnSpellCastAtBeforeScript);
            _event.RegisterEvent<CreatureOnMeleeAttackedBeforeEvent>(EventScript.CreatureOnMeleeAttackedBeforeScript);
            _event.RegisterEvent<CreatureOnDamagedBeforeEvent>(EventScript.CreatureOnDamagedBeforeScript);
            _event.RegisterEvent<CreatureOnDisturbedBeforeEvent>(EventScript.CreatureOnDisturbedBeforeScript);
            _event.RegisterEvent<CreatureOnEndCombatRoundBeforeEvent>(EventScript.CreatureOnEndCombatRoundBeforeScript);
            _event.RegisterEvent<CreatureOnSpawnInBeforeEvent>(EventScript.CreatureOnSpawnInBeforeScript);
            _event.RegisterEvent<CreatureOnRestedBeforeEvent>(EventScript.CreatureOnRestedBeforeScript);
            _event.RegisterEvent<CreatureOnDeathBeforeEvent>(EventScript.CreatureOnDeathBeforeScript);
            _event.RegisterEvent<CreatureOnUserDefinedBeforeEvent>(EventScript.CreatureOnUserDefinedBeforeScript);
            _event.RegisterEvent<CreatureOnBlockedByDoorBeforeEvent>(EventScript.CreatureOnBlockedByDoorBeforeScript);

            _event.RegisterEvent<CreatureOnHeartbeatAfterEvent>(EventScript.CreatureOnHeartbeatAfterScript);
            _event.RegisterEvent<CreatureOnPerceptionAfterEvent>(EventScript.CreatureOnNoticeAfterScript);
            _event.RegisterEvent<CreatureOnSpellCastAtAfter>(EventScript.CreatureOnSpellCastAtAfterScript);
            _event.RegisterEvent<CreatureOnMeleeAttackedAfterEvent>(EventScript.CreatureOnMeleeAttackedAfterScript);
            _event.RegisterEvent<CreatureOnDamagedAfterEvent>(EventScript.CreatureOnDamagedAfterScript);
            _event.RegisterEvent<CreatureOnDisturbedAfterEvent>(EventScript.CreatureOnDisturbedAfterScript);
            _event.RegisterEvent<CreatureOnEndCombatRoundAfterEvent>(EventScript.CreatureOnEndCombatRoundAfterScript);
            _event.RegisterEvent<CreatureOnSpawnInAfterEvent>(EventScript.CreatureOnSpawnInAfterScript);
            _event.RegisterEvent<CreatureOnRestedAfterEvent>(EventScript.CreatureOnRestedAfterScript);
            _event.RegisterEvent<CreatureOnDeathAfterEvent>(EventScript.CreatureOnDeathAfterScript);
            _event.RegisterEvent<CreatureOnUserDefinedAfterEvent>(EventScript.CreatureOnUserDefinedAfterScript);
            _event.RegisterEvent<CreatureOnBlockedByDoorAfterEvent>(EventScript.CreatureOnBlockedByDoorAfterScript);
        }

        /// <summary>
        /// Assigns the necessary scripts to the targeted creature.
        /// This is most useful in the spawn system.
        /// </summary>
        /// <param name="spawn">The targeted creature that will have their scripts updated</param>
        private void SetCreatureScripts(uint spawn)
        {
            const string StandardHeartbeat = "x2_def_heartbeat";
            const string StandardPerception = "x2_def_percept";
            const string StandardSpellCast = "x2_def_spellcast";
            const string StandardAttacked = "x2_def_attacked";
            const string StandardDamaged = "x2_def_ondamage";
            const string StandardDisturbed = "x2_def_ondisturb";
            const string StandardCombatRoundEnd = "x2_def_endcombat";
            const string StandardSpawn = "x2_def_spawn";
            const string StandardRested = "x2_def_rested";
            const string StandardDeath = "x2_def_ondeath";
            const string StandardUserDefined = "x2_def_userdef";
            const string StandardBlocked = "x2_def_onblocked";

            SetEventScript(spawn, EventScriptType.CreatureOnBlockedByDoor, StandardBlocked);
            SetEventScript(spawn, EventScriptType.CreatureOnEndCombatRound, StandardCombatRoundEnd);
            SetEventScript(spawn, EventScriptType.CreatureOnDamaged, StandardDamaged);
            SetEventScript(spawn, EventScriptType.CreatureOnDeath, StandardDeath);
            SetEventScript(spawn, EventScriptType.CreatureOnDisturbed, StandardDisturbed);
            SetEventScript(spawn, EventScriptType.CreatureOnHeartbeat, StandardHeartbeat);
            SetEventScript(spawn, EventScriptType.CreatureOnNotice, StandardPerception);
            SetEventScript(spawn, EventScriptType.CreatureOnMeleeAttacked, StandardAttacked);
            SetEventScript(spawn, EventScriptType.CreatureOnRested, StandardRested);
            SetEventScript(spawn, EventScriptType.CreatureOnSpawnIn, StandardSpawn);
            SetEventScript(spawn, EventScriptType.CreatureOnSpellCastAt, StandardSpellCast);
            SetEventScript(spawn, EventScriptType.CreatureOnUserDefinedEvent, StandardUserDefined);
        }

        private void OnSpawnCreated()
        {
            var spawn = OBJECT_SELF;
            SetCreatureScripts(spawn);
        }
    }
}
