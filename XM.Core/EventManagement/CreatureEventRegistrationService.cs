using System.Collections.Generic;
using Anvil.Services;
using XM.API.Constants;
using XM.Core.EventManagement.CreatureEvent;
using XM.Core.EventManagement.XMEvent;

namespace XM.Core.EventManagement
{
    [ServiceBinding(typeof(CreatureEventRegistrationService))]
    [ServiceBinding(typeof(ISpawnCreatedEvent))]
    internal class CreatureEventRegistrationService : EventRegistrationServiceBase, ISpawnCreatedEvent
    {
        // Before subscriptions
        [Inject]
        public IList<ICreatureOnHeartbeatBeforeEvent> CreatureOnHeartbeatBeforeSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnNoticeBeforeEvent> CreatureOnNoticeBeforeSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnSpellCastAtBeforeEvent> CreatureOnSpellCastAtBeforeSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnMeleeAttackedBeforeEvent> CreatureOnMeleeAttackedBeforeSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnDamagedBeforeEvent> CreatureOnDamagedBeforeSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnDisturbedBeforeEvent> CreatureOnDisturbedBeforeSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnEndCombatRoundBeforeEvent> CreatureOnEndCombatRoundBeforeSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnSpawnInBeforeEvent> CreatureOnSpawnInBeforeSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnRestedBeforeEvent> CreatureOnRestedBeforeSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnDeathBeforeEvent> CreatureOnDeathBeforeSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnUserDefinedBeforeEvent> CreatureOnUserDefinedBeforeSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnBlockedByDoorBeforeEvent> CreatureOnBlockedByDoorBeforeSubscriptions { get; set; }

        // After Subscriptions
        [Inject]
        public IList<ICreatureOnHeartbeatAfterEvent> CreatureOnHeartbeatAfterSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnNoticeAfterEvent> CreatureOnNoticeAfterSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnSpellCastAtAfter> CreatureOnSpellCastAtAfterSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnMeleeAttackedAfterEvent> CreatureOnMeleeAttackedAfterSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnDamagedAfterEvent> CreatureOnDamagedAfterSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnDisturbedAfterEvent> CreatureOnDisturbedAfterSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnEndCombatRoundAfterEvent> CreatureOnEndCombatRoundAfterSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnSpawnInAfterEvent> CreatureOnSpawnInAfterSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnRestedAfterEvent> CreatureOnRestedAfterSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnDeathAfterEvent> CreatureOnDeathAfterSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnUserDefinedAfterEvent> CreatureOnUserDefinedAfterSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnBlockedByDoorAfterEvent> CreatureOnBlockedByDoorAfterSubscriptions { get; set; }





        [ScriptHandler(EventScript.CreatureOnHeartbeatBeforeScript)]
        public void HandleCreatureOnHeartbeatBefore() => HandleEvent(CreatureOnHeartbeatBeforeSubscriptions, (subscription) => subscription.CreatureOnHeartbeatBefore());

        [ScriptHandler(EventScript.CreatureOnNoticeBeforeScript)]
        public void HandleCreatureOnNoticeBefore() => HandleEvent(CreatureOnNoticeBeforeSubscriptions, (subscription) => subscription.CreatureOnNoticeBefore());

        [ScriptHandler(EventScript.CreatureOnSpellCastAtBeforeScript)]
        public void HandleCreatureOnSpellCastAtBefore() => HandleEvent(CreatureOnSpellCastAtBeforeSubscriptions, (subscription) => subscription.CreatureOnSpellCastAtBefore());

        [ScriptHandler(EventScript.CreatureOnMeleeAttackedBeforeScript)]
        public void HandleCreatureOnMeleeAttackedBefore() => HandleEvent(CreatureOnMeleeAttackedBeforeSubscriptions, (subscription) => subscription.CreatureOnMeleeAttackedBefore());

        [ScriptHandler(EventScript.CreatureOnDamagedBeforeScript)]
        public void HandleCreatureOnDamagedBefore() => HandleEvent(CreatureOnDamagedBeforeSubscriptions, (subscription) => subscription.CreatureOnDamagedBefore());

        [ScriptHandler(EventScript.CreatureOnDisturbedBeforeScript)]
        public void HandleCreatureOnDisturbedBefore() => HandleEvent(CreatureOnDisturbedBeforeSubscriptions, (subscription) => subscription.CreatureOnDisturbedBefore());

        [ScriptHandler(EventScript.CreatureOnEndCombatRoundBeforeScript)]
        public void HandleCreatureOnEndCombatRoundBefore() => HandleEvent(CreatureOnEndCombatRoundBeforeSubscriptions, (subscription) => subscription.CreatureOnEndCombatRoundBefore());

