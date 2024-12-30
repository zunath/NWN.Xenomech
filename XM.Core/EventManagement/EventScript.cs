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
        public const string CreatureOnHeartbeatBeforeScript = "crea_hb_bef";
        public const string CreatureOnNoticeBeforeScript = "crea_perc_bef";
        public const string CreatureOnSpellCastAtBeforeScript = "crea_splcast_bef";
        public const string CreatureOnMeleeAttackedBeforeScript = "crea_attack_bef";
        public const string CreatureOnDamagedBeforeScript = "crea_damaged_bef";
        public const string CreatureOnDisturbedBeforeScript = "crea_disturb_bef";
        public const string CreatureOnEndCombatRoundBeforeScript = "crea_rndend_bef";
        public const string CreatureOnSpawnInBeforeScript = "crea_spawn_bef";
        public const string CreatureOnRestedBeforeScript = "crea_rest_bef";
        public const string CreatureOnDeathBeforeScript = "crea_death_bef";
        public const string CreatureOnUserDefinedBeforeScript = "crea_userdef_bef";
        public const string CreatureOnBlockedByDoorBeforeScript = "crea_block_bef";

        public const string CreatureOnHeartbeatAfterScript = "crea_hb_aft";
        public const string CreatureOnNoticeAfterScript = "crea_perc_aft";
        public const string CreatureOnSpellCastAtAfterScript = "crea_splcast_aft";
        public const string CreatureOnMeleeAttackedAfterScript = "crea_attack_aft";
        public const string CreatureOnDamagedAfterScript = "crea_damaged_aft";
        public const string CreatureOnDisturbedAfterScript = "crea_disturb_aft";
        public const string CreatureOnEndCombatRoundAfterScript = "crea_rndend_aft";
        public const string CreatureOnSpawnInAfterScript = "crea_spawn_aft";
        public const string CreatureOnRestedAfterScript = "crea_rest_aft";
        public const string CreatureOnDeathAfterScript = "crea_death_aft";
        public const string CreatureOnUserDefinedAfterScript = "crea_userdef_aft";
        public const string CreatureOnBlockedByDoorAfterScript = "crea_block_aft";

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
        public const string OnXMCacheDataBeforeScript = "xm_cache_bef";
        public const string OnXMCacheDataAfterScript = "xm_cache_aft";
        public const string OnXMDatabaseLoadedScript = "xm_db_loaded";
        public const string OnXMPCInitializedScript = "xm_pc_init";
        public const string OnXMPlayerMigrationBeforeScript = "xm_migrate_bef";
        public const string OnXMPlayerMigrationAfterScript = "xm_migrate_aft";
    }
}
