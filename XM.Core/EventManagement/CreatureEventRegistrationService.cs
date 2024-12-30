using System;
using System.Collections.Generic;
using Anvil.Services;
using NLog;
using XM.API.Constants;
using XM.Core.EventManagement.CreatureEvent;
using XM.Core.EventManagement.XMEvent;

namespace XM.Core.EventManagement
{
    [ServiceBinding(typeof(CreatureEventRegistrationService))]
    [ServiceBinding(typeof(IXMOnSpawnCreated))]
    internal class CreatureEventRegistrationService : EventRegistrationServiceBase, IXMOnSpawnCreated
    {
        [Inject]
        public IList<ICreatureOnHeartbeatBefore> CreatureOnHeartbeatSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnNoticeBefore> CreatureOnNoticeSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnSpellCastAtBefore> CreatureOnSpellCastAtSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnMeleeAttackedBefore> CreatureOnMeleeAttackedSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnDamagedBefore> CreatureOnDamagedSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnDisturbedBefore> CreatureOnDisturbedSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnEndCombatRoundBefore> CreatureOnEndCombatRoundSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnSpawnInBefore> CreatureOnSpawnInSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnRestedBefore> CreatureOnRestedSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnDeathBefore> CreatureOnDeathSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnUserDefinedBefore> CreatureOnUserDefinedSubscriptions { get; set; }

        [Inject]
        public IList<ICreatureOnBlockedByDoorBefore> CreatureOnBlockedByDoorSubscriptions { get; set; }


        [ScriptHandler(EventScript.CreatureOnHeartbeatBeforeScript)]
        public void HandleCreatureOnHeartbeatBefore() => HandleEvent(CreatureOnHeartbeatSubscriptions, (subscription) => subscription.CreatureOnHeartbeatBefore());

        [ScriptHandler(EventScript.CreatureOnNoticeBeforeScript)]
        public void HandleCreatureOnNoticeBefore() => HandleEvent(CreatureOnNoticeSubscriptions, (subscription) => subscription.CreatureOnNoticeBefore());

        [ScriptHandler(EventScript.CreatureOnSpellCastAtBeforeScript)]
        public void HandleCreatureOnSpellCastAtBefore() => HandleEvent(CreatureOnSpellCastAtSubscriptions, (subscription) => subscription.CreatureOnSpellCastAtBefore());

        [ScriptHandler(EventScript.CreatureOnMeleeAttackedBeforeScript)]
        public void HandleCreatureOnMeleeAttackedBefore() => HandleEvent(CreatureOnMeleeAttackedSubscriptions, (subscription) => subscription.CreatureOnMeleeAttackedBefore());

        [ScriptHandler(EventScript.CreatureOnDamagedBeforeScript)]
        public void HandleCreatureOnDamagedBefore() => HandleEvent(CreatureOnDamagedSubscriptions, (subscription) => subscription.CreatureOnDamagedBefore());

        [ScriptHandler(EventScript.CreatureOnDisturbedBeforeScript)]
        public void HandleCreatureOnDisturbedBefore() => HandleEvent(CreatureOnDisturbedSubscriptions, (subscription) => subscription.CreatureOnDisturbedBefore());

        [ScriptHandler(EventScript.CreatureOnEndCombatRoundBeforeScript)]
        public void HandleCreatureOnEndCombatRoundBefore() => HandleEvent(CreatureOnEndCombatRoundSubscriptions, (subscription) => subscription.CreatureOnEndCombatRoundBefore());

        [ScriptHandler(EventScript.CreatureOnSpawnInBeforeScript)]
        public void HandleCreatureOnSpawnInBefore() => HandleEvent(CreatureOnSpawnInSubscriptions, (subscription) => subscription.CreatureOnSpawnInBefore());

        [ScriptHandler(EventScript.CreatureOnRestedBeforeScript)]
        public void HandleCreatureOnRestedBefore() => HandleEvent(CreatureOnRestedSubscriptions, (subscription) => subscription.CreatureOnRestedBefore());

        [ScriptHandler(EventScript.CreatureOnDeathBeforeScript)]
        public void HandleCreatureOnDeathBefore() => HandleEvent(CreatureOnDeathSubscriptions, (subscription) => subscription.CreatureOnDeathBefore());

        [ScriptHandler(EventScript.CreatureOnUserDefinedBeforeScript)]
        public void HandleCreatureOnUserDefinedBefore() => HandleEvent(CreatureOnUserDefinedSubscriptions, (subscription) => subscription.CreatureOnUserDefinedBefore());

        [ScriptHandler(EventScript.CreatureOnBlockedByDoorBeforeScript)]
        public void HandleCreatureOnBlockedByDoorBefore() => HandleEvent(CreatureOnBlockedByDoorSubscriptions, (subscription) => subscription.CreatureOnBlockedByDoorBefore());


        /// <summary>
        /// Assigns the necessary scripts to the targeted creature.
        /// This is most useful in the spawn system.
        /// </summary>
        /// <param name="spawn">The targeted creature that will have their scripts updated</param>
        public void SetCreatureScripts(uint spawn)
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
