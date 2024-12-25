
using NWN.Xenomech.Core.Interop;
using NWN.Xenomech.Core.NWScript.Enum;

namespace NWN.Xenomech.Core.NWScript
{
    public partial class NWScript
    {
        /// <summary>
        /// Returns the resource location of sResRef.nResType, as seen by the running module.
        /// Note for dedicated servers: Checks on the module/server side, not the client.
        /// Returns an empty string if the resource does not exist in the search space.
        /// </summary>
        public static string ResManGetAliasFor(string sResRef, ResType nResType)
        {
            NWNXPInvoke.StackPushInteger((int)nResType);
            NWNXPInvoke.StackPushString(sResRef);
            NWNXPInvoke.CallBuiltIn(1008);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        /// Finds the nNth available resref starting with sPrefix.
        /// - bSearchBaseData: If TRUE, also searches base game content stored in the game installation directory.
        ///   WARNING: This can be very slow.
        /// - sOnlyKeyTable: Specify a keytable to search (e.g., "OVERRIDE:").
        /// Returns an empty string if no such resref exists.
        /// </summary>
        public static string ResManFindPrefix(string sPrefix, ResType nResType, int nNth = 1, bool bSearchBaseData = false, string sOnlyKeyTable = "")
        {
            NWNXPInvoke.StackPushString(sOnlyKeyTable);
            NWNXPInvoke.StackPushInteger(bSearchBaseData ? 1 : 0);
            NWNXPInvoke.StackPushInteger(nNth);
            NWNXPInvoke.StackPushInteger((int)nResType);
            NWNXPInvoke.StackPushString(sPrefix);
            NWNXPInvoke.CallBuiltIn(1009);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        /// Get the contents of a file as a string, as seen by the server's resman.
        /// Note: If the output contains binary data, it will only return data up to the first null byte.
        /// - nResType: a RESTYPE_* constant.
        /// - nFormat: one of RESMAN_FILE_CONTENTS_FORMAT_* constants.
        /// Returns an empty string if the file does not exist.
        /// </summary>
        public static string ResManGetFileContents(string sResRef, int nResType, ResmanFileContentsFormatType nFormat = ResmanFileContentsFormatType.Raw)
        {
            NWNXPInvoke.StackPushInteger((int)nFormat);
            NWNXPInvoke.StackPushInteger(nResType);
            NWNXPInvoke.StackPushString(sResRef);
            NWNXPInvoke.CallBuiltIn(1071);
            return NWNXPInvoke.StackPopString();
        }
    }
}
