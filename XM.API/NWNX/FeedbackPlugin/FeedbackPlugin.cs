namespace XM.API.NWNX.FeedbackPlugin
{
    internal static class FeedbackPlugin
    {
        /// <summary>
        /// Gets whether a feedback message is hidden.
        /// </summary>
        /// <param name="messageType">The type of feedback message to check.</param>
        /// <param name="player">The player character or OBJECT_INVALID for a global setting.</param>
        /// <returns>True if the message is hidden; otherwise, false.</returns>
        public static bool GetFeedbackMessageHidden(FeedbackMessageType messageType, uint player = OBJECT_INVALID)
        {
            return NWN.Core.NWNX.FeedbackPlugin.GetFeedbackMessageHidden((int)messageType, player) == 1;
        }

        /// <summary>
        /// Sets whether a feedback message is hidden.
        /// </summary>
        /// <param name="messageType">The type of feedback message to set.</param>
        /// <param name="isHidden">True to hide the message; otherwise, false.</param>
        /// <param name="player">The player character or OBJECT_INVALID for a global setting.</param>
        public static void SetFeedbackMessageHidden(FeedbackMessageType messageType, bool isHidden, uint player = OBJECT_INVALID)
        {
            NWN.Core.NWNX.FeedbackPlugin.SetFeedbackMessageHidden((int)messageType, isHidden ? 1 : 0, player);
        }

        /// <summary>
        /// Gets whether a combat log message is hidden.
        /// </summary>
        /// <param name="combatLogType">The type of combat log message to check.</param>
        /// <param name="player">The player character or OBJECT_INVALID for a global setting.</param>
        /// <returns>True if the message is hidden; otherwise, false.</returns>
        public static bool GetCombatLogMessageHidden(FeedbackCombatLogType combatLogType, uint player = OBJECT_INVALID)
        {
            return NWN.Core.NWNX.FeedbackPlugin.GetCombatLogMessageHidden((int)combatLogType, player) == 1;
        }

        /// <summary>
        /// Sets whether a combat log message is hidden.
        /// </summary>
        /// <param name="combatLogType">The type of combat log message to set.</param>
        /// <param name="isHidden">True to hide the message; otherwise, false.</param>
        /// <param name="player">The player character or OBJECT_INVALID for a global setting.</param>
        public static void SetCombatLogMessageHidden(FeedbackCombatLogType combatLogType, bool isHidden, uint player = OBJECT_INVALID)
        {
            NWN.Core.NWNX.FeedbackPlugin.SetCombatLogMessageHidden((int)combatLogType, isHidden ? 1 : 0, player);
        }

        /// <summary>
        /// Gets whether the journal update message is hidden.
        /// </summary>
        /// <param name="player">The player character or OBJECT_INVALID for a global setting.</param>
        /// <returns>True if the message is hidden; otherwise, false.</returns>
        public static bool GetJournalUpdatedMessageHidden(uint player = OBJECT_INVALID)
        {
            return NWN.Core.NWNX.FeedbackPlugin.GetJournalUpdatedMessageHidden(player) == 1;
        }

        /// <summary>
        /// Sets whether the journal update message is hidden.
        /// </summary>
        /// <param name="isHidden">True to hide the message; otherwise, false.</param>
        /// <param name="player">The player character or OBJECT_INVALID for a global setting.</param>
        public static void SetJournalUpdatedMessageHidden(bool isHidden, uint player = OBJECT_INVALID)
        {
            NWN.Core.NWNX.FeedbackPlugin.SetJournalUpdatedMessageHidden(isHidden ? 1 : 0, player);
        }

        /// <summary>
        /// Sets the mode for feedback messages.
        /// </summary>
        /// <param name="isWhitelist">True for whitelist mode, where all messages are hidden by default; otherwise, false for blacklist mode.</param>
        public static void SetFeedbackMessageMode(bool isWhitelist)
        {
            NWN.Core.NWNX.FeedbackPlugin.SetFeedbackMessageMode(isWhitelist ? 1 : 0);
        }

        /// <summary>
        /// Sets the mode for combat log messages.
        /// </summary>
        /// <param name="isWhitelist">True for whitelist mode, where all messages are hidden by default; otherwise, false for blacklist mode.</param>
        public static void SetCombatLogMessageMode(bool isWhitelist)
        {
            NWN.Core.NWNX.FeedbackPlugin.SetCombatLogMessageMode(isWhitelist ? 1 : 0);
        }

    }
}
