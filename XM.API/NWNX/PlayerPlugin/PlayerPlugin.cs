using System.Numerics;
using NWN.Core.NWNX;
using XM.API.BaseTypes;
using XM.API.Constants;

namespace XM.API.NWNX.PlayerPlugin
{
    public static class PlayerPlugin
    {
        /// Force display placeable examine window for player
        /// @note If used on a placeable in a different area than the player, the portrait will not be shown.
        /// <param name="player">The player object.</param>
        /// <param name="placeable">The placeable object.</param>
        public static void ForcePlaceableExamineWindow(uint player, uint placeable)
        {
            NWN.Core.NWNX.PlayerPlugin.ForcePlaceableExamineWindow(player, placeable);
        }

        /// Force opens the target object's inventory for the player.
        /// @note If the placeable is in a different area than the player, the portrait will not be shown
        /// * The placeable's open/close animations will be played
        /// * Clicking the 'close' button will cause the player to walk to the placeable If the placeable is in a
        /// different area, the player will just walk to the edge of the current area and stop.
        /// This action can be cancelled manually.
        /// * Walking will close the placeable automatically.
        /// <param name="player">The player object.</param>
        /// <param name="placeable">The placeable object.</param>
        public static void ForcePlaceableInventoryWindow(uint player, uint placeable)
        {
            NWN.Core.NWNX.PlayerPlugin.ForcePlaceableInventoryWindow(player, placeable);
        }

        /// Starts displaying a timing bar.
        /// <param name="player">The player object.</param>
        /// <param name="seconds">The length of time the timing bar will complete.</param>
        /// <param name="script">The script to run at the bar's completion.</param>
        /// <param name="type">The @ref timing_bar_types "Timing Bar Type"</param>
        /// @remark Only one timing bar can be ran at the same time.
        public static void StartGuiTimingBar(uint player, float seconds, string script = "", TimingBarType type = TimingBarType.Custom)
        {
            NWN.Core.NWNX.PlayerPlugin.StartGuiTimingBar(player, seconds, script, (int)type);
        }

        /// Stop displaying a timing bar.
        /// <param name="player">The player object.</param>
        /// <param name="script">The script to run when stopped.</param>
        public static void StopGuiTimingBar(uint player, string script = "")
        {
            NWN.Core.NWNX.PlayerPlugin.StopGuiTimingBar(player, script);
        }

        /// Sets whether the player should always walk when given movement commands.
        /// <param name="player">The player object.</param>
        /// <param name="bWalk">True to set the player to always walk.</param>
        /// @remark Clicking on the ground or using WASD will trigger walking instead of running.
        public static void SetAlwaysWalk(uint player, bool bWalk = true)
        {
            NWN.Core.NWNX.PlayerPlugin.SetAlwaysWalk(player, bWalk ? 1 : 0);
        }

        /// Gets the player's quickbar slot info.
        /// <param name="player">The player object.</param>
        /// <param name="slot">Slot ID 0-35</param>
        /// <returns>An NWNX_Player_QuickBarSlot struct.</returns>
        public static QuickBarSlot GetQuickBarSlot(uint player, int slot)
        {
            return NWN.Core.NWNX.PlayerPlugin.GetQuickBarSlot(player, slot);
        }

        /// Sets the player's quickbar slot info.
        /// <param name="player">The player object.</param>
        /// <param name="slot">Slot ID 0-35</param>
        /// <param name="qbs">An NWNX_Player_QuickBarSlot struct.</param>
        public static void SetQuickBarSlot(uint player, int slot, QuickBarSlot qbs)
        {
            NWN.Core.NWNX.PlayerPlugin.SetQuickBarSlot(player, slot, qbs);
        }

        /// Get the name of the .bic file associated with the player's character.
        /// <param name="player">The player object.</param>
        /// <returns>The filename for this player's bic. (Not including the ".bic")</returns>
        public static string GetBicFileName(uint player)
        {
            return NWN.Core.NWNX.PlayerPlugin.GetBicFileName(player);
        }

