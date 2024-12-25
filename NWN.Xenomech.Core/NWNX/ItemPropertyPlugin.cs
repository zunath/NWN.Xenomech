using NWN.Xenomech.Core.Interop;
using NWN.Xenomech.Core.NWNX.Enum;

namespace NWN.Xenomech.Core.NWNX
{
    public static class ItemPropertyPlugin
    {
        private const string PLUGIN_NAME = "NWNX_ItemProperty";
        // Convert native itemproperty type to unpacked structure
        public static ItemPropertyUnpacked UnpackIP(ItemProperty ip)
        {
            const string func = "UnpackIP";

            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, func);
            NWNXPInvoke.NWNXPushItemProperty(ip.Handle);
            NWNXPInvoke.NWNXCallFunction();

            return new ItemPropertyUnpacked
            {
                Id = NWNXPInvoke.NWNXPopString(),
                Property = NWNXPInvoke.NWNXPopInt(),
                SubType = NWNXPInvoke.NWNXPopInt(),
                CostTable = NWNXPInvoke.NWNXPopInt(),
                CostTableValue = NWNXPInvoke.NWNXPopInt(),
                Param1 = NWNXPInvoke.NWNXPopInt(),
                Param1Value = NWNXPInvoke.NWNXPopInt(),
                UsesPerDay = NWNXPInvoke.NWNXPopInt(),
                ChanceToAppear = NWNXPInvoke.NWNXPopInt(),
                IsUseable = Convert.ToBoolean(NWNXPInvoke.NWNXPopInt()),
                SpellId = NWNXPInvoke.NWNXPopInt(),
                Creator = NWNXPInvoke.NWNXPopObject(),
                Tag = NWNXPInvoke.NWNXPopString()
            };
        }

        // Convert unpacked itemproperty structure to native type.
        public static ItemProperty PackIP(ItemPropertyUnpacked itemProperty)
        {
            const string sFunc = "PackIP";

            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, sFunc);

            NWNXPInvoke.NWNXPushString(itemProperty.Tag);
            NWNXPInvoke.NWNXPushObject(itemProperty.Creator);
            NWNXPInvoke.NWNXPushInt(itemProperty.SpellId);
            NWNXPInvoke.NWNXPushInt(itemProperty.IsUseable ? 1 : 0);
            NWNXPInvoke.NWNXPushInt(itemProperty.ChanceToAppear);
            NWNXPInvoke.NWNXPushInt(itemProperty.UsesPerDay);
            NWNXPInvoke.NWNXPushInt(itemProperty.Param1Value);
            NWNXPInvoke.NWNXPushInt(itemProperty.Param1);
            NWNXPInvoke.NWNXPushInt(itemProperty.CostTableValue);
            NWNXPInvoke.NWNXPushInt(itemProperty.CostTable);
            NWNXPInvoke.NWNXPushInt(itemProperty.SubType);
            NWNXPInvoke.NWNXPushInt(itemProperty.Property);

            NWNXPInvoke.NWNXCallFunction();
            return new ItemProperty(NWNXPInvoke.NWNXPopItemProperty());
        }

        /// @brief Gets the active item property at the index
        /// @param oItem - the item with the property
        /// @param nIndex - the index such as returned by some Item Events
        /// @return A constructed NWNX_IPUnpacked, except for creator, and spell id.
        public static ItemPropertyUnpacked GetActiveProperty(uint oItem, int nIndex)
        {
            const string sFunc = "GetActiveProperty";

            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, sFunc);
            NWNXPInvoke.NWNXPushInt(nIndex);
            NWNXPInvoke.NWNXPushObject(oItem);
            NWNXPInvoke.NWNXCallFunction();

            return new ItemPropertyUnpacked
            {
                Property = NWNXPInvoke.NWNXPopInt(),
                SubType = NWNXPInvoke.NWNXPopInt(),
                CostTable = NWNXPInvoke.NWNXPopInt(),
                CostTableValue = NWNXPInvoke.NWNXPopInt(),
                Param1 = NWNXPInvoke.NWNXPopInt(),
                Param1Value = NWNXPInvoke.NWNXPopInt(),
                UsesPerDay = NWNXPInvoke.NWNXPopInt(),
                ChanceToAppear = NWNXPInvoke.NWNXPopInt(),
                IsUseable = Convert.ToBoolean(NWNXPInvoke.NWNXPopInt()),
                Tag = NWNXPInvoke.NWNXPopString()
            };
        }

    }
}