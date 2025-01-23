using System;
using System.Runtime.InteropServices;
using Anvil.API;
using Anvil.Services;
using NWN.Native.API;

namespace XM.Combat.NativeOverride
{
    [ServiceBinding(typeof(OnAIActionAttackObject))]
    internal sealed unsafe class OnAIActionAttackObject
    {
        private const int ACTION_IN_PROGRESS = 1;
        private const int ACTION_COMPLETE = 2;
        private const int ACTION_FAILED = 3;

        private const int SANCTUARY_SAVE_FAILED = 1;
        private const float CNW_PATHFIND_TOLERANCE = 0.01f;

        private const ushort AISTATE_CREATURE_ABLE_TO_GO_HOSTILE = 0x0080;
        private const ushort AISTATE_CREATURE_USE_HANDS = 0x0004;

        private const int NWANIMBASE_ANIM_PAUSE = 0;
        private const int FEEDBACK_ACTION_CANT_REACH_TARGET = 218;

        private const int CSERVERAIMASTER_AIACTION_ATTACKOBJECT = 12;
        private const int CSERVERAIMASTER_AIACTION_CHECKMOVETOOBJECTRADIUS = 17;
        private const int CSERVERAIMASTER_AIACTION_CHANGEFACINGOBJECT = 19;
        private const int CNWSOBJECTACTION_PARAMETER_INTEGER = 1;
        private const int CNWSOBJECTACTION_PARAMETER_FLOAT = 2;
        private const int CNWSOBJECTACTION_PARAMETER_OBJECT = 3;

        private const int CNWSCOMBATROUND_TYPE_INVALID = 0;
        private const int CNWSCOMBATROUND_TYPE_ATTACK = 1;
        private const int CNWSCOMBATROUND_TYPE_REACTION = 3;
        private const int CNWSCOMBATROUND_TYPE_COMSTEP = 4;
        private const int CNWSCOMBATROUND_TYPE_COMSTEPFB = 5;
        private const int CNWSCOMBATROUND_TYPE_EQUIP = 6;
        private const int CNWSCOMBATROUND_TYPE_UNEQUIP = 7;
        private const int CNWSCOMBATROUND_TYPE_PARRY = 8;

        private const int WEAPON_ATTACK_TYPE_OFFHAND = 2;

        private const int COMBAT_STEP_TYPE_ADJUST = 0;
        private const int COMBAT_STEP_TYPE_APPROPRIATE = 1;

        private const int CRULES_MULTIPLE_ATTACKS_BAB_PENALTY_MULTIPLIER = 5; // todo: read from rules

        private const int NWANIMBASE_ANIM_ATTACK = 9;



        [NativeFunction("_ZN12CNWSCreature20AIActionAttackObjectEP20CNWSObjectActionNode", "")]
        private delegate int AIActionAttackObjectHook(void* pCreature, void* pNode);

        private readonly FunctionHook<AIActionAttackObjectHook> _aiActionAttackObjectHook;

        public OnAIActionAttackObject(HookService hook)
        {
            _aiActionAttackObjectHook = hook.RequestHook<AIActionAttackObjectHook>(HandleAIActionAttackObject, HookOrder.Late);
        }

        private bool IsAIState(ushort nAIState, CNWSCreature pCreature)
        {
            return ((pCreature.m_nAIState & nAIState) == nAIState);
        }