        /// Plays the VFX at the target position in the current area for the given player only.
        /// <param name="player">The player object.</param>
        /// <param name="effectId">The effect id.</param>
        /// <param name="position">The position to play the visual effect.</param>
        /// <param name="scale">The scale of the effect</param>
        /// <param name="translate">A translation vector to offset the position of the effect</param>
        /// <param name="rotate">A rotation vector to rotate the effect</param>
        public static void ShowVisualEffect(uint player, int effectId, Vector3 position, float scale = 1.0f, Vector3 translate = default, Vector3 rotate = default)
        {
            NWN.Core.NWNX.PlayerPlugin.ShowVisualEffect(player, effectId, position, scale, translate, rotate);
        }

        /// Changes the daytime music track for the given player only.
        /// <param name="player">The player object.</param>
        /// <param name="track">The track id to play.</param>
        public static void MusicBackgroundChangeDay(uint player, int track)
        {
            NWN.Core.NWNX.PlayerPlugin.MusicBackgroundChangeDay(player, track);
        }

        /// Changes the nighttime music track for the given player only.
        /// <param name="player">The player object.</param>
        /// <param name="track">The track id to play.</param>
        public static void MusicBackgroundChangeNight(uint player, int track)
        {
            NWN.Core.NWNX.PlayerPlugin.MusicBackgroundChangeNight(player, track);
        }

        /// Starts the background music for the given player only.
        /// <param name="player">The player object.</param>
        public static void MusicBackgroundStart(uint player)
        {
            NWN.Core.NWNX.PlayerPlugin.MusicBackgroundStart(player);
        }

        /// Stops the background music for the given player only.
        /// <param name="player">The player object.</param>
        public static void MusicBackgroundStop(uint player)
        {
            NWN.Core.NWNX.PlayerPlugin.MusicBackgroundStop(player);
        }

        /// Changes the battle music track for the given player only.
        /// <param name="player">The player object.</param>
        /// <param name="track">The track id to play.</param>
        public static void MusicBattleChange(uint player, int track)
        {
            NWN.Core.NWNX.PlayerPlugin.MusicBattleChange(player, track);
        }

        /// Starts the battle music for the given player only.
        /// <param name="player">The player object.</param>
        public static void MusicBattleStart(uint player)
        {
            NWN.Core.NWNX.PlayerPlugin.MusicBattleStart(player);
        }
        /// Stops the battle music for the given player only
        /// <param name="player">The player object.</param>
        public static void MusicBattleStop(uint player)
        {
            NWN.Core.NWNX.PlayerPlugin.MusicBattleStop(player);
        }

        /// Play a sound at the location of target for the given player only
        /// <param name="player">The player object.</param>
        /// <param name="sound">The sound resref.</param>
        /// <param name="target">The target object for the sound to originate. If target OBJECT_INVALID the sound will play at the location of the player.</param>
        public static void PlaySound(uint player, string sound, uint target = OBJECT_INVALID)
        {
            NWN.Core.NWNX.PlayerPlugin.PlaySound(player, sound, target);
        }

        /// Toggle a placeable's usable flag for the given player only
        /// <param name="player">The player object.</param>
        /// <param name="placeable">The placeable object.</param>
        /// <param name="usable">True for usable.</param>
        public static void SetPlaceableUsable(uint player, uint placeable, bool usable)
        {
            NWN.Core.NWNX.PlayerPlugin.SetPlaceableUsable(player, placeable, usable ? 1 : 0);
        }

        /// Override player's rest duration
        /// <param name="player">The player object.</param>
        /// <param name="duration">The duration of rest in milliseconds, 1000 = 1 second. Minimum duration of 10ms. -1 clears the override.</param>
        public static void SetRestDuration(uint player, int duration)
        {
            NWN.Core.NWNX.PlayerPlugin.SetRestDuration(player, duration);
        }

