using Anvil.API;
using Anvil.Services;
using NWN.Native.API;

namespace XM.Combat.NativeOverride
{
    [ServiceBinding(typeof(OnGetDamageRoll))]
    internal sealed unsafe class OnGetDamageRoll
    {
        [NativeFunction("_ZN17CNWSCreatureStats13GetDamageRollEP10CNWSObjectiiiii", "")]
        internal delegate int GetDamageRollHook(void* thisPtr, void* pTarget, int bOffHand, int bCritical, int bSneakAttack, int bDeathAttack, int bForceMax);

        // ReSharper disable once NotAccessedField.Local
        private readonly FunctionHook<GetDamageRollHook> _getDamageRollHook;

        private readonly VirtualMachine _vm;
        private readonly CombatService _combat;

        public OnGetDamageRoll(
            HookService hook,
            VirtualMachine vm,
            CombatService combat)
        {
            _vm = vm;
            _combat = combat;

            _getDamageRollHook = hook.RequestHook<GetDamageRollHook>(GetDamageRoll);
        }

        private int GetDamageRoll(
            void* thisPtr, 
            void* pTarget, 
            int bOffHand, 
            int bCritical, 
            int bSneakAttack, 
            int bDeathAttack, 
            int bForceMax)
        {
            return _vm.ExecuteInScriptContext(() =>
            {
                if (thisPtr == null || pTarget == null)
                    return 0;

                var defender = CNWSObject.FromPointer(pTarget);
                if (defender.m_idSelf == OBJECT_INVALID)
                    return 0;

                var attackerStats = CNWSCreatureStats.FromPointer(thisPtr);
                var attacker = CNWSCreature.FromPointer(attackerStats.m_pBaseCreature);
                var round = attacker.m_pcCombatRound;
                var attackData = round.GetAttack(round.m_nCurrentAttack);
                var attackType = attackData.m_bRangedAttack == 1
                    ? AttackType.Ranged
                    : AttackType.Melee;
                var hitResult = bCritical == 1
                    ? HitResultType.Critical
                    : HitResultType.Hit;
                var weapon = round.GetCurrentAttackWeapon();

                var damage = _combat.DetermineDamage(
                    attacker.m_idSelf,
                    defender.m_idSelf,
                    weapon == null ? OBJECT_INVALID : weapon.m_idSelf,
                    attackType,
                    hitResult);

                if (damage > 0)
                {
                    _combat.GainTP(attacker.m_idSelf);
                }

                return damage;
            });
        }
    }
}
