using Anvil.Services;
using NLog;
using NWN.Core.NWNX;
using XM.Core.EventManagement.NWNXEvent;

namespace XM.Core.EventManagement
{
    [ServiceBinding(typeof(NWNXEventRegistrationService))]
    internal class NWNXEventRegistrationService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly XMEventService _event;

        public NWNXEventRegistrationService(XMEventService @event)
        {
            _event = @event;

            _logger.Info($"Registering NWNX events...");
            RegisterEvents();
            HookNWNXEvents();
        }

        private void RegisterEvents()
        {
            _event.RegisterEvent<ModulePreloadEvent>(EventScript.NWNXOnModulePreloadScript);
            _event.RegisterEvent<AddAssociateBeforeEvent>(EventScript.NWNXOnAddAssociateBeforeScript);
            _event.RegisterEvent<AddAssociateAfterEvent>(EventScript.NWNXOnAddAssociateAfterScript);
            _event.RegisterEvent<RemoveAssociateBeforeEvent>(EventScript.NWNXOnRemoveAssociateBeforeScript);
            _event.RegisterEvent<RemoveAssociateAfterEvent>(EventScript.NWNXOnRemoveAssociateAfterScript);
            _event.RegisterEvent<StealthEnterBeforeEvent>(EventScript.NWNXOnStealthEnterBeforeScript);
            _event.RegisterEvent<StealthEnterAfterEvent>(EventScript.NWNXOnStealthEnterAfterScript);
            _event.RegisterEvent<StealthExitBeforeEvent>(EventScript.NWNXOnStealthExitBeforeScript);
            _event.RegisterEvent<StealthExitAfterEvent>(EventScript.NWNXOnStealthExitAfterScript);
            _event.RegisterEvent<ExamineObjectBeforeEvent>(EventScript.NWNXOnExamineObjectBeforeScript);
            _event.RegisterEvent<ExamineObjectAfterEvent>(EventScript.NWNXOnExamineObjectAfterScript);
            _event.RegisterEvent<ValidateUseItemBeforeEvent>(EventScript.NWNXOnValidateUseItemBeforeScript);
            _event.RegisterEvent<ValidateUseItemAfterEvent>(EventScript.NWNXOnValidateUseItemAfterScript);
            _event.RegisterEvent<UseItemBeforeEvent>(EventScript.NWNXOnUseItemBeforeScript);
            _event.RegisterEvent<UseItemAfterEvent>(EventScript.NWNXOnUseItemAfterScript);
            _event.RegisterEvent<ItemInventoryOpenBeforeEvent>(EventScript.NWNXOnItemInventoryOpenBeforeScript);
            _event.RegisterEvent<ItemInventoryOpenAfterEvent>(EventScript.NWNXOnItemInventoryOpenAfterScript);
            _event.RegisterEvent<ItemInventoryCloseBeforeEvent>(EventScript.NWNXOnItemInventoryCloseBeforeScript);
            _event.RegisterEvent<ItemInventoryCloseAfterEvent>(EventScript.NWNXOnItemInventoryCloseAfterScript);
            _event.RegisterEvent<ItemAmmoReloadBeforeEvent>(EventScript.NWNXOnItemAmmoReloadBeforeScript);
            _event.RegisterEvent<ItemAmmoReloadAfterEvent>(EventScript.NWNXOnItemAmmoReloadAfterScript);
            _event.RegisterEvent<ItemScrollLearnBeforeEvent>(EventScript.NWNXOnItemScrollLearnBeforeScript);
            _event.RegisterEvent<ItemScrollLearnAfterEvent>(EventScript.NWNXOnItemScrollLearnAfterScript);
            _event.RegisterEvent<ValidateItemEquipBeforeEvent>(EventScript.NWNXOnValidateItemEquipBeforeScript);
            _event.RegisterEvent<ValidateItemEquipAfterEvent>(EventScript.NWNXOnValidateItemEquipAfterScript);
            _event.RegisterEvent<ItemEquipBeforeEvent>(EventScript.NWNXOnItemEquipBeforeScript);
            _event.RegisterEvent<ItemEquipAfterEvent>(EventScript.NWNXOnItemEquipAfterScript);
            _event.RegisterEvent<ItemUnequipBeforeEvent>(EventScript.NWNXOnItemUnequipBeforeScript);
            _event.RegisterEvent<ItemUnequipAfterEvent>(EventScript.NWNXOnItemUnequipAfterScript);
            _event.RegisterEvent<ItemDestroyObjectBeforeEvent>(EventScript.NWNXOnItemDestroyObjectBeforeScript);
            _event.RegisterEvent<ItemDestroyObjectAfterEvent>(EventScript.NWNXOnItemDestroyObjectAfterScript);
            _event.RegisterEvent<ItemDecrementStacksizeBeforeEvent>(EventScript.NWNXOnItemDecrementStacksizeBeforeScript);
            _event.RegisterEvent<ItemDecrementStacksizeAfterEvent>(EventScript.NWNXOnItemDecrementStacksizeAfterScript);
            _event.RegisterEvent<ItemUseLoreBeforeEvent>(EventScript.NWNXOnItemUseLoreBeforeScript);
            _event.RegisterEvent<ItemUseLoreAfterEvent>(EventScript.NWNXOnItemUseLoreAfterScript);
            _event.RegisterEvent<ItemPayToIdentifyBeforeEvent>(EventScript.NWNXOnItemPayToIdentifyBeforeScript);
            _event.RegisterEvent<ItemPayToIdentifyAfterEvent>(EventScript.NWNXOnItemPayToIdentifyAfterScript);
            _event.RegisterEvent<ItemSplitBeforeEvent>(EventScript.NWNXOnItemSplitBeforeScript);
            _event.RegisterEvent<ItemSplitAfterEvent>(EventScript.NWNXOnItemSplitAfterScript);
            _event.RegisterEvent<ItemMergeBeforeEvent>(EventScript.NWNXOnItemMergeBeforeScript);
            _event.RegisterEvent<ItemMergeAfterEvent>(EventScript.NWNXOnItemMergeAfterScript);
            _event.RegisterEvent<ItemAcquireBeforeEvent>(EventScript.NWNXOnItemAcquireBeforeScript);
            _event.RegisterEvent<ItemAcquireAfterEvent>(EventScript.NWNXOnItemAcquireAfterScript);
            _event.RegisterEvent<UseFeatBeforeEvent>(EventScript.NWNXOnUseFeatBeforeScript);
            _event.RegisterEvent<UseFeatAfterEvent>(EventScript.NWNXOnUseFeatAfterScript);
            _event.RegisterEvent<DmGiveGoldBeforeEvent>(EventScript.NWNXOnDmGiveGoldBeforeScript);
            _event.RegisterEvent<DmGiveGoldAfterEvent>(EventScript.NWNXOnDmGiveGoldAfterScript);
            _event.RegisterEvent<DmGiveXpBeforeEvent>(EventScript.NWNXOnDmGiveXpBeforeScript);
            _event.RegisterEvent<DmGiveXpAfterEvent>(EventScript.NWNXOnDmGiveXpAfterScript);
            _event.RegisterEvent<DmGiveLevelBeforeEvent>(EventScript.NWNXOnDmGiveLevelBeforeScript);
            _event.RegisterEvent<DmGiveLevelAfterEvent>(EventScript.NWNXOnDmGiveLevelAfterScript);
            _event.RegisterEvent<DmGiveAlignmentBeforeEvent>(EventScript.NWNXOnDmGiveAlignmentBeforeScript);
            _event.RegisterEvent<DmGiveAlignmentAfterEvent>(EventScript.NWNXOnDmGiveAlignmentAfterScript);
            _event.RegisterEvent<DmSpawnObjectBeforeEvent>(EventScript.NWNXOnDmSpawnObjectBeforeScript);
            _event.RegisterEvent<DmSpawnObjectAfterEvent>(EventScript.NWNXOnDmSpawnObjectAfterScript);
            _event.RegisterEvent<DmGiveItemBeforeEvent>(EventScript.NWNXOnDmGiveItemBeforeScript);
            _event.RegisterEvent<DmGiveItemAfterEvent>(EventScript.NWNXOnDmGiveItemAfterScript);
            _event.RegisterEvent<DmHealBeforeEvent>(EventScript.NWNXOnDmHealBeforeScript);
            _event.RegisterEvent<DmHealAfterEvent>(EventScript.NWNXOnDmHealAfterScript);
            _event.RegisterEvent<DmKillBeforeEvent>(EventScript.NWNXOnDmKillBeforeScript);
            _event.RegisterEvent<DmKillAfterEvent>(EventScript.NWNXOnDmKillAfterScript);
            _event.RegisterEvent<DmToggleInvulnerableBeforeEvent>(EventScript.NWNXOnDmToggleInvulnerableBeforeScript);
            _event.RegisterEvent<DmToggleInvulnerableAfterEvent>(EventScript.NWNXOnDmToggleInvulnerableAfterScript);
            _event.RegisterEvent<DmForceRestBeforeEvent>(EventScript.NWNXOnDmForceRestBeforeScript);
            _event.RegisterEvent<DmForceRestAfterEvent>(EventScript.NWNXOnDmForceRestAfterScript);
            _event.RegisterEvent<DmLimboBeforeEvent>(EventScript.NWNXOnDmLimboBeforeScript);
            _event.RegisterEvent<DmLimboAfterEvent>(EventScript.NWNXOnDmLimboAfterScript);
            _event.RegisterEvent<DmToggleAiBeforeEvent>(EventScript.NWNXOnDmToggleAiBeforeScript);
            _event.RegisterEvent<DmToggleAiAfterEvent>(EventScript.NWNXOnDmToggleAiAfterScript);
            _event.RegisterEvent<DmToggleImmortalBeforeEvent>(EventScript.NWNXOnDmToggleImmortalBeforeScript);
            _event.RegisterEvent<DmToggleImmortalAfterEvent>(EventScript.NWNXOnDmToggleImmortalAfterScript);
            _event.RegisterEvent<DmGotoBeforeEvent>(EventScript.NWNXOnDmGotoBeforeScript);
            _event.RegisterEvent<DmGotoAfterEvent>(EventScript.NWNXOnDmGotoAfterScript);
            _event.RegisterEvent<DmPossessBeforeEvent>(EventScript.NWNXOnDmPossessBeforeScript);
            _event.RegisterEvent<DmPossessAfterEvent>(EventScript.NWNXOnDmPossessAfterScript);
            _event.RegisterEvent<DmPossessFullPowerBeforeEvent>(EventScript.NWNXOnDmPossessFullPowerBeforeScript);
            _event.RegisterEvent<DmPossessFullPowerAfterEvent>(EventScript.NWNXOnDmPossessFullPowerAfterScript);
            _event.RegisterEvent<DmToggleLockBeforeEvent>(EventScript.NWNXOnDmToggleLockBeforeScript);
            _event.RegisterEvent<DmToggleLockAfterEvent>(EventScript.NWNXOnDmToggleLockAfterScript);
            _event.RegisterEvent<DmDisableTrapBeforeEvent>(EventScript.NWNXOnDmDisableTrapBeforeScript);
            _event.RegisterEvent<DmDisableTrapAfterEvent>(EventScript.NWNXOnDmDisableTrapAfterScript);
            _event.RegisterEvent<DmJumpToPointBeforeEvent>(EventScript.NWNXOnDmJumpToPointBeforeScript);
            _event.RegisterEvent<DmJumpToPointAfterEvent>(EventScript.NWNXOnDmJumpToPointAfterScript);
            _event.RegisterEvent<DmJumpTargetToPointBeforeEvent>(EventScript.NWNXOnDmJumpTargetToPointBeforeScript);
            _event.RegisterEvent<DmJumpTargetToPointAfterEvent>(EventScript.NWNXOnDmJumpTargetToPointAfterScript);
            _event.RegisterEvent<DmJumpAllPlayersToPointBeforeEvent>(EventScript.NWNXOnDmJumpAllPlayersToPointBeforeScript);
            _event.RegisterEvent<DmJumpAllPlayersToPointAfterEvent>(EventScript.NWNXOnDmJumpAllPlayersToPointAfterScript);
            _event.RegisterEvent<DmChangeDifficultyBeforeEvent>(EventScript.NWNXOnDmChangeDifficultyBeforeScript);
            _event.RegisterEvent<DmChangeDifficultyAfterEvent>(EventScript.NWNXOnDmChangeDifficultyAfterScript);
            _event.RegisterEvent<DmViewInventoryBeforeEvent>(EventScript.NWNXOnDmViewInventoryBeforeScript);
            _event.RegisterEvent<DmViewInventoryAfterEvent>(EventScript.NWNXOnDmViewInventoryAfterScript);
            _event.RegisterEvent<DmSpawnTrapOnObjectBeforeEvent>(EventScript.NWNXOnDmSpawnTrapOnObjectBeforeScript);
            _event.RegisterEvent<DmSpawnTrapOnObjectAfterEvent>(EventScript.NWNXOnDmSpawnTrapOnObjectAfterScript);
            _event.RegisterEvent<DmDumpLocalsBeforeEvent>(EventScript.NWNXOnDmDumpLocalsBeforeScript);
            _event.RegisterEvent<DmDumpLocalsAfterEvent>(EventScript.NWNXOnDmDumpLocalsAfterScript);
            _event.RegisterEvent<DmAppearBeforeEvent>(EventScript.NWNXOnDmAppearBeforeScript);
            _event.RegisterEvent<DmAppearAfterEvent>(EventScript.NWNXOnDmAppearAfterScript);
            _event.RegisterEvent<DmDisappearBeforeEvent>(EventScript.NWNXOnDmDisappearBeforeScript);
            _event.RegisterEvent<DmDisappearAfterEvent>(EventScript.NWNXOnDmDisappearAfterScript);
            _event.RegisterEvent<DmSetFactionBeforeEvent>(EventScript.NWNXOnDmSetFactionBeforeScript);
            _event.RegisterEvent<DmSetFactionAfterEvent>(EventScript.NWNXOnDmSetFactionAfterScript);
            _event.RegisterEvent<DmTakeItemBeforeEvent>(EventScript.NWNXOnDmTakeItemBeforeScript);
            _event.RegisterEvent<DmTakeItemAfterEvent>(EventScript.NWNXOnDmTakeItemAfterScript);
            _event.RegisterEvent<DmSetStatBeforeEvent>(EventScript.NWNXOnDmSetStatBeforeScript);
            _event.RegisterEvent<DmSetStatAfterEvent>(EventScript.NWNXOnDmSetStatAfterScript);
            _event.RegisterEvent<DmGetVariableBeforeEvent>(EventScript.NWNXOnDmGetVariableBeforeScript);
            _event.RegisterEvent<DmGetVariableAfterEvent>(EventScript.NWNXOnDmGetVariableAfterScript);
            _event.RegisterEvent<DmSetVariableBeforeEvent>(EventScript.NWNXOnDmSetVariableBeforeScript);
            _event.RegisterEvent<DmSetVariableAfterEvent>(EventScript.NWNXOnDmSetVariableAfterScript);
            _event.RegisterEvent<DmSetTimeBeforeEvent>(EventScript.NWNXOnDmSetTimeBeforeScript);
            _event.RegisterEvent<DmSetTimeAfterEvent>(EventScript.NWNXOnDmSetTimeAfterScript);
            _event.RegisterEvent<DmSetDateBeforeEvent>(EventScript.NWNXOnDmSetDateBeforeScript);
            _event.RegisterEvent<DmSetDateAfterEvent>(EventScript.NWNXOnDmSetDateAfterScript);
            _event.RegisterEvent<DmSetFactionReputationBeforeEvent>(EventScript.NWNXOnDmSetFactionReputationBeforeScript);
            _event.RegisterEvent<DmSetFactionReputationAfterEvent>(EventScript.NWNXOnDmSetFactionReputationAfterScript);
            _event.RegisterEvent<DmGetFactionReputationBeforeEvent>(EventScript.NWNXOnDmGetFactionReputationBeforeScript);
            _event.RegisterEvent<DmGetFactionReputationAfterEvent>(EventScript.NWNXOnDmGetFactionReputationAfterScript);
            _event.RegisterEvent<ClientDisconnectBeforeEvent>(EventScript.NWNXOnClientDisconnectBeforeScript);
            _event.RegisterEvent<ClientDisconnectAfterEvent>(EventScript.NWNXOnClientDisconnectAfterScript);
            _event.RegisterEvent<ClientConnectBeforeEvent>(EventScript.NWNXOnClientConnectBeforeScript);
            _event.RegisterEvent<ClientConnectAfterEvent>(EventScript.NWNXOnClientConnectAfterScript);
            _event.RegisterEvent<StartCombatRoundBeforeEvent>(EventScript.NWNXOnStartCombatRoundBeforeScript);
            _event.RegisterEvent<StartCombatRoundAfterEvent>(EventScript.NWNXOnStartCombatRoundAfterScript);
            _event.RegisterEvent<CastSpellBeforeEvent>(EventScript.NWNXOnCastSpellBeforeScript);
            _event.RegisterEvent<CastSpellAfterEvent>(EventScript.NWNXOnCastSpellAfterScript);
            _event.RegisterEvent<SetMemorizedSpellSlotBeforeEvent>(EventScript.NWNXSetMemorizedSpellSlotBeforeScript);
            _event.RegisterEvent<SetMemorizedSpellSlotAfterEvent>(EventScript.NWNXSetMemorizedSpellSlotAfterScript);
            _event.RegisterEvent<ClearMemorizedSpellSlotBeforeEvent>(EventScript.NWNXClearMemorizedSpellSlotBeforeScript);
            _event.RegisterEvent<ClearMemorizedSpellSlotAfterEvent>(EventScript.NWNXClearMemorizedSpellSlotAfterScript);
            _event.RegisterEvent<HealerKitBeforeEvent>(EventScript.NWNXOnHealerKitBeforeScript);
            _event.RegisterEvent<HealerKitAfterEvent>(EventScript.NWNXOnHealerKitAfterScript);
            _event.RegisterEvent<HealBeforeEvent>(EventScript.NWNXOnHealBeforeScript);
            _event.RegisterEvent<HealAfterEvent>(EventScript.NWNXOnHealAfterScript);
            _event.RegisterEvent<PartyLeaveBeforeEvent>(EventScript.NWNXOnPartyLeaveBeforeScript);
            _event.RegisterEvent<PartyLeaveAfterEvent>(EventScript.NWNXOnPartyLeaveAfterScript);
            _event.RegisterEvent<PartyKickBeforeEvent>(EventScript.NWNXOnPartyKickBeforeScript);
            _event.RegisterEvent<PartyKickAfterEvent>(EventScript.NWNXOnPartyKickAfterScript);
            _event.RegisterEvent<PartyTransferLeadershipBeforeEvent>(EventScript.NWNXOnPartyTransferLeadershipBeforeScript);
            _event.RegisterEvent<PartyTransferLeadershipAfterEvent>(EventScript.NWNXOnPartyTransferLeadershipAfterScript);
            _event.RegisterEvent<PartyInviteBeforeEvent>(EventScript.NWNXOnPartyInviteBeforeScript);
            _event.RegisterEvent<PartyInviteAfterEvent>(EventScript.NWNXOnPartyInviteAfterScript);
            _event.RegisterEvent<PartyIgnoreInvitationBeforeEvent>(EventScript.NWNXOnPartyIgnoreInvitationBeforeScript);
            _event.RegisterEvent<PartyIgnoreInvitationAfterEvent>(EventScript.NWNXOnPartyIgnoreInvitationAfterScript);
            _event.RegisterEvent<PartyAcceptInvitationBeforeEvent>(EventScript.NWNXOnPartyAcceptInvitationBeforeScript);
            _event.RegisterEvent<PartyAcceptInvitationAfterEvent>(EventScript.NWNXOnPartyAcceptInvitationAfterScript);
            _event.RegisterEvent<PartyRejectInvitationBeforeEvent>(EventScript.NWNXOnPartyRejectInvitationBeforeScript);
            _event.RegisterEvent<PartyRejectInvitationAfterEvent>(EventScript.NWNXOnPartyRejectInvitationAfterScript);
            _event.RegisterEvent<PartyKickHenchmanBeforeEvent>(EventScript.NWNXOnPartyKickHenchmanBeforeScript);
            _event.RegisterEvent<PartyKickHenchmanAfterEvent>(EventScript.NWNXOnPartyKickHenchmanAfterScript);
            _event.RegisterEvent<CombatModeOnEvent>(EventScript.NWNXOnCombatModeOnScript);
            _event.RegisterEvent<CombatModeOffEvent>(EventScript.NWNXOnCombatModeOffScript);
            _event.RegisterEvent<UseSkillBeforeEvent>(EventScript.NWNXOnUseSkillBeforeScript);
            _event.RegisterEvent<UseSkillAfterEvent>(EventScript.NWNXOnUseSkillAfterScript);
            _event.RegisterEvent<MapPinAddPinBeforeEvent>(EventScript.NWNXOnMapPinAddPinBeforeScript);
            _event.RegisterEvent<MapPinAddPinAfterEvent>(EventScript.NWNXOnMapPinAddPinAfterScript);
            _event.RegisterEvent<MapPinChangePinBeforeEvent>(EventScript.NWNXOnMapPinChangePinBeforeScript);
            _event.RegisterEvent<MapPinChangePinAfterEvent>(EventScript.NWNXOnMapPinChangePinAfterScript);
            _event.RegisterEvent<MapPinDestroyPinBeforeEvent>(EventScript.NWNXOnMapPinDestroyPinBeforeScript);
            _event.RegisterEvent<MapPinDestroyPinAfterEvent>(EventScript.NWNXOnMapPinDestroyPinAfterScript);
            _event.RegisterEvent<DoListenDetectionBeforeEvent>(EventScript.NWNXOnDoListenDetectionBeforeScript);
            _event.RegisterEvent<DoListenDetectionAfterEvent>(EventScript.NWNXOnDoListenDetectionAfterScript);
            _event.RegisterEvent<DoSpotDetectionBeforeEvent>(EventScript.NWNXOnDoSpotDetectionBeforeScript);
            _event.RegisterEvent<DoSpotDetectionAfterEvent>(EventScript.NWNXOnDoSpotDetectionAfterScript);
            _event.RegisterEvent<PolymorphBeforeEvent>(EventScript.NWNXOnPolymorphBeforeScript);
            _event.RegisterEvent<PolymorphAfterEvent>(EventScript.NWNXOnPolymorphAfterScript);
            _event.RegisterEvent<UnpolymorphBeforeEvent>(EventScript.NWNXOnUnpolymorphBeforeScript);
            _event.RegisterEvent<UnpolymorphAfterEvent>(EventScript.NWNXOnUnpolymorphAfterScript);
            _event.RegisterEvent<EffectAppliedBeforeEvent>(EventScript.NWNXOnEffectAppliedBeforeScript);
            _event.RegisterEvent<EffectAppliedAfterEvent>(EventScript.NWNXOnEffectAppliedAfterScript);
            _event.RegisterEvent<EffectRemovedBeforeEvent>(EventScript.NWNXOnEffectRemovedBeforeScript);
            _event.RegisterEvent<EffectRemovedAfterEvent>(EventScript.NWNXOnEffectRemovedAfterScript);
            _event.RegisterEvent<QuickchatBeforeEvent>(EventScript.NWNXOnQuickchatBeforeScript);
            _event.RegisterEvent<QuickchatAfterEvent>(EventScript.NWNXOnQuickchatAfterScript);
            _event.RegisterEvent<InventoryOpenBeforeEvent>(EventScript.NWNXOnInventoryOpenBeforeScript);
            _event.RegisterEvent<InventoryOpenAfterEvent>(EventScript.NWNXOnInventoryOpenAfterScript);
            _event.RegisterEvent<InventorySelectPanelBeforeEvent>(EventScript.NWNXOnInventorySelectPanelBeforeScript);
            _event.RegisterEvent<InventorySelectPanelAfterEvent>(EventScript.NWNXOnInventorySelectPanelAfterScript);
            _event.RegisterEvent<BarterStartBeforeEvent>(EventScript.NWNXOnBarterStartBeforeScript);
            _event.RegisterEvent<BarterStartAfterEvent>(EventScript.NWNXOnBarterStartAfterScript);
            _event.RegisterEvent<BarterEndBeforeEvent>(EventScript.NWNXOnBarterEndBeforeScript);
            _event.RegisterEvent<BarterEndAfterEvent>(EventScript.NWNXOnBarterEndAfterScript);
            _event.RegisterEvent<TrapDisarmBeforeEvent>(EventScript.NWNXOnTrapDisarmBeforeScript);
            _event.RegisterEvent<TrapDisarmAfterEvent>(EventScript.NWNXOnTrapDisarmAfterScript);
            _event.RegisterEvent<TrapEnterBeforeEvent>(EventScript.NWNXOnTrapEnterBeforeScript);
            _event.RegisterEvent<TrapEnterAfterEvent>(EventScript.NWNXOnTrapEnterAfterScript);
            _event.RegisterEvent<TrapExamineBeforeEvent>(EventScript.NWNXOnTrapExamineBeforeScript);
            _event.RegisterEvent<TrapExamineAfterEvent>(EventScript.NWNXOnTrapExamineAfterScript);
            _event.RegisterEvent<TrapFlagBeforeEvent>(EventScript.NWNXOnTrapFlagBeforeScript);
            _event.RegisterEvent<TrapFlagAfterEvent>(EventScript.NWNXOnTrapFlagAfterScript);
            _event.RegisterEvent<TrapRecoverBeforeEvent>(EventScript.NWNXOnTrapRecoverBeforeScript);
            _event.RegisterEvent<TrapRecoverAfterEvent>(EventScript.NWNXOnTrapRecoverAfterScript);
            _event.RegisterEvent<TrapSetBeforeEvent>(EventScript.NWNXOnTrapSetBeforeScript);
            _event.RegisterEvent<TrapSetAfterEvent>(EventScript.NWNXOnTrapSetAfterScript);
            _event.RegisterEvent<TimingBarStartBeforeEvent>(EventScript.NWNXOnTimingBarStartBeforeScript);
            _event.RegisterEvent<TimingBarStartAfterEvent>(EventScript.NWNXOnTimingBarStartAfterScript);
            _event.RegisterEvent<TimingBarStopBeforeEvent>(EventScript.NWNXOnTimingBarStopBeforeScript);
            _event.RegisterEvent<TimingBarStopAfterEvent>(EventScript.NWNXOnTimingBarStopAfterScript);
            _event.RegisterEvent<TimingBarCancelBeforeEvent>(EventScript.NWNXOnTimingBarCancelBeforeScript);
            _event.RegisterEvent<TimingBarCancelAfterEvent>(EventScript.NWNXOnTimingBarCancelAfterScript);
            _event.RegisterEvent<WebhookSuccessEvent>(EventScript.NWNXOnWebhookSuccessScript);
            _event.RegisterEvent<WebhookFailureEvent>(EventScript.NWNXOnWebhookFailureScript);
            _event.RegisterEvent<CheckStickyPlayerNameReservedBeforeEvent>(EventScript.NWNXOnCheckStickyPlayerNameReservedBeforeScript);
            _event.RegisterEvent<CheckStickyPlayerNameReservedAfterEvent>(EventScript.NWNXOnCheckStickyPlayerNameReservedAfterScript);
            _event.RegisterEvent<LevelUpBeforeEvent>(EventScript.NWNXOnLevelUpBeforeScript);
            _event.RegisterEvent<LevelUpAfterEvent>(EventScript.NWNXOnLevelUpAfterScript);
            _event.RegisterEvent<LevelUpAutomaticBeforeEvent>(EventScript.NWNXOnLevelUpAutomaticBeforeScript);
            _event.RegisterEvent<LevelUpAutomaticAfterEvent>(EventScript.NWNXOnLevelUpAutomaticAfterScript);
            _event.RegisterEvent<LevelDownBeforeEvent>(EventScript.NWNXOnLevelDownBeforeScript);
            _event.RegisterEvent<LevelDownAfterEvent>(EventScript.NWNXOnLevelDownAfterScript);
            _event.RegisterEvent<InventoryAddItemBeforeEvent>(EventScript.NWNXOnInventoryAddItemBeforeScript);
            _event.RegisterEvent<InventoryAddItemAfterEvent>(EventScript.NWNXOnInventoryAddItemAfterScript);
            _event.RegisterEvent<InventoryRemoveItemBeforeEvent>(EventScript.NWNXOnInventoryRemoveItemBeforeScript);
            _event.RegisterEvent<InventoryRemoveItemAfterEvent>(EventScript.NWNXOnInventoryRemoveItemAfterScript);
            _event.RegisterEvent<InventoryAddGoldBeforeEvent>(EventScript.NWNXOnInventoryAddGoldBeforeScript);
            _event.RegisterEvent<InventoryAddGoldAfterEvent>(EventScript.NWNXOnInventoryAddGoldAfterScript);
            _event.RegisterEvent<InventoryRemoveGoldBeforeEvent>(EventScript.NWNXOnInventoryRemoveGoldBeforeScript);
            _event.RegisterEvent<InventoryRemoveGoldAfterEvent>(EventScript.NWNXOnInventoryRemoveGoldAfterScript);
            _event.RegisterEvent<PvpAttitudeChangeBeforeEvent>(EventScript.NWNXOnPvpAttitudeChangeBeforeScript);
            _event.RegisterEvent<PvpAttitudeChangeAfterEvent>(EventScript.NWNXOnPvpAttitudeChangeAfterScript);
            _event.RegisterEvent<InputWalkToWaypointBeforeEvent>(EventScript.NWNXOnInputWalkToWaypointBeforeScript);
            _event.RegisterEvent<InputWalkToWaypointAfterEvent>(EventScript.NWNXOnInputWalkToWaypointAfterScript);
            _event.RegisterEvent<MaterialChangeBeforeEvent>(EventScript.NWNXOnMaterialChangeBeforeScript);
            _event.RegisterEvent<MaterialChangeAfterEvent>(EventScript.NWNXOnMaterialChangeAfterScript);
            _event.RegisterEvent<InputAttackObjectBeforeEvent>(EventScript.NWNXOnInputAttackObjectBeforeScript);
            _event.RegisterEvent<InputAttackObjectAfterEvent>(EventScript.NWNXOnInputAttackObjectAfterScript);
            _event.RegisterEvent<ObjectLockBeforeEvent>(EventScript.NWNXOnObjectLockBeforeScript);
            _event.RegisterEvent<ObjectLockAfterEvent>(EventScript.NWNXOnObjectLockAfterScript);
            _event.RegisterEvent<ObjectUnlockBeforeEvent>(EventScript.NWNXOnObjectUnlockBeforeScript);
            _event.RegisterEvent<ObjectUnlockAfterEvent>(EventScript.NWNXOnObjectUnlockAfterScript);
            _event.RegisterEvent<UuidCollisionBeforeEvent>(EventScript.NWNXOnUuidCollisionBeforeScript);
            _event.RegisterEvent<UuidCollisionAfterEvent>(EventScript.NWNXOnUuidCollisionAfterScript);
            _event.RegisterEvent<ElcValidateCharacterBeforeEvent>(EventScript.NWNXOnElcValidateCharacterBeforeScript);
            _event.RegisterEvent<ElcValidateCharacterAfterEvent>(EventScript.NWNXOnElcValidateCharacterAfterScript);
            _event.RegisterEvent<QuickbarSetButtonBeforeEvent>(EventScript.NWNXOnQuickbarSetButtonBeforeScript);
            _event.RegisterEvent<QuickbarSetButtonAfterEvent>(EventScript.NWNXOnQuickbarSetButtonAfterScript);
            _event.RegisterEvent<CalendarHourEvent>(EventScript.NWNXOnCalendarHourScript);
            _event.RegisterEvent<CalendarDayEvent>(EventScript.NWNXOnCalendarDayScript);
            _event.RegisterEvent<CalendarMonthEvent>(EventScript.NWNXOnCalendarMonthScript);
            _event.RegisterEvent<CalendarYearEvent>(EventScript.NWNXOnCalendarYearScript);
            _event.RegisterEvent<CalendarDawnEvent>(EventScript.NWNXOnCalendarDawnScript);
            _event.RegisterEvent<CalendarDuskEvent>(EventScript.NWNXOnCalendarDuskScript);
            _event.RegisterEvent<BroadcastCastSpellBeforeEvent>(EventScript.NWNXOnBroadcastCastSpellBeforeScript);
            _event.RegisterEvent<BroadcastCastSpellAfterEvent>(EventScript.NWNXOnBroadcastCastSpellAfterScript);
            _event.RegisterEvent<DebugRunScriptBeforeEvent>(EventScript.NWNXOnDebugRunScriptBeforeScript);
            _event.RegisterEvent<DebugRunScriptAfterEvent>(EventScript.NWNXOnDebugRunScriptAfterScript);
            _event.RegisterEvent<DebugRunScriptChunkBeforeEvent>(EventScript.NWNXOnDebugRunScriptChunkBeforeScript);
            _event.RegisterEvent<DebugRunScriptChunkAfterEvent>(EventScript.NWNXOnDebugRunScriptChunkAfterScript);
            _event.RegisterEvent<StoreRequestBuyBeforeEvent>(EventScript.NWNXOnStoreRequestBuyBeforeScript);
            _event.RegisterEvent<StoreRequestBuyAfterEvent>(EventScript.NWNXOnStoreRequestBuyAfterScript);
            _event.RegisterEvent<StoreRequestSellBeforeEvent>(EventScript.NWNXOnStoreRequestSellBeforeScript);
            _event.RegisterEvent<StoreRequestSellAfterEvent>(EventScript.NWNXOnStoreRequestSellAfterScript);
            _event.RegisterEvent<InputDropItemBeforeEvent>(EventScript.NWNXOnInputDropItemBeforeScript);
            _event.RegisterEvent<InputDropItemAfterEvent>(EventScript.NWNXOnInputDropItemAfterScript);
            _event.RegisterEvent<BroadcastAttackOfOpportunityBeforeEvent>(EventScript.NWNXOnBroadcastAttackOfOpportunityBeforeScript);
            _event.RegisterEvent<BroadcastAttackOfOpportunityAfterEvent>(EventScript.NWNXOnBroadcastAttackOfOpportunityAfterScript);
            _event.RegisterEvent<CombatAttackOfOpportunityBeforeEvent>(EventScript.NWNXOnCombatAttackOfOpportunityBeforeScript);
            _event.RegisterEvent<CombatAttackOfOpportunityAfterEvent>(EventScript.NWNXOnCombatAttackOfOpportunityAfterScript);
        }

        private void HookNWNXEvents()
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

    }
}
