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
            _event.RegisterEvent<CreatureEvent.OnHeartbeatBefore>(EventScript.CreatureOnHeartbeatBeforeScript);
            _event.RegisterEvent<CreatureEvent.OnPerceptionBefore>(EventScript.CreatureOnNoticeBeforeScript);
            _event.RegisterEvent<CreatureEvent.OnSpellCastAtBefore>(EventScript.CreatureOnSpellCastAtBeforeScript);
            _event.RegisterEvent<CreatureEvent.OnMeleeAttackedBefore>(EventScript.CreatureOnMeleeAttackedBeforeScript);
            _event.RegisterEvent<CreatureEvent.OnDamagedBefore>(EventScript.CreatureOnDamagedBeforeScript);
            _event.RegisterEvent<CreatureEvent.OnDisturbedBefore>(EventScript.CreatureOnDisturbedBeforeScript);
            _event.RegisterEvent<CreatureEvent.OnEndCombatRoundBefore>(EventScript.CreatureOnEndCombatRoundBeforeScript);
            _event.RegisterEvent<CreatureEvent.OnSpawnInBefore>(EventScript.CreatureOnSpawnInBeforeScript);
            _event.RegisterEvent<CreatureEvent.OnRestedBefore>(EventScript.CreatureOnRestedBeforeScript);
            _event.RegisterEvent<CreatureEvent.OnDeathBefore>(EventScript.CreatureOnDeathBeforeScript);
            _event.RegisterEvent<CreatureEvent.OnUserDefinedBefore>(EventScript.CreatureOnUserDefinedBeforeScript);
            _event.RegisterEvent<CreatureEvent.OnBlockedByDoorBefore>(EventScript.CreatureOnBlockedByDoorBeforeScript);

            _event.RegisterEvent<CreatureEvent.OnHeartbeatAfter>(EventScript.CreatureOnHeartbeatAfterScript);
            _event.RegisterEvent<CreatureEvent.OnPerceptionAfter>(EventScript.CreatureOnNoticeAfterScript);
            _event.RegisterEvent<CreatureEvent.OnSpellCastAtAfter>(EventScript.CreatureOnSpellCastAtAfterScript);
            _event.RegisterEvent<CreatureEvent.OnMeleeAttackedAfter>(EventScript.CreatureOnMeleeAttackedAfterScript);
            _event.RegisterEvent<CreatureEvent.OnDamagedAfter>(EventScript.CreatureOnDamagedAfterScript);
            _event.RegisterEvent<CreatureEvent.OnDisturbedAfter>(EventScript.CreatureOnDisturbedAfterScript);
            _event.RegisterEvent<CreatureEvent.OnEndCombatRoundAfter>(EventScript.CreatureOnEndCombatRoundAfterScript);
            _event.RegisterEvent<CreatureEvent.OnSpawnInAfter>(EventScript.CreatureOnSpawnInAfterScript);
            _event.RegisterEvent<CreatureEvent.OnRestedAfter>(EventScript.CreatureOnRestedAfterScript);
            _event.RegisterEvent<CreatureEvent.OnDeathAfter>(EventScript.CreatureOnDeathAfterScript);
            _event.RegisterEvent<CreatureEvent.OnUserDefinedAfter>(EventScript.CreatureOnUserDefinedAfterScript);
            _event.RegisterEvent<CreatureEvent.OnBlockedByDoorAfter>(EventScript.CreatureOnBlockedByDoorAfterScript);
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

        private void OnSpawnCreated(uint objectSelf)
        {
            var spawn = OBJECT_SELF;
            SetCreatureScripts(spawn);
        }
    }
}
