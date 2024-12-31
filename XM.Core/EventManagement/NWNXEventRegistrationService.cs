using System.Collections.Generic;
using Anvil.Services;
using NWN.Core.NWNX;
using XM.Core.EventManagement.NWNXEvent;

namespace XM.Core.EventManagement
{
    [ServiceBinding(typeof(NWNXEventRegistrationService))]
    internal class NWNXEventRegistrationService: 
        EventRegistrationServiceBase
    {

        public NWNXEventRegistrationService()
        {
            HookNWNXEvents();
        }

        public void HookNWNXEvents()
        {
            // Associate events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ADD_ASSOCIATE_BEFORE, EventScript.NWNXOnAddAssociateBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ADD_ASSOCIATE_AFTER, EventScript.NWNXOnAddAssociateAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_REMOVE_ASSOCIATE_BEFORE, EventScript.NWNXOnRemoveAssociateBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_REMOVE_ASSOCIATE_AFTER, EventScript.NWNXOnRemoveAssociateAfterScript);

            // Stealth events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_STEALTH_ENTER_BEFORE, EventScript.NWNXOnStealthEnterBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_STEALTH_ENTER_AFTER, EventScript.NWNXOnStealthEnterAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_STEALTH_EXIT_BEFORE, EventScript.NWNXOnStealthExitBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_STEALTH_EXIT_AFTER, EventScript.NWNXOnStealthExitAfterScript);

            // Examine events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_EXAMINE_OBJECT_BEFORE, EventScript.NWNXOnExamineObjectBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_EXAMINE_OBJECT_AFTER, EventScript.NWNXOnExamineObjectAfterScript);

            // Validate Use Item events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_VALIDATE_USE_ITEM_BEFORE, EventScript.NWNXOnValidateUseItemBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_VALIDATE_USE_ITEM_AFTER, EventScript.NWNXOnValidateUseItemAfterScript);

            // Use Item events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_USE_ITEM_BEFORE, EventScript.NWNXOnUseItemBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_USE_ITEM_AFTER, EventScript.NWNXOnUseItemAfterScript);

            // Item Container events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_INVENTORY_OPEN_BEFORE, EventScript.NWNXOnItemInventoryOpenBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_INVENTORY_OPEN_AFTER, EventScript.NWNXOnItemInventoryOpenAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_INVENTORY_CLOSE_BEFORE, EventScript.NWNXOnItemInventoryCloseBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_INVENTORY_CLOSE_AFTER, EventScript.NWNXOnItemInventoryCloseAfterScript);
            
            // Ammunition Reload events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_AMMO_RELOAD_BEFORE, EventScript.NWNXOnItemAmmoReloadBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_AMMO_RELOAD_AFTER, EventScript.NWNXOnItemAmmoReloadAfterScript);

            // Scroll Learn events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_SCROLL_LEARN_BEFORE, EventScript.NWNXOnItemScrollLearnBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_SCROLL_LEARN_AFTER, EventScript.NWNXOnItemScrollLearnAfterScript);

            // Validate Item Equip events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_VALIDATE_ITEM_EQUIP_BEFORE, EventScript.NWNXOnValidateItemEquipBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_VALIDATE_ITEM_EQUIP_AFTER, EventScript.NWNXOnValidateItemEquipAfterScript);

            // Item Equip events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_EQUIP_BEFORE, EventScript.NWNXOnItemEquipBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_EQUIP_AFTER, EventScript.NWNXOnItemEquipAfterScript);

            // Item Unequip events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_UNEQUIP_BEFORE, EventScript.NWNXOnItemUnequipBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_UNEQUIP_AFTER, EventScript.NWNXOnItemUnequipAfterScript);

            // Item Destroy events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_DESTROY_OBJECT_BEFORE, EventScript.NWNXOnItemDestroyObjectBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_DESTROY_OBJECT_AFTER, EventScript.NWNXOnItemDestroyObjectAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_DECREMENT_STACKSIZE_BEFORE, EventScript.NWNXOnItemDecrementStacksizeBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_DECREMENT_STACKSIZE_AFTER, EventScript.NWNXOnItemDecrementStacksizeAfterScript);

            // Item Use Lore to Identify events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_USE_LORE_BEFORE, EventScript.NWNXOnItemUseLoreBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_USE_LORE_AFTER, EventScript.NWNXOnItemUseLoreAfterScript);

            // Item Pay to Identify events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_PAY_TO_IDENTIFY_BEFORE, EventScript.NWNXOnItemPayToIdentifyBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_PAY_TO_IDENTIFY_AFTER, EventScript.NWNXOnItemPayToIdentifyAfterScript);

            // Item Split events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_SPLIT_BEFORE, EventScript.NWNXOnItemSplitBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_SPLIT_AFTER, EventScript.NWNXOnItemSplitAfterScript);

            // Item Merge events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_MERGE_BEFORE, EventScript.NWNXOnItemMergeBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_MERGE_AFTER, EventScript.NWNXOnItemMergeAfterScript);

            // Acquire Item events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_ACQUIRE_BEFORE, EventScript.NWNXOnItemAcquireBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ITEM_ACQUIRE_AFTER, EventScript.NWNXOnItemAcquireAfterScript);

            // Feat Use events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_USE_FEAT_BEFORE, EventScript.NWNXOnUseFeatBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_USE_FEAT_AFTER, EventScript.NWNXOnUseFeatAfterScript);

            // DM Give events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_GIVE_GOLD_BEFORE, EventScript.NWNXOnDmGiveGoldBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_GIVE_GOLD_AFTER, EventScript.NWNXOnDmGiveGoldAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_GIVE_XP_BEFORE, EventScript.NWNXOnDmGiveXpBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_GIVE_XP_AFTER, EventScript.NWNXOnDmGiveXpAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_GIVE_LEVEL_BEFORE, EventScript.NWNXOnDmGiveLevelBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_GIVE_LEVEL_AFTER, EventScript.NWNXOnDmGiveLevelAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_GIVE_ALIGNMENT_BEFORE, EventScript.NWNXOnDmGiveAlignmentBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_GIVE_ALIGNMENT_AFTER, EventScript.NWNXOnDmGiveAlignmentAfterScript);

            // DM Spawn events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_SPAWN_OBJECT_BEFORE, EventScript.NWNXOnDmSpawnObjectBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_SPAWN_OBJECT_AFTER, EventScript.NWNXOnDmSpawnObjectAfterScript);

            // DM Give Item events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_GIVE_ITEM_BEFORE, EventScript.NWNXOnDmGiveItemBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_GIVE_ITEM_AFTER, EventScript.NWNXOnDmGiveItemAfterScript);
            
            // DM Multiple Object Action events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_HEAL_BEFORE, EventScript.NWNXOnDmHealBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_HEAL_AFTER, EventScript.NWNXOnDmHealAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_KILL_BEFORE, EventScript.NWNXOnDmKillBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_KILL_AFTER, EventScript.NWNXOnDmKillAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_TOGGLE_INVULNERABLE_BEFORE, EventScript.NWNXOnDmToggleInvulnerableBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_TOGGLE_INVULNERABLE_AFTER, EventScript.NWNXOnDmToggleInvulnerableAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_FORCE_REST_BEFORE, EventScript.NWNXOnDmForceRestBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_FORCE_REST_AFTER, EventScript.NWNXOnDmForceRestAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_LIMBO_BEFORE, EventScript.NWNXOnDmLimboBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_LIMBO_AFTER, EventScript.NWNXOnDmLimboAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_TOGGLE_AI_BEFORE, EventScript.NWNXOnDmToggleAiBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_TOGGLE_AI_AFTER, EventScript.NWNXOnDmToggleAiAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_TOGGLE_IMMORTAL_BEFORE, EventScript.NWNXOnDmToggleImmortalBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_TOGGLE_IMMORTAL_AFTER, EventScript.NWNXOnDmToggleImmortalAfterScript);

            // DM Single Object Action events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_GOTO_BEFORE, EventScript.NWNXOnDmGotoBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_GOTO_AFTER, EventScript.NWNXOnDmGotoAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_POSSESS_BEFORE, EventScript.NWNXOnDmPossessBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_POSSESS_AFTER, EventScript.NWNXOnDmPossessAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_POSSESS_FULL_POWER_BEFORE, EventScript.NWNXOnDmPossessFullPowerBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_POSSESS_FULL_POWER_AFTER, EventScript.NWNXOnDmPossessFullPowerAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_TOGGLE_LOCK_BEFORE, EventScript.NWNXOnDmToggleLockBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_TOGGLE_LOCK_AFTER, EventScript.NWNXOnDmToggleLockAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_DISABLE_TRAP_BEFORE, EventScript.NWNXOnDmDisableTrapBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_DISABLE_TRAP_AFTER, EventScript.NWNXOnDmDisableTrapAfterScript);

            // DM Jump events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_JUMP_TO_POINT_BEFORE, EventScript.NWNXOnDmJumpToPointBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_JUMP_TO_POINT_AFTER, EventScript.NWNXOnDmJumpToPointAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_JUMP_TARGET_TO_POINT_BEFORE, EventScript.NWNXOnDmJumpTargetToPointBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_JUMP_TARGET_TO_POINT_AFTER, EventScript.NWNXOnDmJumpTargetToPointAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_JUMP_ALL_PLAYERS_TO_POINT_BEFORE, EventScript.NWNXOnDmJumpAllPlayersToPointBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_JUMP_ALL_PLAYERS_TO_POINT_AFTER, EventScript.NWNXOnDmJumpAllPlayersToPointAfterScript);

            // DM Change Difficulty events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_CHANGE_DIFFICULTY_BEFORE, EventScript.NWNXOnDmChangeDifficultyBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_CHANGE_DIFFICULTY_AFTER, EventScript.NWNXOnDmChangeDifficultyAfterScript);

            // DM View Inventory events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_VIEW_INVENTORY_BEFORE, EventScript.NWNXOnDmViewInventoryBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_VIEW_INVENTORY_AFTER, EventScript.NWNXOnDmViewInventoryAfterScript);

            // DM Spawn Trap events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_SPAWN_TRAP_ON_OBJECT_BEFORE, EventScript.NWNXOnDmSpawnTrapOnObjectBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_SPAWN_TRAP_ON_OBJECT_AFTER, EventScript.NWNXOnDmSpawnTrapOnObjectAfterScript);

            // DM Dump Locals events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_DUMP_LOCALS_BEFORE, EventScript.NWNXOnDmDumpLocalsBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_DUMP_LOCALS_AFTER, EventScript.NWNXOnDmDumpLocalsAfterScript);
            // DM Other events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_APPEAR_BEFORE, EventScript.NWNXOnDmAppearBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_APPEAR_AFTER, EventScript.NWNXOnDmAppearAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_DISAPPEAR_BEFORE, EventScript.NWNXOnDmDisappearBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_DISAPPEAR_AFTER, EventScript.NWNXOnDmDisappearAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_SET_FACTION_BEFORE, EventScript.NWNXOnDmSetFactionBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_SET_FACTION_AFTER, EventScript.NWNXOnDmSetFactionAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_TAKE_ITEM_BEFORE, EventScript.NWNXOnDmTakeItemBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_TAKE_ITEM_AFTER, EventScript.NWNXOnDmTakeItemAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_SET_STAT_BEFORE, EventScript.NWNXOnDmSetStatBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_SET_STAT_AFTER, EventScript.NWNXOnDmSetStatAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_GET_VARIABLE_BEFORE, EventScript.NWNXOnDmGetVariableBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_GET_VARIABLE_AFTER, EventScript.NWNXOnDmGetVariableAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_SET_VARIABLE_BEFORE, EventScript.NWNXOnDmSetVariableBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_SET_VARIABLE_AFTER, EventScript.NWNXOnDmSetVariableAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_SET_TIME_BEFORE, EventScript.NWNXOnDmSetTimeBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_SET_TIME_AFTER, EventScript.NWNXOnDmSetTimeAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_SET_DATE_BEFORE, EventScript.NWNXOnDmSetDateBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_SET_DATE_AFTER, EventScript.NWNXOnDmSetDateAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_SET_FACTION_REPUTATION_BEFORE, EventScript.NWNXOnDmSetFactionReputationBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_SET_FACTION_REPUTATION_AFTER, EventScript.NWNXOnDmSetFactionReputationAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_GET_FACTION_REPUTATION_BEFORE, EventScript.NWNXOnDmGetFactionReputationBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DM_GET_FACTION_REPUTATION_AFTER, EventScript.NWNXOnDmGetFactionReputationAfterScript);

            // Client Disconnect events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_CLIENT_DISCONNECT_BEFORE, EventScript.NWNXOnClientDisconnectBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_CLIENT_DISCONNECT_AFTER, EventScript.NWNXOnClientDisconnectAfterScript);

            // Client Connect events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_CLIENT_CONNECT_BEFORE, EventScript.NWNXOnClientConnectBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_CLIENT_CONNECT_AFTER, EventScript.NWNXOnClientConnectAfterScript);

            // Combat Round Start events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_START_COMBAT_ROUND_BEFORE, EventScript.NWNXOnStartCombatRoundBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_START_COMBAT_ROUND_AFTER, EventScript.NWNXOnStartCombatRoundAfterScript);

            // Cast Spell events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_CAST_SPELL_BEFORE, EventScript.NWNXOnCastSpellBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_CAST_SPELL_AFTER, EventScript.NWNXOnCastSpellAfterScript);

            // Set Memorized Spell Slot events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_SET_MEMORIZED_SPELL_SLOT_BEFORE, EventScript.NWNXSetMemorizedSpellSlotBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_SET_MEMORIZED_SPELL_SLOT_AFTER, EventScript.NWNXSetMemorizedSpellSlotAfterScript);

            // Clear Memorized Spell Slot events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_CLEAR_MEMORIZED_SPELL_SLOT_BEFORE, EventScript.NWNXClearMemorizedSpellSlotBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_CLEAR_MEMORIZED_SPELL_SLOT_AFTER, EventScript.NWNXClearMemorizedSpellSlotAfterScript);

            // Healer Kit Use events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_HEALER_KIT_BEFORE, EventScript.NWNXOnHealerKitBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_HEALER_KIT_AFTER, EventScript.NWNXOnHealerKitAfterScript);

            // Healing events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_HEAL_BEFORE, EventScript.NWNXOnHealBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_HEAL_AFTER, EventScript.NWNXOnHealAfterScript);

            // Party Action events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_LEAVE_BEFORE, EventScript.NWNXOnPartyLeaveBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_LEAVE_AFTER, EventScript.NWNXOnPartyLeaveAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_KICK_BEFORE, EventScript.NWNXOnPartyKickBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_KICK_AFTER, EventScript.NWNXOnPartyKickAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_TRANSFER_LEADERSHIP_BEFORE, EventScript.NWNXOnPartyTransferLeadershipBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_TRANSFER_LEADERSHIP_AFTER, EventScript.NWNXOnPartyTransferLeadershipAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_INVITE_BEFORE, EventScript.NWNXOnPartyInviteBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_INVITE_AFTER, EventScript.NWNXOnPartyInviteAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_IGNORE_INVITATION_BEFORE, EventScript.NWNXOnPartyIgnoreInvitationBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_IGNORE_INVITATION_AFTER, EventScript.NWNXOnPartyIgnoreInvitationAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_ACCEPT_INVITATION_BEFORE, EventScript.NWNXOnPartyAcceptInvitationBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_ACCEPT_INVITATION_AFTER, EventScript.NWNXOnPartyAcceptInvitationAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_REJECT_INVITATION_BEFORE, EventScript.NWNXOnPartyRejectInvitationBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_REJECT_INVITATION_AFTER, EventScript.NWNXOnPartyRejectInvitationAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_KICK_HENCHMAN_BEFORE, EventScript.NWNXOnPartyKickHenchmanBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_KICK_HENCHMAN_AFTER, EventScript.NWNXOnPartyKickHenchmanAfterScript);

            // Combat Mode Toggle events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_COMBAT_MODE_ON, EventScript.NWNXOnCombatModeOnScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_COMBAT_MODE_OFF, EventScript.NWNXOnCombatModeOffScript);

            // Use Skill events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_USE_SKILL_BEFORE, EventScript.NWNXOnUseSkillBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_USE_SKILL_AFTER, EventScript.NWNXOnUseSkillAfterScript);

            // Map Pin events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_MAP_PIN_ADD_PIN_BEFORE, EventScript.NWNXOnMapPinAddPinBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_MAP_PIN_ADD_PIN_AFTER, EventScript.NWNXOnMapPinAddPinAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_MAP_PIN_CHANGE_PIN_BEFORE, EventScript.NWNXOnMapPinChangePinBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_MAP_PIN_CHANGE_PIN_AFTER, EventScript.NWNXOnMapPinChangePinAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_MAP_PIN_DESTROY_PIN_BEFORE, EventScript.NWNXOnMapPinDestroyPinBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_MAP_PIN_DESTROY_PIN_AFTER, EventScript.NWNXOnMapPinDestroyPinAfterScript);

            // Spot/Listen Detection events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DO_LISTEN_DETECTION_BEFORE, EventScript.NWNXOnDoListenDetectionBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DO_LISTEN_DETECTION_AFTER, EventScript.NWNXOnDoListenDetectionAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DO_SPOT_DETECTION_BEFORE, EventScript.NWNXOnDoSpotDetectionBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DO_SPOT_DETECTION_AFTER, EventScript.NWNXOnDoSpotDetectionAfterScript);

            // Polymorph events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_POLYMORPH_BEFORE, EventScript.NWNXOnPolymorphBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_POLYMORPH_AFTER, EventScript.NWNXOnPolymorphAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_UNPOLYMORPH_BEFORE, EventScript.NWNXOnUnpolymorphBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_UNPOLYMORPH_AFTER, EventScript.NWNXOnUnpolymorphAfterScript);

            // Effect Applied/Removed events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_EFFECT_APPLIED_BEFORE, EventScript.NWNXOnEffectAppliedBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_EFFECT_APPLIED_AFTER, EventScript.NWNXOnEffectAppliedAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_EFFECT_REMOVED_BEFORE, EventScript.NWNXOnEffectRemovedBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_EFFECT_REMOVED_AFTER, EventScript.NWNXOnEffectRemovedAfterScript);

            // Quickchat events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_QUICKCHAT_BEFORE, EventScript.NWNXOnQuickchatBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_QUICKCHAT_AFTER, EventScript.NWNXOnQuickchatAfterScript);

            // Inventory Open events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_INVENTORY_OPEN_BEFORE, EventScript.NWNXOnInventoryOpenBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_INVENTORY_OPEN_AFTER, EventScript.NWNXOnInventoryOpenAfterScript);
            // Inventory Select Panel events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_INVENTORY_SELECT_PANEL_BEFORE, EventScript.NWNXOnInventorySelectPanelBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_INVENTORY_SELECT_PANEL_AFTER, EventScript.NWNXOnInventorySelectPanelAfterScript);

            // Barter Start events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_BARTER_START_BEFORE, EventScript.NWNXOnBarterStartBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_BARTER_START_AFTER, EventScript.NWNXOnBarterStartAfterScript);

            // Barter End events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_BARTER_END_BEFORE, EventScript.NWNXOnBarterEndBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_BARTER_END_AFTER, EventScript.NWNXOnBarterEndAfterScript);

            // Trap events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_TRAP_DISARM_BEFORE, EventScript.NWNXOnTrapDisarmBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_TRAP_DISARM_AFTER, EventScript.NWNXOnTrapDisarmAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_TRAP_ENTER_BEFORE, EventScript.NWNXOnTrapEnterBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_TRAP_ENTER_AFTER, EventScript.NWNXOnTrapEnterAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_TRAP_EXAMINE_BEFORE, EventScript.NWNXOnTrapExamineBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_TRAP_EXAMINE_AFTER, EventScript.NWNXOnTrapExamineAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_TRAP_FLAG_BEFORE, EventScript.NWNXOnTrapFlagBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_TRAP_FLAG_AFTER, EventScript.NWNXOnTrapFlagAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_TRAP_RECOVER_BEFORE, EventScript.NWNXOnTrapRecoverBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_TRAP_RECOVER_AFTER, EventScript.NWNXOnTrapRecoverAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_TRAP_SET_BEFORE, EventScript.NWNXOnTrapSetBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_TRAP_SET_AFTER, EventScript.NWNXOnTrapSetAfterScript);

            // Timing Bar events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_TIMING_BAR_START_BEFORE, EventScript.NWNXOnTimingBarStartBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_TIMING_BAR_START_AFTER, EventScript.NWNXOnTimingBarStartAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_TIMING_BAR_STOP_BEFORE, EventScript.NWNXOnTimingBarStopBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_TIMING_BAR_STOP_AFTER, EventScript.NWNXOnTimingBarStopAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_TIMING_BAR_CANCEL_BEFORE, EventScript.NWNXOnTimingBarCancelBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_TIMING_BAR_CANCEL_AFTER, EventScript.NWNXOnTimingBarCancelAfterScript);

            // Webhook events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_WEBHOOK_SUCCESS, EventScript.NWNXOnWebhookSuccessScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_WEBHOOK_FAILURE, EventScript.NWNXOnWebhookFailureScript);

            // Servervault events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_CHECK_STICKY_PLAYER_NAME_RESERVED_BEFORE, EventScript.NWNXOnCheckStickyPlayerNameReservedBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_CHECK_STICKY_PLAYER_NAME_RESERVED_AFTER, EventScript.NWNXOnCheckStickyPlayerNameReservedAfterScript);

            // Levelling events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_LEVEL_UP_BEFORE, EventScript.NWNXOnLevelUpBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_LEVEL_UP_AFTER, EventScript.NWNXOnLevelUpAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_LEVEL_UP_AUTOMATIC_BEFORE, EventScript.NWNXOnLevelUpAutomaticBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_LEVEL_UP_AUTOMATIC_AFTER, EventScript.NWNXOnLevelUpAutomaticAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_LEVEL_DOWN_BEFORE, EventScript.NWNXOnLevelDownBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_LEVEL_DOWN_AFTER, EventScript.NWNXOnLevelDownAfterScript);
            // Container Change events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_INVENTORY_ADD_ITEM_BEFORE, EventScript.NWNXOnInventoryAddItemBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_INVENTORY_ADD_ITEM_AFTER, EventScript.NWNXOnInventoryAddItemAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_INVENTORY_REMOVE_ITEM_BEFORE, EventScript.NWNXOnInventoryRemoveItemBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_INVENTORY_REMOVE_ITEM_AFTER, EventScript.NWNXOnInventoryRemoveItemAfterScript);

            // Gold events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_INVENTORY_ADD_GOLD_BEFORE, EventScript.NWNXOnInventoryAddGoldBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_INVENTORY_ADD_GOLD_AFTER, EventScript.NWNXOnInventoryAddGoldAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_INVENTORY_REMOVE_GOLD_BEFORE, EventScript.NWNXOnInventoryRemoveGoldBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_INVENTORY_REMOVE_GOLD_AFTER, EventScript.NWNXOnInventoryRemoveGoldAfterScript);

            // PVP Attitude Change events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PVP_ATTITUDE_CHANGE_BEFORE, EventScript.NWNXOnPvpAttitudeChangeBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PVP_ATTITUDE_CHANGE_AFTER, EventScript.NWNXOnPvpAttitudeChangeAfterScript);

            // Input Walk To events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_INPUT_WALK_TO_WAYPOINT_BEFORE, EventScript.NWNXOnInputWalkToWaypointBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_INPUT_WALK_TO_WAYPOINT_AFTER, EventScript.NWNXOnInputWalkToWaypointAfterScript);

            // Material Change events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_MATERIALCHANGE_BEFORE, EventScript.NWNXOnMaterialChangeBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_MATERIALCHANGE_AFTER, EventScript.NWNXOnMaterialChangeAfterScript);

            // Input Attack events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_INPUT_ATTACK_OBJECT_BEFORE, EventScript.NWNXOnInputAttackObjectBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_INPUT_ATTACK_OBJECT_AFTER, EventScript.NWNXOnInputAttackObjectAfterScript);

            // Object Lock events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_OBJECT_LOCK_BEFORE, EventScript.NWNXOnObjectLockBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_OBJECT_LOCK_AFTER, EventScript.NWNXOnObjectLockAfterScript);

            // Object Unlock events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_OBJECT_UNLOCK_BEFORE, EventScript.NWNXOnObjectUnlockBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_OBJECT_UNLOCK_AFTER, EventScript.NWNXOnObjectUnlockAfterScript);

            // UUID Collision events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_UUID_COLLISION_BEFORE, EventScript.NWNXOnUuidCollisionBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_UUID_COLLISION_AFTER, EventScript.NWNXOnUuidCollisionAfterScript);

            // ELC Events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ELC_VALIDATE_CHARACTER_BEFORE, EventScript.NWNXOnElcValidateCharacterBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ELC_VALIDATE_CHARACTER_AFTER, EventScript.NWNXOnElcValidateCharacterAfterScript);

            // Quickbar Events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_QUICKBAR_SET_BUTTON_BEFORE, EventScript.NWNXOnQuickbarSetButtonBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_QUICKBAR_SET_BUTTON_AFTER, EventScript.NWNXOnQuickbarSetButtonAfterScript);

            // Calendar Events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_CALENDAR_HOUR, EventScript.NWNXOnCalendarHourScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_CALENDAR_DAY, EventScript.NWNXOnCalendarDayScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_CALENDAR_MONTH, EventScript.NWNXOnCalendarMonthScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_CALENDAR_YEAR, EventScript.NWNXOnCalendarYearScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_CALENDAR_DAWN, EventScript.NWNXOnCalendarDawnScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_CALENDAR_DUSK, EventScript.NWNXOnCalendarDuskScript);

            // Broadcast Spell Cast Events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_BROADCAST_CAST_SPELL_BEFORE, EventScript.NWNXOnBroadcastCastSpellBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_BROADCAST_CAST_SPELL_AFTER, EventScript.NWNXOnBroadcastCastSpellAfterScript);

            // RunScript Debug Events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DEBUG_RUN_SCRIPT_BEFORE, EventScript.NWNXOnDebugRunScriptBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DEBUG_RUN_SCRIPT_AFTER, EventScript.NWNXOnDebugRunScriptAfterScript);
            
            // RunScriptChunk Debug Events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DEBUG_RUN_SCRIPT_CHUNK_BEFORE, EventScript.NWNXOnDebugRunScriptChunkBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_DEBUG_RUN_SCRIPT_CHUNK_AFTER, EventScript.NWNXOnDebugRunScriptChunkAfterScript);

            // Buy/Sell Store Events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_STORE_REQUEST_BUY_BEFORE, EventScript.NWNXOnStoreRequestBuyBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_STORE_REQUEST_BUY_AFTER, EventScript.NWNXOnStoreRequestBuyAfterScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_STORE_REQUEST_SELL_BEFORE, EventScript.NWNXOnStoreRequestSellBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_STORE_REQUEST_SELL_AFTER, EventScript.NWNXOnStoreRequestSellAfterScript);

            // Input Drop Item Events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_INPUT_DROP_ITEM_BEFORE, EventScript.NWNXOnInputDropItemBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_INPUT_DROP_ITEM_AFTER, EventScript.NWNXOnInputDropItemAfterScript);

            // Broadcast Attack of Opportunity Events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_BROADCAST_ATTACK_OF_OPPORTUNITY_BEFORE, EventScript.NWNXOnBroadcastAttackOfOpportunityBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_BROADCAST_ATTACK_OF_OPPORTUNITY_AFTER, EventScript.NWNXOnBroadcastAttackOfOpportunityAfterScript);

            // Combat Attack of Opportunity Events
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_COMBAT_ATTACK_OF_OPPORTUNITY_BEFORE, EventScript.NWNXOnCombatAttackOfOpportunityBeforeScript);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_COMBAT_ATTACK_OF_OPPORTUNITY_AFTER, EventScript.NWNXOnCombatAttackOfOpportunityAfterScript);

        }

        [Inject]
        public IList<IModulePreloadEvent> OnModulePreloadSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnModulePreloadScript)]
        public void HandleModulePreload() => HandleEvent(OnModulePreloadSubscriptions, (subscription) => subscription.OnModulePreload());
        
        [Inject]
        public IList<IAddAssociateBeforeEvent> OnAddAssociateBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnAddAssociateBeforeScript)]
        public void HandleAddAssociateBefore() => HandleEvent(OnAddAssociateBeforeSubscriptions, (subscription) => subscription.OnAddAssociateBefore());

        [Inject]
        public IList<IAddAssociateAfterEvent> OnAddAssociateAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnAddAssociateAfterScript)]
        public void HandleAddAssociateAfter() => HandleEvent(OnAddAssociateAfterSubscriptions, (subscription) => subscription.OnAddAssociateAfter());

        [Inject]
        public IList<IRemoveAssociateBeforeEvent> OnRemoveAssociateBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnRemoveAssociateBeforeScript)]
        public void HandleRemoveAssociateBefore() => HandleEvent(OnRemoveAssociateBeforeSubscriptions, (subscription) => subscription.OnRemoveAssociateBefore());

        [Inject]
        public IList<IRemoveAssociateAfterEvent> OnRemoveAssociateAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnRemoveAssociateAfterScript)]
        public void HandleRemoveAssociateAfter() => HandleEvent(OnRemoveAssociateAfterSubscriptions, (subscription) => subscription.OnRemoveAssociateAfter());

        [Inject]
        public IList<IStealthEnterBeforeEvent> OnStealthEnterBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnStealthEnterBeforeScript)]
        public void HandleStealthEnterBefore() => HandleEvent(OnStealthEnterBeforeSubscriptions, (subscription) => subscription.OnStealthEnterBefore());

        [Inject]
        public IList<IStealthEnterAfterEvent> OnStealthEnterAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnStealthEnterAfterScript)]
        public void HandleStealthEnterAfter() => HandleEvent(OnStealthEnterAfterSubscriptions, (subscription) => subscription.OnStealthEnterAfter());

        [Inject]
        public IList<IStealthExitBeforeEvent> OnStealthExitBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnStealthExitBeforeScript)]
        public void HandleStealthExitBefore() => HandleEvent(OnStealthExitBeforeSubscriptions, (subscription) => subscription.OnStealthExitBefore());

        [Inject]
        public IList<IStealthExitAfterEvent> OnStealthExitAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnStealthExitAfterScript)]
        public void HandleStealthExitAfter() => HandleEvent(OnStealthExitAfterSubscriptions, (subscription) => subscription.OnStealthExitAfter());

        [Inject]
        public IList<IExamineObjectBeforeEvent> OnExamineObjectBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnExamineObjectBeforeScript)]
        public void HandleExamineObjectBefore() => HandleEvent(OnExamineObjectBeforeSubscriptions, (subscription) => subscription.OnExamineObjectBefore());

        [Inject]
        public IList<IExamineObjectAfterEvent> OnExamineObjectAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnExamineObjectAfterScript)]
        public void HandleExamineObjectAfter() => HandleEvent(OnExamineObjectAfterSubscriptions, (subscription) => subscription.OnExamineObjectAfter());

        [Inject]
        public IList<IValidateUseItemBeforeEvent> OnValidateUseItemBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnValidateUseItemBeforeScript)]
        public void HandleValidateUseItemBefore() => HandleEvent(OnValidateUseItemBeforeSubscriptions, (subscription) => subscription.OnValidateUseItemBefore());

        [Inject]
        public IList<IValidateUseItemAfterEvent> OnValidateUseItemAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnValidateUseItemAfterScript)]
        public void HandleValidateUseItemAfter() => HandleEvent(OnValidateUseItemAfterSubscriptions, (subscription) => subscription.OnValidateUseItemAfter());

        [Inject]
        public IList<IUseItemBeforeEvent> OnUseItemBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnUseItemBeforeScript)]
        public void HandleUseItemBefore() => HandleEvent(OnUseItemBeforeSubscriptions, (subscription) => subscription.OnUseItemBefore());

        [Inject]
        public IList<IUseItemAfterEvent> OnUseItemAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnUseItemAfterScript)]
        public void HandleUseItemAfter() => HandleEvent(OnUseItemAfterSubscriptions, (subscription) => subscription.OnUseItemAfter());

        [Inject]
        public IList<IItemInventoryOpenBeforeEvent> OnItemInventoryOpenBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemInventoryOpenBeforeScript)]
        public void HandleItemInventoryOpenBefore() => HandleEvent(OnItemInventoryOpenBeforeSubscriptions, (subscription) => subscription.OnItemInventoryOpenBefore());

        [Inject]
        public IList<IItemInventoryOpenAfterEvent> OnItemInventoryOpenAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemInventoryOpenAfterScript)]
        public void HandleItemInventoryOpenAfter() => HandleEvent(OnItemInventoryOpenAfterSubscriptions, (subscription) => subscription.OnItemInventoryOpenAfter());

        [Inject]
        public IList<IItemInventoryCloseBeforeEvent> OnItemInventoryCloseBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemInventoryCloseBeforeScript)]
        public void HandleItemInventoryCloseBefore() => HandleEvent(OnItemInventoryCloseBeforeSubscriptions, (subscription) => subscription.OnItemInventoryCloseBefore());

        [Inject]
        public IList<IItemInventoryCloseAfterEvent> OnItemInventoryCloseAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemInventoryCloseAfterScript)]
        public void HandleItemInventoryCloseAfter() => HandleEvent(OnItemInventoryCloseAfterSubscriptions, (subscription) => subscription.OnItemInventoryCloseAfter());

        [Inject]
        public IList<IItemAmmoReloadBeforeEvent> OnItemAmmoReloadBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemAmmoReloadBeforeScript)]
        public void HandleItemAmmoReloadBefore() => HandleEvent(OnItemAmmoReloadBeforeSubscriptions, (subscription) => subscription.OnItemAmmoReloadBefore());

        [Inject]
        public IList<IItemAmmoReloadAfterEvent> OnItemAmmoReloadAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemAmmoReloadAfterScript)]
        public void HandleItemAmmoReloadAfter() => HandleEvent(OnItemAmmoReloadAfterSubscriptions, (subscription) => subscription.OnItemAmmoReloadAfter());

        [Inject]
        public IList<IItemScrollLearnBeforeEvent> OnItemScrollLearnBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemScrollLearnBeforeScript)]
        public void HandleItemScrollLearnBefore() => HandleEvent(OnItemScrollLearnBeforeSubscriptions, (subscription) => subscription.OnItemScrollLearnBefore());

        [Inject]
        public IList<IItemScrollLearnAfterEvent> OnItemScrollLearnAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemScrollLearnAfterScript)]
        public void HandleItemScrollLearnAfter() => HandleEvent(OnItemScrollLearnAfterSubscriptions, (subscription) => subscription.OnItemScrollLearnAfter());

        [Inject]
        public IList<IValidateItemEquipBeforeEvent> OnValidateItemEquipBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnValidateItemEquipBeforeScript)]
        public void HandleValidateItemEquipBefore() => HandleEvent(OnValidateItemEquipBeforeSubscriptions, (subscription) => subscription.OnValidateItemEquipBefore());

        [Inject]
        public IList<IValidateItemEquipAfterEvent> OnValidateItemEquipAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnValidateItemEquipAfterScript)]
        public void HandleValidateItemEquipAfter() => HandleEvent(OnValidateItemEquipAfterSubscriptions, (subscription) => subscription.OnValidateItemEquipAfter());

        [Inject]
        public IList<IItemEquipBeforeEvent> OnItemEquipBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemEquipBeforeScript)]
        public void HandleItemEquipBefore() => HandleEvent(OnItemEquipBeforeSubscriptions, (subscription) => subscription.OnItemEquipBefore());

        [Inject]
        public IList<IItemEquipAfterEvent> OnItemEquipAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemEquipAfterScript)]
        public void HandleItemEquipAfter() => HandleEvent(OnItemEquipAfterSubscriptions, (subscription) => subscription.OnItemEquipAfter());

        [Inject]
        public IList<IItemUnequipBeforeEvent> OnItemUnequipBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemUnequipBeforeScript)]
        public void HandleItemUnequipBefore() => HandleEvent(OnItemUnequipBeforeSubscriptions, (subscription) => subscription.OnItemUnequipBefore());

        [Inject]
        public IList<IItemUnequipAfterEvent> OnItemUnequipAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemUnequipAfterScript)]
        public void HandleItemUnequipAfter() => HandleEvent(OnItemUnequipAfterSubscriptions, (subscription) => subscription.OnItemUnequipAfter());

        [Inject]
        public IList<IItemDestroyObjectBeforeEvent> OnItemDestroyObjectBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemDestroyObjectBeforeScript)]
        public void HandleItemDestroyObjectBefore() => HandleEvent(OnItemDestroyObjectBeforeSubscriptions, (subscription) => subscription.OnItemDestroyObjectBefore());

        [Inject]
        public IList<IItemDestroyObjectAfterEvent> OnItemDestroyObjectAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemDestroyObjectAfterScript)]
        public void HandleItemDestroyObjectAfter() => HandleEvent(OnItemDestroyObjectAfterSubscriptions, (subscription) => subscription.OnItemDestroyObjectAfter());

        [Inject]
        public IList<IItemDecrementStacksizeBeforeEvent> OnItemDecrementStacksizeBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemDecrementStacksizeBeforeScript)]
        public void HandleItemDecrementStacksizeBefore() => HandleEvent(OnItemDecrementStacksizeBeforeSubscriptions, (subscription) => subscription.OnItemDecrementStacksizeBefore());

        [Inject]
        public IList<IItemDecrementStacksizeAfterEvent> OnItemDecrementStacksizeAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemDecrementStacksizeAfterScript)]
        public void HandleItemDecrementStacksizeAfter() => HandleEvent(OnItemDecrementStacksizeAfterSubscriptions, (subscription) => subscription.OnItemDecrementStacksizeAfter());

        [Inject]
        public IList<IItemUseLoreBeforeEvent> OnItemUseLoreBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemUseLoreBeforeScript)]
        public void HandleItemUseLoreBefore() => HandleEvent(OnItemUseLoreBeforeSubscriptions, (subscription) => subscription.OnItemUseLoreBefore());

        [Inject]
        public IList<IItemUseLoreAfterEvent> OnItemUseLoreAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemUseLoreAfterScript)]
        public void HandleItemUseLoreAfter() => HandleEvent(OnItemUseLoreAfterSubscriptions, (subscription) => subscription.OnItemUseLoreAfter());


        [Inject]
        public IList<IItemPayToIdentifyBeforeEvent> OnItemPayToIdentifyBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemPayToIdentifyBeforeScript)]
        public void HandleItemPayToIdentifyBefore() => HandleEvent(OnItemPayToIdentifyBeforeSubscriptions, (subscription) => subscription.OnItemPayToIdentifyBefore());

        [Inject]
        public IList<IItemPayToIdentifyAfterEvent> OnItemPayToIdentifyAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemPayToIdentifyAfterScript)]
        public void HandleItemPayToIdentifyAfter() => HandleEvent(OnItemPayToIdentifyAfterSubscriptions, (subscription) => subscription.OnItemPayToIdentifyAfter());


        [Inject]
        public IList<IItemSplitBeforeEvent> OnItemSplitBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemSplitBeforeScript)]
        public void HandleItemSplitBefore() => HandleEvent(OnItemSplitBeforeSubscriptions, (subscription) => subscription.OnItemSplitBefore());

        [Inject]
        public IList<IItemSplitAfterEvent> OnItemSplitAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemSplitAfterScript)]
        public void HandleItemSplitAfter() => HandleEvent(OnItemSplitAfterSubscriptions, (subscription) => subscription.OnItemSplitAfter());


        [Inject]
        public IList<IItemMergeBeforeEvent> OnItemMergeBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemMergeBeforeScript)]
        public void HandleItemMergeBefore() => HandleEvent(OnItemMergeBeforeSubscriptions, (subscription) => subscription.OnItemMergeBefore());

        [Inject]
        public IList<IItemMergeAfterEvent> OnItemMergeAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemMergeAfterScript)]
        public void HandleItemMergeAfter() => HandleEvent(OnItemMergeAfterSubscriptions, (subscription) => subscription.OnItemMergeAfter());


        [Inject]
        public IList<IItemAcquireBeforeEvent> OnItemAcquireBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemAcquireBeforeScript)]
        public void HandleItemAcquireBefore() => HandleEvent(OnItemAcquireBeforeSubscriptions, (subscription) => subscription.OnItemAcquireBefore());

        [Inject]
        public IList<IItemAcquireAfterEvent> OnItemAcquireAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnItemAcquireAfterScript)]
        public void HandleItemAcquireAfter() => HandleEvent(OnItemAcquireAfterSubscriptions, (subscription) => subscription.OnItemAcquireAfter());


        [Inject]
        public IList<IUseFeatBeforeEvent> OnUseFeatBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnUseFeatBeforeScript)]
        public void HandleUseFeatBefore() => HandleEvent(OnUseFeatBeforeSubscriptions, (subscription) => subscription.OnUseFeatBefore());

        [Inject]
        public IList<IUseFeatAfterEvent> OnUseFeatAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnUseFeatAfterScript)]
        public void HandleUseFeatAfter() => HandleEvent(OnUseFeatAfterSubscriptions, (subscription) => subscription.OnUseFeatAfter());
        [Inject]
        public IList<IDmGiveGoldBeforeEvent> OnDmGiveGoldBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmGiveGoldBeforeScript)]
        public void HandleDmGiveGoldBefore() => HandleEvent(OnDmGiveGoldBeforeSubscriptions, (subscription) => subscription.OnDmGiveGoldBefore());

        [Inject]
        public IList<IDmGiveGoldAfterEvent> OnDmGiveGoldAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmGiveGoldAfterScript)]
        public void HandleDmGiveGoldAfter() => HandleEvent(OnDmGiveGoldAfterSubscriptions, (subscription) => subscription.OnDmGiveGoldAfter());

        [Inject]
        public IList<IDmGiveXpBeforeEvent> OnDmGiveXpBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmGiveXpBeforeScript)]
        public void HandleDmGiveXpBefore() => HandleEvent(OnDmGiveXpBeforeSubscriptions, (subscription) => subscription.OnDmGiveXpBefore());

        [Inject]
        public IList<IDmGiveXpAfterEvent> OnDmGiveXpAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmGiveXpAfterScript)]
        public void HandleDmGiveXpAfter() => HandleEvent(OnDmGiveXpAfterSubscriptions, (subscription) => subscription.OnDmGiveXpAfter());

        [Inject]
        public IList<IDmGiveLevelBeforeEvent> OnDmGiveLevelBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmGiveLevelBeforeScript)]
        public void HandleDmGiveLevelBefore() => HandleEvent(OnDmGiveLevelBeforeSubscriptions, (subscription) => subscription.OnDmGiveLevelBefore());

        [Inject]
        public IList<IDmGiveLevelAfterEvent> OnDmGiveLevelAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmGiveLevelAfterScript)]
        public void HandleDmGiveLevelAfter() => HandleEvent(OnDmGiveLevelAfterSubscriptions, (subscription) => subscription.OnDmGiveLevelAfter());

        [Inject]
        public IList<IDmGiveAlignmentBeforeEvent> OnDmGiveAlignmentBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmGiveAlignmentBeforeScript)]
        public void HandleDmGiveAlignmentBefore() => HandleEvent(OnDmGiveAlignmentBeforeSubscriptions, (subscription) => subscription.OnDmGiveAlignmentBefore());

        [Inject]
        public IList<IDmGiveAlignmentAfterEvent> OnDmGiveAlignmentAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmGiveAlignmentAfterScript)]
        public void HandleDmGiveAlignmentAfter() => HandleEvent(OnDmGiveAlignmentAfterSubscriptions, (subscription) => subscription.OnDmGiveAlignmentAfter());

        [Inject]
        public IList<IDmSpawnObjectBeforeEvent> OnDmSpawnObjectBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmSpawnObjectBeforeScript)]
        public void HandleDmSpawnObjectBefore() => HandleEvent(OnDmSpawnObjectBeforeSubscriptions, (subscription) => subscription.OnDmSpawnObjectBefore());

        [Inject]
        public IList<IDmSpawnObjectAfterEvent> OnDmSpawnObjectAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmSpawnObjectAfterScript)]
        public void HandleDmSpawnObjectAfter() => HandleEvent(OnDmSpawnObjectAfterSubscriptions, (subscription) => subscription.OnDmSpawnObjectAfter());

        [Inject]
        public IList<IDmGiveItemBeforeEvent> OnDmGiveItemBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmGiveItemBeforeScript)]
        public void HandleDmGiveItemBefore() => HandleEvent(OnDmGiveItemBeforeSubscriptions, (subscription) => subscription.OnDmGiveItemBefore());

        [Inject]
        public IList<IDmGiveItemAfterEvent> OnDmGiveItemAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmGiveItemAfterScript)]
        public void HandleDmGiveItemAfter() => HandleEvent(OnDmGiveItemAfterSubscriptions, (subscription) => subscription.OnDmGiveItemAfter());
        [Inject]
        public IList<IDmHealBeforeEvent> OnDmHealBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmHealBeforeScript)]
        public void HandleDmHealBefore() => HandleEvent(OnDmHealBeforeSubscriptions, (subscription) => subscription.OnDmHealBefore());

        [Inject]
        public IList<IDmHealAfterEvent> OnDmHealAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmHealAfterScript)]
        public void HandleDmHealAfter() => HandleEvent(OnDmHealAfterSubscriptions, (subscription) => subscription.OnDmHealAfter());

        [Inject]
        public IList<IDmKillBeforeEvent> OnDmKillBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmKillBeforeScript)]
        public void HandleDmKillBefore() => HandleEvent(OnDmKillBeforeSubscriptions, (subscription) => subscription.OnDmKillBefore());

        [Inject]
        public IList<IDmKillAfterEvent> OnDmKillAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmKillAfterScript)]
        public void HandleDmKillAfter() => HandleEvent(OnDmKillAfterSubscriptions, (subscription) => subscription.OnDmKillAfter());

        [Inject]
        public IList<IDmToggleInvulnerableBeforeEvent> OnDmToggleInvulnerableBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmToggleInvulnerableBeforeScript)]
        public void HandleDmToggleInvulnerableBefore() => HandleEvent(OnDmToggleInvulnerableBeforeSubscriptions, (subscription) => subscription.OnDmToggleInvulnerableBefore());

        [Inject]
        public IList<IDmToggleInvulnerableAfterEvent> OnDmToggleInvulnerableAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmToggleInvulnerableAfterScript)]
        public void HandleDmToggleInvulnerableAfter() => HandleEvent(OnDmToggleInvulnerableAfterSubscriptions, (subscription) => subscription.OnDmToggleInvulnerableAfter());

        [Inject]
        public IList<IDmForceRestBeforeEvent> OnDmForceRestBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmForceRestBeforeScript)]
        public void HandleDmForceRestBefore() => HandleEvent(OnDmForceRestBeforeSubscriptions, (subscription) => subscription.OnDmForceRestBefore());

        [Inject]
        public IList<IDmForceRestAfterEvent> OnDmForceRestAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmForceRestAfterScript)]
        public void HandleDmForceRestAfter() => HandleEvent(OnDmForceRestAfterSubscriptions, (subscription) => subscription.OnDmForceRestAfter());

        [Inject]
        public IList<IDmLimboBeforeEvent> OnDmLimboBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmLimboBeforeScript)]
        public void HandleDmLimboBefore() => HandleEvent(OnDmLimboBeforeSubscriptions, (subscription) => subscription.OnDmLimboBefore());

        [Inject]
        public IList<IDmLimboAfterEvent> OnDmLimboAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmLimboAfterScript)]
        public void HandleDmLimboAfter() => HandleEvent(OnDmLimboAfterSubscriptions, (subscription) => subscription.OnDmLimboAfter());

        [Inject]
        public IList<IDmToggleAiBeforeEvent> OnDmToggleAiBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmToggleAiBeforeScript)]
        public void HandleDmToggleAiBefore() => HandleEvent(OnDmToggleAiBeforeSubscriptions, (subscription) => subscription.OnDmToggleAiBefore());

        [Inject]
        public IList<IDmToggleAiAfterEvent> OnDmToggleAiAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmToggleAiAfterScript)]
        public void HandleDmToggleAiAfter() => HandleEvent(OnDmToggleAiAfterSubscriptions, (subscription) => subscription.OnDmToggleAiAfter());

        [Inject]
        public IList<IDmToggleImmortalBeforeEvent> OnDmToggleImmortalBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmToggleImmortalBeforeScript)]
        public void HandleDmToggleImmortalBefore() => HandleEvent(OnDmToggleImmortalBeforeSubscriptions, (subscription) => subscription.OnDmToggleImmortalBefore());

        [Inject]
        public IList<IDmToggleImmortalAfterEvent> OnDmToggleImmortalAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmToggleImmortalAfterScript)]
        public void HandleDmToggleImmortalAfter() => HandleEvent(OnDmToggleImmortalAfterSubscriptions, (subscription) => subscription.OnDmToggleImmortalAfter());
        [Inject]
        public IList<IDmGotoBeforeEvent> OnDmGotoBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmGotoBeforeScript)]
        public void HandleDmGotoBefore() => HandleEvent(OnDmGotoBeforeSubscriptions, (subscription) => subscription.OnDmGotoBefore());

        [Inject]
        public IList<IDmGotoAfterEvent> OnDmGotoAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmGotoAfterScript)]
        public void HandleDmGotoAfter() => HandleEvent(OnDmGotoAfterSubscriptions, (subscription) => subscription.OnDmGotoAfter());

        [Inject]
        public IList<IDmPossessBeforeEvent> OnDmPossessBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmPossessBeforeScript)]
        public void HandleDmPossessBefore() => HandleEvent(OnDmPossessBeforeSubscriptions, (subscription) => subscription.OnDmPossessBefore());

        [Inject]
        public IList<IDmPossessAfterEvent> OnDmPossessAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmPossessAfterScript)]
        public void HandleDmPossessAfter() => HandleEvent(OnDmPossessAfterSubscriptions, (subscription) => subscription.OnDmPossessAfter());

        [Inject]
        public IList<IDmPossessFullPowerBeforeEvent> OnDmPossessFullPowerBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmPossessFullPowerBeforeScript)]
        public void HandleDmPossessFullPowerBefore() => HandleEvent(OnDmPossessFullPowerBeforeSubscriptions, (subscription) => subscription.OnDmPossessFullPowerBefore());

        [Inject]
        public IList<IDmPossessFullPowerAfterEvent> OnDmPossessFullPowerAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmPossessFullPowerAfterScript)]
        public void HandleDmPossessFullPowerAfter() => HandleEvent(OnDmPossessFullPowerAfterSubscriptions, (subscription) => subscription.OnDmPossessFullPowerAfter());

        [Inject]
        public IList<IDmToggleLockBeforeEvent> OnDmToggleLockBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmToggleLockBeforeScript)]
        public void HandleDmToggleLockBefore() => HandleEvent(OnDmToggleLockBeforeSubscriptions, (subscription) => subscription.OnDmToggleLockBefore());

        [Inject]
        public IList<IDmToggleLockAfterEvent> OnDmToggleLockAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmToggleLockAfterScript)]
        public void HandleDmToggleLockAfter() => HandleEvent(OnDmToggleLockAfterSubscriptions, (subscription) => subscription.OnDmToggleLockAfter());

        [Inject]
        public IList<IDmDisableTrapBeforeEvent> OnDmDisableTrapBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmDisableTrapBeforeScript)]
        public void HandleDmDisableTrapBefore() => HandleEvent(OnDmDisableTrapBeforeSubscriptions, (subscription) => subscription.OnDmDisableTrapBefore());

        [Inject]
        public IList<IDmDisableTrapAfterEvent> OnDmDisableTrapAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmDisableTrapAfterScript)]
        public void HandleDmDisableTrapAfter() => HandleEvent(OnDmDisableTrapAfterSubscriptions, (subscription) => subscription.OnDmDisableTrapAfter());

        [Inject]
        public IList<IDmJumpToPointBeforeEvent> OnDmJumpToPointBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmJumpToPointBeforeScript)]
        public void HandleDmJumpToPointBefore() => HandleEvent(OnDmJumpToPointBeforeSubscriptions, (subscription) => subscription.OnDmJumpToPointBefore());

        [Inject]
        public IList<IDmJumpToPointAfterEvent> OnDmJumpToPointAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmJumpToPointAfterScript)]
        public void HandleDmJumpToPointAfter() => HandleEvent(OnDmJumpToPointAfterSubscriptions, (subscription) => subscription.OnDmJumpToPointAfter());

        [Inject]
        public IList<IDmJumpTargetToPointBeforeEvent> OnDmJumpTargetToPointBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmJumpTargetToPointBeforeScript)]
        public void HandleDmJumpTargetToPointBefore() => HandleEvent(OnDmJumpTargetToPointBeforeSubscriptions, (subscription) => subscription.OnDmJumpTargetToPointBefore());

        [Inject]
        public IList<IDmJumpTargetToPointAfterEvent> OnDmJumpTargetToPointAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmJumpTargetToPointAfterScript)]
        public void HandleDmJumpTargetToPointAfter() => HandleEvent(OnDmJumpTargetToPointAfterSubscriptions, (subscription) => subscription.OnDmJumpTargetToPointAfter());

        [Inject]
        public IList<IDmJumpAllPlayersToPointBeforeEvent> OnDmJumpAllPlayersToPointBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmJumpAllPlayersToPointBeforeScript)]
        public void HandleDmJumpAllPlayersToPointBefore() => HandleEvent(OnDmJumpAllPlayersToPointBeforeSubscriptions, (subscription) => subscription.OnDmJumpAllPlayersToPointBefore());

        [Inject]
        public IList<IDmJumpAllPlayersToPointAfterEvent> OnDmJumpAllPlayersToPointAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmJumpAllPlayersToPointAfterScript)]
        public void HandleDmJumpAllPlayersToPointAfter() => HandleEvent(OnDmJumpAllPlayersToPointAfterSubscriptions, (subscription) => subscription.OnDmJumpAllPlayersToPointAfter());
        [Inject]
        public IList<IDmChangeDifficultyBeforeEvent> OnDmChangeDifficultyBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmChangeDifficultyBeforeScript)]
        public void HandleDmChangeDifficultyBefore() => HandleEvent(OnDmChangeDifficultyBeforeSubscriptions, (subscription) => subscription.OnDmChangeDifficultyBefore());

        [Inject]
        public IList<IDmChangeDifficultyAfterEvent> OnDmChangeDifficultyAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmChangeDifficultyAfterScript)]
        public void HandleDmChangeDifficultyAfter() => HandleEvent(OnDmChangeDifficultyAfterSubscriptions, (subscription) => subscription.OnDmChangeDifficultyAfter());

        [Inject]
        public IList<IDmViewInventoryBeforeEvent> OnDmViewInventoryBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmViewInventoryBeforeScript)]
        public void HandleDmViewInventoryBefore() => HandleEvent(OnDmViewInventoryBeforeSubscriptions, (subscription) => subscription.OnDmViewInventoryBefore());

        [Inject]
        public IList<IDmViewInventoryAfterEvent> OnDmViewInventoryAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmViewInventoryAfterScript)]
        public void HandleDmViewInventoryAfter() => HandleEvent(OnDmViewInventoryAfterSubscriptions, (subscription) => subscription.OnDmViewInventoryAfter());

        [Inject]
        public IList<IDmSpawnTrapOnObjectBeforeEvent> OnDmSpawnTrapOnObjectBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmSpawnTrapOnObjectBeforeScript)]
        public void HandleDmSpawnTrapOnObjectBefore() => HandleEvent(OnDmSpawnTrapOnObjectBeforeSubscriptions, (subscription) => subscription.OnDmSpawnTrapOnObjectBefore());

        [Inject]
        public IList<IDmSpawnTrapOnObjectAfterEvent> OnDmSpawnTrapOnObjectAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmSpawnTrapOnObjectAfterScript)]
        public void HandleDmSpawnTrapOnObjectAfter() => HandleEvent(OnDmSpawnTrapOnObjectAfterSubscriptions, (subscription) => subscription.OnDmSpawnTrapOnObjectAfter());

        [Inject]
        public IList<IDmDumpLocalsBeforeEvent> OnDmDumpLocalsBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmDumpLocalsBeforeScript)]
        public void HandleDmDumpLocalsBefore() => HandleEvent(OnDmDumpLocalsBeforeSubscriptions, (subscription) => subscription.OnDmDumpLocalsBefore());

        [Inject]
        public IList<IDmDumpLocalsAfterEvent> OnDmDumpLocalsAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmDumpLocalsAfterScript)]
        public void HandleDmDumpLocalsAfter() => HandleEvent(OnDmDumpLocalsAfterSubscriptions, (subscription) => subscription.OnDmDumpLocalsAfter());

        [Inject]
        public IList<IDmAppearBeforeEvent> OnDmAppearBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmAppearBeforeScript)]
        public void HandleDmAppearBefore() => HandleEvent(OnDmAppearBeforeSubscriptions, (subscription) => subscription.OnDmAppearBefore());

        [Inject]
        public IList<IDmAppearAfterEvent> OnDmAppearAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmAppearAfterScript)]
        public void HandleDmAppearAfter() => HandleEvent(OnDmAppearAfterSubscriptions, (subscription) => subscription.OnDmAppearAfter());

        [Inject]
        public IList<IDmDisappearBeforeEvent> OnDmDisappearBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmDisappearBeforeScript)]
        public void HandleDmDisappearBefore() => HandleEvent(OnDmDisappearBeforeSubscriptions, (subscription) => subscription.OnDmDisappearBefore());

        [Inject]
        public IList<IDmDisappearAfterEvent> OnDmDisappearAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmDisappearAfterScript)]
        public void HandleDmDisappearAfter() => HandleEvent(OnDmDisappearAfterSubscriptions, (subscription) => subscription.OnDmDisappearAfter());

        [Inject]
        public IList<IDmSetFactionBeforeEvent> OnDmSetFactionBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmSetFactionBeforeScript)]
        public void HandleDmSetFactionBefore() => HandleEvent(OnDmSetFactionBeforeSubscriptions, (subscription) => subscription.OnDmSetFactionBefore());

        [Inject]
        public IList<IDmSetFactionAfterEvent> OnDmSetFactionAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmSetFactionAfterScript)]
        public void HandleDmSetFactionAfter() => HandleEvent(OnDmSetFactionAfterSubscriptions, (subscription) => subscription.OnDmSetFactionAfter());
        [Inject]
        public IList<IDmTakeItemBeforeEvent> OnDmTakeItemBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmTakeItemBeforeScript)]
        public void HandleDmTakeItemBefore() => HandleEvent(OnDmTakeItemBeforeSubscriptions, (subscription) => subscription.OnDmTakeItemBefore());

        [Inject]
        public IList<IDmTakeItemAfterEvent> OnDmTakeItemAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmTakeItemAfterScript)]
        public void HandleDmTakeItemAfter() => HandleEvent(OnDmTakeItemAfterSubscriptions, (subscription) => subscription.OnDmTakeItemAfter());

        [Inject]
        public IList<IDmSetStatBeforeEvent> OnDmSetStatBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmSetStatBeforeScript)]
        public void HandleDmSetStatBefore() => HandleEvent(OnDmSetStatBeforeSubscriptions, (subscription) => subscription.OnDmSetStatBefore());

        [Inject]
        public IList<IDmSetStatAfterEvent> OnDmSetStatAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmSetStatAfterScript)]
        public void HandleDmSetStatAfter() => HandleEvent(OnDmSetStatAfterSubscriptions, (subscription) => subscription.OnDmSetStatAfter());

        [Inject]
        public IList<IDmGetVariableBeforeEvent> OnDmGetVariableBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmGetVariableBeforeScript)]
        public void HandleDmGetVariableBefore() => HandleEvent(OnDmGetVariableBeforeSubscriptions, (subscription) => subscription.OnDmGetVariableBefore());

        [Inject]
        public IList<IDmGetVariableAfterEvent> OnDmGetVariableAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmGetVariableAfterScript)]
        public void HandleDmGetVariableAfter() => HandleEvent(OnDmGetVariableAfterSubscriptions, (subscription) => subscription.OnDmGetVariableAfter());

        [Inject]
        public IList<IDmSetVariableBeforeEvent> OnDmSetVariableBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmSetVariableBeforeScript)]
        public void HandleDmSetVariableBefore() => HandleEvent(OnDmSetVariableBeforeSubscriptions, (subscription) => subscription.OnDmSetVariableBefore());

        [Inject]
        public IList<IDmSetVariableAfterEvent> OnDmSetVariableAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmSetVariableAfterScript)]
        public void HandleDmSetVariableAfter() => HandleEvent(OnDmSetVariableAfterSubscriptions, (subscription) => subscription.OnDmSetVariableAfter());

        [Inject]
        public IList<IDmSetTimeBeforeEvent> OnDmSetTimeBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmSetTimeBeforeScript)]
        public void HandleDmSetTimeBefore() => HandleEvent(OnDmSetTimeBeforeSubscriptions, (subscription) => subscription.OnDmSetTimeBefore());

        [Inject]
        public IList<IDmSetTimeAfterEvent> OnDmSetTimeAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmSetTimeAfterScript)]
        public void HandleDmSetTimeAfter() => HandleEvent(OnDmSetTimeAfterSubscriptions, (subscription) => subscription.OnDmSetTimeAfter());

        [Inject]
        public IList<IDmSetDateBeforeEvent> OnDmSetDateBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmSetDateBeforeScript)]
        public void HandleDmSetDateBefore() => HandleEvent(OnDmSetDateBeforeSubscriptions, (subscription) => subscription.OnDmSetDateBefore());

        [Inject]
        public IList<IDmSetDateAfterEvent> OnDmSetDateAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmSetDateAfterScript)]
        public void HandleDmSetDateAfter() => HandleEvent(OnDmSetDateAfterSubscriptions, (subscription) => subscription.OnDmSetDateAfter());

        [Inject]
        public IList<IDmSetFactionReputationBeforeEvent> OnDmSetFactionReputationBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmSetFactionReputationBeforeScript)]
        public void HandleDmSetFactionReputationBefore() => HandleEvent(OnDmSetFactionReputationBeforeSubscriptions, (subscription) => subscription.OnDmSetFactionReputationBefore());

        [Inject]
        public IList<IDmSetFactionReputationAfterEvent> OnDmSetFactionReputationAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmSetFactionReputationAfterScript)]
        public void HandleDmSetFactionReputationAfter() => HandleEvent(OnDmSetFactionReputationAfterSubscriptions, (subscription) => subscription.OnDmSetFactionReputationAfter());

        [Inject]
        public IList<IDmGetFactionReputationBeforeEvent> OnDmGetFactionReputationBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmGetFactionReputationBeforeScript)]
        public void HandleDmGetFactionReputationBefore() => HandleEvent(OnDmGetFactionReputationBeforeSubscriptions, (subscription) => subscription.OnDmGetFactionReputationBefore());

        [Inject]
        public IList<IDmGetFactionReputationAfterEvent> OnDmGetFactionReputationAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDmGetFactionReputationAfterScript)]
        public void HandleDmGetFactionReputationAfter() => HandleEvent(OnDmGetFactionReputationAfterSubscriptions, (subscription) => subscription.OnDmGetFactionReputationAfter());

        [Inject]
        public IList<IClientDisconnectBeforeEvent> OnClientDisconnectBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnClientDisconnectBeforeScript)]
        public void HandleClientDisconnectBefore() => HandleEvent(OnClientDisconnectBeforeSubscriptions, (subscription) => subscription.OnClientDisconnectBefore());

        [Inject]
        public IList<IClientDisconnectAfterEvent> OnClientDisconnectAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnClientDisconnectAfterScript)]
        public void HandleClientDisconnectAfter() => HandleEvent(OnClientDisconnectAfterSubscriptions, (subscription) => subscription.OnClientDisconnectAfter());

        [Inject]
        public IList<IClientConnectBeforeEvent> OnClientConnectBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnClientConnectBeforeScript)]
        public void HandleClientConnectBefore() => HandleEvent(OnClientConnectBeforeSubscriptions, (subscription) => subscription.OnClientConnectBefore());

        [Inject]
        public IList<IClientConnectAfterEvent> OnClientConnectAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnClientConnectAfterScript)]
        public void HandleClientConnectAfter() => HandleEvent(OnClientConnectAfterSubscriptions, (subscription) => subscription.OnClientConnectAfter());

        [Inject]
        public IList<IStartCombatRoundBeforeEvent> OnStartCombatRoundBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnStartCombatRoundBeforeScript)]
        public void HandleStartCombatRoundBefore() => HandleEvent(OnStartCombatRoundBeforeSubscriptions, (subscription) => subscription.OnStartCombatRoundBefore());

        [Inject]
        public IList<IStartCombatRoundAfterEvent> OnStartCombatRoundAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnStartCombatRoundAfterScript)]
        public void HandleStartCombatRoundAfter() => HandleEvent(OnStartCombatRoundAfterSubscriptions, (subscription) => subscription.OnStartCombatRoundAfter());
        [Inject]
        public IList<ICastSpellBeforeEvent> OnCastSpellBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnCastSpellBeforeScript)]
        public void HandleCastSpellBefore() => HandleEvent(OnCastSpellBeforeSubscriptions, (subscription) => subscription.OnCastSpellBefore());

        [Inject]
        public IList<ICastSpellAfterEvent> OnCastSpellAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnCastSpellAfterScript)]
        public void HandleCastSpellAfter() => HandleEvent(OnCastSpellAfterSubscriptions, (subscription) => subscription.OnCastSpellAfter());

        [Inject]
        public IList<ISetMemorizedSpellSlotBeforeEvent> OnSetMemorizedSpellSlotBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXSetMemorizedSpellSlotBeforeScript)]
        public void HandleSetMemorizedSpellSlotBefore() => HandleEvent(OnSetMemorizedSpellSlotBeforeSubscriptions, (subscription) => subscription.OnSetMemorizedSpellSlotBefore());

        [Inject]
        public IList<ISetMemorizedSpellSlotAfterEvent> OnSetMemorizedSpellSlotAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXSetMemorizedSpellSlotAfterScript)]
        public void HandleSetMemorizedSpellSlotAfter() => HandleEvent(OnSetMemorizedSpellSlotAfterSubscriptions, (subscription) => subscription.OnSetMemorizedSpellSlotAfter());

        [Inject]
        public IList<IClearMemorizedSpellSlotBeforeEvent> OnClearMemorizedSpellSlotBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXClearMemorizedSpellSlotBeforeScript)]
        public void HandleClearMemorizedSpellSlotBefore() => HandleEvent(OnClearMemorizedSpellSlotBeforeSubscriptions, (subscription) => subscription.OnClearMemorizedSpellSlotBefore());

        [Inject]
        public IList<IClearMemorizedSpellSlotAfterEvent> OnClearMemorizedSpellSlotAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXClearMemorizedSpellSlotAfterScript)]
        public void HandleClearMemorizedSpellSlotAfter() => HandleEvent(OnClearMemorizedSpellSlotAfterSubscriptions, (subscription) => subscription.OnClearMemorizedSpellSlotAfter());

        [Inject]
        public IList<IHealerKitBeforeEvent> OnHealerKitBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnHealerKitBeforeScript)]
        public void HandleHealerKitBefore() => HandleEvent(OnHealerKitBeforeSubscriptions, (subscription) => subscription.OnHealerKitBefore());

        [Inject]
        public IList<IHealerKitAfterEvent> OnHealerKitAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnHealerKitAfterScript)]
        public void HandleHealerKitAfter() => HandleEvent(OnHealerKitAfterSubscriptions, (subscription) => subscription.OnHealerKitAfter());

        [Inject]
        public IList<IHealBeforeEvent> OnHealBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnHealBeforeScript)]
        public void HandleHealBefore() => HandleEvent(OnHealBeforeSubscriptions, (subscription) => subscription.OnHealBefore());

        [Inject]
        public IList<IHealAfterEvent> OnHealAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnHealAfterScript)]
        public void HandleHealAfter() => HandleEvent(OnHealAfterSubscriptions, (subscription) => subscription.OnHealAfter());
        [Inject]
        public IList<IPartyLeaveBeforeEvent> OnPartyLeaveBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnPartyLeaveBeforeScript)]
        public void HandleOnPartyLeaveBefore() => HandleEvent(OnPartyLeaveBeforeSubscriptions, (subscription) => subscription.OnPartyLeaveBefore());

        [Inject]
        public IList<IPartyLeaveAfterEvent> OnPartyLeaveAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnPartyLeaveAfterScript)]
        public void HandleOnPartyLeaveAfter() => HandleEvent(OnPartyLeaveAfterSubscriptions, (subscription) => subscription.OnPartyLeaveAfter());

        [Inject]
        public IList<IPartyKickBeforeEvent> OnPartyKickBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnPartyKickBeforeScript)]
        public void HandleOnPartyKickBefore() => HandleEvent(OnPartyKickBeforeSubscriptions, (subscription) => subscription.OnPartyKickBefore());

        [Inject]
        public IList<IPartyKickAfterEvent> OnPartyKickAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnPartyKickAfterScript)]
        public void HandleOnPartyKickAfter() => HandleEvent(OnPartyKickAfterSubscriptions, (subscription) => subscription.OnPartyKickAfter());

        [Inject]
        public IList<IPartyTransferLeadershipBeforeEvent> OnPartyTransferLeadershipBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnPartyTransferLeadershipBeforeScript)]
        public void HandleOnPartyTransferLeadershipBefore() => HandleEvent(OnPartyTransferLeadershipBeforeSubscriptions, (subscription) => subscription.OnPartyTransferLeadershipBefore());

        [Inject]
        public IList<IPartyTransferLeadershipAfterEvent> OnPartyTransferLeadershipAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnPartyTransferLeadershipAfterScript)]
        public void HandleOnPartyTransferLeadershipAfter() => HandleEvent(OnPartyTransferLeadershipAfterSubscriptions, (subscription) => subscription.OnPartyTransferLeadershipAfter());

        [Inject]
        public IList<IPartyInviteBeforeEvent> OnPartyInviteBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnPartyInviteBeforeScript)]
        public void HandleOnPartyInviteBefore() => HandleEvent(OnPartyInviteBeforeSubscriptions, (subscription) => subscription.OnPartyInviteBefore());

        [Inject]
        public IList<IPartyInviteAfterEvent> OnPartyInviteAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnPartyInviteAfterScript)]
        public void HandleOnPartyInviteAfter() => HandleEvent(OnPartyInviteAfterSubscriptions, (subscription) => subscription.OnPartyInviteAfter());

        [Inject]
        public IList<IPartyIgnoreInvitationBeforeEvent> OnPartyIgnoreInvitationBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnPartyIgnoreInvitationBeforeScript)]
        public void HandleOnPartyIgnoreInvitationBefore() => HandleEvent(OnPartyIgnoreInvitationBeforeSubscriptions, (subscription) => subscription.OnPartyIgnoreInvitationBefore());

        [Inject]
        public IList<IPartyIgnoreInvitationAfterEvent> OnPartyIgnoreInvitationAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnPartyIgnoreInvitationAfterScript)]
        public void HandleOnPartyIgnoreInvitationAfter() => HandleEvent(OnPartyIgnoreInvitationAfterSubscriptions, (subscription) => subscription.OnPartyIgnoreInvitationAfter());

        [Inject]
        public IList<IPartyAcceptInvitationBeforeEvent> OnPartyAcceptInvitationBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnPartyAcceptInvitationBeforeScript)]
        public void HandleOnPartyAcceptInvitationBefore() => HandleEvent(OnPartyAcceptInvitationBeforeSubscriptions, (subscription) => subscription.OnPartyAcceptInvitationBefore());

        [Inject]
        public IList<IPartyAcceptInvitationAfterEvent> OnPartyAcceptInvitationAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnPartyAcceptInvitationAfterScript)]
        public void HandleOnPartyAcceptInvitationAfter() => HandleEvent(OnPartyAcceptInvitationAfterSubscriptions, (subscription) => subscription.OnPartyAcceptInvitationAfter());

        [Inject]
        public IList<IPartyRejectInvitationBeforeEvent> OnPartyRejectInvitationBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnPartyRejectInvitationBeforeScript)]
        public void HandleOnPartyRejectInvitationBefore() => HandleEvent(OnPartyRejectInvitationBeforeSubscriptions, (subscription) => subscription.OnPartyRejectInvitationBefore());

        [Inject]
        public IList<IPartyRejectInvitationAfterEvent> OnPartyRejectInvitationAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnPartyRejectInvitationAfterScript)]
        public void HandleOnPartyRejectInvitationAfter() => HandleEvent(OnPartyRejectInvitationAfterSubscriptions, (subscription) => subscription.OnPartyRejectInvitationAfter());

        [Inject]
        public IList<IPartyKickHenchmanBeforeEvent> OnPartyKickHenchmanBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnPartyKickHenchmanBeforeScript)]
        public void HandleOnPartyKickHenchmanBefore() => HandleEvent(OnPartyKickHenchmanBeforeSubscriptions, (subscription) => subscription.OnPartyKickHenchmanBefore());

        [Inject]
        public IList<IPartyKickHenchmanAfterEvent> OnPartyKickHenchmanAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnPartyKickHenchmanAfterScript)]
        public void HandleOnPartyKickHenchmanAfter() => HandleEvent(OnPartyKickHenchmanAfterSubscriptions, (subscription) => subscription.OnPartyKickHenchmanAfter());
        [Inject]
        public IList<ICombatModeOnEvent> CombatModeOnSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnCombatModeOnScript)]
        public void HandleCombatModeOn() => HandleEvent(CombatModeOnSubscriptions, (subscription) => subscription.OnCombatModeOn());

        [Inject]
        public IList<ICombatModeOffEvent> CombatModeOffSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnCombatModeOffScript)]
        public void HandleCombatModeOff() => HandleEvent(CombatModeOffSubscriptions, (subscription) => subscription.OnCombatModeOff());

        [Inject]
        public IList<IUseSkillBeforeEvent> UseSkillBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnUseSkillBeforeScript)]
        public void HandleUseSkillBefore() => HandleEvent(UseSkillBeforeSubscriptions, (subscription) => subscription.OnUseSkillBefore());

        [Inject]
        public IList<IUseSkillAfterEvent> UseSkillAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnUseSkillAfterScript)]
        public void HandleUseSkillAfter() => HandleEvent(UseSkillAfterSubscriptions, (subscription) => subscription.OnUseSkillAfter());

        [Inject]
        public IList<IMapPinAddPinBeforeEvent> MapPinAddPinBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnMapPinAddPinBeforeScript)]
        public void HandleMapPinAddPinBefore() => HandleEvent(MapPinAddPinBeforeSubscriptions, (subscription) => subscription.OnMapPinAddPinBefore());

        [Inject]
        public IList<IMapPinAddPinAfterEvent> MapPinAddPinAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnMapPinAddPinAfterScript)]
        public void HandleMapPinAddPinAfter() => HandleEvent(MapPinAddPinAfterSubscriptions, (subscription) => subscription.OnMapPinAddPinAfter());

        [Inject]
        public IList<IMapPinChangePinBeforeEvent> MapPinChangePinBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnMapPinChangePinBeforeScript)]
        public void HandleMapPinChangePinBefore() => HandleEvent(MapPinChangePinBeforeSubscriptions, (subscription) => subscription.OnMapPinChangePinBefore());

        [Inject]
        public IList<IMapPinChangePinAfterEvent> MapPinChangePinAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnMapPinChangePinAfterScript)]
        public void HandleMapPinChangePinAfter() => HandleEvent(MapPinChangePinAfterSubscriptions, (subscription) => subscription.OnMapPinChangePinAfter());

        [Inject]
        public IList<IMapPinDestroyPinBeforeEvent> MapPinDestroyPinBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnMapPinDestroyPinBeforeScript)]
        public void HandleMapPinDestroyPinBefore() => HandleEvent(MapPinDestroyPinBeforeSubscriptions, (subscription) => subscription.OnMapPinDestroyPinBefore());

        [Inject]
        public IList<IMapPinDestroyPinAfterEvent> MapPinDestroyPinAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnMapPinDestroyPinAfterScript)]
        public void HandleMapPinDestroyPinAfter() => HandleEvent(MapPinDestroyPinAfterSubscriptions, (subscription) => subscription.OnMapPinDestroyPinAfter());

        [Inject]
        public IList<IDoListenDetectionBeforeEvent> DoListenDetectionBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDoListenDetectionBeforeScript)]
        public void HandleDoListenDetectionBefore() => HandleEvent(DoListenDetectionBeforeSubscriptions, (subscription) => subscription.OnDoListenDetectionBefore());

        [Inject]
        public IList<IDoListenDetectionAfterEvent> DoListenDetectionAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDoListenDetectionAfterScript)]
        public void HandleDoListenDetectionAfter() => HandleEvent(DoListenDetectionAfterSubscriptions, (subscription) => subscription.OnDoListenDetectionAfter());

        [Inject]
        public IList<IDoSpotDetectionBeforeEvent> DoSpotDetectionBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDoSpotDetectionBeforeScript)]
        public void HandleDoSpotDetectionBefore() => HandleEvent(DoSpotDetectionBeforeSubscriptions, (subscription) => subscription.OnDoSpotDetectionBefore());

        [Inject]
        public IList<IDoSpotDetectionAfterEvent> DoSpotDetectionAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDoSpotDetectionAfterScript)]
        public void HandleDoSpotDetectionAfter() => HandleEvent(DoSpotDetectionAfterSubscriptions, (subscription) => subscription.OnDoSpotDetectionAfter());
        [Inject]
        public IList<IPolymorphBeforeEvent> PolymorphBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnPolymorphBeforeScript)]
        public void HandlePolymorphBefore() => HandleEvent(PolymorphBeforeSubscriptions, (subscription) => subscription.OnPolymorphBefore());

        [Inject]
        public IList<IPolymorphAfterEvent> PolymorphAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnPolymorphAfterScript)]
        public void HandlePolymorphAfter() => HandleEvent(PolymorphAfterSubscriptions, (subscription) => subscription.OnPolymorphAfter());

        [Inject]
        public IList<IUnpolymorphBeforeEvent> UnpolymorphBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnUnpolymorphBeforeScript)]
        public void HandleUnpolymorphBefore() => HandleEvent(UnpolymorphBeforeSubscriptions, (subscription) => subscription.OnUnpolymorphBefore());

        [Inject]
        public IList<IUnpolymorphAfterEvent> UnpolymorphAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnUnpolymorphAfterScript)]
        public void HandleUnpolymorphAfter() => HandleEvent(UnpolymorphAfterSubscriptions, (subscription) => subscription.OnUnpolymorphAfter());

        [Inject]
        public IList<IEffectAppliedBeforeEvent> EffectAppliedBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnEffectAppliedBeforeScript)]
        public void HandleEffectAppliedBefore() => HandleEvent(EffectAppliedBeforeSubscriptions, (subscription) => subscription.OnEffectAppliedBefore());

        [Inject]
        public IList<IEffectAppliedAfterEvent> EffectAppliedAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnEffectAppliedAfterScript)]
        public void HandleEffectAppliedAfter() => HandleEvent(EffectAppliedAfterSubscriptions, (subscription) => subscription.OnEffectAppliedAfter());

        [Inject]
        public IList<IEffectRemovedBeforeEvent> EffectRemovedBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnEffectRemovedBeforeScript)]
        public void HandleEffectRemovedBefore() => HandleEvent(EffectRemovedBeforeSubscriptions, (subscription) => subscription.OnEffectRemovedBefore());

        [Inject]
        public IList<IEffectRemovedAfterEvent> EffectRemovedAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnEffectRemovedAfterScript)]
        public void HandleEffectRemovedAfter() => HandleEvent(EffectRemovedAfterSubscriptions, (subscription) => subscription.OnEffectRemovedAfter());

        [Inject]
        public IList<IQuickchatBeforeEvent> QuickchatBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnQuickchatBeforeScript)]
        public void HandleQuickchatBefore() => HandleEvent(QuickchatBeforeSubscriptions, (subscription) => subscription.OnQuickchatBefore());

        [Inject]
        public IList<IQuickchatAfterEvent> QuickchatAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnQuickchatAfterScript)]
        public void HandleQuickchatAfter() => HandleEvent(QuickchatAfterSubscriptions, (subscription) => subscription.OnQuickchatAfter());

        [Inject]
        public IList<IInventoryOpenBeforeEvent> InventoryOpenBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnInventoryOpenBeforeScript)]
        public void HandleInventoryOpenBefore() => HandleEvent(InventoryOpenBeforeSubscriptions, (subscription) => subscription.OnInventoryOpenBefore());

        [Inject]
        public IList<IInventoryOpenAfterEvent> InventoryOpenAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnInventoryOpenAfterScript)]
        public void HandleInventoryOpenAfter() => HandleEvent(InventoryOpenAfterSubscriptions, (subscription) => subscription.OnInventoryOpenAfter());

        [Inject]
        public IList<IInventorySelectPanelBeforeEvent> InventorySelectPanelBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnInventorySelectPanelBeforeScript)]
        public void HandleInventorySelectPanelBefore() => HandleEvent(InventorySelectPanelBeforeSubscriptions, (subscription) => subscription.OnInventorySelectPanelBefore());

        [Inject]
        public IList<IInventorySelectPanelAfterEvent> InventorySelectPanelAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnInventorySelectPanelAfterScript)]
        public void HandleInventorySelectPanelAfter() => HandleEvent(InventorySelectPanelAfterSubscriptions, (subscription) => subscription.OnInventorySelectPanelAfter());

        [Inject]
        public IList<IBarterStartBeforeEvent> BarterStartBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnBarterStartBeforeScript)]
        public void HandleBarterStartBefore() => HandleEvent(BarterStartBeforeSubscriptions, (subscription) => subscription.OnBarterStartBefore());

        [Inject]
        public IList<IBarterStartAfterEvent> BarterStartAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnBarterStartAfterScript)]
        public void HandleBarterStartAfter() => HandleEvent(BarterStartAfterSubscriptions, (subscription) => subscription.OnBarterStartAfter());

        [Inject]
        public IList<IBarterEndBeforeEvent> BarterEndBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnBarterEndBeforeScript)]
        public void HandleBarterEndBefore() => HandleEvent(BarterEndBeforeSubscriptions, (subscription) => subscription.OnBarterEndBefore());

        [Inject]
        public IList<IBarterEndAfterEvent> BarterEndAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnBarterEndAfterScript)]
        public void HandleBarterEndAfter() => HandleEvent(BarterEndAfterSubscriptions, (subscription) => subscription.OnBarterEndAfter());
        [Inject]
        public IList<ITrapDisarmBeforeEvent> TrapDisarmBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnTrapDisarmBeforeScript)]
        public void HandleTrapDisarmBefore() => HandleEvent(TrapDisarmBeforeSubscriptions, (subscription) => subscription.OnTrapDisarmBefore());

        [Inject]
        public IList<ITrapDisarmAfterEvent> TrapDisarmAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnTrapDisarmAfterScript)]
        public void HandleTrapDisarmAfter() => HandleEvent(TrapDisarmAfterSubscriptions, (subscription) => subscription.OnTrapDisarmAfter());

        [Inject]
        public IList<ITrapEnterBeforeEvent> TrapEnterBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnTrapEnterBeforeScript)]
        public void HandleTrapEnterBefore() => HandleEvent(TrapEnterBeforeSubscriptions, (subscription) => subscription.OnTrapEnterBefore());

        [Inject]
        public IList<ITrapEnterAfterEvent> TrapEnterAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnTrapEnterAfterScript)]
        public void HandleTrapEnterAfter() => HandleEvent(TrapEnterAfterSubscriptions, (subscription) => subscription.OnTrapEnterAfter());

        [Inject]
        public IList<ITrapExamineBeforeEvent> TrapExamineBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnTrapExamineBeforeScript)]
        public void HandleTrapExamineBefore() => HandleEvent(TrapExamineBeforeSubscriptions, (subscription) => subscription.OnTrapExamineBefore());

        [Inject]
        public IList<ITrapExamineAfterEvent> TrapExamineAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnTrapExamineAfterScript)]
        public void HandleTrapExamineAfter() => HandleEvent(TrapExamineAfterSubscriptions, (subscription) => subscription.OnTrapExamineAfter());

        [Inject]
        public IList<ITrapFlagBeforeEvent> TrapFlagBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnTrapFlagBeforeScript)]
        public void HandleTrapFlagBefore() => HandleEvent(TrapFlagBeforeSubscriptions, (subscription) => subscription.OnTrapFlagBefore());

        [Inject]
        public IList<ITrapFlagAfterEvent> TrapFlagAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnTrapFlagAfterScript)]
        public void HandleTrapFlagAfter() => HandleEvent(TrapFlagAfterSubscriptions, (subscription) => subscription.OnTrapFlagAfter());

        [Inject]
        public IList<ITrapRecoverBeforeEvent> TrapRecoverBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnTrapRecoverBeforeScript)]
        public void HandleTrapRecoverBefore() => HandleEvent(TrapRecoverBeforeSubscriptions, (subscription) => subscription.OnTrapRecoverBefore());

        [Inject]
        public IList<ITrapRecoverAfterEvent> TrapRecoverAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnTrapRecoverAfterScript)]
        public void HandleTrapRecoverAfter() => HandleEvent(TrapRecoverAfterSubscriptions, (subscription) => subscription.OnTrapRecoverAfter());

        [Inject]
        public IList<ITrapSetBeforeEvent> TrapSetBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnTrapSetBeforeScript)]
        public void HandleTrapSetBefore() => HandleEvent(TrapSetBeforeSubscriptions, (subscription) => subscription.OnTrapSetBefore());

        [Inject]
        public IList<ITrapSetAfterEvent> TrapSetAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnTrapSetAfterScript)]
        public void HandleTrapSetAfter() => HandleEvent(TrapSetAfterSubscriptions, (subscription) => subscription.OnTrapSetAfter());

        [Inject]
        public IList<ITimingBarStartBeforeEvent> TimingBarStartBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnTimingBarStartBeforeScript)]
        public void HandleTimingBarStartBefore() => HandleEvent(TimingBarStartBeforeSubscriptions, (subscription) => subscription.OnTimingBarStartBefore());

        [Inject]
        public IList<ITimingBarStartAfterEvent> TimingBarStartAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnTimingBarStartAfterScript)]
        public void HandleTimingBarStartAfter() => HandleEvent(TimingBarStartAfterSubscriptions, (subscription) => subscription.OnTimingBarStartAfter());

        [Inject]
        public IList<ITimingBarStopBeforeEvent> TimingBarStopBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnTimingBarStopBeforeScript)]
        public void HandleTimingBarStopBefore() => HandleEvent(TimingBarStopBeforeSubscriptions, (subscription) => subscription.OnTimingBarStopBefore());

        [Inject]
        public IList<ITimingBarStopAfterEvent> TimingBarStopAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnTimingBarStopAfterScript)]
        public void HandleTimingBarStopAfter() => HandleEvent(TimingBarStopAfterSubscriptions, (subscription) => subscription.OnTimingBarStopAfter());

        [Inject]
        public IList<ITimingBarCancelBeforeEvent> TimingBarCancelBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnTimingBarCancelBeforeScript)]
        public void HandleTimingBarCancelBefore() => HandleEvent(TimingBarCancelBeforeSubscriptions, (subscription) => subscription.OnTimingBarCancelBefore());

        [Inject]
        public IList<ITimingBarCancelAfterEvent> TimingBarCancelAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnTimingBarCancelAfterScript)]
        public void HandleTimingBarCancelAfter() => HandleEvent(TimingBarCancelAfterSubscriptions, (subscription) => subscription.OnTimingBarCancelAfter());
        [Inject]
        public IList<IWebhookSuccessEvent> WebhookSuccessSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnWebhookSuccessScript)]
        public void HandleWebhookSuccess() => HandleEvent(WebhookSuccessSubscriptions, (subscription) => subscription.OnWebhookSuccess());

        [Inject]
        public IList<IWebhookFailureEvent> WebhookFailureSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnWebhookFailureScript)]
        public void HandleWebhookFailure() => HandleEvent(WebhookFailureSubscriptions, (subscription) => subscription.OnWebhookFailure());

        [Inject]
        public IList<ICheckStickyPlayerNameReservedBeforeEvent> CheckStickyPlayerNameReservedBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnCheckStickyPlayerNameReservedBeforeScript)]
        public void HandleCheckStickyPlayerNameReservedBefore() => HandleEvent(CheckStickyPlayerNameReservedBeforeSubscriptions, (subscription) => subscription.OnCheckStickyPlayerNameReservedBefore());

        [Inject]
        public IList<ICheckStickyPlayerNameReservedAfterEvent> CheckStickyPlayerNameReservedAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnCheckStickyPlayerNameReservedAfterScript)]
        public void HandleCheckStickyPlayerNameReservedAfter() => HandleEvent(CheckStickyPlayerNameReservedAfterSubscriptions, (subscription) => subscription.OnCheckStickyPlayerNameReservedAfter());

        [Inject]
        public IList<ILevelUpBeforeEvent> LevelUpBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnLevelUpBeforeScript)]
        public void HandleLevelUpBefore() => HandleEvent(LevelUpBeforeSubscriptions, (subscription) => subscription.OnLevelUpBefore());

        [Inject]
        public IList<ILevelUpAfterEvent> LevelUpAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnLevelUpAfterScript)]
        public void HandleLevelUpAfter() => HandleEvent(LevelUpAfterSubscriptions, (subscription) => subscription.OnLevelUpAfter());

        [Inject]
        public IList<ILevelUpAutomaticBeforeEvent> LevelUpAutomaticBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnLevelUpAutomaticBeforeScript)]
        public void HandleLevelUpAutomaticBefore() => HandleEvent(LevelUpAutomaticBeforeSubscriptions, (subscription) => subscription.OnLevelUpAutomaticBefore());

        [Inject]
        public IList<ILevelUpAutomaticAfterEvent> LevelUpAutomaticAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnLevelUpAutomaticAfterScript)]
        public void HandleLevelUpAutomaticAfter() => HandleEvent(LevelUpAutomaticAfterSubscriptions, (subscription) => subscription.OnLevelUpAutomaticAfter());

        [Inject]
        public IList<ILevelDownBeforeEvent> LevelDownBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnLevelDownBeforeScript)]
        public void HandleLevelDownBefore() => HandleEvent(LevelDownBeforeSubscriptions, (subscription) => subscription.OnLevelDownBefore());

        [Inject]
        public IList<ILevelDownAfterEvent> LevelDownAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnLevelDownAfterScript)]
        public void HandleLevelDownAfter() => HandleEvent(LevelDownAfterSubscriptions, (subscription) => subscription.OnLevelDownAfter());

        [Inject]
        public IList<IInventoryAddItemBeforeEvent> InventoryAddItemBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnInventoryAddItemBeforeScript)]
        public void HandleInventoryAddItemBefore() => HandleEvent(InventoryAddItemBeforeSubscriptions, (subscription) => subscription.OnInventoryAddItemBefore());

        [Inject]
        public IList<IInventoryAddItemAfterEvent> InventoryAddItemAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnInventoryAddItemAfterScript)]
        public void HandleInventoryAddItemAfter() => HandleEvent(InventoryAddItemAfterSubscriptions, (subscription) => subscription.OnInventoryAddItemAfter());

        [Inject]
        public IList<IInventoryRemoveItemBeforeEvent> InventoryRemoveItemBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnInventoryRemoveItemBeforeScript)]
        public void HandleInventoryRemoveItemBefore() => HandleEvent(InventoryRemoveItemBeforeSubscriptions, (subscription) => subscription.OnInventoryRemoveItemBefore());

        [Inject]
        public IList<IInventoryRemoveItemAfterEvent> InventoryRemoveItemAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnInventoryRemoveItemAfterScript)]
        public void HandleInventoryRemoveItemAfter() => HandleEvent(InventoryRemoveItemAfterSubscriptions, (subscription) => subscription.OnInventoryRemoveItemAfter());
        [Inject]
        public IList<IInventoryAddGoldBeforeEvent> InventoryAddGoldBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnInventoryAddGoldBeforeScript)]
        public void HandleInventoryAddGoldBefore() => HandleEvent(InventoryAddGoldBeforeSubscriptions, (subscription) => subscription.OnInventoryAddGoldBefore());

        [Inject]
        public IList<IInventoryAddGoldAfterEvent> InventoryAddGoldAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnInventoryAddGoldAfterScript)]
        public void HandleInventoryAddGoldAfter() => HandleEvent(InventoryAddGoldAfterSubscriptions, (subscription) => subscription.OnInventoryAddGoldAfter());

        [Inject]
        public IList<IInventoryRemoveGoldBeforeEvent> InventoryRemoveGoldBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnInventoryRemoveGoldBeforeScript)]
        public void HandleInventoryRemoveGoldBefore() => HandleEvent(InventoryRemoveGoldBeforeSubscriptions, (subscription) => subscription.OnInventoryRemoveGoldBefore());

        [Inject]
        public IList<IInventoryRemoveGoldAfterEvent> InventoryRemoveGoldAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnInventoryRemoveGoldAfterScript)]
        public void HandleInventoryRemoveGoldAfter() => HandleEvent(InventoryRemoveGoldAfterSubscriptions, (subscription) => subscription.OnInventoryRemoveGoldAfter());

        [Inject]
        public IList<IPvpAttitudeChangeBeforeEvent> PvpAttitudeChangeBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnPvpAttitudeChangeBeforeScript)]
        public void HandlePvpAttitudeChangeBefore() => HandleEvent(PvpAttitudeChangeBeforeSubscriptions, (subscription) => subscription.OnPvpAttitudeChangeBefore());

        [Inject]
        public IList<IPvpAttitudeChangeAfterEvent> PvpAttitudeChangeAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnPvpAttitudeChangeAfterScript)]
        public void HandlePvpAttitudeChangeAfter() => HandleEvent(PvpAttitudeChangeAfterSubscriptions, (subscription) => subscription.OnPvpAttitudeChangeAfter());

        [Inject]
        public IList<IInputWalkToWaypointBeforeEvent> InputWalkToWaypointBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnInputWalkToWaypointBeforeScript)]
        public void HandleInputWalkToWaypointBefore() => HandleEvent(InputWalkToWaypointBeforeSubscriptions, (subscription) => subscription.OnInputWalkToWaypointBefore());

        [Inject]
        public IList<IInputWalkToWaypointAfterEvent> InputWalkToWaypointAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnInputWalkToWaypointAfterScript)]
        public void HandleInputWalkToWaypointAfter() => HandleEvent(InputWalkToWaypointAfterSubscriptions, (subscription) => subscription.OnInputWalkToWaypointAfter());

        [Inject]
        public IList<IMaterialChangeBeforeEvent> MaterialChangeBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnMaterialChangeBeforeScript)]
        public void HandleMaterialChangeBefore() => HandleEvent(MaterialChangeBeforeSubscriptions, (subscription) => subscription.OnMaterialChangeBefore());

        [Inject]
        public IList<IMaterialChangeAfterEvent> MaterialChangeAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnMaterialChangeAfterScript)]
        public void HandleMaterialChangeAfter() => HandleEvent(MaterialChangeAfterSubscriptions, (subscription) => subscription.OnMaterialChangeAfter());

        [Inject]
        public IList<IInputAttackObjectBeforeEvent> InputAttackObjectBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnInputAttackObjectBeforeScript)]
        public void HandleInputAttackObjectBefore() => HandleEvent(InputAttackObjectBeforeSubscriptions, (subscription) => subscription.OnInputAttackObjectBefore());

        [Inject]
        public IList<IInputAttackObjectAfterEvent> InputAttackObjectAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnInputAttackObjectAfterScript)]
        public void HandleInputAttackObjectAfter() => HandleEvent(InputAttackObjectAfterSubscriptions, (subscription) => subscription.OnInputAttackObjectAfter());
        [Inject]
        public IList<IObjectLockBeforeEvent> ObjectLockBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnObjectLockBeforeScript)]
        public void HandleObjectLockBefore() => HandleEvent(ObjectLockBeforeSubscriptions, (subscription) => subscription.OnObjectLockBefore());

        [Inject]
        public IList<IObjectLockAfterEvent> ObjectLockAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnObjectLockAfterScript)]
        public void HandleObjectLockAfter() => HandleEvent(ObjectLockAfterSubscriptions, (subscription) => subscription.OnObjectLockAfter());

        [Inject]
        public IList<IObjectUnlockBeforeEvent> ObjectUnlockBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnObjectUnlockBeforeScript)]
        public void HandleObjectUnlockBefore() => HandleEvent(ObjectUnlockBeforeSubscriptions, (subscription) => subscription.OnObjectUnlockBefore());

        [Inject]
        public IList<IObjectUnlockAfterEvent> ObjectUnlockAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnObjectUnlockAfterScript)]
        public void HandleObjectUnlockAfter() => HandleEvent(ObjectUnlockAfterSubscriptions, (subscription) => subscription.OnObjectUnlockAfter());

        [Inject]
        public IList<IUuidCollisionBeforeEvent> UuidCollisionBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnUuidCollisionBeforeScript)]
        public void HandleUuidCollisionBefore() => HandleEvent(UuidCollisionBeforeSubscriptions, (subscription) => subscription.OnUuidCollisionBefore());

        [Inject]
        public IList<IUuidCollisionAfterEvent> UuidCollisionAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnUuidCollisionAfterScript)]
        public void HandleUuidCollisionAfter() => HandleEvent(UuidCollisionAfterSubscriptions, (subscription) => subscription.OnUuidCollisionAfter());
        [Inject]
        public IList<IElcValidateCharacterBeforeEvent> ElcValidateCharacterBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnElcValidateCharacterBeforeScript)]
        public void HandleElcValidateCharacterBefore() => HandleEvent(ElcValidateCharacterBeforeSubscriptions, (subscription) => subscription.OnElcValidateCharacterBefore());


        [Inject]
        public IList<IElcValidateCharacterAfterEvent> ElcValidateCharacterAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnElcValidateCharacterAfterScript)]
        public void HandleElcValidateCharacterAfter() => HandleEvent(ElcValidateCharacterAfterSubscriptions, (subscription) => subscription.OnElcValidateCharacterAfter());


        [Inject]
        public IList<IQuickbarSetButtonBeforeEvent> QuickbarSetButtonBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnQuickbarSetButtonBeforeScript)]
        public void HandleQuickbarSetButtonBefore() => HandleEvent(QuickbarSetButtonBeforeSubscriptions, (subscription) => subscription.OnQuickbarSetButtonBefore());


        [Inject]
        public IList<IQuickbarSetButtonAfterEvent> QuickbarSetButtonAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnQuickbarSetButtonAfterScript)]
        public void HandleQuickbarSetButtonAfter() => HandleEvent(QuickbarSetButtonAfterSubscriptions, (subscription) => subscription.OnQuickbarSetButtonAfter());


        [Inject]
        public IList<ICalendarHourEvent> CalendarHourSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnCalendarHourScript)]
        public void HandleCalendarHour() => HandleEvent(CalendarHourSubscriptions, (subscription) => subscription.OnCalendarHour());


        [Inject]
        public IList<ICalendarDayEvent> CalendarDaySubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnCalendarDayScript)]
        public void HandleCalendarDay() => HandleEvent(CalendarDaySubscriptions, (subscription) => subscription.OnCalendarDay());


        [Inject]
        public IList<ICalendarMonthEvent> CalendarMonthSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnCalendarMonthScript)]
        public void HandleCalendarMonth() => HandleEvent(CalendarMonthSubscriptions, (subscription) => subscription.OnCalendarMonth());


        [Inject]
        public IList<ICalendarYearEvent> CalendarYearSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnCalendarYearScript)]
        public void HandleCalendarYear() => HandleEvent(CalendarYearSubscriptions, (subscription) => subscription.OnCalendarYear());


        [Inject]
        public IList<ICalendarDawnEvent> CalendarDawnSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnCalendarDawnScript)]
        public void HandleCalendarDawn() => HandleEvent(CalendarDawnSubscriptions, (subscription) => subscription.OnCalendarDawn());


        [Inject]
        public IList<ICalendarDuskEvent> CalendarDuskSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnCalendarDuskScript)]
        public void HandleCalendarDusk() => HandleEvent(CalendarDuskSubscriptions, (subscription) => subscription.OnCalendarDusk());


        [Inject]
        public IList<IBroadcastCastSpellBeforeEvent> BroadcastCastSpellBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnBroadcastCastSpellBeforeScript)]
        public void HandleBroadcastCastSpellBefore() => HandleEvent(BroadcastCastSpellBeforeSubscriptions, (subscription) => subscription.OnBroadcastCastSpellBefore());


        [Inject]
        public IList<IBroadcastCastSpellAfterEvent> BroadcastCastSpellAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnBroadcastCastSpellAfterScript)]
        public void HandleBroadcastCastSpellAfter() => HandleEvent(BroadcastCastSpellAfterSubscriptions, (subscription) => subscription.OnBroadcastCastSpellAfter());


        [Inject]
        public IList<IDebugRunScriptBeforeEvent> DebugRunScriptBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDebugRunScriptBeforeScript)]
        public void HandleDebugRunScriptBefore() => HandleEvent(DebugRunScriptBeforeSubscriptions, (subscription) => subscription.OnDebugRunScriptBefore());


        [Inject]
        public IList<IDebugRunScriptAfterEvent> DebugRunScriptAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDebugRunScriptAfterScript)]
        public void HandleDebugRunScriptAfter() => HandleEvent(DebugRunScriptAfterSubscriptions, (subscription) => subscription.OnDebugRunScriptAfter());


        [Inject]
        public IList<IDebugRunScriptChunkBeforeEvent> DebugRunScriptChunkBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDebugRunScriptChunkBeforeScript)]
        public void HandleDebugRunScriptChunkBefore() => HandleEvent(DebugRunScriptChunkBeforeSubscriptions, (subscription) => subscription.OnDebugRunScriptChunkBefore());


        [Inject]
        public IList<IDebugRunScriptChunkAfterEvent> DebugRunScriptChunkAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnDebugRunScriptChunkAfterScript)]
        public void HandleDebugRunScriptChunkAfter() => HandleEvent(DebugRunScriptChunkAfterSubscriptions, (subscription) => subscription.OnDebugRunScriptChunkAfter());


        [Inject]
        public IList<IStoreRequestBuyBeforeEvent> StoreRequestBuyBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnStoreRequestBuyBeforeScript)]
        public void HandleStoreRequestBuyBefore() => HandleEvent(StoreRequestBuyBeforeSubscriptions, (subscription) => subscription.OnStoreRequestBuyBefore());


        [Inject]
        public IList<IStoreRequestBuyAfterEvent> StoreRequestBuyAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnStoreRequestBuyAfterScript)]
        public void HandleStoreRequestBuyAfter() => HandleEvent(StoreRequestBuyAfterSubscriptions, (subscription) => subscription.OnStoreRequestBuyAfter());


        [Inject]
        public IList<IStoreRequestSellBeforeEvent> StoreRequestSellBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnStoreRequestSellBeforeScript)]
        public void HandleStoreRequestSellBefore() => HandleEvent(StoreRequestSellBeforeSubscriptions, (subscription) => subscription.OnStoreRequestSellBefore());


        [Inject]
        public IList<IStoreRequestSellAfterEvent> StoreRequestSellAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnStoreRequestSellAfterScript)]
        public void HandleStoreRequestSellAfter() => HandleEvent(StoreRequestSellAfterSubscriptions, (subscription) => subscription.OnStoreRequestSellAfter());


        [Inject]
        public IList<IInputDropItemBeforeEvent> InputDropItemBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnInputDropItemBeforeScript)]
        public void HandleInputDropItemBefore() => HandleEvent(InputDropItemBeforeSubscriptions, (subscription) => subscription.OnInputDropItemBefore());


        [Inject]
        public IList<IInputDropItemAfterEvent> InputDropItemAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnInputDropItemAfterScript)]
        public void HandleInputDropItemAfter() => HandleEvent(InputDropItemAfterSubscriptions, (subscription) => subscription.OnInputDropItemAfter());


        [Inject]
        public IList<IBroadcastAttackOfOpportunityBeforeEvent> BroadcastAttackOfOpportunityBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnBroadcastAttackOfOpportunityBeforeScript)]
        public void HandleBroadcastAttackOfOpportunityBefore() => HandleEvent(BroadcastAttackOfOpportunityBeforeSubscriptions, (subscription) => subscription.OnBroadcastAttackOfOpportunityBefore());


        [Inject]
        public IList<IBroadcastAttackOfOpportunityAfterEvent> BroadcastAttackOfOpportunityAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnBroadcastAttackOfOpportunityAfterScript)]
        public void HandleBroadcastAttackOfOpportunityAfter() => HandleEvent(BroadcastAttackOfOpportunityAfterSubscriptions, (subscription) => subscription.OnBroadcastAttackOfOpportunityAfter());


        [Inject]
        public IList<ICombatAttackOfOpportunityBeforeEvent> CombatAttackOfOpportunityBeforeSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnCombatAttackOfOpportunityBeforeScript)]
        public void HandleCombatAttackOfOpportunityBefore() => HandleEvent(CombatAttackOfOpportunityBeforeSubscriptions, (subscription) => subscription.OnCombatAttackOfOpportunityBefore());


        [Inject]
        public IList<ICombatAttackOfOpportunityAfterEvent> CombatAttackOfOpportunityAfterSubscriptions { get; set; }

        [ScriptHandler(EventScript.NWNXOnCombatAttackOfOpportunityAfterScript)]
        public void HandleCombatAttackOfOpportunityAfter() => HandleEvent(CombatAttackOfOpportunityAfterSubscriptions, (subscription) => subscription.OnCombatAttackOfOpportunityAfter());

    }
}
