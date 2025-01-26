using Anvil.API;
using NWN.Core;
using NWN.Core.NWNX;

namespace XM.Shared.API.NWNX.UtilPlugin
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
            return NWN.Core.NWNX.UtilPlugin.GetCurrentScriptName(depth);
        }

        /// <summary>
        /// Gets a string that contains the ASCII table.
        /// </summary>
        /// <returns>A string that contains all characters at their position (e.g. 'A' at 65).</returns>
        public static string GetAsciiTableString()
        {
            return NWN.Core.NWNX.UtilPlugin.GetAsciiTableString();
        }

        /// <summary>
        /// Gets an integer hash of a string.
        /// </summary>
        /// <param name="str">The string to hash.</param>
        /// <returns>The hashed string as an integer.</returns>
        public static int Hash(string str)
        {
            return NWN.Core.NWNX.UtilPlugin.Hash(str);
        }

        /// <summary>
        /// Gets the last modified timestamp (mtime) of the module file in seconds.
        /// </summary>
        /// <returns>The mtime of the module file.</returns>
        public static int GetModuleMTime()
        {
            // NWN.Core methods are broken, overwritten here to temporarily fix until PR is merged.
            VM.NWNX.SetFunction("NWNX_Util", "GetModuleMtime");
            VM.NWNX.Call();
            return VM.NWNX.StackPopInt();
        }

        /// <summary>
        /// Gets the module short file name.
        /// </summary>
        /// <returns>The module file as a string.</returns>
        public static string GetModuleFile()
        {
            // NWN.Core methods are broken, overwritten here to temporarily fix until PR is merged.
            VM.NWNX.SetFunction("NWNX_Util", "GetModuleFile");
            VM.NWNX.Call();
            return VM.NWNX.StackPopString();
        }

        /// <summary>
        /// Gets the value of a custom token.
        /// </summary>
        /// <param name="customTokenNumber">The token number to query.</param>
        /// <returns>The string representation of the token value.</returns>
        public static string GetCustomToken(int customTokenNumber)
        {
            return NWN.Core.NWNX.UtilPlugin.GetCustomToken(customTokenNumber);
        }

        /// <summary>
        /// Convert an effect type to an item property type.
        /// </summary>
        /// <param name="e">The effect to convert to an item property.</param>
        /// <returns>The converted item property.</returns>
        public static ItemProperty EffectToItemProperty(Effect e)
        {
            return NWN.Core.NWNX.UtilPlugin.EffectToItemProperty(e);
        }

        /// <summary>
        /// Convert an item property type to an effect type.
        /// </summary>
        /// <param name="ip">The item property to convert to an effect.</param>
        /// <returns>The converted effect.</returns>
        public static Effect ItemPropertyToEffect(ItemProperty ip)
        {
            return NWN.Core.NWNX.UtilPlugin.ItemPropertyToEffect(ip);
        }

        /// <summary>
        /// Strip any color codes from a string.
        /// </summary>
        /// <param name="str">The string to strip of color.</param>
        /// <returns>The new string without any color codes.</returns>
        public static string StripColors(string str)
        {
            return NWN.Core.NWNX.UtilPlugin.StripColors(str);
        }

        /// <summary>
        /// Retrieves an environment variable.
        /// </summary>
        /// <param name="sVarname">The environment variable to query.</param>
        /// <returns>The value of the environment variable.</returns>
        public static string GetEnvironmentVariable(string sVarname)
        {
            return NWN.Core.NWNX.UtilPlugin.GetEnvironmentVariable(sVarname);
        }

        /// <summary>
        /// Gets the module real-life minutes per in-game hour.
        /// </summary>
        /// <returns>The minutes per hour.</returns>
        public static int GetMinutesPerHour()
        {
            return NWN.Core.NWNX.UtilPlugin.GetMinutesPerHour();
        }

        /// <summary>
        /// Set module real-life minutes per in-game hour.
        /// </summary>
        /// <param name="minutes">The minutes per hour.</param>
        public static void SetMinutesPerHour(int minutes)
        {
            NWN.Core.NWNX.UtilPlugin.SetMinutesPerHour(minutes);
        }

        /// <summary>
        /// Encodes a string for usage in a URL.
        /// </summary>
        /// <param name="str">The string to encode for a URL.</param>
        /// <returns>The URL-encoded string.</returns>
        public static string EncodeStringForURL(string sURL)
        {
            return NWN.Core.NWNX.UtilPlugin.EncodeStringForURL(sURL);
        }

        /// <summary>
        /// Get the first resref of a given type.
        /// </summary>
        /// <param name="nType">A ResrefType value to specify the type of resref.</param>
        /// <param name="sRegexFilter">Allows filtering resrefs using a regex pattern.</param>
        /// <param name="bModuleResourcesOnly">If TRUE, only custom resources will be returned.</param>
        /// <returns>The first resref found or an empty string if none is found.</returns>
        public static string GetFirstResRef(ResRefType nType, string sRegexFilter = "", bool bModuleResourcesOnly = true)
        {
            return NWN.Core.NWNX.UtilPlugin.GetFirstResRef((int)nType, sRegexFilter, bModuleResourcesOnly ? 1 : 0);
        }

        /// <summary>
        /// Get the next resref.
        /// </summary>
        /// <returns>The next resref found or an empty string if none is found.</returns>
        public static string GetNextResRef()
        {
            return NWN.Core.NWNX.UtilPlugin.GetNextResRef();
        }

        /// <summary>
        /// Get the last created object.
        /// </summary>
        /// <param name="nObjectType">The object type to query (translated from NWScript constants).</param>
        /// <param name="nNthLast">The nth last object created.</param>
        /// <returns>The last created object or OBJECT_INVALID on error.</returns>
        public static uint GetLastCreatedObject(int nObjectType, int nNthLast = 1)
        {
            return NWN.Core.NWNX.UtilPlugin.GetLastCreatedObject(nObjectType, nNthLast);
        }
        /// <summary>
        /// Compiles and adds a script to the UserDirectory/nwnx folder, or to the location of sAlias.
        /// Will override existing scripts that are in the module.
        /// </summary>
        /// <param name="sFileName">The script filename without extension, 16 or less characters.</param>
        /// <param name="sScriptData">The script data to compile</param>
        /// <param name="bWrapIntoMain">Set to TRUE to wrap sScriptData into void main(){}.</param>
        /// <param name="sAlias">The alias of the resource directory to add the ncs file to. Default: UserDirectory/nwnx</param>
        /// <returns>"</returns>
        public static string AddScript(string sFileName, string sScriptData, int bWrapIntoMain = 0, string sAlias = "NWNX")
        {
            return NWN.Core.NWNX.UtilPlugin.AddScript(sFileName, sScriptData, bWrapIntoMain, sAlias);
        }

        /// <summary>
        /// Adds a nss file to the UserDirectory/nwnx folder, or to the location of sAlias.
        /// Will override existing nss files that are in the module.
        /// </summary>
        /// <param name="sFileName">The script filename without extension, 16 or less characters.</param>
        /// <param name="sContents">The contents of the nss file</param>
        /// <param name="sAlias">The alias of the resource directory to add the nss file to. Default: UserDirectory/nwnx</param>
        /// <returns>TRUE on success.</returns>
        public static int AddNSSFile(string sFileName, string sContents, string sAlias = "NWNX")
        {
            return NWN.Core.NWNX.UtilPlugin.AddNSSFile(sFileName, sContents, sAlias);
        }

        /// <summary>
        /// Remove sFileName of nType from the UserDirectory/nwnx folder, or from the location of sAlias.
        /// </summary>
        /// <param name="sFileName">The filename without extension, 16 or less characters.</param>
        /// <param name="nType">The @ref resref_types "Resref Type".</param>
        /// <param name="sAlias">The alias of the resource directory to remove the file from. Default: UserDirectory/nwnx</param>
        /// <returns>TRUE on success.</returns>
        public static int RemoveNWNXResourceFile(string sFileName, int nType, string sAlias = "NWNX")
        {
            return NWN.Core.NWNX.UtilPlugin.RemoveNWNXResourceFile(sFileName, nType, sAlias);
        }

        /// <summary>
        /// Set the NWScript instruction limit.
        /// </summary>
        /// <param name="nInstructionLimit">The new limit or -1 to reset to default.</param>
        public static void SetInstructionLimit(int nInstructionLimit)
        {
            NWN.Core.NWNX.UtilPlugin.SetInstructionLimit(nInstructionLimit);
        }

        /// <summary>
        /// Get the NWScript instruction limit.
        /// </summary>
        /// <returns>Instruction limit.</returns>
        public static int GetInstructionLimit()
        {
            return NWN.Core.NWNX.UtilPlugin.GetInstructionLimit();
        }

        /// <summary>
        /// Set the number of NWScript instructions currently executed.
        /// </summary>
        /// <param name="nInstructions">The number of instructions, must be >= 0.</param>
        public static void SetInstructionsExecuted(int nInstructions)
        {
            NWN.Core.NWNX.UtilPlugin.SetInstructionsExecuted(nInstructions);
        }

        /// <summary>
        /// Get the number of NWScript instructions currently executed.
        /// </summary>
        /// <returns>Number of instructions executed.</returns>
        public static int GetInstructionsExecuted()
        {
            return NWN.Core.NWNX.UtilPlugin.GetInstructionsExecuted();
        }

        /// <summary>
        /// Register a server console command that will execute a script chunk.
        /// </summary>
        /// <param name="sCommand">The name of the command.</param>
        /// <param name="sScriptChunk">The script chunk to run. You can use $args to get the console command arguments.</param>
        /// <returns>TRUE on success.</returns>
        public static int RegisterServerConsoleCommand(string sCommand, string sScriptChunk)
        {
            return NWN.Core.NWNX.UtilPlugin.RegisterServerConsoleCommand(sCommand, sScriptChunk);
        }

        /// <summary>
        /// Unregister a server console command that was registered with NWNX_Util_RegisterServerConsoleCommand().
        /// </summary>
        /// <param name="sCommand">The name of the command.</param>
        public static void UnregisterServerConsoleCommand(string sCommand)
        {
            NWN.Core.NWNX.UtilPlugin.UnregisterServerConsoleCommand(sCommand);
        }

        /// <summary>
        /// Gets the server's current working user folder.
        /// </summary>
        /// <returns>The absolute path of the server's home directory (-userDirectory)</returns>
        public static string GetUserDirectory()
        {
            return NWN.Core.NWNX.UtilPlugin.GetUserDirectory();
        }

        /// <summary>
        /// Get the return value of the last run script with a StartingConditional.
        /// </summary>
        /// <returns>Return value of the last run script.</returns>
        public static int GetScriptReturnValue()
        {
            return NWN.Core.NWNX.UtilPlugin.GetScriptReturnValue();
        }

        /// <summary>
        /// Create a door.
        /// </summary>
        /// <param name="sResRef">The ResRef of the door.</param>
        /// <param name="locLocation">The location to create the door at.</param>
        /// <param name="sNewTag">An optional new tag for the door.</param>
        /// <param name="nAppearanceType">An optional index into doortypes.2da for appearance.</param>
        /// <returns>The door, or OBJECT_INVALID on failure.</returns>
        public static uint CreateDoor(string sResRef, nint locLocation, string sNewTag = "", int nAppearanceType = -1)
        {
            return NWN.Core.NWNX.UtilPlugin.CreateDoor(sResRef, locLocation, sNewTag, nAppearanceType);
        }

        /// <summary>
        /// Set the object that will be returned by GetItemActivator.
        /// </summary>
        /// <param name="oObject">An object.</param>
        public static void SetItemActivator(uint oObject)
        {
            NWN.Core.NWNX.UtilPlugin.SetItemActivator(oObject);
        }
        /// <summary>
        /// Gets the world time as calendar day and time of day.
        /// This function is useful for calculating effect expiry times.
        /// </summary>
        /// <param name="fAdjustment">An adjustment in seconds, 0.0f will return the current world time. Positive or negative values will return a world time in the future or past.</param>
        /// <returns>A NWNX_Util_WorldTime struct with the calendar day and time of day.</returns>
        public static WorldTime GetWorldTime(float fAdjustment = 0.0f)
        {
            return NWN.Core.NWNX.UtilPlugin.GetWorldTime(fAdjustment);
        }

        /// <summary>
        /// Set a server-side resource override.
        /// </summary>
        /// <param name="nResType">A Resref Type.</param>
        /// <param name="sOldName">The old resource name, 16 characters or less.</param>
        /// <param name="sNewName">The new resource name or "" to clear a previous override, 16 characters or less.</param>
        public static void SetResourceOverride(int nResType, string sOldName, string sNewName)
        {
            NWN.Core.NWNX.UtilPlugin.SetResourceOverride(nResType, sOldName, sNewName);
        }

        /// <summary>
        /// Gets a server-side resource override.
        /// </summary>
        /// <param name="nResType">A Resref Type.</param>
        /// <param name="sName">The name of the resource, 16 characters or less.</param>
        /// <returns>The resource override, or "" if one is not set.</returns>
        public static string GetResourceOverride(int nResType, string sName)
        {
            return NWN.Core.NWNX.UtilPlugin.GetResourceOverride(nResType, sName);
        }

        /// <summary>
        /// Gets if a script param is set.
        /// </summary>
        /// <param name="sParamName">The script parameter name to check.</param>
        /// <returns>TRUE if the script param is set, FALSE if not or on error.</returns>
        public static bool GetScriptParamIsSet(string sParamName)
        {
            return NWN.Core.NWNX.UtilPlugin.GetScriptParamIsSet(sParamName) == 1;
        }

        /// <summary>
        /// Set the module dawn hour.
        /// </summary>
        /// <param name="nDawnHour">The new dawn hour.</param>
        public static void SetDawnHour(int nDawnHour)
        {
            NWN.Core.NWNX.UtilPlugin.SetDawnHour(nDawnHour);
        }

        /// <summary>
        /// Set the module dusk hour.
        /// </summary>
        /// <param name="nDuskHour">The new dusk hour.</param>
        public static void SetDuskHour(int nDuskHour)
        {
            NWN.Core.NWNX.UtilPlugin.SetDuskHour(nDuskHour);
        }

        /// <summary>
        /// Returns the number of microseconds since midnight on January 1, 1970.
        /// </summary>
        /// <returns>A HighResTimestamp struct containing seconds and microseconds.</returns>
        public static HighResTimestamp GetHighResTimeStamp()
        {
            return NWN.Core.NWNX.UtilPlugin.GetHighResTimeStamp();
        }

        /// <summary>
        /// Return the name of a terminal, "" if not a TTY.
        /// </summary>
        /// <returns>Terminal name.</returns>
        public static string GetTTY()
        {
            return NWN.Core.NWNX.UtilPlugin.GetTTY();
        }

        /// <summary>
        /// Set the currently running script event.
        /// </summary>
        /// <param name="nEventID">The ID of the event.</param>
        public static void SetCurrentlyRunningEvent(int nEventID)
        {
            NWN.Core.NWNX.UtilPlugin.SetCurrentlyRunningEvent(nEventID);
        }

        /// <summary>
        /// Calculate the Levenshtein distance of two strings.
        /// </summary>
        /// <param name="sString">The string to compare with.</param>
        /// <param name="sCompareTo">The string to compare sString to.</param>
        /// <returns>The number of characters different between the compared strings.</returns>
        public static int GetStringLevenshteinDistance(string sString, string sCompareTo)
        {
            return NWN.Core.NWNX.UtilPlugin.GetStringLevenshteinDistance(sString, sCompareTo);
        }

        /// <summary>
        /// Sends a full object update of oObjectToUpdate to all clients.
        /// </summary>
        /// <param name="oObjectToUpdate">The object to update.</param>
        /// <param name="oPlayer">The player for which the object needs to update, OBJECT_INVALID for all players.</param>
        public static void UpdateClientObject(uint oObjectToUpdate, uint oPlayer = OBJECT_INVALID)
        {
            NWN.Core.NWNX.UtilPlugin.UpdateClientObject(oObjectToUpdate, oPlayer);
        }

        /// <summary>
        /// Clean a resource directory, deleting all files of nResType.
        /// </summary>
        /// <param name="sAlias">A resource directory alias, NWNX or one defined in the custom resource directory file.</param>
        /// <param name="nResType">The type of file to delete or 0xFFFF for all types.</param>
        /// <returns>TRUE if successful, FALSE on error.</returns>
        public static int CleanResourceDirectory(string sAlias, int nResType = 65535)
        {
            return NWN.Core.NWNX.UtilPlugin.CleanResourceDirectory(sAlias, nResType);
        }

        /// <summary>
        /// Return the filename of the tlk file.
        /// </summary>
        /// <returns>The name of the tlk file.</returns>
        public static string GetModuleTlkFile()
        {
            return NWN.Core.NWNX.UtilPlugin.GetModuleTlkFile();
        }

    }
}
