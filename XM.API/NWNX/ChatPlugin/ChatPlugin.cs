namespace XM.API.NWNX.ChatPlugin
{
    public static class ChatPlugin
    {
        /// <summary>
        /// Sends a chat message.
        /// </summary>
        /// <param name="channel">The channel to send the message.</param>
        /// <param name="message">The message to send.</param>
        /// <param name="sender">The sender of the message.</param>
        /// <param name="target">The receiver of the message.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public static bool SendMessage(ChatChannelType channel, string message, uint sender = OBJECT_INVALID, uint target = OBJECT_INVALID)
        {
            return NWN.Core.NWNX.ChatPlugin.SendMessage((int)channel, message, sender, target) == 1;
        }

        /// <summary>
        /// Registers the script which receives all chat messages.
        /// </summary>
        /// <param name="script">The script name to handle the chat events.</param>
        public static void RegisterChatScript(string script)
        {
            NWN.Core.NWNX.ChatPlugin.RegisterChatScript(script);
        }

        /// <summary>
        /// Skips a chat message.
        /// </summary>
        public static void SkipMessage()
        {
            NWN.Core.NWNX.ChatPlugin.SkipMessage();
        }

        /// <summary>
        /// Gets the chat channel.
        /// </summary>
        /// <returns>The channel the message was sent on.</returns>
        public static ChatChannelType GetChannel()
        {
            return (ChatChannelType)NWN.Core.NWNX.ChatPlugin.GetChannel();
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <returns>The message sent.</returns>
        public static string GetMessage()
        {
            return NWN.Core.NWNX.ChatPlugin.GetMessage();
        }

        /// <summary>
        /// Gets the sender of the message.
        /// </summary>
        /// <returns>The object sending the message.</returns>
        public static uint GetSender()
        {
            return NWN.Core.NWNX.ChatPlugin.GetSender();
        }

        /// <summary>
        /// Gets the target of the message.
        /// </summary>
        /// <returns>The target of the message or OBJECT_INVALID if no target.</returns>
        public static uint GetTarget()
        {
            return NWN.Core.NWNX.ChatPlugin.GetTarget();
        }

        /// <summary>
        /// Sets the distance with which the player hears talks or whispers.
        /// </summary>
        /// <param name="distance">The distance in meters.</param>
        /// <param name="listener">The listener, if OBJECT_INVALID then it will be set server-wide.</param>
        /// <param name="channel">The channel to modify the distance heard. Only applicable for talk and whisper.</param>
        public static void SetChatHearingDistance(float distance, uint listener = OBJECT_INVALID, ChatChannelType channel = ChatChannelType.PlayerTalk)
        {
            NWN.Core.NWNX.ChatPlugin.SetChatHearingDistance(distance, listener, (int)channel);
        }

        /// <summary>
        /// Gets the distance with which the player hears talks or whispers.
        /// </summary>
        /// <param name="listener">The listener, if OBJECT_INVALID then it will return the server-wide setting.</param>
        /// <param name="channel">The channel. Only applicable for talk and whisper.</param>
        /// <returns>The hearing distance in meters.</returns>
        public static float GetChatHearingDistance(uint listener = OBJECT_INVALID, ChatChannelType channel = ChatChannelType.PlayerTalk)
        {
            return NWN.Core.NWNX.ChatPlugin.GetChatHearingDistance(listener, (int)channel);
        }

    }
}
