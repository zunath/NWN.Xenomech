using Anvil.API;
using Anvil.Services;
using NWN.Native.API;
using XM.Shared.API.Constants;
using ObjectType = NWN.Native.API.ObjectType;

namespace XM.Combat.NativeOverride
{
    [ServiceBinding(typeof(OnResolveAttackRoll))]
    internal sealed unsafe class OnResolveAttackRoll
    {
        [NativeFunction("_ZN12CNWSCreature17ResolveAttackRollEP10CNWSObject", "")]
        internal delegate void ResolveAttackRollHook(void* thisPtr, void* pTarget);

        // ReSharper disable once NotAccessedField.Local
        private readonly FunctionHook<ResolveAttackRollHook> _resolveAttackRollHook;
        private readonly VirtualMachine _vm;
        private readonly CombatService _combat;

        public OnResolveAttackRoll(
            HookService hook,
            VirtualMachine vm,
            CombatService combat)
        {
            _resolveAttackRollHook = hook.RequestHook<ResolveAttackRollHook>(ResolveAttackRoll);
            _vm = vm;
            _combat = combat;
        }

        private void ResolveAttackRoll(void* thisPtr, void* pTarget)
        {
            _vm.ExecuteInScriptContext(() =>
            {
                var target = CNWSObject.FromPointer(pTarget);
                if (target == null)
                    return;

                var attacker = CNWSCreature.FromPointer(thisPtr);
                var round = attacker.m_pcCombatRound;

                var attackData = round.GetAttack(attacker.m_pcCombatRound.m_nCurrentAttack);

                if (IsNonCreatureTarget(target, attackData))
                    return;

                var defender = CNWSCreature.FromPointer(pTarget);

                var weapon = round.GetCurrentAttackWeapon();
                var attackType = DetermineAttackType(attacker, weapon, attackData);
                var combatMode = (CombatModeType)attacker.m_nCombatMode;
                var (hitType, hitRate) = _combat.DetermineHitType(attacker.m_idSelf, defender.m_idSelf, attackType, combatMode);

                AssignAttackValues(attackData, hitType);

                var message = _combat.BuildCombatLogMessage(
                    attacker.m_idSelf, 
                    defender.m_idSelf, 
                    hitType,
                    hitRate)
                    .ToExoString();
                attacker.SendFeedbackString(message);
                defender.SendFeedbackString(message);
            });
        }

        private bool IsNonCreatureTarget(CNWSObject target, CNWSCombatAttackData attackData)
        {
            if (target.m_nObjectType != (byte)ObjectType.Creature)
            {
                attackData.m_nAttackResult = 7; // Auto-hit
                return true;
            }
            return false;
        }

        private AttackType DetermineAttackType(CNWSCreature attacker, CNWSItem weapon, CNWSCombatAttackData attackData)
        {
            if (weapon != null && attackData.m_bRangedAttack == 1 && attacker.GetRangeWeaponEquipped() == 1)
            {
                return AttackType.Ranged;
            }
            return AttackType.Melee;
        }

        private void AssignAttackValues(CNWSCombatAttackData attackData, HitResultType hitType)
        {
            if (hitType == HitResultType.Hit)
            {
                attackData.m_nAttackResult = 1;
            }
            else if (hitType == HitResultType.Deflect)
            {
                attackData.m_nAttackResult = 2;
            }
            else if (hitType == HitResultType.Critical)
            {
                attackData.m_bCriticalThreat = 1;
                attackData.m_nThreatRoll = 1;
                attackData.m_nAttackResult = 3;
            }
            else
            {
                attackData.m_nAttackResult = 4; // Miss
                attackData.m_nMissedBy = 1; // Fill this in case it’s used elsewhere
            }
        }
    }
}