        /// Apply visualeffect to target that only player can see
        /// <param name="player">The player object.</param>
        /// <param name="target">The target object to play the effect upon.</param>
        /// <param name="visualEffect">The visual effect id.</param>
        /// <param name="scale">The scale of the effect</param>
        /// <param name="translate">A translation vector to offset the position of the effect</param>
        /// <param name="rotate">A rotation vector to rotate the effect</param>
        /// @note Only works with instant effects: VFX_COM_*, VFX_FNF_*, VFX_IMP_*
        public static void ApplyInstantVisualEffectToObject(uint player, uint target, VisualEffectType visualEffect, float scale = 1.0f, Vector3 translate = default, Vector3 rotate = default)
        {
            NWN.Core.NWNX.PlayerPlugin.ApplyInstantVisualEffectToObject(player, target, (int)visualEffect, scale, translate, rotate);
        }

        /// Refreshes the player's character sheet
        /// <param name="player">The player object.</param>
        /// @note You may need to use DelayCommand if you're manipulating values through nwnx and forcing a UI refresh, 0.5s seemed to be fine
        public static void UpdateCharacterSheet(uint player)
        {
            NWN.Core.NWNX.PlayerPlugin.UpdateCharacterSheet(player);
        }

        /// Allows player to open target's inventory
        /// <param name="player">The player object.</param>
        /// <param name="target">The target object, must be a creature or another player.</param>
        /// <param name="open">True to open.</param>
        /// @remark Only works if player and target are in the same area.
        public static void OpenInventory(uint player, uint target, bool open = true)
        {
            NWN.Core.NWNX.PlayerPlugin.OpenInventory(player, target, open ? 1 : 0);
        }

        /// Get player's area exploration state
        /// <param name="player">The player object.</param>
        /// <param name="area">The area object.</param>
        /// <returns>A string representation of the tiles explored for that area.</returns>
        public static string GetAreaExplorationState(uint player, uint area)
        {
            return NWN.Core.NWNX.PlayerPlugin.GetAreaExplorationState(player, area);
        }

        /// Set player's area exploration state.
        /// <param name="player">The player object.</param>
        /// <param name="area">The area object.</param>
        /// <param name="str">An encoded string obtained with NWNX_Player_GetAreaExplorationState()</param>
        public static void SetAreaExplorationState(uint player, uint area, string str)
        {
            NWN.Core.NWNX.PlayerPlugin.SetAreaExplorationState(player, area, str);
        }

        /// Override player's rest animation.
        /// <param name="oPlayer">The player object.</param>
        /// <param name="nAnimation">The NWNX animation id. This does not take ANIMATION_LOOPING_* or ANIMATION_FIREFORGET_* constants. Instead use NWNX_Consts_TranslateNWScriptAnimation() to get the NWNX equivalent. -1 to clear the override.</param>
        public static void SetRestAnimation(uint oPlayer, int nAnimation)
        {
            NWN.Core.NWNX.PlayerPlugin.SetRestAnimation(oPlayer, nAnimation);
        }

        /// Override a visual transform on the given object that only player will see.
        /// <param name="oPlayer">The player object.</param>
        /// <param name="oObject">The target object. Can be any valid Creature, Placeable, Item or Door.</param>
        /// <param name="nTransform">One of OBJECT_VISUAL_TRANSFORM_* or -1 to remove the override.</param>
        /// <param name="fValue">Depends on the transformation to apply.</param>
        public static void SetObjectVisualTransformOverride(uint oPlayer, uint oObject, ObjectVisualTransformType nTransform, float fValue)
        {
            NWN.Core.NWNX.PlayerPlugin.SetObjectVisualTransformOverride(oPlayer, oObject, (int)nTransform, fValue);
        }

        /// Apply a looping visualeffect to a target that only player can see
        /// <param name="player">The player object.</param>
        /// <param name="target">The target object.</param>
        /// <param name="visualEffect">A VFX_DUR_*. Calling again will remove an applied effect. -1 to remove all effects</param>
        /// @note Only really works with looping effects: VFX_DUR_*. Other types kind of work, they'll play when reentering the area and the object is in view or when they come back in view range.
        public static void ApplyLoopingVisualEffectToObject(uint player, uint target, VisualEffectType visualEffect)
        {
            NWN.Core.NWNX.PlayerPlugin.ApplyLoopingVisualEffectToObject(player, target, (int)visualEffect);
        }

