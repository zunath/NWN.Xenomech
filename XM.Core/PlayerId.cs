using System;

namespace XM.Core
{
    public static class PlayerId
    {
        /// <summary>
        /// Retrieves the ID for a player or DM character
        /// </summary>
        /// <param name="player">The player or DM to retrieve an ID of</param>
        /// <returns>The ID assigned to the player or DM character</returns>
        /// <exception cref="Exception">Thrown in the event player is not a PC or DM.</exception>
        public static string Get(uint player)
        {
            if (GetIsDM(player) && !GetIsDMPossessed(player))
            {
                return "DM-" + GetPCPublicCDKey(player);
            }
            else if (GetIsPC(player) && !GetIsDMPossessed(player) && !GetIsDM(player))
            {
                return GetObjectUUID(player);
            }

            throw new Exception($"Player IDs can only be retrieved for players and DMs.");
        }
    }
}
