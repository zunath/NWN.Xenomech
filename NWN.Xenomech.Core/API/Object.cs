using System.Numerics;
using NWN.Xenomech.Core.API.Enum;
using NWN.Xenomech.Core.Interop;

namespace NWN.Xenomech.Core.API
{
    public partial class NWScript
    {/// <summary>
     ///   Sets a new tag for oObject.
     ///   Will do nothing for invalid objects or the module object.
     ///   Note: Care needs to be taken with this function.
     ///   Changing the tag for creature with waypoints will make them stop walking them.
     ///   Changing waypoint, door or trigger tags will break their area transitions.
     /// </summary>
        public static void SetTag(uint oObject, string sNewTag)
        {
            NWNXPInvoke.StackPushString(sNewTag);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(848);
        }

        /// <summary>
        ///   Get the last object that default clicked (left clicked) on the placeable object
        ///   that is calling this function.
        ///   Should only be called from a placeables OnClick event.
        ///   * Returns OBJECT_INVALID if it is called by something other than a placeable.
        /// </summary>
        public static uint GetPlaceableLastClickedBy()
        {
            NWNXPInvoke.CallBuiltIn(826);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Set the name of oObject.
        ///   - oObject: the object for which you are changing the name (a creature, placeable, item, or door).
        ///   - sNewName: the new name that the object will use.
        ///   Note: SetName() does not work on player objects.
        ///   Setting an object's name to "" will make the object
        ///   revert to using the name it had originally before any
        ///   SetName() calls were made on the object.
        /// </summary>
        public static void SetName(uint oObject, string sNewName = "")
        {
            NWNXPInvoke.StackPushString(sNewName);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(830);
        }

        /// <summary>
        ///   Get the PortraitId of oTarget.
        ///   - oTarget: the object for which you are getting the portrait Id.
        ///   Returns: The Portrait Id number being used for the object oTarget.
        ///   The Portrait Id refers to the row number of the Portraits.2da
        ///   that this portrait is from.
        ///   If a custom portrait is being used, oTarget is a player object,
        ///   or on an error returns PORTRAIT_INVALID. In these instances
        ///   try using GetPortraitResRef() instead.
        /// </summary>
        public static int GetPortraitId(uint oTarget = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(831);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Change the portrait of oTarget to use the Portrait Id specified.
        ///   - oTarget: the object for which you are changing the portrait.
        ///   - nPortraitId: The Id of the new portrait to use.
        ///   nPortraitId refers to a row in the Portraits.2da
        ///   Note: Not all portrait Ids are suitable for use with all object types.
        ///   Setting the portrait Id will also cause the portrait ResRef
        ///   to be set to the appropriate portrait ResRef for the Id specified.
        /// </summary>
        public static void SetPortraitId(uint oTarget, int nPortraitId)
        {
            NWNXPInvoke.StackPushInteger(nPortraitId);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(832);
        }

        /// <summary>
        ///   Get the Portrait ResRef of oTarget.
        ///   - oTarget: the object for which you are getting the portrait ResRef.
        ///   Returns: The Portrait ResRef being used for the object oTarget.
        ///   The Portrait ResRef will not include a trailing size letter.
        /// </summary>
        public static string GetPortraitResRef(uint oTarget = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(833);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Change the portrait of oTarget to use the Portrait ResRef specified.
        ///   - oTarget: the object for which you are changing the portrait.
        ///   - sPortraitResRef: The ResRef of the new portrait to use.
        ///   The ResRef should not include any trailing size letter ( e.g. po_el_f_09_ ).
        ///   Note: Not all portrait ResRefs are suitable for use with all object types.
        ///   Setting the portrait ResRef will also cause the portrait Id
        ///   to be set to PORTRAIT_INVALID.
        /// </summary>
        public static void SetPortraitResRef(uint oTarget, string sPortraitResRef)
        {
            NWNXPInvoke.StackPushString(sPortraitResRef);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(834);
        }

        /// <summary>
        /// Set oTarget's useable object status.
        /// Note: Only works on non-static placeables, creatures, doors and items.
        /// On items, it affects interactivity when they're on the ground, and not useability in inventory.
        /// </summary>
        public static void SetUseableFlag(uint oPlaceable, bool nUseable)
        {
            NWNXPInvoke.StackPushInteger(nUseable ? 1 : 0);
            NWNXPInvoke.StackPushObject(oPlaceable);
            NWNXPInvoke.CallBuiltIn(835);
        }

        /// <summary>
        ///   Get the description of oObject.
        ///   - oObject: the object from which you are obtaining the description.
        ///   Can be a creature, item, placeable, door, trigger or module object.
        ///   - bOriginalDescription:  if set to true any new description specified via a SetDescription scripting command
        ///   is ignored and the original object's description is returned instead.
        ///   - bIdentified: If oObject is an item, setting this to TRUE will return the identified description,
        ///   setting this to FALSE will return the unidentified description. This flag has no
        ///   effect on objects other than items.
        /// </summary>
        public static string GetDescription(uint oObject, bool bOriginalDescription = false,
            bool bIdentifiedDescription = true)
        {
            NWNXPInvoke.StackPushInteger(bIdentifiedDescription ? 1 : 0);
            NWNXPInvoke.StackPushInteger(bOriginalDescription ? 1 : 0);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(836);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Set the description of oObject.
        ///   - oObject: the object for which you are changing the description
        ///   Can be a creature, placeable, item, door, or trigger.
        ///   - sNewDescription: the new description that the object will use.
        ///   - bIdentified: If oObject is an item, setting this to TRUE will set the identified description,
        ///   setting this to FALSE will set the unidentified description. This flag has no
        ///   effect on objects other than items.
        ///   Note: Setting an object's description to "" will make the object
        ///   revert to using the description it originally had before any
        ///   SetDescription() calls were made on the object.
        /// </summary>
        public static void SetDescription(uint oObject, string sNewDescription = "", bool bIdentifiedDescription = true)
        {
            NWNXPInvoke.StackPushInteger(bIdentifiedDescription ? 1 : 0);
            NWNXPInvoke.StackPushString(sNewDescription);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(837);
        }

        /// <summary>
        ///   Get the Color of oObject from the color channel specified.
        ///   - oObject: the object from which you are obtaining the color.
        ///   Can be a creature that has color information (i.e. the playable races).
        ///   - nColorChannel: The color channel that you want to get the color value of.
        ///   COLOR_CHANNEL_SKIN
        ///   COLOR_CHANNEL_HAIR
        ///   COLOR_CHANNEL_TATTOO_1
        ///   COLOR_CHANNEL_TATTOO_2
        ///   * Returns -1 on error.
        /// </summary>
        public static int GetColor(uint oObject, ColorChannel nColorChannel)
        {
            NWNXPInvoke.StackPushInteger((int)nColorChannel);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(843);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Set the color channel of oObject to the color specified.
        ///   - oObject: the object for which you are changing the color.
        ///   Can be a creature that has color information (i.e. the playable races).
        ///   - nColorChannel: The color channel that you want to set the color value of.
        ///   COLOR_CHANNEL_SKIN
        ///   COLOR_CHANNEL_HAIR
        ///   COLOR_CHANNEL_TATTOO_1
        ///   COLOR_CHANNEL_TATTOO_2
        ///   - nColorValue: The color you want to set (0-175).
        /// </summary>
        public static void SetColor(uint oObject, ColorChannel nColorChannel, int nColorValue)
        {
            NWNXPInvoke.StackPushInteger(nColorValue);
            NWNXPInvoke.StackPushInteger((int)nColorChannel);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(844);
        }

        /// <summary>
        ///   Get the feedback message that will be displayed when trying to unlock the object oObject.
        ///   - oObject: a door or placeable.
        ///   Returns an empty string "" on an error or if the game's default feedback message is being used
        /// </summary>
        public static string GetKeyRequiredFeedback(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(819);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Set the feedback message that is displayed when trying to unlock the object oObject.
        ///   This will only have an effect if the object is set to
        ///   "Key required to unlock or lock" either in the toolset
        ///   or by using the scripting command SetLockKeyRequired().
        ///   - oObject: a door or placeable.
        ///   - sFeedbackMessage: the string to be displayed in the player's text window.
        ///   to use the game's default message, set sFeedbackMessage to ""
        /// </summary>
        public static void SetKeyRequiredFeedback(uint oObject, string sFeedbackMessage)
        {
            NWNXPInvoke.StackPushString(sFeedbackMessage);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(820);
        }

        /// <summary>
        ///   Locks the player's camera pitch to its current pitch setting,
        ///   or unlocks the player's camera pitch.
        ///   Stops the player from tilting their camera angle.
        ///   - oPlayer: A player object.
        ///   - bLocked: TRUE/FALSE.
        /// </summary>
        public static void LockCameraPitch(uint oPlayer, bool bLocked = true)
        {
            NWNXPInvoke.StackPushInteger(bLocked ? 1 : 0);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(823);
        }

        /// <summary>
        ///   Locks the player's camera distance to its current distance setting,
        ///   or unlocks the player's camera distance.
        ///   Stops the player from being able to zoom in/out the camera.
        ///   - oPlayer: A player object.
        ///   - bLocked: TRUE/FALSE.
        /// </summary>
        public static void LockCameraDistance(uint oPlayer, bool bLocked = true)
        {
            NWNXPInvoke.StackPushInteger(bLocked ? 1 : 0);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(824);
        }

        /// <summary>
        ///   Locks the player's camera direction to its current direction,
        ///   or unlocks the player's camera direction to enable it to move
        ///   freely again.
        ///   Stops the player from being able to rotate the camera direction.
        ///   - oPlayer: A player object.
        ///   - bLocked: TRUE/FALSE.
        /// </summary>
        public static void LockCameraDirection(uint oPlayer, bool bLocked = true)
        {
            NWNXPInvoke.StackPushInteger(bLocked ? 1 : 0);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(825);
        }

        /// <summary>
        ///   returns the Hardness of a Door or Placeable object.
        ///   - oObject: a door or placeable object.
        ///   returns -1 on an error or if used on an object that is
        ///   neither a door nor a placeable object.
        /// </summary>
        public static int GetHardness(uint oObject = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(796);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Sets the Hardness of a Door or Placeable object.
        ///   - nHardness: must be between 0 and 250.
        ///   - oObject: a door or placeable object.
        ///   Does nothing if used on an object that is neither
        ///   a door nor a placeable.
        /// </summary>
        public static void SetHardness(int nHardness, uint oObject = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.StackPushInteger(nHardness);
            NWNXPInvoke.CallBuiltIn(797);
        }

        /// <summary>
        ///   When set the object can not be opened unless the
        ///   opener possesses the required key. The key tag required
        ///   can be specified either in the toolset, or by using
        ///   the SetLockKeyTag() scripting command.
        ///   - oObject: a door, or placeable.
        ///   - nKeyRequired: TRUE/FALSE
        /// </summary>
        public static void SetLockKeyRequired(uint oObject, bool nKeyRequired = true)
        {
            NWNXPInvoke.StackPushInteger(nKeyRequired ? 1 : 0);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(798);
        }

        /// <summary>
        ///   Set the key tag required to open object oObject.
        ///   This will only have an effect if the object is set to
        ///   "Key required to unlock or lock" either in the toolset
        ///   or by using the scripting command SetLockKeyRequired().
        ///   - oObject: a door, placeable or trigger.
        ///   - sNewKeyTag: the key tag required to open the locked object.
        /// </summary>
        public static void SetLockKeyTag(uint oObject, string sNewKeyTag)
        {
            NWNXPInvoke.StackPushString(sNewKeyTag);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(799);
        }

        /// <summary>
        ///   Sets whether or not the object can be locked.
        ///   - oObject: a door or placeable.
        ///   - nLockable: TRUE/FALSE
        /// </summary>
        public static void SetLockLockable(uint oObject, bool nLockable = true)
        {
            NWNXPInvoke.StackPushInteger(nLockable ? 1 : 0);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(800);
        }

        /// <summary>
        ///   Sets the DC for unlocking the object.
        ///   - oObject: a door or placeable object.
        ///   - nNewUnlockDC: must be between 0 and 250.
        /// </summary>
        public static void SetLockUnlockDC(uint oObject, int nNewUnlockDC)
        {
            NWNXPInvoke.StackPushInteger(nNewUnlockDC);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(801);
        }

        /// <summary>
        ///   Sets the DC for locking the object.
        ///   - oObject: a door or placeable object.
        ///   - nNewLockDC: must be between 0 and 250.
        /// </summary>
        public static void SetLockLockDC(uint oObject, int nNewLockDC)
        {
            NWNXPInvoke.StackPushInteger(nNewLockDC);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(802);
        }

        /// <summary>
        ///   Set the Will saving throw value of the Door or Placeable object oObject.
        ///   - oObject: a door or placeable object.
        ///   - nWillSave: must be between 0 and 250.
        /// </summary>
        public static void SetWillSavingThrow(uint oObject, int nWillSave)
        {
            NWNXPInvoke.StackPushInteger(nWillSave);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(811);
        }

        /// <summary>
        ///   Set the Reflex saving throw value of the Door or Placeable object oObject.
        ///   - oObject: a door or placeable object.
        ///   - nReflexSave: must be between 0 and 250.
        /// </summary>
        public static void SetReflexSavingThrow(uint oObject, int nReflexSave)
        {
            NWNXPInvoke.StackPushInteger(nReflexSave);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(812);
        }

        /// <summary>
        ///   Set the Fortitude saving throw value of the Door or Placeable object oObject.
        ///   - oObject: a door or placeable object.
        ///   - nFortitudeSave: must be between 0 and 250.
        /// </summary>
        public static void SetFortitudeSavingThrow(uint oObject, int nFortitudeSave)
        {
            NWNXPInvoke.StackPushInteger(nFortitudeSave);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(813);
        }

        /// <summary>
        ///   Gets the weight of an item, or the total carried weight of a creature in tenths
        ///   of pounds (as per the baseitems.2da).
        ///   - oTarget: the item or creature for which the weight is needed
        /// </summary>
        public static int GetWeight(uint oTarget = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(706);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Gets the object that acquired the module item.  May be a creature, item, or placeable
        /// </summary>
        public static uint GetModuleItemAcquiredBy()
        {
            NWNXPInvoke.CallBuiltIn(707);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Causes the object to instantly speak a translated string.
        ///   (not an action, not blocked when uncommandable)
        ///   - nStrRef: Reference of the string in the talk table
        ///   - nTalkVolume: TALKVOLUME_*
        /// </summary>
        public static void SpeakStringByStrRef(int nStrRef, TalkVolume nTalkVolume = TalkVolume.Talk)
        {
            NWNXPInvoke.StackPushInteger((int)nTalkVolume);
            NWNXPInvoke.StackPushInteger(nStrRef);
            NWNXPInvoke.CallBuiltIn(691);
        }

        /// <summary>
        ///   Duplicates the object specified by oSource.
        ///   NOTE: this command can be used for copying Creatures, Items, Placeables, Waypoints, Stores, Doors, Triggers, Encounters.
        ///   If an owner is specified and the object is an item, it will be put into their inventory
        ///   If the object is a creature, they will be created at the location.
        ///   If a new tag is specified, it will be assigned to the new object.
        ///   If bCopyLocalState is TRUE, local vars, effects, action queue, and transition info (triggers, doors) are copied over.
        /// </summary>
        public static uint CopyObject(uint oSource, Location locLocation, uint oOwner = OBJECT_INVALID, string sNewTag = "", bool bCopyLocalState = false)
        {
            NWNXPInvoke.StackPushInteger(bCopyLocalState ? 1 : 0);
            NWNXPInvoke.StackPushString(sNewTag);
            NWNXPInvoke.StackPushObject(oOwner);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, locLocation);
            NWNXPInvoke.StackPushObject(oSource);
            NWNXPInvoke.CallBuiltIn(600);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   returns the template used to create this object (if appropriate)
        ///   * returns an empty string when no template found
        /// </summary>
        public static string GetResRef(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(582);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Determine whether oObject has an inventory.
        ///   * Returns TRUE for creatures and stores, and checks to see if an item or placeable object is a container.
        ///   * Returns FALSE for all other object types.
        /// </summary>
        public static bool GetHasInventory(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(570);
            return NWNXPInvoke.StackPopInteger() == 1;
        }

        /// <summary>
        ///   Get the name of oCreature's deity.
        ///   * Returns "" if oCreature is invalid (or if the deity name is blank for
        ///   oCreature).
        /// </summary>
        public static string GetDeity(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(489);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Get the name of oCreature's sub race.
        ///   * Returns "" if oCreature is invalid (or if sub race is blank for oCreature).
        /// </summary>
        public static string GetSubRace(uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(490);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Get oTarget's base fortitude saving throw value (this will only work for
        ///   creatures, doors, and placeables).
        ///   * Returns 0 if oTarget is invalid.
        /// </summary>
        public static int GetFortitudeSavingThrow(uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(491);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get oTarget's base will saving throw value (this will only work for creatures,
        ///   doors, and placeables).
        ///   * Returns 0 if oTarget is invalid.
        /// </summary>
        public static int GetWillSavingThrow(uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(492);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get oTarget's base reflex saving throw value (this will only work for
        ///   creatures, doors, and placeables).
        ///   * Returns 0 if oTarget is invalid.
        /// </summary>
        public static int GetReflexSavingThrow(uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(493);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get oCreature's challenge rating.
        ///   * Returns 0.0 if oCreature is invalid.
        /// </summary>
        public static float GetChallengeRating(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(494);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        ///   Get oCreature's age.
        ///   * Returns 0 if oCreature is invalid.
        /// </summary>
        public static int GetAge(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(495);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get oCreature's movement rate.
        ///   * Returns 0 if oCreature is invalid.
        /// </summary>
        public static int GetMovementRate(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(496);
            return NWNXPInvoke.StackPopInteger();
        }
        /// <summary>
        ///   Determine whether oTarget is a plot object.
        /// </summary>
        public static bool GetPlotFlag(uint oTarget = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(455);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Set oTarget's plot object status.
        /// </summary>
        public static void SetPlotFlag(uint oTarget, bool nPlotFlag)
        {
            NWNXPInvoke.StackPushInteger(nPlotFlag ? 1 : 0);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(456);
        }

        /// <summary>
        ///   Play a voice chat.
        ///   - nVoiceChatID: VOICE_CHAT_*
        ///   - oTarget
        /// </summary>
        public static void PlayVoiceChat(VoiceChat nVoiceChatID, uint oTarget = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushInteger((int)nVoiceChatID);
            NWNXPInvoke.CallBuiltIn(421);
        }

        /// <summary>
        ///   Get the amount of gold possessed by oTarget.
        /// </summary>
        public static int GetGold(uint oTarget = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(418);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Play oSound.
        /// </summary>
        public static void SoundObjectPlay(uint oSound)
        {
            NWNXPInvoke.StackPushObject(oSound);
            NWNXPInvoke.CallBuiltIn(413);
        }

        /// <summary>
        ///   Stop playing oSound.
        /// </summary>
        public static void SoundObjectStop(uint oSound)
        {
            NWNXPInvoke.StackPushObject(oSound);
            NWNXPInvoke.CallBuiltIn(414);
        }

        /// <summary>
        ///   Set the volume of oSound.
        ///   - oSound
        ///   - nVolume: 0-127
        /// </summary>
        public static void SoundObjectSetVolume(uint oSound, int nVolume)
        {
            NWNXPInvoke.StackPushInteger(nVolume);
            NWNXPInvoke.StackPushObject(oSound);
            NWNXPInvoke.CallBuiltIn(415);
        }

        /// <summary>
        ///   Set the position of oSound.
        /// </summary>
        public static void SoundObjectSetPosition(uint oSound, Vector3 vPosition)
        {
            NWNXPInvoke.StackPushVector(vPosition);
            NWNXPInvoke.StackPushObject(oSound);
            NWNXPInvoke.CallBuiltIn(416);
        }

        /// <summary>
        ///   Immediately speak a conversation one-liner.
        ///   - sDialogResRef
        ///   - oTokenTarget: This must be specified if there are creature-specific tokens
        ///   in the string.
        /// </summary>
        public static void SpeakOneLinerConversation(string sDialogResRef = "", uint oTokenTarget = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oTokenTarget);
            NWNXPInvoke.StackPushString(sDialogResRef);
            NWNXPInvoke.CallBuiltIn(417);
        }

        /// <summary>
        ///   Set the destroyable status of the caller.
        ///   - bDestroyable: If this is FALSE, the caller does not fade out on death, but
        ///   sticks around as a corpse.
        ///   - bRaiseable: If this is TRUE, the caller can be raised via resurrection.
        ///   - bSelectableWhenDead: If this is TRUE, the caller is selectable after death.
        /// </summary>
        public static void SetIsDestroyable(bool bDestroyable = true, bool bRaiseable = true,
            bool bSelectableWhenDead = false)
        {
            NWNXPInvoke.StackPushInteger(bSelectableWhenDead ? 1 : 0);
            NWNXPInvoke.StackPushInteger(bRaiseable ? 1 : 0);
            NWNXPInvoke.StackPushInteger(bDestroyable ? 1 : 0);
            NWNXPInvoke.CallBuiltIn(323);
        }

        /// <summary>
        ///   Set the locked state of oTarget, which can be a door or a placeable object.
        /// </summary>
        public static void SetLocked(uint oTarget, bool nLocked)
        {
            NWNXPInvoke.StackPushInteger(nLocked ? 1 : 0);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(324);
        }

        /// <summary>
        ///   Get the locked state of oTarget, which can be a door or a placeable object.
        /// </summary>
        public static bool GetLocked(uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(325);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Create an object of the specified type at lLocation.
        ///   - nObjectType: OBJECT_TYPE_ITEM, OBJECT_TYPE_CREATURE, OBJECT_TYPE_PLACEABLE,
        ///   OBJECT_TYPE_STORE, OBJECT_TYPE_WAYPOINT
        ///   - sTemplate
        ///   - lLocation
        ///   - bUseAppearAnimation
        ///   - sNewTag - if this string is not empty, it will replace the default tag from the template
        /// </summary>
        public static uint CreateObject(ObjectType nObjectType, string sTemplate, Location lLocation,
            bool nUseAppearAnimation = false, string sNewTag = "")
        {
            NWNXPInvoke.StackPushString(sNewTag);
            NWNXPInvoke.StackPushInteger(nUseAppearAnimation ? 1 : 0);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lLocation);
            NWNXPInvoke.StackPushString(sTemplate);
            NWNXPInvoke.StackPushInteger((int)nObjectType);
            NWNXPInvoke.CallBuiltIn(243);
            return NWNXPInvoke.StackPopObject();
        }
        /// <summary>
        ///   Get the Nth object nearest to oTarget that is of the specified type.
        ///   - nObjectType: OBJECT_TYPE_*
        ///   - oTarget
        ///   - nNth
        ///   * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetNearestObject(ObjectType nObjectType = ObjectType.All, uint oTarget = OBJECT_INVALID, int nNth = 1)
        {
            NWNXPInvoke.StackPushInteger(nNth);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushInteger((int)nObjectType);
            NWNXPInvoke.CallBuiltIn(227);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the nNth object nearest to lLocation that is of the specified type.
        ///   - nObjectType: OBJECT_TYPE_*
        ///   - lLocation
        ///   - nNth
        ///   * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetNearestObjectToLocation(Location lLocation, ObjectType nObjectType = ObjectType.All, int nNth = 1)
        {
            NWNXPInvoke.StackPushInteger(nNth);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lLocation);
            NWNXPInvoke.StackPushInteger((int)nObjectType);
            NWNXPInvoke.CallBuiltIn(228);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the nth Object nearest to oTarget that has sTag as its tag.
        ///   * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetNearestObjectByTag(string sTag, uint oTarget = OBJECT_INVALID, int nNth = 1)
        {
            NWNXPInvoke.StackPushInteger(nNth);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushString(sTag);
            NWNXPInvoke.CallBuiltIn(229);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   If oObject is a creature, this will return that creature's armour class
        ///   If oObject is an item, door or placeable, this will return zero.
        ///   - nForFutureUse: this parameter is not currently used
        ///   * Return value if oObject is not a creature, item, door or placeable: -1
        /// </summary>
        public static int GetAC(uint oObject)
        {
            NWNXPInvoke.StackPushInteger(0);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(116);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the object type (OBJECT_TYPE_*) of oTarget
        ///   * Return value if oTarget is not a valid object: -1
        /// </summary>
        public static ObjectType GetObjectType(uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(106);
            return (ObjectType)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the current hitpoints of oObject
        ///   * Return value on error: 0
        /// </summary>
        public static int GetCurrentHitPoints(uint oObject = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(49);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the maximum hitpoints of oObject
        ///   * Return value on error: 0
        /// </summary>
        public static int GetMaxHitPoints(uint oObject = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(50);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   * Returns TRUE if oObject is a valid object.
        /// </summary>
        public static bool GetIsObjectValid(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(42);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        /// Convert sHex, a string containing a hexadecimal object id,
        /// into a object reference. Counterpart to StringToObject().
        /// </summary>
        public static uint StringToObject(string sHex)
        {
            NWNXPInvoke.StackPushString(sHex);
            NWNXPInvoke.CallBuiltIn(936);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        /// Replace's oObject's texture sOld with sNew.
        /// Specifying sNew = "" will restore the original texture.
        /// If sNew cannot be found, the original texture will be restored.
        /// sNew must refer to a simple texture, not PLT
        /// </summary>
        public static void ReplaceObjectTexture(uint oObject, string sOld, string sNew = "")
        {
            NWNXPInvoke.StackPushString(sNew);
            NWNXPInvoke.StackPushString(sOld);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(920);
        }

        /// <summary>
        /// Sets the current hitpoints of oObject.
        /// * You cannot destroy or revive objects or creatures with this function.
        /// * For currently dying PCs, you can only set hitpoints in the range of -9 to 0.
        /// * All other objects need to be alive and the range is clamped to 1 and max hitpoints.
        /// * This is not considered damage (or healing). It circumvents all combat logic, including damage resistance and reduction.
        /// * This is not considered a friendly or hostile combat action. It will not affect factions, nor will it trigger script events.
        /// * This will not advise player parties in the combat log.
        /// </summary>
        public static void SetCurrentHitPoints(uint oObject, int nHitPoints)
        {
            NWNXPInvoke.StackPushInteger(nHitPoints);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(937);
        }

    }
}