        /// Override the name of placeable for player only
        /// <param name="player">The player object.</param>
        /// <param name="placeable">The placeable object.</param>
        /// <param name="name">The name for the placeable for this player, "" to clear the override.</param>
        public static void SetPlaceableNameOverride(uint player, uint placeable, string name)
        {
            NWN.Core.NWNX.PlayerPlugin.SetPlaceableNameOverride(player, placeable, name);
        }

        /// Gets whether a quest has been completed by a player
        /// <param name="player">The player object.</param>
        /// <param name="sQuestName">The name identifier of the quest from the Journal Editor.</param>
        /// <returns>True if the quest has been completed. -1 if the player does not have the journal entry.</returns>
        public static bool GetQuestCompleted(uint player, string sQuestName)
        {
            return NWN.Core.NWNX.PlayerPlugin.GetQuestCompleted(player, sQuestName) == 1;
        }

        /// Place waypoints on module load representing where a PC should start
        /// <param name="sCDKeyOrCommunityName">The Public CD Key or Community Name of the player, this will depend on your vault type.</param>
        /// <param name="sBicFileName">The filename for the character. Retrieved with NWNX_Player_GetBicFileName().</param>
        /// <param name="oWP">The waypoint object to place where the PC should start.</param>
        /// <param name="bFirstConnectOnly">Set to False if you would like the PC to go to this location every time they login instead of just every server restart.</param>
        public static void SetPersistentLocation(string sCDKeyOrCommunityName, string sBicFileName, uint oWP, bool bFirstConnectOnly = true)
        {
            NWN.Core.NWNX.PlayerPlugin.SetPersistentLocation(sCDKeyOrCommunityName, sBicFileName, oWP, bFirstConnectOnly ? 1 : 0);
        }

        /// Force an item name to be updated.
        /// @note This is a workaround for bug that occurs when updating item names in open containers.
        /// <param name="oPlayer">The player object.</param>
        /// <param name="oItem">The item object.</param>
        public static void UpdateItemName(uint oPlayer, uint oItem)
        {
            NWN.Core.NWNX.PlayerPlugin.UpdateItemName(oPlayer, oItem);
        }

        /// Possesses a creature by temporarily making them a familiar
        /// @note The possessed creature will send automap data back to the possessor.
        /// <param name="oPossessor">The possessor player object.</param>
        /// <param name="oPossessed">The possessed creature object. Only works on NPCs.</param>
        /// <param name="bMindImmune">If False will remove the mind immunity effect on the possessor.</param>
        /// <param name="bCreateDefaultQB">If True will populate the quick bar with default buttons.</param>
        /// <returns>True if possession succeeded.</returns>
        public static bool PossessCreature(uint oPossessor, uint oPossessed, bool bMindImmune = true, bool bCreateDefaultQB = false)
        {
            return NWN.Core.NWNX.PlayerPlugin.PossessCreature(oPossessor, oPossessed, bMindImmune ? 1 : 0, bCreateDefaultQB ? 1 : 0) == 1;
        }

        /// Returns the platform ID of the given player (NWNX_PLAYER_PLATFORM_*).
        /// <param name="oPlayer">The player object.</param>
        public static PlayerDevicePlatformType GetPlatformId(uint oPlayer)
        {
            return (PlayerDevicePlatformType)NWN.Core.NWNX.PlayerPlugin.GetPlatformId(oPlayer);
        }

        /// Returns the game language of the given player (uses NWNX_DIALOG_LANGUAGE_*).
        /// <param name="oPlayer">The player object.</param>
        public static PlayerLanguageType GetLanguage(uint oPlayer)
        {
            return (PlayerLanguageType)NWN.Core.NWNX.PlayerPlugin.GetLanguage(oPlayer);
        }

        /// Override sOldResName with sNewResName of nResType for oPlayer.
        /// @warning If sNewResName does not exist on oPlayer's client it will crash their game.
        /// <param name="oPlayer">The player object.</param>
        /// <param name="nResType">The res type, see nwnx_util.nss for constants.</param>
        /// <param name="sOldResName">The old res name, 16 characters or less.</param>
        /// <param name="sNewResName">The new res name or "" to clear a previous override, 16 characters or less.</param>
        public static void SetResManOverride(uint oPlayer, int nResType, string sOldResName, string sNewResName)
        {
            NWN.Core.NWNX.PlayerPlugin.SetResManOverride(oPlayer, nResType, sOldResName, sNewResName);
        }

