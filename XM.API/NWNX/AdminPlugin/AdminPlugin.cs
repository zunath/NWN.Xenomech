namespace XM.API.NWNX.AdminPlugin
{
    public static class AdminPlugin
    {
        /// <summary>
        /// Gets the current player password.
        /// </summary>
        /// <returns>The current player password.</returns>
        public static string GetPlayerPassword()
        {
            return NWN.Core.NWNX.AdminPlugin.GetPlayerPassword();
        }

        /// <summary>
        /// Sets the password for players to log in.
        /// </summary>
        /// <param name="password">The password to set.</param>
        public static void SetPlayerPassword(string password)
        {
            NWN.Core.NWNX.AdminPlugin.SetPlayerPassword(password);
        }

        /// <summary>
        /// Clears the player password required to log in.
        /// </summary>
        public static void ClearPlayerPassword()
        {
            NWN.Core.NWNX.AdminPlugin.ClearPlayerPassword();
        }

        /// <summary>
        /// Gets the current DM password.
        /// </summary>
        /// <returns>The current DM password.</returns>
        public static string GetDMPassword()
        {
            return NWN.Core.NWNX.AdminPlugin.GetDMPassword();
        }

        /// <summary>
        /// Sets the password for DMs to log in.
        /// </summary>
        /// <param name="password">The password to set.</param>
        public static void SetDMPassword(string password)
        {
            NWN.Core.NWNX.AdminPlugin.SetDMPassword(password);
        }

        /// <summary>
        /// Signals the server to immediately shut down.
        /// </summary>
        public static void ShutdownServer()
        {
            NWN.Core.NWNX.AdminPlugin.ShutdownServer();
        }

        /// <summary>
        /// Deletes the player character from the server vault.
        /// </summary>
        /// <param name="playerObject">The player to delete.</param>
        /// <param name="preserveBackup">Whether to preserve a backup of the character file.</param>
        /// <param name="kickMessage">Optional kick message. Defaults to "Delete Character".</param>
        public static void DeletePlayerCharacter(uint playerObject, bool preserveBackup = true, string kickMessage = "")
        {
            NWN.Core.NWNX.AdminPlugin.DeletePlayerCharacter(playerObject, preserveBackup ? 1 : 0, kickMessage);
        }

        /// <summary>
        /// Bans the provided IP address.
        /// </summary>
        /// <param name="ip">The IP address to ban.</param>
        public static void AddBannedIP(string ip)
        {
            NWN.Core.NWNX.AdminPlugin.AddBannedIP(ip);
        }

        /// <summary>
        /// Removes the ban on the provided IP address.
        /// </summary>
        /// <param name="ip">The IP address to unban.</param>
        public static void RemoveBannedIP(string ip)
        {
            NWN.Core.NWNX.AdminPlugin.RemoveBannedIP(ip);
        }

        /// <summary>
        /// Bans the provided Public CD Key.
        /// </summary>
        /// <param name="key">The Public CD Key to ban.</param>
        public static void AddBannedCDKey(string key)
        {
            NWN.Core.NWNX.AdminPlugin.AddBannedCDKey(key);
        }

        /// <summary>
        /// Removes the ban on the provided Public CD Key.
        /// </summary>
        /// <param name="key">The Public CD Key to unban.</param>
        public static void RemoveBannedCDKey(string key)
        {
            NWN.Core.NWNX.AdminPlugin.RemoveBannedCDKey(key);
        }

        /// <summary>
        /// Bans the provided player name.
        /// </summary>
        /// <param name="playerName">The player name to ban.</param>
        public static void AddBannedPlayerName(string playerName)
        {
            NWN.Core.NWNX.AdminPlugin.AddBannedPlayerName(playerName);
        }

        /// <summary>
        /// Removes the ban on the provided player name.
        /// </summary>
        /// <param name="playerName">The player name to unban.</param>
        public static void RemoveBannedPlayerName(string playerName)
        {
            NWN.Core.NWNX.AdminPlugin.RemoveBannedPlayerName(playerName);
        }

        /// <summary>
        /// Gets a list of all banned IPs, keys, and names.
        /// </summary>
        /// <returns>A string listing the banned IPs, keys, and names.</returns>
        public static string GetBannedList()
        {
            return NWN.Core.NWNX.AdminPlugin.GetBannedList();
        }

        /// <summary>
        /// Sets the module name as shown in the server list.
        /// </summary>
        /// <param name="name">The module name to set.</param>
        public static void SetModuleName(string name)
        {
            NWN.Core.NWNX.AdminPlugin.SetModuleName(name);
        }

        /// <summary>
        /// Sets the server name as shown in the server list.
        /// </summary>
        /// <param name="name">The server name to set.</param>
        public static void SetServerName(string name)
        {
            NWN.Core.NWNX.AdminPlugin.SetServerName(name);
        }

        /// <summary>
        /// Gets the server name as shown in the server list.
        /// </summary>
        /// <returns>The server name.</returns>
        public static string GetServerName()
        {
            return NWN.Core.NWNX.AdminPlugin.GetServerName();
        }

        /// <summary>
        /// Gets an Administration Option value.
        /// </summary>
        /// <param name="option">The administration option to retrieve.</param>
        /// <returns>The current setting for the option.</returns>
        public static AdminOptionType GetPlayOption(AdminOptionType option)
        {
            return (AdminOptionType)NWN.Core.NWNX.AdminPlugin.GetPlayOption((int)option);
        }

        /// <summary>
        /// Sets an Administration Option value.
        /// </summary>
        /// <param name="option">The administration option to set.</param>
        /// <param name="value">The value to set for the option.</param>
        public static void SetPlayOption(AdminOptionType option, int value)
        {
            NWN.Core.NWNX.AdminPlugin.SetPlayOption((int)option, value);
        }

        /// <summary>
        /// Deletes the TURD of the specified player and character name.
        /// </summary>
        /// <param name="playerName">The player's login name.</param>
        /// <param name="characterName">The character name.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public static bool DeleteTURD(string playerName, string characterName)
        {
            return NWN.Core.NWNX.AdminPlugin.DeleteTURD(playerName, characterName) == 1;
        }

        /// <summary>
        /// Gets a debug value for the specified Administration Debug Type.
        /// </summary>
        /// <param name="type">The debug type to retrieve.</param>
        /// <returns>The current debug value.</returns>
        public static AdminDebugType GetDebugValue(AdminDebugType type)
        {
            return (AdminDebugType)NWN.Core.NWNX.AdminPlugin.GetDebugValue((int)type);
        }

        /// <summary>
        /// Sets a debug value for the specified Administration Debug Type.
        /// </summary>
        /// <param name="type">The debug type to set.</param>
        /// <param name="state">The state to set for the debug type.</param>
        public static void SetDebugValue(AdminDebugType type, bool state)
        {
            NWN.Core.NWNX.AdminPlugin.SetDebugValue((int)type, state ? 1 : 0);
        }

        /// <summary>
        /// Reloads all rules (e.g., 2da files).
        /// </summary>
        public static void ReloadRules()
        {
            NWN.Core.NWNX.AdminPlugin.ReloadRules();
        }

        /// <summary>
        /// Gets the server's minimum level setting.
        /// </summary>
        /// <returns>The minimum level for the server.</returns>
        public static int GetMinLevel()
        {
            return NWN.Core.NWNX.AdminPlugin.GetMinLevel();
        }

        /// <summary>
        /// Sets the server's minimum level setting.
        /// </summary>
        /// <param name="level">The minimum level to set for the server.</param>
        public static void SetMinLevel(int level)
        {
            NWN.Core.NWNX.AdminPlugin.SetMinLevel(level);
        }

        /// <summary>
        /// Gets the server's maximum level setting.
        /// </summary>
        /// <returns>The maximum level for the server.</returns>
        public static int GetMaxLevel()
        {
            return NWN.Core.NWNX.AdminPlugin.GetMaxLevel();
        }

        /// <summary>
        /// Sets the server's maximum level setting.
        /// </summary>
        /// <param name="level">The maximum level to set for the server.</param>
        public static void SetMaxLevel(int level)
        {
            NWN.Core.NWNX.AdminPlugin.SetMaxLevel(level);
        }
    }
}
