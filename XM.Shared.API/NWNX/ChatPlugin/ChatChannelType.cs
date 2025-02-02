namespace XM.Shared.API.NWNX.ChatPlugin
{
    public enum ChatChannelType
    {
        PlayerTalk = NWN.Core.NWNX.ChatPlugin.NWNX_CHAT_CHANNEL_PLAYER_TALK,
        PlayerShout = NWN.Core.NWNX.ChatPlugin.NWNX_CHAT_CHANNEL_PLAYER_SHOUT,
        PlayerWhisper = NWN.Core.NWNX.ChatPlugin.NWNX_CHAT_CHANNEL_PLAYER_WHISPER,
        PlayerTell = NWN.Core.NWNX.ChatPlugin.NWNX_CHAT_CHANNEL_PLAYER_TELL,
        ServerMessage = NWN.Core.NWNX.ChatPlugin.NWNX_CHAT_CHANNEL_SERVER_MSG,
        PlayerParty = NWN.Core.NWNX.ChatPlugin.NWNX_CHAT_CHANNEL_PLAYER_PARTY,
        PlayerDM = NWN.Core.NWNX.ChatPlugin.NWNX_CHAT_CHANNEL_PLAYER_DM,
        DMTalk = NWN.Core.NWNX.ChatPlugin.NWNX_CHAT_CHANNEL_DM_TALK,
        DMShout = NWN.Core.NWNX.ChatPlugin.NWNX_CHAT_CHANNEL_DM_SHOUT,
        DMWhisper = NWN.Core.NWNX.ChatPlugin.NWNX_CHAT_CHANNEL_DM_WHISPER,
        DMTell = NWN.Core.NWNX.ChatPlugin.NWNX_CHAT_CHANNEL_DM_TELL,
        DMParty = NWN.Core.NWNX.ChatPlugin.NWNX_CHAT_CHANNEL_DM_PARTY,
        DMDM = NWN.Core.NWNX.ChatPlugin.NWNX_CHAT_CHANNEL_DM_DM
    }
}