        /// Set nCustomTokenNumber to sTokenValue for oPlayer only.
        /// @note The basegame SetCustomToken() will override any personal tokens.
        /// <param name="oPlayer">The player object.</param>
        /// <param name="nCustomTokenNumber">The token number.</param>
        /// <param name="sTokenValue">The token text.</param>
        public static void SetCustomToken(uint oPlayer, int nCustomTokenNumber, string sTokenValue)
        {
            NWN.Core.NWNX.PlayerPlugin.SetCustomToken(oPlayer, nCustomTokenNumber, sTokenValue);
        }
        /// Override the name of creature for player only
        /// <param name="oPlayer">The player object.</param>
        /// <param name="oCreature">The creature object.</param>
        /// <param name="sName">The name for the creature for this player, "" to clear the override.</param>
        public static void SetCreatureNameOverride(uint oPlayer, uint oCreature, string sName)
        {
            NWN.Core.NWNX.PlayerPlugin.SetCreatureNameOverride(oPlayer, oCreature, sName);
        }

        /// Display floaty text above oCreature for oPlayer only.
        /// @note This will also display the floaty text above creatures that are not part of oPlayer's faction.
        /// <param name="oPlayer">The player to display the text to.</param>
        /// <param name="oCreature">The creature to display the text above.</param>
        /// <param name="sText">The text to display.</param>
        /// <param name="bChatWindow">If true, sText will be displayed in oPlayer's chat window.</param>
        public static void FloatingTextStringOnCreature(uint oPlayer, uint oCreature, string sText, bool bChatWindow = true)
        {
            NWN.Core.NWNX.PlayerPlugin.FloatingTextStringOnCreature(oPlayer, oCreature, sText, bChatWindow ? 1 : 0);
        }

        /// Toggle oPlayer's PlayerDM status.
        /// @note This function does nothing for actual DMClient DMs or players with a client version < 8193.14
        /// <param name="oPlayer">The player.</param>
        /// <param name="bIsDM">True to toggle DM mode on, false for off.</param>
        public static void ToggleDM(uint oPlayer, bool bIsDM)
        {
            NWN.Core.NWNX.PlayerPlugin.ToggleDM(oPlayer, bIsDM ? 1 : 0);
        }

        /// Override the mouse cursor of oObject for oPlayer only
        /// <param name="oPlayer">The player object.</param>
        /// <param name="oObject">The object.</param>
        /// <param name="nCursor">The cursor, one of MOUSECURSOR_*. -1 to clear the override.</param>
        public static void SetObjectMouseCursorOverride(uint oPlayer, uint oObject, MouseCursorType nCursor)
        {
            NWN.Core.NWNX.PlayerPlugin.SetObjectMouseCursorOverride(oPlayer, oObject, (int)nCursor);
        }

        /// Override the hilite color of oObject for oPlayer only
        /// <param name="oPlayer">The player object.</param>
        /// <param name="oObject">The object.</param>
        /// <param name="nColor">The color in 0xRRGGBB format, -1 to clear the override.</param>
        public static void SetObjectHiliteColorOverride(uint oPlayer, uint oObject, int nColor)
        {
            NWN.Core.NWNX.PlayerPlugin.SetObjectHiliteColorOverride(oPlayer, oObject, nColor);
        }

        /// Remove effects with sEffectTag from oPlayer's TURD
        /// @note This function should be called in the NWNX_ON_CLIENT_DISCONNECT_AFTER event, OnClientLeave is too early for the TURD to exist.
        /// <param name="oPlayer">The player object.</param>
        /// <param name="sEffectTag">The effect tag.</param>
        public static void RemoveEffectFromTURD(uint oPlayer, string sEffectTag)
        {
            NWN.Core.NWNX.PlayerPlugin.RemoveEffectFromTURD(oPlayer, sEffectTag);
        }

