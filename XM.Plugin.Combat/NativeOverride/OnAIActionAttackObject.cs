using System;
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

            if (!bTargetActive)
            {
                pCreature.ChangeAttackTarget(pNode, OBJECT_INVALID);
                return ACTION_FAILED;
            }

            CNWSObject pTarget = pGameObject.AsNWSObject();
            Vector vTargetPosition = pTarget.m_vPosition;
            CNWSArea pTargetArea = pTarget.GetArea();

            float fMaxAttackRange = pCreature.MaxAttackRange(oidAttackTarget);
            float fDesiredAttackRange = pCreature.DesiredAttackRange(oidAttackTarget);
            if (pCreature.m_oidAttemptedAttackTarget == OBJECT_INVALID)
            {
                pCreature.m_oidAttemptedAttackTarget = oidAttackTarget;
            }

            int bClearLineOfAttack = 0;

            if (pArea != null && pArea == pTargetArea)
            {
                bClearLineOfAttack = pCreature.CheckAttackClearLineToTarget(oidAttackTarget, vTargetPosition, pArea);
            }

            bool bOutsideAttackRange = (pTargetArea != pArea ||
                                        DistanceSquared(pCreature.m_vPosition, vTargetPosition) > Math.Pow(fMaxAttackRange + CNW_PATHFIND_TOLERANCE, 2));

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

                if (pCreature.m_vLastAttackPosition != new Vector() && pCreature.m_vLastAttackPosition == pCreature.m_vPosition)
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
                            fMoveToTargetRange = pCreature.m_pcPathfindInformation.m_fCreaturePersonalSpace +
                                                 pTarget.AsNWSCreature().m_pcPathfindInformation.m_fCreaturePersonalSpace;
                        }
                        else
                        {
                            fMoveToTargetRange = pCreature.m_pcPathfindInformation.m_fPersonalSpace;
                        }
                    }

                    pCreature.m_pcPathfindInformation.m_bUsePlotGridPath = 1;
                }

                pCreature.AddMoveToPointActionToFront(pNode.m_nGroupActionId, vTargetPosition, oidArea, oidAttackTarget, 1, fMoveToTargetRange);
                return ACTION_COMPLETE;
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
                        uint oidTarget = pPendingAction.m_oidTarget;
                        if (oidAttackTarget != oidTarget && pPendingAction.m_bActionRetargettable == 1)
                        {
                            oidTarget = oidAttackTarget;
                        }

                        pCreature.m_pcCombatRound.SetRoundPaused(1);
                        pCreature.m_pcCombatRound.SetPauseTimer(pPendingAction.m_nAnimationTime);
                        pCreature.SetAnimation(pPendingAction.m_nAnimation);
                        pCreature.ResolveAttack(oidTarget, pPendingAction.m_nNumAttacks, pPendingAction.m_nAnimationTime);
                    }
                }
            }

            return ACTION_IN_PROGRESS;
        }

        private float DistanceSquared(Vector v1, Vector v2)
        {
            var dx = v1.x - v2.x;
            var dy = v1.y - v2.y;
            var dz = v1.z - v2.z;
            return dx * dx + dy * dy + dz * dz;
        }
    }
}
