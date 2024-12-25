
using NWN.Xenomech.Core.Interop;

namespace NWN.Xenomech.Core.NWScript
{
    public partial class NWScript
    {
        /// <summary>
        ///   Returns the amount of gold a store currently has. -1 indicates it is not using gold.
        ///   -2 indicates the store could not be located.
        /// </summary>
        public static int GetStoreGold(uint oidStore)
        {
            NWNXPInvoke.StackPushObject(oidStore);
            NWNXPInvoke.CallBuiltIn(759);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Sets the amount of gold a store has. -1 means the store does not use gold.
        /// </summary>
        public static void SetStoreGold(uint oidStore, int nGold)
        {
            NWNXPInvoke.StackPushInteger(nGold);
            NWNXPInvoke.StackPushObject(oidStore);
            NWNXPInvoke.CallBuiltIn(760);
        }

        /// <summary>
        ///   Gets the maximum amount a store will pay for any item. -1 means price unlimited.
        ///   -2 indicates the store could not be located.
        /// </summary>
        public static int GetStoreMaxBuyPrice(uint oidStore)
        {
            NWNXPInvoke.StackPushObject(oidStore);
            NWNXPInvoke.CallBuiltIn(761);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Sets the maximum amount a store will pay for any item. -1 means price unlimited.
        /// </summary>
        public static void SetStoreMaxBuyPrice(uint oidStore, int nMaxBuy)
        {
            NWNXPInvoke.StackPushInteger(nMaxBuy);
            NWNXPInvoke.StackPushObject(oidStore);
            NWNXPInvoke.CallBuiltIn(762);
        }

        /// <summary>
        ///   Gets the amount a store charges for identifying an item. Default is 100. -1 means
        ///   the store will not identify items.
        ///   -2 indicates the store could not be located.
        /// </summary>
        public static int GetStoreIdentifyCost(uint oidStore)
        {
            NWNXPInvoke.StackPushObject(oidStore);
            NWNXPInvoke.CallBuiltIn(763);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Sets the amount a store charges for identifying an item. Default is 100. -1 means
        ///   the store will not identify items.
        /// </summary>
        public static void SetStoreIdentifyCost(uint oidStore, int nCost)
        {
            NWNXPInvoke.StackPushInteger(nCost);
            NWNXPInvoke.StackPushObject(oidStore);
            NWNXPInvoke.CallBuiltIn(764);
        }

        /// <summary>
        ///   Open oStore for oPC.
        ///   - nBonusMarkUp is added to the stores default mark up percentage on items sold (-100 to 100)
        ///   - nBonusMarkDown is added to the stores default mark down percentage on items bought (-100 to 100)
        /// </summary>
        public static void OpenStore(uint oStore, uint oPC, int nBonusMarkUp = 0, int nBonusMarkDown = 0)
        {
            NWNXPInvoke.StackPushInteger(nBonusMarkDown);
            NWNXPInvoke.StackPushInteger(nBonusMarkUp);
            NWNXPInvoke.StackPushObject(oPC);
            NWNXPInvoke.StackPushObject(oStore);
            NWNXPInvoke.CallBuiltIn(378);
        }
    }
}
