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

            uint oidAttackTarget = (uint)pNode.m_pParameter[0];
            var pArea = pCreature.GetArea();

            pCreature.m_nLastCombatRoundUpdate = 6000;

            if (pCreature.GetDead() == 1 || pCreature.GetIsPCDying() == 1 ||
                !IsAIState(AISTATE_CREATURE_ABLE_TO_GO_HOSTILE, pCreature) ||
                !IsAIState(AISTATE_CREATURE_USE_HANDS, pCreature))
            {
                pCreature.ChangeAttackTarget(pNode, OBJECT_INVALID);
                return ACTION_FAILED;
            }

            if (oidAttackTarget == pCreature.m_idSelf)
            {
                pCreature.SetAnimation(NWANIMBASE_ANIM_PAUSE);
                return ACTION_FAILED;
            }

            var pGameObject = (CGameObject)NWNXLib.g_pAppManager.m_pServerExoApp.GetGameObject(oidAttackTarget);
            if (pGameObject == null)
            {
                pCreature.ChangeAttackTarget(pNode, OBJECT_INVALID);
                return ACTION_FAILED;
            }

            bool bTargetActive = false;
            var pTarget = pGameObject.AsNWSObject();
            if (pTarget != null && pTarget.GetDead() == 0)
            {
                bTargetActive = true;
            }

            var pCreatureTarget = pGameObject.AsNWSCreature();
            if (pCreatureTarget != null && pCreatureTarget.GetDead() == 0 && pCreatureTarget.m_bPlayerCharacter == 1 && pCreatureTarget.GetIsPCDying() == 1)
            {
                bTargetActive = true;
            }

            var pVisNode = pCreature.GetVisibleListElement(oidAttackTarget);
            if (pVisNode != null)
            {
                if (pVisNode.m_nSanctuary == SANCTUARY_SAVE_FAILED ||
                    (pVisNode.m_bInvisible == 1 && pVisNode.m_bHeard == 0 && pVisNode.m_bSeen == 0))
                {
                    bTargetActive = false;
                }
            }
            else if (pCreatureTarget != null && pCreature.m_bPlayerCharacter == 1)
            {
                bTargetActive = false;
            }

            if (!bTargetActive)
            {
                pCreature.ChangeAttackTarget(pNode, OBJECT_INVALID);
                return ACTION_FAILED;
            }

            var pTargetArea = pTarget.GetArea();
            var vTargetPosition = pTarget.m_vPosition;
            var fMaxAttackRange = pCreature.MaxAttackRange(oidAttackTarget);

            if (ShouldRepositionForAttack(pArea, pTargetArea, vTargetPosition, fMaxAttackRange, pCreature))
            {
                if (HandleRepositioning(pNode, oidAttackTarget, pTarget, vTargetPosition, pTargetArea, fMaxAttackRange, pCreature))
                {
                    return ACTION_IN_PROGRESS;
                }
                return ACTION_COMPLETE;
            }

            if (pCreature.m_pcCombatRound == null)
            {
                pCreature.ChangeAttackTarget(pNode, OBJECT_INVALID);
                return ACTION_FAILED;
            }

            if (pCreature.m_pcCombatRound.m_bRoundStarted == 0)
            {
                pCreature.m_pcCombatRound.StartCombatRound(oidAttackTarget);
            }

            if (pCreature.m_pcCombatRound.m_bRoundPaused == 0 && pCreature.m_pcCombatRound.GetActionPending() == 1)
            {
                HandleCombatAction(pNode, oidAttackTarget, pCreature);
            }

            if (pCreature.m_vLastAttackPosition == pCreature.m_vPosition)
            {
                pCreature.SendFeedbackMessage(FEEDBACK_ACTION_CANT_REACH_TARGET);
                oidAttackTarget = AssignNewTarget(pNode, oidAttackTarget, pCreature);
                return oidAttackTarget != OBJECT_INVALID ? ACTION_IN_PROGRESS : ACTION_FAILED;
            }

            pCreature.m_vLastAttackPosition = pCreature.m_vPosition;
            pCreature.ChangeAttackTarget(pNode, oidAttackTarget);
            return ACTION_IN_PROGRESS;
        }

        private bool ShouldRepositionForAttack(CNWSArea pArea, CNWSArea pTargetArea, Vector vTargetPosition, float fMaxAttackRange, CNWSCreature pCreature)
        {
            return pArea != pTargetArea ||
                   DistanceSquared(pCreature.m_vPosition, vTargetPosition) > (fMaxAttackRange + CNW_PATHFIND_TOLERANCE) * (fMaxAttackRange + CNW_PATHFIND_TOLERANCE) ||
                   pCreature.CheckAttackClearLineToTarget(pCreature.m_oidAttackTarget, vTargetPosition, pArea) == 0;
        }

        private bool HandleRepositioning(CNWSObjectActionNode pNode, uint oidAttackTarget, CNWSObject pTarget, Vector vTargetPosition, CNWSArea pTargetArea, float fMaxAttackRange, CNWSCreature pCreature)
        {
            float fMoveToTargetRange = pCreature.DesiredAttackRange(oidAttackTarget);
            if (pTarget.AsNWSCreature() != null)
            {
                fMoveToTargetRange += pCreature.m_pcPathfindInformation.m_fCreaturePersonalSpace;
                fMoveToTargetRange += pTarget.AsNWSCreature().m_pcPathfindInformation.m_fCreaturePersonalSpace;
            }
            else
            {
                fMoveToTargetRange += pCreature.m_pcPathfindInformation.m_fPersonalSpace;
            }

            pCreature.m_pcPathfindInformation.m_bUsePlotGridPath = 1;

            pCreature.AddMoveToPointActionToFront(
                pNode.m_nGroupActionId,
                vTargetPosition,
                pTargetArea != null ? pTargetArea.m_idSelf : OBJECT_INVALID,
                oidAttackTarget,
                1,
                fMoveToTargetRange);

            return true;
        }

        private uint AssignNewTarget(CNWSObjectActionNode pNode, uint oidAttackTarget, CNWSCreature pCreature)
        {
            var newTarget = pCreature.GetNewCombatTarget(oidAttackTarget);
            if (newTarget != null)
            {
                oidAttackTarget = newTarget.m_idSelf;
                pCreature.m_bPassiveAttackBehaviour = 1;
            }
            else
            {
                oidAttackTarget = OBJECT_INVALID;
            }

            pCreature.ChangeAttackTarget(pNode, oidAttackTarget);
            return oidAttackTarget;
        }

        private void HandleCombatAction(CNWSObjectActionNode pNode, uint oidAttackTarget, CNWSCreature pCreature)
        {
            var pPendingAction = pCreature.m_pcCombatRound.GetAction();
            if (pPendingAction == null) return;

            var oidTarget = pPendingAction.m_oidTarget;
            if (oidTarget != oidAttackTarget && pPendingAction.m_bActionRetargettable == 1)
            {
                oidTarget = oidAttackTarget;
            }

            pCreature.m_pcCombatRound.SetRoundPaused(1);
            pCreature.m_pcCombatRound.SetPauseTimer(pPendingAction.m_nAnimationTime);
            pCreature.SetAnimation(pPendingAction.m_nAnimation);
            pCreature.ResolveAttack(oidTarget, pPendingAction.m_nNumAttacks, pPendingAction.m_nAnimationTime);
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