        [ScriptHandler(EventScript.CreatureOnSpawnInBeforeScript)]
        public void HandleCreatureOnSpawnInBefore() => HandleEvent(CreatureOnSpawnInBeforeSubscriptions, (subscription) => subscription.CreatureOnSpawnInBefore());

        [ScriptHandler(EventScript.CreatureOnRestedBeforeScript)]
        public void HandleCreatureOnRestedBefore() => HandleEvent(CreatureOnRestedBeforeSubscriptions, (subscription) => subscription.CreatureOnRestedBefore());

        [ScriptHandler(EventScript.CreatureOnDeathBeforeScript)]
        public void HandleCreatureOnDeathBefore() => HandleEvent(CreatureOnDeathBeforeSubscriptions, (subscription) => subscription.CreatureOnDeathBefore());

        [ScriptHandler(EventScript.CreatureOnUserDefinedBeforeScript)]
        public void HandleCreatureOnUserDefinedBefore() => HandleEvent(CreatureOnUserDefinedBeforeSubscriptions, (subscription) => subscription.CreatureOnUserDefinedBefore());

        [ScriptHandler(EventScript.CreatureOnBlockedByDoorBeforeScript)]
        public void HandleCreatureOnBlockedByDoorBefore() => HandleEvent(CreatureOnBlockedByDoorBeforeSubscriptions, (subscription) => subscription.CreatureOnBlockedByDoorBefore());


        [ScriptHandler(EventScript.CreatureOnHeartbeatAfterScript)]
        public void HandleCreatureOnHeartbeat() => HandleEvent(CreatureOnHeartbeatAfterSubscriptions, (subscription) => subscription.CreatureOnHeartbeatAfter());

        [ScriptHandler(EventScript.CreatureOnNoticeAfterScript)]
        public void HandleCreatureOnNotice() => HandleEvent(CreatureOnNoticeAfterSubscriptions, (subscription) => subscription.CreatureOnNoticeAfter());

        [ScriptHandler(EventScript.CreatureOnSpellCastAtAfterScript)]
        public void HandleCreatureOnSpellCastAt() => HandleEvent(CreatureOnSpellCastAtAfterSubscriptions, (subscription) => subscription.CreatureOnSpellCastAtAfterEvent());

        [ScriptHandler(EventScript.CreatureOnMeleeAttackedAfterScript)]
        public void HandleCreatureOnMeleeAttacked() => HandleEvent(CreatureOnMeleeAttackedAfterSubscriptions, (subscription) => subscription.CreatureOnMeleeAttackedAfter());

        [ScriptHandler(EventScript.CreatureOnDamagedAfterScript)]
        public void HandleCreatureOnDamaged() => HandleEvent(CreatureOnDamagedAfterSubscriptions, (subscription) => subscription.CreatureOnDamagedAfter());

        [ScriptHandler(EventScript.CreatureOnDisturbedAfterScript)]
        public void HandleCreatureOnDisturbed() => HandleEvent(CreatureOnDisturbedAfterSubscriptions, (subscription) => subscription.CreatureOnDisturbedAfter());

        [ScriptHandler(EventScript.CreatureOnEndCombatRoundAfterScript)]
        public void HandleCreatureOnEndCombatRound() => HandleEvent(CreatureOnEndCombatRoundAfterSubscriptions, (subscription) => subscription.CreatureOnEndCombatRoundAfter());

        [ScriptHandler(EventScript.CreatureOnSpawnInAfterScript)]
        public void HandleCreatureOnSpawnIn() => HandleEvent(CreatureOnSpawnInAfterSubscriptions, (subscription) => subscription.CreatureOnSpawnInAfter());

        [ScriptHandler(EventScript.CreatureOnRestedAfterScript)]
        public void HandleCreatureOnRested() => HandleEvent(CreatureOnRestedAfterSubscriptions, (subscription) => subscription.CreatureOnRestedAfter());

        [ScriptHandler(EventScript.CreatureOnDeathAfterScript)]
        public void HandleCreatureOnDeath() => HandleEvent(CreatureOnDeathAfterSubscriptions, (subscription) => subscription.CreatureOnDeathAfter());

        [ScriptHandler(EventScript.CreatureOnUserDefinedAfterScript)]
        public void HandleCreatureOnUserDefined() => HandleEvent(CreatureOnUserDefinedAfterSubscriptions, (subscription) => subscription.CreatureOnUserDefinedAfter());

        [ScriptHandler(EventScript.CreatureOnBlockedByDoorAfterScript)]
        public void HandleCreatureOnBlockedByDoor() => HandleEvent(CreatureOnBlockedByDoorAfterSubscriptions, (subscription) => subscription.CreatureOnBlockedByDoorAfter());

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

        public void OnSpawnCreated()
        {
            var spawn = OBJECT_SELF;
            SetCreatureScripts(spawn);
        }
    }
}
