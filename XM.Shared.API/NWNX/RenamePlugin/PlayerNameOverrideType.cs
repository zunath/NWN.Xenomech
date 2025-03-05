namespace XM.Shared.API.NWNX.RenamePlugin
{
    public enum PlayerNameOverrideType
    {
        /// <summary>
        /// Don't rename
        /// </summary>
        Default = 0,

        /// <summary>
        /// Generate random string for Community Name
        /// </summary>
        Obfuscate = 1,

        /// <summary>
        /// Use character name specified
        /// </summary>
        Override = 2,

        /// <summary>
        /// Use the value of the NWNX_RENAME_ANONYMOUS_NAME environment variable
        /// </summary>
        Anonymous = 3,
    }
}
