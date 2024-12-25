using NWN.Xenomech.Core.Interop;
using NWN.Xenomech.Core.NWNX.Enum;

namespace NWN.Xenomech.Core.NWNX
{
    public static class AdministrationPlugin
    {
        private const string PLUGIN_NAME = "NWNX_Administration";
        // Gets the current player password.
        public static string GetPlayerPassword()
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetPlayerPassword");
            NWNXPInvoke.NWNXCallFunction();

            return NWNXPInvoke.NWNXPopString();
        }

        // Sets the current player password.
        public static void SetPlayerPassword(string password)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetPlayerPassword");
            NWNXPInvoke.NWNXPushString(password);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Removes the current player password.
        public static void ClearPlayerPassword()
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "ClearPlayerPassword");
            NWNXPInvoke.NWNXCallFunction();
        }

        // Gets the current DM password.
        public static string GetDMPassword()
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetDMPassword");
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopString();
        }

        // Sets the current DM password. May be set to an empty string.
        public static void SetDMPassword(string password)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetDMPassword");
            NWNXPInvoke.NWNXPushString(password);
            NWNXPInvoke.NWNXCallFunction();
        }

        /// Signals the server to immediately shut down.
        public static void ShutdownServer()
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "ShutdownServer");
            NWNXPInvoke.NWNXCallFunction();
        }

        // Deletes the player character from the servervault
        // The PC will be immediately booted from the game with a "Delete Character" message
        public static void DeletePlayerCharacter(uint pc, bool bPreserveBackup = true, string kickMessage = "")
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "DeletePlayerCharacter");
            NWNXPInvoke.NWNXPushString(kickMessage);
            NWNXPInvoke.NWNXPushInt(bPreserveBackup ? 1 : 0);
            NWNXPInvoke.NWNXPushObject(pc);
            NWNXPInvoke.NWNXCallFunction();
        }


        // Ban a given IP - get via GetPCIPAddress()
        public static void AddBannedIP(string ip)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "AddBannedIP");
            NWNXPInvoke.NWNXPushString(ip);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Removes a banned IP address.
        public static void RemoveBannedIP(string ip)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "RemoveBannedIP");
            NWNXPInvoke.NWNXPushString(ip);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Adds a banned CD key. Get via GetPCPublicCDKey
        public static void AddBannedCDKey(string key)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "AddBannedCDKey");
            NWNXPInvoke.NWNXPushString(key);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Removes a banned CD key.
        public static void RemoveBannedCDKey(string key)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "RemoveBannedCDKey");
            NWNXPInvoke.NWNXPushString(key);
            NWNXPInvoke.NWNXCallFunction();
        }


        // Adds a banned player name - get via GetPCPlayerName.
        public static void AddBannedPlayerName(string playerName)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "AddBannedPlayerName");
            NWNXPInvoke.NWNXPushString(playerName);
            NWNXPInvoke.NWNXCallFunction();
        }

        /// Removes a banned player name.
        public static void RemoveBannedPlayerName(string playerName)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "RemoveBannedPlayerName");
            NWNXPInvoke.NWNXPushString(playerName);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Gets a list of all banned IPs, CD Keys, and player names as a string.
        public static string GetBannedList()
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetBannedList");
            return NWNXPInvoke.NWNXPopString();
        }

        // Sets the module's name as shown in the server list.
        public static void SetModuleName(string name)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetModuleName");
            NWNXPInvoke.NWNXPushString(name);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Sets the server's name as shown in the server list.
        public static void SetServerName(string name)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetServerName");
            NWNXPInvoke.NWNXPushString(name);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Get an AdministrationOption value
        public static bool GetPlayOption(AdministrationOption option)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetPlayOption");
            NWNXPInvoke.NWNXPushInt((int)option);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt() == 1;
        }
        // Set an AdministrationOption value
        public static void SetPlayOption(AdministrationOption option, bool value)
        {
            // Use the NWNXPInvoke API instead of NativeFunction calls
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetPlayOption");
            NWNXPInvoke.NWNXPushInt(value ? 1 : 0);
            NWNXPInvoke.NWNXPushInt((int)option);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Delete the temporary user resource data (TURD) of a playerName + characterName
        public static bool DeleteTURD(string playerName, string characterName)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "DeleteTURD");
            NWNXPInvoke.NWNXPushString(playerName);
            NWNXPInvoke.NWNXPushString(characterName);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt() == 1;
        }


        // Get an admin_debug "Administration Debug Type" value.
        public static bool GetDebugValue(AdministrationDebugType type)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetDebugValue");
            NWNXPInvoke.NWNXPushInt((int)type);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt() == 1;
        }

        // Set an "Administration Debug Type" to a value.
        public static void SetDebugValue(AdministrationDebugType type, bool state)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetDebugValue");
            NWNXPInvoke.NWNXPushInt(state ? 1 : 0);
            NWNXPInvoke.NWNXPushInt((int)type);
            NWNXPInvoke.NWNXCallFunction();
        }

        /// Get the servers minimum level.
        public static int GetMinLevel()
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetMinLevel");
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt();
        }

        /// Set the servers minimum level.
        public static void SetMinLevel(int nLevel)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetMinLevel");
            NWNXPInvoke.NWNXPushInt(nLevel);
            NWNXPInvoke.NWNXCallFunction();
        }

        /// Get the servers maximum level.
        public static int GetMaxLevel()
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetMaxLevel");
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt();
        }

        /// Set the servers maximum level.
        public static void SetMaxLevel(int nLevel)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetMaxLevel");
            NWNXPInvoke.NWNXPushInt(nLevel);
            NWNXPInvoke.NWNXCallFunction();
        }

    }
}