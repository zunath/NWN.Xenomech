using Anvil.Services;
using NLog;
using NWN.Core.NWNX;

namespace XM.Core.EventManagement.Registration
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
            _event.RegisterEvent<NWNXEvent.OnNWNXChat>(EventScript.NWNXOnChat);
            _event.RegisterEvent<NWNXEvent.OnModulePreload>(EventScript.NWNXOnModulePreloadScript);
            _event.RegisterEvent<NWNXEvent.OnAddAssociateBefore>(EventScript.NWNXOnAddAssociateBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnAddAssociateAfter>(EventScript.NWNXOnAddAssociateAfterScript);
            _event.RegisterEvent<NWNXEvent.OnRemoveAssociateBefore>(EventScript.NWNXOnRemoveAssociateBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnRemoveAssociateAfter>(EventScript.NWNXOnRemoveAssociateAfterScript);
            _event.RegisterEvent<NWNXEvent.OnStealthEnterBefore>(EventScript.NWNXOnStealthEnterBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnStealthEnterAfter>(EventScript.NWNXOnStealthEnterAfterScript);
            _event.RegisterEvent<NWNXEvent.OnStealthExitBefore>(EventScript.NWNXOnStealthExitBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnStealthExitAfter>(EventScript.NWNXOnStealthExitAfterScript);
            _event.RegisterEvent<NWNXEvent.OnExamineObjectBefore>(EventScript.NWNXOnExamineObjectBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnExamineObjectAfter>(EventScript.NWNXOnExamineObjectAfterScript);
            _event.RegisterEvent<NWNXEvent.OnValidateUseItemBefore>(EventScript.NWNXOnValidateUseItemBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnValidateUseItemAfter>(EventScript.NWNXOnValidateUseItemAfterScript);
            _event.RegisterEvent<NWNXEvent.OnUseItemBefore>(EventScript.NWNXOnUseItemBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnUseItemAfter>(EventScript.NWNXOnUseItemAfterScript);
            _event.RegisterEvent<NWNXEvent.OnItemInventoryOpenBefore>(EventScript.NWNXOnItemInventoryOpenBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnItemInventoryOpenAfter>(EventScript.NWNXOnItemInventoryOpenAfterScript);
            _event.RegisterEvent<NWNXEvent.OnItemInventoryCloseBefore>(EventScript.NWNXOnItemInventoryCloseBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnItemInventoryCloseAfter>(EventScript.NWNXOnItemInventoryCloseAfterScript);
            _event.RegisterEvent<NWNXEvent.OnItemAmmoReloadBefore>(EventScript.NWNXOnItemAmmoReloadBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnItemAmmoReloadAfter>(EventScript.NWNXOnItemAmmoReloadAfterScript);
            _event.RegisterEvent<NWNXEvent.OnItemScrollLearnBefore>(EventScript.NWNXOnItemScrollLearnBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnItemScrollLearnAfter>(EventScript.NWNXOnItemScrollLearnAfterScript);
            _event.RegisterEvent<NWNXEvent.OnValidateItemEquipBefore>(EventScript.NWNXOnValidateItemEquipBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnValidateItemEquipAfter>(EventScript.NWNXOnValidateItemEquipAfterScript);
            _event.RegisterEvent<NWNXEvent.OnItemEquipBefore>(EventScript.NWNXOnItemEquipBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnItemEquipAfter>(EventScript.NWNXOnItemEquipAfterScript);
            _event.RegisterEvent<NWNXEvent.OnItemUnequipBefore>(EventScript.NWNXOnItemUnequipBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnItemUnequipAfter>(EventScript.NWNXOnItemUnequipAfterScript);
            _event.RegisterEvent<NWNXEvent.OnItemDestroyObjectBefore>(EventScript.NWNXOnItemDestroyObjectBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnItemDestroyObjectAfter>(EventScript.NWNXOnItemDestroyObjectAfterScript);
            _event.RegisterEvent<NWNXEvent.OnItemDecrementStacksizeBefore>(EventScript.NWNXOnItemDecrementStacksizeBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnItemDecrementStacksizeAfter>(EventScript.NWNXOnItemDecrementStacksizeAfterScript);
            _event.RegisterEvent<NWNXEvent.OnItemUseLoreBefore>(EventScript.NWNXOnItemUseLoreBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnItemUseLoreAfter>(EventScript.NWNXOnItemUseLoreAfterScript);
            _event.RegisterEvent<NWNXEvent.OnItemPayToIdentifyBefore>(EventScript.NWNXOnItemPayToIdentifyBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnItemPayToIdentifyAfter>(EventScript.NWNXOnItemPayToIdentifyAfterScript);
            _event.RegisterEvent<NWNXEvent.OnItemSplitBefore>(EventScript.NWNXOnItemSplitBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnItemSplitAfter>(EventScript.NWNXOnItemSplitAfterScript);
            _event.RegisterEvent<NWNXEvent.OnItemMergeBefore>(EventScript.NWNXOnItemMergeBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnItemMergeAfter>(EventScript.NWNXOnItemMergeAfterScript);
            _event.RegisterEvent<NWNXEvent.OnItemAcquireBefore>(EventScript.NWNXOnItemAcquireBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnItemAcquireAfter>(EventScript.NWNXOnItemAcquireAfterScript);
            _event.RegisterEvent<NWNXEvent.OnUseFeatBefore>(EventScript.NWNXOnUseFeatBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnUseFeatAfter>(EventScript.NWNXOnUseFeatAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmGiveGoldBefore>(EventScript.NWNXOnDmGiveGoldBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmGiveGoldAfter>(EventScript.NWNXOnDmGiveGoldAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmGiveXpBefore>(EventScript.NWNXOnDmGiveXpBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmGiveXpAfter>(EventScript.NWNXOnDmGiveXpAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmGiveLevelBefore>(EventScript.NWNXOnDmGiveLevelBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmGiveLevelAfter>(EventScript.NWNXOnDmGiveLevelAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmGiveAlignmentBefore>(EventScript.NWNXOnDmGiveAlignmentBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmGiveAlignmentAfter>(EventScript.NWNXOnDmGiveAlignmentAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmSpawnObjectBefore>(EventScript.NWNXOnDmSpawnObjectBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmSpawnObjectAfter>(EventScript.NWNXOnDmSpawnObjectAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmGiveItemBefore>(EventScript.NWNXOnDmGiveItemBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmGiveItemAfter>(EventScript.NWNXOnDmGiveItemAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmHealBefore>(EventScript.NWNXOnDmHealBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmHealAfter>(EventScript.NWNXOnDmHealAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmKillBefore>(EventScript.NWNXOnDmKillBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmKillAfter>(EventScript.NWNXOnDmKillAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmToggleInvulnerableBefore>(EventScript.NWNXOnDmToggleInvulnerableBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmToggleInvulnerableAfter>(EventScript.NWNXOnDmToggleInvulnerableAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmForceRestBefore>(EventScript.NWNXOnDmForceRestBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmForceRestAfter>(EventScript.NWNXOnDmForceRestAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmLimboBefore>(EventScript.NWNXOnDmLimboBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmLimboAfter>(EventScript.NWNXOnDmLimboAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmToggleAiBefore>(EventScript.NWNXOnDmToggleAiBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmToggleAiAfter>(EventScript.NWNXOnDmToggleAiAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmToggleImmortalBefore>(EventScript.NWNXOnDmToggleImmortalBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmToggleImmortalAfter>(EventScript.NWNXOnDmToggleImmortalAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmGotoBefore>(EventScript.NWNXOnDmGotoBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmGotoAfter>(EventScript.NWNXOnDmGotoAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmPossessBefore>(EventScript.NWNXOnDmPossessBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmPossessAfter>(EventScript.NWNXOnDmPossessAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmPossessFullPowerBefore>(EventScript.NWNXOnDmPossessFullPowerBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmPossessFullPowerAfter>(EventScript.NWNXOnDmPossessFullPowerAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmToggleLockBefore>(EventScript.NWNXOnDmToggleLockBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmToggleLockAfter>(EventScript.NWNXOnDmToggleLockAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmDisableTrapBefore>(EventScript.NWNXOnDmDisableTrapBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmDisableTrapAfter>(EventScript.NWNXOnDmDisableTrapAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmJumpToPointBefore>(EventScript.NWNXOnDmJumpToPointBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmJumpToPointAfter>(EventScript.NWNXOnDmJumpToPointAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmJumpTargetToPointBefore>(EventScript.NWNXOnDmJumpTargetToPointBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmJumpTargetToPointAfter>(EventScript.NWNXOnDmJumpTargetToPointAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmJumpAllPlayersToPointBefore>(EventScript.NWNXOnDmJumpAllPlayersToPointBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmJumpAllPlayersToPointAfter>(EventScript.NWNXOnDmJumpAllPlayersToPointAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmChangeDifficultyBefore>(EventScript.NWNXOnDmChangeDifficultyBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmChangeDifficultyAfter>(EventScript.NWNXOnDmChangeDifficultyAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmViewInventoryBefore>(EventScript.NWNXOnDmViewInventoryBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmViewInventoryAfter>(EventScript.NWNXOnDmViewInventoryAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmSpawnTrapOnObjectBefore>(EventScript.NWNXOnDmSpawnTrapOnObjectBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmSpawnTrapOnObjectAfter>(EventScript.NWNXOnDmSpawnTrapOnObjectAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmDumpLocalsBefore>(EventScript.NWNXOnDmDumpLocalsBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmDumpLocalsAfter>(EventScript.NWNXOnDmDumpLocalsAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmAppearBefore>(EventScript.NWNXOnDmAppearBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmAppearAfter>(EventScript.NWNXOnDmAppearAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmDisappearBefore>(EventScript.NWNXOnDmDisappearBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmDisappearAfter>(EventScript.NWNXOnDmDisappearAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmSetFactionBefore>(EventScript.NWNXOnDmSetFactionBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmSetFactionAfter>(EventScript.NWNXOnDmSetFactionAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmTakeItemBefore>(EventScript.NWNXOnDmTakeItemBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmTakeItemAfter>(EventScript.NWNXOnDmTakeItemAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmSetStatBefore>(EventScript.NWNXOnDmSetStatBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmSetStatAfter>(EventScript.NWNXOnDmSetStatAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmGetVariableBefore>(EventScript.NWNXOnDmGetVariableBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmGetVariableAfter>(EventScript.NWNXOnDmGetVariableAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmSetVariableBefore>(EventScript.NWNXOnDmSetVariableBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmSetVariableAfter>(EventScript.NWNXOnDmSetVariableAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmSetTimeBefore>(EventScript.NWNXOnDmSetTimeBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmSetTimeAfter>(EventScript.NWNXOnDmSetTimeAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmSetDateBefore>(EventScript.NWNXOnDmSetDateBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmSetDateAfter>(EventScript.NWNXOnDmSetDateAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmSetFactionReputationBefore>(EventScript.NWNXOnDmSetFactionReputationBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmSetFactionReputationAfter>(EventScript.NWNXOnDmSetFactionReputationAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDmGetFactionReputationBefore>(EventScript.NWNXOnDmGetFactionReputationBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDmGetFactionReputationAfter>(EventScript.NWNXOnDmGetFactionReputationAfterScript);
            _event.RegisterEvent<NWNXEvent.OnClientDisconnectBefore>(EventScript.NWNXOnClientDisconnectBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnClientDisconnectAfter>(EventScript.NWNXOnClientDisconnectAfterScript);
            _event.RegisterEvent<NWNXEvent.OnClientConnectBefore>(EventScript.NWNXOnClientConnectBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnClientConnectAfter>(EventScript.NWNXOnClientConnectAfterScript);
            _event.RegisterEvent<NWNXEvent.OnStartCombatRoundBefore>(EventScript.NWNXOnStartCombatRoundBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnStartCombatRoundAfter>(EventScript.NWNXOnStartCombatRoundAfterScript);
            _event.RegisterEvent<NWNXEvent.OnCastSpellBefore>(EventScript.NWNXOnCastSpellBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnCastSpellAfter>(EventScript.NWNXOnCastSpellAfterScript);
            _event.RegisterEvent<NWNXEvent.OnSetMemorizedSpellSlotBefore>(EventScript.NWNXSetMemorizedSpellSlotBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnSetMemorizedSpellSlotAfter>(EventScript.NWNXSetMemorizedSpellSlotAfterScript);
            _event.RegisterEvent<NWNXEvent.OnClearMemorizedSpellSlotBefore>(EventScript.NWNXClearMemorizedSpellSlotBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnClearMemorizedSpellSlotAfter>(EventScript.NWNXClearMemorizedSpellSlotAfterScript);
            _event.RegisterEvent<NWNXEvent.OnHealerKitBefore>(EventScript.NWNXOnHealerKitBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnHealerKitAfter>(EventScript.NWNXOnHealerKitAfterScript);
            _event.RegisterEvent<NWNXEvent.OnHealBefore>(EventScript.NWNXOnHealBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnHealAfter>(EventScript.NWNXOnHealAfterScript);
            _event.RegisterEvent<NWNXEvent.OnPartyLeaveBefore>(EventScript.NWNXOnPartyLeaveBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnPartyLeaveAfter>(EventScript.NWNXOnPartyLeaveAfterScript);
            _event.RegisterEvent<NWNXEvent.OnPartyKickBefore>(EventScript.NWNXOnPartyKickBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnPartyKickAfter>(EventScript.NWNXOnPartyKickAfterScript);
            _event.RegisterEvent<NWNXEvent.OnPartyTransferLeadershipBefore>(EventScript.NWNXOnPartyTransferLeadershipBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnPartyTransferLeadershipAfter>(EventScript.NWNXOnPartyTransferLeadershipAfterScript);
            _event.RegisterEvent<NWNXEvent.OnPartyInviteBefore>(EventScript.NWNXOnPartyInviteBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnPartyInviteAfter>(EventScript.NWNXOnPartyInviteAfterScript);
            _event.RegisterEvent<NWNXEvent.OnPartyIgnoreInvitationBefore>(EventScript.NWNXOnPartyIgnoreInvitationBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnPartyIgnoreInvitationAfter>(EventScript.NWNXOnPartyIgnoreInvitationAfterScript);
            _event.RegisterEvent<NWNXEvent.OnPartyAcceptInvitationBefore>(EventScript.NWNXOnPartyAcceptInvitationBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnPartyAcceptInvitationAfter>(EventScript.NWNXOnPartyAcceptInvitationAfterScript);
            _event.RegisterEvent<NWNXEvent.OnPartyRejectInvitationBefore>(EventScript.NWNXOnPartyRejectInvitationBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnPartyRejectInvitationAfter>(EventScript.NWNXOnPartyRejectInvitationAfterScript);
            _event.RegisterEvent<NWNXEvent.OnPartyKickHenchmanBefore>(EventScript.NWNXOnPartyKickHenchmanBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnPartyKickHenchmanAfter>(EventScript.NWNXOnPartyKickHenchmanAfterScript);
            _event.RegisterEvent<NWNXEvent.OnCombatModeOn>(EventScript.NWNXOnCombatModeOnScript);
            _event.RegisterEvent<NWNXEvent.OnCombatModeOff>(EventScript.NWNXOnCombatModeOffScript);
            _event.RegisterEvent<NWNXEvent.OnUseSkillBefore>(EventScript.NWNXOnUseSkillBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnUseSkillAfter>(EventScript.NWNXOnUseSkillAfterScript);
            _event.RegisterEvent<NWNXEvent.OnMapPinAddPinBefore>(EventScript.NWNXOnMapPinAddPinBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnMapPinAddPinAfter>(EventScript.NWNXOnMapPinAddPinAfterScript);
            _event.RegisterEvent<NWNXEvent.OnMapPinChangePinBefore>(EventScript.NWNXOnMapPinChangePinBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnMapPinChangePinAfter>(EventScript.NWNXOnMapPinChangePinAfterScript);
            _event.RegisterEvent<NWNXEvent.OnMapPinDestroyPinBefore>(EventScript.NWNXOnMapPinDestroyPinBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnMapPinDestroyPinAfter>(EventScript.NWNXOnMapPinDestroyPinAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDoListenDetectionBefore>(EventScript.NWNXOnDoListenDetectionBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDoListenDetectionAfter>(EventScript.NWNXOnDoListenDetectionAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDoSpotDetectionBefore>(EventScript.NWNXOnDoSpotDetectionBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDoSpotDetectionAfter>(EventScript.NWNXOnDoSpotDetectionAfterScript);
            _event.RegisterEvent<NWNXEvent.OnPolymorphBefore>(EventScript.NWNXOnPolymorphBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnPolymorphAfter>(EventScript.NWNXOnPolymorphAfterScript);
            _event.RegisterEvent<NWNXEvent.OnUnpolymorphBefore>(EventScript.NWNXOnUnpolymorphBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnUnpolymorphAfter>(EventScript.NWNXOnUnpolymorphAfterScript);
            _event.RegisterEvent<NWNXEvent.OnEffectAppliedBefore>(EventScript.NWNXOnEffectAppliedBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnEffectAppliedAfter>(EventScript.NWNXOnEffectAppliedAfterScript);
            _event.RegisterEvent<NWNXEvent.OnEffectRemovedBefore>(EventScript.NWNXOnEffectRemovedBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnEffectRemovedAfter>(EventScript.NWNXOnEffectRemovedAfterScript);
            _event.RegisterEvent<NWNXEvent.OnQuickchatBefore>(EventScript.NWNXOnQuickchatBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnQuickchatAfter>(EventScript.NWNXOnQuickchatAfterScript);
            _event.RegisterEvent<NWNXEvent.OnInventoryOpenBefore>(EventScript.NWNXOnInventoryOpenBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnInventoryOpenAfter>(EventScript.NWNXOnInventoryOpenAfterScript);
            _event.RegisterEvent<NWNXEvent.OnInventorySelectPanelBefore>(EventScript.NWNXOnInventorySelectPanelBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnInventorySelectPanelAfter>(EventScript.NWNXOnInventorySelectPanelAfterScript);
            _event.RegisterEvent<NWNXEvent.OnBarterStartBefore>(EventScript.NWNXOnBarterStartBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnBarterStartAfter>(EventScript.NWNXOnBarterStartAfterScript);
            _event.RegisterEvent<NWNXEvent.OnBarterEndBefore>(EventScript.NWNXOnBarterEndBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnBarterEndAfter>(EventScript.NWNXOnBarterEndAfterScript);
            _event.RegisterEvent<NWNXEvent.OnTrapDisarmBefore>(EventScript.NWNXOnTrapDisarmBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnTrapDisarmAfter>(EventScript.NWNXOnTrapDisarmAfterScript);
            _event.RegisterEvent<NWNXEvent.OnTrapEnterBefore>(EventScript.NWNXOnTrapEnterBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnTrapEnterAfter>(EventScript.NWNXOnTrapEnterAfterScript);
            _event.RegisterEvent<NWNXEvent.OnTrapExamineBefore>(EventScript.NWNXOnTrapExamineBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnTrapExamineAfter>(EventScript.NWNXOnTrapExamineAfterScript);
            _event.RegisterEvent<NWNXEvent.OnTrapFlagBefore>(EventScript.NWNXOnTrapFlagBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnTrapFlagAfter>(EventScript.NWNXOnTrapFlagAfterScript);
            _event.RegisterEvent<NWNXEvent.OnTrapRecoverBefore>(EventScript.NWNXOnTrapRecoverBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnTrapRecoverAfter>(EventScript.NWNXOnTrapRecoverAfterScript);
            _event.RegisterEvent<NWNXEvent.OnTrapSetBefore>(EventScript.NWNXOnTrapSetBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnTrapSetAfter>(EventScript.NWNXOnTrapSetAfterScript);
            _event.RegisterEvent<NWNXEvent.OnTimingBarStartBefore>(EventScript.NWNXOnTimingBarStartBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnTimingBarStartAfter>(EventScript.NWNXOnTimingBarStartAfterScript);
            _event.RegisterEvent<NWNXEvent.OnTimingBarStopBefore>(EventScript.NWNXOnTimingBarStopBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnTimingBarStopAfter>(EventScript.NWNXOnTimingBarStopAfterScript);
            _event.RegisterEvent<NWNXEvent.OnTimingBarCancelBefore>(EventScript.NWNXOnTimingBarCancelBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnTimingBarCancelAfter>(EventScript.NWNXOnTimingBarCancelAfterScript);
            _event.RegisterEvent<NWNXEvent.OnWebhookSuccess>(EventScript.NWNXOnWebhookSuccessScript);
            _event.RegisterEvent<NWNXEvent.OnWebhookFailure>(EventScript.NWNXOnWebhookFailureScript);
            _event.RegisterEvent<NWNXEvent.OnCheckStickyPlayerNameReservedBefore>(EventScript.NWNXOnCheckStickyPlayerNameReservedBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnCheckStickyPlayerNameReservedAfter>(EventScript.NWNXOnCheckStickyPlayerNameReservedAfterScript);
            _event.RegisterEvent<NWNXEvent.OnLevelUpBefore>(EventScript.NWNXOnLevelUpBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnLevelUpAfter>(EventScript.NWNXOnLevelUpAfterScript);
            _event.RegisterEvent<NWNXEvent.OnLevelUpAutomaticBefore>(EventScript.NWNXOnLevelUpAutomaticBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnLevelUpAutomaticAfter>(EventScript.NWNXOnLevelUpAutomaticAfterScript);
            _event.RegisterEvent<NWNXEvent.OnLevelDownBefore>(EventScript.NWNXOnLevelDownBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnLevelDownAfter>(EventScript.NWNXOnLevelDownAfterScript);
            _event.RegisterEvent<NWNXEvent.OnInventoryAddItemBefore>(EventScript.NWNXOnInventoryAddItemBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnInventoryAddItemAfter>(EventScript.NWNXOnInventoryAddItemAfterScript);
            _event.RegisterEvent<NWNXEvent.OnInventoryRemoveItemBefore>(EventScript.NWNXOnInventoryRemoveItemBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnInventoryRemoveItemAfter>(EventScript.NWNXOnInventoryRemoveItemAfterScript);
            _event.RegisterEvent<NWNXEvent.OnInventoryAddGoldBefore>(EventScript.NWNXOnInventoryAddGoldBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnInventoryAddGoldAfter>(EventScript.NWNXOnInventoryAddGoldAfterScript);
            _event.RegisterEvent<NWNXEvent.OnInventoryRemoveGoldBefore>(EventScript.NWNXOnInventoryRemoveGoldBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnInventoryRemoveGoldAfter>(EventScript.NWNXOnInventoryRemoveGoldAfterScript);
            _event.RegisterEvent<NWNXEvent.OnPvpAttitudeChangeBefore>(EventScript.NWNXOnPvpAttitudeChangeBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnPvpAttitudeChangeAfter>(EventScript.NWNXOnPvpAttitudeChangeAfterScript);
            _event.RegisterEvent<NWNXEvent.OnInputWalkToWaypointBefore>(EventScript.NWNXOnInputWalkToWaypointBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnInputWalkToWaypointAfter>(EventScript.NWNXOnInputWalkToWaypointAfterScript);
            _event.RegisterEvent<NWNXEvent.OnMaterialChangeBefore>(EventScript.NWNXOnMaterialChangeBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnMaterialChangeAfter>(EventScript.NWNXOnMaterialChangeAfterScript);
            _event.RegisterEvent<NWNXEvent.OnInputAttackObjectBefore>(EventScript.NWNXOnInputAttackObjectBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnInputAttackObjectAfter>(EventScript.NWNXOnInputAttackObjectAfterScript);
            _event.RegisterEvent<NWNXEvent.OnObjectLockBefore>(EventScript.NWNXOnObjectLockBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnObjectLockAfter>(EventScript.NWNXOnObjectLockAfterScript);
            _event.RegisterEvent<NWNXEvent.OnObjectUnlockBefore>(EventScript.NWNXOnObjectUnlockBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnObjectUnlockAfter>(EventScript.NWNXOnObjectUnlockAfterScript);
            _event.RegisterEvent<NWNXEvent.OnUuidCollisionBefore>(EventScript.NWNXOnUuidCollisionBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnUuidCollisionAfter>(EventScript.NWNXOnUuidCollisionAfterScript);
            _event.RegisterEvent<NWNXEvent.OnElcValidateCharacterBefore>(EventScript.NWNXOnElcValidateCharacterBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnElcValidateCharacterAfter>(EventScript.NWNXOnElcValidateCharacterAfterScript);
            _event.RegisterEvent<NWNXEvent.OnQuickbarSetButtonBefore>(EventScript.NWNXOnQuickbarSetButtonBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnQuickbarSetButtonAfter>(EventScript.NWNXOnQuickbarSetButtonAfterScript);
            _event.RegisterEvent<NWNXEvent.OnCalendarHour>(EventScript.NWNXOnCalendarHourScript);
            _event.RegisterEvent<NWNXEvent.OnCalendarDay>(EventScript.NWNXOnCalendarDayScript);
            _event.RegisterEvent<NWNXEvent.OnCalendarMonth>(EventScript.NWNXOnCalendarMonthScript);
            _event.RegisterEvent<NWNXEvent.OnCalendarYear>(EventScript.NWNXOnCalendarYearScript);
            _event.RegisterEvent<NWNXEvent.OnCalendarDawn>(EventScript.NWNXOnCalendarDawnScript);
            _event.RegisterEvent<NWNXEvent.OnCalendarDusk>(EventScript.NWNXOnCalendarDuskScript);
            _event.RegisterEvent<NWNXEvent.OnBroadcastCastSpellBefore>(EventScript.NWNXOnBroadcastCastSpellBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnBroadcastCastSpellAfter>(EventScript.NWNXOnBroadcastCastSpellAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDebugRunScriptBefore>(EventScript.NWNXOnDebugRunScriptBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDebugRunScriptAfter>(EventScript.NWNXOnDebugRunScriptAfterScript);
            _event.RegisterEvent<NWNXEvent.OnDebugRunScriptChunkBefore>(EventScript.NWNXOnDebugRunScriptChunkBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnDebugRunScriptChunkAfter>(EventScript.NWNXOnDebugRunScriptChunkAfterScript);
            _event.RegisterEvent<NWNXEvent.OnStoreRequestBuyBefore>(EventScript.NWNXOnStoreRequestBuyBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnStoreRequestBuyAfter>(EventScript.NWNXOnStoreRequestBuyAfterScript);
            _event.RegisterEvent<NWNXEvent.OnStoreRequestSellBefore>(EventScript.NWNXOnStoreRequestSellBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnStoreRequestSellAfter>(EventScript.NWNXOnStoreRequestSellAfterScript);
            _event.RegisterEvent<NWNXEvent.OnInputDropItemBefore>(EventScript.NWNXOnInputDropItemBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnInputDropItemAfter>(EventScript.NWNXOnInputDropItemAfterScript);
            _event.RegisterEvent<NWNXEvent.OnBroadcastAttackOfOpportunityBefore>(EventScript.NWNXOnBroadcastAttackOfOpportunityBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnBroadcastAttackOfOpportunityAfter>(EventScript.NWNXOnBroadcastAttackOfOpportunityAfterScript);
            _event.RegisterEvent<NWNXEvent.OnCombatAttackOfOpportunityBefore>(EventScript.NWNXOnCombatAttackOfOpportunityBeforeScript);
            _event.RegisterEvent<NWNXEvent.OnCombatAttackOfOpportunityAfter>(EventScript.NWNXOnCombatAttackOfOpportunityAfterScript);
        }

        private void HookNWNXEvents()
        {
            // Chat event
            ChatPlugin.RegisterChatScript(EventScript.NWNXOnChat);

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
