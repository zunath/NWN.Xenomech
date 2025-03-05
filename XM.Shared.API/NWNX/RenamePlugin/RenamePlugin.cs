namespace XM.Shared.API.NWNX.RenamePlugin
{
    public static class RenamePlugin
    {
        public static void SetPCNameOverride(
            uint target,
            string newName,
            string prefix = "",
            string suffix = "",
            PlayerNameOverrideType type = PlayerNameOverrideType.Default,
            uint observer = OBJECT_INVALID)
        {
            NWN.Core.NWNX.RenamePlugin.SetPCNameOverride(target, newName, prefix, suffix, (int)type, observer);
        }

        public static string GetPCNameOverride(uint target, uint observer = OBJECT_INVALID)
        {
            return NWN.Core.NWNX.RenamePlugin.GetPCNameOverride(target, observer);
        }

        public static void ClearPCNameOverride(uint target, uint observer = OBJECT_INVALID, bool clearAll = false)
        {
            NWN.Core.NWNX.RenamePlugin.ClearPCNameOverride(target, observer, clearAll ? 1 : 0);
        }
    }
}
