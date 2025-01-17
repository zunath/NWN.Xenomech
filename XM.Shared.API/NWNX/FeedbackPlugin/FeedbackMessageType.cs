﻿namespace XM.Shared.API.NWNX.FeedbackPlugin
{
    public enum FeedbackMessageType
    {
        SkillCantUse = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_SKILL_CANT_USE,
        SkillCantUseTimer = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_SKILL_CANT_USE_TIMER,
        SkillAnimalEmpathyValidTargets = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_SKILL_ANIMALEMPATHY_VALID_TARGETS,
        SkillTauntValidTargets = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_SKILL_TAUNT_VALID_TARGETS,
        SkillTauntTargetImmune = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_SKILL_TAUNT_TARGET_IMMUNE,
        SkillPickpocketStoleItem = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_SKILL_PICKPOCKET_STOLE_ITEM,
        SkillPickpocketStoleGold = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_SKILL_PICKPOCKET_STOLE_GOLD,
        SkillPickpocketAttemptingToSteal = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_SKILL_PICKPOCKET_ATTEMPTING_TO_STEAL,
        SkillPickpocketAttemptDetected = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_SKILL_PICKPOCKET_ATTEMPT_DETECTED,
        SkillPickpocketStoleItemTarget = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_SKILL_PICKPOCKET_STOLE_ITEM_TARGET,
        SkillPickpocketStoleGoldTarget = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_SKILL_PICKPOCKET_STOLE_GOLD_TARGET,
        SkillPickpocketTargetBroke = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_SKILL_PICKPOCKET_TARGET_BROKE,
        SkillHealTargetNotDispsnd = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_SKILL_HEAL_TARGET_NOT_DISPSND,
        SkillHealValidTargets = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_SKILL_HEAL_VALID_TARGETS,
        SkillStealthInCombat = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_SKILL_STEALTH_IN_COMBAT,
        TargetUnaware = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_TARGET_UNAWARE,
        ActionNotPossibleStatus = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ACTION_NOT_POSSIBLE_STATUS,
        ActionNotPossiblePvp = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ACTION_NOT_POSSIBLE_PVP,
        ActionCantReachTarget = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ACTION_CANT_REACH_TARGET,
        ActionNoLoot = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ACTION_NO_LOOT,
        WeightTooEncumberedToRun = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_WEIGHT_TOO_ENCUMBERED_TO_RUN,
        WeightTooEncumberedWalkSlow = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_WEIGHT_TOO_ENCUMBERED_WALK_SLOW,
        WeightTooEncumberedCantPickup = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_WEIGHT_TOO_ENCUMBERED_CANT_PICKUP,
        StatsLevelUp = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_STATS_LEVELUP,
        InventoryFull = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_INVENTORY_FULL,
        ContainerFull = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CONTAINER_FULL,
        TrapTriggered = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_TRAP_TRIGGERED,
        DamageHealed = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_DAMAGE_HEALED,
        ExperienceGained = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EXPERIENCE_GAINNED,
        ExperienceLost = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EXPERIENCE_LOST,
        JournalUpdated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_JOURNALUPDATED,
        BarterCancelled = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_BARTER_CANCELLED,
        DetectModeActivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_DETECT_MODE_ACTIVATED,
        DetectModeDeactivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_DETECT_MODE_DEACTIVATED,
        StealthModeActivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_STEALTH_MODE_ACTIVATED,
        StealthModeDeactivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_STEALTH_MODE_DEACTIVATED,
        ParryModeActivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PARRY_MODE_ACTIVATED,
        ParryModeDeactivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PARRY_MODE_DEACTIVATED,
        PowerAttackModeActivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_POWER_ATTACK_MODE_ACTIVATED,
        PowerAttackModeDeactivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_POWER_ATTACK_MODE_DEACTIVATED,
        ImprovedPowerAttackModeActivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMPROVED_POWER_ATTACK_MODE_ACTIVATED,
        ImprovedPowerAttackModeDeactivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMPROVED_POWER_ATTACK_MODE_DEACTIVATED,
        RapidShotModeActivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_RAPID_SHOT_MODE_ACTIVATED,
        RapidShotModeDeactivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_RAPID_SHOT_MODE_DEACTIVATED,
        FlurryOfBlowsModeActivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_FLURRY_OF_BLOWS_MODE_ACTIVATED,
        FlurryOfBlowsModeDeactivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_FLURRY_OF_BLOWS_MODE_DEACTIVATED,
        ExpertiseModeActivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EXPERTISE_MODE_ACTIVATED,
        ExpertiseModeDeactivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EXPERTISE_MODE_DEACTIVATED,
        ImprovedExpertiseModeActivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMPROVED_EXPERTISE_MODE_ACTIVATED,
        ImprovedExpertiseModeDeactivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMPROVED_EXPERTISE_MODE_DEACTIVATED,
        DefensiveCastModeActivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_DEFENSIVE_CAST_MODE_ACTIVATED,
        DefensiveCastModeDeactivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_DEFENSIVE_CAST_MODE_DEACTIVATED,
        ModeCannotUseWeapons = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_MODE_CANNOT_USE_WEAPONS,
        DirtyFightingModeActivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_DIRTY_FIGHTING_MODE_ACTIVATED,
        DirtyFightingModeDeactivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_DIRTY_FIGHTING_MODE_DEACTIVATED,
        DefensiveStanceModeActivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_DEFENSIVE_STANCE_MODE_ACTIVATED,
        DefensiveStanceModeDeactivated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_DEFENSIVE_STANCE_MODE_DEACTIVATED,
        EquipSkillSpellModifiers = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EQUIP_SKILL_SPELL_MODIFIERS,
        EquipUnidentified = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EQUIP_UNIDENTIFIED,
        EquipMonkAbilities = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EQUIP_MONK_ABILITIES,
        EquipInsufficientLevel = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EQUIP_INSUFFICIENT_LEVEL,
        EquipProficiencies = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EQUIP_PROFICIENCIES,
        EquipWeaponTooLarge = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EQUIP_WEAPON_TOO_LARGE,
        EquipWeaponTooSmall = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EQUIP_WEAPON_TOO_SMALL,
        EquipOneHandedWeapon = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EQUIP_ONE_HANDED_WEAPON,
        EquipTwoHandedWeapon = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EQUIP_TWO_HANDED_WEAPON,
        EquipWeaponSwappedOut = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EQUIP_WEAPON_SWAPPED_OUT,
        EquipOneChainWeapon = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EQUIP_ONE_CHAIN_WEAPON,
        EquipNaturalAcNoStack = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EQUIP_NATURAL_AC_NO_STACK,
        EquipArmourAcNoStack = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EQUIP_ARMOUR_AC_NO_STACK,
        EquipShieldAcNoStack = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EQUIP_SHIELD_AC_NO_STACK,
        EquipDeflectionAcNoStack = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EQUIP_DEFLECTION_AC_NO_STACK,
        EquipNoArmorCombat = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EQUIP_NO_ARMOR_COMBAT,
        EquipRangerAbilities = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EQUIP_RANGER_ABILITIES,
        EquipAlignment = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EQUIP_ALIGNMENT,
        EquipClass = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EQUIP_CLASS,
        EquipRace = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EQUIP_RACE,
        UnequipNoArmorCombat = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_UNEQUIP_NO_ARMOR_COMBAT,
        ObjectLocked = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_OBJECT_LOCKED,
        ObjectNotLocked = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_OBJECT_NOT_LOCKED,
        ObjectSpecialKey = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_OBJECT_SPECIAL_KEY,
        ObjectUsedKey = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_OBJECT_USED_KEY,
        RestExcitedCantRest = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_REST_EXCITED_CANT_REST,
        RestBeginningRest = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_REST_BEGINNING_REST,
        RestFinishedRest = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_REST_FINISHED_REST,
        RestCancelRest = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_REST_CANCEL_REST,
        RestNotAllowedInArea = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_REST_NOT_ALLOWED_IN_AREA,
        RestNotAllowedByPossessedFamiliar = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_REST_NOT_ALLOWED_BY_POSSESSED_FAMILIAR,
        RestNotAllowedEnemies = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_REST_NOT_ALLOWED_ENEMIES,
        RestCantUnderThisEffect = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_REST_CANT_UNDER_THIS_EFFECT,
        CastLostTarget = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CAST_LOST_TARGET,
        CastCantCast = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CAST_CANT_CAST,
        CastCntrspellTargetLostTarget = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CAST_CNTRSPELL_TARGET_LOST_TARGET,
        CastArcaneSpellFailure = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CAST_ARCANE_SPELL_FAILURE,
        CastCntrspellTargetArcaneSpellFailure = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CAST_CNTRSPELL_TARGET_ARCANE_SPELL_FAILURE,
        CastEntangleConcentrationFailure = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CAST_ENTANGLE_CONCENTRATION_FAILURE,
        CastCntrspellTargetEntangleConcentrationFailure = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CAST_CNTRSPELL_TARGET_ENTANGLE_CONCENTRATION_FAILURE,
        CastSpellInterrupted = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CAST_SPELL_INTERRUPTED,
        CastEffectSpellFailure = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CAST_EFFECT_SPELL_FAILURE,
        CastCantCastWhilePolymorphed = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CAST_CANT_CAST_WHILE_POLYMORPHED,
        CastUseHands = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CAST_USE_HANDS,
        CastUseMouth = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CAST_USE_MOUTH,
        CastDefcastConcentrationFailure = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CAST_DEFCAST_CONCENTRATION_FAILURE,
        CastDefcastConcentrationSuccess = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CAST_DEFCAST_CONCENTRATION_SUCCESS,
        UseItemCantUse = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_USEITEM_CANT_USE,
        ConversationToofar = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CONVERSATION_TOOFAR,
        ConversationBusy = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CONVERSATION_BUSY,
        ConversationInCombat = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CONVERSATION_IN_COMBAT,
        CharacterInTransit = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CHARACTER_INTRANSIT,
        CharacterOutTransit = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CHARACTER_OUTTRANSIT,
        UseItemNotEquipped = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_USEITEM_NOT_EQUIPPED,
        DropItemCantDrop = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_DROPITEM_CANT_DROP,
        DropItemCantGive = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_DROPITEM_CANT_GIVE,
        ClientServerSpellMismatch = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CLIENT_SERVER_SPELL_MISMATCH,
        CombatRunningOutOfAmmo = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_COMBAT_RUNNING_OUT_OF_AMMO,
        CombatOutOfAmmo = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_COMBAT_OUT_OF_AMMO,
        CombatHenchmanOutOfAmmo = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_COMBAT_HENCHMAN_OUT_OF_AMMO,
        CombatDamageImmunity = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_COMBAT_DAMAGE_IMMUNITY,
        CombatSpellImmunity = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_COMBAT_SPELL_IMMUNITY,
        CombatDamageResistance = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_COMBAT_DAMAGE_RESISTANCE,
        CombatDamageResistanceRemaining = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_COMBAT_DAMAGE_RESISTANCE_REMAINING,
        CombatDamageReduction = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_COMBAT_DAMAGE_REDUCTION,
        CombatDamageReductionRemaining = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_COMBAT_DAMAGE_REDUCTION_REMAINING,
        CombatSpellLevelAbsorption = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_COMBAT_SPELL_LEVEL_ABSORPTION,
        CombatSpellLevelAbsorptionRemaining = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_COMBAT_SPELL_LEVEL_ABSORPTION_REMAINING,
        CombatWeaponNotEffective = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_COMBAT_WEAPON_NOT_EFFECTIVE,
        CombatEpicDodgeAttackEvaded = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_COMBAT_EPIC_DODGE_ATTACK_EVADED,
        CombatMassiveDamage = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_COMBAT_MASSIVE_DAMAGE,
        CombatSavedVsMassiveDamage = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_COMBAT_SAVED_VS_MASSIVE_DAMAGE,
        CombatSavedVsDevastatingCritical = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_COMBAT_SAVED_VS_DEVASTATING_CRITICAL,
        FeatSapValidTargets = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_FEAT_SAP_VALID_TARGETS,
        FeatKnockdownValidTargets = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_FEAT_KNOCKDOWN_VALID_TARGETS,
        FeatImpKnockdownValidTargets = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_FEAT_IMPKNOCKDOWN_VALID_TARGETS,
        FeatCalledShotNoLegs = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_FEAT_CALLED_SHOT_NO_LEGS,
        FeatCalledShotNoArms = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_FEAT_CALLED_SHOT_NO_ARMS,
        FeatSmiteGoodTargetNotGood = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_FEAT_SMITE_GOOD_TARGET_NOT_GOOD,
        FeatSmiteEvilTargetNotEvil = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_FEAT_SMITE_EVIL_TARGET_NOT_EVIL,
        FeatQuiveringPalmHigherLevel = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_FEAT_QUIVERING_PALM_HIGHER_LEVEL,
        FeatKeenSenseDetect = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_FEAT_KEEN_SENSE_DETECT,
        FeatUseUnarmed = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_FEAT_USE_UNARMED,
        FeatUses = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_FEAT_USES,
        FeatUseWeaponOfChoice = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_FEAT_USE_WEAPON_OF_CHOICE,
        PartyNewLeader = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PARTY_NEW_LEADER,
        PartyMemberKicked = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PARTY_MEMBER_KICKED,
        PartyKickedYou = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PARTY_KICKED_YOU,
        PartyAlreadyConsidering = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PARTY_ALREADY_CONSIDERING,
        PartyAlreadyInvolved = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PARTY_ALREADY_INVOLVED,
        PartySentInvitation = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PARTY_SENT_INVITATION,
        PartyReceivedInvitation = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PARTY_RECEIVED_INVITATION,
        PartyJoined = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PARTY_JOINED,
        PartyInvitationIgnored = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PARTY_INVITATION_IGNORED,
        PartyYouIgnoredInvitation = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PARTY_YOU_IGNORED_INVITATION,
        PartyInvitationRejected = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PARTY_INVITATION_REJECTED,
        PartyYouRejectedInvitation = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PARTY_YOU_REJECTED_INVITATION,
        PartyInvitationExpired = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PARTY_INVITATION_EXPIRED,
        PartyLeftParty = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PARTY_LEFT_PARTY,
        PartyYouLeft = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PARTY_YOU_LEFT,
        PartyHenchmanLimit = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PARTY_HENCHMAN_LIMIT,
        PartyCannotLeaveTheOneParty = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PARTY_CANNOT_LEAVE_THE_ONE_PARTY,
        PartyCannotKickFromTheOneParty = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PARTY_CANNOT_KICK_FROM_THE_ONE_PARTY,
        PartyYouInvitedNonSingleton = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PARTY_YOU_INVITED_NON_SINGLETON,
        PvpReactionDislikesYou = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PVP_REACTION_DISLIKESYOU,
        ItemReceived = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ITEM_RECEIVED,
        ItemLost = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ITEM_LOST,
        ItemEjected = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ITEM_EJECTED,
        ItemUseUnidentified = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ITEM_USE_UNIDENTIFIED,
        ItemGoldGained = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ITEM_GOLD_GAINED,
        ItemGoldLost = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ITEM_GOLD_LOST,
        LearnScrollNotScroll = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_LEARN_SCROLL_NOT_SCROLL,
        LearnScrollCantLearnClass = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_LEARN_SCROLL_CANT_LEARN_CLASS,
        LearnScrollCantLearnLevel = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_LEARN_SCROLL_CANT_LEARN_LEVEL,
        LearnScrollCantLearnAbility = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_LEARN_SCROLL_CANT_LEARN_ABILITY,
        LearnScrollCantLearnOpposition = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_LEARN_SCROLL_CANT_LEARN_OPPOSITION,
        LearnScrollCantLearnPossess = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_LEARN_SCROLL_CANT_LEARN_POSSESS,
        LearnScrollCantLearnKnown = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_LEARN_SCROLL_CANT_LEARN_KNOWN,
        LearnScrollCantLearnDivine = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_LEARN_SCROLL_CANT_LEARN_DIVINE,
        LearnScrollSuccess = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_LEARN_SCROLL_SUCCESS,
        FloatyTextStrref = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_FLOATY_TEXT_STRREF,
        FloatyTextString = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_FLOATY_TEXT_STRING,
        CannotSellPlotItem = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CANNOT_SELL_PLOT_ITEM,
        CannotSellContainer = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CANNOT_SELL_CONTAINER,
        CannotSellItem = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CANNOT_SELL_ITEM,
        NotEnoughGold = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_NOT_ENOUGH_GOLD,
        TransactionSucceeded = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_TRANSACTION_SUCCEEDED,
        PriceTooHigh = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PRICE_TOO_HIGH,
        StoreNotEnoughGold = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_STORE_NOT_ENOUGH_GOLD,
        CannotSellStolenItem = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CANNOT_SELL_STOLEN_ITEM,
        CannotSellRestrictedItem = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CANNOT_SELL_RESTRICTED_ITEM,
        PortalTimedout = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PORTAL_TIMEDOUT,
        PortalInvalid = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PORTAL_INVALID,
        ChatTellPlayerNotFound = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CHAT_TELL_PLAYER_NOT_FOUND,
        AlignmentShift = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ALIGNMENT_SHIFT,
        AlignmentPartyShift = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ALIGNMENT_PARTY_SHIFT,
        AlignmentChange = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ALIGNMENT_CHANGE,
        AlignmentRestrictedByClassLost = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ALIGNMENT_RESTRICTED_BY_CLASS_LOST,
        AlignmentRestrictedByClassGain = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ALIGNMENT_RESTRICTED_BY_CLASS_GAIN,
        AlignmentRestrictedWarningLoss = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ALIGNMENT_RESTRICTED_WARNING_LOSS,
        AlignmentRestrictedWarningGain = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ALIGNMENT_RESTRICTED_WARNING_GAIN,
        AlignmentEpitomeGained = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ALIGNMENT_EPITOME_GAINED,
        AlignmentEpitomeLost = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ALIGNMENT_EPITOME_LOST,
        ImmunityDisease = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_DISEASE,
        ImmunityCriticalHit = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_CRITICAL_HIT,
        ImmunityDeathMagic = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_DEATH_MAGIC,
        ImmunityFear = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_FEAR,
        ImmunityKnockdown = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_KNOCKDOWN,
        ImmunityParalysis = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_PARALYSIS,
        ImmunityNegativeLevel = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_NEGATIVE_LEVEL,
        ImmunityMindSpells = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_MIND_SPELLS,
        ImmunityPoison = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_POISON,
        ImmunitySneakAttack = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_SNEAK_ATTACK,
        ImmunitySleep = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_SLEEP,
        ImmunityDaze = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_DAZE,
        ImmunityConfusion = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_CONFUSION,
        ImmunityStun = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_STUN,
        ImmunityBlindness = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_BLINDNESS,
        ImmunityDeafness = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_DEAFNESS,
        ImmunityCurse = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_CURSE,
        ImmunityCharm = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_CHARM,
        ImmunityDominate = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_DOMINATE,
        ImmunityEntangle = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_ENTANGLE,
        ImmunitySilence = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_SILENCE,
        ImmunitySlow = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_IMMUNITY_SLOW,
        AssociateSummoned = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ASSOCIATE_SUMMONED,
        AssociateUnsummoning = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ASSOCIATE_UNSUMMONING,
        AssociateUnsummoningBecauseRest = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ASSOCIATE_UNSUMMONING_BECAUSE_REST,
        AssociateUnsummoningBecauseDied = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ASSOCIATE_UNSUMMONING_BECAUSE_DIED,
        AssociateDominated = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ASSOCIATE_DOMINATED,
        AssociateDominationEnded = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ASSOCIATE_DOMINATION_ENDED,
        AssociatePossessedCannotRecoverTrap = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ASSOCIATE_POSSESSED_CANNOT_RECOVER_TRAP,
        AssociatePossessedCannotBarter = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ASSOCIATE_POSSESSED_CANNOT_BARTER,
        AssociatePossessedCannotEquip = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ASSOCIATE_POSSESSED_CANNOT_EQUIP,
        AssociatePossessedCannotRepositoryMove = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ASSOCIATE_POSSESSED_CANNOT_REPOSITORY_MOVE,
        AssociatePossessedCannotPickUp = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ASSOCIATE_POSSESSED_CANNOT_PICK_UP,
        AssociatePossessedCannotDrop = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ASSOCIATE_POSSESSED_CANNOT_DROP,
        AssociatePossessedCannotUnequip = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ASSOCIATE_POSSESSED_CANNOT_UNEQUIP,
        AssociatePossessedCannotRest = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ASSOCIATE_POSSESSED_CANNOT_REST,
        AssociatePossessedCannotDialogue = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ASSOCIATE_POSSESSED_CANNOT_DIALOGUE,
        AssociatePossessedCannotGiveItem = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ASSOCIATE_POSSESSED_CANNOT_GIVE_ITEM,
        AssociatePossessedCannotTakeItem = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ASSOCIATE_POSSESSED_CANNOT_TAKE_ITEM,
        AssociatePossessedCannotUseContainer = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ASSOCIATE_POSSESSED_CANNOT_USE_CONTAINER,
        ScriptError = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_SCRIPT_ERROR,
        ActionListOverflow = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ACTION_LIST_OVERFLOW,
        EffectListOverflow = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EFFECT_LIST_OVERFLOW,
        AiUpdateTimeOverflow = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_AI_UPDATE_TIME_OVERFLOW,
        ActionListWipeOverflow = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_ACTION_LIST_WIPE_OVERFLOW,
        EffectListWipeOverflow = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_EFFECT_LIST_WIPE_OVERFLOW,
        SendMessageToPc = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_SEND_MESSAGE_TO_PC,
        SendMessageToPcStrref = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_SEND_MESSAGE_TO_PC_STRREF,
        GuiOnlyPartyLeaderMayClick = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_GUI_ONLY_PARTY_LEADER_MAY_CLICK,
        Paused = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_PAUSED,
        Unpaused = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_UNPAUSED,
        RestYouMayNotAtThisTime = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_REST_YOU_MAY_NOT_AT_THIS_TIME,
        GuiCharExportRequestSent = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_GUI_CHAR_EXPORT_REQUEST_SENT,
        GuiCharExportedSuccessfully = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_GUI_CHAR_EXPORTED_SUCCESSFULLY,
        GuiErrorCharNotExported = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_GUI_ERROR_CHAR_NOT_EXPORTED,
        CameraBg = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CAMERA_BG,
        CameraEq = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CAMERA_EQ,
        CameraChasecam = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_CAMERA_CHASECAM,
        Saving = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_SAVING,
        SaveComplete = NWN.Core.NWNX.FeedbackPlugin.NWNX_FEEDBACK_SAVE_COMPLETE


    }
}
