using System.Numerics;
using NWN.Xenomech.Core.Interop;
using NWN.Xenomech.Core.NWNX.Enum;
using NWN.Xenomech.Core.NWScript.Enum;
using NWN.Xenomech.Core.NWScript.Enum.VisualEffect;

namespace NWN.Xenomech.Core.NWNX
{
    public class PlayerPlugin
    {
        private const string PLUGIN_NAME = "NWNX_Player";
        // Force display placeable examine window for player
        // If used on a placeable in a different area than the player, the portrait will not be shown.
        public static void ForcePlaceableExamineWindow(uint player, uint placeable)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "ForcePlaceableExamineWindow");
            NWNXPInvoke.NWNXPushObject(placeable);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Force opens the target object's inventory for the player.
        public static void ForcePlaceableInventoryWindow(uint player, uint placeable)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "ForcePlaceableInventoryWindow");
            NWNXPInvoke.NWNXPushObject(placeable);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Starts displaying a timing bar.
        public static void StartGuiTimingBar(uint player, float seconds, string script = "",
            TimingBarType type = TimingBarType.Custom)
        {
            if (GetLocalInt(player, "NWNX_PLAYER_GUI_TIMING_ACTIVE") == 1) return;
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "StartGuiTimingBar");
            NWNXPInvoke.NWNXPushInt((int)type);
            NWNXPInvoke.NWNXPushFloat(seconds);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();

            var id = GetLocalInt(player, "NWNX_PLAYER_GUI_TIMING_ID") + 1;
            SetLocalInt(player, "NWNX_PLAYER_GUI_TIMING_ACTIVE", id);
            SetLocalInt(player, "NWNX_PLAYER_GUI_TIMING_ID", id);

            DelayCommand(seconds, () => StopGuiTimingBar(player, script, id));
        }

        // Stops displaying a timing bar.
        private static void StopGuiTimingBar(uint creature, string script, int id)
        {
            var activeId = GetLocalInt(creature, "NWNX_PLAYER_GUI_TIMING_ACTIVE");
            if (activeId == 0) return;
            if (id != -1 && id != activeId) return;
            DeleteLocalInt(creature, "NWNX_PLAYER_GUI_TIMING_ACTIVE");
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "StopGuiTimingBar");
            NWNXPInvoke.NWNXPushObject(creature);
            NWNXPInvoke.NWNXCallFunction();
            if (!string.IsNullOrWhiteSpace(script)) ExecuteScript(script, creature);
        }

        // Stops displaying a timing bar.
        public static void StopGuiTimingBar(uint player, string script = "")
        {
            StopGuiTimingBar(player, script, -1);
        }

        // Sets whether the player should always walk when given movement commands.
        public static void SetAlwaysWalk(uint player, bool walk)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetAlwaysWalk");
            NWNXPInvoke.NWNXPushInt(walk ? 1 : 0);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Gets the player's quickbar slot info
        public static QuickBarSlot GetQuickBarSlot(uint player, int slot)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetQuickBarSlot");
            var qbs = new QuickBarSlot();
            NWNXPInvoke.NWNXPushInt(slot);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
            qbs.Associate = NWNXPInvoke.NWNXPopObject();
            qbs.AssociateType = NWNXPInvoke.NWNXPopInt();
            qbs.DomainLevel = NWNXPInvoke.NWNXPopInt();
            qbs.MetaType = NWNXPInvoke.NWNXPopInt();
            qbs.INTParam1 = NWNXPInvoke.NWNXPopInt();
            qbs.ToolTip = NWNXPInvoke.NWNXPopString();
            qbs.CommandLine = NWNXPInvoke.NWNXPopString();
            qbs.CommandLabel = NWNXPInvoke.NWNXPopString();
            qbs.Resref = NWNXPInvoke.NWNXPopString();
            qbs.MultiClass = NWNXPInvoke.NWNXPopInt();
            qbs.ObjectType = (QuickBarSlotType)NWNXPInvoke.NWNXPopInt();
            qbs.SecondaryItem = NWNXPInvoke.NWNXPopObject();
            qbs.Item = NWNXPInvoke.NWNXPopObject();
            return qbs;
        }

        // Sets a player's quickbar slot
        public static void SetQuickBarSlot(uint player, int slot, QuickBarSlot qbs)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetQuickBarSlot");
            NWNXPInvoke.NWNXPushObject(qbs.Item ?? OBJECT_INVALID);
            NWNXPInvoke.NWNXPushObject(qbs.SecondaryItem ?? OBJECT_INVALID);
            NWNXPInvoke.NWNXPushInt((int)qbs.ObjectType);
            NWNXPInvoke.NWNXPushInt(qbs.MultiClass);
            NWNXPInvoke.NWNXPushString(qbs.Resref!);
            NWNXPInvoke.NWNXPushString(qbs.CommandLabel!);
            NWNXPInvoke.NWNXPushString(qbs.CommandLine!);
            NWNXPInvoke.NWNXPushString(qbs.ToolTip!);
            NWNXPInvoke.NWNXPushInt(qbs.INTParam1);
            NWNXPInvoke.NWNXPushInt(qbs.MetaType);
            NWNXPInvoke.NWNXPushInt(qbs.DomainLevel);
            NWNXPInvoke.NWNXPushInt(qbs.AssociateType);
            NWNXPInvoke.NWNXPushObject(qbs.Associate ?? OBJECT_INVALID);
            NWNXPInvoke.NWNXPushInt(slot);
            NWNXPInvoke.NWNXPushObject(player!);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Get the name of the .bic file associated with the player's character.
        public static string GetBicFileName(uint player)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetBicFileName");
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopString();
        }

        // Plays the VFX at the target position in current area for the given player only
        public static void ShowVisualEffect(uint player, int effectId, float scale, Vector3 position, Vector3 translate, Vector3 rotate)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "ShowVisualEffect");

            NWNXPInvoke.NWNXPushFloat(rotate.X);
            NWNXPInvoke.NWNXPushFloat(rotate.Y);
            NWNXPInvoke.NWNXPushFloat(rotate.Z);
            NWNXPInvoke.NWNXPushFloat(translate.X);
            NWNXPInvoke.NWNXPushFloat(translate.Y);
            NWNXPInvoke.NWNXPushFloat(translate.Z);
            NWNXPInvoke.NWNXPushFloat(scale);
            NWNXPInvoke.NWNXPushFloat(position.X);
            NWNXPInvoke.NWNXPushFloat(position.Y);
            NWNXPInvoke.NWNXPushFloat(position.Z);
            NWNXPInvoke.NWNXPushInt(effectId);
            NWNXPInvoke.NWNXPushObject(player);

            NWNXPInvoke.NWNXCallFunction();
        }

        // Changes the nighttime music track for the given player only
        public static void MusicBackgroundChangeTimeToggle(uint player, int track, bool night)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "ChangeBackgroundMusic");
            NWNXPInvoke.NWNXPushInt(track);
            NWNXPInvoke.NWNXPushInt(night ? 1 : 0); // bool day = false
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Toggle the background music for the given player only
        public static void MusicBackgroundToggle(uint player, bool on)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "PlayBackgroundMusic");
            NWNXPInvoke.NWNXPushInt(on ? 1 : 0); // bool play = false
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Changes the battle music track for the given player only
        public static void MusicBattleChange(uint player, int track)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "ChangeBattleMusic");
            NWNXPInvoke.NWNXPushInt(track);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Toggle the background music for the given player only
        public static void MusicBattleToggle(uint player, bool on)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "PlayBattleMusic");
            NWNXPInvoke.NWNXPushInt(on ? 1 : 0);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Play a sound at the location of target for the given player only
        public static void PlaySound(uint player, string sound, uint target)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "PlaySound");
            NWNXPInvoke.NWNXPushObject(target);
            NWNXPInvoke.NWNXPushString(sound);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Toggle a placeable's usable flag for the given player only
        public static void SetPlaceableUseable(uint player, uint placeable, bool usable)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetPlaceableUsable");
            NWNXPInvoke.NWNXPushInt(usable ? 1 : 0);
            NWNXPInvoke.NWNXPushObject(placeable);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Override player's rest duration
        public static void SetRestDuration(uint player, int duration)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetRestDuration");
            NWNXPInvoke.NWNXPushInt(duration);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Apply visualeffect to target that only player can see
        public static void ApplyInstantVisualEffectToObject(uint player, uint target, VisualEffect visualEffect)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "ApplyInstantVisualEffectToObject");
            NWNXPInvoke.NWNXPushInt((int)visualEffect);
            NWNXPInvoke.NWNXPushObject(target);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Refreshes the players character sheet
        public static void UpdateCharacterSheet(uint player)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "UpdateCharacterSheet");
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Allows player to open target's inventory
        public static void OpenInventory(uint player, uint target, bool open = true)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "OpenInventory");
            NWNXPInvoke.NWNXPushInt(open ? 1 : 0);
            NWNXPInvoke.NWNXPushObject(target);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Get player's area exploration state
        public static string GetAreaExplorationState(uint player, uint area)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetAreaExplorationState");
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopString();
        }

        // Set player's area exploration state (str is an encoded string obtained with NWNX_Player_GetAreaExplorationState)
        public static void SetAreaExplorationState(uint player, uint area, string encodedString)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetAreaExplorationState");
            NWNXPInvoke.NWNXPushString(encodedString);
            NWNXPInvoke.NWNXPushObject(area);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Override oPlayer's rest animation to nAnimation
        public static void SetRestAnimation(uint player, int animation)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetRestAnimation");
            NWNXPInvoke.NWNXPushInt(animation);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Override a visual transform on the given object that only oPlayer will see.
        public static void SetObjectVisualTransformOverride(uint player, uint target, int transform, float valueToApply)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetObjectVisualTransformOverride");
            NWNXPInvoke.NWNXPushFloat(valueToApply);
            NWNXPInvoke.NWNXPushInt(transform);
            NWNXPInvoke.NWNXPushObject(target);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Apply a looping visualeffect to target that only player can see
        public static void ApplyLoopingVisualEffectToObject(uint player, uint target, VisualEffect visualEffect)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "ApplyLoopingVisualEffectToObject");
            NWNXPInvoke.NWNXPushInt((int)visualEffect);
            NWNXPInvoke.NWNXPushObject(target);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Override the name of placeable for player only
        // "" to clear the override
        public static void SetPlaceableNameOverride(uint player, uint placeable, string name)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetPlaceableNameOverride");
            NWNXPInvoke.NWNXPushString(name);
            NWNXPInvoke.NWNXPushObject(placeable);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Gets whether a quest has been completed by a player
        // Returns -1 if they don't have the journal entry
        public static int GetQuestCompleted(uint player, string questName)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetQuestCompleted");
            NWNXPInvoke.NWNXPushString(questName);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt();
        }

        // Set persistent location for a player
        public static void SetPersistentLocation(string cdKeyOrCommunityName, string bicFileName, uint wayPoint,
            bool firstConnect = true)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetPersistentLocation");
            NWNXPInvoke.NWNXPushInt(firstConnect ? 1 : 0);
            NWNXPInvoke.NWNXPushObject(wayPoint);
            NWNXPInvoke.NWNXPushString(bicFileName);
            NWNXPInvoke.NWNXPushString(cdKeyOrCommunityName);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Force an item name to be updated.
        public static void UpdateItemName(uint player, uint item)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "UpdateItemName");
            NWNXPInvoke.NWNXPushObject(item);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Possesses a creature by temporarily making them a familiar
        public static bool PossessCreature(uint possessor, uint possessed, bool mindImmune = true,
            bool createDefaultQB = false)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "PossessCreature");
            NWNXPInvoke.NWNXPushInt(createDefaultQB ? 1 : 0);
            NWNXPInvoke.NWNXPushInt(mindImmune ? 1 : 0);
            NWNXPInvoke.NWNXPushObject(possessed);
            NWNXPInvoke.NWNXPushObject(possessor);
            NWNXPInvoke.NWNXCallFunction();
            return Convert.ToBoolean(NWNXPInvoke.NWNXPopInt());
        }

        // Returns the platform ID of the given player (NWNX_PLAYER_PLATFORM_*)
        public static int GetPlatformId(uint player)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetPlatformId");
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt();
        }

        // Returns the game language of the given player (uses NWNX_DIALOG_LANGUAGE_*)
        public static int GetLanguage(uint player)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetLanguage");
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt();
        }

        // Override sOldResName with sNewResName of nResType for oPlayer.
        public static void SetResManOverride(uint player, int resType, string resName, string newResName)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetResManOverride");
            NWNXPInvoke.NWNXPushString(newResName);
            NWNXPInvoke.NWNXPushString(resName);
            NWNXPInvoke.NWNXPushInt(resType);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Toggle oPlayer's PlayerDM status.
        public static void ToggleDM(uint oPlayer, bool bIsDM)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "ToggleDM");

            NWNXPInvoke.NWNXPushInt(bIsDM ? 1 : 0);
            NWNXPInvoke.NWNXPushObject(oPlayer);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Override the mouse cursor of oObject for oPlayer only
        public static void SetObjectMouseCursorOverride(uint oPlayer, uint oObject, MouseCursor nCursor)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetObjectMouseCursorOverride");

            NWNXPInvoke.NWNXPushInt((int)nCursor);
            NWNXPInvoke.NWNXPushObject(oObject);
            NWNXPInvoke.NWNXPushObject(oPlayer);

            NWNXPInvoke.NWNXCallFunction();
        }

        // Override the hilite color of oObject for oPlayer only
        public static void SetObjectHiliteColorOverride(uint oPlayer, uint oObject, int nColor)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetObjectHiliteColorOverride");

            NWNXPInvoke.NWNXPushInt(nColor);
            NWNXPInvoke.NWNXPushObject(oObject);
            NWNXPInvoke.NWNXPushObject(oPlayer);

            NWNXPInvoke.NWNXCallFunction();
        }

        // Remove effects with sEffectTag from oPlayer's TURD
        public static void RemoveEffectFromTURD(uint oPlayer, string sEffectTag)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "RemoveEffectFromTURD");
            NWNXPInvoke.NWNXPushString(sEffectTag);
            NWNXPInvoke.NWNXPushObject(oPlayer);
            NWNXPInvoke.NWNXCallFunction();
        }

        // Set the location oPlayer will spawn when logging in to the server.
        public static void SetSpawnLocation(uint oPlayer, Location locSpawn)
        {
            var vPosition = GetPositionFromLocation(locSpawn);

            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetSpawnLocation");
            NWNXPInvoke.NWNXPushFloat(GetFacingFromLocation(locSpawn));
            NWNXPInvoke.NWNXPushFloat(vPosition.Z);
            NWNXPInvoke.NWNXPushFloat(vPosition.Y);
            NWNXPInvoke.NWNXPushFloat(vPosition.X);
            NWNXPInvoke.NWNXPushObject(GetAreaFromLocation(locSpawn));
            NWNXPInvoke.NWNXPushObject(oPlayer);

            NWNXPInvoke.NWNXCallFunction();
        }

        public static void SetCustomToken(uint oPlayer, int nCustomTokenNumber, string sTokenValue)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetCustomToken");
            NWNXPInvoke.NWNXPushString(sTokenValue);
            NWNXPInvoke.NWNXPushInt(nCustomTokenNumber);
            NWNXPInvoke.NWNXPushObject(oPlayer);

            NWNXPInvoke.NWNXCallFunction();
        }

        public static void SetCreatureNameOverride(uint oPlayer, uint oCreature, string sName)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetCreatureNameOverride");

            NWNXPInvoke.NWNXPushString(sName);
            NWNXPInvoke.NWNXPushObject(oCreature);
            NWNXPInvoke.NWNXPushObject(oPlayer);

            NWNXPInvoke.NWNXCallFunction();
        }

        // Display floaty text above oCreature for oPlayer only.
        public static void FloatingTextStringOnCreature(uint oPlayer, uint oCreature, string sText)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "FloatingTextStringOnCreature");

            NWNXPInvoke.NWNXPushString(sText);
            NWNXPInvoke.NWNXPushObject(oCreature);
            NWNXPInvoke.NWNXPushObject(oPlayer);

            NWNXPInvoke.NWNXCallFunction();
        }

        // Give a custom journal entry to oPlayer.
        public static int AddCustomJournalEntry(uint player, JournalEntry journalEntry, bool isSilentUpdate = false)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "AddCustomJournalEntry");
            NWNXPInvoke.NWNXPushInt(isSilentUpdate ? 1 : 0);
            NWNXPInvoke.NWNXPushInt(journalEntry.TimeOfDay);
            NWNXPInvoke.NWNXPushInt(journalEntry.CalendarDay);
            NWNXPInvoke.NWNXPushInt(journalEntry.Updated);
            NWNXPInvoke.NWNXPushInt(journalEntry.IsQuestDisplayed ? 1 : 0);
            NWNXPInvoke.NWNXPushInt(journalEntry.IsQuestCompleted ? 1 : 0);
            NWNXPInvoke.NWNXPushInt(journalEntry.Priority);
            NWNXPInvoke.NWNXPushInt(journalEntry.State);
            NWNXPInvoke.NWNXPushString(journalEntry.Tag);
            NWNXPInvoke.NWNXPushString(journalEntry.Text);
            NWNXPInvoke.NWNXPushString(journalEntry.Name);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();
            return NWNXPInvoke.NWNXPopInt();
        }

        // Returns a struct containing a journal entry
        public static JournalEntry GetJournalEntry(uint player, string questTag)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "GetJournalEntry");
            var entry = new JournalEntry();

            NWNXPInvoke.NWNXPushString(questTag);
            NWNXPInvoke.NWNXPushObject(player);
            NWNXPInvoke.NWNXCallFunction();

            entry.Updated = NWNXPInvoke.NWNXPopInt();
            if (entry.Updated == -1)
            {
                return entry;
            }

            entry.IsQuestDisplayed = NWNXPInvoke.NWNXPopInt() == 1;
            entry.IsQuestCompleted = NWNXPInvoke.NWNXPopInt() == 1;
            entry.Priority = NWNXPInvoke.NWNXPopInt();
            entry.State = NWNXPInvoke.NWNXPopInt();
            entry.TimeOfDay = NWNXPInvoke.NWNXPopInt();
            entry.CalendarDay = NWNXPInvoke.NWNXPopInt();
            entry.Name = NWNXPInvoke.NWNXPopString();
            entry.Text = NWNXPInvoke.NWNXPopString();
            entry.Tag = questTag;
            return entry;
        }

        public static void CloseStore(uint oPlayer)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "CloseStore");
            NWNXPInvoke.NWNXPushObject(oPlayer);
            NWNXPInvoke.NWNXCallFunction();
        }

        public static void SetTlkOverride(uint oPlayer, int nStrRef, string sOverride, bool bRestoreGlobal = true)
        {
            NWNXPInvoke.NWNXSetFunction(PLUGIN_NAME, "SetTlkOverride");
            NWNXPInvoke.NWNXPushInt(bRestoreGlobal ? 1 : 0);
            NWNXPInvoke.NWNXPushString(sOverride);
            NWNXPInvoke.NWNXPushInt(nStrRef);
            NWNXPInvoke.NWNXPushObject(oPlayer);
            NWNXPInvoke.NWNXCallFunction();
        }

    }
}