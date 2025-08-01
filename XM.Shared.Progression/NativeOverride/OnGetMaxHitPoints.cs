using Anvil.API;
using Anvil.Services;
using NWN.Native.API;

namespace XM.Progression.NativeOverride
{
    [ServiceBinding(typeof(OnGetMaxHitPoints))]
    internal sealed unsafe class OnGetMaxHitPoints
    {
        [NativeFunction("_ZN12CNWSCreature15GetMaxHitPointsEi", "")]
        private delegate short GetMaxHitPointsHook(void* thisPtr, int bIncludeToughness);

        private readonly FunctionHook<GetMaxHitPointsHook> _getMaxHitPointsHook;

        public OnGetMaxHitPoints(HookService hook)
        {
            _getMaxHitPointsHook = hook.RequestHook<GetMaxHitPointsHook>(GetMaxHitPoints);
        }

        private short GetMaxHitPoints(void* thisPtr, int bIncludeToughness)
        {
            var creature = CNWSCreature.FromPointer(thisPtr);
            if (creature == null)
                return 0;

            var con = creature.m_pStats.m_nConstitutionModifier;
            creature.m_pStats.m_nConstitutionModifier = 0;
            var hp = _getMaxHitPointsHook.CallOriginal(thisPtr, bIncludeToughness);
            creature.m_pStats.m_nConstitutionModifier = con;

            return hp;
        }
    }
}
