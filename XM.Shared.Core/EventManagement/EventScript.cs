namespace XM.Shared.Core.EventManagement
{
    public class EventScript
    {
        // Area scripts
        public const string AreaOnEnterScript = "area_enter";
        public const string AreaOnExitScript = "area_exit";
        public const string AreaOnHeartbeatScript = "area_hb";
        public const string AreaOnUserDefinedEventScript = "area_user_def";

        // Creature scripts
        public const string CreatureOnHeartbeatScript = "ai_heartbeat";
        public const string CreatureOnNoticeScript = "ai_perception";
        public const string CreatureOnSpellCastAtScript = "ai_spellcast";
        public const string CreatureOnMeleeAttackedScript = "ai_attacked";
        public const string CreatureOnDamagedScript = "ai_damaged";
        public const string CreatureOnDisturbedScript = "ai_disturbed";
        public const string CreatureOnEndCombatRoundScript = "ai_roundend";
        public const string CreatureOnSpawnInScript = "ai_spawn";
        public const string CreatureOnRestedScript = "ai_rested";
        public const string CreatureOnDeathScript = "ai_death";
        public const string CreatureOnUserDefinedScript = "ai_userdefined";
        public const string CreatureOnBlockedByDoorScript = "ai_blocked";

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
        public const string NWNXOnChat = "on_nwnx_chat";
        public const string NWNXOnModulePreloadScript = "mod_preload";
        public const string NWNXOnAddAssociateBeforeScript = "asso_add_bef";
        public const string NWNXOnAddAssociateAfterScript = "asso_add_aft";
        public const string NWNXOnRemoveAssociateBeforeScript = "asso_rem_bef";
        public const string NWNXOnRemoveAssociateAfterScript = "asso_rem_aft";

        public const string NWNXOnStealthEnterBeforeScript = "stlent_add_bef";
        public const string NWNXOnStealthEnterAfterScript = "stlent_add_aft";
        public const string NWNXOnStealthExitBeforeScript = "stlex_add_bef";
        public const string NWNXOnStealthExitAfterScript = "stlex_add_aft";

        public const string NWNXOnExamineObjectBeforeScript = "examine_bef";
        public const string NWNXOnExamineObjectAfterScript = "examine_aft";

        public const string NWNXOnValidateUseItemBeforeScript = "item_valid_bef";
        public const string NWNXOnValidateUseItemAfterScript = "item_valid_aft";

        public const string NWNXOnUseItemBeforeScript = "item_use_bef";
        public const string NWNXOnUseItemAfterScript = "item_use_aft";

        public const string NWNXOnItemInventoryOpenBeforeScript = "inv_iopen_bef";
        public const string NWNXOnItemInventoryOpenAfterScript = "inv_iopen_aft";
        public const string NWNXOnItemInventoryCloseBeforeScript = "inv_close_bef";
        public const string NWNXOnItemInventoryCloseAfterScript = "inv_close_aft";

        public const string NWNXOnItemAmmoReloadBeforeScript = "ammo_reload_bef";
        public const string NWNXOnItemAmmoReloadAfterScript = "ammo_reload_aft";

        public const string NWNXOnItemScrollLearnBeforeScript = "scroll_lrn_bef";
        public const string NWNXOnItemScrollLearnAfterScript = "scroll_lrn_aft";

        public const string NWNXOnValidateItemEquipBeforeScript = "item_val_bef";
        public const string NWNXOnValidateItemEquipAfterScript = "item_val_aft";

        public const string NWNXOnItemEquipBeforeScript = "item_eqpval_bef";
        public const string NWNXOnItemEquipAfterScript = "item_eqpval_aft";

        public const string NWNXOnItemUnequipBeforeScript = "item_uneqp_bef";
        public const string NWNXOnItemUnequipAfterScript = "item_uneqp_aft";

        public const string NWNXOnItemDestroyObjectBeforeScript = "item_dest_bef";
        public const string NWNXOnItemDestroyObjectAfterScript = "item_dest_aft";
        public const string NWNXOnItemDecrementStacksizeBeforeScript = "item_dec_bef";
        public const string NWNXOnItemDecrementStacksizeAfterScript = "item_dec_aft";

        public const string NWNXOnItemUseLoreBeforeScript = "lore_id_bef";
        public const string NWNXOnItemUseLoreAfterScript = "lore_id_aft";

        public const string NWNXOnItemPayToIdentifyBeforeScript = "pay_id_bef";
        public const string NWNXOnItemPayToIdentifyAfterScript = "pay_id_aft";

        public const string NWNXOnItemSplitBeforeScript = "item_splt_bef";
        public const string NWNXOnItemSplitAfterScript = "item_splt_aft";

        public const string NWNXOnItemMergeBeforeScript = "item_merge_bef";
        public const string NWNXOnItemMergeAfterScript = "item_merge_aft";

        public const string NWNXOnItemAcquireBeforeScript = "item_acquire_bef";
        public const string NWNXOnItemAcquireAfterScript = "item_acquire_aft";

        public const string NWNXOnUseFeatBeforeScript = "feat_use_bef";
        public const string NWNXOnUseFeatAfterScript = "feat_use_aft";

        public const string NWNXOnDmGiveGoldBeforeScript = "dm_givegold_bef";
        public const string NWNXOnDmGiveGoldAfterScript = "dm_givegold_aft";
        public const string NWNXOnDmGiveXpBeforeScript = "dm_givexp_bef";
        public const string NWNXOnDmGiveXpAfterScript = "dm_givexp_aft";
        public const string NWNXOnDmGiveLevelBeforeScript = "dm_givelvl_bef";
        public const string NWNXOnDmGiveLevelAfterScript = "dm_givelvl_aft";
        public const string NWNXOnDmGiveAlignmentBeforeScript = "dm_givealn_bef";
        public const string NWNXOnDmGiveAlignmentAfterScript = "dm_givealn_aft";

        public const string NWNXOnDmSpawnObjectBeforeScript = "dm_spwnobj_bef";
        public const string NWNXOnDmSpawnObjectAfterScript = "dm_spwnobj_aft";

        public const string NWNXOnDmGiveItemBeforeScript = "dm_giveitem_bef";
        public const string NWNXOnDmGiveItemAfterScript = "dm_giveitem_aft";

        public const string NWNXOnDmHealBeforeScript = "dm_heal_bef";
        public const string NWNXOnDmHealAfterScript = "dm_heal_aft";
        public const string NWNXOnDmKillBeforeScript = "dm_kill_bef";
        public const string NWNXOnDmKillAfterScript = "dm_kill_aft";
        public const string NWNXOnDmToggleInvulnerableBeforeScript = "dm_invuln_bef";
        public const string NWNXOnDmToggleInvulnerableAfterScript = "dm_invuln_aft";
        public const string NWNXOnDmForceRestBeforeScript = "dm_forcerest_bef";
        public const string NWNXOnDmForceRestAfterScript = "dm_forcerest_aft";
        public const string NWNXOnDmLimboBeforeScript = "dm_limbo_bef";
        public const string NWNXOnDmLimboAfterScript = "dm_limbo_aft";
        public const string NWNXOnDmToggleAiBeforeScript = "dm_ai_bef";
        public const string NWNXOnDmToggleAiAfterScript = "dm_ai_aft";
        public const string NWNXOnDmToggleImmortalBeforeScript = "dm_immortal_bef";
        public const string NWNXOnDmToggleImmortalAfterScript = "dm_immortal_aft";

        public const string NWNXOnDmGotoBeforeScript = "dm_goto_bef";
        public const string NWNXOnDmGotoAfterScript = "dm_goto_aft";
        public const string NWNXOnDmPossessBeforeScript = "dm_poss_bef";
        public const string NWNXOnDmPossessAfterScript = "dm_poss_aft";
        public const string NWNXOnDmPossessFullPowerBeforeScript = "dm_possfull_bef";
        public const string NWNXOnDmPossessFullPowerAfterScript = "dm_possfull_aft";
        public const string NWNXOnDmToggleLockBeforeScript = "dm_lock_bef";
        public const string NWNXOnDmToggleLockAfterScript = "dm_lock_aft";
        public const string NWNXOnDmDisableTrapBeforeScript = "dm_distrap_bef";
        public const string NWNXOnDmDisableTrapAfterScript = "dm_distrap_aft";

        public const string NWNXOnDmJumpToPointBeforeScript = "dm_jumppt_bef";
        public const string NWNXOnDmJumpToPointAfterScript = "dm_jumppt_aft";
        public const string NWNXOnDmJumpTargetToPointBeforeScript = "dm_jumptarg_bef";
        public const string NWNXOnDmJumpTargetToPointAfterScript = "dm_jumptarg_aft";
        public const string NWNXOnDmJumpAllPlayersToPointBeforeScript = "dm_jumpall_bef";
        public const string NWNXOnDmJumpAllPlayersToPointAfterScript = "dm_jumpall_aft";

        public const string NWNXOnDmChangeDifficultyBeforeScript = "dm_chgdiff_bef";
        public const string NWNXOnDmChangeDifficultyAfterScript = "dm_chgdiff_aft";

        public const string NWNXOnDmViewInventoryBeforeScript = "dm_vwinven_bef";
        public const string NWNXOnDmViewInventoryAfterScript = "dm_vwinven_aft";

        public const string NWNXOnDmSpawnTrapOnObjectBeforeScript = "dm_spwntrap_bef";
        public const string NWNXOnDmSpawnTrapOnObjectAfterScript = "dm_spwntrap_aft";

        public const string NWNXOnDmDumpLocalsBeforeScript = "dm_dumploc_bef";
        public const string NWNXOnDmDumpLocalsAfterScript = "dm_dumploc_aft";

        public const string NWNXOnDmAppearBeforeScript = "dm_appear_bef";
        public const string NWNXOnDmAppearAfterScript = "dm_appear_aft";
        public const string NWNXOnDmDisappearBeforeScript = "dm_disappear_bef";
        public const string NWNXOnDmDisappearAfterScript = "dm_disappear_aft";
        public const string NWNXOnDmSetFactionBeforeScript = "dm_setfac_bef";
        public const string NWNXOnDmSetFactionAfterScript = "dm_setfac_aft";
        public const string NWNXOnDmTakeItemBeforeScript = "dm_takeitem_bef";
        public const string NWNXOnDmTakeItemAfterScript = "dm_takeitem_aft";
        public const string NWNXOnDmSetStatBeforeScript = "dm_setstat_bef";
        public const string NWNXOnDmSetStatAfterScript = "dm_setstat_aft";
        public const string NWNXOnDmGetVariableBeforeScript = "dm_getvar_bef";
        public const string NWNXOnDmGetVariableAfterScript = "dm_getvar_aft";
        public const string NWNXOnDmSetVariableBeforeScript = "dm_setvar_bef";
        public const string NWNXOnDmSetVariableAfterScript = "dm_setvar_aft";
        public const string NWNXOnDmSetTimeBeforeScript = "dm_settime_bef";
        public const string NWNXOnDmSetTimeAfterScript = "dm_settime_aft";
        public const string NWNXOnDmSetDateBeforeScript = "dm_setdate_bef";
        public const string NWNXOnDmSetDateAfterScript = "dm_setdate_aft";
        public const string NWNXOnDmSetFactionReputationBeforeScript = "dm_setrep_bef";
        public const string NWNXOnDmSetFactionReputationAfterScript = "dm_setrep_aft";
        public const string NWNXOnDmGetFactionReputationBeforeScript = "dm_getrep_bef";
        public const string NWNXOnDmGetFactionReputationAfterScript = "dm_getrep_aft";

        public const string NWNXOnClientDisconnectBeforeScript = "client_disc_bef";
        public const string NWNXOnClientDisconnectAfterScript = "client_disc_aft";

        public const string NWNXOnClientConnectBeforeScript = "client_conn_bef";
        public const string NWNXOnClientConnectAfterScript = "client_conn_aft";

        public const string NWNXOnStartCombatRoundBeforeScript = "comb_round_bef";
        public const string NWNXOnStartCombatRoundAfterScript = "comb_round_aft";

        public const string NWNXOnCastSpellBeforeScript = "cast_spell_bef";
        public const string NWNXOnCastSpellAfterScript = "cast_spell_aft";

        public const string NWNXSetMemorizedSpellSlotBeforeScript = "set_spell_bef";
        public const string NWNXSetMemorizedSpellSlotAfterScript = "set_spell_aft";

        public const string NWNXClearMemorizedSpellSlotBeforeScript = "clr_spell_bef";
        public const string NWNXClearMemorizedSpellSlotAfterScript = "clr_spell_aft";

        public const string NWNXOnHealerKitBeforeScript = "heal_kit_bef";
        public const string NWNXOnHealerKitAfterScript = "heal_kit_aft";

        public const string NWNXOnHealBeforeScript = "heal_bef";
        public const string NWNXOnHealAfterScript = "heal_aft";

        public const string NWNXOnPartyLeaveBeforeScript = "pty_leave_bef";
        public const string NWNXOnPartyLeaveAfterScript = "pty_leave_aft";
        public const string NWNXOnPartyKickBeforeScript = "pty_kick_bef";
        public const string NWNXOnPartyKickAfterScript = "pty_kick_aft";
        public const string NWNXOnPartyTransferLeadershipBeforeScript = "pty_chgldr_bef";
        public const string NWNXOnPartyTransferLeadershipAfterScript = "pty_chgldr_aft";
        public const string NWNXOnPartyInviteBeforeScript = "pty_invite_bef";
        public const string NWNXOnPartyInviteAfterScript = "pty_invite_aft";
        public const string NWNXOnPartyIgnoreInvitationBeforeScript = "pty_ignore_bef";
        public const string NWNXOnPartyIgnoreInvitationAfterScript = "pty_ignore_aft";
        public const string NWNXOnPartyAcceptInvitationBeforeScript = "pty_accept_bef";
        public const string NWNXOnPartyAcceptInvitationAfterScript = "pty_accept_aft";
        public const string NWNXOnPartyRejectInvitationBeforeScript = "pty_reject_bef";
        public const string NWNXOnPartyRejectInvitationAfterScript = "pty_reject_aft";
        public const string NWNXOnPartyKickHenchmanBeforeScript = "pty_kickhen_bef";
        public const string NWNXOnPartyKickHenchmanAfterScript = "pty_kickhen_aft";

        public const string NWNXOnCombatModeOnScript = "combat_mode_on";
        public const string NWNXOnCombatModeOffScript = "combat_mode_off";

        public const string NWNXOnUseSkillBeforeScript = "use_skill_bef";
        public const string NWNXOnUseSkillAfterScript = "use_skill_aft";

        public const string NWNXOnMapPinAddPinBeforeScript = "mappin_add_bef";
        public const string NWNXOnMapPinAddPinAfterScript = "mappin_add_aft";
        public const string NWNXOnMapPinChangePinBeforeScript = "mappin_chg_bef";
        public const string NWNXOnMapPinChangePinAfterScript = "mappin_chg_aft";
        public const string NWNXOnMapPinDestroyPinBeforeScript = "mappin_rem_bef";
        public const string NWNXOnMapPinDestroyPinAfterScript = "mappin_rem_aft";

        public const string NWNXOnDoListenDetectionBeforeScript = "det_listen_bef";
        public const string NWNXOnDoListenDetectionAfterScript = "det_listen_aft";
        public const string NWNXOnDoSpotDetectionBeforeScript = "det_spot_bef";
        public const string NWNXOnDoSpotDetectionAfterScript = "det_spot_aft";

        public const string NWNXOnPolymorphBeforeScript = "polymorph_bef";
        public const string NWNXOnPolymorphAfterScript = "polymorph_aft";
        public const string NWNXOnUnpolymorphBeforeScript = "unpolymorph_bef";
        public const string NWNXOnUnpolymorphAfterScript = "unpolymorph_aft";

        public const string NWNXOnEffectAppliedBeforeScript = "effect_app_bef";
        public const string NWNXOnEffectAppliedAfterScript = "effect_app_aft";
        public const string NWNXOnEffectRemovedBeforeScript = "effect_rem_bef";
        public const string NWNXOnEffectRemovedAfterScript = "effect_rem_aft";

        public const string NWNXOnQuickchatBeforeScript = "quickchat_bef";
        public const string NWNXOnQuickchatAfterScript = "quickchat_aft";

        public const string NWNXOnInventoryOpenBeforeScript = "inv_open_bef";
        public const string NWNXOnInventoryOpenAfterScript = "inv_open_aft";

        public const string NWNXOnInventorySelectPanelBeforeScript = "inv_panel_bef";
        public const string NWNXOnInventorySelectPanelAfterScript = "inv_panel_aft";

        public const string NWNXOnBarterStartBeforeScript = "bart_start_bef";
        public const string NWNXOnBarterStartAfterScript = "bart_start_aft";

        public const string NWNXOnBarterEndBeforeScript = "bart_end_bef";
        public const string NWNXOnBarterEndAfterScript = "bart_end_aft";

        public const string NWNXOnTrapDisarmBeforeScript = "trap_disarm_bef";
        public const string NWNXOnTrapDisarmAfterScript = "trap_disarm_aft";
        public const string NWNXOnTrapEnterBeforeScript = "trap_enter_bef";
        public const string NWNXOnTrapEnterAfterScript = "trap_enter_aft";
        public const string NWNXOnTrapExamineBeforeScript = "trap_exam_bef";
        public const string NWNXOnTrapExamineAfterScript = "trap_exam_aft";
        public const string NWNXOnTrapFlagBeforeScript = "trap_flag_bef";
        public const string NWNXOnTrapFlagAfterScript = "trap_flag_aft";
        public const string NWNXOnTrapRecoverBeforeScript = "trap_rec_bef";
        public const string NWNXOnTrapRecoverAfterScript = "trap_rec_aft";
        public const string NWNXOnTrapSetBeforeScript = "trap_set_bef";
        public const string NWNXOnTrapSetAfterScript = "trap_set_aft";

        public const string NWNXOnTimingBarStartBeforeScript = "timing_start_bef";
        public const string NWNXOnTimingBarStartAfterScript = "timing_start_aft";
        public const string NWNXOnTimingBarStopBeforeScript = "timing_stop_bef";
        public const string NWNXOnTimingBarStopAfterScript = "timing_stop_aft";
        public const string NWNXOnTimingBarCancelBeforeScript = "timing_canc_bef";
        public const string NWNXOnTimingBarCancelAfterScript = "timing_canc_aft";

        public const string NWNXOnWebhookSuccessScript = "webhook_success";
        public const string NWNXOnWebhookFailureScript = "webhook_failure";

        public const string NWNXOnCheckStickyPlayerNameReservedBeforeScript = "name_reserve_bef";
        public const string NWNXOnCheckStickyPlayerNameReservedAfterScript = "name_reserve_aft";

        public const string NWNXOnLevelUpBeforeScript = "lvl_up_bef";
        public const string NWNXOnLevelUpAfterScript = "lvl_up_aft";
        public const string NWNXOnLevelUpAutomaticBeforeScript = "lvl_upauto_bef";
        public const string NWNXOnLevelUpAutomaticAfterScript = "lvl_upauto_aft";
        public const string NWNXOnLevelDownBeforeScript = "lvl_down_bef";
        public const string NWNXOnLevelDownAfterScript = "lvl_down_aft";

        public const string NWNXOnInventoryAddItemBeforeScript = "inv_add_bef";
        public const string NWNXOnInventoryAddItemAfterScript = "inv_add_aft";
        public const string NWNXOnInventoryRemoveItemBeforeScript = "inv_rem_bef";
        public const string NWNXOnInventoryRemoveItemAfterScript = "inv_rem_aft";

        public const string NWNXOnInventoryAddGoldBeforeScript = "add_gold_bef";
        public const string NWNXOnInventoryAddGoldAfterScript = "add_gold_aft";
        public const string NWNXOnInventoryRemoveGoldBeforeScript = "rem_gold_bef";
        public const string NWNXOnInventoryRemoveGoldAfterScript = "rem_gold_aft";

        public const string NWNXOnPvpAttitudeChangeBeforeScript = "pvp_chgatt_bef";
        public const string NWNXOnPvpAttitudeChangeAfterScript = "pvp_chgatt_aft";

        public const string NWNXOnInputWalkToWaypointBeforeScript = "input_walk_bef";
        public const string NWNXOnInputWalkToWaypointAfterScript = "input_walk_aft";

        public const string NWNXOnMaterialChangeBeforeScript = "material_chg_bef";
        public const string NWNXOnMaterialChangeAfterScript = "material_chg_aft";

        public const string NWNXOnInputAttackObjectBeforeScript = "input_atk_bef";
        public const string NWNXOnInputAttackObjectAfterScript = "input_atk_aft";

        public const string NWNXOnObjectLockBeforeScript = "obj_lock_bef";
        public const string NWNXOnObjectLockAfterScript = "obj_lock_aft";

        public const string NWNXOnObjectUnlockBeforeScript = "obj_unlock_bef";
        public const string NWNXOnObjectUnlockAfterScript = "obj_unlock_aft";

        public const string NWNXOnUuidCollisionBeforeScript = "uuid_coll_bef";
        public const string NWNXOnUuidCollisionAfterScript = "uuid_coll_aft";

        // ELC Events
        public const string NWNXOnElcValidateCharacterBeforeScript = "elc_validate_bef";
        public const string NWNXOnElcValidateCharacterAfterScript = "elc_validate_aft";

        // Quickbar Events
        public const string NWNXOnQuickbarSetButtonBeforeScript = "qb_set_bef";
        public const string NWNXOnQuickbarSetButtonAfterScript = "qb_set_aft";

        // Calendar Events
        public const string NWNXOnCalendarHourScript = "calendar_hour";
        public const string NWNXOnCalendarDayScript = "calendar_day";
        public const string NWNXOnCalendarMonthScript = "calendar_month";
        public const string NWNXOnCalendarYearScript = "calendar_year";
        public const string NWNXOnCalendarDawnScript = "calendar_dawn";
        public const string NWNXOnCalendarDuskScript = "calendar_dusk";

        // Broadcast Spell Cast Events
        public const string NWNXOnBroadcastCastSpellBeforeScript = "cast_bspell_bef";
        public const string NWNXOnBroadcastCastSpellAfterScript = "cast_bspell_aft";

        // RunScript Debug Events
        public const string NWNXOnDebugRunScriptBeforeScript = "debug_script_bef";
        public const string NWNXOnDebugRunScriptAfterScript = "debug_script_aft";

        // RunScriptChunk Debug Events
        public const string NWNXOnDebugRunScriptChunkBeforeScript = "debug_chunk_bef";
        public const string NWNXOnDebugRunScriptChunkAfterScript = "debug_chunk_aft";

        // Buy/Sell Store Events
        public const string NWNXOnStoreRequestBuyBeforeScript = "store_buy_bef";
        public const string NWNXOnStoreRequestBuyAfterScript = "store_buy_aft";
        public const string NWNXOnStoreRequestSellBeforeScript = "store_sell_bef";
        public const string NWNXOnStoreRequestSellAfterScript = "store_sell_aft";

        // Input Drop Item Events
        public const string NWNXOnInputDropItemBeforeScript = "item_drop_bef";
        public const string NWNXOnInputDropItemAfterScript = "item_drop_aft";

        // Broadcast Attack of Opportunity Events
        public const string NWNXOnBroadcastAttackOfOpportunityBeforeScript = "brdcast_aoo_bef";
        public const string NWNXOnBroadcastAttackOfOpportunityAfterScript = "brdcast_aoo_aft";

        // Combat Attack of Opportunity Events
        public const string NWNXOnCombatAttackOfOpportunityBeforeScript = "combat_aoo_bef";
        public const string NWNXOnCombatAttackOfOpportunityAfterScript = "combat_aoo_aft";


        // Xenomech scripts
        public const string OnXMServerHeartbeatScript = "xm_heartbeat";
        public const string OnXMSpawnCreatedScript = "xm_spawn_created";
        public const string OnXMAreaCreatedScript = "xm_area_created";
        public const string OnXMModuleChangedScript = "xm_mod_changed";
        public const string OnXMPCInitializedScript = "xm_pc_init";
        public const string OnXMPlayerMigrationBeforeScript = "xm_migrate_bef";
        public const string OnXMPlayerMigrationAfterScript = "xm_migrate_aft";
        public const string OnXMPlayerOpenedQuestsMenuScript = "xm_open_quests";
        public const string OnXMPlayerOpenedAppearanceMenuScript = "xm_open_appear";
        public const string OnXMItemHitScript = "xm_item_hit";
    }
}
