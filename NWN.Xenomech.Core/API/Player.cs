using System.Numerics;
using NWN.Xenomech.Core.API.Enum;
using NWN.Xenomech.Core.Interop;

namespace NWN.Xenomech.Core.API
{
    public partial class NWScript
    {/// <summary>
     ///   Gets the current cutscene state of the player specified by oCreature.
     ///   Returns TRUE if the player is in cutscene mode.
     ///   Returns FALSE if the player is not in cutscene mode, or on an error
     ///   (such as specifying a non creature object).
     /// </summary>
        public static bool GetIsInCutsceneMode(uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(781);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Forces this player's camera to be set to this height. Setting this value to zero will
        ///   restore the camera to the racial default height.
        /// </summary>
        public static void SetCameraHeight(uint oPlayer, float fHeight = 0.0f)
        {
            NWNXPInvoke.StackPushFloat(fHeight);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(776);
        }

        /// <summary>
        ///   Changes the current Day/Night cycle for this player to night
        ///   - oPlayer: which player to change the lighting for
        ///   - fTransitionTime: how long the transition should take
        /// </summary>
        public static void DayToNight(uint oPlayer, float fTransitionTime = 0.0f)
        {
            NWNXPInvoke.StackPushFloat(fTransitionTime);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(750);
        }

        /// <summary>
        ///   Changes the current Day/Night cycle for this player to daylight
        ///   - oPlayer: which player to change the lighting for
        ///   - fTransitionTime: how long the transition should take
        /// </summary>
        public static void NightToDay(uint oPlayer, float fTransitionTime = 0.0f)
        {
            NWNXPInvoke.StackPushFloat(fTransitionTime);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(751);
        }

        /// <summary>
        ///   Returns the current movement rate factor
        ///   of the cutscene 'camera man'.
        ///   NOTE: This will be a value between 0.1, 2.0 (10%-200%)
        /// </summary>
        public static float GetCutsceneCameraMoveRate(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(742);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        ///   Sets the current movement rate factor for the cutscene
        ///   camera man.
        ///   NOTE: You can only set values between 0.1, 2.0 (10%-200%)
        /// </summary>
        public static void SetCutsceneCameraMoveRate(uint oCreature, float fRate)
        {
            NWNXPInvoke.StackPushFloat(fRate);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(743);
        }

        /// <summary>
        ///   Makes a player examine the object oExamine. This causes the examination
        ///   pop-up box to appear for the object specified.
        /// </summary>
        public static void ActionExamine(uint oExamine)
        {
            NWNXPInvoke.StackPushObject(oExamine);
            NWNXPInvoke.CallBuiltIn(738);
        }

        /// <summary>
        ///   Use this to get the item last equipped by a player character in OnPlayerEquipItem..
        /// </summary>
        public static uint GetPCItemLastEquipped()
        {
            NWNXPInvoke.CallBuiltIn(727);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Use this to get the player character who last equipped an item in OnPlayerEquipItem..
        /// </summary>
        public static uint GetPCItemLastEquippedBy()
        {
            NWNXPInvoke.CallBuiltIn(728);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Use this to get the item last unequipped by a player character in OnPlayerEquipItem..
        /// </summary>
        public static uint GetPCItemLastUnequipped()
        {
            NWNXPInvoke.CallBuiltIn(729);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Use this to get the player character who last unequipped an item in OnPlayerUnEquipItem..
        /// </summary>
        public static uint GetPCItemLastUnequippedBy()
        {
            NWNXPInvoke.CallBuiltIn(730);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Send a server message (szMessage) to the oPlayer.
        /// </summary>
        public static void SendMessageToPCByStrRef(uint oPlayer, int nStrRef)
        {
            NWNXPInvoke.StackPushInteger(nStrRef);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(717);
        }

        /// <summary>
        ///   Open's this creature's inventory panel for this player
        ///   - oCreature: creature to view
        ///   - oPlayer: the owner of this creature will see the panel pop up
        ///   * DM's can view any creature's inventory
        ///   * Players can view their own inventory, or that of their henchman, familiar or animal companion
        /// </summary>
        public static void OpenInventory(uint oCreature, uint oPlayer)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(701);
        }

        /// <summary>
        ///   Stores the current camera mode and position so that it can be restored (using
        ///   RestoreCameraFacing())
        /// </summary>
        public static void StoreCameraFacing()
        {
            NWNXPInvoke.CallBuiltIn(702);
        }

        /// <summary>
        ///   Restores the camera mode and position to what they were last time StoreCameraFacing
        ///   was called.  RestoreCameraFacing can only be called once, and must correspond to a
        ///   previous call to StoreCameraFacing.
        /// </summary>
        public static void RestoreCameraFacing()
        {
            NWNXPInvoke.CallBuiltIn(703);
        }

        /// <summary>
        ///   Fades the screen for the given creature/player from black to regular screen
        ///   - oCreature: creature controlled by player that should fade from black
        /// </summary>
        public static void FadeFromBlack(uint oCreature, float fSpeed = FadeSpeed.Medium)
        {
            NWNXPInvoke.StackPushFloat(fSpeed);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(695);
        }

        /// <summary>
        ///   Fades the screen for the given creature/player from regular screen to black
        ///   - oCreature: creature controlled by player that should fade to black
        /// </summary>
        public static void FadeToBlack(uint oCreature, float fSpeed = FadeSpeed.Medium)
        {
            NWNXPInvoke.StackPushFloat(fSpeed);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(696);
        }

        /// <summary>
        ///   Removes any fading or black screen.
        ///   - oCreature: creature controlled by player that should be cleared
        /// </summary>
        public static void StopFade(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(697);
        }

        /// <summary>
        ///   Sets the screen to black.  Can be used in preparation for a fade-in (FadeFromBlack)
        ///   Can be cleared by either doing a FadeFromBlack, or by calling StopFade.
        ///   - oCreature: creature controlled by player that should see black screen
        /// </summary>
        public static void BlackScreen(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(698);
        }

        /// <summary>
        ///   Sets the given creature into cutscene mode.  This prevents the player from
        ///   using the GUI and camera controls.
        ///   - oCreature: creature in a cutscene
        ///   - nInCutscene: TRUE to move them into cutscene, FALSE to remove cutscene mode
        ///   - nLeftClickingEnabled: TRUE to allow the user to interact with the game world using the left mouse button only.
        ///   FALSE to stop the user from interacting with the game world.
        ///   Note: SetCutsceneMode(oPlayer, TRUE) will also make the player 'plot' (unkillable).
        ///   SetCutsceneMode(oPlayer, FALSE) will restore the player's plot flag to what it
        ///   was when SetCutsceneMode(oPlayer, TRUE) was called.
        /// </summary>
        public static void SetCutsceneMode(uint oCreature, bool nInCutscene = true, bool nLeftClickingEnabled = false)
        {
            NWNXPInvoke.StackPushInteger(nLeftClickingEnabled ? 1 : 0);
            NWNXPInvoke.StackPushInteger(nInCutscene ? 1 : 0);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(692);
        }

        /// <summary>
        ///   Gets the last player character to cancel from a cutscene.
        /// </summary>
        public static uint GetLastPCToCancelCutscene()
        {
            NWNXPInvoke.CallBuiltIn(693);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Remove oPlayer from the server.
        ///   You can optionally specify a reason to override the text shown to the player.
        /// </summary>
        public static void BootPC(uint oPlayer, string sReason = "")
        {
            NWNXPInvoke.StackPushString(sReason);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(565);
        }

        /// <summary>
        ///   Spawn in the Death GUI.
        ///   The default (as defined by BioWare) can be spawned in by PopUpGUIPanel, but
        ///   if you want to turn off the "Respawn" or "Wait for Help" buttons, this is the
        ///   function to use.
        ///   - oPC
        ///   - bRespawnButtonEnabled: if this is TRUE, the "Respawn" button will be enabled
        ///   on the Death GUI.
        ///   - bWaitForHelpButtonEnabled: if this is TRUE, the "Wait For Help" button will
        ///   be enabled on the Death GUI (Note: This button will not appear in single player games).
        ///   - nHelpStringReference
        ///   - sHelpString
        /// </summary>
        public static void PopUpDeathGUIPanel(uint oPC, bool bRespawnButtonEnabled = true,
            bool bWaitForHelpButtonEnabled = true, int nHelpStringReference = 0, string sHelpString = "")
        {
            NWNXPInvoke.StackPushString(sHelpString);
            NWNXPInvoke.StackPushInteger(nHelpStringReference);
            NWNXPInvoke.StackPushInteger(bWaitForHelpButtonEnabled ? 1 : 0);
            NWNXPInvoke.StackPushInteger(bRespawnButtonEnabled ? 1 : 0);
            NWNXPInvoke.StackPushObject(oPC);
            NWNXPInvoke.CallBuiltIn(554);
        }

        /// <summary>
        ///   Get the first PC in the player list.
        ///   This resets the position in the player list for GetNextPC().
        /// </summary>
        public static uint GetFirstPC()
        {
            NWNXPInvoke.CallBuiltIn(548);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the next PC in the player list.
        ///   This picks up where the last GetFirstPC() or GetNextPC() left off.
        /// </summary>
        public static uint GetNextPC()
        {
            NWNXPInvoke.CallBuiltIn(549);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the last PC that levelled up.
        /// </summary>
        public static uint GetPCLevellingUp()
        {
            NWNXPInvoke.CallBuiltIn(542);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Set the camera mode for oPlayer.
        ///   - oPlayer
        ///   - nCameraMode: CAMERA_MODE_*
        public static void SetCameraMode(uint oPlayer, int nCameraMode)
        {
            NWNXPInvoke.StackPushInteger(nCameraMode);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(504);
        }

        /// <summary>
        ///   Use this in an OnPlayerDying module script to get the last player who is dying.
        /// </summary>
        public static uint GetLastPlayerDying()
        {
            NWNXPInvoke.CallBuiltIn(410);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Spawn a GUI panel for the client that controls oPC.
        ///   Will force show panels disabled with SetGuiPanelDisabled()
        ///   - oPC
        ///   - nGUIPanel: GUI_PANEL_*, except GUI_PANEL_COMPASS / GUI_PANEL_LEVELUP / GUI_PANEL_GOLD_* / GUI_PANEL_EXAMINE_*
        public static void PopUpGUIPanel(uint oPC, GuiPanel nGUIPanel)
        {
            NWNXPInvoke.StackPushInteger((int)nGUIPanel);
            NWNXPInvoke.StackPushObject(oPC);
            NWNXPInvoke.CallBuiltIn(388);
        }

        /// <summary>
        /// Returns the build number of oPlayer (i.e. 8193).
        /// Returns 0 if the given object isn't a player or did not advertise their build info.
        /// </summary>
        public static int GetPlayerBuildVersionMajor(uint oPlayer)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(904);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Returns the patch revision of oPlayer (i.e. 8).
        /// Returns 0 if the given object isn't a player or did not advertise their build info.
        /// </summary>
        public static int GetPlayerBuildVersionMinor(uint oPlayer)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(905);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Returns TRUE if the given player-controlled creature has DM privileges
        /// gained through a player login (as opposed to the DM client).
        /// Note: GetIsDM() also returns TRUE for player creature DMs.
        /// </summary>
        public static int GetIsPlayerDM(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(918);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Gets the player that last triggered the module OnPlayerGuiEvent event.
        /// </summary>
        public static uint GetLastGuiEventPlayer()
        {
            NWNXPInvoke.CallBuiltIn(960);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        /// Gets the last triggered GUIEVENT_* in the module OnPlayerGuiEvent event.
        /// </summary>
        public static GuiEventType GetLastGuiEventType()
        {
            NWNXPInvoke.CallBuiltIn(961);
            return (GuiEventType)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Gets an optional integer of specific gui events in the module OnPlayerGuiEvent event.
        /// </summary>
        public static int GetLastGuiEventInteger()
        {
            NWNXPInvoke.CallBuiltIn(962);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Gets an optional object of specific gui events in the module OnPlayerGuiEvent event.
        /// </summary>
        public static uint GetLastGuiEventObject()
        {
            NWNXPInvoke.CallBuiltIn(963);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        /// Disable a gui panel for the client that controls oPlayer.
        /// Notes: Will close the gui panel if currently open, except GUI_PANEL_LEVELUP / GUI_PANEL_GOLD_*
        /// </summary>
        public static void SetGuiPanelDisabled(uint oPlayer, GuiPanel nGuiPanel, bool bDisabled, uint oTarget = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushInteger(bDisabled ? 1 : 0);
            NWNXPInvoke.StackPushInteger((int)nGuiPanel);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(964);
        }

        /// <summary>
        /// Gets the ID (1..8) of the last tile action performed in OnPlayerTileAction
        /// </summary>
        public static int GetLastTileActionId()
        {
            NWNXPInvoke.CallBuiltIn(965);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Gets the target position in the module OnPlayerTileAction event.
        /// </summary>
        public static Vector3 GetLastTileActionPosition()
        {
            NWNXPInvoke.CallBuiltIn(966);
            return NWNXPInvoke.StackPopVector();
        }

        /// <summary>
        /// Gets the player object that triggered the OnPlayerTileAction event.
        /// </summary>
        public static uint GetLastPlayerToDoTileAction()
        {
            NWNXPInvoke.CallBuiltIn(967);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        /// Gets a device property/capability as advertised by the client.
        /// sProperty is one of PLAYER_DEVICE_PROPERTY_xxx.
        /// Returns -1 if
        /// - the property was never set by the client,
        /// - the actual value is -1,
        /// - the player is running an older build that does not advertise device properties,
        /// - the player has disabled sending device properties (Options->Game->Privacy).
        /// </summary>
        public static int GetPlayerDeviceProperty(uint oPlayer, string sProperty)
        {
            NWNXPInvoke.StackPushString(sProperty);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1004);

            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Returns the LANGUAGE_xx code of the given player, or -1 if unavailable.
        /// </summary>
        public static PlayerLanguageType GetPlayerLanguage(uint oPlayer)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1005);

            return (PlayerLanguageType)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Returns one of PLAYER_DEVICE_PLATFORM_xxx, or 0 if unavailable.
        /// </summary>
        public static PlayerDevicePlatformType GetPlayerDevicePlatform(uint oPlayer)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1006);

            return (PlayerDevicePlatformType)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Returns the patch postfix of oPlayer (i.e. the 29 out of "87.8193.35-29 abcdef01").
        /// Returns 0 if the given object isn't a player or did not advertise their build info, or the
        /// player version is old enough not to send this bit of build info to the server.
        /// </summary>
        public static int GetPlayerBuildVersionPostfix(uint oPlayer)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1093);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Returns the patch commit sha1 of oPlayer (i.e. the "abcdef01" out of "87.8193.35-29 abcdef01").
        /// Returns "" if the given object isn't a player or did not advertise their build info, or the
        /// player version is old enough not to send this bit of build info to the server.
        /// </summary>
        public static string GetPlayerBuildVersionCommitSha1(uint oPlayer)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1094);
            return NWNXPInvoke.StackPopString();
        }

    }
}