        private int HandleAIActionAttackObject(void* creature, void* node)
        {
            var pCreature = CNWSCreature.FromPointer(creature);
            var pNode = CNWSObjectActionNode.FromPointer(node);

            uint oidAttackTarget, oidArea;

            CNWSArea pArea = pCreature.GetArea();

            // This action was just run... reset
            // the combat round update time.
            // - BKH - May/21/02
            pCreature.m_nLastCombatRoundUpdate = 6000;

            /*	inline BOOL        IsAIState(uint16_t nAIState)                 {return ((m_nAIState & nAIState) == nAIState);}*/
            if (pCreature.GetDead() == 1 ||
                pCreature.GetIsPCDying() == 1 ||
                !IsAIState(AISTATE_CREATURE_ABLE_TO_GO_HOSTILE, pCreature) ||
                !IsAIState(AISTATE_CREATURE_USE_HANDS, pCreature))
            {
                pCreature.ChangeAttackTarget(pNode, OBJECT_INVALID);
                return ACTION_FAILED;
            }

            oidAttackTarget = (uint)pNode.m_pParameter[0];

            // You cannot attack yourself
            if (oidAttackTarget == pCreature.m_idSelf)
            {
                pCreature.SetAnimation(NWANIMBASE_ANIM_PAUSE);
                return ACTION_FAILED;
            }

            CGameObject pGameObject = (CGameObject)NWNXLib.g_pAppManager.m_pServerExoApp.GetGameObject(oidAttackTarget);

            bool bTargetActive = false;
            if (pGameObject != null)
            {
                if (pGameObject.AsNWSObject() != null)
                {
                    if (pGameObject.AsNWSObject().GetDead() == 0)
                    {
                        bTargetActive = true;
                    }

                    if (pGameObject.AsNWSCreature() != null &&
                        pGameObject.AsNWSCreature().GetDead() == 0 &&
                        pGameObject.AsNWSCreature().m_bPlayerCharacter == 1 &&
                        pGameObject.AsNWSCreature().GetIsPCDying() == 1)
                    {
                        bTargetActive = true;
                    }

                    // If the target is invisible and we can't see or hear them,
                    // then they aren't an acceptable target.
                    CNWVisibilityNode pVisNode = pCreature.GetVisibleListElement(oidAttackTarget);
                    if (pVisNode != null)
                    {
                        if (pVisNode.m_nSanctuary == SANCTUARY_SAVE_FAILED ||
                            (pVisNode.m_bInvisible == 1 &&
                             pVisNode.m_bHeard == 0 &&
                             pVisNode.m_bSeen == 0))
                        {
                            bTargetActive = false;
                        }
                    }
                    else
                    {
                        if (pGameObject.AsNWSCreature() != null &&
                            pCreature.m_bPlayerCharacter == 1)
                        //pCreature.GetIsDM() == false)
                        {
                            bTargetActive = false;
                        }
                    }
                }
            }

            if (bTargetActive)
            {
                CNWSObject pTarget = pGameObject.AsNWSObject();
                Vector vTargetPosition = pTarget.m_vPosition;
                CNWSArea pTargetArea = pTarget.GetArea();

                float fMaxAttackRange = pCreature.MaxAttackRange(oidAttackTarget);
                float fDesiredAttackRange = pCreature.DesiredAttackRange(oidAttackTarget);
                if (pCreature.m_oidAttemptedAttackTarget == OBJECT_INVALID)
                {
                    pCreature.m_oidAttemptedAttackTarget = oidAttackTarget;
                }

                float fUseRange = 0;

                if (pGameObject.AsNWSCreature() != null)
                {
                    IntPtr pFUseRange = Marshal.AllocHGlobal(sizeof(float));

                    try
                    {
                        Marshal.StructureToPtr(fUseRange, pFUseRange, false);
                        pCreature.GetUseRange(oidAttackTarget, vTargetPosition, (float*)pFUseRange);
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(pFUseRange);
                    }
                }

                int bClearLineOfAttack = 0;

                if (pArea != null && pArea == pTargetArea)
                {
                    bClearLineOfAttack = pCreature.CheckAttackClearLineToTarget(oidAttackTarget, vTargetPosition, pArea);
                }

                var vDelta = new Vector(
                        pCreature.m_vPosition.x - vTargetPosition.x,
                        pCreature.m_vPosition.y - vTargetPosition.y,
                        pCreature.m_vPosition.z - vTargetPosition.z
                    );
                bool bOutsideAttackRange = (pTargetArea != pArea ||
                                            MagnitudeSquared(vDelta) > Sqr(fMaxAttackRange + CNW_PATHFIND_TOLERANCE));

                if (bOutsideAttackRange || bClearLineOfAttack == 0)
                {
                    if (pCreature.m_bPassiveAttackBehaviour == 1)
                    {
                        CNWSCreature newTarget = pCreature.GetNewCombatTarget(oidAttackTarget);
                        oidAttackTarget = OBJECT_INVALID;

                        if (newTarget != null)
                        {
                            oidAttackTarget = newTarget.m_idSelf;
                            pCreature.m_bPassiveAttackBehaviour = 1;
                        }

                        pCreature.ChangeAttackTarget(pNode, oidAttackTarget);
                        return (oidAttackTarget != OBJECT_INVALID ? ACTION_IN_PROGRESS : ACTION_FAILED);
                    }

                    if (pTargetArea != null)
                    {
                        oidArea = pTargetArea.m_idSelf;
                    }
                    else
                    {
                        if (pTarget.AsNWSCreature() != null && pCreature.m_oidEncounter == OBJECT_INVALID)
                        {
                            oidArea = pTarget.AsNWSCreature().m_oidDesiredArea;
                            vTargetPosition = pTarget.AsNWSCreature().m_vDesiredAreaLocation;
                        }
                        else
                        {
                            CNWSCreature newTarget = pCreature.GetNewCombatTarget(oidAttackTarget);
                            oidAttackTarget = OBJECT_INVALID;

                            if (newTarget != null)
                            {
                                oidAttackTarget = newTarget.m_idSelf;
                                pCreature.m_bPassiveAttackBehaviour = 1;
                            }

                            pCreature.ChangeAttackTarget(pNode, oidAttackTarget);
                            return (oidAttackTarget != OBJECT_INVALID ? ACTION_IN_PROGRESS : ACTION_FAILED);
                        }
                    }

                    if (pCreature.m_vLastAttackPosition != new Vector() &&  // todo: could be problematic based on C++ code
                        pCreature.m_vLastAttackPosition == pCreature.m_vPosition)
                    {
                        if (pCreature.m_bPlayerCharacter == 1)
                        {
                            pCreature.SendFeedbackMessage(FEEDBACK_ACTION_CANT_REACH_TARGET);
                        }

                        CNWSCreature newTarget = null;

                        if (pCreature.GetRangeWeaponEquipped() == 0)
                        {
                            newTarget = pCreature.GetNewCombatTarget(oidAttackTarget);
                        }

                        bool bUpdateTarget = false;
                        if (newTarget != null)
                        {
                            oidAttackTarget = newTarget.m_idSelf;
                            pCreature.m_bPassiveAttackBehaviour = 1;
                            bUpdateTarget = true;
                        }
                        else if (pCreature.m_bPlayerCharacter == 1)
                        {
                            oidAttackTarget = OBJECT_INVALID;
                            bUpdateTarget = true;
                        }

                        if (bUpdateTarget)
                        {
                            pCreature.ChangeAttackTarget(pNode, oidAttackTarget);
                            return (oidAttackTarget != OBJECT_INVALID ? ACTION_IN_PROGRESS : ACTION_FAILED);
                        }
                    }
                    else
                    {
                        pCreature.m_vLastAttackPosition = pCreature.m_vPosition;
                    }

                    float fMoveToTargetRange = fDesiredAttackRange;
                    float fMoveToTargetMaxRange = fMaxAttackRange;

                    if (bClearLineOfAttack == 0)
                    {
                        if (!bOutsideAttackRange)
                        {
                            if (pTarget.AsNWSCreature() != null)
                            {
                                fMoveToTargetRange = pCreature.m_pcPathfindInformation.m_fCreaturePersonalSpace;
                                fMoveToTargetRange += pTarget.AsNWSCreature().m_pcPathfindInformation.m_fCreaturePersonalSpace;
                            }
                            else
                            {
                                fMoveToTargetRange = pCreature.m_pcPathfindInformation.m_fPersonalSpace;
                            }
                        }

                        pCreature.m_pcPathfindInformation.m_bUsePlotGridPath = 1;
                    }

                    var bRunToTarget = true;
                    var bLineOfSightRequired = true;

                    void* pOidAttackTarget = &oidAttackTarget;
                    pCreature.AddActionToFront(
                        CSERVERAIMASTER_AIACTION_ATTACKOBJECT,
                        pNode.m_nGroupActionId,
                        CNWSOBJECTACTION_PARAMETER_OBJECT,
                        pOidAttackTarget);

                    pCreature.AddActionToFront(
                        CSERVERAIMASTER_AIACTION_CHECKMOVETOOBJECTRADIUS,
                        pNode.m_nGroupActionId,
                        CNWSOBJECTACTION_PARAMETER_OBJECT, pOidAttackTarget,
                        CNWSOBJECTACTION_PARAMETER_INTEGER, &bRunToTarget,
                        CNWSOBJECTACTION_PARAMETER_FLOAT, &fMoveToTargetRange,
                        CNWSOBJECTACTION_PARAMETER_FLOAT, &fMoveToTargetMaxRange,
                        CNWSOBJECTACTION_PARAMETER_INTEGER, &bLineOfSightRequired);

                    pCreature.AddActionToFront(
                        CSERVERAIMASTER_AIACTION_CHANGEFACINGOBJECT,
                        pNode.m_nGroupActionId,
                        CNWSOBJECTACTION_PARAMETER_OBJECT, pOidAttackTarget);


                    if (pGameObject.AsNWSDoor() != null)
                    {
                        pCreature.AddMoveToPointActionToFront(
                            pNode.m_nGroupActionId,
                            vTargetPosition,
                            oidArea,
                            OBJECT_INVALID,
                            bRunToTarget ? 1 : 0,
                            fMoveToTargetRange);
                    }
                    else
                    {
                        pCreature.AddMoveToPointActionToFront(
                            pNode.m_nGroupActionId,
                            vTargetPosition,
                            oidArea,
                            oidAttackTarget,
                            bRunToTarget ? 1 : 0,
                            fMoveToTargetRange);
                    }


                    return ACTION_COMPLETE;
                }
            }



            if (pCreature.m_pcCombatRound == null)
            {
                pCreature.ChangeAttackTarget(pNode, OBJECT_INVALID);
                return ACTION_FAILED;
            }

            pCreature.m_vLastAttackPosition = new Vector();

            if (pCreature.m_pcCombatRound.m_bRoundStarted == 0)
            {
                pCreature.m_pcCombatRound.StartCombatRound(oidAttackTarget);
            }

            if (pCreature.m_pcCombatRound.m_bRoundPaused == 0)
            {
                if (pCreature.m_pcCombatRound.GetActionPending() == 1)
                {
                    CNWSCombatRoundAction pPendingAction = pCreature.m_pcCombatRound.GetAction();

                    if (pPendingAction != null)
                    {
                        var nAnimation = pPendingAction.m_nAnimation;
                        var nActionType = pPendingAction.m_nActionType;
                        var oidTarget = pPendingAction.m_oidTarget;
                        var nTimeAnimation = pPendingAction.m_nAnimationTime;
                        var nAttacks = pPendingAction.m_nNumAttacks;
                        var bOverrideAction = false;


                        if (!bTargetActive &&
                            pCreature.m_pcCombatRound.m_oidNewAttackTarget == OBJECT_INVALID)
                        {
                            nActionType = CNWSCOMBATROUND_TYPE_INVALID;
                            bOverrideAction = true;
                        }

                        switch (nActionType)
                        {
                            case CNWSCOMBATROUND_TYPE_ATTACK:
                            {
                                pCreature.SetAnimation(nAnimation);
                                if (oidAttackTarget != oidTarget)
                                {
                                    if (pPendingAction.m_bActionRetargettable == 1)
                                    {
                                        oidTarget = oidAttackTarget;
                                    }
                                    else
                                    {
                                        pCreature.m_oidAttemptedAttackTarget = oidTarget;
                                    }
                                }

                                pCreature.m_pcCombatRound.SetRoundPaused(1, pCreature.m_idSelf);
                                if (nAttacks > 1)
                                {
                                    pCreature.m_pcCombatRound.SetPauseTimer((int)(nTimeAnimation * ((nAttacks + 1) * 0.5f)));
                                }
                                else
                                {
                                    pCreature.m_pcCombatRound.SetPauseTimer(nTimeAnimation);
                                }

                                if (pCreature.m_pcCombatRound.m_oidNewAttackTarget != OBJECT_INVALID)
                                {
                                    var oidNewTarget = pCreature.m_pcCombatRound.m_oidNewAttackTarget;
                                    pCreature.m_bPassiveAttackBehaviour = 1;
                                    pCreature.ChangeAttackTarget(pNode, oidNewTarget);
                                    pCreature.m_pcCombatRound.m_oidNewAttackTarget = OBJECT_INVALID;
                                    oidTarget = oidNewTarget;
                                    bTargetActive = true;
                                }

                                pCreature.ResolveAttack(oidTarget, nAttacks, nTimeAnimation);
                            }
                            break;


                            case CNWSCOMBATROUND_TYPE_PARRY:
                                {
                                    var nWeaponAttackType = pCreature.m_pcCombatRound.GetWeaponAttackType();
                                    if (nWeaponAttackType == WEAPON_ATTACK_TYPE_OFFHAND)
                                    {
                                        var nAttackValueToUse = pCreature.m_pcCombatRound.m_nOffHandAttacksTaken;
                                        var nBaseAttackBonus = pCreature.m_pStats.GetBaseAttackBonus() - (nAttackValueToUse *  CRULES_MULTIPLE_ATTACKS_BAB_PENALTY_MULTIPLIER);

                                        pCreature.m_pcCombatRound.m_nOffHandAttacksTaken = nAttackValueToUse + 1;
                                    }

                                    pCreature.m_pcCombatRound.SetCurrentAttack((byte)(pCreature.m_pcCombatRound.m_nCurrentAttack + 1));
                                }
                                break;

                            case CNWSCOMBATROUND_TYPE_COMSTEP:
                            case CNWSCOMBATROUND_TYPE_COMSTEPFB:
                                {
                                    if (oidAttackTarget != oidTarget)
                                    {
                                        oidTarget = oidAttackTarget;
                                    }

                                    pCreature.m_pcCombatRound.SetRoundPaused(1, pCreature.m_idSelf);
                                    if (nActionType == CNWSCOMBATROUND_TYPE_COMSTEPFB)
                                    {
                                        pCreature.DoCombatStep(COMBAT_STEP_TYPE_ADJUST, nTimeAnimation, oidTarget);
                                    }
                                    else
                                    {
                                        pCreature.DoCombatStep(COMBAT_STEP_TYPE_APPROPRIATE, nTimeAnimation, oidTarget);
                                    }
                                }
                                break;

                            case CNWSCOMBATROUND_TYPE_REACTION:
                                {
                                    pCreature.m_pcCombatRound.SetRoundPaused(1, pCreature.m_idSelf);
                                    pCreature.m_pcCombatRound.SetPauseTimer(nTimeAnimation);
                                }
                                break;

                            case CNWSCOMBATROUND_TYPE_EQUIP:
                                {
                                    pCreature.m_pcCombatRound.SetRoundPaused(1, pCreature.m_idSelf);
                                    pCreature.m_pcCombatRound.SetPauseTimer(nTimeAnimation);
                                    if (pCreature.RunEquip(oidTarget, pPendingAction.m_nInventorySlot) == 1)
                                    {
                                        pCreature.m_pcCombatRound.RecomputeRound();
                                    }
                                }
                                break;

                            case CNWSCOMBATROUND_TYPE_UNEQUIP:
                                {
                                    pCreature.m_pcCombatRound.SetRoundPaused(1, pCreature.m_idSelf);
                                    pCreature.m_pcCombatRound.SetPauseTimer(nTimeAnimation);
                                    if (pCreature.RunUnequip(oidTarget, pPendingAction.m_oidTargetRepository, pPendingAction.m_nRepositoryX, pPendingAction.m_nRepositoryY, 0) == 1)
                                    {
                                        pCreature.m_pcCombatRound.RecomputeRound();
                                    }
                                }
                                break;

                        }

                        if (nActionType == CNWSCOMBATROUND_TYPE_ATTACK)
                        {
                            if (pCreature.m_nAnimation == NWANIMBASE_ANIM_ATTACK)
                            {
                                var nCurrentAttack = pCreature.m_pcCombatRound.m_nCurrentAttack;
                                if (nCurrentAttack == 0)
                                {

                                }
                            }
                        }

                        pPendingAction.Dispose();
                        pPendingAction = null;
                    }
                }

                if (bTargetActive == false)
                {
                    var newTarget = pCreature.GetNewCombatTarget(oidAttackTarget);
                    oidAttackTarget = OBJECT_INVALID;

                    if (newTarget != null)
                    {
                        oidAttackTarget = newTarget.m_idSelf;
                        pCreature.m_bPassiveAttackBehaviour = 1;
                    }

                    pCreature.ChangeAttackTarget(pNode, oidAttackTarget);

                    return (oidAttackTarget != OBJECT_INVALID
                        ? ACTION_IN_PROGRESS
                        : ACTION_FAILED);
                }
            }

            return ACTION_IN_PROGRESS;
        }

        private float MagnitudeSquared(Vector v)
        {
            return v.x * v.x + v.y * v.y + v.z * v.z;
        }

        private float Sqr(float x)
        {
            return x * x;
        }

    }
}
