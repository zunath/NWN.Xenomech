namespace XM.Core.EventManagement
{
    public class EventScript
    {
        // Area scripts
        public const string AreaOnEnterScript = "area_enter";
        public const string AreaOnExitScript = "area_exit";
        public const string AreaOnHeartbeatScript = "area_hb";
        public const string AreaOnUserDefinedEventScript = "area_user_def";

        // Creature scripts
        public const string CreatureOnHeartbeatScript = "x2_def_heartbeat";
        public const string CreatureOnNoticeScript = "x2_def_percept";
        public const string CreatureOnSpellCastAtScript = "x2_def_spellcast";
        public const string CreatureOnMeleeAttackedScript = "x2_def_attacked";
        public const string CreatureOnDamagedScript = "x2_def_ondamage";
        public const string CreatureOnDisturbedScript = "x2_def_ondisturb";
        public const string CreatureOnEndCombatRoundScript = "x2_def_endcombat";
        public const string CreatureOnSpawnInScript = "x2_def_spawn";
        public const string CreatureOnRestedScript = "x2_def_rested";
        public const string CreatureOnDeathScript = "x2_def_ondeath";
        public const string CreatureOnUserDefinedScript = "x2_def_userdef";
        public const string CreatureOnBlockedByDoorScript = "x2_def_onblocked";

        // Module scripts
        public const string OnModuleAcquireItemScript = "mod_acquire";
        public const string OnModuleActivateItemScript = "mod_activate";
        public const string OnModuleOnClientEnterScript = "mod_enter";
        public const string OnModuleOnClientExitScript = "mod_exit";
        public const string OnModulePlayerCancelCutsceneScript = "mod_abort_cs";
        public const string OnModuleHeartbeatScript = "mod_heartbeat";
        public const string OnModuleLoadScript = "mod_load";
        public const string OnModulePlayerChatScript = "mod_chat";
        public const string OnModulePlayerDyingScript = "mod_dying";
        public const string OnModulePlayerDeathScript = "mod_death";
        public const string OnModuleEquipItemScript = "mod_equip";
        public const string OnModulePlayerLevelUpScript = "mod_level_up";
        public const string OnModuleRespawnScript = "mod_respawn";
        public const string OnModulePlayerRestScript = "mod_rest";
        public const string OnModuleUnequipItemScript = "mod_unequip";
        public const string OnModuleUnacquireItemScript = "mod_unacquire";
        public const string OnModuleUserDefinedEventScript = "mod_user_def";
        public const string OnModulePlayerTargetScript = "mod_p_target";
        public const string OnModulePlayerGuiScript = "mod_gui_event";
        public const string OnModulePlayerTileScript = "mod_tile_event";
        public const string OnModuleNuiEventScript = "mod_nui_event";

        // Player scripts
        public const string PlayerOnHeartbeatScript = "pc_heartbeat";
        public const string PlayerOnNoticeScript = "pc_perception";
        public const string PlayerOnSpellCastAtScript = "pc_spellcastat";
        public const string PlayerOnMeleeAttackedScript = "pc_attacked";
        public const string PlayerOnDamagedScript = "pc_damaged";
        public const string PlayerOnDisturbedScript = "pc_disturb";
        public const string PlayerOnEndCombatRoundScript = "pc_roundend";
        public const string PlayerOnSpawnInScript = "pc_spawn";
        public const string PlayerOnRestedScript = "pc_rested";
        public const string PlayerOnDeathScript = "pc_death";
        public const string PlayerOnUserDefinedScript = "pc_userdef";
        public const string PlayerOnBlockedByDoorScript = "pc_blocked";

        // NWNX scripts
        public const string NWNXOnModulePreloadScript = "mod_preload";

        // Xenomech scripts
        public const string OnXMServerHeartbeatScript = "xm_heartbeat";
        public const string OnXMSpawnCreatedScript = "xm_spawn_created";
        public const string OnXMAreaCreatedScript = "xm_area_created";
        public const string OnXMModuleChangedScript = "xm_mod_changed";
    }
}