        /// Set the location oPlayer will spawn when logging in to the server.
        /// @note This function is best called in the NWNX_ON_ELC_VALIDATE_CHARACTER_BEFORE event, OnClientEnter will be too late.
        /// <param name="oPlayer">The player object.</param>
        /// <param name="locSpawn">The location.</param>
        public static void SetSpawnLocation(uint oPlayer, Location locSpawn)
        {
            NWN.Core.NWNX.PlayerPlugin.SetSpawnLocation(oPlayer, locSpawn);
        }

        /// Resends palettes to a DM.
        /// <param name="oPlayer">- the DM to send them to.</param>
        public static void SendDMAllCreatorLists(uint oPlayer)
        {
            NWN.Core.NWNX.PlayerPlugin.SendDMAllCreatorLists(oPlayer);
        }

        /// Give a custom journal entry to oPlayer.
        /// @warning Custom entries are wiped on client enter - they must be reapplied.
        /// <param name="oPlayer">The player object.</param>
        /// <param name="journalEntry">The journal entry in the form of a struct.</param>
        /// <param name="nSilentUpdate">0 = Notify player via sound effects and feedback message, 1 = Suppress sound effects and feedback message</param>
        /// <returns>A positive number to indicate the new amount of journal entries on the player.</returns>
        /// @note In contrast to conventional nwn journal entries - this method will overwrite entries with the same tag, so the index / count of entries will only increase if you add new entries with unique tags
        public static int AddCustomJournalEntry(uint oPlayer, JournalEntry journalEntry, bool nSilentUpdate = false)
        {
            return NWN.Core.NWNX.PlayerPlugin.AddCustomJournalEntry(oPlayer, journalEntry, nSilentUpdate ? 1 : 0);
        }

        /// Returns a struct containing a journal entry that can then be modified.
        /// <param name="oPlayer">The player object.</param>
        /// <param name="questTag">The quest tag you wish to get the journal entry for.</param>
        /// <returns>A struct containing the journal entry data.</returns>
        /// @note This method will return -1 for the Updated field in the event that no matching journal entry was found,
        /// only the last matching quest tag will be returned. Eg: If you add 3 journal updates to a player, only the 3rd one will be returned as that is the active one that the player currently sees.
        public static JournalEntry GetJournalEntry(uint oPlayer, string questTag)
        {
            return NWN.Core.NWNX.PlayerPlugin.GetJournalEntry(oPlayer, questTag);
        }

        /// Closes any store oPlayer may have open.
        /// <param name="oPlayer">The player object.</param>
        public static void CloseStore(uint oPlayer)
        {
            NWN.Core.NWNX.PlayerPlugin.CloseStore(oPlayer);
        }

        /// Override nStrRef from the TlkTable with sOverride for oPlayer only.
        /// <param name="oPlayer">The player.</param>
        /// <param name="nStrRef">The StrRef.</param>
        /// <param name="sOverride">The new value for nStrRef or "" to remove the override.</param>
        /// <param name="bRestoreGlobal">If true, when removing a personal override it will attempt to restore the global override if it exists.</param>
        /// @note Overrides will not persist through relogging.
        public static void SetTlkOverride(uint oPlayer, int nStrRef, string sOverride, bool bRestoreGlobal = true)
        {
            NWN.Core.NWNX.PlayerPlugin.SetTlkOverride(oPlayer, nStrRef, sOverride, bRestoreGlobal ? 1 : 0);
        }
        /// Make the player reload its TlkTable.
        /// <param name="oPlayer">The player.</param>
        public static void ReloadTlk(uint oPlayer)
        {
            NWN.Core.NWNX.PlayerPlugin.ReloadTlk(oPlayer);
        }

        /// Update wind for oPlayer only.
        /// <param name="oPlayer">The player.</param>
        /// <param name="vDirection">The Wind's direction.</param>
        /// <param name="fMagnitude">The Wind's magnitude.</param>
        /// <param name="fYaw">The Wind's yaw.</param>
        /// <param name="fPitch">The Wind's pitch.</param>
        public static void UpdateWind(uint oPlayer, Vector3 vDirection, float fMagnitude, float fYaw, float fPitch)
        {
            NWN.Core.NWNX.PlayerPlugin.UpdateWind(oPlayer, vDirection, fMagnitude, fYaw, fPitch);
        }

