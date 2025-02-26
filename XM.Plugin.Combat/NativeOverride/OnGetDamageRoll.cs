using Anvil.API;
using Anvil.Services;
using NWN.Native.API;
using XM.Progression.Ability;
using XM.Shared.Core.EventManagement;

namespace XM.Plugin.Combat.NativeOverride
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
        private readonly AbilityService _ability;
        private readonly XMEventService _event;

        public OnGetDamageRoll(
            HookService hook,
            VirtualMachine vm,
            CombatService combat,
            AbilityService ability,
            XMEventService @event)
        {
            _vm = vm;
            _combat = combat;
            _ability = ability;
            _event = @event;

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
                    OnDamaged(attacker.m_idSelf, defender.m_idSelf, damage);
                }

                _ability.ProcessQueuedAbility(attacker.m_idSelf, defender.m_idSelf);

                return damage;
            });
        }

        private void ApplyTP(uint attacker, uint defender)
        {
            var attackerTPAmount = GetIsPC(attacker)
                ? _combat.CalculateTPGainPlayer(attacker, false)
                : _combat.CalculateTPGainNPC(attacker, false);
            var defenderTPAmount = GetIsPC(attacker)
                ? _combat.CalculateTPGainPlayer(attacker, true)
                : _combat.CalculateTPGainNPC(attacker, true);

            _combat.UpdateTP(attacker, attackerTPAmount);
            _combat.UpdateTP(defender, defenderTPAmount);
        }

        private void OnDamaged(uint attacker, uint defender, int damage)
        {
            ApplyTP(attacker, defender);
            
            _event.PublishEvent(attacker, new XMEvent.OnDamageDealt(defender, damage));
        }
    }
}
