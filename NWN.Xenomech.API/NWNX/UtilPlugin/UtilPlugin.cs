using NWN.Xenomech.API.BaseTypes;

namespace NWN.Xenomech.API.NWNX.UtilPlugin
{
    public static class UtilPlugin
    {
        /// <summary>
        /// Gets the name of the currently executing script.
        /// </summary>
        /// <param name="depth">The depth to seek the executing script.</param>
        /// <returns>The name of the currently executing script.</returns>
        public static string GetCurrentScriptName(int depth = 0)
        {
            return Core.NWNX.UtilPlugin.GetCurrentScriptName(depth);
        }

        /// <summary>
        /// Gets a string that contains the ASCII table.
        /// </summary>
        /// <returns>A string that contains all characters at their position (e.g. 'A' at 65).</returns>
        public static string GetAsciiTableString()
        {
            return Core.NWNX.UtilPlugin.GetAsciiTableString();
        }

        /// <summary>
        /// Gets an integer hash of a string.
        /// </summary>
        /// <param name="str">The string to hash.</param>
        /// <returns>The hashed string as an integer.</returns>
        public static int Hash(string str)
        {
            return Core.NWNX.UtilPlugin.Hash(str);
        }

        /// <summary>
        /// Gets the last modified timestamp (mtime) of the module file in seconds.
        /// </summary>
        /// <returns>The mtime of the module file.</returns>
        public static int GetModuleMtime()
        {
            return Core.NWNX.UtilPlugin.GetModuleMtime();
        }

        /// <summary>
        /// Gets the module short file name.
        /// </summary>
        /// <returns>The module file as a string.</returns>
        public static string GetModuleFile()
        {
            return Core.NWNX.UtilPlugin.GetModuleFile();
        }

        /// <summary>
        /// Gets the value of a custom token.
        /// </summary>
        /// <param name="customTokenNumber">The token number to query.</param>
        /// <returns>The string representation of the token value.</returns>
        public static string GetCustomToken(int customTokenNumber)
        {
            return Core.NWNX.UtilPlugin.GetCustomToken(customTokenNumber);
        }

        /// <summary>
        /// Convert an effect type to an item property type.
        /// </summary>
        /// <param name="e">The effect to convert to an item property.</param>
        /// <returns>The converted item property.</returns>
        public static ItemProperty EffectToItemProperty(Effect e)
        {
            return Core.NWNX.UtilPlugin.EffectToItemProperty(e);
        }

        /// <summary>
        /// Convert an item property type to an effect type.
        /// </summary>
        /// <param name="ip">The item property to convert to an effect.</param>
        /// <returns>The converted effect.</returns>
        public static Effect ItemPropertyToEffect(ItemProperty ip)
        {
            return Core.NWNX.UtilPlugin.ItemPropertyToEffect(ip);
        }

        /// <summary>
        /// Strip any color codes from a string.
        /// </summary>
        /// <param name="str">The string to strip of color.</param>
        /// <returns>The new string without any color codes.</returns>
        public static string StripColors(string str)
        {
            return Core.NWNX.UtilPlugin.StripColors(str);
        }

        /// <summary>
        /// Retrieves an environment variable.
        /// </summary>
        /// <param name="sVarname">The environment variable to query.</param>
        /// <returns>The value of the environment variable.</returns>
        public static string GetEnvironmentVariable(string sVarname)
        {
            return Core.NWNX.UtilPlugin.GetEnvironmentVariable(sVarname);
        }

        /// <summary>
        /// Gets the module real-life minutes per in-game hour.
        /// </summary>
        /// <returns>The minutes per hour.</returns>
        public static int GetMinutesPerHour()
        {
            return Core.NWNX.UtilPlugin.GetMinutesPerHour();
        }

        /// <summary>
        /// Set module real-life minutes per in-game hour.
        /// </summary>
        /// <param name="minutes">The minutes per hour.</param>
        public static void SetMinutesPerHour(int minutes)
        {
            Core.NWNX.UtilPlugin.SetMinutesPerHour(minutes);
        }

        /// <summary>
        /// Encodes a string for usage in a URL.
        /// </summary>
        /// <param name="str">The string to encode for a URL.</param>
        /// <returns>The URL-encoded string.</returns>
        public static string EncodeStringForURL(string sURL)
        {
            return Core.NWNX.UtilPlugin.EncodeStringForURL(sURL);
        }

        /// <summary>
        /// Get the first resref of a given type.
        /// </summary>
        /// <param name="nType">A ResrefType value to specify the type of resref.</param>
        /// <param name="sRegexFilter">Allows filtering resrefs using a regex pattern.</param>
        /// <param name="bModuleResourcesOnly">If TRUE, only custom resources will be returned.</param>
        /// <returns>The first resref found or an empty string if none is found.</returns>
        public static string GetFirstResRef(ResrefType nType, string sRegexFilter = "", bool bModuleResourcesOnly = true)
        {
            return Core.NWNX.UtilPlugin.GetFirstResRef((int)nType, sRegexFilter, bModuleResourcesOnly ? 1 : 0);
        }

        /// <summary>
        /// Get the next resref.
        /// </summary>
        /// <returns>The next resref found or an empty string if none is found.</returns>
        public static string GetNextResRef()
        {
            return Core.NWNX.UtilPlugin.GetNextResRef();
        }

        /// <summary>
        /// Get the last created object.
        /// </summary>
        /// <param name="nObjectType">The object type to query (translated from NWScript constants).</param>
        /// <param name="nNthLast">The nth last object created.</param>
        /// <returns>The last created object or OBJECT_INVALID on error.</returns>
        public static uint GetLastCreatedObject(int nObjectType, int nNthLast = 1)
        {
            return Core.NWNX.UtilPlugin.GetLastCreatedObject(nObjectType, nNthLast);
        }
    }
}