        /// Update the SkyBox for oPlayer only.
        /// <param name="oPlayer">The player.</param>
        /// <param name="nSkyBox">The Skybox ID.</param>
        public static void UpdateSkyBox(uint oPlayer, SkyboxType nSkyBox)
        {
            NWN.Core.NWNX.PlayerPlugin.UpdateSkyBox(oPlayer, (int)nSkyBox);
        }

        /// Update Sun and Moon Fog Color for oPlayer only.
        /// <param name="oPlayer">The player.</param>
        /// <param name="nSunFogColor">The int value of Sun Fog color.</param>
        /// <param name="nMoonFogColor">The int value of Moon Fog color.</param>
        public static void UpdateFogColor(uint oPlayer, int nSunFogColor, int nMoonFogColor)
        {
            NWN.Core.NWNX.PlayerPlugin.UpdateFogColor(oPlayer, nSunFogColor, nMoonFogColor);
        }

        /// Update Sun and Moon Fog Amount for oPlayer only.
        /// <param name="oPlayer">The player.</param>
        /// <param name="nSunFogAmount">The int value of Sun Fog amount (range 0-255).</param>
        /// <param name="nMoonFogAmount">The int value of Moon Fog amount (range 0-255).</param>
        public static void UpdateFogAmount(uint oPlayer, int nSunFogAmount, int nMoonFogAmount)
        {
            NWN.Core.NWNX.PlayerPlugin.UpdateFogAmount(oPlayer, nSunFogAmount, nMoonFogAmount);
        }

        /// Returns the currently-possessed game object of a player.
        /// <param name="oPlayer">The player object (e.g. from GetFirst/NextPC()).</param>
        /// <returns>The actual game object of oPlayer, or OBJECT_INVALID on error.</returns>
        public static uint GetGameObject(uint oPlayer)
        {
            return NWN.Core.NWNX.PlayerPlugin.GetGameObject(oPlayer);
        }

        /// Override the ui discovery mask of oObject for oPlayer only
        /// <param name="oPlayer">The player object.</param>
        /// <param name="oObject">The target object.</param>
        /// <param name="nMask">A mask of OBJECT_UI_DISCOVERY_*, or -1 to clear the override</param>
        public static void SetObjectUiDiscoveryMaskOverride(uint oPlayer, uint oObject, ObjectUIDiscoveryType nMask)
        {
            NWN.Core.NWNX.PlayerPlugin.SetObjectUiDiscoveryMaskOverride(oPlayer, oObject, (int)nMask);
        }

        /// Send a party invite from oInviter to oPlayer
        /// <param name="oPlayer">The player to invite</param>
        /// <param name="oInviter">The one inviting the player</param>
        /// <param name="bForceInvite">True: Sends the invite even if the target ignores invites</param>
        /// <param name="bHideDialog">True: Does not show the party invitation dialog</param>
        public static void SendPartyInvite(uint oPlayer, uint oInviter, bool bForceInvite = false, bool bHideDialog = false)
        {
            NWN.Core.NWNX.PlayerPlugin.SendPartyInvite(oPlayer, oInviter, bForceInvite ? 1 : 0, bHideDialog ? 1 : 0);
        }

        /// Get the TURD for oPlayer
        /// <param name="oPlayer">The offline player to get the TURD from</param>
        /// <returns>The TURD object of oPlayer, or OBJECT_INVALID if no TURD exists</returns>
        public static uint GetTURD(uint oPlayer)
        {
            return NWN.Core.NWNX.PlayerPlugin.GetTURD(oPlayer);
        }

        /// Reloads the color palettes for oPlayer
        /// <param name="oPlayer">The player to reload the color palette for</param>
        public static void ReloadColorPalettes(uint oPlayer)
        {
            NWN.Core.NWNX.PlayerPlugin.ReloadColorPalettes(oPlayer);
        }
    }
}
