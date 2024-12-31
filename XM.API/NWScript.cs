using System.Numerics;
using XM.API.BaseTypes;
using XM.API.Constants;
using ClassType = XM.API.Constants.ClassType;
using MissChanceType = XM.API.Constants.MissChanceType;
using ProjectilePathType = XM.API.Constants.ProjectilePathType;
using SavingThrowType = XM.API.Constants.SavingThrowType;

namespace XM.API
{
    public static partial class NWScript
    {
        public const uint OBJECT_INVALID = 0x7F000000;
        public static uint OBJECT_SELF => NWN.Core.NWScript.OBJECT_SELF;
        public static Location LOCATION_INVALID => Location(OBJECT_INVALID, new Vector3(0f, 0f, 0f), 0f);

        public const string sLanguage = "nwscript";
        /// <summary>
        ///  Get an integer between 0 and nMaxInteger-1.
        ///  Return value on error: 0
        /// </summary>
        public static int Random(int nMaxInteger)
        {
            return NWN.Core.NWScript.Random(nMaxInteger);
        }

        /// <summary>
        ///  Output sString to the log file.
        /// </summary>
        public static void PrintString(string sString)
        {
            NWN.Core.NWScript.PrintString(sString);
        }

        /// <summary>
        ///  Output a formatted float to the log file.
        ///  - nWidth should be a value from 0 to 18 inclusive.
        ///  - nDecimals should be a value from 0 to 9 inclusive.
        /// </summary>
        public static void PrintFloat(float fFloat, int nWidth = 18, int nDecimals = 9)
        {
            NWN.Core.NWScript.PrintFloat(fFloat, nWidth, nDecimals);
        }

        /// <summary>
        ///  Convert fFloat into a string.
        ///  - nWidth should be a value from 0 to 18 inclusive.
        ///  - nDecimals should be a value from 0 to 9 inclusive.
        /// </summary>
        public static string FloatToString(float fFloat, int nWidth = 18, int nDecimals = 9)
        {
            return NWN.Core.NWScript.FloatToString(fFloat, nWidth, nDecimals);
        }

        /// <summary>
        ///  Output nInteger to the log file.
        /// </summary>
        public static void PrintInteger(int nInteger)
        {
            NWN.Core.NWScript.PrintInteger(nInteger);
        }
        /// <summary>
        ///  Output oObject's ID to the log file.
        /// </summary>
        public static void PrintObject(uint oObject)
        {
            NWN.Core.NWScript.PrintObject(oObject);
        }

        /// <summary>
        ///  Assign aActionToAssign to oActionSubject.
        ///  * No return value, but if an error occurs, the log file will contain
        ///    "AssignCommand failed."
        ///    (If the object doesn't exist, nothing happens.)
        /// </summary>
        public static void AssignCommand(uint oActionSubject, System.Action aActionToAssign)
        {
            NWN.Core.NWScript.AssignCommand(oActionSubject, aActionToAssign);
        }

        /// <summary>
        ///  Delay aActionToDelay by fSeconds.
        ///  * No return value, but if an error occurs, the log file will contain
        ///    "DelayCommand failed."
        ///  It is suggested that functions which create effects should not be used
        ///  as parameters to delayed actions. Instead, the effect should be created in the
        ///  script and then passed into the action. For example:
        ///  effect eDamage = EffectDamage(nDamage, DAMAGE_TYPE_MAGICAL);
        ///  DelayCommand(fDelay, ApplyEffectToObject(DURATION_TYPE_INSTANT, eDamage, oTarget));
        /// </summary>
        public static void DelayCommand(float fSeconds, System.Action aActionToDelay)
        {
            NWN.Core.NWScript.DelayCommand(fSeconds, aActionToDelay);
        }

        /// <summary>
        ///  Make oTarget run sScript and then return execution to the calling script.
        ///  If sScript does not specify a compiled script, nothing happens.
        /// </summary>
        public static void ExecuteScript(string sScript, uint oTarget = NWN.Core.NWScript.OBJECT_INVALID)
        {
            NWN.Core.NWScript.ExecuteScript(sScript, oTarget);
        }

        /// <summary>
        ///  Clear all the actions of the caller.
        ///  * No return value, but if an error occurs, the log file will contain
        ///    "ClearAllActions failed."
        ///  - nClearCombatState: if true, this will immediately clear the combat state
        ///    on a creature, which will stop the combat music and allow them to rest,
        ///    engage in dialog, or other actions that they would normally have to wait for.
        /// </summary>
        public static void ClearAllActions(int nClearCombatState = NWN.Core.NWScript.FALSE)
        {
            NWN.Core.NWScript.ClearAllActions(nClearCombatState);
        }

        /// <summary>
        ///  Cause the caller to face fDirection.
        ///  - fDirection is expressed as anticlockwise degrees from Due East.
        ///    DIRECTION_EAST, DIRECTION_NORTH, DIRECTION_WEST and DIRECTION_SOUTH are
        ///    predefined. (0.0f=East, 90.0f=North, 180.0f=West, 270.0f=South)
        /// </summary>
        public static void SetFacing(float fDirection)
        {
            NWN.Core.NWScript.SetFacing(fDirection);
        }

        /// <summary>
        ///  Set the calendar to the specified date.
        ///  - nYear should be from 0 to 32000 inclusive
        ///  - nMonth should be from 1 to 12 inclusive
        ///  - nDay should be from 1 to 28 inclusive
        ///  1) Time can only be advanced forwards; attempting to set the time backwards
        ///     will result in no change to the calendar.
        ///  2) If values larger than the month or day are specified, they will be wrapped
        ///     around and the overflow will be used to advance the next field.
        ///     e.g. Specifying a year of 1350, month of 33 and day of 10 will result in
        ///     the calendar being set to a year of 1352, a month of 9 and a day of 10.
        /// </summary>
        public static void SetCalendar(int nYear, int nMonth, int nDay)
        {
            NWN.Core.NWScript.SetCalendar(nYear, nMonth, nDay);
        }

        /// <summary>
        ///  Set the time to the time specified.
        ///  - nHour should be from 0 to 23 inclusive
        ///  - nMinute should be from 0 to 59 inclusive
        ///  - nSecond should be from 0 to 59 inclusive
        ///  - nMillisecond should be from 0 to 999 inclusive
        ///  1) Time can only be advanced forwards; attempting to set the time backwards
        ///     will result in the day advancing and then the time being set to that
        ///     specified, e.g. if the current hour is 15 and then the hour is set to 3,
        ///     the day will be advanced by 1 and the hour will be set to 3.
        ///  2) If values larger than the max hour, minute, second or millisecond are
        ///     specified, they will be wrapped around and the overflow will be used to
        ///     advance the next field, e.g. specifying 62 hours, 250 minutes, 10 seconds
        ///     and 10 milliseconds will result in the calendar day being advanced by 2
        ///     and the time being set to 18 hours, 10 minutes, 10 milliseconds.
        /// </summary>
        public static void SetTime(int nHour, int nMinute, int nSecond, int nMillisecond)
        {
            NWN.Core.NWScript.SetTime(nHour, nMinute, nSecond, nMillisecond);
        }
        /// <summary>
        ///  Get the current calendar year.
        /// </summary>
        public static int GetCalendarYear()
        {
            return NWN.Core.NWScript.GetCalendarYear();
        }

        /// <summary>
        ///  Get the current calendar month.
        /// </summary>
        public static int GetCalendarMonth()
        {
            return NWN.Core.NWScript.GetCalendarMonth();
        }

        /// <summary>
        ///  Get the current calendar day.
        /// </summary>
        public static int GetCalendarDay()
        {
            return NWN.Core.NWScript.GetCalendarDay();
        }

        /// <summary>
        ///  Get the current hour.
        /// </summary>
        public static int GetTimeHour()
        {
            return NWN.Core.NWScript.GetTimeHour();
        }

        /// <summary>
        ///  Get the current minute.
        /// </summary>
        public static int GetTimeMinute()
        {
            return NWN.Core.NWScript.GetTimeMinute();
        }

        /// <summary>
        ///  Get the current second.
        /// </summary>
        public static int GetTimeSecond()
        {
            return NWN.Core.NWScript.GetTimeSecond();
        }

        /// <summary>
        ///  Get the current millisecond.
        /// </summary>
        public static int GetTimeMillisecond()
        {
            return NWN.Core.NWScript.GetTimeMillisecond();
        }

        /// <summary>
        ///  The action subject will generate a random location near its current location
        ///  and pathfind to it. ActionRandomwalk never ends, which means it is necessary
        ///  to call ClearAllActions in order to allow a creature to perform any other action
        ///  once ActionRandomWalk has been called.
        ///  * No return value, but if an error occurs the log file will contain
        ///    "ActionRandomWalk failed."
        /// </summary>
        public static void ActionRandomWalk()
        {
            NWN.Core.NWScript.ActionRandomWalk();
        }

        /// <summary>
        ///  The action subject will move to lDestination.
        ///  - lDestination: The object will move to this location. If the location is
        ///    invalid or a path cannot be found to it, the command does nothing.
        ///  - bRun: If this is true, the action subject will run rather than walk.
        ///  * No return value, but if an error occurs the log file will contain
        ///    "MoveToPoint failed."
        /// </summary>
        public static void ActionMoveToLocation(Location lDestination, bool bRun = false)
        {
            NWN.Core.NWScript.ActionMoveToLocation(lDestination, bRun ? 1 : 0);
        }

        /// <summary>
        ///  Cause the action subject to move to a certain distance from oMoveTo.
        ///  If there is no path to oMoveTo, this command will do nothing.
        ///  - oMoveTo: This is the object we wish the action subject to move to.
        ///  - bRun: If this is true, the action subject will run rather than walk.
        ///  - fRange: This is the desired distance between the action subject and oMoveTo.
        ///  * No return value, but if an error occurs the log file will contain
        ///    "ActionMoveToObject failed."
        /// </summary>
        public static void ActionMoveToObject(uint oMoveTo, bool bRun = false, float fRange = 1.0f)
        {
            NWN.Core.NWScript.ActionMoveToObject(oMoveTo, bRun ? 1 : 0, fRange);
        }

        /// <summary>
        ///  Cause the action subject to move to a certain distance away from oFleeFrom.
        ///  - oFleeFrom: This is the object we wish the action subject to move away from.
        ///    If oFleeFrom is not in the same area as the action subject, nothing will
        ///    happen.
        ///  - bRun: If this is true, the action subject will run rather than walk.
        ///  - fMoveAwayRange: This is the distance we wish the action subject to put
        ///    between themselves and oFleeFrom.
        ///  * No return value, but if an error occurs the log file will contain
        ///    "ActionMoveAwayFromObject failed."
        /// </summary>
        public static void ActionMoveAwayFromObject(uint oFleeFrom, bool bRun = false, float fMoveAwayRange = 40.0f)
        {
            NWN.Core.NWScript.ActionMoveAwayFromObject(oFleeFrom, bRun ? 1 : 0, fMoveAwayRange);
        }

        /// <summary>
        ///  Get the area that oTarget is currently in.
        ///  * Return value on error: OBJECT_INVALID.
        /// </summary>
        public static uint GetArea(uint oTarget)
        {
            return NWN.Core.NWScript.GetArea(oTarget);
        }

        /// <summary>
        ///  The value returned by this function depends on the object type of the caller:
        ///  1) If the caller is a door it returns the object that last
        ///     triggered it.
        ///  2) If the caller is a trigger, area of effect, module, area or encounter it
        ///     returns the object that last entered it.
        ///  * Return value on error: OBJECT_INVALID
        ///   When used for doors, this should only be called from the OnAreaTransitionClick
        ///   event. Otherwise, it should only be called in OnEnter scripts.
        /// </summary>
        public static uint GetEnteringObject()
        {
            return NWN.Core.NWScript.GetEnteringObject();
        }

        /// <summary>
        ///  Get the object that last left the caller. This function works on triggers,
        ///  areas of effect, modules, areas and encounters.
        ///  * Return value on error: OBJECT_INVALID
        ///  Should only be called in OnExit scripts.
        /// </summary>
        public static uint GetExitingObject()
        {
            return NWN.Core.NWScript.GetExitingObject();
        }

        /// <summary>
        ///  Get the position of oTarget.
        ///  * Return value on error: vector (0.0f, 0.0f, 0.0f).
        /// </summary>
        public static Vector3 GetPosition(uint oTarget)
        {
            return NWN.Core.NWScript.GetPosition(oTarget);
        }

        /// <summary>
        ///  Get the direction in which oTarget is facing, expressed as a float between
        ///  0.0f and 360.0f
        ///  * Return value on error: -1.0f
        /// </summary>
        public static float GetFacing(uint oTarget)
        {
            return NWN.Core.NWScript.GetFacing(oTarget);
        }

        /// <summary>
        ///  Get the possessor of oItem
        ///  - bReturnBags: If true will potentially return a bag container item the item is in, instead of
        ///                 the object holding the bag. Make sure to check the returning item object type with this flag.
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetItemPossessor(uint oItem, bool bReturnBags = false)
        {
            return NWN.Core.NWScript.GetItemPossessor(oItem, bReturnBags ? 1 : 0);
        }

        /// <summary>
        ///  Get the object possessed by oCreature with the tag sItemTag
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetItemPossessedBy(uint oCreature, string sItemTag)
        {
            return NWN.Core.NWScript.GetItemPossessedBy(oCreature, sItemTag);
        }

        /// <summary>
        ///  Create an item with the template sItemTemplate in oTarget's inventory.
        ///  - nStackSize: This is the stack size of the item to be created
        ///  - sNewTag: If this string is not empty, it will replace the default tag from the template
        ///  * Return value: The object that has been created.  On error, this returns
        ///    OBJECT_INVALID.
        ///  If the item created was merged into an existing stack of similar items,
        ///  the function will return the merged stack object. If the merged stack
        ///  overflowed, the function will return the overflowed stack that was created.
        /// </summary>
        public static uint CreateItemOnObject(string sItemTemplate, uint oTarget = OBJECT_INVALID, int nStackSize = 1, string sNewTag = "")
        {
            return NWN.Core.NWScript.CreateItemOnObject(sItemTemplate, oTarget, nStackSize, sNewTag);
        }

        /// <summary>
        ///  Equip oItem into nInventorySlot.
        ///  - nInventorySlot: INVENTORY_SLOT_*
        ///  * No return value, but if an error occurs the log file will contain
        ///    "ActionEquipItem failed."
        /// 
        ///  Note:
        ///        If the creature already has an item equipped in the slot specified, it will be
        ///        unequipped automatically by the call to ActionEquipItem.
        /// 
        ///        In order for ActionEquipItem to succeed the creature must be able to equip the
        ///        item oItem normally. This means that:
        ///        1) The item is in the creature's inventory.
        ///        2) The item must already be identified (if magical).
        ///        3) The creature has the level required to equip the item (if magical and ILR is on).
        ///        4) The creature possesses the required feats to equip the item (such as weapon proficiencies).
        /// </summary>
        public static void ActionEquipItem(uint oItem, InventorySlotType nInventorySlot)
        {
            NWN.Core.NWScript.ActionEquipItem(oItem, (int)nInventorySlot);
        }

        /// <summary>
        ///  Unequip oItem from whatever slot it is currently in.
        /// </summary>
        public static void ActionUnequipItem(uint oItem)
        {
            NWN.Core.NWScript.ActionUnequipItem(oItem);
        }

        /// <summary>
        ///  Pick up oItem from the ground.
        ///  * No return value, but if an error occurs the log file will contain
        ///    "ActionPickUpItem failed."
        /// </summary>
        public static void ActionPickUpItem(uint oItem)
        {
            NWN.Core.NWScript.ActionPickUpItem(oItem);
        }

        /// <summary>
        ///  Put down oItem on the ground.
        ///  * No return value, but if an error occurs the log file will contain
        ///    "ActionPutDownItem failed."
        /// </summary>
        public static void ActionPutDownItem(uint oItem)
        {
            NWN.Core.NWScript.ActionPutDownItem(oItem);
        }

        /// <summary>
        ///  Get the last attacker of oAttackee.  This should only be used ONLY in the
        ///  OnAttacked events for creatures, placeables and doors.
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetLastAttacker(uint oAttackee = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetLastAttacker(oAttackee);
        }

        /// <summary>
        ///  Attack oAttackee.
        ///  - bPassive: If this is true, attack is in passive mode.
        /// </summary>
        public static void ActionAttack(uint oAttackee, bool bPassive = false)
        {
            NWN.Core.NWScript.ActionAttack(oAttackee, bPassive ? 1 : 0);
        }

        /// <summary>
        ///  Get the creature nearest to oTarget, subject to all the criteria specified.
        ///  - nFirstCriteriaType: CREATURE_TYPE_*
        ///  - nFirstCriteriaValue:
        ///    -> CLASS_TYPE_* if nFirstCriteriaType was CREATURE_TYPE_CLASS
        ///    -> SPELL_* if nFirstCriteriaType was CREATURE_TYPE_DOES_NOT_HAVE_SPELL_EFFECT
        ///       or CREATURE_TYPE_HAS_SPELL_EFFECT
        ///    -> true or false if nFirstCriteriaType was CREATURE_TYPE_IS_ALIVE
        ///    -> PERCEPTION_* if nFirstCriteriaType was CREATURE_TYPE_PERCEPTION
        ///    -> PLAYER_CHAR_IS_PC or PLAYER_CHAR_NOT_PC if nFirstCriteriaType was
        ///       CREATURE_TYPE_PLAYER_CHAR
        ///    -> RACIAL_TYPE_* if nFirstCriteriaType was CREATURE_TYPE_RACIAL_TYPE
        ///    -> REPUTATION_TYPE_* if nFirstCriteriaType was CREATURE_TYPE_REPUTATION
        ///    For example, to get the nearest PC, use:
        ///    (CREATURE_TYPE_PLAYER_CHAR, PLAYER_CHAR_IS_PC)
        ///  - oTarget: We're trying to find the creature of the specified type that is
        ///    nearest to oTarget
        ///  - nNth: We don't have to find the first nearest: we can find the Nth nearest...
        ///  - nSecondCriteriaType: This is used in the same way as nFirstCriteriaType to
        ///    further specify the type of creature that we are looking for.
        ///  - nSecondCriteriaValue: This is used in the same way as nFirstCriteriaValue
        ///    to further specify the type of creature that we are looking for.
        ///  - nThirdCriteriaType: This is used in the same way as nFirstCriteriaType to
        ///    further specify the type of creature that we are looking for.
        ///  - nThirdCriteriaValue: This is used in the same way as nFirstCriteriaValue to
        ///    further specify the type of creature that we are looking for.
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetNearestCreature(CreatureType nFirstCriteriaType, int nFirstCriteriaValue, uint oTarget = OBJECT_INVALID, int nNth = 1, int nSecondCriteriaType = -1, int nSecondCriteriaValue = -1, int nThirdCriteriaType = -1, int nThirdCriteriaValue = -1)
        {
            return NWN.Core.NWScript.GetNearestCreature((int)nFirstCriteriaType, nFirstCriteriaValue, oTarget, nNth, nSecondCriteriaType, nSecondCriteriaValue, nThirdCriteriaType, nThirdCriteriaValue);
        }
        /// <summary>
        ///  Add a speak action to the action subject.<br/>
        ///  - sStringToSpeak: String to be spoken<br/>
        ///  - nTalkVolume: TALKVOLUME_*
        /// </summary>
        public static void ActionSpeakString(string sStringToSpeak, TalkVolumeType nTalkVolume = TalkVolumeType.Talk)
        {
            NWN.Core.NWScript.ActionSpeakString(sStringToSpeak, (int)nTalkVolume);
        }

        /// <summary>
        ///  Cause the action subject to play an animation<br/>
        ///  - nAnimation: ANIMATION_*<br/>
        ///  - fSpeed: Speed of the animation<br/>
        ///  - fDurationSeconds: Duration of the animation (this is not used for Fire and<br/>
        ///    Forget animations)
        /// </summary>
        public static void ActionPlayAnimation(AnimationType nAnimation, float fSpeed = 1.0f, float fDurationSeconds = 0.0f)
        {
            NWN.Core.NWScript.ActionPlayAnimation((int)nAnimation, fSpeed, fDurationSeconds);
        }

        /// <summary>
        ///  Get the distance from the caller to oObject in metres.<br/>
        ///  * Return value on error: -1.0f
        /// </summary>
        public static float GetDistanceToObject(uint oObject)
        {
            return NWN.Core.NWScript.GetDistanceToObject(oObject);
        }

        /// <summary>
        ///  * Returns true if oObject is a valid object.
        /// </summary>
        public static bool GetIsObjectValid(uint oObject)
        {
            return NWN.Core.NWScript.GetIsObjectValid(oObject) == 1;
        }

        /// <summary>
        ///  Cause the action subject to open oDoor
        /// </summary>
        public static void ActionOpenDoor(uint oDoor)
        {
            NWN.Core.NWScript.ActionOpenDoor(oDoor);
        }

        /// <summary>
        ///  Cause the action subject to close oDoor
        /// </summary>
        public static void ActionCloseDoor(uint oDoor)
        {
            NWN.Core.NWScript.ActionCloseDoor(oDoor);
        }

        /// <summary>
        ///  Change the direction in which the camera is facing<br/>
        ///  - fDirection is expressed as anticlockwise degrees from Due East.<br/>
        ///    (0.0f=East, 90.0f=North, 180.0f=West, 270.0f=South)<br/>
        ///  A value of -1.0f for any parameter will be ignored and instead it will<br/>
        ///  use the current camera value.<br/>
        ///  This can be used to change the way the camera is facing after the player<br/>
        ///  emerges from an area transition.<br/>
        ///  - nTransitionType: CAMERA_TRANSITION_TYPE_*  SNAP will immediately move the<br/>
        ///    camera to the new position, while the other types will result in the camera moving gradually into position<br/>
        ///  Pitch and distance are limited to valid values for the current camera mode:<br/>
        ///  Top Down: Distance = 5-20, Pitch = 1-50<br/>
        ///  Driving camera: Distance = 6 (can't be changed), Pitch = 1-62<br/>
        ///  Chase: Distance = 5-20, Pitch = 1-50<br/>
        ///  *** NOTE *** In NWN:Hordes of the Underdark the camera limits have been relaxed to the following:<br/>
        ///  Distance 1-25<br/>
        ///  Pitch 1-89
        /// </summary>
        public static void SetCameraFacing(float fDirection, float fDistance = -1.0f, float fPitch = -1.0f, CameraTransitionType nTransitionType = CameraTransitionType.Snap)
        {
            NWN.Core.NWScript.SetCameraFacing(fDirection, fDistance, fPitch, (int)nTransitionType);
        }

        /// <summary>
        ///  Play sSoundName<br/>
        ///  - sSoundName: TBD - SS<br/>
        ///  This will play a mono sound from the location of the object running the command.
        /// </summary>
        public static void PlaySound(string sSoundName)
        {
            NWN.Core.NWScript.PlaySound(sSoundName);
        }

        /// <summary>
        ///  Get the object at which the caller last cast a spell<br/>
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetSpellTargetObject()
        {
            return NWN.Core.NWScript.GetSpellTargetObject();
        }

        /// <summary>
        ///  This action casts a spell at oTarget.<br/>
        ///  - nSpell: SPELL_*<br/>
        ///  - oTarget: Target for the spell<br/>
        ///  - nMetaMagic: METAMAGIC_*. If nClass is specified, cannot be METAMAGIC_ANY.<br/>
        ///  - bCheat: If this is true, then the executor of the action doesn't have to be<br/>
        ///    able to cast the spell. Ignored if nClass is specified.<br/>
        ///  - bCheat: If this is true, then the executor of the action doesn't have to be<br/>
        ///    able to cast the spell.<br/>
        ///  - nDomainLevel: The level of the spell if cast from a domain slot.<br/>
        ///    eg SPELL_HEAL can be spell level 5 on a cleric. Use 0 for no domain slot.<br/>
        ///  - nProjectilePathType: PROJECTILE_PATH_TYPE_*<br/>
        ///  - bInstantSpell: If this is true, the spell is cast immediately. This allows<br/>
        ///    the end-user to simulate a high-level magic-user having lots of advance<br/>
        ///    warning of impending trouble<br/>
        ///  - nClass: If set to a CLASS_TYPE_* it will cast using that class specifically.<br/>
        ///    CLASS_TYPE_INVALID will use spell abilities.<br/>
        ///  - bSpontaneousCast: If set to true will attempt to cast the given spell spontaneously,<br/>
        ///    ie a Cleric casting Cure Light Wounds using any level 1 slot. Needs a valid nClass set.
        /// </summary>
        public static void ActionCastSpellAtObject(SpellType nSpell, uint oTarget, MetamagicType nMetaMagic = MetamagicType.Any, bool bCheat = false, int nDomainLevel = 0, ProjectilePathType nProjectilePathType = ProjectilePathType.Default, bool bInstantSpell = false, ClassType nClass = ClassType.Invalid, bool bSpontaneousCast = false)
        {
            NWN.Core.NWScript.ActionCastSpellAtObject((int)nSpell, oTarget, (int)nMetaMagic, bCheat ? 1 : 0, nDomainLevel, (int)nProjectilePathType, bInstantSpell ? 1 : 0, (int)nClass, bSpontaneousCast ? 1 : 0);
        }

        /// <summary>
        ///  Get the current hitpoints of oObject<br/>
        ///  * Return value on error: 0
        /// </summary>
        public static int GetCurrentHitPoints(uint oObject = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetCurrentHitPoints(oObject);
        }

        /// <summary>
        ///  Get the maximum hitpoints of oObject<br/>
        ///  * Return value on error: 0
        /// </summary>
        public static int GetMaxHitPoints(uint oObject = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetMaxHitPoints(oObject);
        }

        /// <summary>
        ///  Get oObject's local integer variable sVarName<br/>
        ///  * Return value on error: 0
        /// </summary>
        public static int GetLocalInt(uint oObject, string sVarName)
        {
            return NWN.Core.NWScript.GetLocalInt(oObject, sVarName);
        }

        /// <summary>
        ///  Get oObject's local float variable sVarName<br/>
        ///  * Return value on error: 0.0f
        /// </summary>
        public static float GetLocalFloat(uint oObject, string sVarName)
        {
            return NWN.Core.NWScript.GetLocalFloat(oObject, sVarName);
        }

        /// <summary>
        ///  Get oObject's local string variable sVarName<br/>
        ///  * Return value on error: ""
        /// </summary>
        public static string GetLocalString(uint oObject, string sVarName)
        {
            return NWN.Core.NWScript.GetLocalString(oObject, sVarName);
        }

        /// <summary>
        ///  Get oObject's local object variable sVarName<br/>
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetLocalObject(uint oObject, string sVarName)
        {
            return NWN.Core.NWScript.GetLocalObject(oObject, sVarName);
        }

        /// <summary>
        ///  Set oObject's local integer variable sVarName to nValue
        /// </summary>
        public static void SetLocalInt(uint oObject, string sVarName, int nValue)
        {
            NWN.Core.NWScript.SetLocalInt(oObject, sVarName, nValue);
        }

        /// <summary>
        ///  Set oObject's local float variable sVarName to nValue
        /// </summary>
        public static void SetLocalFloat(uint oObject, string sVarName, float fValue)
        {
            NWN.Core.NWScript.SetLocalFloat(oObject, sVarName, fValue);
        }

        /// <summary>
        ///  Set oObject's local string variable sVarName to nValue
        /// </summary>
        public static void SetLocalString(uint oObject, string sVarName, string sValue)
        {
            NWN.Core.NWScript.SetLocalString(oObject, sVarName, sValue);
        }

        /// <summary>
        ///  Set oObject's local object variable sVarName to nValue
        /// </summary>
        public static void SetLocalObject(uint oObject, string sVarName, uint oValue)
        {
            NWN.Core.NWScript.SetLocalObject(oObject, sVarName, oValue);
        }

        /// <summary>
        ///  Get the length of sString<br/>
        ///  * Return value on error: -1
        /// </summary>
        public static int GetStringLength(string sString)
        {
            return NWN.Core.NWScript.GetStringLength(sString);
        }

        /// <summary>
        ///  Convert sString into upper case<br/>
        ///  * Return value on error: ""
        /// </summary>
        public static string GetStringUpperCase(string sString)
        {
            return NWN.Core.NWScript.GetStringUpperCase(sString);
        }

        /// <summary>
        ///  Convert sString into lower case<br/>
        ///  * Return value on error: ""
        /// </summary>
        public static string GetStringLowerCase(string sString)
        {
            return NWN.Core.NWScript.GetStringLowerCase(sString);
        }

        /// <summary>
        ///  Get nCount characters from the right end of sString<br/>
        ///  * Return value on error: ""
        /// </summary>
        public static string GetStringRight(string sString, int nCount)
        {
            return NWN.Core.NWScript.GetStringRight(sString, nCount);
        }
        /// <summary>
        ///  Get nCounter characters from the left end of sString<br/>
        ///  * Return value on error: ""
        /// </summary>
        public static string GetStringLeft(string sString, int nCount)
        {
            return NWN.Core.NWScript.GetStringLeft(sString, nCount);
        }

        /// <summary>
        ///  Insert sString into sDestination at nPosition<br/>
        ///  * Return value on error: ""
        /// </summary>
        public static string InsertString(string sDestination, string sString, int nPosition)
        {
            return NWN.Core.NWScript.InsertString(sDestination, sString, nPosition);
        }

        /// <summary>
        ///  Get nCount characters from sString, starting at nStart<br/>
        ///  * Return value on error: ""
        /// </summary>
        public static string GetSubString(string sString, int nStart, int nCount)
        {
            return NWN.Core.NWScript.GetSubString(sString, nStart, nCount);
        }

        /// <summary>
        ///  Find the position of sSubstring inside sString<br/>
        ///  - nStart: The character position to start searching at (from the left end of the string).<br/>
        ///  * Return value on error: -1
        /// </summary>
        public static int FindSubString(string sString, string sSubString, int nStart = 0)
        {
            return NWN.Core.NWScript.FindSubString(sString, sSubString, nStart);
        }

        /// <summary>
        ///  Maths operation: absolute value of fValue
        /// </summary>
        public static float fabs(float fValue)
        {
            return NWN.Core.NWScript.fabs(fValue);
        }

        /// <summary>
        ///  Maths operation: cosine of fValue
        /// </summary>
        public static float cos(float fValue)
        {
            return NWN.Core.NWScript.cos(fValue);
        }

        /// <summary>
        ///  Maths operation: sine of fValue
        /// </summary>
        public static float sin(float fValue)
        {
            return NWN.Core.NWScript.sin(fValue);
        }

        /// <summary>
        ///  Maths operation: tan of fValue
        /// </summary>
        public static float tan(float fValue)
        {
            return NWN.Core.NWScript.tan(fValue);
        }

        /// <summary>
        ///  Maths operation: arccosine of fValue<br/>
        ///  * Returns zero if fValue > 1 or fValue < -1
        /// </summary>
        public static float acos(float fValue)
        {
            return NWN.Core.NWScript.acos(fValue);
        }

        /// <summary>
        ///  Maths operation: arcsine of fValue<br/>
        ///  * Returns zero if fValue >1 or fValue < -1
        /// </summary>
        public static float asin(float fValue)
        {
            return NWN.Core.NWScript.asin(fValue);
        }

        /// <summary>
        ///  Maths operation: arctan of fValue
        /// </summary>
        public static float atan(float fValue)
        {
            return NWN.Core.NWScript.atan(fValue);
        }

        /// <summary>
        ///  Maths operation: log of fValue<br/>
        ///  * Returns zero if fValue <= zero
        /// </summary>
        public static float log(float fValue)
        {
            return NWN.Core.NWScript.log(fValue);
        }

        /// <summary>
        ///  Maths operation: fValue is raised to the power of fExponent<br/>
        ///  * Returns zero if fValue ==0 and fExponent <0
        /// </summary>
        public static float pow(float fValue, float fExponent)
        {
            return NWN.Core.NWScript.pow(fValue, fExponent);
        }

        /// <summary>
        ///  Maths operation: square root of fValue<br/>
        ///  * Returns zero if fValue <0
        /// </summary>
        public static float sqrt(float fValue)
        {
            return NWN.Core.NWScript.sqrt(fValue);
        }

        /// <summary>
        ///  Maths operation: integer absolute value of nValue<br/>
        ///  * Return value on error: 0
        /// </summary>
        public static int abs(int nValue)
        {
            return NWN.Core.NWScript.abs(nValue);
        }

        /// <summary>
        ///  Create a Heal effect. This should be applied as an instantaneous effect.<br/>
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nDamageToHeal < 0.
        /// </summary>
        public static Effect EffectHeal(int nDamageToHeal)
        {
            return NWN.Core.NWScript.EffectHeal(nDamageToHeal);
        }

        /// <summary>
        ///  Create a Damage effect<br/>
        ///  - nDamageAmount: amount of damage to be dealt. This should be applied as an<br/>
        ///    instantaneous effect.<br/>
        ///  - nDamageType: DAMAGE_TYPE_*<br/>
        ///  - nDamagePower: DAMAGE_POWER_*
        /// </summary>
        public static Effect EffectDamage(int nDamageAmount, DamageType nDamageType = DamageType.Magical, DamagePowerType nDamagePower = DamagePowerType.Normal)
        {
            return NWN.Core.NWScript.EffectDamage(nDamageAmount, (int)nDamageType, (int)nDamagePower);
        }

        /// <summary>
        ///  Create an Ability Increase effect<br/>
        ///  - bAbilityToIncrease: ABILITY_*
        /// </summary>
        public static Effect EffectAbilityIncrease(int nAbilityToIncrease, int nModifyBy)
        {
            return NWN.Core.NWScript.EffectAbilityIncrease(nAbilityToIncrease, nModifyBy);
        }
        /// <summary>
        ///  Create a Damage Resistance effect that removes the first nAmount points of<br/>
        ///  damage of type nDamageType, up to nLimit (or infinite if nLimit is 0)<br/>
        ///  - nDamageType: DAMAGE_TYPE_*<br/>
        ///  - nAmount: The amount of damage to soak each time the target is damaged.<br/>
        ///  - nLimit: How much damage the effect can absorb before disappearing.<br/>
        ///    Set to zero for infinite.<br/>
        ///  - bRangedOnly: Set to true to have this resistance only apply to ranged attacks.
        /// </summary>
        public static Effect EffectDamageResistance(DamageType nDamageType, int nAmount, int nLimit = 0, bool bRangedOnly = false)
        {
            return NWN.Core.NWScript.EffectDamageResistance((int)nDamageType, nAmount, nLimit, bRangedOnly ? 1 : 0);
        }

        /// <summary>
        ///  Create a Resurrection effect. This should be applied as an instantaneous effect.
        /// </summary>
        public static Effect EffectResurrection()
        {
            return NWN.Core.NWScript.EffectResurrection();
        }

        /// <summary>
        ///  Create a Summon Creature effect.  The creature is created and placed into the<br/>
        ///  caller's party/faction.<br/>
        ///  - sCreatureResref: Identifies the creature to be summoned<br/>
        ///  - nVisualEffectId: VFX_*<br/>
        ///  - fDelaySeconds: There can be delay between the visual effect being played, and the<br/>
        ///    creature being added to the area<br/>
        ///  - nUseAppearAnimation: should this creature play it's "appear" animation when it is<br/>
        ///    summoned. If zero, it will just fade in somewhere near the target.  If the value is 1<br/>
        ///    it will use the appear animation, and if it's 2 it will use appear2 (which doesn't exist for most creatures)
        /// </summary>
        public static Effect EffectSummonCreature(string sCreatureResref, VisualEffectType nVisualEffectId = VisualEffectType.None, float fDelaySeconds = 0.0f, int nUseAppearAnimation = 0)
        {
            return NWN.Core.NWScript.EffectSummonCreature(sCreatureResref, (int)nVisualEffectId, fDelaySeconds, nUseAppearAnimation);
        }

        /// <summary>
        ///  Get the caster level of an object. This is consistent with the caster level used when applying effects if OBJECT_SELF is used.<br/>
        ///  - oObject: A creature will return the caster level of their currently cast spell or ability, or the item's caster level if an item was used<br/>
        ///             A placeable will return an automatic caster level: floor(10, (spell innate level * 2) - 1)<br/>
        ///             An Area of Effect object will return the caster level that was used to create the Area of Effect.<br/>
        ///  * Return value on error, or if oObject has not yet cast a spell: 0;
        /// </summary>
        public static int GetCasterLevel(uint oObject)
        {
            return NWN.Core.NWScript.GetCasterLevel(oObject);
        }

        /// <summary>
        ///  Get the first in-game effect on oCreature.
        /// </summary>
        public static Effect GetFirstEffect(uint oCreature)
        {
            return NWN.Core.NWScript.GetFirstEffect(oCreature);
        }

        /// <summary>
        ///  Get the next in-game effect on oCreature.
        /// </summary>
        public static Effect GetNextEffect(uint oCreature)
        {
            return NWN.Core.NWScript.GetNextEffect(oCreature);
        }

        /// <summary>
        ///  Remove eEffect from oCreature.<br/>
        ///  * No return value
        /// </summary>
        public static void RemoveEffect(uint oCreature, Effect eEffect)
        {
            NWN.Core.NWScript.RemoveEffect(oCreature, eEffect);
        }

        /// <summary>
        ///  * Returns true if eEffect is a valid effect. The effect must have been applied to<br/>
        ///  * an object or else it will return false
        /// </summary>
        public static bool GetIsEffectValid(Effect eEffect)
        {
            return NWN.Core.NWScript.GetIsEffectValid(eEffect) == 1;
        }

        /// <summary>
        ///  Get the duration type (DURATION_TYPE_*) of eEffect.<br/>
        ///  * Return value if eEffect is not valid: -1
        /// </summary>
        public static DurationType GetEffectDurationType(Effect eEffect)
        {
            return (DurationType)NWN.Core.NWScript.GetEffectDurationType(eEffect);
        }

        /// <summary>
        ///  Get the subtype (SUBTYPE_*) of eEffect.<br/>
        ///  * Return value on error: 0
        /// </summary>
        public static EffectSubType GetEffectSubType(Effect eEffect)
        {
            return (EffectSubType)NWN.Core.NWScript.GetEffectSubType(eEffect);
        }

        /// <summary>
        ///  Get the object that created eEffect.<br/>
        ///  * Returns OBJECT_INVALID if eEffect is not a valid effect.
        /// </summary>
        public static uint GetEffectCreator(Effect eEffect)
        {
            return NWN.Core.NWScript.GetEffectCreator(eEffect);
        }

        /// <summary>
        ///  Convert nInteger into a string.<br/>
        ///  * Return value on error: ""
        /// </summary>
        public static string IntToString(int nInteger)
        {
            return NWN.Core.NWScript.IntToString(nInteger);
        }
        /// <summary>
        ///  Get the first object in oArea.<br/>
        ///  If no valid area is specified, it will use the caller's area.<br/>
        ///  - nObjectFilter: This allows you to filter out undesired object types, using bitwise "or".<br/>
        ///    For example, to return only creatures and doors, the value for this parameter would be OBJECT_TYPE_CREATURE | OBJECT_TYPE_DOOR<br/>
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetFirstObjectInArea(uint oArea = OBJECT_INVALID, ObjectType nObjectFilter = ObjectType.All)
        {
            return NWN.Core.NWScript.GetFirstObjectInArea(oArea, (int)nObjectFilter);
        }

        /// <summary>
        ///  Get the next object in oArea.<br/>
        ///  If no valid area is specified, it will use the caller's area.<br/>
        ///  - nObjectFilter: This allows you to filter out undesired object types, using bitwise "or".<br/>
        ///    For example, to return only creatures and doors, the value for this parameter would be OBJECT_TYPE_CREATURE | OBJECT_TYPE_DOOR<br/>
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetNextObjectInArea(uint oArea = OBJECT_INVALID, ObjectType nObjectFilter = ObjectType.All)
        {
            return NWN.Core.NWScript.GetNextObjectInArea(oArea, (int)nObjectFilter);
        }

        /// <summary>
        ///  Get the total from rolling (nNumDice x d2 dice).<br/>
        ///  - nNumDice: If this is less than 1, the value 1 will be used.
        /// </summary>
        public static int d2(int nNumDice = 1)
        {
            return NWN.Core.NWScript.d2(nNumDice);
        }

        /// <summary>
        ///  Get the total from rolling (nNumDice x d3 dice).<br/>
        ///  - nNumDice: If this is less than 1, the value 1 will be used.
        /// </summary>
        public static int d3(int nNumDice = 1)
        {
            return NWN.Core.NWScript.d3(nNumDice);
        }

        /// <summary>
        ///  Get the total from rolling (nNumDice x d4 dice).<br/>
        ///  - nNumDice: If this is less than 1, the value 1 will be used.
        /// </summary>
        public static int d4(int nNumDice = 1)
        {
            return NWN.Core.NWScript.d4(nNumDice);
        }

        /// <summary>
        ///  Get the total from rolling (nNumDice x d6 dice).<br/>
        ///  - nNumDice: If this is less than 1, the value 1 will be used.
        /// </summary>
        public static int d6(int nNumDice = 1)
        {
            return NWN.Core.NWScript.d6(nNumDice);
        }

        /// <summary>
        ///  Get the total from rolling (nNumDice x d8 dice).<br/>
        ///  - nNumDice: If this is less than 1, the value 1 will be used.
        /// </summary>
        public static int d8(int nNumDice = 1)
        {
            return NWN.Core.NWScript.d8(nNumDice);
        }

        /// <summary>
        ///  Get the total from rolling (nNumDice x d10 dice).<br/>
        ///  - nNumDice: If this is less than 1, the value 1 will be used.
        /// </summary>
        public static int d10(int nNumDice = 1)
        {
            return NWN.Core.NWScript.d10(nNumDice);
        }

        /// <summary>
        ///  Get the total from rolling (nNumDice x d12 dice).<br/>
        ///  - nNumDice: If this is less than 1, the value 1 will be used.
        /// </summary>
        public static int d12(int nNumDice = 1)
        {
            return NWN.Core.NWScript.d12(nNumDice);
        }

        /// <summary>
        ///  Get the total from rolling (nNumDice x d20 dice).<br/>
        ///  - nNumDice: If this is less than 1, the value 1 will be used.
        /// </summary>
        public static int d20(int nNumDice = 1)
        {
            return NWN.Core.NWScript.d20(nNumDice);
        }

        /// <summary>
        ///  Get the total from rolling (nNumDice x d100 dice).<br/>
        ///  - nNumDice: If this is less than 1, the value 1 will be used.
        /// </summary>
        public static int d100(int nNumDice = 1)
        {
            return NWN.Core.NWScript.d100(nNumDice);
        }

        /// <summary>
        ///  Get the magnitude of vVector; this can be used to determine the<br/>
        ///  distance between two points.<br/>
        ///  * Return value on error: 0.0f
        /// </summary>
        public static float VectorMagnitude(Vector3 vVector)
        {
            return NWN.Core.NWScript.VectorMagnitude(vVector);
        }

        /// <summary>
        ///  Get the metamagic type (METAMAGIC_*) of the last spell cast by the caller<br/>
        ///  * Return value if the caster is not a valid object: -1
        /// </summary>
        public static MetamagicType GetMetaMagicFeat()
        {
            return (MetamagicType)NWN.Core.NWScript.GetMetaMagicFeat();
        }

        /// <summary>
        ///  Get the object type (OBJECT_TYPE_*) of oTarget<br/>
        ///  * Return value if oTarget is not a valid object: -1
        /// </summary>
        public static ObjectType GetObjectType(uint oTarget)
        {
            return (ObjectType)NWN.Core.NWScript.GetObjectType(oTarget);
        }

        /// <summary>
        ///  Get the racial type (RACIAL_TYPE_*) of oCreature<br/>
        ///  * Return value if oCreature is not a valid creature: RACIAL_TYPE_INVALID
        /// </summary>
        public static RacialType GetRacialType(uint oCreature)
        {
            return (RacialType)NWN.Core.NWScript.GetRacialType(oCreature);
        }

        /// <summary>
        ///  Do a Fortitude Save check for the given DC<br/>
        ///  - oCreature<br/>
        ///  - nDC: Difficulty check<br/>
        ///  - nSaveType: SAVING_THROW_TYPE_*<br/>
        ///  - oSaveVersus<br/>
        ///  Returns: 0 if the saving throw roll failed<br/>
        ///  Returns: 1 if the saving throw roll succeeded<br/>
        ///  Returns: 2 if the target was immune to the save type specified<br/>
        ///  Note: If used within an Area of Effect Object Script (On Enter, OnExit, OnHeartbeat), you MUST pass<br/>
        ///  GetAreaOfEffectCreator() into oSaveVersus!!
        /// </summary>
        public static int FortitudeSave(uint oCreature, int nDC, SavingThrowType nSaveType = SavingThrowType.None, uint oSaveVersus = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.FortitudeSave(oCreature, nDC, (int)nSaveType, oSaveVersus);
        }

        /// <summary>
        ///  Does a Reflex Save check for the given DC<br/>
        ///  - oCreature<br/>
        ///  - nDC: Difficulty check<br/>
        ///  - nSaveType: SAVING_THROW_TYPE_*<br/>
        ///  - oSaveVersus<br/>
        ///  Returns: 0 if the saving throw roll failed<br/>
        ///  Returns: 1 if the saving throw roll succeeded<br/>
        ///  Returns: 2 if the target was immune to the save type specified<br/>
        ///  Note: If used within an Area of Effect Object Script (On Enter, OnExit, OnHeartbeat), you MUST pass<br/>
        ///  GetAreaOfEffectCreator() into oSaveVersus!!
        /// </summary>
        public static int ReflexSave(uint oCreature, int nDC, SavingThrowType nSaveType = SavingThrowType.None, uint oSaveVersus = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.ReflexSave(oCreature, nDC, (int)nSaveType, oSaveVersus);
        }

        /// <summary>
        ///  Does a Will Save check for the given DC<br/>
        ///  - oCreature<br/>
        ///  - nDC: Difficulty check<br/>
        ///  - nSaveType: SAVING_THROW_TYPE_*<br/>
        ///  - oSaveVersus<br/>
        ///  Returns: 0 if the saving throw roll failed<br/>
        ///  Returns: 1 if the saving throw roll succeeded<br/>
        ///  Returns: 2 if the target was immune to the save type specified<br/>
        ///  Note: If used within an Area of Effect Object Script (On Enter, OnExit, OnHeartbeat), you MUST pass<br/>
        ///  GetAreaOfEffectCreator() into oSaveVersus!!
        /// </summary>
        public static int WillSave(uint oCreature, int nDC, SavingThrowType nSaveType = SavingThrowType.None, uint oSaveVersus = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.WillSave(oCreature, nDC, (int)nSaveType, oSaveVersus);
        }

        /// <summary>
        ///  Get the DC to save against for a spell (10 + spell level + relevant ability<br/>
        ///  bonus).  This can be called by a creature or by an Area of Effect object.
        /// </summary>
        public static int GetSpellSaveDC()
        {
            return NWN.Core.NWScript.GetSpellSaveDC();
        }

        /// <summary>
        ///  Set the subtype of eEffect to Magical and return eEffect.<br/>
        ///  (Effects default to magical if the subtype is not set)<br/>
        ///  Magical effects are removed by resting, and by dispel magic
        /// </summary>
        public static Effect MagicalEffect(Effect eEffect)
        {
            return NWN.Core.NWScript.MagicalEffect(eEffect);
        }

        /// <summary>
        ///  Set the subtype of eEffect to Supernatural and return eEffect.<br/>
        ///  (Effects default to magical if the subtype is not set)<br/>
        ///  Permanent supernatural effects are not removed by resting
        /// </summary>
        public static Effect SupernaturalEffect(Effect eEffect)
        {
            return NWN.Core.NWScript.SupernaturalEffect(eEffect);
        }

        /// <summary>
        ///  Set the subtype of eEffect to Extraordinary and return eEffect.<br/>
        ///  (Effects default to magical if the subtype is not set)<br/>
        ///  Extraordinary effects are removed by resting, but not by dispel magic
        /// </summary>
        public static Effect ExtraordinaryEffect(Effect eEffect)
        {
            return NWN.Core.NWScript.ExtraordinaryEffect(eEffect);
        }

        /// <summary>
        ///  Create an AC Increase effect<br/>
        ///  - nValue: size of AC increase<br/>
        ///  - nModifyType: AC_*_BONUS<br/>
        ///  - nDamageType: DAMAGE_TYPE_*<br/>
        ///    * Default value for nDamageType should only ever be used in this function prototype.
        /// </summary>
        public static Effect EffectACIncrease(int nValue, ACBonusType nModifyType = ACBonusType.Dodge, ACType nDamageType = ACType.VsDamageTypeAll)
        {
            return NWN.Core.NWScript.EffectACIncrease(nValue, (int)nModifyType, (int)nDamageType);
        }

        /// <summary>
        ///  If oObject is a creature, this will return that creature's armour class<br/>
        ///  If oObject is an item, door or placeable, this will return zero.<br/>
        ///  - nForFutureUse: this parameter is not currently used<br/>
        ///  * Return value if oObject is not a creature, item, door or placeable: -1
        /// </summary>
        public static int GetAC(uint oObject, int nForFutureUse = 0)
        {
            return NWN.Core.NWScript.GetAC(oObject, nForFutureUse);
        }

        /// <summary>
        ///  Create a Saving Throw Increase effect<br/>
        ///  - nSave: SAVING_THROW_* (not SAVING_THROW_TYPE_*)<br/>
        ///           SAVING_THROW_ALL<br/>
        ///           SAVING_THROW_FORT<br/>
        ///           SAVING_THROW_REFLEX<br/>
        ///           SAVING_THROW_WILL<br/>
        ///  - nValue: size of the Saving Throw increase<br/>
        ///  - nSaveType: SAVING_THROW_TYPE_* (e.g. SAVING_THROW_TYPE_ACID )
        /// </summary>
        public static Effect EffectSavingThrowIncrease(SavingThrowType nSave, int nValue, SavingThrowCategoryType nSaveType = SavingThrowCategoryType.All)
        {
            return NWN.Core.NWScript.EffectSavingThrowIncrease((int)nSave, nValue, (int)nSaveType);
        }

        /// <summary>
        ///  Create an Attack Increase effect<br/>
        ///  - nBonus: size of attack bonus<br/>
        ///  - nModifierType: ATTACK_BONUS_*
        /// </summary>
        public static Effect EffectAttackIncrease(int nBonus, AttackBonusType nModifierType = AttackBonusType.Misc)
        {
            return NWN.Core.NWScript.EffectAttackIncrease(nBonus, (int)nModifierType);
        }

        /// <summary>
        ///  Create a Damage Reduction effect<br/>
        ///  - nAmount: amount of damage reduction<br/>
        ///  - nDamagePower: DAMAGE_POWER_*<br/>
        ///  - nLimit: How much damage the effect can absorb before disappearing.<br/>
        ///    Set to zero for infinite<br/>
        ///  - bRangedOnly: Set to true to have this reduction only apply to ranged attacks 
        /// </summary>
        public static Effect EffectDamageReduction(int nAmount, int nDamagePower, int nLimit = 0, bool bRangedOnly = false)
        {
            return NWN.Core.NWScript.EffectDamageReduction(nAmount, nDamagePower, nLimit, bRangedOnly ? 1 : 0);
        }

        /// <summary>
        ///  Create a Damage Increase effect<br/>
        ///  - nBonus: DAMAGE_BONUS_*<br/>
        ///  - nDamageType: DAMAGE_TYPE_*<br/>
        ///  NOTE! You *must* use the DAMAGE_BONUS_* constants! Using other values may<br/>
        ///        result in odd behaviour.
        /// </summary>
        public static Effect EffectDamageIncrease(int nBonus, DamageType nDamageType = DamageType.Magical)
        {
            return NWN.Core.NWScript.EffectDamageIncrease(nBonus, (int)nDamageType);
        }

        /// <summary>
        ///  Convert nRounds into a number of seconds<br/>
        ///  A round is always 6.0 seconds
        /// </summary>
        public static float RoundsToSeconds(int nRounds)
        {
            return NWN.Core.NWScript.RoundsToSeconds(nRounds);
        }

        /// <summary>
        ///  Convert nHours into a number of seconds<br/>
        ///  The result will depend on how many minutes there are per hour (game-time)
        /// </summary>
        public static float HoursToSeconds(int nHours)
        {
            return NWN.Core.NWScript.HoursToSeconds(nHours);
        }

        /// <summary>
        ///  Convert nTurns into a number of seconds<br/>
        ///  A turn is always 60.0 seconds
        /// </summary>
        public static float TurnsToSeconds(int nTurns)
        {
            return NWN.Core.NWScript.TurnsToSeconds(nTurns);
        }

        /// <summary>
        ///  Get an integer between 0 and 100 (inclusive) to represent oCreature's<br/>
        ///  Law/Chaos alignment<br/>
        ///  (100=law, 0=chaos)<br/>
        ///  * Return value if oCreature is not a valid creature: -1
        /// </summary>
        public static int GetLawChaosValue(uint oCreature)
        {
            return NWN.Core.NWScript.GetLawChaosValue(oCreature);
        }

        /// <summary>
        ///  Get an integer between 0 and 100 (inclusive) to represent oCreature's<br/>
        ///  Good/Evil alignment<br/>
        ///  (100=good, 0=evil)<br/>
        ///  * Return value if oCreature is not a valid creature: -1
        /// </summary>
        public static int GetGoodEvilValue(uint oCreature)
        {
            return NWN.Core.NWScript.GetGoodEvilValue(oCreature);
        }

        /// <summary>
        ///  Return an ALIGNMENT_* constant to represent oCreature's law/chaos alignment<br/>
        ///  * Return value if oCreature is not a valid creature: -1
        /// </summary>
        public static int GetAlignmentLawChaos(uint oCreature)
        {
            return NWN.Core.NWScript.GetAlignmentLawChaos(oCreature);
        }

        /// <summary>
        ///  Return an ALIGNMENT_* constant to represent oCreature's good/evil alignment<br/>
        ///  * Return value if oCreature is not a valid creature: -1
        /// </summary>
        public static int GetAlignmentGoodEvil(uint oCreature)
        {
            return NWN.Core.NWScript.GetAlignmentGoodEvil(oCreature);
        }

        /// <summary>
        ///  Get the first object in nShape<br/>
        ///  - nShape: SHAPE_*<br/>
        ///  - fSize:<br/>
        ///    -> If nShape == SHAPE_SPHERE, this is the radius of the sphere<br/>
        ///    -> If nShape == SHAPE_SPELLCYLINDER, this is the length of the cylinder<br/>
        ///       Spell Cylinder's always have a radius of 1.5m.<br/>
        ///    -> If nShape == SHAPE_CONE, this is the widest radius of the cone<br/>
        ///    -> If nShape == SHAPE_SPELLCONE, this is the length of the cone in the<br/>
        ///       direction of lTarget. Spell cones are always 60 degrees with the origin<br/>
        ///       at OBJECT_SELF.<br/>
        ///    -> If nShape == SHAPE_CUBE, this is half the length of one of the sides of<br/>
        ///       the cube<br/>
        ///  - lTarget: This is the centre of the effect, usually GetSpellTargetLocation(),<br/>
        ///    or the end of a cylinder or cone.<br/>
        ///  - bLineOfSight: This controls whether to do a line-of-sight check on the<br/>
        ///    object returned. Line of sight check is done from origin to target object<br/>
        ///    at a height 1m above the ground<br/>
        ///    (This can be used to ensure that spell effects do not go through walls.)<br/>
        ///  - nObjectFilter: This allows you to filter out undesired object types, using<br/>
        ///    bitwise "or".<br/>
        ///    For example, to return only creatures and doors, the value for<br/>
        ///    this parameter would be OBJECT_TYPE_CREATURE | OBJECT_TYPE_DOOR<br/>
        ///  - vOrigin: This is only used for cylinders and cones, and specifies the<br/>
        ///    origin of the effect(normally the spell-caster's position).<br/>
        ///  Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetFirstObjectInShape(int nShape, float fSize, Location lTarget, bool bLineOfSight = false, ObjectType nObjectFilter = ObjectType.Creature, Vector3 vOrigin = default)
        {
            return NWN.Core.NWScript.GetFirstObjectInShape(nShape, fSize, lTarget, bLineOfSight ? 1 : 0, (int)nObjectFilter, vOrigin);
        }

        /// <summary>
        ///  Get the next object in nShape<br/>
        ///  - nShape: SHAPE_*<br/>
        ///  - fSize:<br/>
        ///    -> If nShape == SHAPE_SPHERE, this is the radius of the sphere<br/>
        ///    -> If nShape == SHAPE_SPELLCYLINDER, this is the length of the cylinder.<br/>
        ///       Spell Cylinder's always have a radius of 1.5m.<br/>
        ///    -> If nShape == SHAPE_CONE, this is the widest radius of the cone<br/>
        ///    -> If nShape == SHAPE_SPELLCONE, this is the length of the cone in the<br/>
        ///       direction of lTarget. Spell cones are always 60 degrees with the origin<br/>
        ///       at OBJECT_SELF.<br/>
        ///    -> If nShape == SHAPE_CUBE, this is half the length of one of the sides of<br/>
        ///       the cube<br/>
        ///  - lTarget: This is the centre of the effect, usually GetSpellTargetLocation(),<br/>
        ///    or the end of a cylinder or cone.<br/>
        ///  - bLineOfSight: This controls whether to do a line-of-sight check on the<br/>
        ///    object returned. (This can be used to ensure that spell effects do not go<br/>
        ///    through walls.) Line of sight check is done from origin to target object<br/>
        ///    at a height 1m above the ground<br/>
        ///  - nObjectFilter: This allows you to filter out undesired object types, using<br/>
        ///    bitwise "or". For example, to return only creatures and doors, the value for<br/>
        ///    this parameter would be OBJECT_TYPE_CREATURE | OBJECT_TYPE_DOOR<br/>
        ///  - vOrigin: This is only used for cylinders and cones, and specifies the origin<br/>
        ///    of the effect (normally the spell-caster's position).<br/>
        ///  Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetNextObjectInShape(int nShape, float fSize, Location lTarget, bool bLineOfSight = false, ObjectType nObjectFilter = ObjectType.Creature, Vector3 vOrigin = default)
        {
            return NWN.Core.NWScript.GetNextObjectInShape(nShape, fSize, lTarget, bLineOfSight ? 1 : 0, (int)nObjectFilter, vOrigin);
        }

        /// <summary>
        ///  Create an Entangle effect<br/>
        ///  When applied, this effect will restrict the creature's movement and apply a<br/>
        ///  (-2) to all attacks and a -4 to AC.
        /// </summary>
        public static Effect EffectEntangle()
        {
            return NWN.Core.NWScript.EffectEntangle();
        }
        /// <summary>
        ///  Causes object oObject to run the event evToRun. The script on the object that is<br/>
        ///  associated with the event specified will run.<br/>
        ///  Events can be created using the following event functions:<br/>
        ///     EventActivateItem() - This creates an OnActivateItem module event. The script for handling<br/>
        ///                           this event can be set in Module Properties on the Event Tab.<br/>
        ///     EventConversation() - This creates on OnConversation creature event. The script for handling<br/>
        ///                           this event can be set by viewing the Creature Properties on a<br/>
        ///                           creature and then clicking on the Scripts Tab.<br/>
        ///     EventSpellCastAt()  - This creates an OnSpellCastAt event. The script for handling this<br/>
        ///                           event can be set in the Scripts Tab of the Properties menu<br/>
        ///                           for the object.<br/>
        ///     EventUserDefined()  - This creates on OnUserDefined event. The script for handling this event<br/>
        ///                           can be set in the Scripts Tab of the Properties menu for the object/area/module.
        /// </summary>
        public static void SignalEvent(uint oObject, Event evToRun)
        {
            NWN.Core.NWScript.SignalEvent(oObject, evToRun);
        }

        /// <summary>
        ///  Create an event of the type nUserDefinedEventNumber<br/>
        ///  Note: This only creates the event. The event wont actually trigger until SignalEvent()<br/>
        ///  is called using this created UserDefined event as an argument.<br/>
        ///  For example:<br/>
        ///      SignalEvent(oObject, EventUserDefined(9999));<br/>
        ///  Once the event has been signaled. The script associated with the OnUserDefined event will<br/>
        ///  run on the object oObject.<br/>
        /// <br/>
        ///  To specify the OnUserDefined script that should run, view the object's Properties<br/>
        ///  and click on the Scripts Tab. Then specify a script for the OnUserDefined event.<br/>
        ///  From inside the OnUserDefined script call:<br/>
        ///     GetUserDefinedEventNumber() to retrieve the value of nUserDefinedEventNumber<br/>
        ///     that was used when the event was signaled.
        /// </summary>
        public static Event EventUserDefined(int nUserDefinedEventNumber)
        {
            return NWN.Core.NWScript.EventUserDefined(nUserDefinedEventNumber);
        }

        /// <summary>
        ///  Create a Death effect<br/>
        ///  - nSpectacularDeath: if this is true, the creature to which this effect is<br/>
        ///    applied will die in an extraordinary fashion<br/>
        ///  - nDisplayFeedback
        /// </summary>
        public static Effect EffectDeath(bool nSpectacularDeath = false, bool nDisplayFeedback = true)
        {
            return NWN.Core.NWScript.EffectDeath(nSpectacularDeath ? 1 : 0, nDisplayFeedback ? 1 : 0);
        }

        /// <summary>
        ///  Create a Knockdown effect<br/>
        ///  This effect knocks creatures off their feet, they will sit until the effect<br/>
        ///  is removed. This should be applied as a temporary effect with a 3 second<br/>
        ///  duration minimum (1 second to fall, 1 second sitting, 1 second to get up).
        /// </summary>
        public static Effect EffectKnockdown()
        {
            return NWN.Core.NWScript.EffectKnockdown();
        }

        /// <summary>
        ///  Give oItem to oGiveTo<br/>
        ///  If oItem is not a valid item, or oGiveTo is not a valid object, nothing will<br/>
        ///  happen.
        /// </summary>
        public static void ActionGiveItem(uint oItem, uint oGiveTo)
        {
            NWN.Core.NWScript.ActionGiveItem(oItem, oGiveTo);
        }

        /// <summary>
        ///  Take oItem from oTakeFrom<br/>
        ///  If oItem is not a valid item, or oTakeFrom is not a valid object, nothing<br/>
        ///  will happen.
        /// </summary>
        public static void ActionTakeItem(uint oItem, uint oTakeFrom)
        {
            NWN.Core.NWScript.ActionTakeItem(oItem, oTakeFrom);
        }

        /// <summary>
        ///  Normalize vVector
        /// </summary>
        public static Vector3 VectorNormalize(Vector3 vVector)
        {
            return NWN.Core.NWScript.VectorNormalize(vVector);
        }

        /// <summary>
        ///  Create a Curse effect.<br/>
        ///  - nStrMod: strength modifier<br/>
        ///  - nDexMod: dexterity modifier<br/>
        ///  - nConMod: constitution modifier<br/>
        ///  - nIntMod: intelligence modifier<br/>
        ///  - nWisMod: wisdom modifier<br/>
        ///  - nChaMod: charisma modifier
        /// </summary>
        public static Effect EffectCurse(int nStrMod = 1, int nDexMod = 1, int nConMod = 1, int nIntMod = 1, int nWisMod = 1, int nChaMod = 1)
        {
            return NWN.Core.NWScript.EffectCurse(nStrMod, nDexMod, nConMod, nIntMod, nWisMod, nChaMod);
        }

        /// <summary>
        ///  Get the ability score of type nAbility for a creature (otherwise 0)<br/>
        ///  - oCreature: the creature whose ability score we wish to find out<br/>
        ///  - nAbilityType: ABILITY_*<br/>
        ///  - nBaseAbilityScore: if set to true will return the base ability score without<br/>
        ///                       bonuses (e.g. ability bonuses granted from equipped items).<br/>
        ///  Return value on error: 0
        /// </summary>
        public static int GetAbilityScore(uint oCreature, AbilityType nAbilityType, bool nBaseAbilityScore = false)
        {
            return NWN.Core.NWScript.GetAbilityScore(oCreature, (int)nAbilityType, nBaseAbilityScore ? 1 : 0);
        }

        /// <summary>
        ///  * Returns true if oCreature is a dead NPC, dead PC or a dying PC.
        /// </summary>
        public static bool GetIsDead(uint oCreature)
        {
            return NWN.Core.NWScript.GetIsDead(oCreature) == 1;
        }

        /// <summary>
        ///  Output vVector to the logfile.<br/>
        ///  - vVector<br/>
        ///  - bPrepend: if this is true, the message will be prefixed with "PRINTVECTOR:"
        /// </summary>
        public static void PrintVector(Vector3 vVector, bool bPrepend)
        {
            NWN.Core.NWScript.PrintVector(vVector, bPrepend ? 1 : 0);
        }

        /// <summary>
        ///  Create a vector with the specified values for x, y and z
        /// </summary>
        public static Vector3 Vector(float x = 0.0f, float y = 0.0f, float z = 0.0f)
        {
            return NWN.Core.NWScript.Vector(x, y, z);
        }

        /// <summary>
        ///  Cause the caller to face vTarget
        /// </summary>
        public static void SetFacingPoint(Vector3 vTarget)
        {
            NWN.Core.NWScript.SetFacingPoint(vTarget);
        }

        /// <summary>
        ///  Convert fAngle to a vector
        /// </summary>
        public static Vector3 AngleToVector(float fAngle)
        {
            return NWN.Core.NWScript.AngleToVector(fAngle);
        }

        /// <summary>
        ///  Convert vVector to an angle
        /// </summary>
        public static float VectorToAngle(Vector3 vVector)
        {
            return NWN.Core.NWScript.VectorToAngle(vVector);
        }

        /// <summary>
        ///  The caller will perform a Melee Touch Attack on oTarget<br/>
        ///  This is not an action, and it assumes the caller is already within range of<br/>
        ///  oTarget<br/>
        ///  * Returns 0 on a miss, 1 on a hit and 2 on a critical hit
        /// </summary>
        public static TouchAttackResultType TouchAttackMelee(uint oTarget, bool bDisplayFeedback = true)
        {
            return (TouchAttackResultType)NWN.Core.NWScript.TouchAttackMelee(oTarget, bDisplayFeedback ? 1 : 0);
        }

        /// <summary>
        ///  The caller will perform a Ranged Touch Attack on oTarget<br/>
        ///  * Returns 0 on a miss, 1 on a hit and 2 on a critical hit
        /// </summary>
        public static TouchAttackResultType TouchAttackRanged(uint oTarget, bool bDisplayFeedback = true)
        {
            return (TouchAttackResultType)NWN.Core.NWScript.TouchAttackRanged(oTarget, bDisplayFeedback ? 1 : 0);
        }

        /// <summary>
        ///  Create a Paralyze effect
        /// </summary>
        public static Effect EffectParalyze()
        {
            return NWN.Core.NWScript.EffectParalyze();
        }

        /// <summary>
        ///  Create a Spell Immunity effect.<br/>
        ///  There is a known bug with this function. There *must* be a parameter specified<br/>
        ///  when this is called (even if the desired parameter is SPELL_ALL_SPELLS),<br/>
        ///  otherwise an effect of type EFFECT_TYPE_INVALIDEFFECT will be returned.<br/>
        ///  - nImmunityToSpell: SPELL_*<br/>
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nImmunityToSpell is<br/>
        ///    invalid.
        /// </summary>
        public static Effect EffectSpellImmunity(SpellType nImmunityToSpell = SpellType.AllSpells)
        {
            return NWN.Core.NWScript.EffectSpellImmunity((int)nImmunityToSpell);
        }

        /// <summary>
        ///  Create a Deaf effect
        /// </summary>
        public static Effect EffectDeaf()
        {
            return NWN.Core.NWScript.EffectDeaf();
        }

        /// <summary>
        ///  Get the distance in metres between oObjectA and oObjectB.<br/>
        ///  * Return value if either object is invalid: 0.0f
        /// </summary>
        public static float GetDistanceBetween(uint oObjectA, uint oObjectB)
        {
            return NWN.Core.NWScript.GetDistanceBetween(oObjectA, oObjectB);
        }

        /// <summary>
        ///  Set oObject's local location variable sVarname to lValue
        /// </summary>
        public static void SetLocalLocation(uint oObject, string sVarName, Location lValue)
        {
            NWN.Core.NWScript.SetLocalLocation(oObject, sVarName, lValue);
        }

        /// <summary>
        ///  Get oObject's local location variable sVarname
        /// </summary>
        public static Location GetLocalLocation(uint oObject, string sVarName)
        {
            return NWN.Core.NWScript.GetLocalLocation(oObject, sVarName);
        }

        /// <summary>
        ///  Create a Sleep effect
        /// </summary>
        public static Effect EffectSleep()
        {
            return NWN.Core.NWScript.EffectSleep();
        }

        /// <summary>
        ///  Get the object which is in oCreature's specified inventory slot<br/>
        ///  - nInventorySlot: INVENTORY_SLOT_*<br/>
        ///  - oCreature<br/>
        ///  * Returns OBJECT_INVALID if oCreature is not a valid creature or there is no<br/>
        ///    item in nInventorySlot.
        /// </summary>
        public static uint GetItemInSlot(InventorySlotType nInventorySlot, uint oCreature = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetItemInSlot((int)nInventorySlot, oCreature);
        }

        /// <summary>
        ///  Create a Charm effect
        /// </summary>
        public static Effect EffectCharmed()
        {
            return NWN.Core.NWScript.EffectCharmed();
        }

        /// <summary>
        ///  Create a Confuse effect
        /// </summary>
        public static Effect EffectConfused()
        {
            return NWN.Core.NWScript.EffectConfused();
        }

        /// <summary>
        ///  Create a Frighten effect
        /// </summary>
        public static Effect EffectFrightened()
        {
            return NWN.Core.NWScript.EffectFrightened();
        }

        /// <summary>
        ///  Create a Dominate effect
        /// </summary>
        public static Effect EffectDominated()
        {
            return NWN.Core.NWScript.EffectDominated();
        }

        /// <summary>
        ///  Create a Daze effect
        /// </summary>
        public static Effect EffectDazed()
        {
            return NWN.Core.NWScript.EffectDazed();
        }

        /// <summary>
        ///  Create a Stun effect
        /// </summary>
        public static Effect EffectStunned()
        {
            return NWN.Core.NWScript.EffectStunned();
        }

        /// <summary>
        ///  Set whether oTarget's action stack can be modified
        /// </summary>
        public static void SetCommandable(bool bCommandable, uint oTarget = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetCommandable(bCommandable ? 1 : 0, oTarget);
        }
        /// <summary>
        ///  Determine whether oTarget's action stack can be modified.
        /// </summary>
        public static bool GetCommandable(uint oTarget = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetCommandable(oTarget) == 1;
        }

        /// <summary>
        ///  Create a Regenerate effect.<br/>
        ///  - nAmount: amount of damage to be regenerated per time interval<br/>
        ///  - fIntervalSeconds: length of interval in seconds
        /// </summary>
        public static Effect EffectRegenerate(int nAmount, float fIntervalSeconds)
        {
            return NWN.Core.NWScript.EffectRegenerate(nAmount, fIntervalSeconds);
        }

        /// <summary>
        ///  Create a Movement Speed Increase effect.<br/>
        ///  - nPercentChange - range 0 through 99<br/>
        ///  eg.<br/>
        ///     0 = no change in speed<br/>
        ///    50 = 50% faster<br/>
        ///    99 = almost twice as fast
        /// </summary>
        public static Effect EffectMovementSpeedIncrease(int nPercentChange)
        {
            return NWN.Core.NWScript.EffectMovementSpeedIncrease(nPercentChange);
        }

        /// <summary>
        ///  Get the number of hitdice for oCreature.<br/>
        ///  * Return value if oCreature is not a valid creature: 0
        /// </summary>
        public static int GetHitDice(uint oCreature)
        {
            return NWN.Core.NWScript.GetHitDice(oCreature);
        }

        /// <summary>
        ///  The action subject will follow oFollow until a ClearAllActions() is called.<br/>
        ///  - oFollow: this is the object to be followed<br/>
        ///  - fFollowDistance: follow distance in metres<br/>
        ///  * No return value
        /// </summary>
        public static void ActionForceFollowObject(uint oFollow, float fFollowDistance = 0.0f)
        {
            NWN.Core.NWScript.ActionForceFollowObject(oFollow, fFollowDistance);
        }

        /// <summary>
        ///  Get the Tag of oObject<br/>
        ///  * Return value if oObject is not a valid object: ""
        /// </summary>
        public static string GetTag(uint oObject)
        {
            return NWN.Core.NWScript.GetTag(oObject);
        }

        /// <summary>
        ///  Do a Spell Resistance check between oCaster and oTarget, returning true if<br/>
        ///  the spell was resisted.<br/>
        ///  * Return value if oCaster or oTarget is an invalid object: false<br/>
        ///  * Return value if spell cast is not a player spell: - 1<br/>
        ///  * Return value if spell resisted: 1<br/>
        ///  * Return value if spell resisted via magic immunity: 2<br/>
        ///  * Return value if spell resisted via spell absorption: 3
        /// </summary>
        public static int ResistSpell(uint oCaster, uint oTarget)
        {
            return NWN.Core.NWScript.ResistSpell(oCaster, oTarget);
        }
        /// <summary>
        ///  Get the effect type (EFFECT_TYPE_*) of eEffect.<br/>
        ///  - bAllTypes: Set to true to return additional values the game used to return EFFECT_INVALIDEFFECT for, specifically:<br/>
        ///   EFFECT_TYPE: APPEAR, CUTSCENE_DOMINATED, DAMAGE, DEATH, DISAPPEAR, HEAL, HITPOINTCHANGEWHENDYING, KNOCKDOWN, MODIFYNUMATTACKS, SUMMON_CREATURE, TAUNT, WOUNDING<br/>
        ///  * Return value if eEffect is invalid: EFFECT_INVALIDEFFECT
        /// </summary>
        public static EffectType GetEffectType(Effect eEffect, bool bAllTypes = false)
        {
            return (EffectType)NWN.Core.NWScript.GetEffectType(eEffect, bAllTypes ? 1 : 0);
        }

        /// <summary>
        ///  Create an Area Of Effect effect in the area of the creature it is applied to.<br/>
        ///  If the scripts are not specified, default ones will be used.
        /// </summary>
        public static Effect EffectAreaOfEffect(int nAreaEffectId, string sOnEnterScript = "", string sHeartbeatScript = "", string sOnExitScript = "")
        {
            return NWN.Core.NWScript.EffectAreaOfEffect(nAreaEffectId, sOnEnterScript, sHeartbeatScript, sOnExitScript);
        }
        /// <summary>
        ///  * Returns true if the Faction Ids of the two objects are the same
        /// </summary>
        public static bool GetFactionEqual(uint oFirstObject, uint oSecondObject = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetFactionEqual(oFirstObject, oSecondObject) == 1;
        }

        /// <summary>
        ///  Make oObjectToChangeFaction join the faction of oMemberOfFactionToJoin.<br/>
        ///  NB. ** This will only work for two NPCs **
        /// </summary>
        public static void ChangeFaction(uint oObjectToChangeFaction, uint oMemberOfFactionToJoin)
        {
            NWN.Core.NWScript.ChangeFaction(oObjectToChangeFaction, oMemberOfFactionToJoin);
        }

        /// <summary>
        ///  * Returns true if oObject is listening for something
        /// </summary>
        public static bool GetIsListening(uint oObject)
        {
            return NWN.Core.NWScript.GetIsListening(oObject) == 1;
        }

        /// <summary>
        ///  Set whether oObject is listening.
        /// </summary>
        public static void SetListening(uint oObject, bool bValue)
        {
            NWN.Core.NWScript.SetListening(oObject, bValue ? 1 : 0);
        }

        /// <summary>
        ///  Set the string for oObject to listen for.<br/>
        ///  Note: this does not set oObject to be listening.
        /// </summary>
        public static void SetListenPattern(uint oObject, string sPattern, int nNumber = 0)
        {
            NWN.Core.NWScript.SetListenPattern(oObject, sPattern, nNumber);
        }

        /// <summary>
        ///  * Returns true if sStringToTest matches sPattern.
        /// </summary>
        public static bool TestStringAgainstPattern(string sPattern, string sStringToTest)
        {
            return NWN.Core.NWScript.TestStringAgainstPattern(sPattern, sStringToTest) == 1;
        }

        /// <summary>
        ///  Get the appropriate matched string (this should only be used in<br/>
        ///  OnConversation scripts).<br/>
        ///  * Returns the appropriate matched string, otherwise returns ""
        /// </summary>
        public static string GetMatchedSubstring(int nString)
        {
            return NWN.Core.NWScript.GetMatchedSubstring(nString);
        }

        /// <summary>
        ///  Get the number of string parameters available.<br/>
        ///  * Returns -1 if no string matched (this could be because of a dialogue event)
        /// </summary>
        public static int GetMatchedSubstringsCount()
        {
            return NWN.Core.NWScript.GetMatchedSubstringsCount();
        }

        /// <summary>
        ///  * Create a Visual Effect that can be applied to an object.<br/>
        ///  - nVisualEffectId<br/>
        ///  - nMissEffect: if this is true, a random vector near or past the target will<br/>
        ///    be generated, on which to play the effect
        /// </summary>
        public static Effect EffectVisualEffect(VisualEffectType nVisualEffectId, bool nMissEffect = false, float fScale = 1.0f, Vector3 vTranslate = default, Vector3 vRotate = default)
        {
            return NWN.Core.NWScript.EffectVisualEffect((int)nVisualEffectId, nMissEffect ? 1 : 0, fScale, vTranslate, vRotate);
        }

        /// <summary>
        ///  Get the weakest member of oFactionMember's faction.<br/>
        ///  * Returns OBJECT_INVALID if oFactionMember's faction is invalid.
        /// </summary>
        public static uint GetFactionWeakestMember(uint oFactionMember = OBJECT_INVALID, bool bMustBeVisible = true)
        {
            return NWN.Core.NWScript.GetFactionWeakestMember(oFactionMember, bMustBeVisible ? 1 : 0);
        }

        /// <summary>
        ///  Get the strongest member of oFactionMember's faction.<br/>
        ///  * Returns OBJECT_INVALID if oFactionMember's faction is invalid.
        /// </summary>
        public static uint GetFactionStrongestMember(uint oFactionMember = OBJECT_INVALID, bool bMustBeVisible = true)
        {
            return NWN.Core.NWScript.GetFactionStrongestMember(oFactionMember, bMustBeVisible ? 1 : 0);
        }

        /// <summary>
        ///  Get the member of oFactionMember's faction that has taken the most hit points<br/>
        ///  of damage.<br/>
        ///  * Returns OBJECT_INVALID if oFactionMember's faction is invalid.
        /// </summary>
        public static uint GetFactionMostDamagedMember(uint oFactionMember = OBJECT_INVALID, bool bMustBeVisible = true)
        {
            return NWN.Core.NWScript.GetFactionMostDamagedMember(oFactionMember, bMustBeVisible ? 1 : 0);
        }

        /// <summary>
        ///  Get the member of oFactionMember's faction that has taken the fewest hit<br/>
        ///  points of damage.<br/>
        ///  * Returns OBJECT_INVALID if oFactionMember's faction is invalid.
        /// </summary>
        public static uint GetFactionLeastDamagedMember(uint oFactionMember = OBJECT_INVALID, bool bMustBeVisible = true)
        {
            return NWN.Core.NWScript.GetFactionLeastDamagedMember(oFactionMember, bMustBeVisible ? 1 : 0);
        }

        /// <summary>
        ///  Get the amount of gold held by oFactionMember's faction.<br/>
        ///  * Returns -1 if oFactionMember's faction is invalid.
        /// </summary>
        public static int GetFactionGold(uint oFactionMember)
        {
            return NWN.Core.NWScript.GetFactionGold(oFactionMember);
        }

        /// <summary>
        ///  Get an integer between 0 and 100 (inclusive) that represents how<br/>
        ///  oSourceFactionMember's faction feels about oTarget.<br/>
        ///  * Return value on error: -1
        /// </summary>
        public static int GetFactionAverageReputation(uint oSourceFactionMember, uint oTarget)
        {
            return NWN.Core.NWScript.GetFactionAverageReputation(oSourceFactionMember, oTarget);
        }

        /// <summary>
        ///  Get an integer between 0 and 100 (inclusive) that represents the average<br/>
        ///  good/evil alignment of oFactionMember's faction.<br/>
        ///  * Return value on error: -1
        /// </summary>
        public static int GetFactionAverageGoodEvilAlignment(uint oFactionMember)
        {
            return NWN.Core.NWScript.GetFactionAverageGoodEvilAlignment(oFactionMember);
        }

        /// <summary>
        ///  Get an integer between 0 and 100 (inclusive) that represents the average<br/>
        ///  law/chaos alignment of oFactionMember's faction.<br/>
        ///  * Return value on error: -1
        /// </summary>
        public static int GetFactionAverageLawChaosAlignment(uint oFactionMember)
        {
            return NWN.Core.NWScript.GetFactionAverageLawChaosAlignment(oFactionMember);
        }

        /// <summary>
        ///  Get the average level of the members of the faction.<br/>
        ///  * Return value on error: -1
        /// </summary>
        public static int GetFactionAverageLevel(uint oFactionMember)
        {
            return NWN.Core.NWScript.GetFactionAverageLevel(oFactionMember);
        }

        /// <summary>
        ///  Get the average XP of the members of the faction.<br/>
        ///  * Return value on error: -1
        /// </summary>
        public static int GetFactionAverageXP(uint oFactionMember)
        {
            return NWN.Core.NWScript.GetFactionAverageXP(oFactionMember);
        }

        /// <summary>
        ///  Get the most frequent class in the faction - this can be compared with the<br/>
        ///  constants CLASS_TYPE_*.<br/>
        ///  * Return value on error: -1
        /// </summary>
        public static ClassType GetFactionMostFrequentClass(uint oFactionMember)
        {
            return (ClassType)NWN.Core.NWScript.GetFactionMostFrequentClass(oFactionMember);
        }

        /// <summary>
        ///  Get the object faction member with the lowest armour class.<br/>
        ///  * Returns OBJECT_INVALID if oFactionMember's faction is invalid.
        /// </summary>
        public static uint GetFactionWorstAC(uint oFactionMember = OBJECT_INVALID, bool bMustBeVisible = true)
        {
            return NWN.Core.NWScript.GetFactionWorstAC(oFactionMember, bMustBeVisible ? 1 : 0);
        }

        /// <summary>
        ///  Get the object faction member with the highest armour class.<br/>
        ///  * Returns OBJECT_INVALID if oFactionMember's faction is invalid.
        /// </summary>
        public static uint GetFactionBestAC(uint oFactionMember = OBJECT_INVALID, bool bMustBeVisible = true)
        {
            return NWN.Core.NWScript.GetFactionBestAC(oFactionMember, bMustBeVisible ? 1 : 0);
        }

        /// <summary>
        ///  Sit in oChair.<br/>
        ///  Note: Not all creatures will be able to sit and not all<br/>
        ///        objects can be sat on.<br/>
        ///        The object oChair must also be marked as usable in the toolset.<br/>
        /// <br/>
        ///  For Example: To get a player to sit in oChair when they click on it,<br/>
        ///  place the following script in the OnUsed event for the object oChair.<br/>
        ///  void main()<br/>
        ///  {<br/>
        ///     object oChair = OBJECT_SELF;<br/>
        ///     AssignCommand(GetLastUsedBy(),ActionSit(oChair));<br/>
        ///  }
        /// </summary>
        public static void ActionSit(uint oChair)
        {
            NWN.Core.NWScript.ActionSit(oChair);
        }

        /// <summary>
        ///  In an onConversation script this gets the number of the string pattern<br/>
        ///  matched (the one that triggered the script).<br/>
        ///  * Returns -1 if no string matched
        /// </summary>
        public static int GetListenPatternNumber()
        {
            return NWN.Core.NWScript.GetListenPatternNumber();
        }

        /// <summary>
        ///  Jump to an object ID, or as near to it as possible.
        /// </summary>
        public static void ActionJumpToObject(uint oToJumpTo, bool bWalkStraightLineToPoint = true)
        {
            NWN.Core.NWScript.ActionJumpToObject(oToJumpTo, bWalkStraightLineToPoint ? 1 : 0);
        }

        /// <summary>
        ///  Get the first waypoint with the specified tag.<br/>
        ///  * Returns OBJECT_INVALID if the waypoint cannot be found.
        /// </summary>
        public static uint GetWaypointByTag(string sWaypointTag)
        {
            return NWN.Core.NWScript.GetWaypointByTag(sWaypointTag);
        }

        /// <summary>
        ///  Get the destination object for the given object.<br/>
        /// <br/>
        ///  All objects can hold a transition target, but only Doors and Triggers<br/>
        ///  will be made clickable by the game engine (This may change in the<br/>
        ///  future). You can set and query transition targets on other objects for<br/>
        ///  your own scripted purposes.<br/>
        /// <br/>
        ///  * Returns OBJECT_INVALID if oTransition does not hold a target.
        /// </summary>
        public static uint GetTransitionTarget(uint oTransition)
        {
            return NWN.Core.NWScript.GetTransitionTarget(oTransition);
        }

        /// <summary>
        ///  Link the two supplied effects, returning eChildEffect as a child of<br/>
        ///  eParentEffect.<br/>
        ///  Note: When applying linked effects if the target is immune to all valid<br/>
        ///  effects all other effects will be removed as well. This means that if you<br/>
        ///  apply a visual effect and a silence effect (in a link) and the target is<br/>
        ///  immune to the silence effect that the visual effect will get removed as well.<br/>
        ///  Visual Effects are not considered "valid" effects for the purposes of<br/>
        ///  determining if an effect will be removed or not and as such should never be<br/>
        ///  packaged *only* with other visual effects in a link.
        /// </summary>
        public static Effect EffectLinkEffects(Effect eChildEffect, Effect eParentEffect)
        {
            return NWN.Core.NWScript.EffectLinkEffects(eChildEffect, eParentEffect);
        }

        /// <summary>
        ///  Get the nNth object with the specified tag.<br/>
        ///  - sTag<br/>
        ///  - nNth: the nth object with this tag may be requested<br/>
        ///  * Returns OBJECT_INVALID if the object cannot be found.<br/>
        ///  Note: The module cannot be retrieved by GetObjectByTag(), use GetModule() instead.
        /// </summary>
        public static uint GetObjectByTag(string sTag, int nNth = 0)
        {
            return NWN.Core.NWScript.GetObjectByTag(sTag, nNth);
        }
        /// <summary>
        ///  Adjust the alignment of oSubject.<br/>
        ///  - oSubject<br/>
        ///  - nAlignment:<br/>
        ///    -&gt; ALIGNMENT_LAWFUL/ALIGNMENT_CHAOTIC/ALIGNMENT_GOOD/ALIGNMENT_EVIL: oSubject's<br/>
        ///       alignment will be shifted in the direction specified<br/>
        ///    -&gt; ALIGNMENT_ALL: nShift will be added to oSubject's law/chaos and<br/>
        ///       good/evil alignment values<br/>
        ///    -&gt; ALIGNMENT_NEUTRAL: nShift is applied to oSubject's law/chaos and<br/>
        ///       good/evil alignment values in the direction which is towards neutrality.<br/>
        ///      e.g. If oSubject has a law/chaos value of 10 (i.e. chaotic) and a<br/>
        ///           good/evil value of 80 (i.e. good) then if nShift is 15, the<br/>
        ///           law/chaos value will become (10+15)=25 and the good/evil value will<br/>
        ///           become (80-25)=55<br/>
        ///      Furthermore, the shift will at most take the alignment value to 50 and<br/>
        ///      not beyond.<br/>
        ///      e.g. If oSubject has a law/chaos value of 40 and a good/evil value of 70,<br/>
        ///           then if nShift is 15, the law/chaos value will become 50 and the<br/>
        ///           good/evil value will become 55<br/>
        ///  - nShift: this is the desired shift in alignment<br/>
        ///  - bAllPartyMembers: when true the alignment shift of oSubject also has a<br/>
        ///                      diminished affect all members of oSubject's party (if oSubject is a Player).<br/>
        ///                      When false the shift only affects oSubject.<br/>
        ///  * No return value
        /// </summary>
        public static void AdjustAlignment(uint oSubject, int nAlignment, int nShift, bool bAllPartyMembers = true)
        {
            NWN.Core.NWScript.AdjustAlignment(oSubject, nAlignment, nShift, bAllPartyMembers ? 1 : 0);
        }

        /// <summary>
        ///  Do nothing for fSeconds seconds.
        /// </summary>
        public static void ActionWait(float fSeconds)
        {
            NWN.Core.NWScript.ActionWait(fSeconds);
        }

        /// <summary>
        ///  Set the transition bitmap of a player; this should only be called in area<br/>
        ///  transition scripts. This action should be run by the person "clicking" the<br/>
        ///  area transition via AssignCommand.<br/>
        ///  - nPredefinedAreaTransition:<br/>
        ///    -&gt; To use a predefined area transition bitmap, use one of AREA_TRANSITION_*<br/>
        ///    -&gt; To use a custom, user-defined area transition bitmap, use<br/>
        ///       AREA_TRANSITION_USER_DEFINED and specify the filename in the second<br/>
        ///       parameter<br/>
        ///  - sCustomAreaTransitionBMP: this is the filename of a custom, user-defined<br/>
        ///    area transition bitmap
        /// </summary>
        public static void SetAreaTransitionBMP(int nPredefinedAreaTransition, string sCustomAreaTransitionBMP = "")
        {
            NWN.Core.NWScript.SetAreaTransitionBMP(nPredefinedAreaTransition, sCustomAreaTransitionBMP);
        }

        /// <summary>
        ///  Starts a conversation with oObjectToConverseWith - this will cause their<br/>
        ///  OnDialog event to fire.<br/>
        ///  - oObjectToConverseWith<br/>
        ///  - sDialogResRef: If this is blank, the creature's own dialogue file will be used<br/>
        ///  - bPrivateConversation<br/>
        ///  Turn off bPlayHello if you don't want the initial greeting to play
        /// </summary>
        public static void ActionStartConversation(uint oObjectToConverseWith, string sDialogResRef = "", bool bPrivateConversation = false, bool bPlayHello = true)
        {
            NWN.Core.NWScript.ActionStartConversation(oObjectToConverseWith, sDialogResRef, bPrivateConversation ? 1 : 0, bPlayHello ? 1 : 0);
        }

        /// <summary>
        ///  Pause the current conversation.
        /// </summary>
        public static void ActionPauseConversation()
        {
            NWN.Core.NWScript.ActionPauseConversation();
        }

        /// <summary>
        ///  Resume a conversation after it has been paused.
        /// </summary>
        public static void ActionResumeConversation()
        {
            NWN.Core.NWScript.ActionResumeConversation();
        }

        /// <summary>
        ///  Create a Beam effect.<br/>
        ///  - nBeamVisualEffect: VFX_BEAM_*<br/>
        ///  - oEffector: the beam is emitted from this creature<br/>
        ///  - nBodyPart: BODY_NODE_*<br/>
        ///  - bMissEffect: If this is true, the beam will fire to a random vector near or<br/>
        ///    past the target<br/>
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nBeamVisualEffect is<br/>
        ///    not valid.
        /// </summary>
        public static Effect EffectBeam(int nBeamVisualEffect, uint oEffector, int nBodyPart, bool bMissEffect = false, float fScale = 1.0f, Vector3 vTranslate = default, Vector3 vRotate = default)
        {
            return NWN.Core.NWScript.EffectBeam(nBeamVisualEffect, oEffector, nBodyPart, bMissEffect ? 1 : 0, fScale, vTranslate, vRotate);
        }

        /// <summary>
        ///  Get an integer between 0 and 100 (inclusive) that represents how oSource<br/>
        ///  feels about oTarget.<br/>
        ///  -&gt; 0-10 means oSource is hostile to oTarget<br/>
        ///  -&gt; 11-89 means oSource is neutral to oTarget<br/>
        ///  -&gt; 90-100 means oSource is friendly to oTarget<br/>
        ///  * Returns -1 if oSource or oTarget does not identify a valid object
        /// </summary>
        public static int GetReputation(uint oSource, uint oTarget)
        {
            return NWN.Core.NWScript.GetReputation(oSource, oTarget);
        }

        /// <summary>
        ///  Adjust how oSourceFactionMember's faction feels about oTarget by the<br/>
        ///  specified amount.<br/>
        ///  Note: This adjusts Faction Reputation, how the entire faction that<br/>
        ///  oSourceFactionMember is in, feels about oTarget.<br/>
        ///  * No return value<br/>
        ///  Note: You can't adjust a player character's (PC) faction towards<br/>
        ///        NPCs, so attempting to make an NPC hostile by passing in a PC object<br/>
        ///        as oSourceFactionMember in the following call will fail:<br/>
        ///        AdjustReputation(oNPC,oPC,-100);<br/>
        ///        Instead you should pass in the PC object as the first<br/>
        ///        parameter as in the following call which should succeed:<br/>
        ///        AdjustReputation(oPC,oNPC,-100);<br/>
        ///  Note: Will fail if oSourceFactionMember is a plot object.
        /// </summary>
        public static void AdjustReputation(uint oTarget, uint oSourceFactionMember, int nAdjustment)
        {
            NWN.Core.NWScript.AdjustReputation(oTarget, oSourceFactionMember, nAdjustment);
        }

        /// <summary>
        ///  Get the creature that is currently sitting on the specified object.<br/>
        ///  - oChair<br/>
        ///  * Returns OBJECT_INVALID if oChair is not a valid placeable.
        /// </summary>
        public static uint GetSittingCreature(uint oChair)
        {
            return NWN.Core.NWScript.GetSittingCreature(oChair);
        }

        /// <summary>
        ///  Get the creature that is going to attack oTarget.<br/>
        ///  Note: This value is cleared out at the end of every combat round and should<br/>
        ///  not be used in any case except when getting a "going to be attacked" shout<br/>
        ///  from the master creature (and this creature is a henchman)<br/>
        ///  * Returns OBJECT_INVALID if oTarget is not a valid creature.
        /// </summary>
        public static uint GetGoingToBeAttackedBy(uint oTarget)
        {
            return NWN.Core.NWScript.GetGoingToBeAttackedBy(oTarget);
        }

        /// <summary>
        ///  Create a Spell Resistance Increase effect.<br/>
        ///  - nValue: size of spell resistance increase
        /// </summary>
        public static Effect EffectSpellResistanceIncrease(int nValue)
        {
            return NWN.Core.NWScript.EffectSpellResistanceIncrease(nValue);
        }

        /// <summary>
        ///  Get the location of oObject.
        /// </summary>
        public static Location GetLocation(uint oObject)
        {
            return NWN.Core.NWScript.GetLocation(oObject);
        }

        /// <summary>
        ///  The subject will jump to lLocation instantly (even between areas).<br/>
        ///  If lLocation is invalid, nothing will happen.
        /// </summary>
        public static void ActionJumpToLocation(Location lLocation)
        {
            NWN.Core.NWScript.ActionJumpToLocation(lLocation);
        }

        /// <summary>
        ///  Create a location.<br/>
        ///  The special constant LOCATION_INVALID describes a location with area equalling OBJECT_INVALID<br/>
        ///  and all other values 0.0f. Declared but not initialised location variables default to this value.
        /// </summary>
        public static Location Location(uint oArea, Vector3 vPosition, float fOrientation)
        {
            return NWN.Core.NWScript.Location(oArea, vPosition, fOrientation);
        }

        /// <summary>
        ///  Apply eEffect at lLocation.
        /// </summary>
        public static void ApplyEffectAtLocation(DurationType nDurationType, Effect eEffect, Location lLocation, float fDuration = 0.0f)
        {
            NWN.Core.NWScript.ApplyEffectAtLocation((int)nDurationType, eEffect, lLocation, fDuration);
        }
        /// <summary>
        ///  * Returns true if oCreature is a Player Controlled character.
        /// </summary>
        public static bool GetIsPC(uint oCreature)
        {
            return NWN.Core.NWScript.GetIsPC(oCreature) == 1;
        }

        /// <summary>
        ///  Convert fFeet into a number of meters.
        /// </summary>
        public static float FeetToMeters(float fFeet)
        {
            return NWN.Core.NWScript.FeetToMeters(fFeet);
        }

        /// <summary>
        ///  Convert fYards into a number of meters.
        /// </summary>
        public static float YardsToMeters(float fYards)
        {
            return NWN.Core.NWScript.YardsToMeters(fYards);
        }

        /// <summary>
        ///  Apply eEffect to oTarget.
        /// </summary>
        public static void ApplyEffectToObject(DurationType nDurationType, Effect eEffect, uint oTarget, float fDuration = 0.0f)
        {
            NWN.Core.NWScript.ApplyEffectToObject((int)nDurationType, eEffect, oTarget, fDuration);
        }

        /// <summary>
        ///  The caller will immediately speak sStringToSpeak (this is different from<br/>
        ///  ActionSpeakString)<br/>
        ///  - sStringToSpeak<br/>
        ///  - nTalkVolume: TALKVOLUME_*
        /// </summary>
        public static void SpeakString(string sStringToSpeak, TalkVolumeType nTalkVolume = TalkVolumeType.Talk)
        {
            NWN.Core.NWScript.SpeakString(sStringToSpeak, (int)nTalkVolume);
        }

        /// <summary>
        ///  Get the location of the caller's last spell target.
        /// </summary>
        public static Location GetSpellTargetLocation()
        {
            return NWN.Core.NWScript.GetSpellTargetLocation();
        }

        /// <summary>
        ///  Get the position vector from lLocation.
        /// </summary>
        public static Vector3 GetPositionFromLocation(Location lLocation)
        {
            return NWN.Core.NWScript.GetPositionFromLocation(lLocation);
        }

        /// <summary>
        ///  Get the area's object ID from lLocation.
        /// </summary>
        public static uint GetAreaFromLocation(Location lLocation)
        {
            return NWN.Core.NWScript.GetAreaFromLocation(lLocation);
        }

        /// <summary>
        ///  Get the orientation value from lLocation.
        /// </summary>
        public static float GetFacingFromLocation(Location lLocation)
        {
            return NWN.Core.NWScript.GetFacingFromLocation(lLocation);
        }

        /// <summary>
        ///  Get the creature nearest to lLocation, subject to all the criteria specified.<br/>
        ///  - nFirstCriteriaType: CREATURE_TYPE_*<br/>
        ///  - nFirstCriteriaValue:<br/>
        ///    -&gt; CLASS_TYPE_* if nFirstCriteriaType was CREATURE_TYPE_CLASS<br/>
        ///    -&gt; SPELL_* if nFirstCriteriaType was CREATURE_TYPE_DOES_NOT_HAVE_SPELL_EFFECT<br/>
        ///       or CREATURE_TYPE_HAS_SPELL_EFFECT<br/>
        ///    -&gt; true or false if nFirstCriteriaType was CREATURE_TYPE_IS_ALIVE<br/>
        ///    -&gt; PERCEPTION_* if nFirstCriteriaType was CREATURE_TYPE_PERCEPTION<br/>
        ///    -&gt; PLAYER_CHAR_IS_PC or PLAYER_CHAR_NOT_PC if nFirstCriteriaType was<br/>
        ///       CREATURE_TYPE_PLAYER_CHAR<br/>
        ///    -&gt; RACIAL_TYPE_* if nFirstCriteriaType was CREATURE_TYPE_RACIAL_TYPE<br/>
        ///    -&gt; REPUTATION_TYPE_* if nFirstCriteriaType was CREATURE_TYPE_REPUTATION<br/>
        ///    For example, to get the nearest PC, use<br/>
        ///    (CREATURE_TYPE_PLAYER_CHAR, PLAYER_CHAR_IS_PC)<br/>
        ///  - lLocation: We're trying to find the creature of the specified type that is<br/>
        ///    nearest to lLocation<br/>
        ///  - nNth: We don't have to find the first nearest: we can find the Nth nearest....<br/>
        ///  - nSecondCriteriaType: This is used in the same way as nFirstCriteriaType to<br/>
        ///    further specify the type of creature that we are looking for.<br/>
        ///  - nSecondCriteriaValue: This is used in the same way as nFirstCriteriaValue<br/>
        ///    to further specify the type of creature that we are looking for.<br/>
        ///  - nThirdCriteriaType: This is used in the same way as nFirstCriteriaType to<br/>
        ///    further specify the type of creature that we are looking for.<br/>
        ///  - nThirdCriteriaValue: This is used in the same way as nFirstCriteriaValue to<br/>
        ///    further specify the type of creature that we are looking for.<br/>
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetNearestCreatureToLocation(int nFirstCriteriaType, int nFirstCriteriaValue, Location lLocation, int nNth = 1, int nSecondCriteriaType = -1, int nSecondCriteriaValue = -1, int nThirdCriteriaType = -1, int nThirdCriteriaValue = -1)
        {
            return NWN.Core.NWScript.GetNearestCreatureToLocation(nFirstCriteriaType, nFirstCriteriaValue, lLocation, nNth, nSecondCriteriaType, nSecondCriteriaValue, nThirdCriteriaType, nThirdCriteriaValue);
        }

        /// <summary>
        ///  Get the nth Object nearest to oTarget that is of the specified type.<br/>
        ///  - nObjectType: OBJECT_TYPE_*<br/>
        ///  - oTarget<br/>
        ///  - nNth<br/>
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetNearestObject(ObjectType nObjectType = ObjectType.All, uint oTarget = OBJECT_INVALID, int nNth = 1)
        {
            return NWN.Core.NWScript.GetNearestObject((int)nObjectType, oTarget, nNth);
        }

        /// <summary>
        ///  Get the nNth object nearest to lLocation that is of the specified type.<br/>
        ///  - nObjectType: OBJECT_TYPE_*<br/>
        ///  - lLocation<br/>
        ///  - nNth<br/>
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetNearestObjectToLocation(ObjectType nObjectType, Location lLocation, int nNth = 1)
        {
            return NWN.Core.NWScript.GetNearestObjectToLocation((int)nObjectType, lLocation, nNth);
        }

        /// <summary>
        ///  Get the nth Object nearest to oTarget that has sTag as its tag.<br/>
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetNearestObjectByTag(string sTag, uint oTarget = OBJECT_INVALID, int nNth = 1)
        {
            return NWN.Core.NWScript.GetNearestObjectByTag(sTag, oTarget, nNth);
        }

        /// <summary>
        ///  Convert nInteger into a floating point number.
        /// </summary>
        public static float IntToFloat(int nInteger)
        {
            return NWN.Core.NWScript.IntToFloat(nInteger);
        }

        /// <summary>
        ///  Convert fFloat into the nearest integer.
        /// </summary>
        public static int FloatToInt(float fFloat)
        {
            return NWN.Core.NWScript.FloatToInt(fFloat);
        }

        /// <summary>
        ///  Convert sNumber into an integer.
        /// </summary>
        public static int StringToInt(string sNumber)
        {
            return NWN.Core.NWScript.StringToInt(sNumber);
        }

        /// <summary>
        ///  Convert sNumber into a floating point number.
        /// </summary>
        public static float StringToFloat(string sNumber)
        {
            return NWN.Core.NWScript.StringToFloat(sNumber);
        }

        /// <summary>
        ///  Cast spell nSpell at lTargetLocation.<br/>
        ///  - nSpell: SPELL_*<br/>
        ///  - lTargetLocation<br/>
        ///  - nMetaMagic: METAMAGIC_*. If nClass is specified, cannot be METAMAGIC_ANY.<br/>
        ///  - bCheat: If this is true, then the executor of the action doesn't have to be<br/>
        ///    able to cast the spell. Ignored if nClass is specified.<br/>
        ///  - bCheat: If this is true, then the executor of the action doesn't have to be<br/>
        ///    able to cast the spell.<br/>
        ///  - nProjectilePathType: PROJECTILE_PATH_TYPE_*<br/>
        ///  - bInstantSpell: If this is true, the spell is cast immediately; this allows<br/>
        ///    the end-user to simulate<br/>
        ///    a high-level magic user having lots of advance warning of impending trouble.<br/>
        ///  - nClass: If set to a CLASS_TYPE_* it will cast using that class specifically.<br/>
        ///    CLASS_TYPE_INVALID will use spell abilities.<br/>
        ///  - bSpontaneousCast: If set to true will attempt to cast the given spell spontaneously,<br/>
        ///    ie a Cleric casting Cure Light Wounds using any level 1 slot. Needs a valid nClass set.<br/>
        ///  - nDomainLevel: The level of the spell if cast from a domain slot.<br/>
        ///    eg SPELL_HEAL can be spell level 5 on a cleric. Use 0 for no domain slot.
        /// </summary>
        public static void ActionCastSpellAtLocation(SpellType nSpell, Location lTargetLocation, MetamagicType nMetaMagic = MetamagicType.Any, bool bCheat = false, ProjectilePathType nProjectilePathType = ProjectilePathType.Default, bool bInstantSpell = false, int nClass = -1, bool bSpontaneousCast = false, int nDomainlevel = 0)
        {
            NWN.Core.NWScript.ActionCastSpellAtLocation((int)nSpell, lTargetLocation, (int)nMetaMagic, bCheat ? 1 : 0, (int)nProjectilePathType, bInstantSpell ? 1 : 0, nClass, bSpontaneousCast ? 1 : 0, nDomainlevel);
        }

        /// <summary>
        ///  * Returns true if oSource considers oTarget as an enemy.
        /// </summary>
        public static bool GetIsEnemy(uint oTarget, uint oSource = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetIsEnemy(oTarget, oSource) == 1;
        }

        /// <summary>
        ///  * Returns true if oSource considers oTarget as a friend.
        /// </summary>
        public static bool GetIsFriend(uint oTarget, uint oSource = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetIsFriend(oTarget, oSource) == 1;
        }


        /// <summary>
        ///  * Returns true if oSource considers oTarget as neutral.
        /// </summary>
        public static bool GetIsNeutral(uint oTarget, uint oSource = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetIsNeutral(oTarget, oSource) == 1;
        }

        /// <summary>
        ///  Get the PC that is involved in the conversation.<br/>
        ///  * Returns OBJECT_INVALID on error.
        /// </summary>
        public static uint GetPCSpeaker()
        {
            return NWN.Core.NWScript.GetPCSpeaker();
        }

        /// <summary>
        ///  Get a string from the talk table using nStrRef.
        /// </summary>
        public static string GetStringByStrRef(int nStrRef, GenderType nGender = GenderType.Male)
        {
            return NWN.Core.NWScript.GetStringByStrRef(nStrRef, (int)nGender);
        }

        /// <summary>
        ///  Causes the creature to speak a translated string.<br/>
        ///  - nStrRef: Reference of the string in the talk table<br/>
        ///  - nTalkVolume: TALKVOLUME_*
        /// </summary>
        public static void ActionSpeakStringByStrRef(int nStrRef, TalkVolumeType nTalkVolume = TalkVolumeType.Talk)
        {
            NWN.Core.NWScript.ActionSpeakStringByStrRef(nStrRef, (int)nTalkVolume);
        }

        /// <summary>
        ///  Destroy oObject (irrevocably).<br/>
        ///  This will not work on modules and areas.
        /// </summary>
        public static void DestroyObject(uint oDestroy, float fDelay = 0.0f)
        {
            NWN.Core.NWScript.DestroyObject(oDestroy, fDelay);
        }

        /// <summary>
        ///  Get the module.<br/>
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetModule()
        {
            return NWN.Core.NWScript.GetModule();
        }

        /// <summary>
        ///  Create an object of the specified type at lLocation.<br/>
        ///  - nObjectType: OBJECT_TYPE_ITEM, OBJECT_TYPE_CREATURE, OBJECT_TYPE_PLACEABLE,<br/>
        ///    OBJECT_TYPE_STORE, OBJECT_TYPE_WAYPOINT<br/>
        ///  - sTemplate<br/>
        ///  - lLocation<br/>
        ///  - bUseAppearAnimation<br/>
        ///  - sNewTag - if this string is not empty, it will replace the default tag from the template
        /// </summary>
        public static uint CreateObject(ObjectType nObjectType, string sTemplate, Location lLocation, bool bUseAppearAnimation = false, string sNewTag = "")
        {
            return NWN.Core.NWScript.CreateObject((int)nObjectType, sTemplate, lLocation, bUseAppearAnimation ? 1 : 0, sNewTag);
        }

        /// <summary>
        ///  Create an event which triggers the "SpellCastAt" script<br/>
        ///  Note: This only creates the event. The event wont actually trigger until SignalEvent()<br/>
        ///  is called using this created SpellCastAt event as an argument.<br/>
        ///  For example:<br/>
        ///      SignalEvent(oCreature, EventSpellCastAt(oCaster, SPELL_MAGIC_MISSILE, true));<br/>
        ///  This function doesn't cast the spell specified, it only creates an event so that<br/>
        ///  when the event is signaled on an object, the object will use its OnSpellCastAt script<br/>
        ///  to react to the spell being cast.<br/>
        /// <br/>
        ///  To specify the OnSpellCastAt script that should run, view the Object's Properties<br/>
        ///  and click on the Scripts Tab. Then specify a script for the OnSpellCastAt event.<br/>
        ///  From inside the OnSpellCastAt script call:<br/>
        ///      GetLastSpellCaster() to get the object that cast the spell (oCaster).<br/>
        ///      GetLastSpell() to get the type of spell cast (nSpell)<br/>
        ///      GetLastSpellHarmful() to determine if the spell cast at the object was harmful.
        /// </summary>
        public static Event EventSpellCastAt(uint oCaster, int nSpell, bool bHarmful = true)
        {
            return NWN.Core.NWScript.EventSpellCastAt(oCaster, nSpell, bHarmful ? 1 : 0);
        }

        /// <summary>
        ///  This is for use in a "Spell Cast" script, it gets who cast the spell.<br/>
        ///  The spell could have been cast by a creature, placeable or door.<br/>
        ///  * Returns OBJECT_INVALID if the caller is not a creature, placeable or door.
        /// </summary>
        public static uint GetLastSpellCaster()
        {
            return NWN.Core.NWScript.GetLastSpellCaster();
        }
        /// <summary>
        ///  This is for use in a "Spell Cast" script, it gets the ID of the spell that<br/>
        ///  was cast.
        /// </summary>
        public static SpellType GetLastSpell()
        {
            return (SpellType)NWN.Core.NWScript.GetLastSpell();
        }

        /// <summary>
        ///  This is for use in a user-defined script, it gets the event number.
        /// </summary>
        public static int GetUserDefinedEventNumber()
        {
            return NWN.Core.NWScript.GetUserDefinedEventNumber();
        }
        /// <summary>
        ///  This is for use in a Spell script, it gets the ID of the spell that is being cast.<br/>
        ///  If used in an Area of Effect script it will return the ID of the spell that generated the AOE effect.<br/>
        ///  * Returns the spell ID (SPELL_*) or -1 if no spell was cast or on error
        /// </summary>
        public static SpellType GetSpellId()
        {
            return (SpellType)NWN.Core.NWScript.GetSpellId();
        }


        /// <summary>
        ///  Generate a random name.<br/>
        ///  nNameType: The type of random name to be generated (NAME_*)
        /// </summary>
        public static string RandomName(NameType nNameType = NameType.FirstGenericMale)
        {
            return NWN.Core.NWScript.RandomName((int)nNameType);
        }

        /// <summary>
        ///  Create a Poison effect.<br/>
        ///  - nPoisonType: POISON_*
        /// </summary>
        public static Effect EffectPoison(PoisonType nPoisonType)
        {
            return NWN.Core.NWScript.EffectPoison((int)nPoisonType);
        }

        /// <summary>
        ///  Create a Disease effect.<br/>
        ///  - nDiseaseType: DISEASE_*
        /// </summary>
        public static Effect EffectDisease(DiseaseType nDiseaseType)
        {
            return NWN.Core.NWScript.EffectDisease((int)nDiseaseType);
        }

        /// <summary>
        ///  Create a Silence effect.
        /// </summary>
        public static Effect EffectSilence()
        {
            return NWN.Core.NWScript.EffectSilence();
        }

        /// <summary>
        ///  Get the name of oObject.<br/>
        ///  - bOriginalName:  if set to true any new name specified via a SetName scripting command<br/>
        ///                    is ignored and the original object's name is returned instead.
        /// </summary>
        public static string GetName(uint oObject, bool bOriginalName = false)
        {
            return NWN.Core.NWScript.GetName(oObject, bOriginalName ? 1 : 0);
        }

        /// <summary>
        ///  Use this in a conversation script to get the person with whom you are conversing.<br/>
        ///  * Returns OBJECT_INVALID if the caller is not a valid creature.
        /// </summary>
        public static uint GetLastSpeaker()
        {
            return NWN.Core.NWScript.GetLastSpeaker();
        }

        /// <summary>
        ///  Use this in an OnDialog script to start up the dialog tree.<br/>
        ///  - sResRef: if this is not specified, the default dialog file will be used<br/>
        ///  - oObjectToDialog: if this is not specified the person that triggered the<br/>
        ///    event will be used
        /// </summary>
        public static int BeginConversation(string sResRef = "", uint oObjectToDialog = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.BeginConversation(sResRef, oObjectToDialog);
        }

        /// <summary>
        ///  Use this in an OnPerception script to get the object that was perceived.<br/>
        ///  * Returns OBJECT_INVALID if the caller is not a valid creature.
        /// </summary>
        public static uint GetLastPerceived()
        {
            return NWN.Core.NWScript.GetLastPerceived();
        }

        /// <summary>
        ///  Use this in an OnPerception script to determine whether the object that was<br/>
        ///  perceived was heard.
        /// </summary>
        public static int GetLastPerceptionHeard()
        {
            return NWN.Core.NWScript.GetLastPerceptionHeard();
        }

        /// <summary>
        ///  Use this in an OnPerception script to determine whether the object that was<br/>
        ///  perceived has become inaudible.
        /// </summary>
        public static int GetLastPerceptionInaudible()
        {
            return NWN.Core.NWScript.GetLastPerceptionInaudible();
        }

        /// <summary>
        ///  Use this in an OnPerception script to determine whether the object that was<br/>
        ///  perceived was seen.
        /// </summary>
        public static int GetLastPerceptionSeen()
        {
            return NWN.Core.NWScript.GetLastPerceptionSeen();
        }

        /// <summary>
        ///  Use this in an OnClosed script to get the object that closed the door or placeable.<br/>
        ///  * Returns OBJECT_INVALID if the caller is not a valid door or placeable.
        /// </summary>
        public static uint GetLastClosedBy()
        {
            return NWN.Core.NWScript.GetLastClosedBy();
        }
        /// <summary>
        ///  Use this in an OnPerception script to determine whether the object that was
        ///  perceived has vanished.
        /// </summary>
        public static int GetLastPerceptionVanished()
        {
            return NWN.Core.NWScript.GetLastPerceptionVanished();
        }

        /// <summary>
        ///  Get the first object within oPersistentObject.<br/>
        ///  - oPersistentObject<br/>
        ///  - nResidentObjectType: OBJECT_TYPE_*<br/>
        ///  - nPersistentZone: PERSISTENT_ZONE_ACTIVE. [This could also take the value
        ///    PERSISTENT_ZONE_FOLLOW, but this is no longer used.]<br/>
        ///  * Returns OBJECT_INVALID if no object is found.
        /// </summary>
        public static uint GetFirstInPersistentObject(uint oPersistentObject = OBJECT_INVALID, ObjectType nResidentObjectType = ObjectType.Creature, PersistentZoneType nPersistentZone = PersistentZoneType.Active)
        {
            return NWN.Core.NWScript.GetFirstInPersistentObject(oPersistentObject, (int)nResidentObjectType, (int)nPersistentZone);
        }

        /// <summary>
        ///  Get the next object within oPersistentObject.<br/>
        ///  - oPersistentObject<br/>
        ///  - nResidentObjectType: OBJECT_TYPE_*<br/>
        ///  - nPersistentZone: PERSISTENT_ZONE_ACTIVE. [This could also take the value
        ///    PERSISTENT_ZONE_FOLLOW, but this is no longer used.]<br/>
        ///  * Returns OBJECT_INVALID if no object is found.
        /// </summary>
        public static uint GetNextInPersistentObject(uint oPersistentObject = OBJECT_INVALID, ObjectType nResidentObjectType = ObjectType.Creature, PersistentZoneType nPersistentZone = PersistentZoneType.Active)
        {
            return NWN.Core.NWScript.GetNextInPersistentObject(oPersistentObject, (int)nResidentObjectType, (int)nPersistentZone);
        }

        /// <summary>
        ///  This returns the creator of oAreaOfEffectObject.<br/>
        ///  * Returns OBJECT_INVALID if oAreaOfEffectObject is not a valid Area of Effect object.
        /// </summary>
        public static uint GetAreaOfEffectCreator(uint oAreaOfEffectObject = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetAreaOfEffectCreator(oAreaOfEffectObject);
        }

        /// <summary>
        ///  Delete oObject&apos;s local integer variable sVarName
        /// </summary>
        public static void DeleteLocalInt(uint oObject, string sVarName)
        {
            NWN.Core.NWScript.DeleteLocalInt(oObject, sVarName);
        }

        /// <summary>
        ///  Delete oObject&apos;s local float variable sVarName
        /// </summary>
        public static void DeleteLocalFloat(uint oObject, string sVarName)
        {
            NWN.Core.NWScript.DeleteLocalFloat(oObject, sVarName);
        }

        /// <summary>
        ///  Delete oObject&apos;s local string variable sVarName
        /// </summary>
        public static void DeleteLocalString(uint oObject, string sVarName)
        {
            NWN.Core.NWScript.DeleteLocalString(oObject, sVarName);
        }

        /// <summary>
        ///  Delete oObject&apos;s local object variable sVarName
        /// </summary>
        public static void DeleteLocalObject(uint oObject, string sVarName)
        {
            NWN.Core.NWScript.DeleteLocalObject(oObject, sVarName);
        }

        /// <summary>
        ///  Delete oObject&apos;s local location variable sVarName
        /// </summary>
        public static void DeleteLocalLocation(uint oObject, string sVarName)
        {
            NWN.Core.NWScript.DeleteLocalLocation(oObject, sVarName);
        }

        /// <summary>
        ///  Create a Haste effect.
        /// </summary>
        public static Effect EffectHaste()
        {
            return NWN.Core.NWScript.EffectHaste();
        }

        /// <summary>
        ///  Create a Slow effect.
        /// </summary>
        public static Effect EffectSlow()
        {
            return NWN.Core.NWScript.EffectSlow();
        }

        /// <summary>
        ///  Convert oObject into a hexadecimal string.
        /// </summary>
        public static string ObjectToString(uint oObject)
        {
            return NWN.Core.NWScript.ObjectToString(oObject);
        }

        /// <summary>
        ///  Create an Immunity effect.<br/>
        ///  - nImmunityType: IMMUNITY_TYPE_*
        /// </summary>
        public static Effect EffectImmunity(ImmunityType nImmunityType)
        {
            return NWN.Core.NWScript.EffectImmunity((int)nImmunityType);
        }
        /// <summary>
        ///  - oCreature<br/>
        ///  - nImmunityType: IMMUNITY_TYPE_*<br/>
        ///  - oVersus: if this is specified, then we also check for the race and
        ///    alignment of oVersus<br/>
        ///  * Returns true if oCreature has immunity of type nImmunity versus oVersus.
        /// </summary>
        public static bool GetIsImmune(uint oCreature, ImmunityType nImmunityType, uint oVersus = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetIsImmune(oCreature, (int)nImmunityType, oVersus) == 1;
        }

        /// <summary>
        ///  Creates a Damage Immunity Increase effect.<br/>
        ///  - nDamageType: DAMAGE_TYPE_*<br/>
        ///  - nPercentImmunity
        /// </summary>
        public static Effect EffectDamageImmunityIncrease(DamageType nDamageType, int nPercentImmunity)
        {
            return NWN.Core.NWScript.EffectDamageImmunityIncrease((int)nDamageType, nPercentImmunity);
        }

        /// <summary>
        ///  Determine whether oEncounter is active.
        /// </summary>
        public static bool GetEncounterActive(uint oEncounter = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetEncounterActive(oEncounter) == 1;
        }

        /// <summary>
        ///  Set oEncounter&apos;s active state to nNewValue.<br/>
        ///  - nNewValue: true/FALSE<br/>
        ///  - oEncounter
        /// </summary>
        public static void SetEncounterActive(int nNewValue, uint oEncounter = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetEncounterActive(nNewValue, oEncounter);
        }

        /// <summary>
        ///  Get the maximum number of times that oEncounter will spawn.
        /// </summary>
        public static int GetEncounterSpawnsMax(uint oEncounter = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetEncounterSpawnsMax(oEncounter);
        }

        /// <summary>
        ///  Set the maximum number of times that oEncounter can spawn
        /// </summary>
        public static void SetEncounterSpawnsMax(int nNewValue, uint oEncounter = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetEncounterSpawnsMax(nNewValue, oEncounter);
        }
        /// <summary>
        ///  Get the number of times that oEncounter has spawned so far
        /// </summary>
        public static int GetEncounterSpawnsCurrent(uint oEncounter = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetEncounterSpawnsCurrent(oEncounter);
        }

        /// <summary>
        /// Set the number of times that oEncounter has spawned so far
        /// </summary>
        public static void SetEncounterSpawnsCurrent(int nNewValue, uint oEncounter = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetEncounterSpawnsCurrent(nNewValue, oEncounter);
        }

        /// <summary>
        ///  Use this in an OnItemAcquired script to get the item that was acquired.<br/>
        ///  * Returns OBJECT_INVALID if the module is not valid.
        /// </summary>
        public static uint GetModuleItemAcquired()
        {
            return NWN.Core.NWScript.GetModuleItemAcquired();
        }

        /// <summary>
        ///  Use this in an OnItemAcquired script to get the creature that previously<br/>
        ///  possessed the item.<br/>
        ///  * Returns OBJECT_INVALID if the item was picked up from the ground.
        /// </summary>
        public static uint GetModuleItemAcquiredFrom()
        {
            return NWN.Core.NWScript.GetModuleItemAcquiredFrom();
        }

        /// <summary>
        ///  Set the value for a custom token.
        /// </summary>
        public static void SetCustomToken(int nCustomTokenNumber, string sTokenValue)
        {
            NWN.Core.NWScript.SetCustomToken(nCustomTokenNumber, sTokenValue);
        }
        /// <summary>
        ///  Determine whether oCreature has nFeat, optionally if nFeat is useable.<br/>
        ///  - nFeat: FEAT_*<br/>
        ///  - oCreature<br/>
        ///  - bIgnoreUses: Will check if the creature has the given feat even if it has no uses remaining
        /// </summary>
        public static bool GetHasFeat(int nFeat, uint oCreature = OBJECT_INVALID, bool bIgnoreUses = false)
        {
            return NWN.Core.NWScript.GetHasFeat(nFeat, oCreature, bIgnoreUses ? 1 : 0) == 1;
        }

        /// <summary>
        ///  Determine whether oCreature has nSkill, and nSkill is useable.<br/>
        ///  - nSkill: SKILL_*<br/>
        ///  - oCreature
        /// </summary>
        public static bool GetHasSkill(int nSkill, uint oCreature = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetHasSkill(nSkill, oCreature) == 1;
        }

        /// <summary>
        ///  Use nFeat on oTarget.<br/>
        ///  - nFeat: FEAT_*<br/>
        ///  - oTarget: Target of the feat. Must be OBJECT_INVALID if lTarget is used.<br/>
        ///  - nSubFeat: - For feats with subdial options, use either:<br/>
        ///         - SUBFEAT_* for some specific feats like called shot<br/>
        ///         - spells.2da line of the subdial spell, eg 708 for Dragon Shape: Blue Dragon when using FEAT_EPIC_WILD_SHAPE_DRAGON<br/>
        ///  - lTarget: The location to use the feat at. oTarget must be OBJECT_INVALID for this to be used.
        /// </summary>
        public static void ActionUseFeat(int nFeat, uint oTarget = OBJECT_INVALID, int nSubFeat = 0, Location lTarget = default)
        {
            NWN.Core.NWScript.ActionUseFeat(nFeat, oTarget, nSubFeat, lTarget);
        }

        /// <summary>
        ///  Runs the action "UseSkill" on the current creature<br/>
        ///  Use nSkill on oTarget.<br/>
        ///  - nSkill: SKILL_*<br/>
        ///  - oTarget<br/>
        ///  - nSubSkill: SUBSKILL_*<br/>
        ///  - oItemUsed: Item to use in conjunction with the skill
        /// </summary>
        public static void ActionUseSkill(int nSkill, uint oTarget, int nSubSkill = 0, uint oItemUsed = OBJECT_INVALID)
        {
            NWN.Core.NWScript.ActionUseSkill(nSkill, oTarget, nSubSkill, oItemUsed);
        }
        /// <summary>
        ///  Determine whether oSource sees oTarget.<br/>
        ///  NOTE: This *only* works on creatures, as visibility lists are not<br/>
        ///        maintained for non-creature objects.
        /// </summary>
        public static bool GetObjectSeen(uint oTarget, uint oSource = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetObjectSeen(oTarget, oSource) == 1;
        }

        /// <summary>
        ///  Determine whether oSource hears oTarget.<br/>
        ///  NOTE: This *only* works on creatures, as visibility lists are not<br/>
        ///        maintained for non-creature objects.
        /// </summary>
        public static bool GetObjectHeard(uint oTarget, uint oSource = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetObjectHeard(oTarget, oSource) == 1;
        }

        /// <summary>
        ///  Use this in an OnPlayerDeath module script to get the last player that died.
        /// </summary>
        public static uint GetLastPlayerDied()
        {
            return NWN.Core.NWScript.GetLastPlayerDied();
        }

        /// <summary>
        ///  Use this in an OnItemLost script to get the item that was lost/dropped.<br/>
        ///  * Returns OBJECT_INVALID if the module is not valid.
        /// </summary>
        public static uint GetModuleItemLost()
        {
            return NWN.Core.NWScript.GetModuleItemLost();
        }

        /// <summary>
        ///  Use this in an OnItemLost script to get the creature that lost the item.<br/>
        ///  * Returns OBJECT_INVALID if the module is not valid.
        /// </summary>
        public static uint GetModuleItemLostBy()
        {
            return NWN.Core.NWScript.GetModuleItemLostBy();
        }

        /// <summary>
        ///  Do aActionToDo.
        /// </summary>
        public static void ActionDoCommand(System.Action aActionToDo)
        {
            NWN.Core.NWScript.ActionDoCommand(aActionToDo);
            // Function ID 294
        }

        /// <summary>
        ///  Creates a conversation event.<br/>
        ///  Note: This only creates the event. The event wont actually trigger until SignalEvent()<br/>
        ///  is called using this created conversation event as an argument.<br/>
        ///  For example:<br/>
        ///      SignalEvent(oCreature, EventConversation());<br/>
        ///  Once the event has been signaled. The script associated with the OnConversation event will<br/>
        ///  run on the creature oCreature.<br/>
        /// <br/>
        ///  To specify the OnConversation script that should run, view the Creature Properties on<br/>
        ///  the creature and click on the Scripts Tab. Then specify a script for the OnConversation event.
        /// </summary>
        public static Event EventConversation()
        {
            return NWN.Core.NWScript.EventConversation();
        }

        /// <summary>
        ///  Set the difficulty level of oEncounter.<br/>
        ///  - nEncounterDifficulty: ENCOUNTER_DIFFICULTY_*<br/>
        ///  - oEncounter
        /// </summary>
        public static void SetEncounterDifficulty(int nEncounterDifficulty, uint oEncounter = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetEncounterDifficulty(nEncounterDifficulty, oEncounter);
        }

        /// <summary>
        ///  Get the difficulty level of oEncounter.
        /// </summary>
        public static int GetEncounterDifficulty(uint oEncounter = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetEncounterDifficulty(oEncounter);
        }

        /// <summary>
        ///  Get the distance between lLocationA and lLocationB.
        /// </summary>
        public static float GetDistanceBetweenLocations(Location lLocationA, Location lLocationB)
        {
            return NWN.Core.NWScript.GetDistanceBetweenLocations(lLocationA, lLocationB);
        }

        /// <summary>
        ///  Use this in spell scripts to get nDamage adjusted by oTarget&apos;s reflex and
        ///  evasion saves.<br/>
        ///  - nDamage<br/>
        ///  - oTarget<br/>
        ///  - nDC: Difficulty check<br/>
        ///  - nSaveType: SAVING_THROW_TYPE_*<br/>
        ///  - oSaveVersus
        /// </summary>
        public static int GetReflexAdjustedDamage(int nDamage, uint oTarget, int nDC, SavingThrowType nSaveType = SavingThrowType.None, uint oSaveVersus = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetReflexAdjustedDamage(nDamage, oTarget, nDC, (int)nSaveType, oSaveVersus);
        }

        /// <summary>
        ///  Play nAnimation immediately.<br/>
        ///  - nAnimation: ANIMATION_*<br/>
        ///  - fSpeed<br/>
        ///  - fSeconds
        /// </summary>
        public static void PlayAnimation(AnimationType nAnimation, float fSpeed = 1.0f, float fSeconds = 0.0f)
        {
            NWN.Core.NWScript.PlayAnimation((int)nAnimation, fSpeed, fSeconds);
        }

        /// <summary>
        ///  Create a Spell Talent.<br/>
        ///  - nSpell: SPELL_*
        /// </summary>
        public static Talent TalentSpell(int nSpell)
        {
            return NWN.Core.NWScript.TalentSpell(nSpell);
        }

        /// <summary>
        ///  Create a Feat Talent.<br/>
        ///  - nFeat: FEAT_*
        /// </summary>
        public static Talent TalentFeat(int nFeat)
        {
            return NWN.Core.NWScript.TalentFeat(nFeat);
        }

        /// <summary>
        ///  Create a Skill Talent.<br/>
        ///  - nSkill: SKILL_*
        /// </summary>
        public static Talent TalentSkill(int nSkill)
        {
            return NWN.Core.NWScript.TalentSkill(nSkill);
        }
        /// <summary>
        ///  Determines whether oObject has any effects applied by nSpell<br/>
        ///  - nSpell: SPELL_*<br/>
        ///  - oObject<br/>
        ///  * The spell id on effects is only valid if the effect is created<br/>
        ///    when the spell script runs. If it is created in a delayed command<br/>
        ///    then the spell id on the effect will be invalid.
        /// </summary>
        public static bool GetHasSpellEffect(SpellType nSpell, uint oObject = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetHasSpellEffect((int)nSpell, oObject) == 1;
        }

        /// <summary>
        ///  Get the spell (SPELL_*) that applied eSpellEffect.<br/>
        ///  * Returns -1 if eSpellEffect was applied outside a spell script.
        /// </summary>
        public static SpellType GetEffectSpellId(Effect eSpellEffect)
        {
            return (SpellType)NWN.Core.NWScript.GetEffectSpellId(eSpellEffect);
        }

        /// <summary>
        ///  Determine whether oCreature has tTalent.
        /// </summary>
        public static bool GetCreatureHasTalent(Talent tTalent, uint oCreature = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetCreatureHasTalent(tTalent, oCreature) == 1;
        }

        /// <summary>
        ///  Get a random talent of oCreature, within nCategory.<br/>
        ///  - nCategory: TALENT_CATEGORY_*<br/>
        ///  - oCreature
        /// </summary>
        public static Talent GetCreatureTalentRandom(TalentCategoryType nCategory, uint oCreature = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetCreatureTalentRandom((int)nCategory, oCreature);
        }

        /// <summary>
        ///  Get the best talent (i.e. closest to nCRMax without going over) of oCreature,<br/>
        ///  within nCategory.<br/>
        ///  - nCategory: TALENT_CATEGORY_*<br/>
        ///  - nCRMax: Challenge Rating of the talent<br/>
        ///  - oCreature
        /// </summary>
        public static Talent GetCreatureTalentBest(TalentCategoryType nCategory, int nCRMax, uint oCreature = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetCreatureTalentBest((int)nCategory, nCRMax, oCreature);
        }

        /// <summary>
        ///  Use tChosenTalent on oTarget.
        /// </summary>
        public static void ActionUseTalentOnObject(Talent tChosenTalent, uint oTarget)
        {
            NWN.Core.NWScript.ActionUseTalentOnObject(tChosenTalent, oTarget);
        }

        /// <summary>
        ///  Use tChosenTalent at lTargetLocation.
        /// </summary>
        public static void ActionUseTalentAtLocation(Talent tChosenTalent, Location lTargetLocation)
        {
            NWN.Core.NWScript.ActionUseTalentAtLocation(tChosenTalent, lTargetLocation);
        }

        /// <summary>
        ///  Get the gold piece value of oItem.<br/>
        ///  * Returns 0 if oItem is not a valid item.
        /// </summary>
        public static int GetGoldPieceValue(uint oItem)
        {
            return NWN.Core.NWScript.GetGoldPieceValue(oItem);
        }

        /// <summary>
        ///  * Returns true if oCreature is of a playable racial type.
        /// </summary>
        public static bool GetIsPlayableRacialType(uint oCreature)
        {
            return NWN.Core.NWScript.GetIsPlayableRacialType(oCreature) == 1;
        }


        /// <summary>
        ///  Jump to lDestination.  The action is added to the TOP of the action queue.
        /// </summary>
        public static void JumpToLocation(Location lDestination)
        {
            NWN.Core.NWScript.JumpToLocation(lDestination);
        }

        /// <summary>
        ///  Create a Temporary Hitpoints effect.<br/>
        ///  - nHitPoints: a positive integer<br/>
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nHitPoints &lt; 0.
        /// </summary>
        public static Effect EffectTemporaryHitpoints(int nHitPoints)
        {
            return NWN.Core.NWScript.EffectTemporaryHitpoints(nHitPoints);
        }

        /// <summary>
        ///  Get the number of ranks that oTarget has in nSkill.<br/>
        ///  - nSkill: SKILL_*<br/>
        ///  - oTarget<br/>
        ///  - nBaseSkillRank: if set to true returns the number of base skill ranks the target<br/>
        ///                    has (i.e. not including any bonuses from ability scores, feats, etc).<br/>
        ///  * Returns -1 if oTarget doesn&apos;t have nSkill.<br/>
        ///  * Returns 0 if nSkill is untrained.
        /// </summary>
        public static int GetSkillRank(int nSkill, uint oTarget = OBJECT_INVALID, bool nBaseSkillRank = false)
        {
            return NWN.Core.NWScript.GetSkillRank(nSkill, oTarget, nBaseSkillRank ? 1 : 0);
        }

        /// <summary>
        ///  Get the attack target of oCreature.<br/>
        ///  This only works when oCreature is in combat.
        /// </summary>
        public static uint GetAttackTarget(uint oCreature = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetAttackTarget(oCreature);
        }

        /// <summary>
        ///  Get the attack type (SPECIAL_ATTACK_*) of oCreature&apos;s last attack.<br/>
        ///  This only works when oCreature is in combat.
        /// </summary>
        public static SpecialAttackType GetLastAttackType(uint oCreature = OBJECT_INVALID)
        {
            return (SpecialAttackType)NWN.Core.NWScript.GetLastAttackType(oCreature);
        }

        /// <summary>
        ///  Get the attack mode (COMBAT_MODE_*) of oCreature&apos;s last attack.<br/>
        ///  This only works when oCreature is in combat.
        /// </summary>
        public static CombatModeType GetLastAttackMode(uint oCreature = OBJECT_INVALID)
        {
            return (CombatModeType)NWN.Core.NWScript.GetLastAttackMode(oCreature);
        }


        /// <summary>
        ///  Get the master of oAssociate.
        /// </summary>
        public static uint GetMaster(uint oAssociate = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetMaster(oAssociate);
        }

        /// <summary>
        ///  * Returns true if oCreature is in combat.
        /// </summary>
        public static bool GetIsInCombat(uint oCreature = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetIsInCombat(oCreature) == 1;
        }

        /// <summary>
        ///  Get the last command (ASSOCIATE_COMMAND_*) issued to oAssociate.
        /// </summary>
        public static AssociateCommandType GetLastAssociateCommand(uint oAssociate = OBJECT_INVALID)
        {
            return (AssociateCommandType)NWN.Core.NWScript.GetLastAssociateCommand(oAssociate);
        }

        /// <summary>
        ///  Give nGP gold to oCreature.
        /// </summary>
        public static void GiveGoldToCreature(uint oCreature, int nGP)
        {
            NWN.Core.NWScript.GiveGoldToCreature(oCreature, nGP);
        }

        /// <summary>
        ///  Set the destroyable status of the caller.<br/>
        ///  - bDestroyable: If this is false, the caller does not fade out on death, but<br/>
        ///    sticks around as a corpse.<br/>
        ///  - bRaiseable: If this is true, the caller can be raised via resurrection.<br/>
        ///  - bSelectableWhenDead: If this is true, the caller is selectable after death.
        /// </summary>
        public static void SetIsDestroyable(bool bDestroyable, bool bRaiseable = true, bool bSelectableWhenDead = false)
        {
            NWN.Core.NWScript.SetIsDestroyable(bDestroyable ? 1 : 0, bRaiseable ? 1 : 0, bSelectableWhenDead ? 1 : 0);
        }

        /// <summary>
        ///  Set the locked state of oTarget, which can be a door or a placeable object.
        /// </summary>
        public static void SetLocked(uint oTarget, bool bLocked)
        {
            NWN.Core.NWScript.SetLocked(oTarget, bLocked ? 1 : 0);
        }

        /// <summary>
        ///  Get the locked state of oTarget, which can be a door or a placeable object.
        /// </summary>
        public static bool GetLocked(uint oTarget)
        {
            return NWN.Core.NWScript.GetLocked(oTarget) == 1;
        }


        /// <summary>
        ///  Use this in a trigger&apos;s OnClick event script to get the object that last
        ///  clicked on it.<br/>
        ///  This is identical to GetEnteringObject.<br/>
        ///  GetClickingObject() should not be called from a placeable&apos;s OnClick event,<br/>
        ///  instead use GetPlaceableLastClickedBy();
        /// </summary>
        public static uint GetClickingObject()
        {
            return NWN.Core.NWScript.GetClickingObject();
        }

        /// <summary>
        ///  Initialise oTarget to listen for the standard Associates commands.
        /// </summary>
        public static void SetAssociateListenPatterns(uint oTarget = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetAssociateListenPatterns(oTarget);
        }

        /// <summary>
        ///  Get the last weapon that oCreature used in an attack.<br/>
        ///  * Returns OBJECT_INVALID if oCreature did not attack, or has no weapon equipped.
        /// </summary>
        public static uint GetLastWeaponUsed(uint oCreature)
        {
            return NWN.Core.NWScript.GetLastWeaponUsed(oCreature);
        }

        /// <summary>
        ///  Use oPlaceable.
        /// </summary>
        public static void ActionInteractObject(uint oPlaceable)
        {
            NWN.Core.NWScript.ActionInteractObject(oPlaceable);
        }

        /// <summary>
        ///  Get the last object that used the placeable object that is calling this function.<br/>
        ///  * Returns OBJECT_INVALID if it is called by something other than a placeable or
        ///    a door.
        /// </summary>
        public static uint GetLastUsedBy()
        {
            return NWN.Core.NWScript.GetLastUsedBy();
        }

        /// <summary>
        ///  Returns the ability modifier for the specified ability<br/>
        ///  Get oCreature&apos;s ability modifier for nAbility.<br/>
        ///  - nAbility: ABILITY_*<br/>
        ///  - oCreature
        /// </summary>
        public static int GetAbilityModifier(AbilityType nAbility, uint oCreature = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetAbilityModifier((int)nAbility, oCreature);
        }

        /// <summary>
        ///  Determined whether oItem has been identified.
        /// </summary>
        public static bool GetIdentified(uint oItem)
        {
            return NWN.Core.NWScript.GetIdentified(oItem) == 1;
        }

        /// <summary>
        ///  Set whether oItem has been identified.
        /// </summary>
        public static void SetIdentified(uint oItem, bool bIdentified)
        {
            NWN.Core.NWScript.SetIdentified(oItem, bIdentified ? 1 : 0);
        }

        /// <summary>
        ///  Summon an Animal Companion
        /// </summary>
        public static void SummonAnimalCompanion(uint oMaster = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SummonAnimalCompanion(oMaster);
        }

        /// <summary>
        ///  Summon a Familiar
        /// </summary>
        public static void SummonFamiliar(uint oMaster = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SummonFamiliar(oMaster);
        }

        /// <summary>
        ///  Get the last blocking door encountered by the caller of this function.<br/>
        ///  * Returns OBJECT_INVALID if the caller is not a valid creature.
        /// </summary>
        public static uint GetBlockingDoor()
        {
            return NWN.Core.NWScript.GetBlockingDoor();
        }

        /// <summary>
        ///  - oTargetDoor<br/>
        ///  - nDoorAction: DOOR_ACTION_*<br/>
        ///  * Returns true if nDoorAction can be performed on oTargetDoor.
        /// </summary>
        public static bool GetIsDoorActionPossible(uint oTargetDoor, DoorActionType nDoorAction)
        {
            return NWN.Core.NWScript.GetIsDoorActionPossible(oTargetDoor, (int)nDoorAction) == 1;
        }

        /// <summary>
        ///  Perform nDoorAction on oTargetDoor.
        /// </summary>
        public static void DoDoorAction(uint oTargetDoor, DoorActionType nDoorAction)
        {
            NWN.Core.NWScript.DoDoorAction(oTargetDoor, (int)nDoorAction);
        }

        /// <summary>
        ///  Get the first item in oTarget&apos;s inventory (start to cycle through oTarget&apos;s
        ///  inventory).<br/>
        ///  * Returns OBJECT_INVALID if the caller is not a creature, item, placeable or store,<br/>
        ///    or if no item is found.
        /// </summary>
        public static uint GetFirstItemInInventory(uint oTarget = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetFirstItemInInventory(oTarget);
        }

        /// <summary>
        ///  Get the next item in oTarget&apos;s inventory (continue to cycle through oTarget&apos;s
        ///  inventory).<br/>
        ///  * Returns OBJECT_INVALID if the caller is not a creature, item, placeable or store,<br/>
        ///    or if no item is found.
        /// </summary>
        public static uint GetNextItemInInventory(uint oTarget = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetNextItemInInventory(oTarget);
        }

        /// <summary>
        ///  A creature can have up to three classes.  This function determines the
        ///  creature's class (CLASS_TYPE_*) based on nClassPosition.
        ///  - nClassPosition: 1, 2 or 3
        ///  - oCreature
        ///  * Returns CLASS_TYPE_INVALID if the oCreature does not have a class in
        ///    nClassPosition (i.e. a single-class creature will only have a value in
        ///    nClassLocation=1) or if oCreature is not a valid creature.
        /// </summary>
        public static ClassType GetClassByPosition(int nClassPosition, uint oCreature = OBJECT_INVALID)
        {
            return (ClassType)NWN.Core.NWScript.GetClassByPosition(nClassPosition, oCreature);
        }

        /// <summary>
        ///  A creature can have up to three classes.  This function determines the
        ///  creature's class level based on nClass Position.
        ///  - nClassPosition: 1, 2 or 3
        ///  - oCreature
        ///  * Returns 0 if oCreature does not have a class in nClassPosition
        ///    (i.e. a single-class creature will only have a value in nClassLocation=1)
        ///    or if oCreature is not a valid creature.
        /// </summary>
        public static int GetLevelByPosition(int nClassPosition, uint oCreature = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetLevelByPosition(nClassPosition, oCreature);
        }

        /// <summary>
        ///  Determine the levels that oCreature holds in nClassType.
        ///  - nClassType: CLASS_TYPE_*
        ///  - oCreature
        /// </summary>
        public static int GetLevelByClass(ClassType nClassType, uint oCreature = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetLevelByClass((int)nClassType, oCreature);
        }

        /// <summary>
        ///  Get the amount of damage of type nDamageType that has been dealt to the caller.
        ///  - nDamageType: DAMAGE_TYPE_*
        /// </summary>
        public static int GetDamageDealtByType(DamageType nDamageType)
        {
            return NWN.Core.NWScript.GetDamageDealtByType((int)nDamageType);
        }

        /// <summary>
        ///  Get the total amount of damage that has been dealt to the caller.
        /// </summary>
        public static int GetTotalDamageDealt()
        {
            return NWN.Core.NWScript.GetTotalDamageDealt();
        }

        /// <summary>
        ///  Get the last object that damaged oObject
        ///  * Returns OBJECT_INVALID if the passed in object is not a valid object.
        /// </summary>
        public static uint GetLastDamager(uint oObject = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetLastDamager(oObject);
        }

        /// <summary>
        ///  Get the last object that disarmed the trap on the caller.
        ///  * Returns OBJECT_INVALID if the caller is not a valid placeable, trigger or
        ///    door.
        /// </summary>
        public static uint GetLastDisarmed()
        {
            return NWN.Core.NWScript.GetLastDisarmed();
        }

        /// <summary>
        ///  Get the last object that disturbed the inventory of the caller.
        ///  * Returns OBJECT_INVALID if the caller is not a valid creature or placeable.
        /// </summary>
        public static uint GetLastDisturbed()
        {
            return NWN.Core.NWScript.GetLastDisturbed();
        }

        /// <summary>
        ///  Get the last object that locked the caller.
        ///  * Returns OBJECT_INVALID if the caller is not a valid door or placeable.
        /// </summary>
        public static uint GetLastLocked()
        {
            return NWN.Core.NWScript.GetLastLocked();
        }

        /// <summary>
        ///  Get the last object that unlocked the caller.
        ///  * Returns OBJECT_INVALID if the caller is not a valid door or placeable.
        /// </summary>
        public static uint GetLastUnlocked()
        {
            return NWN.Core.NWScript.GetLastUnlocked();
        }

        /// <summary>
        ///  Create a Skill Increase effect.
        ///  - nSkill: SKILL_*
        ///  - nValue
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nSkill is invalid.
        /// </summary>
        public static Effect EffectSkillIncrease(int nSkill, int nValue)
        {
            return NWN.Core.NWScript.EffectSkillIncrease(nSkill, nValue);
        }

        /// <summary>
        ///  Get the type of disturbance (INVENTORY_DISTURB_*) that caused the caller's
        ///  OnInventoryDisturbed script to fire.  This will only work for creatures and
        ///  placeables.
        /// </summary>
        public static InventoryDisturbType GetInventoryDisturbType()
        {
            return (InventoryDisturbType)NWN.Core.NWScript.GetInventoryDisturbType();
        }


        /// <summary>
        ///  get the item that caused the caller's OnInventoryDisturbed script to fire.
        ///  * Returns OBJECT_INVALID if the caller is not a valid object.
        /// </summary>
        public static uint GetInventoryDisturbItem()
        {
            return NWN.Core.NWScript.GetInventoryDisturbItem();
        }

        /// <summary>
        ///  Get the henchman belonging to oMaster.
        ///  * Return OBJECT_INVALID if oMaster does not have a henchman.
        ///  -nNth: Which henchman to return.
        /// </summary>
        public static uint GetHenchman(uint oMaster = OBJECT_INVALID, int nNth = 1)
        {
            return NWN.Core.NWScript.GetHenchman(oMaster, nNth);
        }

        /// <summary>
        ///  Set eEffect to be versus a specific alignment.
        ///  - eEffect
        ///  - nLawChaos: ALIGNMENT_LAWFUL/ALIGNMENT_CHAOTIC/ALIGNMENT_ALL
        ///  - nGoodEvil: ALIGNMENT_GOOD/ALIGNMENT_EVIL/ALIGNMENT_ALL
        /// </summary>
        public static Effect VersusAlignmentEffect(Effect eEffect, AlignmentType nLawChaos = AlignmentType.All, AlignmentType nGoodEvil = AlignmentType.All)
        {
            return NWN.Core.NWScript.VersusAlignmentEffect(eEffect, (int)nLawChaos, (int)nGoodEvil);
        }

        /// <summary>
        ///  Set eEffect to be versus nRacialType.
        ///  - eEffect
        ///  - nRacialType: RACIAL_TYPE_*
        /// </summary>
        public static Effect VersusRacialTypeEffect(Effect eEffect, RacialType nRacialType)
        {
            return NWN.Core.NWScript.VersusRacialTypeEffect(eEffect, (int)nRacialType);
        }

        /// <summary>
        ///  Set eEffect to be versus traps.
        /// </summary>
        public static Effect VersusTrapEffect(Effect eEffect)
        {
            return NWN.Core.NWScript.VersusTrapEffect(eEffect);
        }

        /// <summary>
        ///  Get the gender of oCreature.
        /// </summary>
        public static GenderType GetGender(uint oCreature)
        {
            return (GenderType)NWN.Core.NWScript.GetGender(oCreature);
        }

        /// <summary>
        ///  * Returns true if tTalent is valid.
        /// </summary>
        public static bool GetIsTalentValid(Talent tTalent)
        {
            return NWN.Core.NWScript.GetIsTalentValid(tTalent) == 1;
        }


        /// <summary>
        ///  Causes the action subject to move away from lMoveAwayFrom.
        /// </summary>
        public static void ActionMoveAwayFromLocation(Location lMoveAwayFrom, bool bRun = false, float fMoveAwayRange = 40.0f)
        {
            NWN.Core.NWScript.ActionMoveAwayFromLocation(lMoveAwayFrom, bRun ? 1 : 0, fMoveAwayRange);
        }

        /// <summary>
        ///  Get the target that the caller attempted to attack - this should be used in
        ///  conjunction with GetAttackTarget(). This value is set every time an attack is
        ///  made, and is reset at the end of combat.
        ///  * Returns OBJECT_INVALID if the caller is not a valid creature.
        /// </summary>
        public static uint GetAttemptedAttackTarget()
        {
            return NWN.Core.NWScript.GetAttemptedAttackTarget();
        }

        /// <summary>
        ///  Get the type (TALENT_TYPE_*) of tTalent.
        /// </summary>
        public static TalentType GetTypeFromTalent(Talent tTalent)
        {
            return (TalentType)NWN.Core.NWScript.GetTypeFromTalent(tTalent);
        }


        /// <summary>
        ///  Get the ID of tTalent.  This could be a SPELL_*, FEAT_* or SKILL_*.
        /// </summary>
        public static int GetIdFromTalent(Talent tTalent)
        {
            return NWN.Core.NWScript.GetIdFromTalent(tTalent);
        }

        /// <summary>
        ///  Get the associate of type nAssociateType belonging to oMaster.
        ///  - nAssociateType: ASSOCIATE_TYPE_*
        ///  - nMaster
        ///  - nTh: Which associate of the specified type to return
        ///  * Returns OBJECT_INVALID if no such associate exists.
        /// </summary>
        public static uint GetAssociate(AssociateType nAssociateType, uint oMaster = OBJECT_INVALID, int nTh = 1)
        {
            return NWN.Core.NWScript.GetAssociate((int)nAssociateType, oMaster, nTh);
        }

        /// <summary>
        ///  Add oHenchman as a henchman to oMaster
        ///  If oHenchman is either a DM or a player character, this will have no effect.
        /// </summary>
        public static void AddHenchman(uint oMaster, uint oHenchman = OBJECT_INVALID)
        {
            NWN.Core.NWScript.AddHenchman(oMaster, oHenchman);
        }

        /// <summary>
        ///  Remove oHenchman from the service of oMaster, returning them to their original faction.
        /// </summary>
        public static void RemoveHenchman(uint oMaster, uint oHenchman = OBJECT_INVALID)
        {
            NWN.Core.NWScript.RemoveHenchman(oMaster, oHenchman);
        }

        /// <summary>
        ///  Add a journal quest entry to oCreature.
        ///  - szPlotID: the plot identifier used in the toolset's Journal Editor
        ///  - nState: the state of the plot as seen in the toolset's Journal Editor
        ///  - oCreature
        ///  - bAllPartyMembers: If this is true, the entry will show up in the journal of
        ///    everyone in the party
        ///  - bAllPlayers: If this is true, the entry will show up in the journal of
        ///    everyone in the world
        ///  - bAllowOverrideHigher: If this is true, you can set the state to a lower
        ///    number than the one it is currently on
        /// </summary>
        public static void AddJournalQuestEntry(string szPlotID, int nState, uint oCreature, bool bAllPartyMembers = true, bool bAllPlayers = false, bool bAllowOverrideHigher = false)
        {
            NWN.Core.NWScript.AddJournalQuestEntry(szPlotID, nState, oCreature, bAllPartyMembers ? 1 : 0, bAllPlayers ? 1 : 0, bAllowOverrideHigher ? 1 : 0);
        }

        /// <summary>
        ///  Remove a journal quest entry from oCreature.
        ///  - szPlotID: the plot identifier used in the toolset's Journal Editor
        ///  - oCreature
        ///  - bAllPartyMembers: If this is true, the entry will be removed from the
        ///    journal of everyone in the party
        ///  - bAllPlayers: If this is true, the entry will be removed from the journal of
        ///    everyone in the world
        /// </summary>
        public static void RemoveJournalQuestEntry(string szPlotID, uint oCreature, bool bAllPartyMembers = true, bool bAllPlayers = false)
        {
            NWN.Core.NWScript.RemoveJournalQuestEntry(szPlotID, oCreature, bAllPartyMembers ? 1 : 0, bAllPlayers ? 1 : 0);
        }

        /// <summary>
        ///  Get the public part of the CD Key that oPlayer used when logging in.
        ///  - nSinglePlayerCDKey: If set to true, the player's public CD Key will
        ///    be returned when the player is playing in single player mode
        ///    (otherwise returns an empty string in single player mode).
        /// </summary>
        public static string GetPCPublicCDKey(uint oPlayer, bool nSinglePlayerCDKey = false)
        {
            return NWN.Core.NWScript.GetPCPublicCDKey(oPlayer, nSinglePlayerCDKey ? 1 : 0);
        }

        /// <summary>
        ///  Get the IP address from which oPlayer has connected.
        /// </summary>
        public static string GetPCIPAddress(uint oPlayer)
        {
            return NWN.Core.NWScript.GetPCIPAddress(oPlayer);
        }

        /// <summary>
        ///  Get the name of oPlayer.
        /// </summary>
        public static string GetPCPlayerName(uint oPlayer)
        {
            return NWN.Core.NWScript.GetPCPlayerName(oPlayer);
        }

        /// <summary>
        ///  Sets oPlayer's like towards oTarget.
        /// </summary>
        public static void SetPCLike(uint oPlayer, uint oTarget)
        {
            NWN.Core.NWScript.SetPCLike(oPlayer, oTarget);
        }

        /// <summary>
        ///  Sets oPlayer's dislike towards oTarget.
        /// </summary>
        public static void SetPCDislike(uint oPlayer, uint oTarget)
        {
            NWN.Core.NWScript.SetPCDislike(oPlayer, oTarget);
        }

        /// <summary>
        ///  Send a server message (szMessage) to the oPlayer.
        /// </summary>
        public static void SendMessageToPC(uint oPlayer, string szMessage)
        {
            NWN.Core.NWScript.SendMessageToPC(oPlayer, szMessage);
        }

        /// <summary>
        ///  Get the target at which the caller attempted to cast a spell.
        ///  This value is set every time a spell is cast and is reset at the end of
        ///  combat.
        ///  * Returns OBJECT_INVALID if the caller is not a valid creature.
        /// </summary>
        public static uint GetAttemptedSpellTarget()
        {
            return NWN.Core.NWScript.GetAttemptedSpellTarget();
        }

        /// <summary>
        ///  Get the last creature that opened the caller.
        ///  * Returns OBJECT_INVALID if the caller is not a valid door, placeable or store.
        /// </summary>
        public static uint GetLastOpenedBy()
        {
            return NWN.Core.NWScript.GetLastOpenedBy();
        }

        /// <summary>
        ///  Determines the number of times that oCreature has nSpell memorised.
        ///  - nSpell: SPELL_*
        ///  - oCreature
        /// </summary>
        public static int GetHasSpell(int nSpell, uint oCreature = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetHasSpell(nSpell, oCreature);
        }

        /// <summary>
        ///  Open oStore for oPC.
        ///  - nBonusMarkUp is added to the stores default mark up percentage on items sold (-100 to 100)
        ///  - nBonusMarkDown is added to the stores default mark down percentage on items bought (-100 to 100)
        /// </summary>
        public static void OpenStore(uint oStore, uint oPC, int nBonusMarkUp = 0, int nBonusMarkDown = 0)
        {
            NWN.Core.NWScript.OpenStore(oStore, oPC, nBonusMarkUp, nBonusMarkDown);
        }

        /// <summary>
        ///  Create a Turned effect.
        ///  Turned effects are supernatural by default.
        /// </summary>
        public static Effect EffectTurned()
        {
            return NWN.Core.NWScript.EffectTurned();
        }

        /// <summary>
        ///  Get the first member of oMemberOfFaction's faction (start to cycle through
        ///  oMemberOfFaction's faction).
        ///  * Returns OBJECT_INVALID if oMemberOfFaction's faction is invalid.
        /// </summary>
        public static uint GetFirstFactionMember(uint oMemberOfFaction, bool bPCOnly = true)
        {
            return NWN.Core.NWScript.GetFirstFactionMember(oMemberOfFaction, bPCOnly ? 1 : 0);
        }

        /// <summary>
        ///  Get the next member of oMemberOfFaction's faction (continue to cycle through
        ///  oMemberOfFaction's faction).
        ///  * Returns OBJECT_INVALID if oMemberOfFaction's faction is invalid.
        /// </summary>
        public static uint GetNextFactionMember(uint oMemberOfFaction, bool bPCOnly = true)
        {
            return NWN.Core.NWScript.GetNextFactionMember(oMemberOfFaction, bPCOnly ? 1 : 0);
        }

        /// <summary>
        ///  Force the action subject to move to lDestination.
        /// </summary>
        public static void ActionForceMoveToLocation(Location lDestination, bool bRun = false, float fTimeout = 30.0f)
        {
            NWN.Core.NWScript.ActionForceMoveToLocation(lDestination, bRun ? 1 : 0, fTimeout);
        }

        /// <summary>
        ///  Force the action subject to move to oMoveTo.
        /// </summary>
        public static void ActionForceMoveToObject(uint oMoveTo, bool bRun = false, float fRange = 1.0f, float fTimeout = 30.0f)
        {
            NWN.Core.NWScript.ActionForceMoveToObject(oMoveTo, bRun ? 1 : 0, fRange, fTimeout);
        }

        /// <summary>
        ///  Get the experience assigned in the journal editor for szPlotID.
        /// </summary>
        public static int GetJournalQuestExperience(string szPlotID)
        {
            return NWN.Core.NWScript.GetJournalQuestExperience(szPlotID);
        }

        /// <summary>
        ///  Jump to oToJumpTo (the action is added to the top of the action queue).
        /// </summary>
        public static void JumpToObject(uint oToJumpTo, int nWalkStraightLineToPoint = 1)
        {
            NWN.Core.NWScript.JumpToObject(oToJumpTo, nWalkStraightLineToPoint);
        }

        /// <summary>
        ///  Set whether oMapPin is enabled.
        ///  - oMapPin
        ///  - nEnabled: 0=Off, 1=On
        /// </summary>
        public static void SetMapPinEnabled(uint oMapPin, int nEnabled)
        {
            NWN.Core.NWScript.SetMapPinEnabled(oMapPin, nEnabled);
        }

        /// <summary>
        ///  Create a Hit Point Change When Dying effect.
        ///  - fHitPointChangePerRound: this can be positive or negative, but not zero.
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if fHitPointChangePerRound is 0.
        /// </summary>
        public static Effect EffectHitPointChangeWhenDying(float fHitPointChangePerRound)
        {
            return NWN.Core.NWScript.EffectHitPointChangeWhenDying(fHitPointChangePerRound);
        }

        /// <summary>
        ///  Spawn a GUI panel for the client that controls oPC.
        ///  Will force show panels disabled with SetGuiPanelDisabled()
        ///  - oPC
        ///  - nGUIPanel: GUI_PANEL_*, except GUI_PANEL_COMPASS / GUI_PANEL_LEVELUP / GUI_PANEL_GOLD_* / GUI_PANEL_EXAMINE_*
        ///  * Nothing happens if oPC is not a player character or if an invalid value is used for nGUIPanel.
        /// </summary>
        public static void PopUpGUIPanel(uint oPC, int nGUIPanel)
        {
            NWN.Core.NWScript.PopUpGUIPanel(oPC, nGUIPanel);
        }

        /// <summary>
        ///  Clear all personal feelings that oSource has about oTarget.
        /// </summary>
        public static void ClearPersonalReputation(uint oTarget, uint oSource = OBJECT_INVALID)
        {
            NWN.Core.NWScript.ClearPersonalReputation(oTarget, oSource);
        }

        /// <summary>
        ///  oSource will temporarily be friends towards oTarget.
        ///  bDecays determines whether the personal reputation value decays over time
        ///  fDurationInSeconds is the length of time that the temporary friendship lasts
        ///  Make oSource into a temporary friend of oTarget using personal reputation.
        ///  - oTarget
        ///  - oSource
        ///  - bDecays: If this is true, the friendship decays over fDurationInSeconds;
        ///    otherwise it is indefinite.
        ///  - fDurationInSeconds: This is only used if bDecays is true, it is how long
        ///    the friendship lasts.
        ///  Note: If bDecays is true, the personal reputation amount decreases in size
        ///  over fDurationInSeconds. Friendship will only be in effect as long as
        ///  (faction reputation + total personal reputation) >= REPUTATION_TYPE_FRIEND.
        /// </summary>
        public static void SetIsTemporaryFriend(uint oTarget, uint oSource = OBJECT_INVALID, bool bDecays = false, float fDurationInSeconds = 180.0f)
        {
            NWN.Core.NWScript.SetIsTemporaryFriend(oTarget, oSource, bDecays ? 1 : 0, fDurationInSeconds);
        }

        /// <summary>
        ///  Make oSource into a temporary enemy of oTarget using personal reputation.
        ///  - oTarget
        ///  - oSource
        ///  - bDecays: If this is true, the enmity decays over fDurationInSeconds;
        ///    otherwise it is indefinite.
        ///  - fDurationInSeconds: This is only used if bDecays is true, it is how long
        ///    the enmity lasts.
        ///  Note: If bDecays is true, the personal reputation amount decreases in size
        ///  over fDurationInSeconds. Enmity will only be in effect as long as
        ///  (faction reputation + total personal reputation) <= REPUTATION_TYPE_ENEMY.
        /// </summary>
        public static void SetIsTemporaryEnemy(uint oTarget, uint oSource = OBJECT_INVALID, bool bDecays = false, float fDurationInSeconds = 180.0f)
        {
            NWN.Core.NWScript.SetIsTemporaryEnemy(oTarget, oSource, bDecays ? 1 : 0, fDurationInSeconds);
        }

        /// <summary>
        ///  Make oSource temporarily neutral to oTarget using personal reputation.
        ///  - oTarget
        ///  - oSource
        ///  - bDecays: If this is true, the neutrality decays over fDurationInSeconds;
        ///    otherwise it is indefinite.
        ///  - fDurationInSeconds: This is only used if bDecays is true, it is how long
        ///    the neutrality lasts.
        ///  Note: If bDecays is true, the personal reputation amount decreases in size
        ///  over fDurationInSeconds. Neutrality will only be in effect as long as
        ///  (faction reputation + total personal reputation) > REPUTATION_TYPE_ENEMY and
        ///  (faction reputation + total personal reputation) < REPUTATION_TYPE_FRIEND.
        /// </summary>
        public static void SetIsTemporaryNeutral(uint oTarget, uint oSource = OBJECT_INVALID, bool bDecays = false, float fDurationInSeconds = 180.0f)
        {
            NWN.Core.NWScript.SetIsTemporaryNeutral(oTarget, oSource, bDecays ? 1 : 0, fDurationInSeconds);
        }

        /// <summary>
        ///  Gives nXpAmount to oCreature.
        /// </summary>
        public static void GiveXPToCreature(uint oCreature, int nXpAmount)
        {
            NWN.Core.NWScript.GiveXPToCreature(oCreature, nXpAmount);
        }

        /// <summary>
        ///  Sets oCreature's experience to nXpAmount.
        /// </summary>
        public static void SetXP(uint oCreature, int nXpAmount)
        {
            NWN.Core.NWScript.SetXP(oCreature, nXpAmount);
        }

        /// <summary>
        ///  Get oCreature's experience.
        /// </summary>
        public static int GetXP(uint oCreature)
        {
            return NWN.Core.NWScript.GetXP(oCreature);
        }

        /// <summary>
        ///  Convert nInteger to hex, returning the hex value as a string.
        ///  * Return value has the format "0x????????" where each ? will be a hex digit
        ///    (8 digits in total).
        /// </summary>
        public static string IntToHexString(int nInteger)
        {
            return NWN.Core.NWScript.IntToHexString(nInteger);
        }

        /// <summary>
        ///  Get the base item type (BASE_ITEM_*) of oItem.
        ///  * Returns BASE_ITEM_INVALID if oItem is an invalid item.
        /// </summary>
        public static BaseItemType GetBaseItemType(uint oItem)
        {
            return (BaseItemType)NWN.Core.NWScript.GetBaseItemType(oItem);
        }

        /// <summary>
        ///  Determines whether oItem has nProperty.
        ///  - oItem
        ///  - nProperty: ITEM_PROPERTY_*
        ///  * Returns false if oItem is not a valid item, or if oItem does not have
        ///    nProperty.
        /// </summary>
        public static bool GetItemHasItemProperty(uint oItem, int nProperty)
        {
            return NWN.Core.NWScript.GetItemHasItemProperty(oItem, nProperty) == 1;
        }


        /// <summary>
        ///  The creature will equip the melee weapon in its possession that can do the
        ///  most damage. If no valid melee weapon is found, it will equip the most
        ///  damaging range weapon. This function should only ever be called in the
        ///  EndOfCombatRound scripts, because otherwise it would have to stop the combat
        ///  round to run simulation.
        ///  - oVersus: You can try to get the most damaging weapon against oVersus
        ///  - bOffHand
        /// </summary>
        public static void ActionEquipMostDamagingMelee(uint oVersus = OBJECT_INVALID, bool bOffHand = false)
        {
            NWN.Core.NWScript.ActionEquipMostDamagingMelee(oVersus, bOffHand ? 1 : 0);
        }

        /// <summary>
        ///  The creature will equip the range weapon in its possession that can do the
        ///  most damage.
        ///  If no valid range weapon can be found, it will equip the most damaging melee
        ///  weapon.
        ///  - oVersus: You can try to get the most damaging weapon against oVersus
        /// </summary>
        public static void ActionEquipMostDamagingRanged(uint oVersus = OBJECT_INVALID)
        {
            NWN.Core.NWScript.ActionEquipMostDamagingRanged(oVersus);
        }

        /// <summary>
        ///  Get the Armour Class of oItem.<br/>
        ///  * Return 0 if the oItem is not a valid item, or if oItem has no armour value.
        /// </summary>
        public static int GetItemACValue(uint oItem)
        {
            return NWN.Core.NWScript.GetItemACValue(oItem);
        }

        /// <summary>
        ///  The creature will rest if not in combat and no enemies are nearby.<br/>
        ///  - bCreatureToEnemyLineOfSightCheck: true to allow the creature to rest if enemies<br/>
        ///                                      are nearby, but the creature can't see the enemy.<br/>
        ///                                      false the creature will not rest if enemies are<br/>
        ///                                      nearby regardless of whether or not the creature<br/>
        ///                                      can see them, such as if an enemy is close by,<br/>
        ///                                      but is in a different room behind a closed door.
        /// </summary>
        public static void ActionRest(bool bCreatureToEnemyLineOfSightCheck = false)
        {
            NWN.Core.NWScript.ActionRest(bCreatureToEnemyLineOfSightCheck ? 1 : 0);
        }

        /// <summary>
        ///  Expose/Hide the entire map of oArea for oPlayer.<br/>
        ///  - oArea: The area that the map will be exposed/hidden for.<br/>
        ///  - oPlayer: The player the map will be exposed/hidden for.<br/>
        ///  - bExplored: true/FALSE. Whether the map should be completely explored or hidden.
        /// </summary>
        public static void ExploreAreaForPlayer(uint oArea, uint oPlayer, bool bExplored = true)
        {
            NWN.Core.NWScript.ExploreAreaForPlayer(oArea, oPlayer, bExplored ? 1 : 0);
        }

        /// <summary>
        ///  The creature will equip the armour in its possession that has the highest<br/>
        ///  armour class.
        /// </summary>
        public static void ActionEquipMostEffectiveArmor()
        {
            NWN.Core.NWScript.ActionEquipMostEffectiveArmor();
        }

        /// <summary>
        ///  * Returns true if it is currently day.
        /// </summary>
        public static bool GetIsDay()
        {
            return NWN.Core.NWScript.GetIsDay() == 1;
        }

        /// <summary>
        ///  * Returns true if it is currently night.
        /// </summary>
        public static bool GetIsNight()
        {
            return NWN.Core.NWScript.GetIsNight() == 1;
        }

        /// <summary>
        ///  * Returns true if it is currently dawn.
        /// </summary>
        public static bool GetIsDawn()
        {
            return NWN.Core.NWScript.GetIsDawn() == 1;
        }

        /// <summary>
        ///  * Returns true if it is currently dusk.
        /// </summary>
        public static bool GetIsDusk()
        {
            return NWN.Core.NWScript.GetIsDusk() == 1;
        }

        /// <summary>
        ///  * Returns true if oCreature was spawned from an encounter.
        /// </summary>
        public static int GetIsEncounterCreature(uint oCreature = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetIsEncounterCreature(oCreature);
        }

        /// <summary>
        ///  Use this in an OnPlayerDying module script to get the last player who is dying.
        /// </summary>
        public static uint GetLastPlayerDying()
        {
            return NWN.Core.NWScript.GetLastPlayerDying();
        }

        /// <summary>
        ///  Get the starting location of the module.
        /// </summary>
        public static Location GetStartingLocation()
        {
            return NWN.Core.NWScript.GetStartingLocation();
        }

        /// <summary>
        ///  Make oCreatureToChange join one of the standard factions.<br/>
        ///  ** This will only work on an NPC **<br/>
        ///  - nStandardFaction: STANDARD_FACTION_*
        /// </summary>
        public static void ChangeToStandardFaction(uint oCreatureToChange, int nStandardFaction)
        {
            NWN.Core.NWScript.ChangeToStandardFaction(oCreatureToChange, nStandardFaction);
        }

        /// <summary>
        ///  Play oSound.
        /// </summary>
        public static void SoundObjectPlay(uint oSound)
        {
            NWN.Core.NWScript.SoundObjectPlay(oSound);
        }

        /// <summary>
        ///  Stop playing oSound.
        /// </summary>
        public static void SoundObjectStop(uint oSound)
        {
            NWN.Core.NWScript.SoundObjectStop(oSound);
        }

        /// <summary>
        ///  Set the volume of oSound.<br/>
        ///  - oSound<br/>
        ///  - nVolume: 0-127
        /// </summary>
        public static void SoundObjectSetVolume(uint oSound, int nVolume)
        {
            NWN.Core.NWScript.SoundObjectSetVolume(oSound, nVolume);
        }

        /// <summary>
        ///  Set the position of oSound.
        /// </summary>
        public static void SoundObjectSetPosition(uint oSound, Vector3 vPosition)
        {
            NWN.Core.NWScript.SoundObjectSetPosition(oSound, vPosition);
        }

        /// <summary>
        ///  Immediately speak a conversation one-liner.<br/>
        ///  - sDialogResRef<br/>
        ///  - oTokenTarget: This must be specified if there are creature-specific tokens<br/>
        ///    in the string.
        /// </summary>
        public static void SpeakOneLinerConversation(string sDialogResRef = "", uint oTokenTarget = (int)ObjectType.Invalid)
        {
            NWN.Core.NWScript.SpeakOneLinerConversation(sDialogResRef, oTokenTarget);
        }

        /// <summary>
        ///  Get the amount of gold possessed by oTarget.
        /// </summary>
        public static int GetGold(uint oTarget = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetGold(oTarget);
        }

        /// <summary>
        ///  Use this in an OnRespawnButtonPressed module script to get the object id of<br/>
        ///  the player who last pressed the respawn button.
        /// </summary>
        public static uint GetLastRespawnButtonPresser()
        {
            return NWN.Core.NWScript.GetLastRespawnButtonPresser();
        }

        /// <summary>
        ///  * Returns true if oCreature is the Dungeon Master.<br/>
        ///  Note: This will return false if oCreature is a DM Possessed creature.<br/>
        ///  To determine if oCreature is a DM Possessed creature, use GetIsDMPossessed()
        /// </summary>
        public static bool GetIsDM(uint oCreature)
        {
            return NWN.Core.NWScript.GetIsDM(oCreature) == 1;
        }

        /// <summary>
        ///  Play a voice chat.<br/>
        ///  - nVoiceChatID: VOICE_CHAT_*<br/>
        ///  - oTarget
        /// </summary>
        public static void PlayVoiceChat(VoiceChatType nVoiceChatID, uint oTarget = OBJECT_INVALID)
        {
            NWN.Core.NWScript.PlayVoiceChat((int)nVoiceChatID, oTarget);
        }

        /// <summary>
        ///  * Returns true if the weapon equipped is capable of damaging oVersus.
        /// </summary>
        public static bool GetIsWeaponEffective(uint oVersus = OBJECT_INVALID, bool bOffHand = false)
        {
            return NWN.Core.NWScript.GetIsWeaponEffective(oVersus, bOffHand ? 1 : 0) == 1;
        }

        /// <summary>
        ///  Use this in a SpellCast script to determine whether the spell was considered<br/>
        ///  harmful.<br/>
        ///  * Returns true if the last spell cast was harmful.
        /// </summary>
        public static bool GetLastSpellHarmful()
        {
            return NWN.Core.NWScript.GetLastSpellHarmful() == 1;
        }

        /// <summary>
        ///  Activate oItem.
        /// </summary>
        public static Event EventActivateItem(uint oItem, Location lTarget, uint oTarget = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.EventActivateItem(oItem, lTarget, oTarget);
        }

        /// <summary>
        ///  Play the background music for oArea.
        /// </summary>
        public static void MusicBackgroundPlay(uint oArea)
        {
            NWN.Core.NWScript.MusicBackgroundPlay(oArea);
        }

        /// <summary>
        ///  Stop the background music for oArea.
        /// </summary>
        public static void MusicBackgroundStop(uint oArea)
        {
            NWN.Core.NWScript.MusicBackgroundStop(oArea);
        }

        /// <summary>
        ///  Set the delay for the background music for oArea.<br/>
        ///  - oArea<br/>
        ///  - nDelay: delay in milliseconds
        /// </summary>
        public static void MusicBackgroundSetDelay(uint oArea, int nDelay)
        {
            NWN.Core.NWScript.MusicBackgroundSetDelay(oArea, nDelay);
        }

        /// <summary>
        ///  Change the background day track for oArea to nTrack.<br/>
        ///  - oArea<br/>
        ///  - nTrack
        /// </summary>
        public static void MusicBackgroundChangeDay(uint oArea, int nTrack)
        {
            NWN.Core.NWScript.MusicBackgroundChangeDay(oArea, nTrack);
        }

        /// <summary>
        ///  Change the background night track for oArea to nTrack.<br/>
        ///  - oArea<br/>
        ///  - nTrack
        /// </summary>
        public static void MusicBackgroundChangeNight(uint oArea, int nTrack)
        {
            NWN.Core.NWScript.MusicBackgroundChangeNight(oArea, nTrack);
        }

        /// <summary>
        ///  Play the battle music for oArea.
        /// </summary>
        public static void MusicBattlePlay(uint oArea)
        {
            NWN.Core.NWScript.MusicBattlePlay(oArea);
        }

        /// <summary>
        ///  Stop the battle music for oArea.
        /// </summary>
        public static void MusicBattleStop(uint oArea)
        {
            NWN.Core.NWScript.MusicBattleStop(oArea);
        }

        /// <summary>
        ///  Change the battle track for oArea.<br/>
        ///  - oArea<br/>
        ///  - nTrack
        /// </summary>
        public static void MusicBattleChange(uint oArea, int nTrack)
        {
            NWN.Core.NWScript.MusicBattleChange(oArea, nTrack);
        }

        /// <summary>
        ///  Play the ambient sound for oArea.
        /// </summary>
        public static void AmbientSoundPlay(uint oArea)
        {
            NWN.Core.NWScript.AmbientSoundPlay(oArea);
        }

        /// <summary>
        ///  Stop the ambient sound for oArea.
        /// </summary>
        public static void AmbientSoundStop(uint oArea)
        {
            NWN.Core.NWScript.AmbientSoundStop(oArea);
        }

        /// <summary>
        ///  Change the ambient day track for oArea to nTrack.<br/>
        ///  - oArea<br/>
        ///  - nTrack
        /// </summary>
        public static void AmbientSoundChangeDay(uint oArea, int nTrack)
        {
            NWN.Core.NWScript.AmbientSoundChangeDay(oArea, nTrack);
        }

        /// <summary>
        ///  Change the ambient night track for oArea to nTrack.<br/>
        ///  - oArea<br/>
        ///  - nTrack
        /// </summary>
        public static void AmbientSoundChangeNight(uint oArea, int nTrack)
        {
            NWN.Core.NWScript.AmbientSoundChangeNight(oArea, nTrack);
        }

        /// <summary>
        ///  Get the object that killed the caller.
        /// </summary>
        public static uint GetLastKiller()
        {
            return NWN.Core.NWScript.GetLastKiller();
        }

        /// <summary>
        ///  Use this in a spell script to get the item used to cast the spell.
        /// </summary>
        public static uint GetSpellCastItem()
        {
            return NWN.Core.NWScript.GetSpellCastItem();
        }

        /// <summary>
        ///  Use this in an OnItemActivated module script to get the item that was activated.
        /// </summary>
        public static uint GetItemActivated()
        {
            return NWN.Core.NWScript.GetItemActivated();
        }

        /// <summary>
        ///  Use this in an OnItemActivated module script to get the creature that<br/>
        ///  activated the item.
        /// </summary>
        public static uint GetItemActivator()
        {
            return NWN.Core.NWScript.GetItemActivator();
        }

        /// <summary>
        ///  Use this in an OnItemActivated module script to get the location of the item's<br/>
        ///  target.
        /// </summary>
        public static Location GetItemActivatedTargetLocation()
        {
            return NWN.Core.NWScript.GetItemActivatedTargetLocation();
        }

        /// <summary>
        ///  Use this in an OnItemActivated module script to get the item's target.
        /// </summary>
        public static uint GetItemActivatedTarget()
        {
            return NWN.Core.NWScript.GetItemActivatedTarget();
        }

        /// <summary>
        ///  * Returns true if oObject (which is a placeable or a door) is currently open.
        /// </summary>
        public static bool GetIsOpen(uint oObject)
        {
            return NWN.Core.NWScript.GetIsOpen(oObject) == 1;
        }

        /// <summary>
        ///  Take nAmount of gold from oCreatureToTakeFrom.<br/>
        ///  - nAmount<br/>
        ///  - oCreatureToTakeFrom: If this is not a valid creature, nothing will happen.<br/>
        ///  - bDestroy: If this is true, the caller will not get the gold.  Instead, the<br/>
        ///    gold will be destroyed and will vanish from the game.
        /// </summary>
        public static void TakeGoldFromCreature(int nAmount, uint oCreatureToTakeFrom, bool bDestroy = false)
        {
            NWN.Core.NWScript.TakeGoldFromCreature(nAmount, oCreatureToTakeFrom, bDestroy ? 1 : 0);
        }

        /// <summary>
        ///  Determine whether oObject is in conversation.
        /// </summary>
        public static bool IsInConversation(uint oObject)
        {
            return NWN.Core.NWScript.IsInConversation(oObject) == 1;
        }

        /// <summary>
        ///  Create an Ability Decrease effect.<br/>
        ///  - nAbility: ABILITY_*<br/>
        ///  - nModifyBy: This is the amount by which to decrement the ability
        /// </summary>
        public static Effect EffectAbilityDecrease(int nAbility, int nModifyBy)
        {
            return NWN.Core.NWScript.EffectAbilityDecrease(nAbility, nModifyBy);
        }

        /// <summary>
        ///  Create an Attack Decrease effect.<br/>
        ///  - nPenalty<br/>
        ///  - nModifierType: ATTACK_BONUS_*
        /// </summary>
        public static Effect EffectAttackDecrease(int nPenalty, AttackBonusType nModifierType = AttackBonusType.Misc)
        {
            return NWN.Core.NWScript.EffectAttackDecrease(nPenalty, (int)nModifierType);
        }

        /// <summary>
        ///  Create a Damage Decrease effect.<br/>
        ///  - nPenalty<br/>
        ///  - nDamageType: DAMAGE_TYPE_*
        /// </summary>
        public static Effect EffectDamageDecrease(int nPenalty, DamageType nDamageType = DamageType.Magical)
        {
            return NWN.Core.NWScript.EffectDamageDecrease(nPenalty, (int)nDamageType);
        }

        /// <summary>
        ///  Create a Damage Immunity Decrease effect.<br/>
        ///  - nDamageType: DAMAGE_TYPE_*<br/>
        ///  - nPercentImmunity
        /// </summary>
        public static Effect EffectDamageImmunityDecrease(DamageType nDamageType, int nPercentImmunity)
        {
            return NWN.Core.NWScript.EffectDamageImmunityDecrease((int)nDamageType, nPercentImmunity);
        }

        /// <summary>
        ///  Create an AC Decrease effect.<br/>
        ///  - nValue<br/>
        ///  - nModifyType: AC_*<br/>
        ///  - nDamageType: DAMAGE_TYPE_*<br/>
        ///    * Default value for nDamageType should only ever be used in this function prototype.
        /// </summary>
        public static Effect EffectACDecrease(int nValue, ACBonusType nModifyType = ACBonusType.Dodge, ACType nDamageType = ACType.VsDamageTypeAll)
        {
            return NWN.Core.NWScript.EffectACDecrease(nValue, (int)nModifyType, (int)nDamageType);
        }

        /// <summary>
        ///  Create a Movement Speed Decrease effect.<br/>
        ///  - nPercentChange - range 0 through 99<br/>
        ///  eg.<br/>
        ///     0 = no change in speed<br/>
        ///    50 = 50% slower<br/>
        ///    99 = almost immobile
        /// </summary>
        public static Effect EffectMovementSpeedDecrease(int nPercentChange)
        {
            return NWN.Core.NWScript.EffectMovementSpeedDecrease(nPercentChange);
        }

        /// <summary>
        ///  Create a Saving Throw Decrease effect.<br/>
        ///  - nSave: SAVING_THROW_* (not SAVING_THROW_TYPE_*)<br/>
        ///           SAVING_THROW_ALL<br/>
        ///           SAVING_THROW_FORT<br/>
        ///           SAVING_THROW_REFLEX<br/>
        ///           SAVING_THROW_WILL<br/>
        ///  - nValue: size of the Saving Throw decrease<br/>
        ///  - nSaveType: SAVING_THROW_TYPE_* (e.g. SAVING_THROW_TYPE_ACID )
        /// </summary>
        public static Effect EffectSavingThrowDecrease(SavingThrowType nSave, int nValue, SavingThrowCategoryType nSaveType = SavingThrowCategoryType.All)
        {
            return NWN.Core.NWScript.EffectSavingThrowDecrease((int)nSave, nValue, (int)nSaveType);
        }

        /// <summary>
        ///  Create a Skill Decrease effect.<br/>
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nSkill is invalid.
        /// </summary>
        public static Effect EffectSkillDecrease(int nSkill, int nValue)
        {
            return NWN.Core.NWScript.EffectSkillDecrease(nSkill, nValue);
        }

        /// <summary>
        ///  Create a Spell Resistance Decrease effect.
        /// </summary>
        public static Effect EffectSpellResistanceDecrease(int nValue)
        {
            return NWN.Core.NWScript.EffectSpellResistanceDecrease(nValue);
        }

        /// <summary>
        ///  Determine whether oTarget is a plot object.
        /// </summary>
        public static bool GetPlotFlag(uint oTarget = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetPlotFlag(oTarget) == 1;
        }

        /// <summary>
        ///  Set oTarget's plot object status.
        /// </summary>
        public static void SetPlotFlag(uint oTarget, int nPlotFlag)
        {
            NWN.Core.NWScript.SetPlotFlag(oTarget, nPlotFlag);
        }

        /// <summary>
        ///  Create an Invisibility effect.<br/>
        ///  - nInvisibilityType: INVISIBILITY_TYPE_*<br/>
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nInvisibilityType<br/>
        ///    is invalid.
        /// </summary>
        public static Effect EffectInvisibility(InvisibilityType nInvisibilityType)
        {
            return NWN.Core.NWScript.EffectInvisibility((int)nInvisibilityType);
        }

        /// <summary>
        ///  Create a Concealment effect.<br/>
        ///  - nPercentage: 1-100 inclusive<br/>
        ///  - nMissChanceType: MISS_CHANCE_TYPE_*<br/>
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nPercentage < 1 or<br/>
        ///    nPercentage > 100.
        /// </summary>
        public static Effect EffectConcealment(int nPercentage, MissChanceType nMissType = MissChanceType.Normal)
        {
            return NWN.Core.NWScript.EffectConcealment(nPercentage, (int)nMissType);
        }

        /// <summary>
        ///  Create a Darkness effect.
        /// </summary>
        public static Effect EffectDarkness()
        {
            return NWN.Core.NWScript.EffectDarkness();
        }
        /// <summary>
        ///  Create a Dispel Magic All effect.<br/>
        ///  If no parameter is specified, USE_CREATURE_LEVEL will be used. This will<br/>
        ///  cause the dispel effect to use the level of the creature that created the<br/>
        ///  effect.
        /// </summary>
        public static Effect EffectDispelMagicAll(int nCasterLevel = GeneralConstants.USE_CREATURE_LEVEL)
        {
            return NWN.Core.NWScript.EffectDispelMagicAll(nCasterLevel);
        }

        /// <summary>
        ///  Create an Ultravision effect.
        /// </summary>
        public static Effect EffectUltravision()
        {
            return NWN.Core.NWScript.EffectUltravision();
        }

        /// <summary>
        ///  Create a Negative Level effect.<br/>
        ///  - nNumLevels: the number of negative levels to apply.<br/>
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nNumLevels > 100.
        /// </summary>
        public static Effect EffectNegativeLevel(int nNumLevels, bool bHPBonus = false)
        {
            return NWN.Core.NWScript.EffectNegativeLevel(nNumLevels, bHPBonus ? 1 : 0);
        }

        /// <summary>
        ///  Create a Polymorph effect.
        /// </summary>
        public static Effect EffectPolymorph(int nPolymorphSelection, bool nLocked = false)
        {
            return NWN.Core.NWScript.EffectPolymorph(nPolymorphSelection, nLocked ? 1 : 0);
        }

        /// <summary>
        ///  Create a Sanctuary effect.<br/>
        ///  - nDifficultyClass: must be a non-zero, positive number<br/>
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nDifficultyClass <= 0.
        /// </summary>
        public static Effect EffectSanctuary(int nDifficultyClass)
        {
            return NWN.Core.NWScript.EffectSanctuary(nDifficultyClass);
        }

        /// <summary>
        ///  Create a True Seeing effect.
        /// </summary>
        public static Effect EffectTrueSeeing()
        {
            return NWN.Core.NWScript.EffectTrueSeeing();
        }

        /// <summary>
        ///  Create a See Invisible effect.
        /// </summary>
        public static Effect EffectSeeInvisible()
        {
            return NWN.Core.NWScript.EffectSeeInvisible();
        }

        /// <summary>
        ///  Create a Time Stop effect.
        /// </summary>
        public static Effect EffectTimeStop()
        {
            return NWN.Core.NWScript.EffectTimeStop();
        }

        /// <summary>
        ///  Create a Blindness effect.
        /// </summary>
        public static Effect EffectBlindness()
        {
            return NWN.Core.NWScript.EffectBlindness();
        }

        /// <summary>
        ///  Determine whether oSource has a friendly reaction towards oTarget, depending<br/>
        ///  on the reputation, PVP setting and (if both oSource and oTarget are PCs),<br/>
        ///  oSource's Like/Dislike setting for oTarget.<br/>
        ///  Note: If you just want to know how two objects feel about each other in terms<br/>
        ///  of faction and personal reputation, use GetIsFriend() instead.<br/>
        ///  * Returns true if oSource has a friendly reaction towards oTarget
        /// </summary>
        public static bool GetIsReactionTypeFriendly(uint oTarget, uint oSource = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetIsReactionTypeFriendly(oTarget, oSource) == 1;
        }

        /// <summary>
        ///  Determine whether oSource has a neutral reaction towards oTarget, depending<br/>
        ///  on the reputation, PVP setting and (if both oSource and oTarget are PCs),<br/>
        ///  oSource's Like/Dislike setting for oTarget.<br/>
        ///  Note: If you just want to know how two objects feel about each other in terms<br/>
        ///  of faction and personal reputation, use GetIsNeutral() instead.<br/>
        ///  * Returns true if oSource has a neutral reaction towards oTarget
        /// </summary>
        public static bool GetIsReactionTypeNeutral(uint oTarget, uint oSource = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetIsReactionTypeNeutral(oTarget, oSource) == 1;
        }

        /// <summary>
        ///  Determine whether oSource has a Hostile reaction towards oTarget, depending<br/>
        ///  on the reputation, PVP setting and (if both oSource and oTarget are PCs),<br/>
        ///  oSource's Like/Dislike setting for oTarget.<br/>
        ///  Note: If you just want to know how two objects feel about each other in terms<br/>
        ///  of faction and personal reputation, use GetIsEnemy() instead.<br/>
        ///  * Returns true if oSource has a hostile reaction towards oTarget
        /// </summary>
        public static bool GetIsReactionTypeHostile(uint oTarget, uint oSource = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetIsReactionTypeHostile(oTarget, oSource) == 1;
        }

        /// <summary>
        ///  Create a Spell Level Absorption effect.<br/>
        ///  - nMaxSpellLevelAbsorbed: maximum spell level that will be absorbed by the<br/>
        ///    effect<br/>
        ///  - nTotalSpellLevelsAbsorbed: maximum number of spell levels that will be<br/>
        ///    absorbed by the effect<br/>
        ///  - nSpellSchool: SPELL_SCHOOL_*<br/>
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if:<br/>
        ///    nMaxSpellLevelAbsorbed is not between -1 and 9 inclusive, or nSpellSchool<br/>
        ///    is invalid.
        /// </summary>
        public static Effect EffectSpellLevelAbsorption(int nMaxSpellLevelAbsorbed, int nTotalSpellLevelsAbsorbed = 0, SpellSchoolType nSpellSchool = SpellSchoolType.General)
        {
            return NWN.Core.NWScript.EffectSpellLevelAbsorption(nMaxSpellLevelAbsorbed, nTotalSpellLevelsAbsorbed, (int)nSpellSchool);
        }

        /// <summary>
        ///  Create a Dispel Magic Best effect.<br/>
        ///  If no parameter is specified, USE_CREATURE_LEVEL will be used. This will<br/>
        ///  cause the dispel effect to use the level of the creature that created the<br/>
        ///  effect.
        /// </summary>
        public static Effect EffectDispelMagicBest(int nCasterLevel = GeneralConstants.USE_CREATURE_LEVEL)
        {
            return NWN.Core.NWScript.EffectDispelMagicBest(nCasterLevel);
        }

        /// <summary>
        ///  Try to send oTarget to a new server defined by sIPaddress.<br/>
        ///  - oTarget<br/>
        ///  - sIPaddress: this can be numerical "192.168.0.84" or alphanumeric<br/>
        ///    "www.bioware.com". It can also contain a port "192.168.0.84:5121" or<br/>
        ///    "www.bioware.com:5121"; if the port is not specified, it will default to<br/>
        ///    5121.<br/>
        ///  - sPassword: login password for the destination server<br/>
        ///  - sWaypointTag: if this is set, after portalling the character will be moved<br/>
        ///    to this waypoint if it exists<br/>
        ///  - bSeamless: if this is set, the client wil not be prompted with the<br/>
        ///    information window telling them about the server, and they will not be<br/>
        ///    allowed to save a copy of their character if they are using a local vault<br/>
        ///    character.
        /// </summary>
        public static void ActivatePortal(uint oTarget, string sIPaddress = "", string sPassword = "", string sWaypointTag = "", bool bSeemless = false)
        {
            NWN.Core.NWScript.ActivatePortal(oTarget, sIPaddress, sPassword, sWaypointTag, bSeemless ? 1 : 0);
        }

        /// <summary>
        ///  Get the number of stacked items that oItem comprises.
        /// </summary>
        public static int GetNumStackedItems(uint oItem)
        {
            return NWN.Core.NWScript.GetNumStackedItems(oItem);
        }

        /// <summary>
        ///  Use this on an NPC to cause all creatures within a 10-metre radius to stop<br/>
        ///  what they are doing and sets the NPC's enemies within this range to be<br/>
        ///  neutral towards the NPC for roughly 3 minutes. If this command is run on a PC<br/>
        ///  or an object that is not a creature, nothing will happen.
        /// </summary>
        public static void SurrenderToEnemies()
        {
            NWN.Core.NWScript.SurrenderToEnemies();
        }

        /// <summary>
        ///  Create a Miss Chance effect.<br/>
        ///  - nPercentage: 1-100 inclusive<br/>
        ///  - nMissChanceType: MISS_CHANCE_TYPE_*<br/>
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nPercentage < 1 or<br/>
        ///    nPercentage > 100.
        /// </summary>
        public static Effect EffectMissChance(int nPercentage, MissChanceType nMissChanceType = MissChanceType.Normal)
        {
            return NWN.Core.NWScript.EffectMissChance(nPercentage, (int)nMissChanceType);
        }

        /// <summary>
        ///  Get the number of Hitdice worth of Turn Resistance that oUndead may have.<br/>
        ///  This will only work on undead creatures.
        /// </summary>
        public static int GetTurnResistanceHD(uint oUndead = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetTurnResistanceHD(oUndead);
        }

        /// <summary>
        ///  Get the size (CREATURE_SIZE_*) of oCreature.
        /// </summary>
        public static CreatureSizeType GetCreatureSize(uint oCreature)
        {
            return (CreatureSizeType)NWN.Core.NWScript.GetCreatureSize(oCreature);
        }


        /// <summary>
        ///  Create a Disappear/Appear effect.<br/>
        ///  The object will "fly away" for the duration of the effect and will reappear<br/>
        ///  at lLocation.<br/>
        ///  - nAnimation determines which appear and disappear animations to use. Most creatures<br/>
        ///  only have animation 1, although a few have 2 (like beholders)
        /// </summary>
        public static Effect EffectDisappearAppear(Location lLocation, AnimationType nAnimation = AnimationType.LoopingPause2)
        {
            return NWN.Core.NWScript.EffectDisappearAppear(lLocation, (int)nAnimation);
        }

        /// <summary>
        ///  Create a Disappear effect to make the object "fly away" and then destroy<br/>
        ///  itself.<br/>
        ///  - nAnimation determines which appear and disappear animations to use. Most creatures<br/>
        ///  only have animation 1, although a few have 2 (like beholders)
        /// </summary>
        public static Effect EffectDisappear(AnimationType nAnimation = AnimationType.LoopingPause2)
        {
            return NWN.Core.NWScript.EffectDisappear((int)nAnimation);
        }

        /// <summary>
        ///  Create an Appear effect to make the object "fly in".<br/>
        ///  - nAnimation determines which appear and disappear animations to use. Most creatures<br/>
        ///  only have animation 1, although a few have 2 (like beholders)
        /// </summary>
        public static Effect EffectAppear(AnimationType nAnimation = AnimationType.LoopingPause2)
        {
            return NWN.Core.NWScript.EffectAppear((int)nAnimation);
        }

        /// <summary>
        ///  The action subject will unlock oTarget, which can be a door or a placeable<br/>
        ///  object.
        /// </summary>
        public static void ActionUnlockObject(uint oTarget)
        {
            NWN.Core.NWScript.ActionUnlockObject(oTarget);
        }

        /// <summary>
        ///  The action subject will lock oTarget, which can be a door or a placeable<br/>
        ///  object.
        /// </summary>
        public static void ActionLockObject(uint oTarget)
        {
            NWN.Core.NWScript.ActionLockObject(oTarget);
        }

        /// <summary>
        ///  Create a Modify Attacks effect to add attacks.<br/>
        ///  - nAttacks: maximum is 5, even with the effect stacked<br/>
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT if nAttacks > 5.
        /// </summary>
        public static Effect EffectModifyAttacks(int nAttacks)
        {
            return NWN.Core.NWScript.EffectModifyAttacks(nAttacks);
        }

        /// <summary>
        ///  Get the last trap detected by oTarget.<br/>
        ///  * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetLastTrapDetected(uint oTarget = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetLastTrapDetected(oTarget);
        }

        /// <summary>
        ///  Create a Damage Shield effect which does (nDamageAmount + nRandomAmount)<br/>
        ///  damage to any melee attacker on a successful attack of damage type nDamageType.<br/>
        ///  - nDamageAmount: an integer value<br/>
        ///  - nRandomAmount: DAMAGE_BONUS_*<br/>
        ///  - nDamageType: DAMAGE_TYPE_*<br/>
        ///  NOTE! You *must* use the DAMAGE_BONUS_* constants! Using other values may<br/>
        ///        result in odd behaviour.
        /// </summary>
        public static Effect EffectDamageShield(int nDamageAmount, int nRandomAmount, DamageType nDamageType)
        {
            return NWN.Core.NWScript.EffectDamageShield(nDamageAmount, nRandomAmount, (int)nDamageType);
        }

        /// <summary>
        ///  Get the trap nearest to oTarget.<br/>
        ///  Note : "trap objects" are actually any trigger, placeable or door that is<br/>
        ///  trapped in oTarget's area.<br/>
        ///  - oTarget<br/>
        ///  - nTrapDetected: if this is true, the trap returned has to have been detected<br/>
        ///    by oTarget.
        /// </summary>
        public static uint GetNearestTrapToObject(uint oTarget = OBJECT_INVALID, bool nTrapDetected = true)
        {
            return NWN.Core.NWScript.GetNearestTrapToObject(oTarget, nTrapDetected ? 1 : 0);
        }

        /// <summary>
        ///  Get the name of oCreature's deity.<br/>
        ///  * Returns "" if oCreature is invalid (or if the deity name is blank for<br/>
        ///    oCreature).
        /// </summary>
        public static string GetDeity(uint oCreature)
        {
            return NWN.Core.NWScript.GetDeity(oCreature);
        }

        /// <summary>
        ///  Get the name of oCreature's sub race.<br/>
        ///  * Returns "" if oCreature is invalid (or if sub race is blank for oCreature).
        /// </summary>
        public static string GetSubRace(uint oTarget)
        {
            return NWN.Core.NWScript.GetSubRace(oTarget);
        }

        /// <summary>
        ///  Get oTarget's base fortitude saving throw value (this will only work for<br/>
        ///  creatures, doors, and placeables).<br/>
        ///  * Returns 0 if oTarget is invalid.
        /// </summary>
        public static int GetFortitudeSavingThrow(uint oTarget)
        {
            return NWN.Core.NWScript.GetFortitudeSavingThrow(oTarget);
        }

        /// <summary>
        ///  Get oTarget's base will saving throw value (this will only work for creatures,<br/>
        ///  doors, and placeables).<br/>
        ///  * Returns 0 if oTarget is invalid.
        /// </summary>
        public static int GetWillSavingThrow(uint oTarget)
        {
            return NWN.Core.NWScript.GetWillSavingThrow(oTarget);
        }

        /// <summary>
        ///  Get oTarget's base reflex saving throw value (this will only work for<br/>
        ///  creatures, doors, and placeables).<br/>
        ///  * Returns 0 if oTarget is invalid.
        /// </summary>
        public static int GetReflexSavingThrow(uint oTarget)
        {
            return NWN.Core.NWScript.GetReflexSavingThrow(oTarget);
        }

        /// <summary>
        ///  Get oCreature's challenge rating.<br/>
        ///  * Returns 0.0 if oCreature is invalid.
        /// </summary>
        public static float GetChallengeRating(uint oCreature)
        {
            return NWN.Core.NWScript.GetChallengeRating(oCreature);
        }

        /// <summary>
        ///  Get oCreature's age.<br/>
        ///  * Returns 0 if oCreature is invalid.
        /// </summary>
        public static int GetAge(uint oCreature)
        {
            return NWN.Core.NWScript.GetAge(oCreature);
        }

        /// <summary>
        ///  Get oCreature's movement rate.<br/>
        ///  * Returns 0 if oCreature is invalid.
        /// </summary>
        public static MovementRateType GetMovementRate(uint oCreature)
        {
            return (MovementRateType)NWN.Core.NWScript.GetMovementRate(oCreature);
        }

        /// <summary>
        ///  Get oCreature's familiar creature type (FAMILIAR_CREATURE_TYPE_*).<br/>
        ///  * Returns FAMILIAR_CREATURE_TYPE_NONE if oCreature is invalid or does<br/>
        ///    not currently have a familiar.
        /// </summary>
        public static FamiliarCreatureType GetFamiliarCreatureType(uint oCreature)
        {
            return (FamiliarCreatureType)NWN.Core.NWScript.GetFamiliarCreatureType(oCreature);
        }

        /// <summary>
        ///  Get oCreature's animal companion creature type<br/>
        ///  (ANIMAL_COMPANION_CREATURE_TYPE_*).<br/>
        ///  * Returns ANIMAL_COMPANION_CREATURE_TYPE_NONE if oCreature is invalid or does<br/>
        ///    not currently have an animal companion.
        /// </summary>
        public static AnimalCompanionCreatureType GetAnimalCompanionCreatureType(uint oCreature)
        {
            return (AnimalCompanionCreatureType)NWN.Core.NWScript.GetAnimalCompanionCreatureType(oCreature);
        }

        /// <summary>
        ///  Get oCreature's familiar's name.<br/>
        ///  * Returns "" if oCreature is invalid, does not currently<br/>
        ///  have a familiar or if the familiar's name is blank.
        /// </summary>
        public static string GetFamiliarName(uint oCreature)
        {
            return NWN.Core.NWScript.GetFamiliarName(oCreature);
        }

        /// <summary>
        ///  Get oCreature's animal companion's name.<br/>
        ///  * Returns "" if oCreature is invalid, does not currently<br/>
        ///  have an animal companion or if the animal companion's name is blank.
        /// </summary>
        public static string GetAnimalCompanionName(uint oTarget)
        {
            return NWN.Core.NWScript.GetAnimalCompanionName(oTarget);
        }
        /// <summary>
        ///  The action subject will fake casting a spell at oTarget; the conjure and cast<br/>
        ///  animations and visuals will occur, nothing else.<br/>
        ///  - nSpell<br/>
        ///  - oTarget<br/>
        ///  - nProjectilePathType: PROJECTILE_PATH_TYPE_*
        /// </summary>
        public static void ActionCastFakeSpellAtObject(SpellType nSpell, uint oTarget, ProjectilePathType nProjectilePathType = ProjectilePathType.Default)
        {
            NWN.Core.NWScript.ActionCastFakeSpellAtObject((int)nSpell, oTarget, (int)nProjectilePathType);
        }

        /// <summary>
        ///  The action subject will fake casting a spell at lLocation; the conjure and<br/>
        ///  cast animations and visuals will occur, nothing else.<br/>
        ///  - nSpell<br/>
        ///  - lTarget<br/>
        ///  - nProjectilePathType: PROJECTILE_PATH_TYPE_*
        /// </summary>
        public static void ActionCastFakeSpellAtLocation(int nSpell, Location lTarget, ProjectilePathType nProjectilePathType = ProjectilePathType.Default)
        {
            NWN.Core.NWScript.ActionCastFakeSpellAtLocation(nSpell, lTarget, (int)nProjectilePathType);
        }

        /// <summary>
        ///  Removes oAssociate from the service of oMaster, returning them to their<br/>
        ///  original faction.
        /// </summary>
        public static void RemoveSummonedAssociate(uint oMaster, uint oAssociate = OBJECT_INVALID)
        {
            NWN.Core.NWScript.RemoveSummonedAssociate(oMaster, oAssociate);
        }

        /// <summary>
        ///  Set the camera mode for oPlayer.<br/>
        ///  - oPlayer<br/>
        ///  - nCameraMode: CAMERA_MODE_*<br/>
        ///  * If oPlayer is not player-controlled or nCameraMode is invalid, nothing<br/>
        ///    happens.
        /// </summary>
        public static void SetCameraMode(uint oPlayer, int nCameraMode)
        {
            NWN.Core.NWScript.SetCameraMode(oPlayer, nCameraMode);
        }

        /// <summary>
        ///  * Returns true if oCreature is resting.
        /// </summary>
        public static bool GetIsResting(uint oCreature = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetIsResting(oCreature) == 1;
        }


        /// <summary>
        ///  Get the last PC that has rested in the module.
        /// </summary>
        public static uint GetLastPCRested()
        {
            return NWN.Core.NWScript.GetLastPCRested();
        }

        /// <summary>
        ///  Set the weather for oTarget.<br/>
        ///  - oTarget: if this is GetModule(), all outdoor areas will be modified by the<br/>
        ///    weather constant. If it is an area, oTarget will play the weather only if<br/>
        ///    it is an outdoor area.<br/>
        ///  - nWeather: WEATHER_*<br/>
        ///    -&gt; WEATHER_USER_AREA_SETTINGS will set the area back to random weather.<br/>
        ///    -&gt; WEATHER_CLEAR, WEATHER_RAIN, WEATHER_SNOW will make the weather go to<br/>
        ///       the appropriate precipitation *without stopping*.
        /// </summary>
        public static void SetWeather(uint oTarget, int nWeather)
        {
            NWN.Core.NWScript.SetWeather(oTarget, nWeather);
        }

        /// <summary>
        ///  Determine the type (REST_EVENTTYPE_REST_*) of the last rest event (as<br/>
        ///  returned from the OnPCRested module event).
        /// </summary>
        public static RestEventType GetLastRestEventType()
        {
            return (RestEventType)NWN.Core.NWScript.GetLastRestEventType();
        }

        /// <summary>
        ///  Shut down the currently loaded module and start a new one (moving all<br/>
        ///  currently-connected players to the starting point.
        /// </summary>
        public static void StartNewModule(string sModuleName)
        {
            NWN.Core.NWScript.StartNewModule(sModuleName);
        }

        /// <summary>
        ///  Create a Swarm effect.<br/>
        ///  - nLooping: If this is true, for the duration of the effect when one creature<br/>
        ///    created by this effect dies, the next one in the list will be created.  If<br/>
        ///    the last creature in the list dies, we loop back to the beginning and<br/>
        ///    sCreatureTemplate1 will be created, and so on...<br/>
        ///  - sCreatureTemplate1<br/>
        ///  - sCreatureTemplate2<br/>
        ///  - sCreatureTemplate3<br/>
        ///  - sCreatureTemplate4
        /// </summary>
        public static Effect EffectSwarm(int nLooping, string sCreatureTemplate1, string sCreatureTemplate2 = "", string sCreatureTemplate3 = "", string sCreatureTemplate4 = "")
        {
            return NWN.Core.NWScript.EffectSwarm(nLooping, sCreatureTemplate1, sCreatureTemplate2, sCreatureTemplate3, sCreatureTemplate4);
        }

        /// <summary>
        ///  * Returns true if oItem is a ranged weapon.
        /// </summary>
        public static bool GetWeaponRanged(uint oItem)
        {
            return NWN.Core.NWScript.GetWeaponRanged(oItem) == 1;
        }

        /// <summary>
        ///  Only if we are in a single player game, AutoSave the game.
        /// </summary>
        public static void DoSinglePlayerAutoSave()
        {
            NWN.Core.NWScript.DoSinglePlayerAutoSave();
        }

        /// <summary>
        ///  Get the game difficulty (GAME_DIFFICULTY_*).
        /// </summary>
        public static GameDifficultyType GetGameDifficulty()
        {
            return (GameDifficultyType)NWN.Core.NWScript.GetGameDifficulty();
        }

        /// <summary>
        ///  Set the main light color on the tile at lTileLocation.<br/>
        ///  - lTileLocation: the vector part of this is the tile grid (x,y) coordinate of<br/>
        ///    the tile.<br/>
        ///  - nMainLight1Color: TILE_MAIN_LIGHT_COLOR_*<br/>
        ///  - nMainLight2Color: TILE_MAIN_LIGHT_COLOR_*
        /// </summary>
        public static void SetTileMainLightColor(Location lTileLocation, int nMainLight1Color, int nMainLight2Color)
        {
            NWN.Core.NWScript.SetTileMainLightColor(lTileLocation, nMainLight1Color, nMainLight2Color);
        }

        /// <summary>
        ///  Set the source light color on the tile at lTileLocation.<br/>
        ///  - lTileLocation: the vector part of this is the tile grid (x,y) coordinate of<br/>
        ///    the tile.<br/>
        ///  - nSourceLight1Color: TILE_SOURCE_LIGHT_COLOR_*<br/>
        ///  - nSourceLight2Color: TILE_SOURCE_LIGHT_COLOR_*
        /// </summary>
        public static void SetTileSourceLightColor(Location lTileLocation, int nSourceLight1Color, int nSourceLight2Color)
        {
            NWN.Core.NWScript.SetTileSourceLightColor(lTileLocation, nSourceLight1Color, nSourceLight2Color);
        }

        /// <summary>
        ///  All clients in oArea will recompute the static lighting.<br/>
        ///  This can be used to update the lighting after changing any tile lights or if<br/>
        ///  placeables with lights have been added/deleted.
        /// </summary>
        public static void RecomputeStaticLighting(uint oArea)
        {
            NWN.Core.NWScript.RecomputeStaticLighting(oArea);
        }

        /// <summary>
        ///  Get the color (TILE_MAIN_LIGHT_COLOR_*) for the main light 1 of the tile at<br/>
        ///  lTile.<br/>
        ///  - lTile: the vector part of this is the tile grid (x,y) coordinate of the tile.
        /// </summary>
        public static TileMainLightColorType GetTileMainLight1Color(Location lTile)
        {
            return (TileMainLightColorType)NWN.Core.NWScript.GetTileMainLight1Color(lTile);
        }

        /// <summary>
        ///  Get the color (TILE_MAIN_LIGHT_COLOR_*) for the main light 2 of the tile at<br/>
        ///  lTile.<br/>
        ///  - lTile: the vector part of this is the tile grid (x,y) coordinate of the<br/>
        ///    tile.
        /// </summary>
        public static TileMainLightColorType GetTileMainLight2Color(Location lTile)
        {
            return (TileMainLightColorType)NWN.Core.NWScript.GetTileMainLight2Color(lTile);
        }

        /// <summary>
        ///  Get the color (TILE_SOURCE_LIGHT_COLOR_*) for the source light 1 of the tile<br/>
        ///  at lTile.<br/>
        ///  - lTile: the vector part of this is the tile grid (x,y) coordinate of the<br/>
        ///    tile.
        /// </summary>
        public static TileSourceLightColorType GetTileSourceLight1Color(Location lTile)
        {
            return (TileSourceLightColorType)NWN.Core.NWScript.GetTileSourceLight1Color(lTile);
        }

        /// <summary>
        ///  Get the color (TILE_SOURCE_LIGHT_COLOR_*) for the source light 2 of the tile<br/>
        ///  at lTile.<br/>
        ///  - lTile: the vector part of this is the tile grid (x,y) coordinate of the<br/>
        ///    tile.
        /// </summary>
        public static TileSourceLightColorType GetTileSourceLight2Color(Location lTile)
        {
            return (TileSourceLightColorType)NWN.Core.NWScript.GetTileSourceLight2Color(lTile);
        }

        /// <summary>
        ///  Make the corresponding panel button on the player's client start or stop<br/>
        ///  flashing.<br/>
        ///  - oPlayer<br/>
        ///  - nButton: PANEL_BUTTON_*<br/>
        ///  - nEnableFlash: if this is true nButton will start flashing.  It if is false,<br/>
        ///    nButton will stop flashing.
        /// </summary>
        public static void SetPanelButtonFlash(uint oPlayer, int nButton, int nEnableFlash)
        {
            NWN.Core.NWScript.SetPanelButtonFlash(oPlayer, nButton, nEnableFlash);
        }

        /// <summary>
        ///  Get the current action (ACTION_*) that oObject is executing.
        /// </summary>
        public static ActionType GetCurrentAction(uint oObject = OBJECT_INVALID)
        {
            return (ActionType)NWN.Core.NWScript.GetCurrentAction(oObject);
        }

        /// <summary>
        ///  Set how nStandardFaction feels about oCreature.<br/>
        ///  - nStandardFaction: STANDARD_FACTION_*<br/>
        ///  - nNewReputation: 0-100 (inclusive)<br/>
        ///  - oCreature
        /// </summary>
        public static void SetStandardFactionReputation(StandardFactionType nStandardFaction, int nNewReputation, uint oCreature = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetStandardFactionReputation((int)nStandardFaction, nNewReputation, oCreature);
        }

        /// <summary>
        ///  Find out how nStandardFaction feels about oCreature.<br/>
        ///  - nStandardFaction: STANDARD_FACTION_*<br/>
        ///  - oCreature<br/>
        ///  Returns -1 on an error.<br/>
        ///  Returns 0-100 based on the standing of oCreature within the faction nStandardFaction.<br/>
        ///  0-10   :  Hostile.<br/>
        ///  11-89  :  Neutral.<br/>
        ///  90-100 :  Friendly.
        /// </summary>
        public static int GetStandardFactionReputation(StandardFactionType nStandardFaction, uint oCreature = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetStandardFactionReputation((int)nStandardFaction, oCreature);
        }

        /// <summary>
        ///  Display floaty text above the specified creature.<br/>
        ///  The text will also appear in the chat buffer of each player that receives the<br/>
        ///  floaty text.<br/>
        ///  - nStrRefToDisplay: String ref (therefore text is translated)<br/>
        ///  - oCreatureToFloatAbove<br/>
        ///  - bBroadcastToFaction: If this is true then only creatures in the same faction<br/>
        ///    as oCreatureToFloatAbove<br/>
        ///    will see the floaty text, and only if they are within range (30 metres).<br/>
        ///  - bChatWindow:  If true, the string reference will be displayed in oCreatureToFloatAbove's chat window
        /// </summary>
        public static void FloatingTextStrRefOnCreature(int nStrRefToDisplay, uint oCreatureToFloatAbove, bool bBroadcastToFaction = true, bool bChatWindow = true)
        {
            NWN.Core.NWScript.FloatingTextStrRefOnCreature(nStrRefToDisplay, oCreatureToFloatAbove, bBroadcastToFaction ? 1 : 0, bChatWindow ? 1 : 0);
        }

        /// <summary>
        ///  Display floaty text above the specified creature.<br/>
        ///  The text will also appear in the chat buffer of each player that receives the<br/>
        ///  floaty text.<br/>
        ///  - sStringToDisplay: String<br/>
        ///  - oCreatureToFloatAbove<br/>
        ///  - bBroadcastToFaction: If this is true then only creatures in the same faction<br/>
        ///    as oCreatureToFloatAbove<br/>
        ///    will see the floaty text, and only if they are within range (30 metres).<br/>
        ///  - bChatWindow:  If true, sStringToDisplay will be displayed in oCreatureToFloatAbove's chat window.
        /// </summary>
        public static void FloatingTextStringOnCreature(string sStringToDisplay, uint oCreatureToFloatAbove, bool bBroadcastToFaction = true, bool bChatWindow = true)
        {
            NWN.Core.NWScript.FloatingTextStringOnCreature(sStringToDisplay, oCreatureToFloatAbove, bBroadcastToFaction ? 1 : 0, bChatWindow ? 1 : 0);
        }

        /// <summary>
        ///  - oTrapObject: a placeable, door or trigger<br/>
        ///  * Returns true if oTrapObject is disarmable.
        /// </summary>
        public static bool GetTrapDisarmable(uint oTrapObject)
        {
            return NWN.Core.NWScript.GetTrapDisarmable(oTrapObject) == 1;
        }

        /// <summary>
        ///  - oTrapObject: a placeable, door or trigger<br/>
        ///  * Returns true if oTrapObject is detectable.
        /// </summary>
        public static bool GetTrapDetectable(uint oTrapObject)
        {
            return NWN.Core.NWScript.GetTrapDetectable(oTrapObject) == 1;
        }

        /// <summary>
        ///  - oTrapObject: a placeable, door or trigger<br/>
        ///  - oCreature<br/>
        ///  * Returns true if oCreature has detected oTrapObject
        /// </summary>
        public static bool GetTrapDetectedBy(uint oTrapObject, uint oCreature)
        {
            return NWN.Core.NWScript.GetTrapDetectedBy(oTrapObject, oCreature) == 1;
        }

        /// <summary>
        ///  - oTrapObject: a placeable, door or trigger<br/>
        ///  * Returns true if oTrapObject has been flagged as visible to all creatures.
        /// </summary>
        public static bool GetTrapFlagged(uint oTrapObject)
        {
            return NWN.Core.NWScript.GetTrapFlagged(oTrapObject) == 1;
        }

        /// <summary>
        ///  Get the trap base type (TRAP_BASE_TYPE_*) of oTrapObject.<br/>
        ///  - oTrapObject: a placeable, door or trigger
        /// </summary>
        public static TrapBaseType GetTrapBaseType(uint oTrapObject)
        {
            return (TrapBaseType)NWN.Core.NWScript.GetTrapBaseType(oTrapObject);
        }

        /// <summary>
        ///  - oTrapObject: a placeable, door or trigger<br/>
        ///  * Returns true if oTrapObject is one-shot (i.e. it does not reset itself<br/>
        ///    after firing.
        /// </summary>
        public static bool GetTrapOneShot(uint oTrapObject)
        {
            return NWN.Core.NWScript.GetTrapOneShot(oTrapObject) == 1;
        }

        /// <summary>
        ///  Get the creator of oTrapObject, the creature that set the trap.<br/>
        ///  - oTrapObject: a placeable, door or trigger<br/>
        ///  * Returns OBJECT_INVALID if oTrapObject was created in the toolset.
        /// </summary>
        public static uint GetTrapCreator(uint oTrapObject)
        {
            return NWN.Core.NWScript.GetTrapCreator(oTrapObject);
        }

        /// <summary>
        ///  Get the tag of the key that will disarm oTrapObject.<br/>
        ///  - oTrapObject: a placeable, door or trigger
        /// </summary>
        public static string GetTrapKeyTag(uint oTrapObject)
        {
            return NWN.Core.NWScript.GetTrapKeyTag(oTrapObject);
        }

        /// <summary>
        ///  Get the DC for disarming oTrapObject.<br/>
        ///  - oTrapObject: a placeable, door or trigger
        /// </summary>
        public static int GetTrapDisarmDC(uint oTrapObject)
        {
            return NWN.Core.NWScript.GetTrapDisarmDC(oTrapObject);
        }

        /// <summary>
        ///  Get the DC for detecting oTrapObject.<br/>
        ///  - oTrapObject: a placeable, door or trigger
        /// </summary>
        public static int GetTrapDetectDC(uint oTrapObject)
        {
            return NWN.Core.NWScript.GetTrapDetectDC(oTrapObject);
        }

        /// <summary>
        ///  * Returns true if a specific key is required to open the lock on oObject.
        /// </summary>
        public static bool GetLockKeyRequired(uint oObject)
        {
            return NWN.Core.NWScript.GetLockKeyRequired(oObject) == 1;
        }


        /// <summary>
        ///  Get the tag of the key that will open the lock on oObject.
        /// </summary>
        public static string GetLockKeyTag(uint oObject)
        {
            return NWN.Core.NWScript.GetLockKeyTag(oObject);
        }

        /// <summary>
        ///  * Returns true if the lock on oObject is lockable.
        /// </summary>
        public static bool GetLockLockable(uint oObject)
        {
            return NWN.Core.NWScript.GetLockLockable(oObject) == 1;
        }

        /// <summary>
        ///  Get the DC for unlocking oObject.
        /// </summary>
        public static int GetLockUnlockDC(uint oObject)
        {
            return NWN.Core.NWScript.GetLockUnlockDC(oObject);
        }

        /// <summary>
        ///  Get the DC for locking oObject.
        /// </summary>
        public static int GetLockLockDC(uint oObject)
        {
            return NWN.Core.NWScript.GetLockLockDC(oObject);
        }

        /// <summary>
        ///  Get the last PC that levelled up.
        /// </summary>
        public static uint GetPCLevellingUp()
        {
            return NWN.Core.NWScript.GetPCLevellingUp();
        }

        /// <summary>
        ///  - nFeat: FEAT_*<br/>
        ///  - oObject<br/>
        ///  * Returns true if oObject has effects on it originating from nFeat.
        /// </summary>
        public static bool GetHasFeatEffect(FeatType nFeat, uint oObject = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetHasFeatEffect((int)nFeat, oObject) == 1;
        }

        /// <summary>
        ///  Set the status of the illumination for oPlaceable.<br/>
        ///  - oPlaceable<br/>
        ///  - bIlluminate: if this is true, oPlaceable's illumination will be turned on.<br/>
        ///    If this is false, oPlaceable's illumination will be turned off.<br/>
        ///  Note: You must call RecomputeStaticLighting() after calling this function in<br/>
        ///  order for the changes to occur visually for the players.<br/>
        ///  SetPlaceableIllumination() buffers the illumination changes, which are then<br/>
        ///  sent out to the players once RecomputeStaticLighting() is called.  As such,<br/>
        ///  it is best to call SetPlaceableIllumination() for all the placeables you wish<br/>
        ///  to set the illumination on, and then call RecomputeStaticLighting() once after<br/>
        ///  all the placeable illumination has been set.<br/>
        ///  * If oPlaceable is not a placeable object, or oPlaceable is a placeable that<br/>
        ///    doesn't have a light, nothing will happen.
        /// </summary>
        public static void SetPlaceableIllumination(uint oPlaceable = OBJECT_INVALID, bool bIlluminate = true)
        {
            NWN.Core.NWScript.SetPlaceableIllumination(oPlaceable, bIlluminate ? 1 : 0);
        }

        /// <summary>
        ///  * Returns true if the illumination for oPlaceable is on
        /// </summary>
        public static bool GetPlaceableIllumination(uint oPlaceable = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetPlaceableIllumination(oPlaceable) == 1;
        }

        /// <summary>
        ///  - oPlaceable<br/>
        ///  - nPlaceableAction: PLACEABLE_ACTION_*<br/>
        ///  * Returns true if nPlacebleAction is valid for oPlaceable.
        /// </summary>
        public static bool GetIsPlaceableObjectActionPossible(uint oPlaceable, PlaceableActionType nPlaceableAction)
        {
            return NWN.Core.NWScript.GetIsPlaceableObjectActionPossible(oPlaceable, (int)nPlaceableAction) == 1;
        }

        /// <summary>
        ///  The caller performs nPlaceableAction on oPlaceable.<br/>
        ///  - oPlaceable<br/>
        ///  - nPlaceableAction: PLACEABLE_ACTION_*
        /// </summary>
        public static void DoPlaceableObjectAction(uint oPlaceable, int nPlaceableAction)
        {
            NWN.Core.NWScript.DoPlaceableObjectAction(oPlaceable, nPlaceableAction);
        }

        /// <summary>
        ///  Get the first PC in the player list.<br/>
        ///  This resets the position in the player list for GetNextPC().
        /// </summary>
        public static uint GetFirstPC()
        {
            return NWN.Core.NWScript.GetFirstPC();
        }

        /// <summary>
        ///  Get the next PC in the player list.<br/>
        ///  This picks up where the last GetFirstPC() or GetNextPC() left off.
        /// </summary>
        public static uint GetNextPC()
        {
            return NWN.Core.NWScript.GetNextPC();
        }

        /// <summary>
        ///  Set whether or not the creature oDetector has detected the trapped object oTrap.<br/>
        ///  - oTrap: A trapped trigger, placeable or door object.<br/>
        ///  - oDetector: This is the creature that the detected status of the trap is being adjusted for.<br/>
        ///  - bDetected: A Boolean that sets whether the trapped object has been detected or not.
        /// </summary>
        public static int SetTrapDetectedBy(uint oTrap, uint oDetector, bool bDetected = true)
        {
            return NWN.Core.NWScript.SetTrapDetectedBy(oTrap, oDetector, bDetected ? 1 : 0);
        }
        /// <summary>
        ///  Note: Only placeables, doors and triggers can be trapped.<br/>
        ///  * Returns true if oObject is trapped.
        /// </summary>
        public static bool GetIsTrapped(uint oObject)
        {
            return NWN.Core.NWScript.GetIsTrapped(oObject) == 1;
        }


        /// <summary>
        ///  Create a Turn Resistance Decrease effect.<br/>
        ///  - nHitDice: a positive number representing the number of hit dice for the<br/>
        /// /  decrease
        /// </summary>
        public static Effect EffectTurnResistanceDecrease(int nHitDice)
        {
            return NWN.Core.NWScript.EffectTurnResistanceDecrease(nHitDice);
        }

        /// <summary>
        ///  Create a Turn Resistance Increase effect.<br/>
        ///  - nHitDice: a positive number representing the number of hit dice for the<br/>
        ///    increase
        /// </summary>
        public static Effect EffectTurnResistanceIncrease(int nHitDice)
        {
            return NWN.Core.NWScript.EffectTurnResistanceIncrease(nHitDice);
        }

        /// <summary>
        ///  Spawn in the Death GUI.<br/>
        ///  The default (as defined by BioWare) can be spawned in by PopUpGUIPanel, but<br/>
        ///  if you want to turn off the "Respawn" or "Wait for Help" buttons, this is the<br/>
        ///  function to use.<br/>
        ///  - oPC<br/>
        ///  - bRespawnButtonEnabled: if this is true, the "Respawn" button will be enabled<br/>
        ///    on the Death GUI.<br/>
        ///  - bWaitForHelpButtonEnabled: if this is true, the "Wait For Help" button will<br/>
        ///    be enabled on the Death GUI (Note: This button will not appear in single player games).<br/>
        ///  - nHelpStringReference<br/>
        ///  - sHelpString
        /// </summary>
        public static void PopUpDeathGUIPanel(uint oPC, bool bRespawnButtonEnabled = true, bool bWaitForHelpButtonEnabled = true, int nHelpStringReference = 0, string sHelpString = "")
        {
            NWN.Core.NWScript.PopUpDeathGUIPanel(oPC, bRespawnButtonEnabled ? 1 : 0, bWaitForHelpButtonEnabled ? 1 : 0, nHelpStringReference, sHelpString);
        }

        /// <summary>
        ///  Disable oTrap.<br/>
        ///  - oTrap: a placeable, door or trigger.
        /// </summary>
        public static void SetTrapDisabled(uint oTrap)
        {
            NWN.Core.NWScript.SetTrapDisabled(oTrap);
        }

        /// <summary>
        ///  Get the last object that was sent as a GetLastAttacker(), GetLastDamager(),<br/>
        ///  GetLastSpellCaster() (for a hostile spell), or GetLastDisturbed() (when a<br/>
        ///  creature is pickpocketed).<br/>
        ///  Note: Return values may only ever be:<br/>
        ///  1) A Creature<br/>
        ///  2) Plot Characters will never have this value set<br/>
        ///  3) Area of Effect Objects will return the AOE creator if they are registered<br/>
        ///     as this value, otherwise they will return INVALID_OBJECT_ID<br/>
        ///  4) Traps will not return the creature that set the trap.<br/>
        ///  5) This value will never be overwritten by another non-creature object.<br/>
        ///  6) This value will never be a dead/destroyed creature
        /// </summary>
        public static uint GetLastHostileActor(uint oVictim = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetLastHostileActor(oVictim);
        }

        /// <summary>
        ///  Force all the characters of the players who are currently in the game to<br/>
        ///  be exported to their respective directories i.e. LocalVault/ServerVault/ etc.
        /// </summary>
        public static void ExportAllCharacters()
        {
            NWN.Core.NWScript.ExportAllCharacters();
        }

        /// <summary>
        ///  Get the Day Track for oArea.
        /// </summary>
        public static int MusicBackgroundGetDayTrack(uint oArea)
        {
            return NWN.Core.NWScript.MusicBackgroundGetDayTrack(oArea);
        }

        /// <summary>
        ///  Get the Night Track for oArea.
        /// </summary>
        public static int MusicBackgroundGetNightTrack(uint oArea)
        {
            return NWN.Core.NWScript.MusicBackgroundGetNightTrack(oArea);
        }

        /// <summary>
        ///  Write sLogEntry as a timestamped entry into the log file
        /// </summary>
        public static void WriteTimestampedLogEntry(string sLogEntry)
        {
            NWN.Core.NWScript.WriteTimestampedLogEntry(sLogEntry);
        }

        /// <summary>
        ///  Get the module's name in the language of the server that's running it.<br/>
        ///  * If there is no entry for the language of the server, it will return an<br/>
        ///    empty string
        /// </summary>
        public static string GetModuleName()
        {
            return NWN.Core.NWScript.GetModuleName();
        }

        /// <summary>
        ///  Get the player leader of the faction of which oMemberOfFaction is a member.<br/>
        ///  * Returns OBJECT_INVALID if oMemberOfFaction is not a valid creature,<br/>
        ///    or oMemberOfFaction is a member of a NPC faction.
        /// </summary>
        public static uint GetFactionLeader(uint oMemberOfFaction)
        {
            return NWN.Core.NWScript.GetFactionLeader(oMemberOfFaction);
        }

        /// <summary>
        ///  Sends szMessage to all the Dungeon Masters currently on the server.
        /// </summary>
        public static void SendMessageToAllDMs(string szMessage)
        {
            NWN.Core.NWScript.SendMessageToAllDMs(szMessage);
        }

        /// <summary>
        ///  End the currently running game, play sEndMovie then return all players to the<br/>
        ///  game's main menu.
        /// </summary>
        public static void EndGame(string sEndMovie)
        {
            NWN.Core.NWScript.EndGame(sEndMovie);
        }

        /// <summary>
        ///  Remove oPlayer from the server.<br/>
        ///  You can optionally specify a reason to override the text shown to the player.
        /// </summary>
        public static void BootPC(uint oPlayer, string sReason = "")
        {
            NWN.Core.NWScript.BootPC(oPlayer, sReason);
        }

        /// <summary>
        ///  Counterspell oCounterSpellTarget.
        /// </summary>
        public static void ActionCounterSpell(uint oCounterSpellTarget)
        {
            NWN.Core.NWScript.ActionCounterSpell(oCounterSpellTarget);
        }

        /// <summary>
        ///  Set the ambient day volume for oArea to nVolume.<br/>
        ///  - oArea<br/>
        ///  - nVolume: 0 - 100
        /// </summary>
        public static void AmbientSoundSetDayVolume(uint oArea, int nVolume)
        {
            NWN.Core.NWScript.AmbientSoundSetDayVolume(oArea, nVolume);
        }

        /// <summary>
        ///  Set the ambient night volume for oArea to nVolume.<br/>
        ///  - oArea<br/>
        ///  - nVolume: 0 - 100
        /// </summary>
        public static void AmbientSoundSetNightVolume(uint oArea, int nVolume)
        {
            NWN.Core.NWScript.AmbientSoundSetNightVolume(oArea, nVolume);
        }

        /// <summary>
        ///  Get the Battle Track for oArea.
        /// </summary>
        public static int MusicBackgroundGetBattleTrack(uint oArea)
        {
            return NWN.Core.NWScript.MusicBackgroundGetBattleTrack(oArea);
        }

        /// <summary>
        ///  Determine whether oObject has an inventory.<br/>
        ///  * Returns true for creatures and stores, and checks to see if an item or placeable object is a container.<br/>
        ///  * Returns false for all other object types.
        /// </summary>
        public static bool GetHasInventory(uint oObject)
        {
            return NWN.Core.NWScript.GetHasInventory(oObject) == 1;
        }


        /// <summary>
        ///  Get the duration (in seconds) of the sound attached to nStrRef<br/>
        ///  * Returns 0.0f if no duration is stored or if no sound is attached
        /// </summary>
        public static float GetStrRefSoundDuration(int nStrRef)
        {
            return NWN.Core.NWScript.GetStrRefSoundDuration(nStrRef);
        }

        /// <summary>
        ///  Add oPC to oPartyLeader's party.  This will only work on two PCs.<br/>
        ///  - oPC: player to add to a party<br/>
        ///  - oPartyLeader: player already in the party
        /// </summary>
        public static void AddToParty(uint oPC, uint oPartyLeader)
        {
            NWN.Core.NWScript.AddToParty(oPC, oPartyLeader);
        }

        /// <summary>
        ///  Remove oPC from their current party. This will only work on a PC.<br/>
        ///  - oPC: removes this player from whatever party they're currently in.
        /// </summary>
        public static void RemoveFromParty(uint oPC)
        {
            NWN.Core.NWScript.RemoveFromParty(oPC);
        }

        /// <summary>
        ///  Returns the stealth mode of the specified creature.<br/>
        ///  - oCreature<br/>
        ///  * Returns a constant STEALTH_MODE_*
        /// </summary>
        public static StealthModeType GetStealthMode(uint oCreature)
        {
            return (StealthModeType)NWN.Core.NWScript.GetStealthMode(oCreature);
        }

        /// <summary>
        ///  Returns the detection mode of the specified creature.<br/>
        ///  - oCreature<br/>
        ///  * Returns a constant DETECT_MODE_*
        /// </summary>
        public static DetectModeType GetDetectMode(uint oCreature)
        {
            return (DetectModeType)NWN.Core.NWScript.GetDetectMode(oCreature);
        }

        /// <summary>
        ///  Returns the defensive casting mode of the specified creature.<br/>
        ///  - oCreature<br/>
        ///  * Returns a constant DEFENSIVE_CASTING_MODE_*
        /// </summary>
        public static DefensiveCastingModeType GetDefensiveCastingMode(uint oCreature)
        {
            return (DefensiveCastingModeType)NWN.Core.NWScript.GetDefensiveCastingMode(oCreature);
        }

        /// <summary>
        ///  returns the appearance type of the specified creature.<br/>
        ///  * returns a constant APPEARANCE_TYPE_* for valid creatures<br/>
        ///  * returns APPEARANCE_TYPE_INVALID for non creatures/invalid creatures
        /// </summary>
        public static AppearanceType GetAppearanceType(uint oCreature)
        {
            return (AppearanceType)NWN.Core.NWScript.GetAppearanceType(oCreature);
        }


        /// <summary>
        ///  SpawnScriptDebugger() will attempt to communicate with the a running script debugger<br/>
        ///  instance. You need to run it yourself, and enable it in Options/Config beforehand.<br/>
        ///  A sample debug server is included with the game installation in utils/.<br/>
        ///  Will only work in singleplayer, NOT on dedicated servers.<br/>
        ///  In order to compile the script for debugging go to Tools-&gt;Options-&gt;Script Editor<br/>
        ///  and check the box labeled "Generate Debug Information When Compiling Scripts"<br/>
        ///  After you have checked the above box, recompile the script that you want to debug.<br/>
        ///  If the script file isn't compiled for debugging, this command will do nothing.<br/>
        ///  Remove any SpawnScriptDebugger() calls once you have finished<br/>
        ///  debugging the script.
        /// </summary>
        public static void SpawnScriptDebugger()
        {
            NWN.Core.NWScript.SpawnScriptDebugger();
        }
        /// <summary>
        ///  in an onItemAcquired script, returns the size of the stack of the item<br/>
        ///  that was just acquired.<br/>
        ///  * returns the stack size of the item acquired
        /// </summary>
        public static int GetModuleItemAcquiredStackSize()
        {
            return NWN.Core.NWScript.GetModuleItemAcquiredStackSize();
        }

        /// <summary>
        ///  Decrement the remaining uses per day for this creature by one.<br/>
        ///  - oCreature: creature to modify<br/>
        ///  - nFeat: constant FEAT_*
        /// </summary>
        public static void DecrementRemainingFeatUses(uint oCreature, int nFeat)
        {
            NWN.Core.NWScript.DecrementRemainingFeatUses(oCreature, nFeat);
        }

        /// <summary>
        ///  Decrement the remaining uses per day for this creature by one.<br/>
        ///  - oCreature: creature to modify<br/>
        ///  - nSpell: constant SPELL_*
        /// </summary>
        public static void DecrementRemainingSpellUses(uint oCreature, int nSpell)
        {
            NWN.Core.NWScript.DecrementRemainingSpellUses(oCreature, nSpell);
        }

        /// <summary>
        ///  returns the template used to create this object (if appropriate)<br/>
        ///  * returns an empty string when no template found
        /// </summary>
        public static string GetResRef(uint oObject)
        {
            return NWN.Core.NWScript.GetResRef(oObject);
        }

        /// <summary>
        ///  returns an effect that will petrify the target<br/>
        ///  * currently applies EffectParalyze and the stoneskin visual effect.
        /// </summary>
        public static Effect EffectPetrify()
        {
            return NWN.Core.NWScript.EffectPetrify();
        }

        /// <summary>
        ///  duplicates the item and returns a new object<br/>
        ///  oItem - item to copy<br/>
        ///  oTargetInventory - create item in this object&apos;s inventory. If this parameter<br/>
        ///                     is not valid, the item will be created in oItem&apos;s location<br/>
        ///  bCopyVars - copy the local variables from the old item to the new one<br/>
        ///  * returns the new item<br/>
        ///  * returns OBJECT_INVALID for non-items.<br/>
        ///  * can only copy empty item containers. will return OBJECT_INVALID if oItem contains<br/>
        ///    other items.<br/>
        ///  * if it is possible to merge this item with any others in the target location,<br/>
        ///    then it will do so and return the merged object.
        /// </summary>
        public static uint CopyItem(uint oItem, uint oTargetInventory = OBJECT_INVALID, bool bCopyVars = false)
        {
            return NWN.Core.NWScript.CopyItem(oItem, oTargetInventory, bCopyVars ? 1 : 0);
        }

        /// <summary>
        ///  returns an effect that is guaranteed to paralyze a creature.<br/>
        ///  this effect is identical to EffectParalyze except that it cannot be resisted.
        /// </summary>
        public static Effect EffectCutsceneParalyze()
        {
            return NWN.Core.NWScript.EffectCutsceneParalyze();
        }

        /// <summary>
        ///  returns true if the item CAN be dropped<br/>
        ///  Droppable items will appear on a creature&apos;s remains when the creature is killed.
        /// </summary>
        public static bool GetDroppableFlag(uint oItem)
        {
            return NWN.Core.NWScript.GetDroppableFlag(oItem) == 1;
        }

        /// <summary>
        ///  returns true if the object is usable
        /// </summary>
        public static bool GetUseableFlag(uint oObject = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetUseableFlag(oObject) == 1;
        }

        /// <summary>
        ///  returns true if the item is stolen
        /// </summary>
        public static bool GetStolenFlag(uint oStolen)
        {
            return NWN.Core.NWScript.GetStolenFlag(oStolen) == 1;
        }

        /// <summary>
        ///  This stores a float out to the specified campaign database<br/>
        ///  The database name:<br/>
        ///   - is case insensitive and it must be the same for both set and get functions.<br/>
        ///   - can only contain alphanumeric characters, no spaces.<br/>
        ///  The var name must be unique across the entire database, regardless of the variable type.<br/>
        ///  If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static void SetCampaignFloat(string sCampaignName, string sVarName, float flFloat, uint oPlayer = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetCampaignFloat(sCampaignName, sVarName, flFloat, oPlayer);
        }

        /// <summary>
        ///  This stores an int out to the specified campaign database<br/>
        ///  The database name:<br/>
        ///   - is case insensitive and it must be the same for both set and get functions.<br/>
        ///   - can only contain alphanumeric characters, no spaces.<br/>
        ///  The var name must be unique across the entire database, regardless of the variable type.<br/>
        ///  If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static void SetCampaignInt(string sCampaignName, string sVarName, int nInt, uint oPlayer = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetCampaignInt(sCampaignName, sVarName, nInt, oPlayer);
        }

        /// <summary>
        ///  This stores a vector out to the specified campaign database<br/>
        ///  The database name:<br/>
        ///   - is case insensitive and it must be the same for both set and get functions.<br/>
        ///   - can only contain alphanumeric characters, no spaces.<br/>
        ///  The var name must be unique across the entire database, regardless of the variable type.<br/>
        ///  If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static void SetCampaignVector(string sCampaignName, string sVarName, Vector3 vVector, uint oPlayer = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetCampaignVector(sCampaignName, sVarName, vVector, oPlayer);
        }

        /// <summary>
        ///  This stores a location out to the specified campaign database<br/>
        ///  The database name:<br/>
        ///   - is case insensitive and it must be the same for both set and get functions.<br/>
        ///   - can only contain alphanumeric characters, no spaces.<br/>
        ///  The var name must be unique across the entire database, regardless of the variable type.<br/>
        ///  If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static void SetCampaignLocation(string sCampaignName, string sVarName, Location locLocation, uint oPlayer = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetCampaignLocation(sCampaignName, sVarName, locLocation, oPlayer);
        }

        /// <summary>
        ///  This stores a string out to the specified campaign database<br/>
        ///  The database name:<br/>
        ///   - is case insensitive and it must be the same for both set and get functions.<br/>
        ///   - can only contain alphanumeric characters, no spaces.<br/>
        ///  The var name must be unique across the entire database, regardless of the variable type.<br/>
        ///  If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static void SetCampaignString(string sCampaignName, string sVarName, string sString, uint oPlayer = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetCampaignString(sCampaignName, sVarName, sString, oPlayer);
        }

        /// <summary>
        ///  This will delete the entire campaign database if it exists.
        /// </summary>
        public static void DestroyCampaignDatabase(string sCampaignName)
        {
            NWN.Core.NWScript.DestroyCampaignDatabase(sCampaignName);
        }

        /// <summary>
        ///  This will read a float from the  specified campaign database<br/>
        ///  The database name:<br/>
        ///   - is case insensitive and it must be the same for both set and get functions.<br/>
        ///   - can only contain alphanumeric characters, no spaces.<br/>
        ///  The var name must be unique across the entire database, regardless of the variable type.<br/>
        ///  If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static float GetCampaignFloat(string sCampaignName, string sVarName, uint oPlayer = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetCampaignFloat(sCampaignName, sVarName, oPlayer);
        }

        /// <summary>
        ///  This will read an int from the  specified campaign database<br/>
        ///  The database name:<br/>
        ///   - is case insensitive and it must be the same for both set and get functions.<br/>
        ///   - can only contain alphanumeric characters, no spaces.<br/>
        ///  The var name must be unique across the entire database, regardless of the variable type.<br/>
        ///  If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static int GetCampaignInt(string sCampaignName, string sVarName, uint oPlayer = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetCampaignInt(sCampaignName, sVarName, oPlayer);
        }

        /// <summary>
        ///  This will read a vector from the  specified campaign database<br/>
        ///  The database name:<br/>
        ///   - is case insensitive and it must be the same for both set and get functions.<br/>
        ///   - can only contain alphanumeric characters, no spaces.<br/>
        ///  The var name must be unique across the entire database, regardless of the variable type.<br/>
        ///  If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static Vector3 GetCampaignVector(string sCampaignName, string sVarName, uint oPlayer = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetCampaignVector(sCampaignName, sVarName, oPlayer);
        }

        /// <summary>
        ///  This will read a location from the  specified campaign database<br/>
        ///  The database name:<br/>
        ///   - is case insensitive and it must be the same for both set and get functions.<br/>
        ///   - can only contain alphanumeric characters, no spaces.<br/>
        ///  The var name must be unique across the entire database, regardless of the variable type.<br/>
        ///  If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static Location GetCampaignLocation(string sCampaignName, string sVarName, uint oPlayer = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetCampaignLocation(sCampaignName, sVarName, oPlayer);
        }

        /// <summary>
        ///  This will read a string from the  specified campaign database<br/>
        ///  The database name:<br/>
        ///   - is case insensitive and it must be the same for both set and get functions.<br/>
        ///   - can only contain alphanumeric characters, no spaces.<br/>
        ///  The var name must be unique across the entire database, regardless of the variable type.<br/>
        ///  If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static string GetCampaignString(string sCampaignName, string sVarName, uint oPlayer = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetCampaignString(sCampaignName, sVarName, oPlayer);
        }

        /// <summary>
        ///  Duplicates the object specified by oSource.<br/>
        ///  NOTE: this command can be used for copying Creatures, Items, Placeables, Waypoints, Stores, Doors, Triggers, Encounters.<br/>
        ///  If an owner is specified and the object is an item, it will be put into their inventory<br/>
        ///  Otherwise, it will be created at the location.<br/>
        ///  If a new tag is specified, it will be assigned to the new object.<br/>
        ///  If bCopyLocalState is true, local vars, effects, action queue, and transition info (triggers, doors) are copied over.
        /// </summary>
        public static uint CopyObject(uint oSource, Location locLocation, uint oOwner = OBJECT_INVALID, string sNewTag = "", bool bCopyLocalState = false)
        {
            return NWN.Core.NWScript.CopyObject(oSource, locLocation, oOwner, sNewTag, bCopyLocalState ? 1 : 0);
        }

        /// <summary>
        ///  This will remove ANY campaign variable. Regardless of type.
        /// </summary>
        public static void DeleteCampaignVariable(string sCampaignName, string sVarName, uint oPlayer = OBJECT_INVALID)
        {
            NWN.Core.NWScript.DeleteCampaignVariable(sCampaignName, sVarName, oPlayer);
        }

        /// <summary>
        ///  Stores an object with the given id.<br/>
        ///  NOTE: this command can be used for storing Creatures, Items, Placeables, Waypoints, Stores, Doors, Triggers, Encounters.<br/>
        ///  Returns 0 if it failled, 1 if it worked.<br/>
        ///  If bSaveObjectState is true, local vars, effects, action queue, and transition info (triggers, doors) are saved out<br/>
        ///  (except for Combined Area Format, which always has object state saved out).
        /// </summary>
        public static int StoreCampaignObject(string sCampaignName, string sVarName, uint oObject, uint oPlayer = OBJECT_INVALID, bool bSaveObjectState = false)
        {
            return NWN.Core.NWScript.StoreCampaignObject(sCampaignName, sVarName, oObject, oPlayer, bSaveObjectState ? 1 : 0);
        }

        /// <summary>
        ///  Use RetrieveCampaign with the given id to restore it.<br/>
        ///  If you specify an owner, the object will try to be created in their repository<br/>
        ///  If the owner can&apos;t handle the item (or if it&apos;s a non-item) it will be created at the given location.<br/>
        ///  If bLoadObjectState is true, local vars, effects, action queue, and transition info (triggers, doors) are read in.
        /// </summary>
        public static uint RetrieveCampaignObject(string sCampaignName, string sVarName, Location locLocation, uint oOwner = OBJECT_INVALID, uint oPlayer = OBJECT_INVALID, bool bLoadObjectState = false)
        {
            return NWN.Core.NWScript.RetrieveCampaignObject(sCampaignName, sVarName, locLocation, oOwner, oPlayer, bLoadObjectState ? 1 : 0);
        }

        /// <summary>
        ///  Returns an effect that is guaranteed to dominate a creature<br/>
        ///  Like EffectDominated but cannot be resisted
        /// </summary>
        public static Effect EffectCutsceneDominated()
        {
            return NWN.Core.NWScript.EffectCutsceneDominated();
        }

        /// <summary>
        ///  Returns stack size of an item<br/>
        ///  - oItem: item to query
        /// </summary>
        public static int GetItemStackSize(uint oItem)
        {
            return NWN.Core.NWScript.GetItemStackSize(oItem);
        }

        /// <summary>
        ///  Sets stack size of an item.<br/>
        ///  - oItem: item to change<br/>
        ///  - nSize: new size of stack.  Will be restricted to be between 1 and the<br/>
        ///    maximum stack size for the item type.  If a value less than 1 is passed it<br/>
        ///    will set the stack to 1.  If a value greater than the max is passed<br/>
        ///    then it will set the stack to the maximum size
        /// </summary>
        public static void SetItemStackSize(uint oItem, int nSize)
        {
            NWN.Core.NWScript.SetItemStackSize(oItem, nSize);
        }

        /// <summary>
        ///  Returns charges left on an item<br/>
        ///  - oItem: item to query
        /// </summary>
        public static int GetItemCharges(uint oItem)
        {
            return NWN.Core.NWScript.GetItemCharges(oItem);
        }

        /// <summary>
        ///  Sets charges left on an item.<br/>
        ///  - oItem: item to change<br/>
        ///  - nCharges: number of charges.  If value below 0 is passed, # charges will<br/>
        ///    be set to 0.  If value greater than maximum is passed, # charges will<br/>
        ///    be set to maximum.  If the # charges drops to 0 the item<br/>
        ///    will be destroyed.
        /// </summary>
        public static void SetItemCharges(uint oItem, int nCharges)
        {
            NWN.Core.NWScript.SetItemCharges(oItem, nCharges);
        }

        /// <summary>
        ///  ***********************  START OF ITEM PROPERTY FUNCTIONS  **********************<br/>
        ///  adds an item property to the specified item<br/>
        ///  Only temporary and permanent duration types are allowed.
        /// </summary>
        public static void AddItemProperty(DurationType nDurationType, ItemProperty ipProperty, uint oItem, float fDuration = 0.0f)
        {
            NWN.Core.NWScript.AddItemProperty((int)nDurationType, ipProperty, oItem, fDuration);
        }
        /// <summary>
        ///  removes an item property from the specified item
        /// </summary>
        public static void RemoveItemProperty(uint oItem, ItemProperty ipProperty)
        {
            NWN.Core.NWScript.RemoveItemProperty(oItem, ipProperty);
        }

        /// <summary>
        ///  if the item property is valid this will return true
        /// </summary>
        public static bool GetIsItemPropertyValid(ItemProperty ipProperty)
        {
            return NWN.Core.NWScript.GetIsItemPropertyValid(ipProperty) == 1;
        }

        /// <summary>
        ///  Gets the first item property on an item
        /// </summary>
        public static ItemProperty GetFirstItemProperty(uint oItem)
        {
            return NWN.Core.NWScript.GetFirstItemProperty(oItem);
        }

        /// <summary>
        ///  Will keep retrieving the next and the next item property on an Item,<br/>
        ///  will return an invalid item property when the list is empty.
        /// </summary>
        public static ItemProperty GetNextItemProperty(uint oItem)
        {
            return NWN.Core.NWScript.GetNextItemProperty(oItem);
        }

        /// <summary>
        ///  will return the item property type (ie. holy avenger)
        /// </summary>
        public static ItemPropertyType GetItemPropertyType(ItemProperty ip)
        {
            return (ItemPropertyType)NWN.Core.NWScript.GetItemPropertyType(ip);
        }

        /// <summary>
        ///  will return the duration type of the item property
        /// </summary>
        public static DurationType GetItemPropertyDurationType(ItemProperty ip)
        {
            return (DurationType)NWN.Core.NWScript.GetItemPropertyDurationType(ip);
        }

        /// <summary>
        ///  Returns Item property ability bonus.  You need to specify an<br/>
        ///  ability constant(IP_CONST_ABILITY_*) and the bonus.  The bonus should<br/>
        ///  be a positive integer between 1 and 12.
        /// </summary>
        public static ItemProperty ItemPropertyAbilityBonus(int nAbility, int nBonus)
        {
            return NWN.Core.NWScript.ItemPropertyAbilityBonus(nAbility, nBonus);
        }

        /// <summary>
        ///  Returns Item property AC bonus.  You need to specify the bonus.<br/>
        ///  The bonus should be a positive integer between 1 and 20. The modifier<br/>
        ///  type depends on the item it is being applied to.
        /// </summary>
        public static ItemProperty ItemPropertyACBonus(int nBonus)
        {
            return NWN.Core.NWScript.ItemPropertyACBonus(nBonus);
        }

        /// <summary>
        ///  Returns Item property AC bonus vs. alignment group.  An example of<br/>
        ///  an alignment group is Chaotic, or Good.  You need to specify the<br/>
        ///  alignment group constant(IP_CONST_ALIGNMENTGROUP_*) and the AC bonus.<br/>
        ///  The AC bonus should be an integer between 1 and 20.  The modifier<br/>
        ///  type depends on the item it is being applied to.
        /// </summary>
        public static ItemProperty ItemPropertyACBonusVsAlign(int nAlignGroup, int nACBonus)
        {
            return NWN.Core.NWScript.ItemPropertyACBonusVsAlign(nAlignGroup, nACBonus);
        }

        /// <summary>
        ///  Returns Item property AC bonus vs. Damage type (ie. piercing).  You<br/>
        ///  need to specify the damage type constant(IP_CONST_DAMAGETYPE_*) and the<br/>
        ///  AC bonus.  The AC bonus should be an integer between 1 and 20.  The<br/>
        ///  modifier type depends on the item it is being applied to.<br/>
        ///  NOTE: Only the first 3 damage types may be used here, the 3 basic<br/>
        ///        physical types.
        /// </summary>
        public static ItemProperty ItemPropertyACBonusVsDmgType(DamageType nDamageType, int nACBonus)
        {
            return NWN.Core.NWScript.ItemPropertyACBonusVsDmgType((int)nDamageType, nACBonus);
        }

        /// <summary>
        ///  Returns Item property AC bonus vs. Racial group.  You need to specify<br/>
        ///  the racial group constant(IP_CONST_RACIALTYPE_*) and the AC bonus.  The AC<br/>
        ///  bonus should be an integer between 1 and 20.  The modifier type depends<br/>
        ///  on the item it is being applied to.
        /// </summary>
        public static ItemProperty ItemPropertyACBonusVsRace(int nRace, int nACBonus)
        {
            return NWN.Core.NWScript.ItemPropertyACBonusVsRace(nRace, nACBonus);
        }

        /// <summary>
        ///  Returns Item property AC bonus vs. Specific alignment.  You need to<br/>
        ///  specify the specific alignment constant(IP_CONST_ALIGNMENT_*) and the AC<br/>
        ///  bonus.  The AC bonus should be an integer between 1 and 20.  The<br/>
        ///  modifier type depends on the item it is being applied to.
        /// </summary>
        public static ItemProperty ItemPropertyACBonusVsSAlign(int nAlign, int nACBonus)
        {
            return NWN.Core.NWScript.ItemPropertyACBonusVsSAlign(nAlign, nACBonus);
        }

        /// <summary>
        ///  Returns Item property Enhancement bonus.  You need to specify the<br/>
        ///  enhancement bonus.  The Enhancement bonus should be an integer between<br/>
        ///  1 and 20.
        /// </summary>
        public static ItemProperty ItemPropertyEnhancementBonus(int nEnhancementBonus)
        {
            return NWN.Core.NWScript.ItemPropertyEnhancementBonus(nEnhancementBonus);
        }

        /// <summary>
        ///  Returns Item property Enhancement bonus vs. an Alignment group.  You<br/>
        ///  need to specify the alignment group constant(IP_CONST_ALIGNMENTGROUP_*)<br/>
        ///  and the enhancement bonus.  The Enhancement bonus should be an integer<br/>
        ///  between 1 and 20.
        /// </summary>
        public static ItemProperty ItemPropertyEnhancementBonusVsAlign(int nAlignGroup, int nBonus)
        {
            return NWN.Core.NWScript.ItemPropertyEnhancementBonusVsAlign(nAlignGroup, nBonus);
        }

        /// <summary>
        ///  Returns Item property Enhancement bonus vs. Racial group.  You need<br/>
        ///  to specify the racial group constant(IP_CONST_RACIALTYPE_*) and the<br/>
        ///  enhancement bonus.  The enhancement bonus should be an integer between<br/>
        ///  1 and 20.
        /// </summary>
        public static ItemProperty ItemPropertyEnhancementBonusVsRace(int nRace, int nBonus)
        {
            return NWN.Core.NWScript.ItemPropertyEnhancementBonusVsRace(nRace, nBonus);
        }

        /// <summary>
        ///  Returns Item property Enhancement bonus vs. a specific alignment.  You<br/>
        ///  need to specify the alignment constant(IP_CONST_ALIGNMENT_*) and the<br/>
        ///  enhancement bonus.  The enhancement bonus should be an integer between<br/>
        ///  1 and 20.
        /// </summary>
        public static ItemProperty ItemPropertyEnhancementBonusVsSAlign(int nAlign, int nBonus)
        {
            return NWN.Core.NWScript.ItemPropertyEnhancementBonusVsSAlign(nAlign, nBonus);
        }

        /// <summary>
        ///  Returns Item property Enhancment penalty.  You need to specify the<br/>
        ///  enhancement penalty.  The enhancement penalty should be a POSITIVE<br/>
        ///  integer between 1 and 5 (ie. 1 = -1).
        /// </summary>
        public static ItemProperty ItemPropertyEnhancementPenalty(int nPenalty)
        {
            return NWN.Core.NWScript.ItemPropertyEnhancementPenalty(nPenalty);
        }

        /// <summary>
        ///  Returns Item property weight reduction.  You need to specify the weight<br/>
        ///  reduction constant(IP_CONST_REDUCEDWEIGHT_*).
        /// </summary>
        public static ItemProperty ItemPropertyWeightReduction(int nReduction)
        {
            return NWN.Core.NWScript.ItemPropertyWeightReduction(nReduction);
        }

        /// <summary>
        ///  Returns Item property Bonus Feat.  You need to specify the the feat<br/>
        ///  constant(IP_CONST_FEAT_*).
        /// </summary>
        public static ItemProperty ItemPropertyBonusFeat(int nFeat)
        {
            return NWN.Core.NWScript.ItemPropertyBonusFeat(nFeat);
        }

        /// <summary>
        ///  Returns Item property Bonus level spell (Bonus spell of level).  You must<br/>
        ///  specify the class constant(IP_CONST_CLASS_*) of the bonus spell(MUST BE a<br/>
        ///  spell casting class) and the level of the bonus spell.  The level of the<br/>
        ///  bonus spell should be an integer between 0 and 9.
        /// </summary>
        public static ItemProperty ItemPropertyBonusLevelSpell(int nClass, int nSpellLevel)
        {
            return NWN.Core.NWScript.ItemPropertyBonusLevelSpell(nClass, nSpellLevel);
        }

        /// <summary>
        ///  Returns Item property Cast spell.  You must specify the spell constant<br/>
        ///  (IP_CONST_CASTSPELL_*) and the number of uses constant(IP_CONST_CASTSPELL_NUMUSES_*).<br/>
        ///  NOTE: The number after the name of the spell in the constant is the level<br/>
        ///        at which the spell will be cast.  Sometimes there are multiple copies<br/>
        ///        of the same spell but they each are cast at a different level.  The higher<br/>
        ///        the level, the more cost will be added to the item.<br/>
        ///  NOTE: The list of spells that can be applied to an item will depend on the<br/>
        ///        item type.  For instance there are spells that can be applied to a wand<br/>
        ///        that cannot be applied to a potion.  Below is a list of the types and the<br/>
        ///        spells that are allowed to be placed on them.  If you try to put a cast<br/>
        ///        spell effect on an item that is not allowed to have that effect it will<br/>
        ///        not work.<br/>
        ///  NOTE: Even if spells have multiple versions of different levels they are only<br/>
        ///        listed below once.<br/>
        /// <br/>
        ///  WANDS:<br/>
        ///           Acid_Splash<br/>
        ///           Activate_Item<br/>
        ///           Aid<br/>
        ///           Amplify<br/>
        ///           Animate_Dead<br/>
        ///           AuraOfGlory<br/>
        ///           BalagarnsIronHorn<br/>
        ///           Bane<br/>
        ///           Banishment<br/>
        ///           Barkskin<br/>
        ///           Bestow_Curse<br/>
        ///           Bigbys_Clenched_Fist<br/>
        ///           Bigbys_Crushing_Hand<br/>
        ///           Bigbys_Forceful_Hand<br/>
        ///           Bigbys_Grasping_Hand<br/>
        ///           Bigbys_Interposing_Hand<br/>
        ///           Bless<br/>
        ///           Bless_Weapon<br/>
        ///           Blindness/Deafness<br/>
        ///           Blood_Frenzy<br/>
        ///           Bombardment<br/>
        ///           Bulls_Strength<br/>
        ///           Burning_Hands<br/>
        ///           Call_Lightning<br/>
        ///           Camoflage<br/>
        ///           Cats_Grace<br/>
        ///           Charm_Monster<br/>
        ///           Charm_Person<br/>
        ///           Charm_Person_or_Animal<br/>
        ///           Clairaudience/Clairvoyance<br/>
        ///           Clarity<br/>
        ///           Color_Spray<br/>
        ///           Confusion<br/>
        ///           Continual_Flame<br/>
        ///           Cure_Critical_Wounds<br/>
        ///           Cure_Light_Wounds<br/>
        ///           Cure_Minor_Wounds<br/>
        ///           Cure_Moderate_Wounds<br/>
        ///           Cure_Serious_Wounds<br/>
        ///           Darkness<br/>
        ///           Darkvision<br/>
        ///           Daze<br/>
        ///           Death_Ward<br/>
        ///           Dirge<br/>
        ///           Dismissal<br/>
        ///           Dispel_Magic<br/>
        ///           Displacement<br/>
        ///           Divine_Favor<br/>
        ///           Divine_Might<br/>
        ///           Divine_Power<br/>
        ///           Divine_Shield<br/>
        ///           Dominate_Animal<br/>
        ///           Dominate_Person<br/>
        ///           Doom<br/>
        ///           Dragon_Breath_Acid<br/>
        ///           Dragon_Breath_Cold<br/>
        ///           Dragon_Breath_Fear<br/>
        ///           Dragon_Breath_Fire<br/>
        ///           Dragon_Breath_Gas<br/>
        ///           Dragon_Breath_Lightning<br/>
        ///           Dragon_Breath_Paralyze<br/>
        ///           Dragon_Breath_Sleep<br/>
        ///           Dragon_Breath_Slow<br/>
        ///           Dragon_Breath_Weaken<br/>
        ///           Drown<br/>
        ///           Eagle_Spledor<br/>
        ///           Earthquake<br/>
        ///           Electric_Jolt<br/>
        ///           Elemental_Shield<br/>
        ///           Endurance<br/>
        ///           Endure_Elements<br/>
        ///           Enervation<br/>
        ///           Entangle<br/>
        ///           Entropic_Shield<br/>
        ///           Etherealness<br/>
        ///           Expeditious_Retreat<br/>
        ///           Fear<br/>
        ///           Find_Traps<br/>
        ///           Fireball<br/>
        ///           Firebrand<br/>
        ///           Flame_Arrow<br/>
        ///           Flame_Lash<br/>
        ///           Flame_Strike<br/>
        ///           Flare<br/>
        ///           Foxs_Cunning<br/>
        ///           Freedom_of_Movement<br/>
        ///           Ghostly_Visage<br/>
        ///           Ghoul_Touch<br/>
        ///           Grease<br/>
        ///           Greater_Magic_Fang<br/>
        ///           Greater_Magic_Weapon<br/>
        ///           Grenade_Acid<br/>
        ///           Grenade_Caltrops<br/>
        ///           Grenade_Chicken<br/>
        ///           Grenade_Choking<br/>
        ///           Grenade_Fire<br/>
        ///           Grenade_Holy<br/>
        ///           Grenade_Tangle<br/>
        ///           Grenade_Thunderstone<br/>
        ///           Gust_of_wind<br/>
        ///           Hammer_of_the_Gods<br/>
        ///           Haste<br/>
        ///           Hold_Animal<br/>
        ///           Hold_Monster<br/>
        ///           Hold_Person<br/>
        ///           Ice_Storm<br/>
        ///           Identify<br/>
        ///           Improved_Invisibility<br/>
        ///           Inferno<br/>
        ///           Inflict_Critical_Wounds<br/>
        ///           Inflict_Light_Wounds<br/>
        ///           Inflict_Minor_Wounds<br/>
        ///           Inflict_Moderate_Wounds<br/>
        ///           Inflict_Serious_Wounds<br/>
        ///           Invisibility<br/>
        ///           Invisibility_Purge<br/>
        ///           Invisibility_Sphere<br/>
        ///           Isaacs_Greater_Missile_Storm<br/>
        ///           Isaacs_Lesser_Missile_Storm<br/>
        ///           Knock<br/>
        ///           Lesser_Dispel<br/>
        ///           Lesser_Restoration<br/>
        ///           Lesser_Spell_Breach<br/>
        ///           Light<br/>
        ///           Lightning_Bolt<br/>
        ///           Mage_Armor<br/>
        ///           Magic_Circle_against_Alignment<br/>
        ///           Magic_Fang<br/>
        ///           Magic_Missile<br/>
        ///           Manipulate_Portal_Stone<br/>
        ///           Mass_Camoflage<br/>
        ///           Melfs_Acid_Arrow<br/>
        ///           Meteor_Swarm<br/>
        ///           Mind_Blank<br/>
        ///           Mind_Fog<br/>
        ///           Negative_Energy_Burst<br/>
        ///           Negative_Energy_Protection<br/>
        ///           Negative_Energy_Ray<br/>
        ///           Neutralize_Poison<br/>
        ///           One_With_The_Land<br/>
        ///           Owls_Insight<br/>
        ///           Owls_Wisdom<br/>
        ///           Phantasmal_Killer<br/>
        ///           Planar_Ally<br/>
        ///           Poison<br/>
        ///           Polymorph_Self<br/>
        ///           Prayer<br/>
        ///           Protection_from_Alignment<br/>
        ///           Protection_From_Elements<br/>
        ///           Quillfire<br/>
        ///           Ray_of_Enfeeblement<br/>
        ///           Ray_of_Frost<br/>
        ///           Remove_Blindness/Deafness<br/>
        ///           Remove_Curse<br/>
        ///           Remove_Disease<br/>
        ///           Remove_Fear<br/>
        ///           Remove_Paralysis<br/>
        ///           Resist_Elements<br/>
        ///           Resistance<br/>
        ///           Restoration<br/>
        ///           Sanctuary<br/>
        ///           Scare<br/>
        ///           Searing_Light<br/>
        ///           See_Invisibility<br/>
        ///           Shadow_Conjuration<br/>
        ///           Shield<br/>
        ///           Shield_of_Faith<br/>
        ///           Silence<br/>
        ///           Sleep<br/>
        ///           Slow<br/>
        ///           Sound_Burst<br/>
        ///           Spike_Growth<br/>
        ///           Stinking_Cloud<br/>
        ///           Stoneskin<br/>
        ///           Summon_Creature_I<br/>
        ///           Summon_Creature_I<br/>
        ///           Summon_Creature_II<br/>
        ///           Summon_Creature_III<br/>
        ///           Summon_Creature_IV<br/>
        ///           Sunburst<br/>
        ///           Tashas_Hideous_Laughter<br/>
        ///           True_Strike<br/>
        ///           Undeaths_Eternal_Foe<br/>
        ///           Unique_Power<br/>
        ///           Unique_Power_Self_Only<br/>
        ///           Vampiric_Touch<br/>
        ///           Virtue<br/>
        ///           Wall_of_Fire<br/>
        ///           Web<br/>
        ///           Wounding_Whispers<br/>
        /// <br/>
        ///  POTIONS:<br/>
        ///           Activate_Item<br/>
        ///           Aid<br/>
        ///           Amplify<br/>
        ///           AuraOfGlory<br/>
        ///           Bane<br/>
        ///           Barkskin<br/>
        ///           Barkskin<br/>
        ///           Barkskin<br/>
        ///           Bless<br/>
        ///           Bless_Weapon<br/>
        ///           Bless_Weapon<br/>
        ///           Blood_Frenzy<br/>
        ///           Bulls_Strength<br/>
        ///           Bulls_Strength<br/>
        ///           Bulls_Strength<br/>
        ///           Camoflage<br/>
        ///           Cats_Grace<br/>
        ///           Cats_Grace<br/>
        ///           Cats_Grace<br/>
        ///           Clairaudience/Clairvoyance<br/>
        ///           Clairaudience/Clairvoyance<br/>
        ///           Clairaudience/Clairvoyance<br/>
        ///           Clarity<br/>
        ///           Continual_Flame<br/>
        ///           Cure_Critical_Wounds<br/>
        ///           Cure_Critical_Wounds<br/>
        ///           Cure_Critical_Wounds<br/>
        ///           Cure_Light_Wounds<br/>
        ///           Cure_Light_Wounds<br/>
        ///           Cure_Minor_Wounds<br/>
        ///           Cure_Moderate_Wounds<br/>
        ///           Cure_Moderate_Wounds<br/>
        ///           Cure_Moderate_Wounds<br/>
        ///           Cure_Serious_Wounds<br/>
        ///           Cure_Serious_Wounds<br/>
        ///           Cure_Serious_Wounds<br/>
        ///           Darkness<br/>
        ///           Darkvision<br/>
        ///           Darkvision<br/>
        ///           Death_Ward<br/>
        ///           Dispel_Magic<br/>
        ///           Dispel_Magic<br/>
        ///           Displacement<br/>
        ///           Divine_Favor<br/>
        ///           Divine_Might<br/>
        ///           Divine_Power<br/>
        ///           Divine_Shield<br/>
        ///           Dragon_Breath_Acid<br/>
        ///           Dragon_Breath_Cold<br/>
        ///           Dragon_Breath_Fear<br/>
        ///           Dragon_Breath_Fire<br/>
        ///           Dragon_Breath_Gas<br/>
        ///           Dragon_Breath_Lightning<br/>
        ///           Dragon_Breath_Paralyze<br/>
        ///           Dragon_Breath_Sleep<br/>
        ///           Dragon_Breath_Slow<br/>
        ///           Dragon_Breath_Weaken<br/>
        ///           Eagle_Spledor<br/>
        ///           Eagle_Spledor<br/>
        ///           Eagle_Spledor<br/>
        ///           Elemental_Shield<br/>
        ///           Elemental_Shield<br/>
        ///           Endurance<br/>
        ///           Endurance<br/>
        ///           Endurance<br/>
        ///           Endure_Elements<br/>
        ///           Entropic_Shield<br/>
        ///           Ethereal_Visage<br/>
        ///           Ethereal_Visage<br/>
        ///           Etherealness<br/>
        ///           Expeditious_Retreat<br/>
        ///           Find_Traps<br/>
        ///           Foxs_Cunning<br/>
        ///           Foxs_Cunning<br/>
        ///           Foxs_Cunning<br/>
        ///           Freedom_of_Movement<br/>
        ///           Ghostly_Visage<br/>
        ///           Ghostly_Visage<br/>
        ///           Ghostly_Visage<br/>
        ///           Globe_of_Invulnerability<br/>
        ///           Greater_Bulls_Strength<br/>
        ///           Greater_Cats_Grace<br/>
        ///           Greater_Dispelling<br/>
        ///           Greater_Dispelling<br/>
        ///           Greater_Eagles_Splendor<br/>
        ///           Greater_Endurance<br/>
        ///           Greater_Foxs_Cunning<br/>
        ///           Greater_Magic_Weapon<br/>
        ///           Greater_Owls_Wisdom<br/>
        ///           Greater_Restoration<br/>
        ///           Greater_Spell_Mantle<br/>
        ///           Greater_Stoneskin<br/>
        ///           Grenade_Acid<br/>
        ///           Grenade_Caltrops<br/>
        ///           Grenade_Chicken<br/>
        ///           Grenade_Choking<br/>
        ///           Grenade_Fire<br/>
        ///           Grenade_Holy<br/>
        ///           Grenade_Tangle<br/>
        ///           Grenade_Thunderstone<br/>
        ///           Haste<br/>
        ///           Haste<br/>
        ///           Heal<br/>
        ///           Hold_Animal<br/>
        ///           Hold_Monster<br/>
        ///           Hold_Person<br/>
        ///           Identify<br/>
        ///           Invisibility<br/>
        ///           Lesser_Dispel<br/>
        ///           Lesser_Dispel<br/>
        ///           Lesser_Mind_Blank<br/>
        ///           Lesser_Restoration<br/>
        ///           Lesser_Spell_Mantle<br/>
        ///           Light<br/>
        ///           Light<br/>
        ///           Mage_Armor<br/>
        ///           Manipulate_Portal_Stone<br/>
        ///           Mass_Camoflage<br/>
        ///           Mind_Blank<br/>
        ///           Minor_Globe_of_Invulnerability<br/>
        ///           Minor_Globe_of_Invulnerability<br/>
        ///           Mordenkainens_Disjunction<br/>
        ///           Negative_Energy_Protection<br/>
        ///           Negative_Energy_Protection<br/>
        ///           Negative_Energy_Protection<br/>
        ///           Neutralize_Poison<br/>
        ///           One_With_The_Land<br/>
        ///           Owls_Insight<br/>
        ///           Owls_Wisdom<br/>
        ///           Owls_Wisdom<br/>
        ///           Owls_Wisdom<br/>
        ///           Polymorph_Self<br/>
        ///           Prayer<br/>
        ///           Premonition<br/>
        ///           Protection_From_Elements<br/>
        ///           Protection_From_Elements<br/>
        ///           Protection_from_Spells<br/>
        ///           Protection_from_Spells<br/>
        ///           Raise_Dead<br/>
        ///           Remove_Blindness/Deafness<br/>
        ///           Remove_Curse<br/>
        ///           Remove_Disease<br/>
        ///           Remove_Fear<br/>
        ///           Remove_Paralysis<br/>
        ///           Resist_Elements<br/>
        ///           Resist_Elements<br/>
        ///           Resistance<br/>
        ///           Resistance<br/>
        ///           Restoration<br/>
        ///           Resurrection<br/>
        ///           Rogues_Cunning<br/>
        ///           See_Invisibility<br/>
        ///           Shadow_Shield<br/>
        ///           Shapechange<br/>
        ///           Shield<br/>
        ///           Shield_of_Faith<br/>
        ///           Special_Alcohol_Beer<br/>
        ///           Special_Alcohol_Spirits<br/>
        ///           Special_Alcohol_Wine<br/>
        ///           Special_Herb_Belladonna<br/>
        ///           Special_Herb_Garlic<br/>
        ///           Spell_Mantle<br/>
        ///           Spell_Resistance<br/>
        ///           Spell_Resistance<br/>
        ///           Stoneskin<br/>
        ///           Tensers_Transformation<br/>
        ///           True_Seeing<br/>
        ///           True_Strike<br/>
        ///           Unique_Power<br/>
        ///           Unique_Power_Self_Only<br/>
        ///           Virtue<br/>
        /// <br/>
        ///  GENERAL USE (ie. everything else):<br/>
        ///           Just about every spell is useable by all the general use items so instead we<br/>
        ///           will only list the ones that are not allowed:<br/>
        ///           Special_Alcohol_Beer<br/>
        ///           Special_Alcohol_Spirits<br/>
        ///           Special_Alcohol_Wine<br/>
        /// </summary>
        public static ItemProperty ItemPropertyCastSpell(int nSpell, int nNumUses)
        {
            return NWN.Core.NWScript.ItemPropertyCastSpell(nSpell, nNumUses);
        }

        /// <summary>
        ///  Returns Item property damage bonus.  You must specify the damage type constant<br/>
        ///  (IP_CONST_DAMAGETYPE_*) and the amount of damage constant(IP_CONST_DAMAGEBONUS_*).<br/>
        ///  NOTE: not all the damage types will work, use only the following: Acid, Bludgeoning,<br/>
        ///        Cold, Electrical, Fire, Piercing, Slashing, Sonic.
        /// </summary>
        public static ItemProperty ItemPropertyDamageBonus(DamageType nDamageType, int nDamage)
        {
            return NWN.Core.NWScript.ItemPropertyDamageBonus((int)nDamageType, nDamage);
        }
        /// <summary>
        ///  Returns Item property damage bonus vs. Alignment groups.  You must specify the<br/>
        ///  alignment group constant(IP_CONST_ALIGNMENTGROUP_*) and the damage type constant<br/>
        ///  (IP_CONST_DAMAGETYPE_*) and the amount of damage constant(IP_CONST_DAMAGEBONUS_*).<br/>
        ///  NOTE: not all the damage types will work, use only the following: Acid, Bludgeoning,<br/>
        ///        Cold, Electrical, Fire, Piercing, Slashing, Sonic.
        /// </summary>
        public static ItemProperty ItemPropertyDamageBonusVsAlign(int nAlignGroup, DamageType nDamageType, int nDamage)
        {
            return NWN.Core.NWScript.ItemPropertyDamageBonusVsAlign(nAlignGroup, (int)nDamageType, nDamage);
        }

        /// <summary>
        ///  Returns Item property damage bonus vs. specific race.  You must specify the<br/>
        ///  racial group constant(IP_CONST_RACIALTYPE_*) and the damage type constant<br/>
        ///  (IP_CONST_DAMAGETYPE_*) and the amount of damage constant(IP_CONST_DAMAGEBONUS_*).<br/>
        ///  NOTE: not all the damage types will work, use only the following: Acid, Bludgeoning,<br/>
        ///        Cold, Electrical, Fire, Piercing, Slashing, Sonic.
        /// </summary>
        public static ItemProperty ItemPropertyDamageBonusVsRace(int nRace, DamageType nDamageType, int nDamage)
        {
            return NWN.Core.NWScript.ItemPropertyDamageBonusVsRace(nRace, (int)nDamageType, nDamage);
        }

        /// <summary>
        ///  Returns Item property damage bonus vs. specific alignment.  You must specify the<br/>
        ///  specific alignment constant(IP_CONST_ALIGNMENT_*) and the damage type constant<br/>
        ///  (IP_CONST_DAMAGETYPE_*) and the amount of damage constant(IP_CONST_DAMAGEBONUS_*).<br/>
        ///  NOTE: not all the damage types will work, use only the following: Acid, Bludgeoning,<br/>
        ///        Cold, Electrical, Fire, Piercing, Slashing, Sonic.
        /// </summary>
        public static ItemProperty ItemPropertyDamageBonusVsSAlign(int nAlign, DamageType nDamageType, int nDamage)
        {
            return NWN.Core.NWScript.ItemPropertyDamageBonusVsSAlign(nAlign, (int)nDamageType, nDamage);
        }

        /// <summary>
        ///  Returns Item property damage immunity.  You must specify the damage type constant<br/>
        ///  (IP_CONST_DAMAGETYPE_*) that you want to be immune to and the immune bonus percentage<br/>
        ///  constant(IP_CONST_DAMAGEIMMUNITY_*).<br/>
        ///  NOTE: not all the damage types will work, use only the following: Acid, Bludgeoning,<br/>
        ///        Cold, Electrical, Fire, Piercing, Slashing, Sonic.
        /// </summary>
        public static ItemProperty ItemPropertyDamageImmunity(DamageType nDamageType, int nImmuneBonus)
        {
            return NWN.Core.NWScript.ItemPropertyDamageImmunity((int)nDamageType, nImmuneBonus);
        }

        /// <summary>
        ///  Returns Item property damage penalty.  You must specify the damage penalty.<br/>
        ///  The damage penalty should be a POSITIVE integer between 1 and 5 (ie. 1 = -1).
        /// </summary>
        public static ItemProperty ItemPropertyDamagePenalty(int nPenalty)
        {
            return NWN.Core.NWScript.ItemPropertyDamagePenalty(nPenalty);
        }

        /// <summary>
        ///  Returns Item property damage reduction.  You must specify the enhancment level<br/>
        ///  (IP_CONST_DAMAGEREDUCTION_*) that is required to get past the damage reduction<br/>
        ///  and the amount of HP of damage constant(IP_CONST_DAMAGESOAK_*) will be soaked<br/>
        ///  up if your weapon is not of high enough enhancement.
        /// </summary>
        public static ItemProperty ItemPropertyDamageReduction(int nEnhancement, int nHPSoak)
        {
            return NWN.Core.NWScript.ItemPropertyDamageReduction(nEnhancement, nHPSoak);
        }

        /// <summary>
        ///  Returns Item property damage resistance.  You must specify the damage type<br/>
        ///  constant(IP_CONST_DAMAGETYPE_*) and the amount of HP of damage constant<br/>
        ///  (IP_CONST_DAMAGERESIST_*) that will be resisted against each round.
        /// </summary>
        public static ItemProperty ItemPropertyDamageResistance(DamageType nDamageType, int nHPResist)
        {
            return NWN.Core.NWScript.ItemPropertyDamageResistance((int)nDamageType, nHPResist);
        }

        /// <summary>
        ///  Returns Item property damage vulnerability.  You must specify the damage type<br/>
        ///  constant(IP_CONST_DAMAGETYPE_*) that you want the user to be extra vulnerable to<br/>
        ///  and the percentage vulnerability constant(IP_CONST_DAMAGEVULNERABILITY_*).
        /// </summary>
        public static ItemProperty ItemPropertyDamageVulnerability(DamageType nDamageType, int nVulnerability)
        {
            return NWN.Core.NWScript.ItemPropertyDamageVulnerability((int)nDamageType, nVulnerability);
        }

        /// <summary>
        ///  Return Item property Darkvision.
        /// </summary>
        public static ItemProperty ItemPropertyDarkvision()
        {
            return NWN.Core.NWScript.ItemPropertyDarkvision();
        }

        /// <summary>
        ///  Return Item property decrease ability score.  You must specify the ability<br/>
        ///  constant(IP_CONST_ABILITY_*) and the modifier constant.  The modifier must be<br/>
        ///  a POSITIVE integer between 1 and 10 (ie. 1 = -1).
        /// </summary>
        public static ItemProperty ItemPropertyDecreaseAbility(int nAbility, int nModifier)
        {
            return NWN.Core.NWScript.ItemPropertyDecreaseAbility(nAbility, nModifier);
        }

        /// <summary>
        ///  Returns Item property decrease Armor Class.  You must specify the armor<br/>
        ///  modifier type constant(IP_CONST_ACMODIFIERTYPE_*) and the armor class penalty.<br/>
        ///  The penalty must be a POSITIVE integer between 1 and 5 (ie. 1 = -1).
        /// </summary>
        public static ItemProperty ItemPropertyDecreaseAC(int nModifierType, int nPenalty)
        {
            return NWN.Core.NWScript.ItemPropertyDecreaseAC(nModifierType, nPenalty);
        }

        /// <summary>
        ///  Returns Item property decrease skill.  You must specify the constant for the<br/>
        ///  skill to be decreased(SKILL_*) and the amount of the penalty.  The penalty<br/>
        ///  must be a POSITIVE integer between 1 and 10 (ie. 1 = -1).
        /// </summary>
        public static ItemProperty ItemPropertyDecreaseSkill(int nSkill, int nPenalty)
        {
            return NWN.Core.NWScript.ItemPropertyDecreaseSkill(nSkill, nPenalty);
        }

        /// <summary>
        ///  Returns Item property container reduced weight.  This is used for special<br/>
        ///  containers that reduce the weight of the objects inside them.  You must<br/>
        ///  specify the container weight reduction type constant(IP_CONST_CONTAINERWEIGHTRED_*).
        /// </summary>
        public static ItemProperty ItemPropertyContainerReducedWeight(int nContainerType)
        {
            return NWN.Core.NWScript.ItemPropertyContainerReducedWeight(nContainerType);
        }

        /// <summary>
        ///  Returns Item property extra melee damage type.  You must specify the extra<br/>
        ///  melee base damage type that you want applied.  It is a constant(IP_CONST_DAMAGETYPE_*).<br/>
        ///  NOTE: only the first 3 base types (piercing, slashing, &amp; bludgeoning are applicable<br/>
        ///        here.<br/>
        ///  NOTE: It is also only applicable to melee weapons.
        /// </summary>
        public static ItemProperty ItemPropertyExtraMeleeDamageType(DamageType nDamageType)
        {
            return NWN.Core.NWScript.ItemPropertyExtraMeleeDamageType((int)nDamageType);
        }

        /// <summary>
        ///  Returns Item property extra ranged damage type.  You must specify the extra<br/>
        ///  melee base damage type that you want applied.  It is a constant(IP_CONST_DAMAGETYPE_*).<br/>
        ///  NOTE: only the first 3 base types (piercing, slashing, &amp; bludgeoning are applicable<br/>
        ///        here.<br/>
        ///  NOTE: It is also only applicable to ranged weapons.
        /// </summary>
        public static ItemProperty ItemPropertyExtraRangeDamageType(DamageType nDamageType)
        {
            return NWN.Core.NWScript.ItemPropertyExtraRangeDamageType((int)nDamageType);
        }

        /// <summary>
        ///  Returns Item property haste.
        /// </summary>
        public static ItemProperty ItemPropertyHaste()
        {
            return NWN.Core.NWScript.ItemPropertyHaste();
        }

        /// <summary>
        ///  Returns Item property Holy Avenger.
        /// </summary>
        public static ItemProperty ItemPropertyHolyAvenger()
        {
            return NWN.Core.NWScript.ItemPropertyHolyAvenger();
        }

        /// <summary>
        ///  Returns Item property immunity to miscellaneous effects.  You must specify the<br/>
        ///  effect to which the user is immune, it is a constant(IP_CONST_IMMUNITYMISC_*).
        /// </summary>
        public static ItemProperty ItemPropertyImmunityMisc(ImmunityType nImmunityType)
        {
            return NWN.Core.NWScript.ItemPropertyImmunityMisc((int)nImmunityType);
        }

        /// <summary>
        ///  Returns Item property improved evasion.
        /// </summary>
        public static ItemProperty ItemPropertyImprovedEvasion()
        {
            return NWN.Core.NWScript.ItemPropertyImprovedEvasion();
        }

        /// <summary>
        ///  Returns Item property bonus spell resistance.  You must specify the bonus spell<br/>
        ///  resistance constant(IP_CONST_SPELLRESISTANCEBONUS_*).
        /// </summary>
        public static ItemProperty ItemPropertyBonusSpellResistance(int nBonus)
        {
            return NWN.Core.NWScript.ItemPropertyBonusSpellResistance(nBonus);
        }

        /// <summary>
        ///  Returns Item property saving throw bonus vs. a specific effect or damage type.<br/>
        ///  You must specify the save type constant(IP_CONST_SAVEVS_*) that the bonus is<br/>
        ///  applied to and the bonus that is be applied.  The bonus must be an integer<br/>
        ///  between 1 and 20.
        /// </summary>
        public static ItemProperty ItemPropertyBonusSavingThrowVsX(int nBonusType, int nBonus)
        {
            return NWN.Core.NWScript.ItemPropertyBonusSavingThrowVsX(nBonusType, nBonus);
        }

        /// <summary>
        ///  Returns Item property saving throw bonus to the base type (ie. will, reflex,<br/>
        ///  fortitude).  You must specify the base type constant(IP_CONST_SAVEBASETYPE_*)<br/>
        ///  to which the user gets the bonus and the bonus that he/she will get.  The<br/>
        ///  bonus must be an integer between 1 and 20.
        /// </summary>
        public static ItemProperty ItemPropertyBonusSavingThrow(int nBaseSaveType, int nBonus)
        {
            return NWN.Core.NWScript.ItemPropertyBonusSavingThrow(nBaseSaveType, nBonus);
        }

        /// <summary>
        ///  Returns Item property keen.  This means a critical threat range of 19-20 on a<br/>
        ///  weapon will be increased to 17-20 etc.
        /// </summary>
        public static ItemProperty ItemPropertyKeen()
        {
            return NWN.Core.NWScript.ItemPropertyKeen();
        }

        /// <summary>
        ///  Returns Item property light.  You must specify the intesity constant of the<br/>
        ///  light(IP_CONST_LIGHTBRIGHTNESS_*) and the color constant of the light<br/>
        ///  (IP_CONST_LIGHTCOLOR_*).
        /// </summary>
        public static ItemProperty ItemPropertyLight(int nBrightness, int nColor)
        {
            return NWN.Core.NWScript.ItemPropertyLight(nBrightness, nColor);
        }

        /// <summary>
        ///  Returns Item property Max range strength modification (ie. mighty).  You must<br/>
        ///  specify the maximum modifier for strength that is allowed on a ranged weapon.<br/>
        ///  The modifier must be a positive integer between 1 and 20.
        /// </summary>
        public static ItemProperty ItemPropertyMaxRangeStrengthMod(int nModifier)
        {
            return NWN.Core.NWScript.ItemPropertyMaxRangeStrengthMod(nModifier);
        }

        /// <summary>
        ///  Returns Item property no damage.  This means the weapon will do no damage in<br/>
        ///  combat.
        /// </summary>
        public static ItemProperty ItemPropertyNoDamage()
        {
            return NWN.Core.NWScript.ItemPropertyNoDamage();
        }

        /// <summary>
        ///  Returns Item property on hit -> do effect property.  You must specify the on<br/>
        ///  hit property constant(IP_CONST_ONHIT_*) and the save DC constant(IP_CONST_ONHIT_SAVEDC_*).<br/>
        ///  Some of the item properties require a special parameter as well.  If the<br/>
        ///  property does not require one you may leave out the last one.  The list of<br/>
        ///  the ones with 3 parameters and what they are are as follows:<br/>
        ///       ABILITYDRAIN      :nSpecial is the ability it is to drain.<br/>
        ///                          constant(IP_CONST_ABILITY_*)<br/>
        ///       BLINDNESS         :nSpecial is the duration/percentage of effecting victim.<br/>
        ///                          constant(IP_CONST_ONHIT_DURATION_*)<br/>
        ///       CONFUSION         :nSpecial is the duration/percentage of effecting victim.<br/>
        ///                          constant(IP_CONST_ONHIT_DURATION_*)<br/>
        ///       DAZE              :nSpecial is the duration/percentage of effecting victim.<br/>
        ///                          constant(IP_CONST_ONHIT_DURATION_*)<br/>
        ///       DEAFNESS          :nSpecial is the duration/percentage of effecting victim.<br/>
        ///                          constant(IP_CONST_ONHIT_DURATION_*)<br/>
        ///       DISEASE           :nSpecial is the type of desease that will effect the victim.<br/>
        ///                          constant(DISEASE_*)<br/>
        ///       DOOM              :nSpecial is the duration/percentage of effecting victim.<br/>
        ///                          constant(IP_CONST_ONHIT_DURATION_*)<br/>
        ///       FEAR              :nSpecial is the duration/percentage of effecting victim.<br/>
        ///                          constant(IP_CONST_ONHIT_DURATION_*)<br/>
        ///       HOLD              :nSpecial is the duration/percentage of effecting victim.<br/>
        ///                          constant(IP_CONST_ONHIT_DURATION_*)<br/>
        ///       ITEMPOISON        :nSpecial is the type of poison that will effect the victim.<br/>
        ///                          constant(IP_CONST_POISON_*)<br/>
        ///       SILENCE           :nSpecial is the duration/percentage of effecting victim.<br/>
        ///                          constant(IP_CONST_ONHIT_DURATION_*)<br/>
        ///       SLAYRACE          :nSpecial is the race that will be slain.<br/>
        ///                          constant(IP_CONST_RACIALTYPE_*)<br/>
        ///       SLAYALIGNMENTGROUP:nSpecial is the alignment group that will be slain(ie. chaotic).<br/>
        ///                          constant(IP_CONST_ALIGNMENTGROUP_*)<br/>
        ///       SLAYALIGNMENT     :nSpecial is the specific alignment that will be slain.<br/>
        ///                          constant(IP_CONST_ALIGNMENT_*)<br/>
        ///       SLEEP             :nSpecial is the duration/percentage of effecting victim.<br/>
        ///                          constant(IP_CONST_ONHIT_DURATION_*)<br/>
        ///       SLOW              :nSpecial is the duration/percentage of effecting victim.<br/>
        ///                          constant(IP_CONST_ONHIT_DURATION_*)<br/>
        ///       STUN              :nSpecial is the duration/percentage of effecting victim.<br/>
        ///                          constant(IP_CONST_ONHIT_DURATION_*)
        /// </summary>
        public static ItemProperty ItemPropertyOnHitProps(int nProperty, int nSaveDC, int nSpecial = 0)
        {
            return NWN.Core.NWScript.ItemPropertyOnHitProps(nProperty, nSaveDC, nSpecial);
        }

        /// <summary>
        ///  Returns Item property reduced saving throw vs. an effect or damage type.  You must<br/>
        ///  specify the constant to which the penalty applies(IP_CONST_SAVEVS_*) and the<br/>
        ///  penalty to be applied.  The penalty must be a POSITIVE integer between 1 and 20<br/>
        ///  (ie. 1 = -1).
        /// </summary>
        public static ItemProperty ItemPropertyReducedSavingThrowVsX(int nBaseSaveType, int nPenalty)
        {
            return NWN.Core.NWScript.ItemPropertyReducedSavingThrowVsX(nBaseSaveType, nPenalty);
        }

        /// <summary>
        ///  Returns Item property reduced saving to base type.  You must specify the base<br/>
        ///  type to which the penalty applies (ie. will, reflex, or fortitude) and the penalty<br/>
        ///  to be applied.  The constant for the base type starts with (IP_CONST_SAVEBASETYPE_*).<br/>
        ///  The penalty must be a POSITIVE integer between 1 and 20 (ie. 1 = -1).
        /// </summary>
        public static ItemProperty ItemPropertyReducedSavingThrow(int nBonusType, int nPenalty)
        {
            return NWN.Core.NWScript.ItemPropertyReducedSavingThrow(nBonusType, nPenalty);
        }

        /// <summary>
        ///  Returns Item property regeneration.  You must specify the regeneration amount.<br/>
        ///  The amount must be an integer between 1 and 20.
        /// </summary>
        public static ItemProperty ItemPropertyRegeneration(int nRegenAmount)
        {
            return NWN.Core.NWScript.ItemPropertyRegeneration(nRegenAmount);
        }

        /// <summary>
        ///  Returns Item property skill bonus.  You must specify the skill to which the user<br/>
        ///  will get a bonus(SKILL_*) and the amount of the bonus.  The bonus amount must<br/>
        ///  be an integer between 1 and 50.
        /// </summary>
        public static ItemProperty ItemPropertySkillBonus(int nSkill, int nBonus)
        {
            return NWN.Core.NWScript.ItemPropertySkillBonus(nSkill, nBonus);
        }

        /// <summary>
        ///  Returns Item property spell immunity vs. specific spell.  You must specify the<br/>
        ///  spell to which the user will be immune(IP_CONST_IMMUNITYSPELL_*).
        /// </summary>
        public static ItemProperty ItemPropertySpellImmunitySpecific(int nSpell)
        {
            return NWN.Core.NWScript.ItemPropertySpellImmunitySpecific(nSpell);
        }

        /// <summary>
        ///  Returns Item property spell immunity vs. spell school.  You must specify the<br/>
        ///  school to which the user will be immune(IP_CONST_SPELLSCHOOL_*).
        /// </summary>
        public static ItemProperty ItemPropertySpellImmunitySchool(int nSchool)
        {
            return NWN.Core.NWScript.ItemPropertySpellImmunitySchool(nSchool);
        }

        /// <summary>
        ///  Returns Item property Thieves tools.  You must specify the modifier you wish<br/>
        ///  the tools to have.  The modifier must be an integer between 1 and 12.
        /// </summary>
        public static ItemProperty ItemPropertyThievesTools(int nModifier)
        {
            return NWN.Core.NWScript.ItemPropertyThievesTools(nModifier);
        }

        /// <summary>
        ///  Returns Item property Attack bonus.  You must specify an attack bonus.  The bonus<br/>
        ///  must be an integer between 1 and 20.
        /// </summary>
        public static ItemProperty ItemPropertyAttackBonus(int nBonus)
        {
            return NWN.Core.NWScript.ItemPropertyAttackBonus(nBonus);
        }

        /// <summary>
        ///  Returns Item property Attack bonus vs. alignment group.  You must specify the<br/>
        ///  alignment group constant(IP_CONST_ALIGNMENTGROUP_*) and the attack bonus.  The<br/>
        ///  bonus must be an integer between 1 and 20.
        /// </summary>
        public static ItemProperty ItemPropertyAttackBonusVsAlign(int nAlignGroup, int nBonus)
        {
            return NWN.Core.NWScript.ItemPropertyAttackBonusVsAlign(nAlignGroup, nBonus);
        }

        /// <summary>
        ///  Returns Item property attack bonus vs. racial group.  You must specify the<br/>
        ///  racial group constant(IP_CONST_RACIALTYPE_*) and the attack bonus.  The bonus<br/>
        ///  must be an integer between 1 and 20.
        /// </summary>
        public static ItemProperty ItemPropertyAttackBonusVsRace(int nRace, int nBonus)
        {
            return NWN.Core.NWScript.ItemPropertyAttackBonusVsRace(nRace, nBonus);
        }

        /// <summary>
        ///  Returns Item property attack bonus vs. a specific alignment.  You must specify<br/>
        ///  the alignment you want the bonus to work against(IP_CONST_ALIGNMENT_*) and the<br/>
        ///  attack bonus.  The bonus must be an integer between 1 and 20.
        /// </summary>
        public static ItemProperty ItemPropertyAttackBonusVsSAlign(int nAlignment, int nBonus)
        {
            return NWN.Core.NWScript.ItemPropertyAttackBonusVsSAlign(nAlignment, nBonus);
        }

        /// <summary>
        ///  Returns Item property attack penalty.  You must specify the attack penalty.<br/>
        ///  The penalty must be a POSITIVE integer between 1 and 5 (ie. 1 = -1).
        /// </summary>
        public static ItemProperty ItemPropertyAttackPenalty(int nPenalty)
        {
            return NWN.Core.NWScript.ItemPropertyAttackPenalty(nPenalty);
        }

        /// <summary>
        ///  Returns Item property unlimited ammo.  If you leave the parameter field blank<br/>
        ///  it will be just a normal bolt, arrow, or bullet.  However you may specify that<br/>
        ///  you want the ammunition to do special damage (ie. +1d6 Fire, or +1 enhancement<br/>
        ///  bonus).  For this parmeter you use the constants beginning with:<br/>
        ///       (IP_CONST_UNLIMITEDAMMO_*).
        /// </summary>
        public static ItemProperty ItemPropertyUnlimitedAmmo(IPConstUnlimitedAmmoType nAmmoDamage = IPConstUnlimitedAmmoType.Basic)
        {
            return NWN.Core.NWScript.ItemPropertyUnlimitedAmmo((int)nAmmoDamage);
        }

        /// <summary>
        ///  Returns Item property limit use by alignment group.  You must specify the<br/>
        ///  alignment group(s) that you want to be able to use this item(IP_CONST_ALIGNMENTGROUP_*).
        /// </summary>
        public static ItemProperty ItemPropertyLimitUseByAlign(int nAlignGroup)
        {
            return NWN.Core.NWScript.ItemPropertyLimitUseByAlign(nAlignGroup);
        }

        /// <summary>
        ///  Returns Item property limit use by class.  You must specify the class(es) who<br/>
        ///  are able to use this item(IP_CONST_CLASS_*).
        /// </summary>
        public static ItemProperty ItemPropertyLimitUseByClass(int nClass)
        {
            return NWN.Core.NWScript.ItemPropertyLimitUseByClass(nClass);
        }

        /// <summary>
        ///  Returns Item property limit use by race.  You must specify the race(s) who are<br/>
        ///  allowed to use this item(IP_CONST_RACIALTYPE_*).
        /// </summary>
        public static ItemProperty ItemPropertyLimitUseByRace(int nRace)
        {
            return NWN.Core.NWScript.ItemPropertyLimitUseByRace(nRace);
        }

        /// <summary>
        ///  Returns Item property limit use by specific alignment.  You must specify the<br/>
        ///  alignment(s) of those allowed to use the item(IP_CONST_ALIGNMENT_*).
        /// </summary>
        public static ItemProperty ItemPropertyLimitUseBySAlign(int nAlignment)
        {
            return NWN.Core.NWScript.ItemPropertyLimitUseBySAlign(nAlignment);
        }

        /// <summary>
        ///  replace this function it does nothing.
        /// </summary>
        public static ItemProperty BadBadReplaceMeThisDoesNothing()
        {
            return NWN.Core.NWScript.BadBadReplaceMeThisDoesNothing();
        }

        /// <summary>
        ///  Returns Item property vampiric regeneration.  You must specify the amount of<br/>
        ///  regeneration.  The regen amount must be an integer between 1 and 20.
        /// </summary>
        public static ItemProperty ItemPropertyVampiricRegeneration(int nRegenAmount)
        {
            return NWN.Core.NWScript.ItemPropertyVampiricRegeneration(nRegenAmount);
        }

        /// <summary>
        ///  Returns Item property Trap.  You must specify the trap level constant<br/>
        ///  (IP_CONST_TRAPSTRENGTH_*) and the trap type constant(IP_CONST_TRAPTYPE_*).
        /// </summary>
        public static ItemProperty ItemPropertyTrap(int nTrapLevel, int nTrapType)
        {
            return NWN.Core.NWScript.ItemPropertyTrap(nTrapLevel, nTrapType);
        }

        /// <summary>
        ///  Returns Item property true seeing.
        /// </summary>
        public static ItemProperty ItemPropertyTrueSeeing()
        {
            return NWN.Core.NWScript.ItemPropertyTrueSeeing();
        }

        /// <summary>
        ///  Returns Item property Monster on hit apply effect property.  You must specify<br/>
        ///  the property that you want applied on hit.  There are some properties that<br/>
        ///  require an additional special parameter to be specified.  The others that<br/>
        ///  don't require any additional parameter you may just put in the one.  The<br/>
        ///  special cases are as follows:<br/>
        ///       ABILITYDRAIN:nSpecial is the ability to drain.<br/>
        ///                    constant(IP_CONST_ABILITY_*)<br/>
        ///       DISEASE     :nSpecial is the disease that you want applied.<br/>
        ///                    constant(DISEASE_*)<br/>
        ///       LEVELDRAIN  :nSpecial is the number of levels that you want drained.<br/>
        ///                    integer between 1 and 5.<br/>
        ///       POISON      :nSpecial is the type of poison that will effect the victim.<br/>
        ///                    constant(IP_CONST_POISON_*)<br/>
        ///       WOUNDING    :nSpecial is the amount of wounding.<br/>
        ///                    integer between 1 and 5.<br/>
        ///  NOTE: Any that do not appear in the above list do not require the second<br/>
        ///        parameter.<br/>
        ///  NOTE: These can only be applied to monster NATURAL weapons (ie. bite, claw,<br/>
        ///        gore, and slam).  IT WILL NOT WORK ON NORMAL WEAPONS.
        /// </summary>
        public static ItemProperty ItemPropertyOnMonsterHitProperties(int nProperty, int nSpecial = 0)
        {
            return NWN.Core.NWScript.ItemPropertyOnMonsterHitProperties(nProperty, nSpecial);
        }

        /// <summary>
        ///  Returns Item property turn resistance.  You must specify the resistance bonus.<br/>
        ///  The bonus must be an integer between 1 and 50.
        /// </summary>
        public static ItemProperty ItemPropertyTurnResistance(int nModifier)
        {
            return NWN.Core.NWScript.ItemPropertyTurnResistance(nModifier);
        }

        /// <summary>
        ///  Returns Item property Massive Critical.  You must specify the extra damage<br/>
        ///  constant(IP_CONST_DAMAGEBONUS_*) of the criticals.
        /// </summary>
        public static ItemProperty ItemPropertyMassiveCritical(int nDamage)
        {
            return NWN.Core.NWScript.ItemPropertyMassiveCritical(nDamage);
        }

        /// <summary>
        ///  Returns Item property free action.
        /// </summary>
        public static ItemProperty ItemPropertyFreeAction()
        {
            return NWN.Core.NWScript.ItemPropertyFreeAction();
        }

        /// <summary>
        ///  Returns Item property monster damage.  You must specify the amount of damage<br/>
        ///  the monster's attack will do(IP_CONST_MONSTERDAMAGE_*).<br/>
        ///  NOTE: These can only be applied to monster NATURAL weapons (ie. bite, claw,<br/>
        ///        gore, and slam).  IT WILL NOT WORK ON NORMAL WEAPONS.
        /// </summary>
        public static ItemProperty ItemPropertyMonsterDamage(int nDamage)
        {
            return NWN.Core.NWScript.ItemPropertyMonsterDamage(nDamage);
        }

        /// <summary>
        ///  Returns Item property immunity to spell level.  You must specify the level of<br/>
        ///  which that and below the user will be immune.  The level must be an integer<br/>
        ///  between 1 and 9.  By putting in a 3 it will mean the user is immune to all<br/>
        ///  3rd level and lower spells.
        /// </summary>
        public static ItemProperty ItemPropertyImmunityToSpellLevel(int nLevel)
        {
            return NWN.Core.NWScript.ItemPropertyImmunityToSpellLevel(nLevel);
        }

        /// <summary>
        ///  Returns Item property special walk.  If no parameters are specified it will<br/>
        ///  automatically use the zombie walk.  This will apply the special walk animation<br/>
        ///  to the user.
        /// </summary>
        public static ItemProperty ItemPropertySpecialWalk(int nWalkType = 0)
        {
            return NWN.Core.NWScript.ItemPropertySpecialWalk(nWalkType);
        }
        /// <summary>
        ///  Returns Item property healers kit.  You must specify the level of the kit.<br/>
        ///  The modifier must be an integer between 1 and 12.
        /// </summary>
        public static ItemProperty ItemPropertyHealersKit(int nModifier)
        {
            return NWN.Core.NWScript.ItemPropertyHealersKit(nModifier);
        }

        /// <summary>
        ///  Returns Item property weight increase.  You must specify the weight increase<br/>
        ///  constant(IP_CONST_WEIGHTINCREASE_*).
        /// </summary>
        public static ItemProperty ItemPropertyWeightIncrease(int nWeight)
        {
            return NWN.Core.NWScript.ItemPropertyWeightIncrease(nWeight);
        }

        /// <summary>
        ///  Returns true if 1d20 roll + skill rank is greater than or equal to difficulty<br/>
        ///  - oTarget: the creature using the skill<br/>
        ///  - nSkill: the skill being used<br/>
        ///  - nDifficulty: Difficulty class of skill
        /// </summary>
        public static bool GetIsSkillSuccessful(uint oTarget, int nSkill, int nDifficulty)
        {
            return NWN.Core.NWScript.GetIsSkillSuccessful(oTarget, nSkill, nDifficulty) == 1;
        }


        /// <summary>
        ///  Creates an effect that inhibits spells<br/>
        ///  - nPercent - percentage of failure<br/>
        ///  - nSpellSchool - the school of spells affected.
        /// </summary>
        public static Effect EffectSpellFailure(int nPercent = 100, SpellSchoolType nSpellSchool = SpellSchoolType.General)
        {
            return NWN.Core.NWScript.EffectSpellFailure(nPercent, (int)nSpellSchool);
        }

        /// <summary>
        ///  Causes the object to instantly speak a translated string.<br/>
        ///  (not an action, not blocked when uncommandable)<br/>
        ///  - nStrRef: Reference of the string in the talk table<br/>
        ///  - nTalkVolume: TALKVOLUME_*
        /// </summary>
        public static void SpeakStringByStrRef(int nStrRef, TalkVolumeType nTalkVolume = TalkVolumeType.Talk)
        {
            NWN.Core.NWScript.SpeakStringByStrRef(nStrRef, (int)nTalkVolume);
        }

        /// <summary>
        ///  Sets the given creature into cutscene mode.  This prevents the player from<br/>
        ///  using the GUI and camera controls.<br/>
        ///  - oCreature: creature in a cutscene<br/>
        ///  - nInCutscene: true to move them into cutscene, false to remove cutscene mode<br/>
        ///  - nLeftClickingEnabled: true to allow the user to interact with the game world using the left mouse button only.<br/>
        ///                          false to stop the user from interacting with the game world.<br/>
        ///  Note: SetCutsceneMode(oPlayer, true) will also make the player &apos;plot&apos; (unkillable).<br/>
        ///  SetCutsceneMode(oPlayer, false) will restore the player&apos;s plot flag to what it<br/>
        ///  was when SetCutsceneMode(oPlayer, true) was called.
        /// </summary>
        public static void SetCutsceneMode(uint oCreature, bool nInCutscene = true, bool nLeftClickingEnabled = false)
        {
            NWN.Core.NWScript.SetCutsceneMode(oCreature, nInCutscene ? 1 : 0, nLeftClickingEnabled ? 1 : 0);
        }

        /// <summary>
        ///  Gets the last player character to cancel from a cutscene.
        /// </summary>
        public static uint GetLastPCToCancelCutscene()
        {
            return NWN.Core.NWScript.GetLastPCToCancelCutscene();
        }

        /// <summary>
        ///  Gets the length of the specified wavefile, in seconds<br/>
        ///  Only works for sounds used for dialog.
        /// </summary>
        public static float GetDialogSoundLength(int nStrRef)
        {
            return NWN.Core.NWScript.GetDialogSoundLength(nStrRef);
        }

        /// <summary>
        ///  Fades the screen for the given creature/player from black to regular screen<br/>
        ///  - oCreature: creature controlled by player that should fade from black
        /// </summary>
        public static void FadeFromBlack(uint oCreature, float fSpeed = FadeSpeedType.Medium)
        {
            NWN.Core.NWScript.FadeFromBlack(oCreature, fSpeed);
        }

        /// <summary>
        ///  Fades the screen for the given creature/player from regular screen to black<br/>
        ///  - oCreature: creature controlled by player that should fade to black
        /// </summary>
        public static void FadeToBlack(uint oCreature, float fSpeed = FadeSpeedType.Medium)
        {
            NWN.Core.NWScript.FadeToBlack(oCreature, fSpeed);
        }

        /// <summary>
        ///  Removes any fading or black screen.<br/>
        ///  - oCreature: creature controlled by player that should be cleared
        /// </summary>
        public static void StopFade(uint oCreature)
        {
            NWN.Core.NWScript.StopFade(oCreature);
        }

        /// <summary>
        ///  Sets the screen to black.  Can be used in preparation for a fade-in (FadeFromBlack)<br/>
        ///  Can be cleared by either doing a FadeFromBlack, or by calling StopFade.<br/>
        ///  - oCreature: creature controlled by player that should see black screen
        /// </summary>
        public static void BlackScreen(uint oCreature)
        {
            NWN.Core.NWScript.BlackScreen(oCreature);
        }

        /// <summary>
        ///  Returns the base attach bonus for the given creature.
        /// </summary>
        public static int GetBaseAttackBonus(uint oCreature)
        {
            return NWN.Core.NWScript.GetBaseAttackBonus(oCreature);
        }

        /// <summary>
        ///  Set a creature&apos;s immortality flag.<br/>
        ///  -oCreature: creature affected<br/>
        ///  -bImmortal: true = creature is immortal and cannot be killed (but still takes damage)<br/>
        ///              false = creature is not immortal and is damaged normally.<br/>
        ///  This scripting command only works on Creature objects.
        /// </summary>
        public static void SetImmortal(uint oCreature, bool bImmortal)
        {
            NWN.Core.NWScript.SetImmortal(oCreature, bImmortal ? 1 : 0);
        }

        /// <summary>
        ///  Open&apos;s this creature&apos;s inventory panel for this player<br/>
        ///  - oCreature: creature to view<br/>
        ///  - oPlayer: the owner of this creature will see the panel pop up<br/>
        ///  * DM&apos;s can view any creature&apos;s inventory<br/>
        ///  * Players can view their own inventory, or that of their henchman, familiar or animal companion
        /// </summary>
        public static void OpenInventory(uint oCreature, uint oPlayer)
        {
            NWN.Core.NWScript.OpenInventory(oCreature, oPlayer);
        }

        /// <summary>
        ///  Stores the current camera mode and position so that it can be restored (using<br/>
        ///  RestoreCameraFacing())
        /// </summary>
        public static void StoreCameraFacing()
        {
            NWN.Core.NWScript.StoreCameraFacing();
        }

        /// <summary>
        ///  Restores the camera mode and position to what they were last time StoreCameraFacing<br/>
        ///  was called.  RestoreCameraFacing can only be called once, and must correspond to a<br/>
        ///  previous call to StoreCameraFacing.
        /// </summary>
        public static void RestoreCameraFacing()
        {
            NWN.Core.NWScript.RestoreCameraFacing();
        }

        /// <summary>
        ///  Levels up a creature using default settings.<br/>
        ///  If successfull it returns the level the creature now is, or 0 if it fails.<br/>
        ///  If you want to give them a different level (ie: Give a Fighter a level of Wizard)<br/>
        ///  you can specify that in the nClass.<br/>
        ///  However, if you specify a class to which the creature no package specified,<br/>
        ///    they will use the default package for that class for their levelup choices.<br/>
        ///    (ie: no Barbarian Savage/Wizard Divination combinations)<br/>
        ///  If you turn on bReadyAllSpells, all memorized spells will be ready to cast without resting.<br/>
        ///  if nPackage is PACKAGE_INVALID then it will use the starting package assigned to that class or just the class package
        /// </summary>
        public static int LevelUpHenchman(uint oCreature, ClassType nClass = ClassType.Invalid, bool bReadyAllSpells = false, PackageType nPackage = PackageType.Invalid)
        {
            return NWN.Core.NWScript.LevelUpHenchman(oCreature, (int)nClass, bReadyAllSpells ? 1 : 0, (int)nPackage);
        }

        /// <summary>
        ///  Sets the droppable flag on an item<br/>
        ///  - oItem: the item to change<br/>
        ///  - bDroppable: true or false, whether the item should be droppable<br/>
        ///  Droppable items will appear on a creature&apos;s remains when the creature is killed.
        /// </summary>
        public static void SetDroppableFlag(uint oItem, bool bDroppable)
        {
            NWN.Core.NWScript.SetDroppableFlag(oItem, bDroppable ? 1 : 0);
        }

        /// <summary>
        ///  Gets the weight of an item, or the total carried weight of a creature in tenths<br/>
        ///  of pounds (as per the baseitems.2da).<br/>
        ///  - oTarget: the item or creature for which the weight is needed
        /// </summary>
        public static int GetWeight(uint oTarget = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetWeight(oTarget);
        }

        /// <summary>
        ///  Gets the object that acquired the module item.  May be a creature, item, or placeable
        /// </summary>
        public static uint GetModuleItemAcquiredBy()
        {
            return NWN.Core.NWScript.GetModuleItemAcquiredBy();
        }

        /// <summary>
        ///  Get the immortal flag on a creature
        /// </summary>
        public static bool GetImmortal(uint oTarget = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetImmortal(oTarget) == 1;
        }


        /// <summary>
        ///  Does a single attack on every hostile creature within 10ft. of the attacker<br/>
        ///  and determines damage accordingly.  If the attacker has a ranged weapon<br/>
        ///  equipped, this will have no effect.<br/>
        ///  ** NOTE ** This is meant to be called inside the spell script for whirlwind<br/>
        ///  attack, it is not meant to be used to queue up a new whirlwind attack.  To do<br/>
        ///  that you need to call ActionUseFeat(FEAT_WHIRLWIND_ATTACK, oEnemy)<br/>
        ///  - bool bDisplayFeedback: true or false, whether or not feedback should be<br/>
        ///    displayed<br/>
        ///  - bool bImproved: If true, the improved version of whirlwind is used
        /// </summary>
        public static void DoWhirlwindAttack(bool bDisplayFeedback = true, bool bImproved = false)
        {
            NWN.Core.NWScript.DoWhirlwindAttack(bDisplayFeedback ? 1 : 0, bImproved ? 1 : 0);
        }

        /// <summary>
        ///  Gets a value from a 2DA file on the server and returns it as a string<br/>
        ///  avoid using this function in loops<br/>
        ///  - s2DA: the name of the 2da file, 16 chars max<br/>
        ///  - sColumn: the name of the column in the 2da<br/>
        ///  - nRow: the row in the 2da<br/>
        ///  * returns an empty string if file, row, or column not found
        /// </summary>
        public static string Get2DAString(string s2DA, string sColumn, int nRow)
        {
            return NWN.Core.NWScript.Get2DAString(s2DA, sColumn, nRow);
        }

        /// <summary>
        ///  Returns an effect of type EFFECT_TYPE_ETHEREAL which works just like EffectSanctuary<br/>
        ///  except that the observers get no saving throw
        /// </summary>
        public static Effect EffectEthereal()
        {
            return NWN.Core.NWScript.EffectEthereal();
        }

        /// <summary>
        ///  Gets the current AI Level that the creature is running at.<br/>
        ///  Returns one of the following:<br/>
        ///  AI_LEVEL_INVALID, AI_LEVEL_VERY_LOW, AI_LEVEL_LOW, AI_LEVEL_NORMAL, AI_LEVEL_HIGH, AI_LEVEL_VERY_HIGH
        /// </summary>
        public static AILevelType GetAILevel(uint oTarget = OBJECT_INVALID)
        {
            return (AILevelType)NWN.Core.NWScript.GetAILevel(oTarget);
        }

        /// <summary>
        ///  Sets the current AI Level of the creature to the value specified. Does not work on Players.<br/>
        ///  The game by default will choose an appropriate AI level for<br/>
        ///  creatures based on the circumstances that the creature is in.<br/>
        ///  Explicitly setting an AI level will over ride the game AI settings.<br/>
        ///  The new setting will last until SetAILevel is called again with the argument AI_LEVEL_DEFAULT.<br/>
        ///  AI_LEVEL_DEFAULT  - Default setting. The game will take over seting the appropriate AI level when required.<br/>
        ///  AI_LEVEL_VERY_LOW - Very Low priority, very stupid, but low CPU usage for AI. Typically used when no players are in the area.<br/>
        ///  AI_LEVEL_LOW      - Low priority, mildly stupid, but slightly more CPU usage for AI. Typically used when not in combat, but a player is in the area.<br/>
        ///  AI_LEVEL_NORMAL   - Normal priority, average AI, but more CPU usage required for AI. Typically used when creature is in combat.<br/>
        ///  AI_LEVEL_HIGH     - High priority, smartest AI, but extremely high CPU usage required for AI. Avoid using this. It is most likely only ever needed for cutscenes.
        /// </summary>
        public static void SetAILevel(uint oTarget, int nAILevel)
        {
            NWN.Core.NWScript.SetAILevel(oTarget, nAILevel);
        }

        /// <summary>
        ///  This will return true if the creature running the script is a familiar currently<br/>
        ///  possessed by his master.<br/>
        ///  returns false if not or if the creature object is invalid
        /// </summary>
        public static bool GetIsPossessedFamiliar(uint oCreature)
        {
            return NWN.Core.NWScript.GetIsPossessedFamiliar(oCreature) == 1;
        }

        /// <summary>
        ///  This will cause a Player Creature to unpossess his/her familiar.  It will work if run<br/>
        ///  on the player creature or the possessed familiar.  It does not work in conjunction with<br/>
        ///  any DM possession.
        /// </summary>
        public static void UnpossessFamiliar(uint oCreature)
        {
            NWN.Core.NWScript.UnpossessFamiliar(oCreature);
        }

        /// <summary>
        ///  This will return true if the area is flagged as either interior or underground.
        /// </summary>
        public static bool GetIsAreaInterior(uint oArea = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetIsAreaInterior(oArea) == 1;
        }

        /// <summary>
        ///  Send a server message (szMessage) to the oPlayer.
        /// </summary>
        public static void SendMessageToPCByStrRef(uint oPlayer, int nStrRef)
        {
            NWN.Core.NWScript.SendMessageToPCByStrRef(oPlayer, nStrRef);
        }

        /// <summary>
        ///  Increment the remaining uses per day for this creature by one.<br/>
        ///  Total number of feats per day can not exceed the maximum.<br/>
        ///  - oCreature: creature to modify<br/>
        ///  - nFeat: constant FEAT_*
        /// </summary>
        public static void IncrementRemainingFeatUses(uint oCreature, int nFeat)
        {
            NWN.Core.NWScript.IncrementRemainingFeatUses(oCreature, nFeat);
        }

        /// <summary>
        ///  Force the character of the player specified to be exported to its respective directory<br/>
        ///  i.e. LocalVault/ServerVault/ etc.
        /// </summary>
        public static void ExportSingleCharacter(uint oPlayer)
        {
            NWN.Core.NWScript.ExportSingleCharacter(oPlayer);
        }

        /// <summary>
        ///  This will play a sound that is associated with a stringRef, it will be a mono sound from the location of the object running the command.<br/>
        ///  if nRunAsAction is off then the sound is forced to play intantly.
        /// </summary>
        public static void PlaySoundByStrRef(int nStrRef, bool nRunAsAction = true)
        {
            NWN.Core.NWScript.PlaySoundByStrRef(nStrRef, nRunAsAction ? 1 : 0);
        }

        /// <summary>
        ///  Set the name of oCreature&apos;s sub race to sSubRace.
        /// </summary>
        public static void SetSubRace(uint oCreature, string sSubRace)
        {
            NWN.Core.NWScript.SetSubRace(oCreature, sSubRace);
        }

        /// <summary>
        ///  Set the name of oCreature&apos;s Deity to sDeity.
        /// </summary>
        public static void SetDeity(uint oCreature, string sDeity)
        {
            NWN.Core.NWScript.SetDeity(oCreature, sDeity);
        }

        /// <summary>
        ///  Returns true if the creature oCreature is currently possessed by a DM character.<br/>
        ///  Returns false otherwise.<br/>
        ///  Note: GetIsDMPossessed() will return false if oCreature is the DM character.<br/>
        ///  To determine if oCreature is a DM character use GetIsDM()
        /// </summary>
        public static bool GetIsDMPossessed(uint oCreature)
        {
            return NWN.Core.NWScript.GetIsDMPossessed(oCreature) == 1;
        }

        /// <summary>
        ///  Gets the current weather conditions for the area oArea.<br/>
        ///    Returns: WEATHER_CLEAR, WEATHER_RAIN, WEATHER_SNOW, WEATHER_INVALID<br/>
        ///    Note: If called on an Interior area, this will always return WEATHER_CLEAR.
        /// </summary>
        public static WeatherType GetWeather(uint oArea)
        {
            return (WeatherType)NWN.Core.NWScript.GetWeather(oArea);
        }

        /// <summary>
        ///  Returns AREA_NATURAL if the area oArea is natural, AREA_ARTIFICIAL otherwise.<br/>
        ///  Returns AREA_INVALID, on an error.
        /// </summary>
        public static AreaType GetIsAreaNatural(uint oArea)
        {
            return (AreaType)NWN.Core.NWScript.GetIsAreaNatural(oArea);
        }

        /// <summary>
        ///  Returns AREA_ABOVEGROUND if the area oArea is above ground, AREA_UNDERGROUND otherwise.<br/>
        ///  Returns AREA_INVALID, on an error.
        /// </summary>
        public static AreaType GetIsAreaAboveGround(uint oArea)
        {
            return (AreaType)NWN.Core.NWScript.GetIsAreaAboveGround(oArea);
        }

        /// <summary>
        ///  Use this to get the item last equipped by a player character in OnPlayerEquipItem..
        /// </summary>
        public static uint GetPCItemLastEquipped()
        {
            return NWN.Core.NWScript.GetPCItemLastEquipped();
        }

        /// <summary>
        ///  Use this to get the player character who last equipped an item in OnPlayerEquipItem..
        /// </summary>
        public static uint GetPCItemLastEquippedBy()
        {
            return NWN.Core.NWScript.GetPCItemLastEquippedBy();
        }

        /// <summary>
        ///  Use this to get the item last unequipped by a player character in OnPlayerEquipItem..
        /// </summary>
        public static uint GetPCItemLastUnequipped()
        {
            return NWN.Core.NWScript.GetPCItemLastUnequipped();
        }

        /// <summary>
        ///  Use this to get the player character who last unequipped an item in OnPlayerUnEquipItem..
        /// </summary>
        public static uint GetPCItemLastUnequippedBy()
        {
            return NWN.Core.NWScript.GetPCItemLastUnequippedBy();
        }

        /// <summary>
        ///  Creates a new copy of an item, while making a single change to the appearance of the item.<br/>
        ///  Helmet models and simple items ignore iIndex.<br/>
        ///  iType                            iIndex                              iNewValue<br/>
        ///  ITEM_APPR_TYPE_SIMPLE_MODEL      [Ignored]                           Model #<br/>
        ///  ITEM_APPR_TYPE_WEAPON_COLOR      ITEM_APPR_WEAPON_COLOR_*            1-4<br/>
        ///  ITEM_APPR_TYPE_WEAPON_MODEL      ITEM_APPR_WEAPON_MODEL_*            Model #<br/>
        ///  ITEM_APPR_TYPE_ARMOR_MODEL       ITEM_APPR_ARMOR_MODEL_*             Model #<br/>
        ///  ITEM_APPR_TYPE_ARMOR_COLOR       ITEM_APPR_ARMOR_COLOR_* [0]         0-175 [1]<br/>
        /// <br/>
        ///  [0] Alternatively, where ITEM_APPR_TYPE_ARMOR_COLOR is specified, if per-part coloring is<br/>
        ///  desired, the following equation can be used for nIndex to achieve that:<br/>
        /// <br/>
        ///    ITEM_APPR_ARMOR_NUM_COLORS + (ITEM_APPR_ARMOR_MODEL_ * ITEM_APPR_ARMOR_NUM_COLORS) + ITEM_APPR_ARMOR_COLOR_<br/>
        /// <br/>
        ///  For example, to change the CLOTH1 channel of the torso, nIndex would be:<br/>
        /// <br/>
        ///    6 + (7 * 6) + 2 = 50<br/>
        /// <br/>
        ///  [1] When specifying per-part coloring, the value 255 is allowed and corresponds with the logical<br/>
        ///  function &apos;clear colour override&apos;, which clears the per-part override for that part.
        /// </summary>
        public static uint CopyItemAndModify(uint oItem, int nType, int nIndex, int nNewValue, bool bCopyVars = false)
        {
            return NWN.Core.NWScript.CopyItemAndModify(oItem, nType, nIndex, nNewValue, bCopyVars ? 1 : 0);
        }

        /// <summary>
        ///  Queries the current value of the appearance settings on an item. The parameters are<br/>
        ///  identical to those of CopyItemAndModify().
        /// </summary>
        public static ItemAppearanceType GetItemAppearance(uint oItem, ItemAppearanceType nType, int nIndex)
        {
            return (ItemAppearanceType)NWN.Core.NWScript.GetItemAppearance(oItem, (int)nType, nIndex);
        }

        /// <summary>
        ///  Creates an item property that (when applied to a weapon item) causes a spell to be cast<br/>
        ///  when a successful strike is made, or (when applied to armor) is struck by an opponent.<br/>
        ///  - nSpell uses the IP_CONST_ONHIT_CASTSPELL_* constants
        /// </summary>
        public static ItemProperty ItemPropertyOnHitCastSpell(int nSpell, int nLevel)
        {
            return NWN.Core.NWScript.ItemPropertyOnHitCastSpell(nSpell, nLevel);
        }

        /// <summary>
        ///  Returns the SubType number of the item property. See the 2DA files for value definitions.
        /// </summary>
        public static int GetItemPropertySubType(ItemProperty iProperty)
        {
            return NWN.Core.NWScript.GetItemPropertySubType(iProperty);
        }

        /// <summary>
        ///  Gets the status of ACTION_MODE_* modes on a creature.
        /// </summary>
        public static bool GetActionMode(uint oCreature, ActionModeType nMode)
        {
            return NWN.Core.NWScript.GetActionMode(oCreature, (int)nMode) == 1;
        }

        /// <summary>
        ///  Sets the status of modes ACTION_MODE_* on a creature.
        /// </summary>
        public static void SetActionMode(uint oCreature, int nMode, int nStatus)
        {
            NWN.Core.NWScript.SetActionMode(oCreature, nMode, nStatus);
        }

        /// <summary>
        ///  Returns the current arcane spell failure factor of a creature
        /// </summary>
        public static int GetArcaneSpellFailure(uint oCreature)
        {
            return NWN.Core.NWScript.GetArcaneSpellFailure(oCreature);
        }

        /// <summary>
        ///  Makes a player examine the object oExamine. This causes the examination<br/>
        ///  pop-up box to appear for the object specified.
        /// </summary>
        public static void ActionExamine(uint oExamine)
        {
            NWN.Core.NWScript.ActionExamine(oExamine);
        }

        /// <summary>
        ///  Creates a visual effect (ITEM_VISUAL_*) that may be applied to<br/>
        ///  melee weapons only.
        /// </summary>
        public static ItemProperty ItemPropertyVisualEffect(int nEffect)
        {
            return NWN.Core.NWScript.ItemPropertyVisualEffect(nEffect);
        }

        /// <summary>
        ///  Sets the lootable state of a *living* NPC creature.<br/>
        ///  This function will *not* work on players or dead creatures.
        /// </summary>
        public static void SetLootable(uint oCreature, bool bLootable)
        {
            NWN.Core.NWScript.SetLootable(oCreature, bLootable ? 1 : 0);
        }

        /// <summary>
        ///  Returns the lootable state of a creature.
        /// </summary>
        public static bool GetLootable(uint oCreature)
        {
            return NWN.Core.NWScript.GetLootable(oCreature) == 1;
        }

        /// <summary>
        ///  Returns the current movement rate factor<br/>
        ///  of the cutscene &apos;camera man&apos;.<br/>
        ///  NOTE: This will be a value between 0.1, 2.0 (10%-200%)
        /// </summary>
        public static float GetCutsceneCameraMoveRate(uint oCreature)
        {
            return NWN.Core.NWScript.GetCutsceneCameraMoveRate(oCreature);
        }

        /// <summary>
        ///  Sets the current movement rate factor for the cutscene<br/>
        ///  camera man.<br/>
        ///  NOTE: You can only set values between 0.1, 2.0 (10%-200%)
        /// </summary>
        public static void SetCutsceneCameraMoveRate(uint oCreature, float fRate)
        {
            NWN.Core.NWScript.SetCutsceneCameraMoveRate(oCreature, fRate);
        }

        /// <summary>
        ///  Returns true if the item is cursed and cannot be dropped
        /// </summary>
        public static bool GetItemCursedFlag(uint oItem)
        {
            return NWN.Core.NWScript.GetItemCursedFlag(oItem) == 1;
        }

        /// <summary>
        ///  When cursed, items cannot be dropped
        /// </summary>
        public static void SetItemCursedFlag(uint oItem, bool nCursed)
        {
            NWN.Core.NWScript.SetItemCursedFlag(oItem, nCursed ? 1 : 0);
        }

        /// <summary>
        ///  Sets the maximum number of henchmen
        /// </summary>
        public static void SetMaxHenchmen(int nNumHenchmen)
        {
            NWN.Core.NWScript.SetMaxHenchmen(nNumHenchmen);
        }

        /// <summary>
        ///  Gets the maximum number of henchmen
        /// </summary>
        public static int GetMaxHenchmen()
        {
            return NWN.Core.NWScript.GetMaxHenchmen();
        }

        /// <summary>
        ///  Returns the associate type of the specified creature.<br/>
        ///  - Returns ASSOCIATE_TYPE_NONE if the creature is not the associate of anyone.
        /// </summary>
        public static AssociateType GetAssociateType(uint oAssociate)
        {
            return (AssociateType)NWN.Core.NWScript.GetAssociateType(oAssociate);
        }

        /// <summary>
        ///  Returns the spell resistance of the specified creature.<br/>
        ///  - Returns 0 if the creature has no spell resistance or an invalid<br/>
        ///    creature is passed in.
        /// </summary>
        public static int GetSpellResistance(uint oCreature)
        {
            return NWN.Core.NWScript.GetSpellResistance(oCreature);
        }

        /// <summary>
        ///  Changes the current Day/Night cycle for this player to night<br/>
        ///  - oPlayer: which player to change the lighting for<br/>
        ///  - fTransitionTime: how long the transition should take
        /// </summary>
        public static void DayToNight(uint oPlayer, float fTransitionTime = 0.0f)
        {
            NWN.Core.NWScript.DayToNight(oPlayer, fTransitionTime);
        }
        /// <summary>
        ///  Changes the current Day/Night cycle for this player to daylight<br/>
        ///  - oPlayer: which player to change the lighting for<br/>
        ///  - fTransitionTime: how long the transition should take
        /// </summary>
        public static void NightToDay(uint oPlayer, float fTransitionTime = 0.0f)
        {
            NWN.Core.NWScript.NightToDay(oPlayer, fTransitionTime);
        }

        /// <summary>
        ///  Returns whether or not there is a direct line of sight<br/>
        ///  between the two objects. (Not blocked by any geometry).<br/>
        /// <br/>
        ///  PLEASE NOTE: This is an expensive function and may<br/>
        ///               degrade performance if used frequently.
        /// </summary>
        public static bool LineOfSightObject(uint oSource, uint oTarget)
        {
            return NWN.Core.NWScript.LineOfSightObject(oSource, oTarget) == 1;
        }

        /// <summary>
        ///  Returns whether or not there is a direct line of sight<br/>
        ///  between the two vectors. (Not blocked by any geometry).<br/>
        /// <br/>
        ///  This function must be run on a valid object in the area<br/>
        ///  it will not work on the module or area.<br/>
        /// <br/>
        ///  PLEASE NOTE: This is an expensive function and may<br/>
        ///               degrade performance if used frequently.
        /// </summary>
        public static bool LineOfSightVector(Vector3 vSource, Vector3 vTarget)
        {
            return NWN.Core.NWScript.LineOfSightVector(vSource, vTarget) == 1;
        }

        /// <summary>
        ///  Returns the class that the spellcaster cast the<br/>
        ///  spell as.<br/>
        ///  - Returns CLASS_TYPE_INVALID if the caster has<br/>
        ///    no valid class (placeables, etc...)<br/>
        ///  If used in an Area of Effect script it will return the creators spellcasting class.
        /// </summary>
        public static ClassType GetLastSpellCastClass()
        {
            return (ClassType)NWN.Core.NWScript.GetLastSpellCastClass();
        }

        /// <summary>
        ///  Sets the number of base attacks each round for the specified creature (PC or NPC).<br/>
        ///  If set on a PC it will not be shown on their character sheet, but will save to BIC/savegame.<br/>
        ///  - nBaseAttackBonus - Number of base attacks per round, 1 to 6
        /// </summary>
        public static void SetBaseAttackBonus(int nBaseAttackBonus, uint oCreature = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetBaseAttackBonus(nBaseAttackBonus, oCreature);
        }

        /// <summary>
        ///  Restores the number of base attacks back to its<br/>
        ///  original state.
        /// </summary>
        public static void RestoreBaseAttackBonus(uint oCreature = OBJECT_INVALID)
        {
            NWN.Core.NWScript.RestoreBaseAttackBonus(oCreature);
        }

        /// <summary>
        ///  Creates a cutscene ghost effect, this will allow creatures<br/>
        ///  to pathfind through other creatures without bumping into them<br/>
        ///  for the duration of the effect.
        /// </summary>
        public static Effect EffectCutsceneGhost()
        {
            return NWN.Core.NWScript.EffectCutsceneGhost();
        }

        /// <summary>
        ///  Creates an item property that offsets the effect on arcane spell failure<br/>
        ///  that a particular item has. Parameters come from the ITEM_PROP_ASF_* group.
        /// </summary>
        public static ItemProperty ItemPropertyArcaneSpellFailure(int nModLevel)
        {
            return NWN.Core.NWScript.ItemPropertyArcaneSpellFailure(nModLevel);
        }

        /// <summary>
        ///  Returns the amount of gold a store currently has. -1 indicates it is not using gold.<br/>
        ///  -2 indicates the store could not be located.
        /// </summary>
        public static int GetStoreGold(uint oidStore)
        {
            return NWN.Core.NWScript.GetStoreGold(oidStore);
        }

        /// <summary>
        ///  Sets the amount of gold a store has. -1 means the store does not use gold.
        /// </summary>
        public static void SetStoreGold(uint oidStore, int nGold)
        {
            NWN.Core.NWScript.SetStoreGold(oidStore, nGold);
        }

        /// <summary>
        ///  Gets the maximum amount a store will pay for any item. -1 means price unlimited.<br/>
        ///  -2 indicates the store could not be located.
        /// </summary>
        public static int GetStoreMaxBuyPrice(uint oidStore)
        {
            return NWN.Core.NWScript.GetStoreMaxBuyPrice(oidStore);
        }

        /// <summary>
        ///  Sets the maximum amount a store will pay for any item. -1 means price unlimited.
        /// </summary>
        public static void SetStoreMaxBuyPrice(uint oidStore, int nMaxBuy)
        {
            NWN.Core.NWScript.SetStoreMaxBuyPrice(oidStore, nMaxBuy);
        }

        /// <summary>
        ///  Gets the amount a store charges for identifying an item. Default is 100. -1 means<br/>
        ///  the store will not identify items.<br/>
        ///  -2 indicates the store could not be located.
        /// </summary>
        public static int GetStoreIdentifyCost(uint oidStore)
        {
            return NWN.Core.NWScript.GetStoreIdentifyCost(oidStore);
        }

        /// <summary>
        ///  Sets the amount a store charges for identifying an item. Default is 100. -1 means<br/>
        ///  the store will not identify items.
        /// </summary>
        public static void SetStoreIdentifyCost(uint oidStore, int nCost)
        {
            NWN.Core.NWScript.SetStoreIdentifyCost(oidStore, nCost);
        }

        /// <summary>
        ///  Sets the creature&apos;s appearance type to the value specified (uses the APPEARANCE_TYPE_XXX constants)
        /// </summary>
        public static void SetCreatureAppearanceType(uint oCreature, AppearanceType nAppearanceType)
        {
            NWN.Core.NWScript.SetCreatureAppearanceType(oCreature, (int)nAppearanceType);
        }

        /// <summary>
        ///  Returns the default package selected for this creature to level up with<br/>
        ///  - returns PACKAGE_INVALID if error occurs
        /// </summary>
        public static PackageType GetCreatureStartingPackage(uint oCreature)
        {
            return (PackageType)NWN.Core.NWScript.GetCreatureStartingPackage(oCreature);
        }

        /// <summary>
        ///  Returns an effect that when applied will paralyze the target&apos;s legs, rendering<br/>
        ///  them unable to walk but otherwise unpenalized. This effect cannot be resisted.
        /// </summary>
        public static Effect EffectCutsceneImmobilize()
        {
            return NWN.Core.NWScript.EffectCutsceneImmobilize();
        }

        /// <summary>
        ///  Is this creature in the given subarea? (trigger, area of effect object, etc..)<br/>
        ///  This function will tell you if the creature has triggered an onEnter event,<br/>
        ///  not if it is physically within the space of the subarea
        /// </summary>
        public static bool GetIsInSubArea(uint oCreature, uint oSubArea = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetIsInSubArea(oCreature, oSubArea) == 1;
        }

        /// <summary>
        ///  Returns the Cost Table number of the item property. See the 2DA files for value definitions.
        /// </summary>
        public static int GetItemPropertyCostTable(ItemProperty iProp)
        {
            return NWN.Core.NWScript.GetItemPropertyCostTable(iProp);
        }

        /// <summary>
        ///  Returns the Cost Table value (index of the cost table) of the item property.<br/>
        ///  See the 2DA files for value definitions.
        /// </summary>
        public static int GetItemPropertyCostTableValue(ItemProperty iProp)
        {
            return NWN.Core.NWScript.GetItemPropertyCostTableValue(iProp);
        }

        /// <summary>
        ///  Returns the Param1 number of the item property. See the 2DA files for value definitions.
        /// </summary>
        public static int GetItemPropertyParam1(ItemProperty iProp)
        {
            return NWN.Core.NWScript.GetItemPropertyParam1(iProp);
        }

        /// <summary>
        ///  Returns the Param1 value of the item property. See the 2DA files for value definitions.
        /// </summary>
        public static int GetItemPropertyParam1Value(ItemProperty iProp)
        {
            return NWN.Core.NWScript.GetItemPropertyParam1Value(iProp);
        }

        /// <summary>
        ///  Is this creature able to be disarmed? (checks disarm flag on creature, and if<br/>
        ///  the creature actually has a weapon equipped in their right hand that is droppable)
        /// </summary>
        public static bool GetIsCreatureDisarmable(uint oCreature)
        {
            return NWN.Core.NWScript.GetIsCreatureDisarmable(oCreature) == 1;
        }


        /// <summary>
        ///  Sets whether this item is &apos;stolen&apos; or not
        /// </summary>
        public static void SetStolenFlag(uint oItem, bool nStolenFlag)
        {
            NWN.Core.NWScript.SetStolenFlag(oItem, nStolenFlag ? 1 : 0);
        }

        /// <summary>
        ///  Instantly gives this creature the benefits of a rest (restored hitpoints, spells, feats, etc..)
        /// </summary>
        public static void ForceRest(uint oCreature)
        {
            NWN.Core.NWScript.ForceRest(oCreature);
        }

        /// <summary>
        ///  Forces this player&apos;s camera to be set to this height. Setting this value to zero will<br/>
        ///  restore the camera to the racial default height.
        /// </summary>
        public static void SetCameraHeight(uint oPlayer, float fHeight = 0.0f)
        {
            NWN.Core.NWScript.SetCameraHeight(oPlayer, fHeight);
        }

        /// <summary>
        ///  Changes the sky that is displayed in the specified area.<br/>
        ///  nSkyBox = SKYBOX_* constants (associated with skyboxes.2da)<br/>
        ///  If no valid area (or object) is specified, it uses the area of caller.<br/>
        ///  If an object other than an area is specified, will use the area that the object is currently in.
        /// </summary>
        public static void SetSkyBox(int nSkyBox, uint oArea = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetSkyBox(nSkyBox, oArea);
        }

        /// <summary>
        ///  Returns the creature&apos;s currently set PhenoType (body type).
        /// </summary>
        public static Phenotype GetPhenoType(uint oCreature)
        {
            return (Phenotype)NWN.Core.NWScript.GetPhenoType(oCreature);
        }

        /// <summary>
        ///  Sets the creature&apos;s PhenoType (body type) to the type specified.<br/>
        ///  nPhenoType = PHENOTYPE_NORMAL<br/>
        ///  nPhenoType = PHENOTYPE_BIG<br/>
        ///  nPhenoType = PHENOTYPE_CUSTOM* - The custom PhenoTypes should only ever<br/>
        ///  be used if you have specifically created your own custom content that<br/>
        ///  requires the use of a new PhenoType and you have specified the appropriate<br/>
        ///  custom PhenoType in your custom content.<br/>
        ///  SetPhenoType will only work on part based creature (i.e. the starting<br/>
        ///  default playable races).
        /// </summary>
        public static void SetPhenoType(Phenotype nPhenoType, uint oCreature = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetPhenoType((int)nPhenoType, oCreature);
        }

        /// <summary>
        ///  Sets the fog color in the area specified.<br/>
        ///  nFogType = FOG_TYPE_* specifies whether the Sun, Moon, or both fog types are set.<br/>
        ///  nFogColor = FOG_COLOR_* specifies the color the fog is being set to.<br/>
        ///  The fog color can also be represented as a hex RGB number if specific color shades<br/>
        ///  are desired.<br/>
        ///  The format of a hex specified color would be 0xFFEEDD where<br/>
        ///  FF would represent the amount of red in the color<br/>
        ///  EE would represent the amount of green in the color<br/>
        ///  DD would represent the amount of blue in the color.<br/>
        ///  If no valid area (or object) is specified, it uses the area of caller.<br/>
        ///  If an object other than an area is specified, will use the area that the object is currently in.<br/>
        ///  If fFadeTime is above 0.0, it will fade to the new color in the amount of seconds specified.
        /// </summary>
        public static void SetFogColor(FogType nFogType, int nFogColor, uint oArea = OBJECT_INVALID, float fFadeTime = 0.0f)
        {
            NWN.Core.NWScript.SetFogColor((int)nFogType, nFogColor, oArea, fFadeTime);
        }

        /// <summary>
        ///  Gets the current cutscene state of the player specified by oCreature.<br/>
        ///  Returns true if the player is in cutscene mode.<br/>
        ///  Returns false if the player is not in cutscene mode, or on an error<br/>
        ///  (such as specifying a non creature object).
        /// </summary>
        public static bool GetCutsceneMode(uint oCreature = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetCutsceneMode(oCreature) == 1;
        }

        /// <summary>
        ///  Gets the skybox that is currently displayed in the specified area.<br/>
        ///  Returns:<br/>
        ///      SKYBOX_* constant<br/>
        ///  If no valid area (or object) is specified, it uses the area of caller.<br/>
        ///  If an object other than an area is specified, will use the area that the object is currently in.
        /// </summary>
        public static SkyboxType GetSkyBox(uint oArea = OBJECT_INVALID)
        {
            return (SkyboxType)NWN.Core.NWScript.GetSkyBox(oArea);
        }

        /// <summary>
        ///  Gets the fog color in the area specified.<br/>
        ///  nFogType specifies whether the Sun, or Moon fog type is returned.<br/>
        ///     Valid values for nFogType are FOG_TYPE_SUN or FOG_TYPE_MOON.<br/>
        ///  If no valid area (or object) is specified, it uses the area of caller.<br/>
        ///  If an object other than an area is specified, will use the area that the object is currently in.
        /// </summary>
        public static FogColorType GetFogColor(FogType nFogType, uint oArea = OBJECT_INVALID)
        {
            return (FogColorType)NWN.Core.NWScript.GetFogColor((int)nFogType, oArea);
        }

        /// <summary>
        ///  Sets the fog amount in the area specified.<br/>
        ///  nFogType = FOG_TYPE_* specifies whether the Sun, Moon, or both fog types are set.<br/>
        ///  nFogAmount = specifies the density that the fog is being set to.<br/>
        ///  If no valid area (or object) is specified, it uses the area of caller.<br/>
        ///  If an object other than an area is specified, will use the area that the object is currently in.
        /// </summary>
        public static void SetFogAmount(FogType nFogType, int nFogAmount, uint oArea = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetFogAmount((int)nFogType, nFogAmount, oArea);
        }

        /// <summary>
        ///  Gets the fog amount in the area specified.<br/>
        ///  nFogType = nFogType specifies whether the Sun, or Moon fog type is returned.<br/>
        ///     Valid values for nFogType are FOG_TYPE_SUN or FOG_TYPE_MOON.<br/>
        ///  If no valid area (or object) is specified, it uses the area of caller.<br/>
        ///  If an object other than an area is specified, will use the area that the object is currently in.
        /// </summary>
        public static int GetFogAmount(FogType nFogType, uint oArea = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetFogAmount((int)nFogType, oArea);
        }

        /// <summary>
        ///  Returns true if the item CAN be pickpocketed
        /// </summary>
        public static bool GetPickpocketableFlag(uint oItem)
        {
            return NWN.Core.NWScript.GetPickpocketableFlag(oItem) == 1;
        }

        /// <summary>
        ///  Sets the Pickpocketable flag on an item<br/>
        ///  - oItem: the item to change<br/>
        ///  - bPickpocketable: true or false, whether the item can be pickpocketed.
        /// </summary>
        public static void SetPickpocketableFlag(uint oItem, bool bPickpocketable)
        {
            NWN.Core.NWScript.SetPickpocketableFlag(oItem, bPickpocketable ? 1 : 0);
        }

        /// <summary>
        ///  Returns the footstep type of the creature specified.<br/>
        ///  The footstep type determines what the creature&apos;s footsteps sound<br/>
        ///  like when ever they take a step.<br/>
        ///  returns FOOTSTEP_TYPE_INVALID if used on a non-creature object, or if<br/>
        ///  used on creature that has no footstep sounds by default (e.g. Will-O&apos;-Wisp).
        /// </summary>
        public static FootstepType GetFootstepType(uint oCreature = OBJECT_INVALID)
        {
            return (FootstepType)NWN.Core.NWScript.GetFootstepType(oCreature);
        }

        /// <summary>
        ///  Sets the footstep type of the creature specified.<br/>
        ///  Changing a creature&apos;s footstep type will change the sound that<br/>
        ///  its feet make when ever the creature makes takes a step.<br/>
        ///  By default a creature&apos;s footsteps are determined by the appearance<br/>
        ///  type of the creature. SetFootstepType() allows you to make a<br/>
        ///  creature use a different footstep type than it would use by default<br/>
        ///  for its given appearance.<br/>
        ///  - nFootstepType (FOOTSTEP_TYPE_*):<br/>
        ///       FOOTSTEP_TYPE_NORMAL<br/>
        ///       FOOTSTEP_TYPE_LARGE<br/>
        ///       FOOTSTEP_TYPE_DRAGON<br/>
        ///       FOOTSTEP_TYPE_SOFT<br/>
        ///       FOOTSTEP_TYPE_HOOF<br/>
        ///       FOOTSTEP_TYPE_HOOF_LARGE<br/>
        ///       FOOTSTEP_TYPE_BEETLE<br/>
        ///       FOOTSTEP_TYPE_SPIDER<br/>
        ///       FOOTSTEP_TYPE_SKELETON<br/>
        ///       FOOTSTEP_TYPE_LEATHER_WING<br/>
        ///       FOOTSTEP_TYPE_FEATHER_WING<br/>
        ///       FOOTSTEP_TYPE_DEFAULT - Makes the creature use its original default footstep sounds.<br/>
        ///       FOOTSTEP_TYPE_NONE<br/>
        ///  - oCreature: the creature to change the footstep sound for.
        /// </summary>
        public static void SetFootstepType(FootstepType nFootstepType, uint oCreature = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetFootstepType((int)nFootstepType, oCreature);
        }

        /// <summary>
        ///  Returns the Wing type of the creature specified.<br/>
        ///       CREATURE_WING_TYPE_NONE<br/>
        ///       CREATURE_WING_TYPE_DEMON<br/>
        ///       CREATURE_WING_TYPE_ANGEL<br/>
        ///       CREATURE_WING_TYPE_BAT<br/>
        ///       CREATURE_WING_TYPE_DRAGON<br/>
        ///       CREATURE_WING_TYPE_BUTTERFLY<br/>
        ///       CREATURE_WING_TYPE_BIRD<br/>
        ///  returns CREATURE_WING_TYPE_NONE if used on a non-creature object,<br/>
        ///  if the creature has no wings, or if the creature can not have its<br/>
        ///  wing type changed in the toolset.
        /// </summary>
        public static CreatureWingType GetCreatureWingType(uint oCreature = OBJECT_INVALID)
        {
            return (CreatureWingType)NWN.Core.NWScript.GetCreatureWingType(oCreature);
        }

        /// <summary>
        ///  Sets the Wing type of the creature specified.<br/>
        ///  - nWingType (CREATURE_WING_TYPE_*)<br/>
        ///       CREATURE_WING_TYPE_NONE<br/>
        ///       CREATURE_WING_TYPE_DEMON<br/>
        ///       CREATURE_WING_TYPE_ANGEL<br/>
        ///       CREATURE_WING_TYPE_BAT<br/>
        ///       CREATURE_WING_TYPE_DRAGON<br/>
        ///       CREATURE_WING_TYPE_BUTTERFLY<br/>
        ///       CREATURE_WING_TYPE_BIRD<br/>
        ///  - oCreature: the creature to change the wing type for.<br/>
        ///  Note: Only two creature model types will support wings.<br/>
        ///  The MODELTYPE for the part based (playable) races &apos;P&apos;<br/>
        ///  and MODELTYPE &apos;W&apos; in the appearance.2da
        /// </summary>
        public static void SetCreatureWingType(CreatureWingType nWingType, uint oCreature = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetCreatureWingType((int)nWingType, oCreature);
        }

        /// <summary>
        ///  Returns the model number being used for the body part and creature specified<br/>
        ///  The model number returned is for the body part when the creature is not wearing<br/>
        ///  armor (i.e. whether or not the creature is wearing armor does not affect<br/>
        ///  the return value).<br/>
        ///  Note: Only works on part based creatures, which is typically restricted to<br/>
        ///  the playable races (unless some new part based custom content has been<br/>
        ///  added to the module).<br/>
        /// <br/>
        ///  returns CREATURE_PART_INVALID if used on a non-creature object,<br/>
        ///  or if the creature does not use a part based model.<br/>
        /// <br/>
        ///  - nPart (CREATURE_PART_*)<br/>
        ///       CREATURE_PART_RIGHT_FOOT<br/>
        ///       CREATURE_PART_LEFT_FOOT<br/>
        ///       CREATURE_PART_RIGHT_SHIN<br/>
        ///       CREATURE_PART_LEFT_SHIN<br/>
        ///       CREATURE_PART_RIGHT_THIGH<br/>
        ///       CREATURE_PART_LEFT_THIGH<br/>
        ///       CREATURE_PART_PELVIS<br/>
        ///       CREATURE_PART_TORSO<br/>
        ///       CREATURE_PART_BELT<br/>
        ///       CREATURE_PART_NECK<br/>
        ///       CREATURE_PART_RIGHT_FOREARM<br/>
        ///       CREATURE_PART_LEFT_FOREARM<br/>
        ///       CREATURE_PART_RIGHT_BICEP<br/>
        ///       CREATURE_PART_LEFT_BICEP<br/>
        ///       CREATURE_PART_RIGHT_SHOULDER<br/>
        ///       CREATURE_PART_LEFT_SHOULDER<br/>
        ///       CREATURE_PART_RIGHT_HAND<br/>
        ///       CREATURE_PART_LEFT_HAND<br/>
        ///       CREATURE_PART_HEAD
        /// </summary>
        public static int GetCreatureBodyPart(CreaturePartType nPart, uint oCreature = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetCreatureBodyPart((int)nPart, oCreature);
        }

        /// <summary>
        ///  Sets the body part model to be used on the creature specified.<br/>
        ///  The model names for parts need to be in the following format:<br/>
        ///    p&lt;m/f&gt;&lt;race letter&gt;&lt;phenotype&gt;_&lt;body part&gt;&lt;model number&gt;.mdl<br/>
        /// <br/>
        ///  - nPart (CREATURE_PART_*)<br/>
        ///       CREATURE_PART_RIGHT_FOOT<br/>
        ///       CREATURE_PART_LEFT_FOOT<br/>
        ///       CREATURE_PART_RIGHT_SHIN<br/>
        ///       CREATURE_PART_LEFT_SHIN<br/>
        ///       CREATURE_PART_RIGHT_THIGH<br/>
        ///       CREATURE_PART_LEFT_THIGH<br/>
        ///       CREATURE_PART_PELVIS<br/>
        ///       CREATURE_PART_TORSO<br/>
        ///       CREATURE_PART_BELT<br/>
        ///       CREATURE_PART_NECK<br/>
        ///       CREATURE_PART_RIGHT_FOREARM<br/>
        ///       CREATURE_PART_LEFT_FOREARM<br/>
        ///       CREATURE_PART_RIGHT_BICEP<br/>
        ///       CREATURE_PART_LEFT_BICEP<br/>
        ///       CREATURE_PART_RIGHT_SHOULDER<br/>
        ///       CREATURE_PART_LEFT_SHOULDER<br/>
        ///       CREATURE_PART_RIGHT_HAND<br/>
        ///       CREATURE_PART_LEFT_HAND<br/>
        ///       CREATURE_PART_HEAD<br/>
        ///  - nModelNumber: CREATURE_MODEL_TYPE_*<br/>
        ///       CREATURE_MODEL_TYPE_NONE<br/>
        ///       CREATURE_MODEL_TYPE_SKIN (not for use on shoulders, pelvis or head).<br/>
        ///       CREATURE_MODEL_TYPE_TATTOO (for body parts that support tattoos, i.e. not heads/feet/hands).<br/>
        ///       CREATURE_MODEL_TYPE_UNDEAD (undead model only exists for the right arm parts).<br/>
        ///  - oCreature: the creature to change the body part for.<br/>
        ///  Note: Only part based creature appearance types are supported.<br/>
        ///  i.e. The model types for the playable races (&apos;P&apos;) in the appearance.2da
        /// </summary>
        public static void SetCreatureBodyPart(CreaturePartType nPart, int nModelNumber, uint oCreature = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetCreatureBodyPart((int)nPart, nModelNumber, oCreature);
        }

        /// <summary>
        ///  Returns the Tail type of the creature specified.<br/>
        ///       CREATURE_TAIL_TYPE_NONE<br/>
        ///       CREATURE_TAIL_TYPE_LIZARD<br/>
        ///       CREATURE_TAIL_TYPE_BONE<br/>
        ///       CREATURE_TAIL_TYPE_DEVIL<br/>
        ///  returns CREATURE_TAIL_TYPE_NONE if used on a non-creature object,<br/>
        ///  if the creature has no Tail, or if the creature can not have its<br/>
        ///  Tail type changed in the toolset.
        /// </summary>
        public static CreatureTailType GetCreatureTailType(uint oCreature = OBJECT_INVALID)
        {
            return (CreatureTailType)NWN.Core.NWScript.GetCreatureTailType(oCreature);
        }

        /// <summary>
        ///  Sets the Tail type of the creature specified.<br/>
        ///  - nTailType (CREATURE_TAIL_TYPE_*)<br/>
        ///       CREATURE_TAIL_TYPE_NONE<br/>
        ///       CREATURE_TAIL_TYPE_LIZARD<br/>
        ///       CREATURE_TAIL_TYPE_BONE<br/>
        ///       CREATURE_TAIL_TYPE_DEVIL<br/>
        ///  - oCreature: the creature to change the Tail type for.<br/>
        ///  Note: Only two creature model types will support Tails.<br/>
        ///  The MODELTYPE for the part based (playable) races &apos;P&apos;<br/>
        ///  and MODELTYPE &apos;T&apos; in the appearance.2da
        /// </summary>
        public static void SetCreatureTailType(CreatureTailType nTailType, uint oCreature = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetCreatureTailType((int)nTailType, oCreature);
        }

        /// <summary>
        ///  Returns the Hardness of a Door or Placeable object.<br/>
        ///  - oObject: a door or placeable object.<br/>
        ///  returns -1 on an error or if used on an object that is<br/>
        ///  neither a door nor a placeable object.
        /// </summary>
        public static int GetHardness(uint oObject = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetHardness(oObject);
        }

        /// <summary>
        ///  Sets the Hardness of a Door or Placeable object.<br/>
        ///  - nHardness: must be between 0 and 250.<br/>
        ///  - oObject: a door or placeable object.<br/>
        ///  Does nothing if used on an object that is neither<br/>
        ///  a door nor a placeable.
        /// </summary>
        public static void SetHardness(int nHardness, uint oObject = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetHardness(nHardness, oObject);
        }

        /// <summary>
        ///  When set the object can not be opened unless the<br/>
        ///  opener possesses the required key. The key tag required<br/>
        ///  can be specified either in the toolset, or by using<br/>
        ///  the SetLockKeyTag() scripting command.<br/>
        ///  - oObject: a door, or placeable.<br/>
        ///  - nKeyRequired: true/FALSE
        /// </summary>
        public static void SetLockKeyRequired(uint oObject, bool nKeyRequired = true)
        {
            NWN.Core.NWScript.SetLockKeyRequired(oObject, nKeyRequired ? 1 : 0);
        }

        /// <summary>
        ///  Set the key tag required to open object oObject.<br/>
        ///  This will only have an effect if the object is set to<br/>
        ///  "Key required to unlock or lock" either in the toolset<br/>
        ///  or by using the scripting command SetLockKeyRequired().<br/>
        ///  - oObject: a door, placeable or trigger.<br/>
        ///  - sNewKeyTag: the key tag required to open the locked object.
        /// </summary>
        public static void SetLockKeyTag(uint oObject, string sNewKeyTag)
        {
            NWN.Core.NWScript.SetLockKeyTag(oObject, sNewKeyTag);
        }

        /// <summary>
        ///  Sets whether or not the object can be locked.<br/>
        ///  - oObject: a door or placeable.<br/>
        ///  - nLockable: true/FALSE
        /// </summary>
        public static void SetLockLockable(uint oObject, bool nLockable = true)
        {
            NWN.Core.NWScript.SetLockLockable(oObject, nLockable ? 1 : 0);
        }

        /// <summary>
        ///  Sets the DC for unlocking the object.<br/>
        ///  - oObject: a door or placeable object.<br/>
        ///  - nNewUnlockDC: must be between 0 and 250.
        /// </summary>
        public static void SetLockUnlockDC(uint oObject, int nNewUnlockDC)
        {
            NWN.Core.NWScript.SetLockUnlockDC(oObject, nNewUnlockDC);
        }

        /// <summary>
        ///  Sets the DC for locking the object.<br/>
        ///  - oObject: a door or placeable object.<br/>
        ///  - nNewLockDC: must be between 0 and 250.
        /// </summary>
        public static void SetLockLockDC(uint oObject, int nNewLockDC)
        {
            NWN.Core.NWScript.SetLockLockDC(oObject, nNewLockDC);
        }

        /// <summary>
        ///  Sets whether or not the trapped object can be disarmed.<br/>
        ///  - oTrapObject: a placeable, door or trigger<br/>
        ///  - nDisarmable: true/FALSE
        /// </summary>
        public static void SetTrapDisarmable(uint oTrapObject, bool nDisarmable = true)
        {
            NWN.Core.NWScript.SetTrapDisarmable(oTrapObject, nDisarmable ? 1 : 0);
        }

        /// <summary>
        ///  Sets whether or not the trapped object can be detected.<br/>
        ///  - oTrapObject: a placeable, door or trigger<br/>
        ///  - nDetectable: true/FALSE<br/>
        ///  Note: Setting a trapped object to not be detectable will<br/>
        ///  not make the trap disappear if it has already been detected.
        /// </summary>
        public static void SetTrapDetectable(uint oTrapObject, bool nDetectable = true)
        {
            NWN.Core.NWScript.SetTrapDetectable(oTrapObject, nDetectable ? 1 : 0);
        }

        /// <summary>
        ///  Sets whether or not the trap is a one-shot trap<br/>
        ///  (i.e. whether or not the trap resets itself after firing).<br/>
        ///  - oTrapObject: a placeable, door or trigger<br/>
        ///  - nOneShot: true/FALSE
        /// </summary>
        public static void SetTrapOneShot(uint oTrapObject, bool nOneShot = true)
        {
            NWN.Core.NWScript.SetTrapOneShot(oTrapObject, nOneShot ? 1 : 0);
        }

        /// <summary>
        ///  Set the tag of the key that will disarm oTrapObject.<br/>
        ///  - oTrapObject: a placeable, door or trigger
        /// </summary>
        public static void SetTrapKeyTag(uint oTrapObject, string sKeyTag)
        {
            NWN.Core.NWScript.SetTrapKeyTag(oTrapObject, sKeyTag);
        }

        /// <summary>
        ///  Set the DC for disarming oTrapObject.<br/>
        ///  - oTrapObject: a placeable, door or trigger<br/>
        ///  - nDisarmDC: must be between 0 and 250.
        /// </summary>
        public static void SetTrapDisarmDC(uint oTrapObject, int nDisarmDC)
        {
            NWN.Core.NWScript.SetTrapDisarmDC(oTrapObject, nDisarmDC);
        }

        /// <summary>
        ///  Set the DC for detecting oTrapObject.<br/>
        ///  - oTrapObject: a placeable, door or trigger<br/>
        ///  - nDetectDC: must be between 0 and 250.
        /// </summary>
        public static void SetTrapDetectDC(uint oTrapObject, int nDetectDC)
        {
            NWN.Core.NWScript.SetTrapDetectDC(oTrapObject, nDetectDC);
        }

        /// <summary>
        ///  Creates a square Trap object.<br/>
        ///  - nTrapType: The base type of trap (TRAP_BASE_TYPE_*)<br/>
        ///  - lLocation: The location and orientation that the trap will be created at.<br/>
        ///  - fSize: The size of the trap. Minimum size allowed is 1.0f.<br/>
        ///  - sTag: The tag of the trap being created.<br/>
        ///  - nFaction: The faction of the trap (STANDARD_FACTION_*).<br/>
        ///  - sOnDisarmScript: The OnDisarm script that will fire when the trap is disarmed.<br/>
        ///                     If &quot;&quot; no script will fire.<br/>
        ///  - sOnTrapTriggeredScript: The OnTrapTriggered script that will fire when the<br/>
        ///                            trap is triggered.<br/>
        ///                            If &quot;&quot; the default OnTrapTriggered script for the trap<br/>
        ///                            type specified will fire instead (as specified in the<br/>
        ///                            traps.2da).
        /// </summary>
        public static uint CreateTrapAtLocation(int nTrapType, Location lLocation, float fSize = 2.0f, string sTag = "", StandardFactionType nFaction = StandardFactionType.Hostile, string sOnDisarmScript = "", string sOnTrapTriggeredScript = "")
        {
            return NWN.Core.NWScript.CreateTrapAtLocation(nTrapType, lLocation, fSize, sTag, (int)nFaction, sOnDisarmScript, sOnTrapTriggeredScript);
        }

        /// <summary>
        ///  Creates a Trap on the object specified.<br/>
        ///  - nTrapType: The base type of trap (TRAP_BASE_TYPE_*)<br/>
        ///  - oObject: The object that the trap will be created on. Works only on Doors and Placeables.<br/>
        ///  - nFaction: The faction of the trap (STANDARD_FACTION_*).<br/>
        ///  - sOnDisarmScript: The OnDisarm script that will fire when the trap is disarmed.<br/>
        ///                     If &quot;&quot; no script will fire.<br/>
        ///  - sOnTrapTriggeredScript: The OnTrapTriggered script that will fire when the<br/>
        ///                            trap is triggered.<br/>
        ///                            If &quot;&quot; the default OnTrapTriggered script for the trap<br/>
        ///                            type specified will fire instead (as specified in the<br/>
        ///                            traps.2da).<br/>
        ///  Note: After creating a trap on an object, you can change the trap&apos;s properties<br/>
        ///        using the various SetTrap* scripting commands by passing in the object<br/>
        ///        that the trap was created on (i.e. oObject) to any subsequent SetTrap* commands.
        /// </summary>
        public static void CreateTrapOnObject(int nTrapType, uint oObject, StandardFactionType nFaction = StandardFactionType.Hostile, string sOnDisarmScript = "", string sOnTrapTriggeredScript = "")
        {
            NWN.Core.NWScript.CreateTrapOnObject(nTrapType, oObject, (int)nFaction, sOnDisarmScript, sOnTrapTriggeredScript);
        }

        /// <summary>
        ///  Set the Will saving throw value of the Door or Placeable object oObject.<br/>
        ///  - oObject: a door or placeable object.<br/>
        ///  - nWillSave: must be between 0 and 250.
        /// </summary>
        public static void SetWillSavingThrow(uint oObject, int nWillSave)
        {
            NWN.Core.NWScript.SetWillSavingThrow(oObject, nWillSave);
        }

        /// <summary>
        ///  Set the Reflex saving throw value of the Door or Placeable object oObject.<br/>
        ///  - oObject: a door or placeable object.<br/>
        ///  - nReflexSave: must be between 0 and 250.
        /// </summary>
        public static void SetReflexSavingThrow(uint oObject, int nReflexSave)
        {
            NWN.Core.NWScript.SetReflexSavingThrow(oObject, nReflexSave);
        }

        /// <summary>
        ///  Set the Fortitude saving throw value of the Door or Placeable object oObject.<br/>
        ///  - oObject: a door or placeable object.<br/>
        ///  - nFortitudeSave: must be between 0 and 250.
        /// </summary>
        public static void SetFortitudeSavingThrow(uint oObject, int nFortitudeSave)
        {
            NWN.Core.NWScript.SetFortitudeSavingThrow(oObject, nFortitudeSave);
        }

        /// <summary>
        ///  Returns the resref (TILESET_RESREF_*) of the tileset used to create area oArea.<br/>
        ///       TILESET_RESREF_BEHOLDER_CAVES<br/>
        ///       TILESET_RESREF_CASTLE_INTERIOR<br/>
        ///       TILESET_RESREF_CITY_EXTERIOR<br/>
        ///       TILESET_RESREF_CITY_INTERIOR<br/>
        ///       TILESET_RESREF_CRYPT<br/>
        ///       TILESET_RESREF_DESERT<br/>
        ///       TILESET_RESREF_DROW_INTERIOR<br/>
        ///       TILESET_RESREF_DUNGEON<br/>
        ///       TILESET_RESREF_FOREST<br/>
        ///       TILESET_RESREF_FROZEN_WASTES<br/>
        ///       TILESET_RESREF_ILLITHID_INTERIOR<br/>
        ///       TILESET_RESREF_MICROSET<br/>
        ///       TILESET_RESREF_MINES_AND_CAVERNS<br/>
        ///       TILESET_RESREF_RUINS<br/>
        ///       TILESET_RESREF_RURAL<br/>
        ///       TILESET_RESREF_RURAL_WINTER<br/>
        ///       TILESET_RESREF_SEWERS<br/>
        ///       TILESET_RESREF_UNDERDARK<br/>
        ///  * returns an empty string on an error.
        /// </summary>
        public static string GetTilesetResRef(uint oArea)
        {
            return NWN.Core.NWScript.GetTilesetResRef(oArea);
        }

        /// <summary>
        ///  - oTrapObject: a placeable, door or trigger<br/>
        ///  * Returns true if oTrapObject can be recovered.
        /// </summary>
        public static bool GetTrapRecoverable(uint oTrapObject)
        {
            return NWN.Core.NWScript.GetTrapRecoverable(oTrapObject) == 1;
        }

        /// <summary>
        ///  Sets whether or not the trapped object can be recovered.<br/>
        ///  - oTrapObject: a placeable, door or trigger
        /// </summary>
        public static void SetTrapRecoverable(uint oTrapObject, bool nRecoverable = true)
        {
            NWN.Core.NWScript.SetTrapRecoverable(oTrapObject, nRecoverable ? 1 : 0);
        }

        /// <summary>
        ///  Get the XP scale being used for the module.
        /// </summary>
        public static int GetModuleXPScale()
        {
            return NWN.Core.NWScript.GetModuleXPScale();
        }

        /// <summary>
        ///  Set the XP scale used by the module.<br/>
        ///  - nXPScale: The XP scale to be used. Must be between 0 and 200.
        /// </summary>
        public static void SetModuleXPScale(int nXPScale)
        {
            NWN.Core.NWScript.SetModuleXPScale(nXPScale);
        }

        /// <summary>
        ///  Get the feedback message that will be displayed when trying to unlock the object oObject.<br/>
        ///  - oObject: a door or placeable.<br/>
        ///  Returns an empty string &quot;&quot; on an error or if the game&apos;s default feedback message is being used
        /// </summary>
        public static string GetKeyRequiredFeedback(uint oObject)
        {
            return NWN.Core.NWScript.GetKeyRequiredFeedback(oObject);
        }

        /// <summary>
        ///  Set the feedback message that is displayed when trying to unlock the object oObject.<br/>
        ///  This will only have an effect if the object is set to<br/>
        ///  &quot;Key required to unlock or lock&quot; either in the toolset<br/>
        ///  or by using the scripting command SetLockKeyRequired().<br/>
        ///  - oObject: a door or placeable.<br/>
        ///  - sFeedbackMessage: the string to be displayed in the player&apos;s text window.<br/>
        ///                      to use the game&apos;s default message, set sFeedbackMessage to &quot;&quot;
        /// </summary>
        public static void SetKeyRequiredFeedback(uint oObject, string sFeedbackMessage)
        {
            NWN.Core.NWScript.SetKeyRequiredFeedback(oObject, sFeedbackMessage);
        }

        /// <summary>
        ///  - oTrapObject: a placeable, door or trigger<br/>
        ///  * Returns true if oTrapObject is active
        /// </summary>
        public static bool GetTrapActive(uint oTrapObject)
        {
            return NWN.Core.NWScript.GetTrapActive(oTrapObject) == 1;
        }

        /// <summary>
        ///  Sets whether or not the trap is an active trap<br/>
        ///  - oTrapObject: a placeable, door or trigger<br/>
        ///  - nActive: true/FALSE<br/>
        ///  Notes:<br/>
        ///  Setting a trap as inactive will not make the<br/>
        ///  trap disappear if it has already been detected.<br/>
        ///  Call SetTrapDetectedBy() to make a detected trap disappear.<br/>
        ///  To make an inactive trap not detectable call SetTrapDetectable()
        /// </summary>
        public static void SetTrapActive(uint oTrapObject, bool nActive = true)
        {
            NWN.Core.NWScript.SetTrapActive(oTrapObject, nActive ? 1 : 0);
        }

        /// <summary>
        ///  Locks the player&apos;s camera pitch to its current pitch setting,<br/>
        ///  or unlocks the player&apos;s camera pitch.<br/>
        ///  Stops the player from tilting their camera angle.<br/>
        ///  - oPlayer: A player object.<br/>
        ///  - bLocked: true/FALSE.
        /// </summary>
        public static void LockCameraPitch(uint oPlayer, bool bLocked = true)
        {
            NWN.Core.NWScript.LockCameraPitch(oPlayer, bLocked ? 1 : 0);
        }

        /// <summary>
        ///  Locks the player&apos;s camera distance to its current distance setting,<br/>
        ///  or unlocks the player&apos;s camera distance.<br/>
        ///  Stops the player from being able to zoom in/out the camera.<br/>
        ///  - oPlayer: A player object.<br/>
        ///  - bLocked: true/FALSE.
        /// </summary>
        public static void LockCameraDistance(uint oPlayer, bool bLocked = true)
        {
            NWN.Core.NWScript.LockCameraDistance(oPlayer, bLocked ? 1 : 0);
        }

        /// <summary>
        ///  Locks the player&apos;s camera direction to its current direction,<br/>
        ///  or unlocks the player&apos;s camera direction to enable it to move<br/>
        ///  freely again.<br/>
        ///  Stops the player from being able to rotate the camera direction.<br/>
        ///  - oPlayer: A player object.<br/>
        ///  - bLocked: true/FALSE.
        /// </summary>
        public static void LockCameraDirection(uint oPlayer, bool bLocked = true)
        {
            NWN.Core.NWScript.LockCameraDirection(oPlayer, bLocked ? 1 : 0);
        }

        /// <summary>
        ///  Get the last object that default clicked (left clicked) on the placeable object<br/>
        ///  that is calling this function.<br/>
        ///  Should only be called from a placeables OnClick event.<br/>
        ///  * Returns OBJECT_INVALID if it is called by something other than a placeable.
        /// </summary>
        public static uint GetPlaceableLastClickedBy()
        {
            return NWN.Core.NWScript.GetPlaceableLastClickedBy();
        }

        /// <summary>
        ///  returns true if the item is flagged as infinite.<br/>
        ///  - oItem: an item.<br/>
        ///  The infinite property affects the buying/selling behavior of the item in a store.<br/>
        ///  An infinite item will still be available to purchase from a store after a player<br/>
        ///  buys the item (non-infinite items will disappear from the store when purchased).
        /// </summary>
        public static bool GetInfiniteFlag(uint oItem)
        {
            return NWN.Core.NWScript.GetInfiniteFlag(oItem) == 1;
        }

        /// <summary>
        ///  Sets the Infinite flag on an item<br/>
        ///  - oItem: the item to change<br/>
        ///  - bInfinite: true or false, whether the item should be Infinite<br/>
        ///  The infinite property affects the buying/selling behavior of the item in a store.<br/>
        ///  An infinite item will still be available to purchase from a store after a player<br/>
        ///  buys the item (non-infinite items will disappear from the store when purchased).
        /// </summary>
        public static void SetInfiniteFlag(uint oItem, bool bInfinite = true)
        {
            NWN.Core.NWScript.SetInfiniteFlag(oItem, bInfinite ? 1 : 0);
        }

        /// <summary>
        ///  Gets the size of the area.<br/>
        ///  - nAreaDimension: The area dimension that you wish to determine.<br/>
        ///       AREA_HEIGHT<br/>
        ///       AREA_WIDTH<br/>
        ///  - oArea: The area that you wish to get the size of.<br/>
        ///  Returns: The number of tiles that the area is wide/high, or zero on an error.<br/>
        ///  If no valid area (or object) is specified, it uses the area of the caller.<br/>
        ///  If an object other than an area is specified, will use the area that the object is currently in.
        /// </summary>
        public static int GetAreaSize(AreaDimensionType nAreaDimension, uint oArea = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetAreaSize((int)nAreaDimension, oArea);
        }

        /// <summary>
        ///  Set the name of oObject.<br/>
        ///  - oObject: the object for which you are changing the name (a creature, placeable, item, or door).<br/>
        ///  - sNewName: the new name that the object will use.<br/>
        ///  Note: Setting an object&apos;s name to &quot;&quot; will make the object<br/>
        ///        revert to using the name it had originally before any<br/>
        ///        SetName() calls were made on the object.
        /// </summary>
        public static void SetName(uint oObject, string sNewName = "")
        {
            NWN.Core.NWScript.SetName(oObject, sNewName);
        }

        /// <summary>
        ///  Get the PortraitId of oTarget.<br/>
        ///  - oTarget: the object for which you are getting the portrait Id.<br/>
        ///  Returns: The Portrait Id number being used for the object oTarget.<br/>
        ///           The Portrait Id refers to the row number of the Portraits.2da<br/>
        ///           that this portrait is from.<br/>
        ///           If a custom portrait is being used, oTarget is a player object,<br/>
        ///           or on an error returns PORTRAIT_INVALID. In these instances<br/>
        ///           try using GetPortraitResRef() instead.
        /// </summary>
        public static int GetPortraitId(uint oTarget = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetPortraitId(oTarget);
        }

        /// <summary>
        ///  Change the portrait of oTarget to use the Portrait Id specified.<br/>
        ///  - oTarget: the object for which you are changing the portrait.<br/>
        ///  - nPortraitId: The Id of the new portrait to use.<br/>
        ///                 nPortraitId refers to a row in the Portraits.2da<br/>
        ///  Note: Not all portrait Ids are suitable for use with all object types.<br/>
        ///        Setting the portrait Id will also cause the portrait ResRef<br/>
        ///        to be set to the appropriate portrait ResRef for the Id specified.
        /// </summary>
        public static void SetPortraitId(uint oTarget, int nPortraitId)
        {
            NWN.Core.NWScript.SetPortraitId(oTarget, nPortraitId);
        }

        /// <summary>
        ///  Get the Portrait ResRef of oTarget.<br/>
        ///  - oTarget: the object for which you are getting the portrait ResRef.<br/>
        ///  Returns: The Portrait ResRef being used for the object oTarget.<br/>
        ///           The Portrait ResRef will not include a trailing size letter.
        /// </summary>
        public static string GetPortraitResRef(uint oTarget = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetPortraitResRef(oTarget);
        }

        /// <summary>
        ///  Change the portrait of oTarget to use the Portrait ResRef specified.<br/>
        ///  - oTarget: the object for which you are changing the portrait.<br/>
        ///  - sPortraitResRef: The ResRef of the new portrait to use.<br/>
        ///                     The ResRef should not include any trailing size letter ( e.g. po_el_f_09_ ).<br/>
        ///  Note: Not all portrait ResRefs are suitable for use with all object types.<br/>
        ///        Setting the portrait ResRef will also cause the portrait Id<br/>
        ///        to be set to PORTRAIT_INVALID.
        /// </summary>
        public static void SetPortraitResRef(uint oTarget, string sPortraitResRef)
        {
            NWN.Core.NWScript.SetPortraitResRef(oTarget, sPortraitResRef);
        }

        /// <summary>
        ///  Set oTarget&apos;s useable object status.<br/>
        ///  Note: Only works on non-static placeables, creatures, doors and items.<br/>
        ///  On items, it affects interactivity when they&apos;re on the ground, and not useability in inventory.
        /// </summary>
        public static void SetUseableFlag(uint oTarget, bool nUseableFlag)
        {
            NWN.Core.NWScript.SetUseableFlag(oTarget, nUseableFlag ? 1 : 0);
        }

        /// <summary>
        ///  Get the description of oObject.<br/>
        ///  - oObject: the object from which you are obtaining the description.<br/>
        ///             Can be a creature, item, placeable, door, trigger or module object.<br/>
        ///  - bOriginalDescription:  if set to true any new description specified via a SetDescription scripting command<br/>
        ///                    is ignored and the original object&apos;s description is returned instead.<br/>
        ///  - bIdentified: If oObject is an item, setting this to true will return the identified description,<br/>
        ///                 setting this to false will return the unidentified description. This flag has no<br/>
        ///                 effect on objects other than items.
        /// </summary>
        public static string GetDescription(uint oObject, bool bOriginalDescription = false, bool bIdentifiedDescription = true)
        {
            return NWN.Core.NWScript.GetDescription(oObject, bOriginalDescription ? 1 : 0, bIdentifiedDescription ? 1 : 0);
        }

        /// <summary>
        ///  Set the description of oObject.<br/>
        ///  - oObject: the object for which you are changing the description<br/>
        ///             Can be a creature, placeable, item, door, or trigger.<br/>
        ///  - sNewDescription: the new description that the object will use.<br/>
        ///  - bIdentified: If oObject is an item, setting this to true will set the identified description,<br/>
        ///                 setting this to false will set the unidentified description. This flag has no<br/>
        ///                 effect on objects other than items.<br/>
        ///  Note: Setting an object&apos;s description to &quot;&quot; will make the object<br/>
        ///        revert to using the description it originally had before any<br/>
        ///        SetDescription() calls were made on the object.
        /// </summary>
        public static void SetDescription(uint oObject, string sNewDescription = "", bool bIdentifiedDescription = true)
        {
            NWN.Core.NWScript.SetDescription(oObject, sNewDescription, bIdentifiedDescription ? 1 : 0);
        }

        /// <summary>
        ///  Get the PC that sent the last player chat(text) message.<br/>
        ///  Should only be called from a module&apos;s OnPlayerChat event script.<br/>
        ///  * Returns OBJECT_INVALID on error.<br/>
        ///  Note: Private tells do not trigger a OnPlayerChat event.
        /// </summary>
        public static uint GetPCChatSpeaker()
        {
            return NWN.Core.NWScript.GetPCChatSpeaker();
        }

        /// <summary>
        ///  Get the last player chat(text) message that was sent.<br/>
        ///  Should only be called from a module&apos;s OnPlayerChat event script.<br/>
        ///  * Returns empty string &quot;&quot; on error.<br/>
        ///  Note: Private tells do not trigger a OnPlayerChat event.
        /// </summary>
        public static string GetPCChatMessage()
        {
            return NWN.Core.NWScript.GetPCChatMessage();
        }

        /// <summary>
        ///  Get the volume of the last player chat(text) message that was sent.<br/>
        ///  Returns one of the following TALKVOLUME_* constants based on the volume setting<br/>
        ///  that the player used to send the chat message.<br/>
        ///                 TALKVOLUME_TALK<br/>
        ///                 TALKVOLUME_WHISPER<br/>
        ///                 TALKVOLUME_SHOUT<br/>
        ///                 TALKVOLUME_SILENT_SHOUT (used for DM chat channel)<br/>
        ///                 TALKVOLUME_PARTY<br/>
        ///  Should only be called from a module&apos;s OnPlayerChat event script.<br/>
        ///  * Returns -1 on error.<br/>
        ///  Note: Private tells do not trigger a OnPlayerChat event.
        /// </summary>
        public static TalkVolumeType GetPCChatVolume()
        {
            return (TalkVolumeType)NWN.Core.NWScript.GetPCChatVolume();
        }

        /// <summary>
        ///  Set the last player chat(text) message before it gets sent to other players.<br/>
        ///  - sNewChatMessage: The new chat text to be sent onto other players.<br/>
        ///                     Setting the player chat message to an empty string &quot;&quot;,<br/>
        ///                     will cause the chat message to be discarded<br/>
        ///                     (i.e. it will not be sent to other players).<br/>
        ///  Note: The new chat message gets sent after the OnPlayerChat script exits.
        /// </summary>
        public static void SetPCChatMessage(string sNewChatMessage = "")
        {
            NWN.Core.NWScript.SetPCChatMessage(sNewChatMessage);
        }

        /// <summary>
        ///  Set the last player chat(text) volume before it gets sent to other players.<br/>
        ///  - nTalkVolume: The new volume of the chat text to be sent onto other players.<br/>
        ///                 TALKVOLUME_TALK<br/>
        ///                 TALKVOLUME_WHISPER<br/>
        ///                 TALKVOLUME_SHOUT<br/>
        ///                 TALKVOLUME_SILENT_SHOUT (used for DM chat channel)<br/>
        ///                 TALKVOLUME_PARTY<br/>
        ///                 TALKVOLUME_TELL (sends the chat message privately back to the original speaker)<br/>
        ///  Note: The new chat message gets sent after the OnPlayerChat script exits.
        /// </summary>
        public static void SetPCChatVolume(TalkVolumeType nTalkVolume = TalkVolumeType.Talk)
        {
            NWN.Core.NWScript.SetPCChatVolume((int)nTalkVolume);
        }

        /// <summary>
        ///  Get the Color of oObject from the color channel specified.<br/>
        ///  - oObject: the object from which you are obtaining the color.<br/>
        ///             Can be a creature that has color information (i.e. the playable races).<br/>
        ///  - nColorChannel: The color channel that you want to get the color value of.<br/>
        ///                    COLOR_CHANNEL_SKIN<br/>
        ///                    COLOR_CHANNEL_HAIR<br/>
        ///                    COLOR_CHANNEL_TATTOO_1<br/>
        ///                    COLOR_CHANNEL_TATTOO_2<br/>
        ///  * Returns -1 on error.
        /// </summary>
        public static int GetColor(uint oObject, ColorChannelType nColorChannel)
        {
            return NWN.Core.NWScript.GetColor(oObject, (int)nColorChannel);
        }

        /// <summary>
        ///  Set the color channel of oObject to the color specified.<br/>
        ///  - oObject: the object for which you are changing the color.<br/>
        ///             Can be a creature that has color information (i.e. the playable races).<br/>
        ///  - nColorChannel: The color channel that you want to set the color value of.<br/>
        ///                    COLOR_CHANNEL_SKIN<br/>
        ///                    COLOR_CHANNEL_HAIR<br/>
        ///                    COLOR_CHANNEL_TATTOO_1<br/>
        ///                    COLOR_CHANNEL_TATTOO_2<br/>
        ///  - nColorValue: The color you want to set (0-175).
        /// </summary>
        public static void SetColor(uint oObject, ColorChannelType nColorChannel, int nColorValue)
        {
            NWN.Core.NWScript.SetColor(oObject, (int)nColorChannel, nColorValue);
        }

        /// <summary>
        ///  Returns Item property Material.  You need to specify the Material Type.<br/>
        ///  - nMasterialType: The Material Type should be a positive integer between 0 and 77 (see iprp_matcost.2da).<br/>
        ///  Note: The Material Type property will only affect the cost of the item if you modify the cost in the iprp_matcost.2da.
        /// </summary>
        public static ItemProperty ItemPropertyMaterial(int nMaterialType)
        {
            return NWN.Core.NWScript.ItemPropertyMaterial(nMaterialType);
        }

        /// <summary>
        ///  Returns Item property Quality. You need to specify the Quality.<br/>
        ///  - nQuality:  The Quality of the item property to create (see iprp_qualcost.2da).<br/>
        ///               IP_CONST_QUALITY_*<br/>
        ///  Note: The quality property will only affect the cost of the item if you modify the cost in the iprp_qualcost.2da.
        /// </summary>
        public static ItemProperty ItemPropertyQuality(int nQuality)
        {
            return NWN.Core.NWScript.ItemPropertyQuality(nQuality);
        }

        /// <summary>
        ///  Returns a generic Additional Item property. You need to specify the Additional property.<br/>
        ///  - nProperty: The item property to create (see iprp_addcost.2da).<br/>
        ///               IP_CONST_ADDITIONAL_*<br/>
        ///  Note: The additional property only affects the cost of the item if you modify the cost in the iprp_addcost.2da.
        /// </summary>
        public static ItemProperty ItemPropertyAdditional(int nAdditionalProperty)
        {
            return NWN.Core.NWScript.ItemPropertyAdditional(nAdditionalProperty);
        }

        /// <summary>
        ///  Sets a new tag for oObject.<br/>
        ///  Will do nothing for invalid objects or the module object.<br/>
        /// <br/>
        ///  Note: Care needs to be taken with this function.<br/>
        ///        Changing the tag for creature with waypoints will make them stop walking them.<br/>
        ///        Changing waypoint, door or trigger tags will break their area transitions.
        /// </summary>
        public static void SetTag(uint oObject, string sNewTag)
        {
            NWN.Core.NWScript.SetTag(oObject, sNewTag);
        }

        /// <summary>
        ///  Returns the string tag set for the provided effect.<br/>
        ///  - If no tag has been set, returns an empty string.
        /// </summary>
        public static string GetEffectTag(Effect eEffect)
        {
            return NWN.Core.NWScript.GetEffectTag(eEffect);
        }

        /// <summary>
        ///  Tags the effect with the provided string.<br/>
        ///  - Any other tags in the link will be overwritten.
        /// </summary>
        public static Effect TagEffect(Effect eEffect, string sNewTag)
        {
            return NWN.Core.NWScript.TagEffect(eEffect, sNewTag);
        }

        /// <summary>
        ///  Returns the caster level of the creature who created the effect.<br/>
        ///  - If not created by a creature, returns 0.<br/>
        ///  - If created by a spell-like ability, returns 0.
        /// </summary>
        public static int GetEffectCasterLevel(Effect eEffect)
        {
            return NWN.Core.NWScript.GetEffectCasterLevel(eEffect);
        }

        /// <summary>
        ///  Returns the total duration of the effect in seconds.<br/>
        ///  - Returns 0 if the duration type of the effect is not DURATION_TYPE_TEMPORARY.
        /// </summary>
        public static int GetEffectDuration(Effect eEffect)
        {
            return NWN.Core.NWScript.GetEffectDuration(eEffect);
        }

        /// <summary>
        ///  Returns the remaining duration of the effect in seconds.<br/>
        ///  - Returns 0 if the duration type of the effect is not DURATION_TYPE_TEMPORARY.
        /// </summary>
        public static int GetEffectDurationRemaining(Effect eEffect)
        {
            return NWN.Core.NWScript.GetEffectDurationRemaining(eEffect);
        }

        /// <summary>
        ///  Returns the string tag set for the provided item property.<br/>
        ///  - If no tag has been set, returns an empty string.
        /// </summary>
        public static string GetItemPropertyTag(ItemProperty nProperty)
        {
            return NWN.Core.NWScript.GetItemPropertyTag(nProperty);
        }

        /// <summary>
        ///  Tags the item property with the provided string.<br/>
        ///  - Any tags currently set on the item property will be overwritten.
        /// </summary>
        public static ItemProperty TagItemProperty(ItemProperty nProperty, string sNewTag)
        {
            return NWN.Core.NWScript.TagItemProperty(nProperty, sNewTag);
        }

        /// <summary>
        ///  Returns the total duration of the item property in seconds.<br/>
        ///  - Returns 0 if the duration type of the item property is not DURATION_TYPE_TEMPORARY.
        /// </summary>
        public static int GetItemPropertyDuration(ItemProperty nProperty)
        {
            return NWN.Core.NWScript.GetItemPropertyDuration(nProperty);
        }

        /// <summary>
        ///  Returns the remaining duration of the item property in seconds.<br/>
        ///  - Returns 0 if the duration type of the item property is not DURATION_TYPE_TEMPORARY.
        /// </summary>
        public static int GetItemPropertyDurationRemaining(ItemProperty nProperty)
        {
            return NWN.Core.NWScript.GetItemPropertyDurationRemaining(nProperty);
        }

        /// <summary>
        ///  Instances a new area from the given sSourceResRef, which needs to be a existing module area.<br/>
        ///  Will optionally set a new area tag and displayed name. The new area is accessible<br/>
        ///  immediately, but initialisation scripts for the area and all contained creatures will only<br/>
        ///  run after the current script finishes (so you can clean up objects before returning).<br/>
        /// <br/>
        ///  Returns the new area, or OBJECT_INVALID on failure.<br/>
        /// <br/>
        ///  Note: When spawning a second instance of a existing area, you will have to manually<br/>
        ///        adjust all transitions (doors, triggers) with the relevant script commands,<br/>
        ///        or players might end up in the wrong area.<br/>
        ///  Note: Areas cannot have duplicate ResRefs, so your new area will have a autogenerated,<br/>
        ///        sequential resref starting with &quot;nw_&quot;; for example: nw_5. You cannot influence this resref.<br/>
        ///        If you destroy an area, that resref will be come free for reuse for the next area created.<br/>
        ///        If you need to know the resref of your new area, you can call GetResRef on it.<br/>
        ///  Note: When instancing an area from a loaded savegame, it will spawn the area as it was at time of save, NOT<br/>
        ///        at module creation. This is because the savegame replaces the module data. Due to technical limitations,<br/>
        ///        polymorphed creatures, personal reputation, and associates will currently fail to restore correctly.
        /// </summary>
        public static uint CreateArea(string sSourceResRef, string sNewTag = "", string sNewName = "")
        {
            return NWN.Core.NWScript.CreateArea(sSourceResRef, sNewTag, sNewName);
        }

        /// <summary>
        ///  Destroys the given area object and everything in it.<br/>
        /// <br/>
        ///  If the area is in a module, the .are and .git data is left behind and you can spawn from<br/>
        ///  it again. If the area is a temporary copy, the data will be deleted and you cannot spawn it again<br/>
        ///  via the resref.<br/>
        /// <br/>
        ///  Return values:<br/>
        ///     0: Object not an area or invalid.<br/>
        ///    -1: Area contains spawn location and removal would leave module without entrypoint.<br/>
        ///    -2: Players in area.<br/>
        ///     1: Area destroyed successfully.
        /// </summary>
        public static AreaDestroyResultType DestroyArea(uint oArea)
        {
            return (AreaDestroyResultType)NWN.Core.NWScript.DestroyArea(oArea);
        }

        /// <summary>
        ///  Creates a copy of a existing area, including everything inside of it (except players).<br/>
        ///  Will optionally set a new area tag and displayed name. The new area is accessible<br/>
        ///  immediately, but initialisation scripts for the area and all contained creatures will only<br/>
        ///  run after the current script finishes (so you can clean up objects before returning).<br/>
        /// <br/>
        ///  This is similar to CreateArea, except this variant will copy all changes made to the source<br/>
        ///  area since it has spawned. CreateArea() will instance the area from the .are and .git data<br/>
        ///  as it was at creation.<br/>
        /// <br/>
        ///  Returns the new area, or OBJECT_INVALID on error.<br/>
        /// <br/>
        ///  Note: You will have to manually adjust all transitions (doors, triggers) with the<br/>
        ///        relevant script commands, or players might end up in the wrong area.<br/>
        ///  Note: Areas cannot have duplicate ResRefs, so your new area will have a autogenerated,<br/>
        ///        sequential resref starting with &quot;nw_&quot;; for example: nw_5. You cannot influence this resref.<br/>
        ///        If you destroy an area, that resref will be come free for reuse for the next area created.<br/>
        ///        If you need to know the resref of your new area, you can call GetResRef on it.
        /// </summary>
        public static uint CopyArea(uint oArea, string sNewTag = "", string sNewName = "")
        {
            return NWN.Core.NWScript.CopyArea(oArea, sNewTag, sNewName);
        }

        /// <summary>
        ///  Returns the first area in the module.
        /// </summary>
        public static uint GetFirstArea()
        {
            return NWN.Core.NWScript.GetFirstArea();
        }

        /// <summary>
        ///  Returns the next area in the module (after GetFirstArea), or OBJECT_INVALID if no more<br/>
        ///  areas are loaded.
        /// </summary>
        public static uint GetNextArea()
        {
            return NWN.Core.NWScript.GetNextArea();
        }

        /// <summary>
        ///  Sets the transition target for oTransition.<br/>
        /// <br/>
        ///  Notes:<br/>
        ///  - oTransition can be any valid game object, except areas.<br/>
        ///  - oTarget can be any valid game object with a location, or OBJECT_INVALID (to unlink).<br/>
        ///  - Rebinding a transition will NOT change the other end of the transition; for example,<br/>
        ///    with normal doors you will have to do either end separately.<br/>
        ///  - Any valid game object can hold a transition target, but only some are used by the game engine<br/>
        ///    (doors and triggers). This might change in the future. You can still set and query them for<br/>
        ///    other game objects from nwscript.<br/>
        ///  - Transition target objects are cached: The toolset-configured destination tag is<br/>
        ///    used for a lookup only once, at first use. Thus, attempting to use SetTag() to change the<br/>
        ///    destination for a transition will not work in a predictable fashion.
        /// </summary>
        public static void SetTransitionTarget(uint oTransition, uint oTarget)
        {
            NWN.Core.NWScript.SetTransitionTarget(oTransition, oTarget);
        }

        /// <summary>
        ///  Sets whether the provided item should be hidden when equipped.<br/>
        ///  - The intended usage of this function is to provide an easy way to hide helmets, but it<br/>
        ///    can be used equally for any slot which has creature mesh visibility when equipped,<br/>
        ///    e.g.: armour, helm, cloak, left hand, and right hand.<br/>
        ///  - nValue should be true or false.
        /// </summary>
        public static void SetHiddenWhenEquipped(uint oItem, int nValue)
        {
            NWN.Core.NWScript.SetHiddenWhenEquipped(oItem, nValue);
        }

        /// <summary>
        ///  Returns whether the provided item is hidden when equipped.
        /// </summary>
        public static bool GetHiddenWhenEquipped(uint oItem)
        {
            return NWN.Core.NWScript.GetHiddenWhenEquipped(oItem) == 1;
        }

        /// <summary>
        ///  Sets if the given creature has explored tile at x, y of the given area.<br/>
        ///  Note that creature needs to be a player- or player-possessed creature.<br/>
        /// <br/>
        ///  Keep in mind that tile exploration also controls object visibility in areas<br/>
        ///  and the fog of war for interior and underground areas.<br/>
        /// <br/>
        ///  Return values:<br/>
        ///   -1: Area or creature invalid.<br/>
        ///    0: Tile was not explored before setting newState.<br/>
        ///    1: Tile was explored before setting newState.
        /// </summary>
        public static int SetTileExplored(uint creature, uint area, int x, int y, int newState)
        {
            return NWN.Core.NWScript.SetTileExplored(creature, area, x, y, newState);
        }

        /// <summary>
        ///  Returns whether the given tile at x, y, for the given creature in the stated<br/>
        ///  area is visible on the map.<br/>
        ///  Note that creature needs to be a player- or player-possessed creature.<br/>
        /// <br/>
        ///  Keep in mind that tile exploration also controls object visibility in areas<br/>
        ///  and the fog of war for interior and underground areas.<br/>
        /// <br/>
        ///  Return values:<br/>
        ///   -1: Area or creature invalid.<br/>
        ///    0: Tile is not explored yet.<br/>
        ///    1: Tile is explored.
        /// </summary>
        public static int GetTileExplored(uint creature, uint area, int x, int y)
        {
            return NWN.Core.NWScript.GetTileExplored(creature, area, x, y);
        }

        /// <summary>
        ///  Sets the creature to auto-explore the map as it walks around.<br/>
        /// <br/>
        ///  Keep in mind that tile exploration also controls object visibility in areas<br/>
        ///  and the fog of war for interior and underground areas.<br/>
        /// <br/>
        ///  This means that if you turn off auto exploration, it falls to you to manage this<br/>
        ///  through SetTileExplored(); otherwise, the player will not be able to see anything.<br/>
        /// <br/>
        ///  Valid arguments: true and false.<br/>
        ///  Does nothing for non-creatures.<br/>
        ///  Returns the previous state (or -1 if non-creature).
        /// </summary>
        public static int SetCreatureExploresMinimap(uint creature, bool newState)
        {
            return NWN.Core.NWScript.SetCreatureExploresMinimap(creature, newState ? 1 : 0);
        }

        /// <summary>
        ///  Returns true if the creature is set to auto-explore the map as it walks around (on by default).<br/>
        ///  Returns false if creature is not actually a creature.
        /// </summary>
        public static bool GetCreatureExploresMinimap(uint creature)
        {
            return NWN.Core.NWScript.GetCreatureExploresMinimap(creature) == 1;
        }

        /// <summary>
        ///  Get the surface material at the given location. (This is<br/>
        ///  equivalent to the walkmesh type).<br/>
        ///  Returns 0 if the location is invalid or has no surface type.
        /// </summary>
        public static int GetSurfaceMaterial(Location at)
        {
            return NWN.Core.NWScript.GetSurfaceMaterial(at);
        }
        /// <summary>
        ///  Returns the z-offset at which the walkmesh is at the given location.<br/>
        ///  Returns -6.0 for invalid locations.
        /// </summary>
        public static float GetGroundHeight(Location at)
        {
            return NWN.Core.NWScript.GetGroundHeight(at);
        }

        /// <summary>
        ///  Gets the attack bonus limit.<br/>
        ///  - The default value is 20.
        /// </summary>
        public static int GetAttackBonusLimit()
        {
            return NWN.Core.NWScript.GetAttackBonusLimit();
        }

        /// <summary>
        ///  Gets the damage bonus limit.<br/>
        ///  - The default value is 100.
        /// </summary>
        public static int GetDamageBonusLimit()
        {
            return NWN.Core.NWScript.GetDamageBonusLimit();
        }

        /// <summary>
        ///  Gets the saving throw bonus limit.<br/>
        ///  - The default value is 20.
        /// </summary>
        public static int GetSavingThrowBonusLimit()
        {
            return NWN.Core.NWScript.GetSavingThrowBonusLimit();
        }

        /// <summary>
        ///  Gets the ability bonus limit.<br/>
        ///  - The default value is 12.
        /// </summary>
        public static int GetAbilityBonusLimit()
        {
            return NWN.Core.NWScript.GetAbilityBonusLimit();
        }

        /// <summary>
        ///  Gets the ability penalty limit.<br/>
        ///  - The default value is 30.
        /// </summary>
        public static int GetAbilityPenaltyLimit()
        {
            return NWN.Core.NWScript.GetAbilityPenaltyLimit();
        }

        /// <summary>
        ///  Gets the skill bonus limit.<br/>
        ///  - The default value is 50.
        /// </summary>
        public static int GetSkillBonusLimit()
        {
            return NWN.Core.NWScript.GetSkillBonusLimit();
        }

        /// <summary>
        ///  Sets the attack bonus limit.<br/>
        ///  - The minimum value is 0.<br/>
        ///  - The maximum value is 255.<br/>
        ///  - This script call will temporarily override user/server configuration for the running module only.
        /// </summary>
        public static void SetAttackBonusLimit(int nNewLimit)
        {
            NWN.Core.NWScript.SetAttackBonusLimit(nNewLimit);
        }

        /// <summary>
        ///  Sets the damage bonus limit.<br/>
        ///  - The minimum value is 0.<br/>
        ///  - The maximum value is 255.<br/>
        ///  - This script call will temporarily override user/server configuration for the running module only.
        /// </summary>
        public static void SetDamageBonusLimit(int nNewLimit)
        {
            NWN.Core.NWScript.SetDamageBonusLimit(nNewLimit);
        }

        /// <summary>
        ///  Sets the saving throw bonus limit.<br/>
        ///  - The minimum value is 0.<br/>
        ///  - The maximum value is 255.<br/>
        ///  - This script call will temporarily override user/server configuration for the running module only.
        /// </summary>
        public static void SetSavingThrowBonusLimit(int nNewLimit)
        {
            NWN.Core.NWScript.SetSavingThrowBonusLimit(nNewLimit);
        }

        /// <summary>
        ///  Sets the ability bonus limit.<br/>
        ///  - The minimum value is 0.<br/>
        ///  - The maximum value is 255.<br/>
        ///  - This script call will temporarily override user/server configuration for the running module only.
        /// </summary>
        public static void SetAbilityBonusLimit(int nNewLimit)
        {
            NWN.Core.NWScript.SetAbilityBonusLimit(nNewLimit);
        }

        /// <summary>
        ///  Sets the ability penalty limit.<br/>
        ///  - The minimum value is 0.<br/>
        ///  - The maximum value is 255.<br/>
        ///  - This script call will temporarily override user/server configuration for the running module only.
        /// </summary>
        public static void SetAbilityPenaltyLimit(int nNewLimit)
        {
            NWN.Core.NWScript.SetAbilityPenaltyLimit(nNewLimit);
        }

        /// <summary>
        ///  Sets the skill bonus limit.<br/>
        ///  - The minimum value is 0.<br/>
        ///  - The maximum value is 255.<br/>
        ///  - This script call will temporarily override user/server configuration for the running module only.
        /// </summary>
        public static void SetSkillBonusLimit(int nNewLimit)
        {
            NWN.Core.NWScript.SetSkillBonusLimit(nNewLimit);
        }

        /// <summary>
        ///  Get if oPlayer is currently connected over a relay (instead of directly).<br/>
        ///  Returns false for any other object, including OBJECT_INVALID.
        /// </summary>
        public static bool GetIsPlayerConnectionRelayed(uint oPlayer)
        {
            return NWN.Core.NWScript.GetIsPlayerConnectionRelayed(oPlayer) == 1;
        }

        /// <summary>
        ///  Returns the event script for the given object and handler.<br/>
        ///  Will return "" if unset, the object is invalid, or the object cannot<br/>
        ///  have the requested handler.
        /// </summary>
        public static string GetEventScript(uint oObject, int nHandler)
        {
            return NWN.Core.NWScript.GetEventScript(oObject, nHandler);
        }

        /// <summary>
        ///  Sets the given event script for the given object and handler.<br/>
        ///  Returns 1 on success, 0 on failure.<br/>
        ///  Will fail if oObject is invalid or does not have the requested handler.
        /// </summary>
        public static int SetEventScript(uint oObject, EventScriptType nHandler, string sScript)
        {
            return NWN.Core.NWScript.SetEventScript(oObject, (int)nHandler, sScript);
        }

        /// <summary>
        ///  Gets a visual transform on the given object.<br/>
        ///  - oObject can be any valid Creature, Placeable, Item or Door.<br/>
        ///  - nTransform is one of OBJECT_VISUAL_TRANSFORM_*<br/>
        ///  - nScope is one of OBJECT_VISUAL_TRANSFORM_DATA_SCOPE_* and specific to the object type being VT'ed.<br/>
        ///  Returns the current (or default) value.
        /// </summary>
        public static float GetObjectVisualTransform(uint oObject, ObjectVisualTransformType nTransform, bool bCurrentLerp = false, ObjectVisualTransformDataScopeType nScope = ObjectVisualTransformDataScopeType.Base)
        {
            return NWN.Core.NWScript.GetObjectVisualTransform(oObject, (int)nTransform, bCurrentLerp ? 1 : 0, (int)nScope);
        }

        /// <summary>
        ///  Sets a visual transform on the given object.<br/>
        ///  - oObject can be any valid Creature, Placeable, Item or Door.<br/>
        ///  - nTransform is one of OBJECT_VISUAL_TRANSFORM_*<br/>
        ///  - fValue depends on the transformation to apply.<br/>
        ///  - nScope is one of OBJECT_VISUAL_TRANSFORM_DATA_SCOPE_* and specific to the object type being VT'ed.<br/>
        ///  - nBehaviorFlags: bitmask of OBJECT_VISUAL_TRANSFORM_BEHAVIOR_*.<br/>
        ///  - nRepeats: If > 0: N times, jump back to initial/from state after completing the transform. If -1: Do forever.<br/>
        ///  Returns the old/previous value.
        /// </summary>
        public static float SetObjectVisualTransform(uint oObject, ObjectVisualTransformType nTransform, float fValue, ObjectVisualTransformLerpType nLerpType = ObjectVisualTransformLerpType.None, float fLerpDuration = 0.0f, bool bPauseWithGame = true, ObjectVisualTransformDataScopeType nScope = ObjectVisualTransformDataScopeType.Base, ObjectVisualTransformBehaviorType nBehaviorFlags = ObjectVisualTransformBehaviorType.Default, int nRepeats = 0)
        {
            return NWN.Core.NWScript.SetObjectVisualTransform(oObject, (int)nTransform, fValue, (int)nLerpType, fLerpDuration, bPauseWithGame ? 1 : 0, (int)nScope, (int)nBehaviorFlags, nRepeats);
        }

        /// <summary>
        ///  Sets an integer material shader uniform override.<br/>
        ///  - sMaterial needs to be a material on that object.<br/>
        ///  - sParam needs to be a valid shader parameter already defined on the material.
        /// </summary>
        public static void SetMaterialShaderUniformInt(uint oObject, string sMaterial, string sParam, int nValue)
        {
            NWN.Core.NWScript.SetMaterialShaderUniformInt(oObject, sMaterial, sParam, nValue);
        }

        /// <summary>
        ///  Sets a vec4 material shader uniform override.<br/>
        ///  - sMaterial needs to be a material on that object.<br/>
        ///  - sParam needs to be a valid shader parameter already defined on the material.<br/>
        ///  - You can specify a single float value to set just a float, instead of a vec4.
        /// </summary>
        public static void SetMaterialShaderUniformVec4(uint oObject, string sMaterial, string sParam, float fValue1, float fValue2 = 0.0f, float fValue3 = 0.0f, float fValue4 = 0.0f)
        {
            NWN.Core.NWScript.SetMaterialShaderUniformVec4(oObject, sMaterial, sParam, fValue1, fValue2, fValue3, fValue4);
        }

        /// <summary>
        ///  Resets material shader parameters on the given object:<br/>
        ///  - Supply a material to only reset shader uniforms for meshes with that material.<br/>
        ///  - Supply a parameter to only reset shader uniforms of that name.<br/>
        ///  - Supply both to only reset shader uniforms of that name on meshes with that material.
        /// </summary>
        public static void ResetMaterialShaderUniforms(uint oObject, string sMaterial = "", string sParam = "")
        {
            NWN.Core.NWScript.ResetMaterialShaderUniforms(oObject, sMaterial, sParam);
        }

        /// <summary>
        ///  Vibrate the player's device or controller. Does nothing if vibration is not supported.<br/>
        ///  - nMotor is one of VIBRATOR_MOTOR_*<br/>
        ///  - fStrength is between 0.0 and 1.0<br/>
        ///  - fSeconds is the number of seconds to vibrate
        /// </summary>
        public static void Vibrate(uint oPlayer, int nMotor, float fStrength, float fSeconds)
        {
            NWN.Core.NWScript.Vibrate(oPlayer, nMotor, fStrength, fSeconds);
        }

        /// <summary>
        ///  Unlock an achievement for the given player who must be logged in.<br/>
        ///  - sId is the achievement ID on the remote server<br/>
        ///  - nLastValue is the previous value of the associated achievement stat<br/>
        ///  - nCurValue is the current value of the associated achievement stat<br/>
        ///  - nMaxValue is the maximum value of the associate achievement stat
        /// </summary>
        public static void UnlockAchievement(uint oPlayer, string sId, int nLastValue = 0, int nCurValue = 0, int nMaxValue = 0)
        {
            NWN.Core.NWScript.UnlockAchievement(oPlayer, sId, nLastValue, nCurValue, nMaxValue);
        }

        /// <summary>
        ///  Execute a script chunk.<br/>
        ///  The script chunk runs immediately, same as ExecuteScript().<br/>
        ///  The script is jitted in place and currently not cached: Each invocation will recompile the script chunk.<br/>
        ///  Note that the script chunk will run as if a separate script. This is not eval().<br/>
        ///  By default, the script chunk is wrapped into void main() {}. Pass in bWrapIntoMain = false to override.<br/>
        ///  Returns "" on success, or the compilation error.
        /// </summary>
        public static string ExecuteScriptChunk(string sScriptChunk, uint oObject = OBJECT_INVALID, bool bWrapIntoMain = true)
        {
            return NWN.Core.NWScript.ExecuteScriptChunk(sScriptChunk, oObject, bWrapIntoMain ? 1 : 0);
        }

        /// <summary>
        ///  Returns a UUID. This UUID will not be associated with any object.<br/>
        ///  The generated UUID is currently a v4.
        /// </summary>
        public static string GetRandomUUID()
        {
            return NWN.Core.NWScript.GetRandomUUID();
        }

        /// <summary>
        ///  Returns the given objects' UUID. This UUID is persisted across save boundaries,<br/>
        ///  like Save/RestoreCampaignObject and save games.<br/>
        /// <br/>
        ///  Thus, reidentification is only guaranteed in scenarios where players cannot introduce<br/>
        ///  new objects (i.e. servervault servers).<br/>
        /// <br/>
        ///  UUIDs are guaranteed to be unique in any single running game.<br/>
        /// <br/>
        ///  If a loaded object would collide with a UUID already present in the game, the<br/>
        ///  object receives no UUID and a warning is emitted to the log. Requesting a UUID<br/>
        ///  for the new object will generate a random one.<br/>
        /// <br/>
        ///  This UUID is useful to, for example:<br/>
        ///  - Safely identify servervault characters<br/>
        ///  - Track serialisable objects (like items or creatures) as they are saved to the<br/>
        ///    campaign DB - i.e. persistent storage chests or dropped items.<br/>
        ///  - Track objects across multiple game instances (in trusted scenarios).<br/>
        /// <br/>
        ///  Currently, the following objects can carry UUIDs:<br/>
        ///    Items, Creatures, Placeables, Triggers, Doors, Waypoints, Stores,<br/>
        ///    Encounters, Areas.<br/>
        /// <br/>
        ///  Will return "" (empty string) when the given object cannot carry a UUID.
        /// </summary>
        public static string GetObjectUUID(uint oObject)
        {
            return NWN.Core.NWScript.GetObjectUUID(oObject);
        }

        /// <summary>
        ///  Forces the given object to receive a new UUID, discarding the current value.
        /// </summary>
        public static void ForceRefreshObjectUUID(uint oObject)
        {
            NWN.Core.NWScript.ForceRefreshObjectUUID(oObject);
        }

        /// <summary>
        ///  Looks up a object on the server by it's UUID.<br/>
        ///  Returns OBJECT_INVALID if the UUID is not on the server.
        /// </summary>
        public static uint GetObjectByUUID(string sUUID)
        {
            return NWN.Core.NWScript.GetObjectByUUID(sUUID);
        }

        /// <summary>
        ///  Do not call. This does nothing on this platform except to return an error.
        /// </summary>
        public static void Reserved899()
        {
            NWN.Core.NWScript.Reserved899();
        }

        /// <summary>
        ///  Makes oPC load texture sNewName instead of sOldName.<br/>
        ///  If oPC is OBJECT_INVALID, it will apply the override to all active players<br/>
        ///  Setting sNewName to "" will clear the override and revert to original.
        /// </summary>
        public static void SetTextureOverride(string sOldName, string sNewName = "", uint oPC = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetTextureOverride(sOldName, sNewName, oPC);
        }

        /// <summary>
        ///  Displays sMsg on oPC's screen.<br/>
        ///  The message is displayed on top of whatever is on the screen, including UI elements<br/>
        ///   nX, nY - coordinates of the first character to be displayed. The value is in terms<br/>
        ///            of character 'slot' relative to the nAnchor anchor point.<br/>
        ///            If the number is negative, it is applied from the bottom/right.<br/>
        ///   nAnchor - SCREEN_ANCHOR_* constant<br/>
        ///   fLife - Duration in seconds until the string disappears.<br/>
        ///   nRGBA, nRGBA2 - Colors of the string in 0xRRGGBBAA format. String starts at nRGBA,<br/>
        ///                   but as it nears end of life, it will slowly blend into nRGBA2.<br/>
        ///   nID - Optional ID of a string. If not 0, subsequent calls to PostString will<br/>
        ///         remove the old string with the same ID, even if it's lifetime has not elapsed.<br/>
        ///         Only positive values are allowed.<br/>
        ///   sFont - If specified, use this custom font instead of default console font.
        /// </summary>
        public static void PostString(uint oPC, string sMsg, int nX = 0, int nY = 0, ScreenAnchorType nAnchor = ScreenAnchorType.TopLeft, float fLife = 10.0f, int nRGBA = 2147418367, int nRGBA2 = 2147418367, int nID = 0, string sFont = "")
        {
            NWN.Core.NWScript.PostString(oPC, sMsg, nX, nY, (int)nAnchor, fLife, nRGBA, nRGBA2, nID, sFont);
        }

        /// <summary>
        ///  Returns oCreature's spell school specialization in nClass (SPELL_SCHOOL_* constants)<br/>
        ///  Unless custom content is used, only Wizards have spell schools<br/>
        ///  Returns -1 on error
        /// </summary>
        public static SpellSchoolType GetSpecialization(uint oCreature, ClassType nClass = ClassType.Wizard)
        {
            return (SpellSchoolType)NWN.Core.NWScript.GetSpecialization(oCreature, (int)nClass);
        }

        /// <summary>
        ///  Returns oCreature's domain in nClass (DOMAIN_* constants)<br/>
        ///  nDomainIndex - 1 or 2<br/>
        ///  Unless custom content is used, only Clerics have domains<br/>
        ///  Returns -1 on error
        /// </summary>
        public static DomainType GetDomain(uint oCreature, int nDomainIndex = 1, ClassType nClass = ClassType.Cleric)
        {
            return (DomainType)NWN.Core.NWScript.GetDomain(oCreature, nDomainIndex, (int)nClass);
        }

        /// <summary>
        ///  Returns the patch build number of oPlayer (i.e. the 8193 out of "87.8193.35-29 abcdef01").<br/>
        ///  Returns 0 if the given object isn't a player or did not advertise their build info, or the<br/>
        ///  player version is old enough not to send this bit of build info to the server.
        /// </summary>
        public static int GetPlayerBuildVersionMajor(uint oPlayer)
        {
            return NWN.Core.NWScript.GetPlayerBuildVersionMajor(oPlayer);
        }

        /// <summary>
        ///  Returns the patch revision number of oPlayer (i.e. the 35 out of "87.8193.35-29 abcdef01").<br/>
        ///  Returns 0 if the given object isn't a player or did not advertise their build info, or the<br/>
        ///  player version is old enough not to send this bit of build info to the server.
        /// </summary>
        public static int GetPlayerBuildVersionMinor(uint oPlayer)
        {
            return NWN.Core.NWScript.GetPlayerBuildVersionMinor(oPlayer);
        }

        /// <summary>
        ///  Returns the script parameter value for a given parameter name.<br/>
        ///  Script parameters can be set for conversation scripts in the toolset's<br/>
        ///  Conversation Editor, or for any script with SetScriptParam().<br/>
        ///  * Will return "" if a parameter with the given name does not exist.
        /// </summary>
        public static string GetScriptParam(string sParamName)
        {
            return NWN.Core.NWScript.GetScriptParam(sParamName);
        }

        /// <summary>
        ///  Set a script parameter value for the next script to be run.<br/>
        ///  Call this function to set parameters right before calling ExecuteScript().
        /// </summary>
        public static void SetScriptParam(string sParamName, string sParamValue)
        {
            NWN.Core.NWScript.SetScriptParam(sParamName, sParamValue);
        }

        /// <summary>
        ///  Returns the number of uses per day remaining of the given item and item property.<br/>
        ///  * Will return 0 if the given item does not have the requested item property,<br/>
        ///    or the item property is not uses/day.
        /// </summary>
        public static int GetItemPropertyUsesPerDayRemaining(uint oItem, ItemProperty ip)
        {
            return NWN.Core.NWScript.GetItemPropertyUsesPerDayRemaining(oItem, ip);
        }

        /// <summary>
        ///  Sets the number of uses per day remaining of the given item and item property.<br/>
        ///  * Will do nothing if the given item and item property is not uses/day.<br/>
        ///  * Will constrain nUsesPerDay to the maximum allowed as the cost table defines.
        /// </summary>
        public static void SetItemPropertyUsesPerDayRemaining(uint oItem, ItemProperty ip, int nUsesPerDay)
        {
            NWN.Core.NWScript.SetItemPropertyUsesPerDayRemaining(oItem, ip, nUsesPerDay);
        }

        /// <summary>
        ///  Queue an action to use an active item property.<br/>
        ///  * oItem - item that has the item property to use<br/>
        ///  * ip - item property to use<br/>
        ///  * object oTarget - target<br/>
        ///  * nSubPropertyIndex - specify if your itemproperty has subproperties (such as subradial spells)<br/>
        ///  * bDecrementCharges - decrement charges if item property is limited
        /// </summary>
        public static void ActionUseItemOnObject(uint oItem, ItemProperty ip, uint oTarget, int nSubPropertyIndex = 0, bool bDecrementCharges = true)
        {
            NWN.Core.NWScript.ActionUseItemOnObject(oItem, ip, oTarget, nSubPropertyIndex, bDecrementCharges ? 1 : 0);
        }

        /// <summary>
        ///  Queue an action to use an active item property.<br/>
        ///  * oItem - item that has the item property to use<br/>
        ///  * ip - item property to use<br/>
        ///  * location lTarget - target location (must be in the same area as item possessor)<br/>
        ///  * nSubPropertyIndex - specify if your itemproperty has subproperties (such as subradial spells)<br/>
        ///  * bDecrementCharges - decrement charges if item property is limited
        /// </summary>
        public static void ActionUseItemAtLocation(uint oItem, ItemProperty ip, Location lTarget, int nSubPropertyIndex = 0, bool bDecrementCharges = true)
        {
            NWN.Core.NWScript.ActionUseItemAtLocation(oItem, ip, lTarget, nSubPropertyIndex, bDecrementCharges ? 1 : 0);
        }

        /// <summary>
        ///  Makes oPC enter a targeting mode, letting them select an object as a target<br/>
        ///  If a PC selects a target or cancels out, it will trigger the module OnPlayerTarget event.
        /// </summary>
        public static void EnterTargetingMode(uint oPC, ObjectType nValidObjectTypes = ObjectType.All, MouseCursorType nMouseCursorId = MouseCursorType.Magic, MouseCursorType nBadTargetCursor = MouseCursorType.NoMagic)
        {
            NWN.Core.NWScript.EnterTargetingMode(oPC, (int)nValidObjectTypes, (int)nMouseCursorId, (int)nBadTargetCursor);
        }

        /// <summary>
        ///  Gets the target object in the module OnPlayerTarget event.<br/>
        ///  Returns the area object when the target is the ground.<br/>
        ///  Note: returns OBJECT_INVALID if the player cancelled out of targeting mode.
        /// </summary>
        public static uint GetTargetingModeSelectedObject()
        {
            return NWN.Core.NWScript.GetTargetingModeSelectedObject();
        }

        /// <summary>
        ///  Gets the target position in the module OnPlayerTarget event.
        /// </summary>
        public static Vector3 GetTargetingModeSelectedPosition()
        {
            return NWN.Core.NWScript.GetTargetingModeSelectedPosition();
        }

        /// <summary>
        ///  Gets the player object that triggered the OnPlayerTarget event.
        /// </summary>
        public static uint GetLastPlayerToSelectTarget()
        {
            return NWN.Core.NWScript.GetLastPlayerToSelectTarget();
        }

        /// <summary>
        ///  Sets oObject's hilite color to nColor<br/>
        ///  The nColor format is 0xRRGGBB; -1 clears the color override.
        /// </summary>
        public static void SetObjectHiliteColor(uint oObject, int nColor = -1)
        {
            NWN.Core.NWScript.SetObjectHiliteColor(oObject, nColor);
        }
        /// <summary>
        ///  Sets the cursor (MOUSECURSOR_*) to use when hovering over oObject
        /// </summary>
        public static void SetObjectMouseCursor(uint oObject, int nCursor = -1)
        {
            NWN.Core.NWScript.SetObjectMouseCursor(oObject, nCursor);
        }

        /// <summary>
        ///  Returns true if the given player-controlled creature has DM privileges<br/>
        ///  gained through a player login (as opposed to the DM client).<br/>
        ///  Note: GetIsDM() also returns true for player creature DMs.
        /// </summary>
        public static bool GetIsPlayerDM(uint oCreature)
        {
            return NWN.Core.NWScript.GetIsPlayerDM(oCreature) == 1;
        }

        /// <summary>
        ///  Sets the detailed wind data for oArea<br/>
        ///  The predefined values in the toolset are:<br/>
        ///    NONE:  vDirection=(1.0, 1.0, 0.0), fMagnitude=0.0, fYaw=0.0,   fPitch=0.0<br/>
        ///    LIGHT: vDirection=(1.0, 1.0, 0.0), fMagnitude=1.0, fYaw=100.0, fPitch=3.0<br/>
        ///    HEAVY: vDirection=(1.0, 1.0, 0.0), fMagnitude=2.0, fYaw=150.0, fPitch=5.0
        /// </summary>
        public static void SetAreaWind(uint oArea, Vector3 vDirection, float fMagnitude, float fYaw, float fPitch)
        {
            NWN.Core.NWScript.SetAreaWind(oArea, vDirection, fMagnitude, fYaw, fPitch);
        }

        /// <summary>
        ///  Replace's oObject's texture sOld with sNew.<br/>
        ///  Specifying sNew = "" will restore the original texture.<br/>
        ///  If sNew cannot be found, the original texture will be restored.<br/>
        ///  sNew must refer to a simple texture, not PLT
        /// </summary>
        public static void ReplaceObjectTexture(uint oObject, string sOld, string sNew = "")
        {
            NWN.Core.NWScript.ReplaceObjectTexture(oObject, sOld, sNew);
        }

        /// <summary>
        ///  Destroys the given sqlite database, clearing out all data and schema.<br/>
        ///  This operation is _immediate_ and _irreversible_, even when<br/>
        ///  inside a transaction or running query.<br/>
        ///  Existing active/prepared sqlqueries will remain functional, but any references<br/>
        ///  to stored data or schema members will be invalidated.<br/>
        ///  oObject: Same as SqlPrepareQueryObject().<br/>
        ///           To reset a campaign database, please use DestroyCampaignDatabase().
        /// </summary>
        public static void SqlDestroyDatabase(uint oObject)
        {
            NWN.Core.NWScript.SqlDestroyDatabase(oObject);
        }

        /// <summary>
        ///  Returns "" if the last Sql command succeeded; or a human-readable error otherwise.<br/>
        ///  Additionally, all SQL errors are logged to the server log.<br/>
        ///  Additionally, all SQL errors are sent to all connected players.
        /// </summary>
        public static string SqlGetError(SQLQuery sqlQuery)
        {
            return NWN.Core.NWScript.SqlGetError(sqlQuery);
        }

        /// <summary>
        ///  Sets up a query.<br/>
        ///  This will NOT run the query; only make it available for parameter binding.<br/>
        ///  To run the query, you need to call SqlStep(); even if you do not<br/>
        ///  expect result data.<br/>
        ///  sDatabase: The name of a campaign database.<br/>
        ///             Note that when accessing campaign databases, you do not write access<br/>
        ///             to the builtin tables needed for CampaignDB functionality.<br/>
        ///  N.B.: You can pass sqlqueries into DelayCommand; HOWEVER<br/>
        ///        *** they will NOT survive a game save/load ***<br/>
        ///        Any commands on a restored sqlquery will fail.<br/>
        ///  N.B.: All uncommitted transactions left over at script termination are automatically rolled back.<br/>
        ///        This ensures that no database handle will be left in an unusable state.<br/>
        ///  Please check the SQLite_README.txt file in lang/en/docs/ for the list of builtin functions.
        /// </summary>
        public static SQLQuery SqlPrepareQueryCampaign(string sDatabase, string sQuery)
        {
            return NWN.Core.NWScript.SqlPrepareQueryCampaign(sDatabase, sQuery);
        }

        /// <summary>
        ///  Sets up a query.<br/>
        ///  This will NOT run the query; only make it available for parameter binding.<br/>
        ///  To run the query, you need to call SqlStep(); even if you do not<br/>
        ///  expect result data.<br/>
        ///  oObject: Can be either the module (GetModule()), or a player character.<br/>
        ///           The database is persisted to savegames in case of the module,<br/>
        ///           and to character files in case of a player characters.<br/>
        ///           Other objects cannot carry databases, and this function call<br/>
        ///           will error for them.<br/>
        ///  N.B: Databases on objects (especially player characters!) should be kept<br/>
        ///       to a reasonable size. Delete old data you no longer need.<br/>
        ///       If you attempt to store more than a few megabytes of data on a<br/>
        ///       player creature, you may have a bad time.<br/>
        ///  N.B.: You can pass sqlqueries into DelayCommand; HOWEVER<br/>
        ///        *** they will NOT survive a game save/load ***<br/>
        ///        Any commands on a restored sqlquery will fail.<br/>
        ///  N.B.: All uncommitted transactions left over at script termination are automatically rolled back.<br/>
        ///        This ensures that no database handle will be left in an unusable state.<br/>
        ///  Please check the SQLite_README.txt file in lang/en/docs/ for the list of builtin functions.
        /// </summary>
        public static SQLQuery SqlPrepareQueryObject(uint oObject, string sQuery)
        {
            return NWN.Core.NWScript.SqlPrepareQueryObject(oObject, sQuery);
        }

        /// <summary>
        ///  Bind an integer to a named parameter of the given prepared query.<br/>
        ///  Example:<br/>
        ///    sqlquery v = SqlPrepareQueryObject(GetModule(), "insert into test (col) values (@myint);");<br/>
        ///    SqlBindInt(v, "@myint", 5);<br/>
        ///    SqlStep(v);
        /// </summary>
        public static void SqlBindInt(SQLQuery sqlQuery, string sParam, int nValue)
        {
            NWN.Core.NWScript.SqlBindInt(sqlQuery, sParam, nValue);
        }

        /// <summary>
        ///  Bind a float to a named parameter of the given prepared query.
        /// </summary>
        public static void SqlBindFloat(SQLQuery sqlQuery, string sParam, float fFloat)
        {
            NWN.Core.NWScript.SqlBindFloat(sqlQuery, sParam, fFloat);
        }

        /// <summary>
        ///  Bind a string to a named parameter of the given prepared query.
        /// </summary>
        public static void SqlBindString(SQLQuery sqlQuery, string sParam, string sString)
        {
            NWN.Core.NWScript.SqlBindString(sqlQuery, sParam, sString);
        }

        /// <summary>
        ///  Bind a vector to a named parameter of the given prepared query.
        /// </summary>
        public static void SqlBindVector(SQLQuery sqlQuery, string sParam, Vector3 vVector)
        {
            NWN.Core.NWScript.SqlBindVector(sqlQuery, sParam, vVector);
        }

        /// <summary>
        ///  Bind a object to a named parameter of the given prepared query.<br/>
        ///  Objects are serialized, NOT stored as a reference!<br/>
        ///  Currently supported object types: Creatures, Items, Placeables, Waypoints, Stores, Doors, Triggers, Encounters, Areas (CAF format)<br/>
        ///  If bSaveObjectState is true, local vars, effects, action queue, and transition info (triggers, doors) are saved out<br/>
        ///  (except for Combined Area Format, which always has object state saved out).
        /// </summary>
        public static void SqlBindObject(SQLQuery sqlQuery, string sParam, uint oObject, bool bSaveObjectState = false)
        {
            NWN.Core.NWScript.SqlBindObject(sqlQuery, sParam, oObject, bSaveObjectState ? 1 : 0);
        }

        /// <summary>
        ///  Executes the given query and fetches a row; returning true if row data was<br/>
        ///  made available; false otherwise. Note that this will return false even if<br/>
        ///  the query ran successfully but did not return data.<br/>
        ///  You need to call SqlPrepareQuery() and potentially SqlBind* before calling this.<br/>
        ///  Example:<br/>
        ///    sqlquery n = SqlPrepareQueryObject(GetFirstPC(), "select widget from widgets;");<br/>
        ///    while (SqlStep(n))<br/>
        ///      SendMessageToPC(GetFirstPC(), "Found widget: " + SqlGetString(n, 0));
        /// </summary>
        public static int SqlStep(SQLQuery sqlQuery)
        {
            return NWN.Core.NWScript.SqlStep(sqlQuery);
        }

        /// <summary>
        ///  Retrieve a column cast as an integer of the currently stepped row.<br/>
        ///  You can call this after SqlStep() returned true.<br/>
        ///  In case of error, 0 will be returned.<br/>
        ///  In traditional fashion, nIndex starts at 0.
        /// </summary>
        public static int SqlGetInt(SQLQuery sqlQuery, int nIndex)
        {
            return NWN.Core.NWScript.SqlGetInt(sqlQuery, nIndex);
        }

        /// <summary>
        ///  Retrieve a column cast as a float of the currently stepped row.<br/>
        ///  You can call this after SqlStep() returned true.<br/>
        ///  In case of error, 0.0f will be returned.<br/>
        ///  In traditional fashion, nIndex starts at 0.
        /// </summary>
        public static float SqlGetFloat(SQLQuery sqlQuery, int nIndex)
        {
            return NWN.Core.NWScript.SqlGetFloat(sqlQuery, nIndex);
        }

        /// <summary>
        ///  Retrieve a column cast as a string of the currently stepped row.<br/>
        ///  You can call this after SqlStep() returned true.<br/>
        ///  In case of error, a empty string will be returned.<br/>
        ///  In traditional fashion, nIndex starts at 0.
        /// </summary>
        public static string SqlGetString(SQLQuery sqlQuery, int nIndex)
        {
            return NWN.Core.NWScript.SqlGetString(sqlQuery, nIndex);
        }

        /// <summary>
        ///  Retrieve a vector of the currently stepped query.<br/>
        ///  You can call this after SqlStep() returned true.<br/>
        ///  In case of error, a zero vector will be returned.<br/>
        ///  In traditional fashion, nIndex starts at 0.
        /// </summary>
        public static Vector3 SqlGetVector(SQLQuery sqlQuery, int nIndex)
        {
            return NWN.Core.NWScript.SqlGetVector(sqlQuery, nIndex);
        }

        /// <summary>
        ///  Retrieve a object of the currently stepped query.<br/>
        ///  You can call this after SqlStep() returned true.<br/>
        ///  The object will be spawned into a inventory if it is a item and the receiver<br/>
        ///  has the capability to receive it, otherwise at lSpawnAt.<br/>
        ///  Objects are serialized, NOT stored as a reference!<br/>
        ///  In case of error, INVALID_OBJECT will be returned.<br/>
        ///  In traditional fashion, nIndex starts at 0.<br/>
        ///  If bLoadObjectState is true, local vars, effects, action queue, and transition info (triggers, doors) are read in.
        /// </summary>
        public static uint SqlGetObject(SQLQuery sqlQuery, int nIndex, Location lSpawnAt, uint oInventory = OBJECT_INVALID, bool bLoadObjectState = false)
        {
            return NWN.Core.NWScript.SqlGetObject(sqlQuery, nIndex, lSpawnAt, oInventory, bLoadObjectState ? 1 : 0);
        }

        /// <summary>
        ///  Convert sHex, a string containing a hexadecimal object id,<br/>
        ///  into a object reference. Counterpart to ObjectToString().
        /// </summary>
        public static uint StringToObject(string sHex)
        {
            return NWN.Core.NWScript.StringToObject(sHex);
        }

        /// <summary>
        ///  Sets the current hitpoints of oObject.<br/>
        ///  * You cannot destroy or revive objects or creatures with this function.<br/>
        ///  * For currently dying PCs, you can only set hitpoints in the range of -9 to 0.<br/>
        ///  * All other objects need to be alive and the range is clamped to 1 and max hitpoints.<br/>
        ///  * This is not considered damage (or healing). It circumvents all combat logic, including damage resistance and reduction.<br/>
        ///  * This is not considered a friendly or hostile combat action. It will not affect factions, nor will it trigger script events.<br/>
        ///  * This will not advise player parties in the combat log.
        /// </summary>
        public static void SetCurrentHitPoints(uint oObject, int nHitPoints)
        {
            NWN.Core.NWScript.SetCurrentHitPoints(oObject, nHitPoints);
        }

        /// <summary>
        ///  Returns the currently executing event (EVENT_SCRIPT_*) or 0 if not determinable.<br/>
        ///  Note: Will return 0 in DelayCommand/AssignCommand.<br/>
        ///  * bInheritParent: If true, ExecuteScript(Chunk) will inherit their event ID from their parent event.<br/>
        ///                    If false, it will return the event ID of the current script, which may be 0.<br/>
        /// <br/>
        ///  Some events can run in the same script context as a previous event (for example: CreatureOnDeath, CreatureOnDamaged)<br/>
        ///  In cases like these calling the function with bInheritParent = true will return the wrong event ID.
        /// </summary>
        public static EventScriptType GetCurrentlyRunningEvent(bool bInheritParent = true)
        {
            return (EventScriptType)NWN.Core.NWScript.GetCurrentlyRunningEvent(bInheritParent ? 1 : 0);
        }

        /// <summary>
        ///  Get the integer parameter of eEffect at nIndex.<br/>
        ///  * nIndex bounds: 0 >= nIndex < 8.<br/>
        ///  * Some experimentation will be needed to find the right index for the value you wish to determine.<br/>
        ///  Returns: the value or 0 on error/when not set.
        /// </summary>
        public static int GetEffectInteger(Effect eEffect, int nIndex)
        {
            return NWN.Core.NWScript.GetEffectInteger(eEffect, nIndex);
        }

        /// <summary>
        ///  Get the float parameter of eEffect at nIndex.<br/>
        ///  * nIndex bounds: 0 >= nIndex < 4.<br/>
        ///  * Some experimentation will be needed to find the right index for the value you wish to determine.<br/>
        ///  Returns: the value or 0.0f on error/when not set.
        /// </summary>
        public static float GetEffectFloat(Effect eEffect, int nIndex)
        {
            return NWN.Core.NWScript.GetEffectFloat(eEffect, nIndex);
        }

        /// <summary>
        ///  Get the string parameter of eEffect at nIndex.<br/>
        ///  * nIndex bounds: 0 >= nIndex < 6.<br/>
        ///  * Some experimentation will be needed to find the right index for the value you wish to determine.<br/>
        ///  Returns: the value or "" on error/when not set.
        /// </summary>
        public static string GetEffectString(Effect eEffect, int nIndex)
        {
            return NWN.Core.NWScript.GetEffectString(eEffect, nIndex);
        }

        /// <summary>
        ///  Get the object parameter of eEffect at nIndex.<br/>
        ///  * nIndex bounds: 0 >= nIndex < 4.<br/>
        ///  * Some experimentation will be needed to find the right index for the value you wish to determine.<br/>
        ///  Returns: the value or OBJECT_INVALID on error/when not set.
        /// </summary>
        public static uint GetEffectObject(Effect eEffect, int nIndex)
        {
            return NWN.Core.NWScript.GetEffectObject(eEffect, nIndex);
        }

        /// <summary>
        ///  Get the vector parameter of eEffect at nIndex.<br/>
        ///  * nIndex bounds: 0 >= nIndex < 2.<br/>
        ///  * Some experimentation will be needed to find the right index for the value you wish to determine.<br/>
        ///  Returns: the value or {0.0f, 0.0f, 0.0f} on error/when not set.
        /// </summary>
        public static Vector3 GetEffectVector(Effect eEffect, int nIndex)
        {
            return NWN.Core.NWScript.GetEffectVector(eEffect, nIndex);
        }

        /// <summary>
        ///  Check if nBaseItemType fits in oTarget's inventory.<br/>
        ///  Note: Does not check inside any container items possessed by oTarget.<br/>
        ///  * nBaseItemType: a BASE_ITEM_* constant.<br/>
        ///  * oTarget: a valid creature, placeable or item.<br/>
        ///  Returns: true if the baseitem type fits, false if not or on error.
        /// </summary>
        public static bool GetBaseItemFitsInInventory(BaseItemType nBaseItemType, uint oTarget)
        {
            return NWN.Core.NWScript.GetBaseItemFitsInInventory((int)nBaseItemType, oTarget) == 1;
        }

        /// <summary>
        ///  Get oObject's local cassowary variable reference sVarName<br/>
        ///  * Return value on error: empty solver<br/>
        ///  * NB: cassowary types are references, same as objects.<br/>
        ///    Unlike scalars such as int and string, solver references share the same data.<br/>
        ///    Modifications made to one reference are reflected on others.
        /// </summary>
        public static Cassowary GetLocalCassowary(uint oObject, string sVarName)
        {
            return NWN.Core.NWScript.GetLocalCassowary(oObject, sVarName);
        }

        /// <summary>
        ///  Set a reference to the given solver on oObject.<br/>
        ///  * NB: cassowary types are references, same as objects.<br/>
        ///    Unlike scalars such as int and string, solver references share the same data.<br/>
        ///    Modifications made to one reference are reflected on others.
        /// </summary>
        public static void SetLocalCassowary(uint oObject, string sVarName, Cassowary cSolver)
        {
            NWN.Core.NWScript.SetLocalCassowary(oObject, sVarName, cSolver);
        }

        /// <summary>
        ///  Delete local solver reference.<br/>
        ///  * NB: cassowary types are references, same as objects.<br/>
        ///    Unlike scalars such as int and string, solver references share the same data.<br/>
        ///    Modifications made to one reference are reflected on others.
        /// </summary>
        public static void DeleteLocalCassowary(uint oObject, string sVarName)
        {
            NWN.Core.NWScript.DeleteLocalCassowary(oObject, sVarName);
        }

        /// <summary>
        ///  Clear out this solver, removing all state, constraints and suggestions.<br/>
        ///  This is provided as a convenience if you wish to reuse a cassowary variable.<br/>
        ///  It is not necessary to call this for solvers you simply want to let go out of scope.
        /// </summary>
        public static void CassowaryReset(Cassowary cSolver)
        {
            NWN.Core.NWScript.CassowaryReset(cSolver);
        }

        /// <summary>
        ///  Add a constraint to the system.<br/>
        ///  * The constraint needs to be a valid comparison equation, one of: >=, ==, <=.<br/>
        ///  * This implementation is a linear constraint solver.<br/>
        ///  * You cannot multiply or divide variables and expressions with each other.<br/>
        ///    Doing so will result in a error when attempting to add the constraint.<br/>
        ///    (You can, of course, multiply or divide by constants).<br/>
        ///  * fStrength must be >= CASSOWARY_STRENGTH_WEAK &amp;&amp; <= CASSOWARY_STRENGTH_REQUIRED.<br/>
        ///  * Any referenced variables can be retrieved with CassowaryGetValue().<br/>
        ///  * Returns "" on success, or the parser/constraint system error message.
        /// </summary>
        public static string CassowaryConstrain(Cassowary cSolver, string sConstraint, float fStrength = CassowaryStrengthType.Required)
        {
            return NWN.Core.NWScript.CassowaryConstrain(cSolver, sConstraint, fStrength);
        }

        /// <summary>
        ///  Suggest a value to the solver.<br/>
        ///  * Edit variables are soft constraints and exist as an optimisation for complex systems.<br/>
        ///    You can do the same with Constrain("v == 5", CASSOWARY_STRENGTH_xxx); but edit variables<br/>
        ///    allow you to suggest values without having to rebuild the solver.<br/>
        ///  * fStrength must be >= CASSOWARY_STRENGTH_WEAK &amp;&amp; < CASSOWARY_STRENGTH_REQUIRED<br/>
        ///    Suggested values cannot be required, as suggesting a value must not invalidate the solver.
        /// </summary>
        public static void CassowarySuggestValue(Cassowary cSolver, string sVarName, float fValue, float fStrength = CassowaryStrengthType.Strong)
        {
            NWN.Core.NWScript.CassowarySuggestValue(cSolver, sVarName, fValue, fStrength);
        }

        /// <summary>
        ///  Get the value for the given variable, or 0.0 on error.
        /// </summary>
        public static float CassowaryGetValue(Cassowary cSolver, string sVarName)
        {
            return NWN.Core.NWScript.CassowaryGetValue(cSolver, sVarName);
        }

        /// <summary>
        ///  Gets a printable debug state of the given solver, which may help you debug<br/>
        ///  complex systems.
        /// </summary>
        public static string CassowaryDebug(Cassowary cSolver)
        {
            return NWN.Core.NWScript.CassowaryDebug(cSolver);
        }

        /// <summary>
        /// Overrides a given strref to always return sValue instead of what is in the TLK file.<br/>
        ///  Setting sValue to &amp;quot;&amp;quot; will delete the override
        /// </summary>
        public static void SetTlkOverride(int nStrRef, string sValue = "")
        {
            NWN.Core.NWScript.SetTlkOverride(nStrRef, sValue);
        }

        /// <summary>
        /// Constructs a custom itemproperty given all the parameters explicitly.<br/>
        ///  This function can be used in place of all the other ItemPropertyXxx constructors<br/>
        ///  Use GetItemProperty{Type,SubType,CostTableValue,Param1Value} to see the values for a given itemproperty.
        /// </summary>
        public static ItemProperty ItemPropertyCustom(int nType, int nSubType = -1, int nCostTableValue = -1, int nParam1Value = -1)
        {
            return NWN.Core.NWScript.ItemPropertyCustom(nType, nSubType, nCostTableValue, nParam1Value);
        }

        /// <summary>
        ///  Create a RunScript effect.<br/>
        ///  Notes: When applied as instant effect, only sOnAppliedScript will fire.<br/>
        ///         In the scripts, OBJECT_SELF will be the object the effect is applied to.<br/>
        ///  * sOnAppliedScript: An optional script to execute when the effect is applied.<br/>
        ///  * sOnRemovedScript: An optional script to execute when the effect is removed.<br/>
        ///  * sOnIntervalScript: An optional script to execute every fInterval seconds.<br/>
        ///  * fInterval: The interval in seconds, must be >0.0f if an interval script is set.<br/>
        ///               Very low values may have an adverse effect on performance.<br/>
        ///  * sData: An optional string of data saved in the effect, retrievable with GetEffectString() at index 0.
        /// </summary>
        public static Effect EffectRunScript(string sOnAppliedScript = "", string sOnRemovedScript = "", string sOnIntervalScript = "", float fInterval = 0.0f, string sData = "")
        {
            return NWN.Core.NWScript.EffectRunScript(sOnAppliedScript, sOnRemovedScript, sOnIntervalScript, fInterval, sData);
        }

        /// <summary>
        ///  Get the effect that last triggered an EffectRunScript() script.<br/>
        ///  Note: This can be used to get the creator or tag, among others, of the EffectRunScript() in one of its scripts.<br/>
        ///  * Returns an effect of type EFFECT_TYPE_INVALIDEFFECT when called outside of an EffectRunScript() script.
        /// </summary>
        public static Effect GetLastRunScriptEffect()
        {
            return NWN.Core.NWScript.GetLastRunScriptEffect();
        }

        /// <summary>
        ///  Get the script type (RUNSCRIPT_EFFECT_SCRIPT_TYPE_*) of the last triggered EffectRunScript() script.<br/>
        ///  * Returns 0 when called outside of an EffectRunScript() script.
        /// </summary>
        public static RunScriptEffectScriptType GetLastRunScriptEffectScriptType()
        {
            return (RunScriptEffectScriptType)NWN.Core.NWScript.GetLastRunScriptEffectScriptType();
        }

        /// <summary>
        ///  Hides the effect icon of eEffect and of all effects currently linked to it.
        /// </summary>
        public static Effect HideEffectIcon(Effect eEffect)
        {
            return NWN.Core.NWScript.HideEffectIcon(eEffect);
        }

        /// <summary>
        ///  Create an Icon effect.<br/>
        ///  * nIconID: The effect icon (EFFECT_ICON_*) to display.<br/>
        ///             Using the icon for Poison/Disease will also color the health bar green/brown, useful to simulate custom poisons/diseases.<br/>
        ///  Returns an effect of type EFFECT_TYPE_INVALIDEFFECT when nIconID is < 1 or > 255.
        /// </summary>
        public static Effect EffectIcon(int nIconID)
        {
            return NWN.Core.NWScript.EffectIcon(nIconID);
        }

        /// <summary>
        ///  Gets the player that last triggered the module OnPlayerGuiEvent event.
        /// </summary>
        public static uint GetLastGuiEventPlayer()
        {
            return NWN.Core.NWScript.GetLastGuiEventPlayer();
        }

        /// <summary>
        ///  Gets the last triggered GUIEVENT_* in the module OnPlayerGuiEvent event.
        /// </summary>
        public static GuiEventType GetLastGuiEventType()
        {
            return (GuiEventType)NWN.Core.NWScript.GetLastGuiEventType();
        }

        /// <summary>
        ///  Gets an optional integer of specific gui events in the module OnPlayerGuiEvent event.<br/>
        ///  * GUIEVENT_CHATBAR_*: The selected chat channel. Does not indicate the actual used channel.<br/>
        ///                        0 = Shout, 1 = Whisper, 2 = Talk, 3 = Party, 4 = DM<br/>
        ///  * GUIEVENT_CHARACTERSHEET_SKILL_SELECT: The skill ID.<br/>
        ///  * GUIEVENT_CHARACTERSHEET_FEAT_SELECT: The feat ID.<br/>
        ///  * GUIEVENT_EFFECTICON_CLICK: The effect icon ID (EFFECT_ICON_*)<br/>
        ///  * GUIEVENT_DISABLED_PANEL_ATTEMPT_OPEN: The GUI_PANEL_* the player attempted to open.<br/>
        ///  * GUIEVENT_QUICKCHAT_SELECT: The hotkey character representing the option<br/>
        ///  * GUIEVENT_EXAMINE_OBJECT: A GUI_PANEL_EXAMINE_* constant
        /// </summary>
        public static int GetLastGuiEventInteger()
        {
            return NWN.Core.NWScript.GetLastGuiEventInteger();
        }

        /// <summary>
        ///  Gets an optional object of specific gui events in the module OnPlayerGuiEvent event.<br/>
        ///  * GUIEVENT_MINIMAP_MAPPIN_CLICK: The waypoint the map note is attached to.<br/>
        ///  * GUIEVENT_CHARACTERSHEET_*_SELECT: The owner of the character sheet.<br/>
        ///  * GUIEVENT_PLAYERLIST_PLAYER_CLICK: The player clicked on.<br/>
        ///  * GUIEVENT_PARTYBAR_PORTRAIT_CLICK: The creature clicked on.<br/>
        ///  * GUIEVENT_DISABLED_PANEL_ATTEMPT_OPEN: For GUI_PANEL_CHARACTERSHEET, the owner of the character sheet.<br/>
        ///                                          For GUI_PANEL_EXAMINE_*, the object being examined.<br/>
        ///  * GUIEVENT_*SELECT_CREATURE: The creature that was (un)selected<br/>
        ///  * GUIEVENT_EXAMINE_OBJECT: The object being examined.<br/>
        ///  * GUIEVENT_CHATLOG_PORTRAIT_CLICK: The owner of the portrait.<br/>
        ///  * GUIEVENT_PLAYERLIST_PLAYER_TELL: The selected player.
        /// </summary>
        public static uint GetLastGuiEventObject()
        {
            return NWN.Core.NWScript.GetLastGuiEventObject();
        }

        /// <summary>
        ///  Disable a gui panel for the client that controls oPlayer.<br/>
        ///  Notes: Will close the gui panel if currently open, except GUI_PANEL_LEVELUP / GUI_PANEL_GOLD_*<br/>
        ///         Does not persist through relogging or in savegames.<br/>
        ///         Will fire a GUIEVENT_DISABLED_PANEL_ATTEMPT_OPEN OnPlayerGuiEvent for some gui panels if a player attempts to open them.<br/>
        ///         You can still force show a panel with PopUpGUIPanel().<br/>
        ///         You can still force examine an object with ActionExamine().<br/>
        ///  * nGuiPanel: A GUI_PANEL_* constant, except GUI_PANEL_PLAYER_DEATH.
        /// </summary>
        public static void SetGuiPanelDisabled(uint oPlayer, GuiPanelType nGuiPanel, bool bDisabled, uint oTarget = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetGuiPanelDisabled(oPlayer, (int)nGuiPanel, bDisabled ? 1 : 0, oTarget);
        }

        /// <summary>
        ///  Gets the ID (1..8) of the last tile action performed in OnPlayerTileAction
        /// </summary>
        public static int GetLastTileActionId()
        {
            return NWN.Core.NWScript.GetLastTileActionId();
        }

        /// <summary>
        ///  Gets the target position in the module OnPlayerTileAction event.
        /// </summary>
        public static Vector3 GetLastTileActionPosition()
        {
            return NWN.Core.NWScript.GetLastTileActionPosition();
        }

        /// <summary>
        ///  Gets the player object that triggered the OnPlayerTileAction event.
        /// </summary>
        public static uint GetLastPlayerToDoTileAction()
        {
            return NWN.Core.NWScript.GetLastPlayerToDoTileAction();
        }

        /// <summary>
        ///  Parse the given string as a valid json value, and returns the corresponding type.<br/>
        ///  Returns a JSON_TYPE_NULL on error.<br/>
        ///  Check JsonGetError() to see the parse error, if any.<br/>
        ///  NB: The parsed string needs to be in game-local encoding, but the generated json structure<br/>
        ///      will contain UTF-8 data.
        /// </summary>
        public static Json JsonParse(string sJson)
        {
            return NWN.Core.NWScript.JsonParse(sJson);
        }

        /// <summary>
        ///  Dump the given json value into a string that can be read back in via JsonParse.<br/>
        ///  nIndent describes the indentation level for pretty-printing; a value of -1 means no indentation and no linebreaks.<br/>
        ///  Returns a string describing JSON_TYPE_NULL on error, or if oObject is not serializable, with JsonGetError() filled in.<br/>
        ///  NB: The dumped string is in game-local encoding, with all non-ascii characters escaped.
        /// </summary>
        public static string JsonDump(Json jValue, int nIndent = -1)
        {
            return NWN.Core.NWScript.JsonDump(jValue, nIndent);
        }

        /// <summary>
        ///  Describes the type of the given json value.<br/>
        ///  Returns JSON_TYPE_NULL if the value is empty.
        /// </summary>
        public static JsonType JsonGetType(Json jValue)
        {
            return (JsonType)NWN.Core.NWScript.JsonGetType(jValue);
        }

        /// <summary>
        ///  Returns the length of the given json type.<br/>
        ///  For objects, returns the number of top-level keys present.<br/>
        ///  For arrays, returns the number of elements.<br/>
        ///  Null types are of size 0.<br/>
        ///  All other types return 1.
        /// </summary>
        public static int JsonGetLength(Json jValue)
        {
            return NWN.Core.NWScript.JsonGetLength(jValue);
        }

        /// <summary>
        ///  Returns the error message if the value has errored out.<br/>
        ///  Currently only describes parse errors.
        /// </summary>
        public static string JsonGetError(Json jValue)
        {
            return NWN.Core.NWScript.JsonGetError(jValue);
        }

        /// <summary>
        ///  Create a NULL json value, seeded with a optional error message for JsonGetError().<br/>
        ///  You can say JSON_NULL for default parameters on functions to initialise with a null value.
        /// </summary>
        public static Json JsonNull(string sError = "")
        {
            return NWN.Core.NWScript.JsonNull(sError);
        }

        /// <summary>
        ///  Create a empty json object.<br/>
        ///  You can say JSON_OBJECT for default parameters on functions to initialise with an empty object.
        /// </summary>
        public static Json JsonObject()
        {
            return NWN.Core.NWScript.JsonObject();
        }

        /// <summary>
        ///  Create a empty json array.<br/>
        ///  You can say JSON_ARRAY for default parameters on functions to initialise with an empty array.
        /// </summary>
        public static Json JsonArray()
        {
            return NWN.Core.NWScript.JsonArray();
        }

        /// <summary>
        ///  Create a json string value.<br/>
        ///  You can say JSON_STRING for default parameters on functions to initialise with a empty string.<br/>
        ///  NB: Strings are encoded to UTF-8 from the game-local charset.
        /// </summary>
        public static Json JsonString(string sValue)
        {
            return NWN.Core.NWScript.JsonString(sValue);
        }

        /// <summary>
        ///  Create a json integer value.
        /// </summary>
        public static Json JsonInt(int nValue)
        {
            return NWN.Core.NWScript.JsonInt(nValue);
        }

        /// <summary>
        ///  Create a json floating point value.
        /// </summary>
        public static Json JsonFloat(float fValue)
        {
            return NWN.Core.NWScript.JsonFloat(fValue);
        }

        /// <summary>
        ///  Create a json bool valye.<br/>
        ///  You can say JSON_TRUE or JSON_FALSE for default parameters on functions to initialise with a bool.
        /// </summary>
        public static Json JsonBool(bool bValue)
        {
            return NWN.Core.NWScript.JsonBool(bValue ? 1 : 0);
        }

        /// <summary>
        ///  Returns a string representation of the json value.<br/>
        ///  Returns "" if the value cannot be represented as a string, or is empty.<br/>
        ///  NB: Strings are decoded from UTF-8 to the game-local charset.
        /// </summary>
        public static string JsonGetString(Json jValue)
        {
            return NWN.Core.NWScript.JsonGetString(jValue);
        }

        /// <summary>
        ///  Returns a int representation of the json value, casting where possible.<br/>
        ///  Returns 0 if the value cannot be represented as a int.<br/>
        ///  Use this to parse json bool types.<br/>
        ///  NB: This will narrow down to signed 32 bit, as that is what NWScript int is.<br/>
        ///      If you are trying to read a 64 bit or unsigned integer that doesn't fit into int32, you will lose data.<br/>
        ///      You will not lose data if you keep the value as a json element (via Object/ArrayGet).
        /// </summary>
        public static int JsonGetInt(Json jValue)
        {
            return NWN.Core.NWScript.JsonGetInt(jValue);
        }

        /// <summary>
        ///  Returns a float representation of the json value, casting where possible.<br/>
        ///  Returns 0.0 if the value cannot be represented as a float.<br/>
        ///  NB: This will narrow doubles down to float.<br/>
        ///      If you are trying to read a double, you will potentially lose precision.<br/>
        ///      You will not lose data if you keep the value as a json element (via Object/ArrayGet).
        /// </summary>
        public static float JsonGetFloat(Json jValue)
        {
            return NWN.Core.NWScript.JsonGetFloat(jValue);
        }

        /// <summary>
        ///  Returns a json array containing all keys of jObject.<br/>
        ///  Returns a empty array if the object is empty or not a json object, with JsonGetError() filled in.
        /// </summary>
        public static Json JsonObjectKeys(Json jObject)
        {
            return NWN.Core.NWScript.JsonObjectKeys(jObject);
        }

        /// <summary>
        ///  Returns the key value of sKey on the object jObect.<br/>
        ///  Returns a json null value if jObject is not a object or sKey does not exist on the object, with JsonGetError() filled in.
        /// </summary>
        public static Json JsonObjectGet(Json jObject, string sKey)
        {
            return NWN.Core.NWScript.JsonObjectGet(jObject, sKey);
        }

        /// <summary>
        ///  Returns a modified copy of jObject with the key at sKey set to jValue.<br/>
        ///  Returns a json null value if jObject is not a object, with JsonGetError() filled in.
        /// </summary>
        public static Json JsonObjectSet(Json jObject, string sKey, Json jValue)
        {
            return NWN.Core.NWScript.JsonObjectSet(jObject, sKey, jValue);
        }

        /// <summary>
        ///  Returns a modified copy of jObject with the key at sKey deleted.<br/>
        ///  Returns a json null value if jObject is not a object, with JsonGetError() filled in.
        /// </summary>
        public static Json JsonObjectDel(Json jObject, string sKey)
        {
            return NWN.Core.NWScript.JsonObjectDel(jObject, sKey);
        }

        /// <summary>
        ///  Gets the json object at jArray index position nIndex.<br/>
        ///  Returns a json null value if the index is out of bounds, with JsonGetError() filled in.
        /// </summary>
        public static Json JsonArrayGet(Json jArray, int nIndex)
        {
            return NWN.Core.NWScript.JsonArrayGet(jArray, nIndex);
        }

        /// <summary>
        ///  Returns a modified copy of jArray with position nIndex set to jValue.<br/>
        ///  Returns a json null value if jArray is not actually an array, with JsonGetError() filled in.<br/>
        ///  Returns a json null value if nIndex is out of bounds, with JsonGetError() filled in.
        /// </summary>
        public static Json JsonArraySet(Json jArray, int nIndex, Json jValue)
        {
            return NWN.Core.NWScript.JsonArraySet(jArray, nIndex, jValue);
        }

        /// <summary>
        ///  Returns a modified copy of jArray with jValue inserted at position nIndex.<br/>
        ///  All succeeding objects in the array will move by one.<br/>
        ///  By default (-1), inserts objects at the end of the array ("push").<br/>
        ///  nIndex = 0 inserts at the beginning of the array.<br/>
        ///  Returns a json null value if jArray is not actually an array, with JsonGetError() filled in.<br/>
        ///  Returns a json null value if nIndex is not 0 or -1 and out of bounds, with JsonGetError() filled in.
        /// </summary>
        public static Json JsonArrayInsert(Json jArray, Json jValue, int nIndex = -1)
        {
            return NWN.Core.NWScript.JsonArrayInsert(jArray, jValue, nIndex);
        }

        /// <summary>
        ///  Returns a modified copy of jArray with the element at position nIndex removed,<br/>
        ///  and the array resized by one.<br/>
        ///  Returns a json null value if jArray is not actually an array, with JsonGetError() filled in.<br/>
        ///  Returns a json null value if nIndex is out of bounds, with JsonGetError() filled in.
        /// </summary>
        public static Json JsonArrayDel(Json jArray, int nIndex)
        {
            return NWN.Core.NWScript.JsonArrayDel(jArray, nIndex);
        }

        /// <summary>
        ///  Transforms the given object into a json structure.<br/>
        ///  The json format is compatible with what https://github.com/niv/neverwinter.nim@1.4.3+ emits.<br/>
        ///  Returns the null json type on errors, or if oObject is not serializable, with JsonGetError() filled in.<br/>
        ///  Supported object types: creature, item, trigger, placeable, door, waypoint, encounter, store, area (combined format)<br/>
        ///  If bSaveObjectState is true, local vars, effects, action queue, and transition info (triggers, doors) are saved out<br/>
        ///  (except for Combined Area Format, which always has object state saved out).
        /// </summary>
        public static Json ObjectToJson(uint oObject, bool bSaveObjectState = false)
        {
            return NWN.Core.NWScript.ObjectToJson(oObject, bSaveObjectState ? 1 : 0);
        }

        /// <summary>
        ///  Deserializes the game object described in jObject.<br/>
        ///  Returns OBJECT_INVALID on errors.<br/>
        ///  Supported object types: creature, item, trigger, placeable, door, waypoint, encounter, store, area (combined format)<br/>
        ///  For areas, locLocation is ignored.<br/>
        ///  If bLoadObjectState is true, local vars, effects, action queue, and transition info (triggers, doors) are read in.
        /// </summary>
        public static uint JsonToObject(Json jObject, Json locLocation, uint oOwner = OBJECT_INVALID, bool bLoadObjectState = false)
        {
            return NWN.Core.NWScript.JsonToObject(jObject, locLocation, oOwner, bLoadObjectState ? 1 : 0);
        }

        /// <summary>
        ///  Returns the element at the given JSON pointer value.<br/>
        ///  For example, given the JSON document:<br/>
        ///    {<br/>
        ///      "foo": ["bar", "baz"],<br/>
        ///      "": 0,<br/>
        ///      "a/b": 1,<br/>
        ///      "c%d": 2,<br/>
        ///      "e^f": 3,<br/>
        ///      "g|h": 4,<br/>
        ///      "i\\j": 5,<br/>
        ///      "k\"l": 6,<br/>
        ///      " ": 7,<br/>
        ///      "m~n": 8<br/>
        ///    }<br/>
        ///  The following JSON strings evaluate to the accompanying values:<br/>
        ///    ""           // the whole document<br/>
        ///    "/foo"       ["bar", "baz"]<br/>
        ///    "/foo/0"     "bar"<br/>
        ///    "/"          0<br/>
        ///    "/a~1b"      1<br/>
        ///    "/c%d"       2<br/>
        ///    "/e^f"       3<br/>
        ///    "/g|h"       4<br/>
        ///    "/i\\j"      5<br/>
        ///    "/k\"l"      6<br/>
        ///    "/ "         7<br/>
        ///    "/m~0n"      8<br/>
        ///  See https://datatracker.ietf.org/doc/html/rfc6901 for more details.
        ///  Returns a json null value on error, with JsonGetError() filled in.
        /// </summary>
        public static Json JsonPointer(Json jData, string sPointer)
        {
            return NWN.Core.NWScript.JsonPointer(jData, sPointer);
        }

        /// <summary>
        ///  Return a modified copy of jData with jPatch applied, according to the rules described below.<br/>
        ///  See JsonPointer() for documentation on the pointer syntax.<br/>
        ///  Returns a json null value on error, with JsonGetError() filled in.<br/>
        ///  jPatch is an array of patch elements, each containing a op, a path, and a value field. Example:<br/>
        ///  [<br/>
        ///    { "op": "replace", "path": "/baz", "value": "boo" },<br/>
        ///    { "op": "add", "path": "/hello", "value": ["world"] },<br/>
        ///    { "op": "remove", "path": "/foo"}<br/>
        ///  ]<br/>
        ///  Valid operations are: add, remove, replace, move, copy, test<br/>
        ///  See https://datatracker.ietf.org/doc/html/rfc7386 for more details on the patch rules.
        /// </summary>
        public static Json JsonPatch(Json jData, Json jPatch)
        {
            return NWN.Core.NWScript.JsonPatch(jData, jPatch);
        }

        /// <summary>
        ///  Returns the diff (described as a json structure you can pass into JsonPatch) between the two objects.<br/>
        ///  Returns a json null value on error, with JsonGetError() filled in.
        /// </summary>
        public static Json JsonDiff(Json jLHS, Json jRHS)
        {
            return NWN.Core.NWScript.JsonDiff(jLHS, jRHS);
        }

        /// <summary>
        ///  Returns a modified copy of jData with jMerge merged into it. This is an alternative to<br/>
        ///  JsonPatch/JsonDiff, with a syntax more closely resembling the final object.<br/>
        ///  See https://datatracker.ietf.org/doc/html/rfc7386 for details.<br/>
        ///  Returns a json null value on error, with JsonGetError() filled in.
        /// </summary>
        public static Json JsonMerge(Json jData, Json jMerge)
        {
            return NWN.Core.NWScript.JsonMerge(jData, jMerge);
        }

        /// <summary>
        ///  Get oObject's local json variable sVarName<br/>
        ///  * Return value on error: json null type
        /// </summary>
        public static Json GetLocalJson(uint oObject, string sVarName)
        {
            return NWN.Core.NWScript.GetLocalJson(oObject, sVarName);
        }

        /// <summary>
        ///  Set oObject's local json variable sVarName to jValue
        /// </summary>
        public static void SetLocalJson(uint oObject, string sVarName, Json jValue)
        {
            NWN.Core.NWScript.SetLocalJson(oObject, sVarName, jValue);
        }

        /// <summary>
        ///  Delete oObject's local json variable sVarName
        /// </summary>
        public static void DeleteLocalJson(uint oObject, string sVarName)
        {
            NWN.Core.NWScript.DeleteLocalJson(oObject, sVarName);
        }

        /// <summary>
        ///  Bind an json to a named parameter of the given prepared query.<br/>
        ///  Json values are serialised into a string.<br/>
        ///  Example:<br/>
        ///    sqlquery v = SqlPrepareQueryObject(GetModule(), "insert into test (col) values (@myjson);");<br/>
        ///    SqlBindJson(v, "@myjson", myJsonObject);<br/>
        ///    SqlStep(v);
        /// </summary>
        public static void SqlBindJson(Json sqlQuery, string sParam, Json jValue)
        {
            NWN.Core.NWScript.SqlBindJson(sqlQuery, sParam, jValue);
        }

        /// <summary>
        ///  Retrieve a column cast as a json value of the currently stepped row.<br/>
        ///  You can call this after SqlStep() returned true.<br/>
        ///  In case of error, a json null value will be returned.<br/>
        ///  In traditional fashion, nIndex starts at 0.
        /// </summary>
        public static Json SqlGetJson(Json sqlQuery, int nIndex)
        {
            return NWN.Core.NWScript.SqlGetJson(sqlQuery, nIndex);
        }

        /// <summary>
        ///  This stores a json out to the specified campaign database<br/>
        ///  The database name:<br/>
        ///   - is case insensitive and it must be the same for both set and get functions.<br/>
        ///   - can only contain alphanumeric characters, no spaces.<br/>
        ///  The var name must be unique across the entire database, regardless of the variable type.<br/>
        ///  If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static void SetCampaignJson(string sCampaignName, string sVarName, Json jValue, uint oPlayer = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetCampaignJson(sCampaignName, sVarName, jValue, oPlayer);
        }

        /// <summary>
        ///  This will read a json from the  specified campaign database<br/>
        ///  The database name:<br/>
        ///   - is case insensitive and it must be the same for both set and get functions.<br/>
        ///   - can only contain alphanumeric characters, no spaces.<br/>
        ///  The var name must be unique across the entire database, regardless of the variable type.<br/>
        ///  If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static Json GetCampaignJson(string sCampaignName, string sVarName, uint oPlayer = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetCampaignJson(sCampaignName, sVarName, oPlayer);
        }

        /// <summary>
        ///  Gets a device property/capability as advertised by the client.<br/>
        ///  sProperty is one of PLAYER_DEVICE_PROPERTY_xxx.<br/>
        ///  Returns -1 if<br/>
        ///  - the property was never set by the client,<br/>
        ///  - the the actual value is -1,<br/>
        ///  - the player is running a older build that does not advertise device properties,<br/>
        ///  - the player has disabled sending device properties (Options->Game->Privacy).
        /// </summary>
        public static int GetPlayerDeviceProperty(uint oPlayer, string sProperty)
        {
            return NWN.Core.NWScript.GetPlayerDeviceProperty(oPlayer, sProperty);
        }

        /// <summary>
        ///  Returns the LANGUAGE_xx code of the given player, or -1 if unavailable.
        /// </summary>
        public static PlayerLanguageType GetPlayerLanguage(uint oPlayer)
        {
            return (PlayerLanguageType)NWN.Core.NWScript.GetPlayerLanguage(oPlayer);
        }

        /// <summary>
        ///  Returns one of PLAYER_DEVICE_PLATFORM_xxx, or 0 if unavailable.
        /// </summary>
        public static PlayerDevicePlatformType GetPlayerDevicePlatform(uint oPlayer)
        {
            return (PlayerDevicePlatformType)NWN.Core.NWScript.GetPlayerDevicePlatform(oPlayer);
        }

        /// <summary>
        ///  Deserializes the given resref/template into a JSON structure.<br/>
        ///  Supported GFF resource types:<br/>
        ///  * RESTYPE_CAF (and RESTYPE_ARE, RESTYPE_GIT, RESTYPE_GIC)<br/>
        ///  * RESTYPE_UTC<br/>
        ///  * RESTYPE_UTI<br/>
        ///  * RESTYPE_UTT<br/>
        ///  * RESTYPE_UTP<br/>
        ///  * RESTYPE_UTD<br/>
        ///  * RESTYPE_UTW<br/>
        ///  * RESTYPE_UTE<br/>
        ///  * RESTYPE_UTM<br/>
        ///  * RESTYPE_DLG<br/>
        ///  * RESTYPE_UTS<br/>
        ///  * RESTYPE_IFO<br/>
        ///  * RESTYPE_FAC<br/>
        ///  * RESTYPE_ITP<br/>
        ///  * RESTYPE_GUI<br/>
        ///  * RESTYPE_GFF<br/>
        ///  Returns a valid gff-type json structure, or a null value with JsonGetError() set.
        /// </summary>
        public static Json TemplateToJson(string sResRef, int nResType)
        {
            return NWN.Core.NWScript.TemplateToJson(sResRef, nResType);
        }

        /// <summary>
        ///  Returns the resource location of sResRef(.jui) as seen by the running module.<br/>
        ///  Note for dedicated servers: Checks on the module/server side, not the client.<br/>
        ///  Returns "" if the resource does not exist in the search space.
        /// </summary>
        public static string ResManGetAliasFor(string sResRef, int nResType)
        {
            return NWN.Core.NWScript.ResManGetAliasFor(sResRef, nResType);
        }

        /// <summary>
        ///  Finds the nNth available resref starting with sPrefix.<br/>
        ///  * Set bSearchBaseData to true to also search base game content stored in your game installation directory.<br/>
        ///    WARNING: This can be very slow.<br/>
        ///  * Set sOnlyKeyTable to a specific keytable to only search the given named keytable (e.g. "OVERRIDE:").<br/>
        ///  Returns "" if no such resref exists.
        /// </summary>
        public static string ResManFindPrefix(string sPrefix, int nResType, int nNth = 1, bool bSearchBaseData = false, string sOnlyKeyTable = "")
        {
            return NWN.Core.NWScript.ResManFindPrefix(sPrefix, nResType, nNth, bSearchBaseData ? 1 : 0, sOnlyKeyTable);
        }

        /// <summary>
        ///  Create a NUI window from the given resref(.jui) for the given player.<br/>
        ///  * The resref needs to be available on the client, not the server.<br/>
        ///  * The token is a integer for ease of handling only. You are not supposed to do anything with it, except store/pass it.<br/>
        ///  * The window ID needs to be alphanumeric and short. Only one window (per client) with the same ID can exist at a time.<br/>
        ///    Re-creating a window with the same id of one already open will immediately close the old one.<br/>
        ///  * sEventScript is optional and overrides the NUI module event for this window only.<br/>
        ///  * See nw_inc_nui.nss for full documentation.<br/>
        ///  Returns the window token on success (>0), or 0 on error.
        /// </summary>
        public static int NuiCreateFromResRef(uint oPlayer, string sResRef, string sWindowId = "", string sEventScript = "")
        {
            return NWN.Core.NWScript.NuiCreateFromResRef(oPlayer, sResRef, sWindowId, sEventScript);
        }

        /// <summary>
        ///  Create a NUI window inline for the given player.<br/>
        ///  * The token is a integer for ease of handling only. You are not supposed to do anything with it, except store/pass it.<br/>
        ///  * The window ID needs to be alphanumeric and short. Only one window (per client) with the same ID can exist at a time.<br/>
        ///    Re-creating a window with the same id of one already open will immediately close the old one.<br/>
        ///  * sEventScript is optional and overrides the NUI module event for this window only.<br/>
        ///  * See nw_inc_nui.nss for full documentation.<br/>
        ///  Returns the window token on success (>0), or 0 on error.
        /// </summary>
        public static int NuiCreate(uint oPlayer, Json jNui, string sWindowId = "", string sEventScript = "")
        {
            return NWN.Core.NWScript.NuiCreate(oPlayer, jNui, sWindowId, sEventScript);
        }

        /// <summary>
        ///  You can look up windows by ID, if you gave them one.<br/>
        ///  * Windows with a ID present are singletons - attempting to open a second one with the same ID<br/>
        ///    will fail, even if the json definition is different.
        /// </summary>
        public static int NuiFindWindow(uint oPlayer, string sId)
        {
            return NWN.Core.NWScript.NuiFindWindow(oPlayer, sId);
        }

        /// <summary>
        ///  Destroys the given window, by token, immediately closing it on the client.<br/>
        ///  Does nothing if nUiToken does not exist on the client.<br/>
        ///  Does not send a close event - this immediately destroys all serverside state.<br/>
        ///  The client will close the window asynchronously.
        /// </summary>
        public static void NuiDestroy(uint oPlayer, int nUiToken)
        {
            NWN.Core.NWScript.NuiDestroy(oPlayer, nUiToken);
        }

        /// <summary>
        ///  Returns the originating player of the current event.
        /// </summary>
        public static uint NuiGetEventPlayer()
        {
            return NWN.Core.NWScript.NuiGetEventPlayer();
        }

        /// <summary>
        ///  Gets the window token of the current event (or 0 if not in a event).
        /// </summary>
        public static int NuiGetEventWindow()
        {
            return NWN.Core.NWScript.NuiGetEventWindow();
        }

        /// <summary>
        ///  Returns the event type of the current event.<br/>
        ///  * See nw_inc_nui.nss for full documentation of all events.
        /// </summary>
        public static string NuiGetEventType()
        {
            return NWN.Core.NWScript.NuiGetEventType();
        }

        /// <summary>
        ///  Returns the ID of the widget that triggered the event.
        /// </summary>
        public static string NuiGetEventElement()
        {
            return NWN.Core.NWScript.NuiGetEventElement();
        }

        /// <summary>
        ///  Get the array index of the current event.<br/>
        ///  This can be used to get the index into an array, for example when rendering lists of buttons.<br/>
        ///  Returns -1 if the event is not originating from within an array.
        /// </summary>
        public static int NuiGetEventArrayIndex()
        {
            return NWN.Core.NWScript.NuiGetEventArrayIndex();
        }

        /// <summary>
        ///  Returns the window ID of the window described by nUiToken.<br/>
        ///  Returns "" on error, or if the window has no ID.
        /// </summary>
        public static string NuiGetWindowId(uint oPlayer, int nUiToken)
        {
            return NWN.Core.NWScript.NuiGetWindowId(oPlayer, nUiToken);
        }

        /// <summary>
        /// Gets the json value for the given player, token and bind.<br/>
        ///  * json values can hold all kinds of values; but NUI widgets require specific bind types.<br/>
        ///    It is up to you to either handle this in NWScript, or just set compatible bind types.<br/>
        ///    No auto-conversion happens.<br/>
        ///  Returns a json null value if the bind does not exist.
        /// </summary>
        public static Json NuiGetBind(uint oPlayer, int nUiToken, string sBindName)
        {
            return NWN.Core.NWScript.NuiGetBind(oPlayer, nUiToken, sBindName);
        }

        /// <summary>
        ///  Sets a json value for the given player, token and bind.<br/>
        ///  The value is synced down to the client and can be used in UI binding.<br/>
        ///  When the UI changes the value, it is returned to the server and can be retrieved via NuiGetBind().<br/>
        ///  * json values can hold all kinds of values; but NUI widgets require specific bind types.<br/>
        ///    It is up to you to either handle this in NWScript, or just set compatible bind types.<br/>
        ///    No auto-conversion happens.<br/>
        ///  * If the bind is on the watch list, this will immediately invoke the event handler with the &amp;quot;watch&amp;quot;<br/>
        ///    even type; even before this function returns. Do not update watched binds from within the watch handler<br/>
        ///    unless you enjoy stack overflows.<br/>
        ///  Does nothing if the given player+token is invalid.
        /// </summary>
        public static void NuiSetBind(uint oPlayer, int nUiToken, string sBindName, Json jValue)
        {
            NWN.Core.NWScript.NuiSetBind(oPlayer, nUiToken, sBindName, jValue);
        }

        /// <summary>
        ///  Swaps out the given element (by id) with the given nui layout (partial).<br/>
        ///  * This currently only works with the &amp;quot;group&amp;quot; element type, and the special &amp;quot;_window_&amp;quot; root group.
        /// </summary>
        public static void NuiSetGroupLayout(uint oPlayer, int nUiToken, string sElement, Json jNui)
        {
            NWN.Core.NWScript.NuiSetGroupLayout(oPlayer, nUiToken, sElement, jNui);
        }

        /// <summary>
        ///  Mark the given bind name as watched.<br/>
        ///  A watched bind will invoke the NUI script event every time it&amp;apos;s value changes.<br/>
        ///  Be careful with binding nui data inside a watch event handler: It&amp;apos;s easy to accidentally recurse yourself into a stack overflow.
        /// </summary>
        public static int NuiSetBindWatch(uint oPlayer, int nUiToken, string sBind, bool bWatch)
        {
            return NWN.Core.NWScript.NuiSetBindWatch(oPlayer, nUiToken, sBind, bWatch ? 1 : 0);
        }

        ///  Returns the nNth window token of the player, or 0.<br/>
        ///  nNth starts at 0.<br/>
        ///  Iterator is not write-safe: Calling DestroyWindow() will invalidate move following offsets by one.
        public static int NuiGetNthWindow(uint oPlayer, int nNth = 0)
        {
            return NWN.Core.NWScript.NuiGetNthWindow(oPlayer, nNth);
        }

        /// <summary>
        ///  Get the array index of the current event.<br/>
        ///  This can be used to get the index into an array, for example when rendering lists of buttons.<br/>
        ///  Returns -1 if the event is not originating from within an array.
        /// </summary>
        public static string NuiGetNthBind(uint oPlayer, int nToken, bool bWatched, int nNth = 0)
        {
            return NWN.Core.NWScript.NuiGetNthBind(oPlayer, nToken, bWatched ? 1 : 0, nNth);
        }

        /// <summary>
        ///  Returns the event payload, specific to the event.<br/>
        ///  Returns JsonNull if event has no payload.
        /// </summary>
        public static Json NuiGetEventPayload()
        {
            return NWN.Core.NWScript.NuiGetEventPayload();
        }

        /// <summary>
        ///  Get the userdata of the given window token.<br/>
        ///  Returns JsonNull if the window does not exist on the given player, or has no userdata set.
        /// </summary>
        public static Json NuiGetUserData(uint oPlayer, int nToken)
        {
            return NWN.Core.NWScript.NuiGetUserData(oPlayer, nToken);
        }

        /// <summary>
        ///  Sets an arbitrary json value as userdata on the given window token.<br/>
        ///  This userdata is not read or handled by the game engine and not sent to clients.<br/>
        ///  This mechanism only exists as a convenience for the programmer to store data bound to a windows' lifecycle.<br/>
        ///  Will do nothing if the window does not exist.
        /// </summary>
        public static void NuiSetUserData(uint oPlayer, int nToken, Json jUserData)
        {
            NWN.Core.NWScript.NuiSetUserData(oPlayer, nToken, jUserData);
        }

        /// <summary>
        ///  Returns the number of script instructions remaining for the currently-running script.<br/>
        ///  Once this value hits zero, the script will abort with TOO MANY INSTRUCTIONS.<br/>
        ///  The instruction limit is configurable by the user, so if you have a really long-running<br/>
        ///  process, this value can guide you with splitting it up into smaller, discretely schedulable parts.<br/>
        ///  Note: Running this command and checking/handling the value also takes up some instructions.
        /// </summary>
        public static int GetScriptInstructionsRemaining()
        {
            return NWN.Core.NWScript.GetScriptInstructionsRemaining();
        }

        /// <summary>
        ///  Returns a modified copy of jArray with the value order changed according to nTransform:<br/>
        ///  * JSON_ARRAY_SORT_ASCENDING, JSON_ARRAY_SORT_DESCENDING<br/>
        ///     Sorting is dependent on the type and follows json standards (.e.g. 99 < "100").<br/>
        ///  * JSON_ARRAY_SHUFFLE<br/>
        ///    Randomises the order of elements.<br/>
        ///  * JSON_ARRAY_REVERSE<br/>
        ///    Reverses the array.<br/>
        ///  * JSON_ARRAY_UNIQUE<br/>
        ///    Returns a modified copy of jArray with duplicate values removed.<br/>
        ///    Coercable but different types are not considered equal (e.g. 99 != "99"); int/float equivalence however applies: 4.0 == 4.<br/>
        ///    Order is preserved.<br/>
        ///  * JSON_ARRAY_COALESCE<br/>
        ///    Returns the first non-null entry. Empty-ish values (e.g. "", 0) are not considered null, only the json scalar type.
        /// </summary>
        public static Json JsonArrayTransform(Json jArray, int nTransform)
        {
            return NWN.Core.NWScript.JsonArrayTransform(jArray, nTransform);
        }

        /// <summary>
        ///  Returns the nth-matching index or key of jNeedle in jHaystack.<br/>
        ///  Supported haystacks: object, array<br/>
        ///  Ordering behaviour for objects is unspecified.<br/>
        ///  Return null when not found or on any error.
        /// </summary>
        public static Json JsonFind(Json jHaystack, Json jNeedle, int nNth = 0, JsonFindType nConditional = JsonFindType.Equal)
        {
            return NWN.Core.NWScript.JsonFind(jHaystack, jNeedle, nNth, (int)nConditional);
        }

        /// <summary>
        ///  Returns a copy of the range (nBeginIndex, nEndIndex) inclusive of jArray.<br/>
        ///  Negative nEndIndex values count from the other end.<br/>
        ///  Out-of-bound values are clamped to the array range.<br/>
        ///  Examples:<br/>
        ///   json a = JsonParse("[0, 1, 2, 3, 4]");<br/>
        ///   JsonArrayGetRange(a, 0, 1)    // => [0, 1]<br/>
        ///   JsonArrayGetRange(a, 1, -1)   // => [1, 2, 3, 4]<br/>
        ///   JsonArrayGetRange(a, 0, 4)    // => [0, 1, 2, 3, 4]<br/>
        ///   JsonArrayGetRange(a, 0, 999)  // => [0, 1, 2, 3, 4]<br/>
        ///   JsonArrayGetRange(a, 1, 0)    // => []<br/>
        ///   JsonArrayGetRange(a, 1, 1)    // => [1]<br/>
        ///  Returns a null type on error, including type mismatches.
        /// </summary>
        public static Json JsonArrayGetRange(Json jArray, int nBeginIndex, int nEndIndex)
        {
            return NWN.Core.NWScript.JsonArrayGetRange(jArray, nBeginIndex, nEndIndex);
        }

        /// <summary>
        ///  Returns the result of a set operation on two arrays.<br/>
        ///  Operations:<br/>
        ///  * JSON_SET_SUBSET (v <= o):<br/>
        ///    Returns true if every element in jValue is also in jOther.<br/>
        ///  * JSON_SET_UNION (v | o):<br/>
        ///    Returns a new array containing values from both sides.<br/>
        ///  * JSON_SET_INTERSECT (v &amp; o):<br/>
        ///    Returns a new array containing only values common to both sides.<br/>
        ///  * JSON_SET_DIFFERENCE (v - o):<br/>
        ///    Returns a new array containing only values not in jOther.<br/>
        ///  * JSON_SET_SYMMETRIC_DIFFERENCE (v ^ o):<br/>
        ///    Returns a new array containing all elements present in either array, but not both.
        /// </summary>
        public static Json JsonSetOp(Json jValue, int nOp, Json jOther)
        {
            return NWN.Core.NWScript.JsonSetOp(jValue, nOp, jOther);
        }

        /// <summary>
        ///  Returns the column name of s2DA at nColumn index (starting at 0).<br/>
        ///  Returns "" if column nColumn doesn't exist (at end).
        /// </summary>
        public static string Get2DAColumn(string s2DA, int nColumnIdx)
        {
            return NWN.Core.NWScript.Get2DAColumn(s2DA, nColumnIdx);
        }

        /// <summary>
        ///  Returns the number of defined rows in the 2da s2DA.
        /// </summary>
        public static int Get2DARowCount(string s2DA)
        {
            return NWN.Core.NWScript.Get2DARowCount(s2DA);
        }

        /// <summary>
        ///  Set the subtype of eEffect to Unyielding and return eEffect.<br/>
        ///  (Effects default to magical if the subtype is not set)<br/>
        ///  Unyielding effects are not removed by resting, death or dispel magic, only by RemoveEffect().<br/>
        ///  Note: effects that modify state, Stunned/Knockdown/Deaf etc, WILL be removed on death.
        /// </summary>
        public static Json UnyieldingEffect(Effect eEffect)
        {
            return NWN.Core.NWScript.UnyieldingEffect(eEffect);
        }

        /// <summary>
        ///  Set eEffect to ignore immunities and return eEffect.
        /// </summary>
        public static Json IgnoreEffectImmunity(Effect eEffect)
        {
            return NWN.Core.NWScript.IgnoreEffectImmunity(eEffect);
        }

        /// <summary>
        ///  Sets the global shader uniform for the player to the specified float.<br/>
        ///  These uniforms are not used by the base game and are reserved for module-specific scripting.<br/>
        ///  You need to add custom shaders that will make use of them.<br/>
        ///  In multiplayer, these need to be reapplied when a player rejoins.<br/>
        ///  - nShader: SHADER_UNIFORM_*
        /// </summary>
        public static void SetShaderUniformFloat(uint oPlayer, int nShader, float fValue)
        {
            NWN.Core.NWScript.SetShaderUniformFloat(oPlayer, nShader, fValue);
        }

        /// <summary>
        ///  Sets the global shader uniform for the player to the specified integer.<br/>
        ///  These uniforms are not used by the base game and are reserved for module-specific scripting.<br/>
        ///  You need to add custom shaders that will make use of them.<br/>
        ///  In multiplayer, these need to be reapplied when a player rejoins.<br/>
        ///  - nShader: SHADER_UNIFORM_*
        /// </summary>
        public static void SetShaderUniformInt(uint oPlayer, int nShader, int nValue)
        {
            NWN.Core.NWScript.SetShaderUniformInt(oPlayer, nShader, nValue);
        }

        /// <summary>
        ///  Sets the global shader uniform for the player to the specified vec4.<br/>
        ///  These uniforms are not used by the base game and are reserved for module-specific scripting.<br/>
        ///  You need to add custom shaders that will make use of them.<br/>
        ///  In multiplayer, these need to be reapplied when a player rejoins.<br/>
        ///  - nShader: SHADER_UNIFORM_*
        /// </summary>
        public static void SetShaderUniformVec(uint oPlayer, int nShader, float fX, float fY, float fZ, float fW)
        {
            NWN.Core.NWScript.SetShaderUniformVec(oPlayer, nShader, fX, fY, fZ, fW);
        }

        /// <summary>
        ///  Sets the spell targeting data manually for the player. This data is usually specified in spells.2da.<br/>
        ///  This data persists through spell casts; you're overwriting the entry in spells.2da for this session.<br/>
        ///  In multiplayer, these need to be reapplied when a player rejoins.<br/>
        ///  - nSpell: SPELL_*<br/>
        ///  - nShape: SPELL_TARGETING_SHAPE_*<br/>
        ///  - nFlags: SPELL_TARGETING_FLAGS_*
        /// </summary>
        public static void SetSpellTargetingData(uint oPlayer, int nSpell, int nShape, float fSizeX, float fSizeY, int nFlags)
        {
            NWN.Core.NWScript.SetSpellTargetingData(oPlayer, nSpell, nShape, fSizeX, fSizeY, nFlags);
        }

        /// <summary>
        ///  Sets the spell targeting data which is used for the next call to EnterTargetingMode() for this player.<br/>
        ///  If the shape is set to SPELL_TARGETING_SHAPE_NONE and the range is provided, the dotted line range indicator will still appear.<br/>
        ///  - nShape: SPELL_TARGETING_SHAPE_*<br/>
        ///  - nFlags: SPELL_TARGETING_FLAGS_*<br/>
        ///  - nSpell: SPELL_* (optional, passed to the shader but does nothing by default, you need to edit the shader to use it)<br/>
        ///  - nFeat: FEAT_* (optional, passed to the shader but does nothing by default, you need to edit the shader to use it)
        /// </summary>
        public static void SetEnterTargetingModeData(uint oPlayer, int nShape, float fSizeX, float fSizeY, int nFlags, float fRange = 0.0f, int nSpell = -1, int nFeat = -1)
        {
            NWN.Core.NWScript.SetEnterTargetingModeData(oPlayer, nShape, fSizeX, fSizeY, nFlags, fRange, nSpell, nFeat);
        }

        /// <summary>
        ///  Gets the number of memorized spell slots for a given spell level.<br/>
        ///  - nClassType: a CLASS_TYPE_* constant. Must be a MemorizesSpells class.<br/>
        ///  - nSpellLevel: the spell level, 0-9.<br/>
        ///  Returns: the number of spell slots.
        /// </summary>
        public static int GetMemorizedSpellCountByLevel(uint oCreature, ClassType nClassType, int nSpellLevel)
        {
            return NWN.Core.NWScript.GetMemorizedSpellCountByLevel(oCreature, (int)nClassType, nSpellLevel);
        }

        /// <summary>
        ///  Gets the spell id of a memorized spell slot.<br/>
        ///  - nClassType: a CLASS_TYPE_* constant. Must be a MemorizesSpells class.<br/>
        ///  - nSpellLevel: the spell level, 0-9.<br/>
        ///  - nIndex: the index of the spell slot. Bounds: 0 <= nIndex < GetMemorizedSpellCountByLevel()<br/>
        ///  Returns: a SPELL_* constant or -1 if the slot is not set.
        /// </summary>
        public static int GetMemorizedSpellId(uint oCreature, ClassType nClassType, int nSpellLevel, int nIndex)
        {
            return NWN.Core.NWScript.GetMemorizedSpellId(oCreature, (int)nClassType, nSpellLevel, nIndex);
        }

        /// <summary>
        ///  Gets the ready state of a memorized spell slot.<br/>
        ///  - nClassType: a CLASS_TYPE_* constant. Must be a MemorizesSpells class.<br/>
        ///  - nSpellLevel: the spell level, 0-9.<br/>
        ///  - nIndex: the index of the spell slot. Bounds: 0 <= nIndex < GetMemorizedSpellCountByLevel()<br/>
        ///  Returns: true/FALSE or -1 if the slot is not set.
        /// </summary>
        public static int GetMemorizedSpellReady(uint oCreature, ClassType nClassType, int nSpellLevel, int nIndex)
        {
            return NWN.Core.NWScript.GetMemorizedSpellReady(oCreature, (int)nClassType, nSpellLevel, nIndex);
        }

        /// <summary>
        ///  Gets the metamagic of a memorized spell slot.<br/>
        ///  - nClassType: a CLASS_TYPE_* constant. Must be a MemorizesSpells class.<br/>
        ///  - nSpellLevel: the spell level, 0-9.<br/>
        ///  - nIndex: the index of the spell slot. Bounds: 0 <= nIndex < GetMemorizedSpellCountByLevel()<br/>
        ///  Returns: a METAMAGIC_* constant or -1 if the slot is not set.
        /// </summary>
        public static int GetMemorizedSpellMetaMagic(uint oCreature, ClassType nClassType, int nSpellLevel, int nIndex)
        {
            return NWN.Core.NWScript.GetMemorizedSpellMetaMagic(oCreature, (int)nClassType, nSpellLevel, nIndex);
        }

        /// <summary>
        ///  Gets if the memorized spell slot has a domain spell.<br/>
        ///  - nClassType: a CLASS_TYPE_* constant. Must be a MemorizesSpells class.<br/>
        ///  - nSpellLevel: the spell level, 0-9.<br/>
        ///  - nIndex: the index of the spell slot. Bounds: 0 <= nIndex < GetMemorizedSpellCountByLevel()<br/>
        ///  Returns: true/FALSE or -1 if the slot is not set.
        /// </summary>
        public static int GetMemorizedSpellIsDomainSpell(uint oCreature, ClassType nClassType, int nSpellLevel, int nIndex)
        {
            return NWN.Core.NWScript.GetMemorizedSpellIsDomainSpell(oCreature, (int)nClassType, nSpellLevel, nIndex);
        }

        /// <summary>
        ///  Set a memorized spell slot.<br/>
        ///  - nClassType: a CLASS_TYPE_* constant. Must be a MemorizesSpells class.<br/>
        ///  - nSpellLevel: the spell level, 0-9.<br/>
        ///  - nIndex: the index of the spell slot. Bounds: 0 <= nIndex < GetMemorizedSpellCountByLevel()<br/>
        ///  - nSpellId: a SPELL_* constant.<br/>
        ///  - bReady: true to mark the slot ready.<br/>
        ///  - nMetaMagic: a METAMAGIC_* constant.<br/>
        ///  - bIsDomainSpell: true for a domain spell.
        /// </summary>
        public static void SetMemorizedSpell(uint oCreature, ClassType nClassType, int nSpellLevel, int nIndex, SpellType nSpellId, bool bReady = true, MetamagicType nMetaMagic = MetamagicType.None, bool bIsDomainSpell = false)
        {
            NWN.Core.NWScript.SetMemorizedSpell(oCreature, (int)nClassType, nSpellLevel, nIndex, (int)nSpellId, bReady ? 1 : 0, (int)nMetaMagic, bIsDomainSpell ? 1 : 0);
        }

        /// <summary>
        ///  Set the ready state of a memorized spell slot.<br/>
        ///  - nClassType: a CLASS_TYPE_* constant. Must be a MemorizesSpells class.<br/>
        ///  - nSpellLevel: the spell level, 0-9.<br/>
        ///  - nIndex: the index of the spell slot. Bounds: 0 <= nIndex < GetMemorizedSpellCountByLevel()<br/>
        ///  - bReady: true to mark the slot ready.
        /// </summary>
        public static void SetMemorizedSpellReady(uint oCreature, ClassType nClassType, int nSpellLevel, int nIndex, bool bReady)
        {
            NWN.Core.NWScript.SetMemorizedSpellReady(oCreature, (int)nClassType, nSpellLevel, nIndex, bReady ? 1 : 0);
        }

        /// <summary>
        ///  Clear a specific memorized spell slot.<br/>
        ///  - nClassType: a CLASS_TYPE_* constant. Must be a MemorizesSpells class.<br/>
        ///  - nSpellLevel: the spell level, 0-9.<br/>
        ///  - nIndex: the index of the spell slot. Bounds: 0 <= nIndex < GetMemorizedSpellCountByLevel()
        /// </summary>
        public static void ClearMemorizedSpell(uint oCreature, ClassType nClassType, int nSpellLevel, int nIndex)
        {
            NWN.Core.NWScript.ClearMemorizedSpell(oCreature, (int)nClassType, nSpellLevel, nIndex);
        }

        /// <summary>
        ///  Clear all memorized spell slots of a specific spell id, including metamagic&apos;d ones.<br/>
        ///  - nClassType: a CLASS_TYPE_* constant. Must be a MemorizesSpells class.<br/>
        ///  - nSpellId: a SPELL_* constant.
        /// </summary>
        public static void ClearMemorizedSpellBySpellId(uint oCreature, ClassType nClassType, int nSpellId)
        {
            NWN.Core.NWScript.ClearMemorizedSpellBySpellId(oCreature, (int)nClassType, nSpellId);
        }

        /// <summary>
        ///  Gets the number of known spells for a given spell level.<br/>
        ///  - nClassType: a CLASS_TYPE_* constant. Must be a SpellBookRestricted class.<br/>
        ///  - nSpellLevel: the spell level, 0-9.<br/>
        ///  Returns: the number of known spells.
        /// </summary>
        public static int GetKnownSpellCount(uint oCreature, ClassType nClassType, int nSpellLevel)
        {
            return NWN.Core.NWScript.GetKnownSpellCount(oCreature, (int)nClassType, nSpellLevel);
        }

        /// <summary>
        ///  Gets the spell id of a known spell.<br/>
        ///  - nClassType: a CLASS_TYPE_* constant. Must be a SpellBookRestricted class.<br/>
        ///  - nSpellLevel: the spell level, 0-9.<br/>
        ///  - nIndex: the index of the known spell. Bounds: 0 <= nIndex < GetKnownSpellCount()<br/>
        ///  Returns: a SPELL_* constant or -1 on error.
        /// </summary>
        public static SpellType GetKnownSpellId(uint oCreature, ClassType nClassType, int nSpellLevel, int nIndex)
        {
            return (SpellType)NWN.Core.NWScript.GetKnownSpellId(oCreature, (int)nClassType, nSpellLevel, nIndex);
        }

        /// <summary>
        ///  Gets if a spell is in the known spell list.<br/>
        ///  - nClassType: a CLASS_TYPE_* constant. Must be a SpellBookRestricted class.<br/>
        ///  - nSpellId: a SPELL_* constant.<br/>
        ///  Returns: true if the spell is in the known spell list.
        /// </summary>
        public static bool GetIsInKnownSpellList(uint oCreature, ClassType nClassType, int nSpellId)
        {
            return NWN.Core.NWScript.GetIsInKnownSpellList(oCreature, (int)nClassType, nSpellId) == 1;
        }

        /// <summary>
        ///  Gets the amount of uses a spell has left.<br/>
        ///  - nClassType: a CLASS_TYPE_* constant.<br/>
        ///  - nSpellid: a SPELL_* constant.<br/>
        ///  - nMetaMagic: a METAMAGIC_* constant.<br/>
        ///  - nDomainLevel: the domain level, if a domain spell.<br/>
        ///  Returns: the amount of spell uses left.
        /// </summary>
        public static int GetSpellUsesLeft(uint oCreature, ClassType nClassType, SpellType nSpellId, MetamagicType nMetaMagic = MetamagicType.None, int nDomainLevel = 0)
        {
            return NWN.Core.NWScript.GetSpellUsesLeft(oCreature, (int)nClassType, (int)nSpellId, (int)nMetaMagic, nDomainLevel);
        }

        /// <summary>
        ///  Gets the spell level at which a class gets a spell.<br/>
        ///  - nClassType: a CLASS_TYPE_* constant.<br/>
        ///  - nSpellId: a SPELL_* constant.<br/>
        ///  Returns: the spell level or -1 if the class does not get the spell.
        /// </summary>
        public static int GetSpellLevelByClass(ClassType nClassType, int nSpellId)
        {
            return NWN.Core.NWScript.GetSpellLevelByClass((int)nClassType, nSpellId);
        }

        /// <summary>
        ///  Replaces oObject&apos;s animation sOld with sNew.<br/>
        ///  Specifying sNew = "" will restore the original animation.
        /// </summary>
        public static void ReplaceObjectAnimation(uint oObject, string sOld, string sNew = "")
        {
            NWN.Core.NWScript.ReplaceObjectAnimation(oObject, sOld, sNew);
        }

        /// <summary>
        ///  Sets the distance (in meters) at which oObject info will be sent to clients (default 45.0)<br/>
        ///  This is still subject to other limitations, such as perception ranges for creatures<br/>
        ///  Note: Increasing visibility ranges of many objects can have a severe negative effect on<br/>
        ///        network latency and server performance, and rendering additional objects will<br/>
        ///        impact graphics performance of clients. Use cautiously.
        /// </summary>
        public static void SetObjectVisibleDistance(uint oObject, float fDistance = 45.0f)
        {
            NWN.Core.NWScript.SetObjectVisibleDistance(oObject, fDistance);
        }

        /// <summary>
        ///  Gets oObject&apos;s visible distance, as set by SetObjectVisibleDistance()<br/>
        ///  Returns -1.0f on error
        /// </summary>
        public static float GetObjectVisibleDistance(uint oObject)
        {
            return NWN.Core.NWScript.GetObjectVisibleDistance(oObject);
        }

        /// <summary>
        ///  Sets the active game pause state - same as if the player requested pause.
        /// </summary>
        public static void SetGameActivePause(PauseStateType bState)
        {
            NWN.Core.NWScript.SetGameActivePause((int)bState);
        }

        /// <summary>
        ///  Returns &amp;gt;0 if the game is currently paused:<br/>
        ///  - 0: Game is not paused.<br/>
        ///  - 1: Timestop<br/>
        ///  - 2: Active Player Pause (optionally on top of timestop)
        /// </summary>
        public static PauseStateType GetGamePauseState()
        {
            return (PauseStateType)NWN.Core.NWScript.GetGamePauseState();
        }

        /// <summary>
        ///  Set the gender of oCreature.<br/>
        ///  - nGender: a GENDER_* constant.
        /// </summary>
        public static void SetGender(uint oCreature, GenderType nGender)
        {
            NWN.Core.NWScript.SetGender(oCreature, (int)nGender);
        }

        /// <summary>
        ///  Get the soundset of oCreature.<br/>
        ///  Returns -1 on error.
        /// </summary>
        public static int GetSoundset(uint oCreature)
        {
            return NWN.Core.NWScript.GetSoundset(oCreature);
        }

        /// <summary>
        ///  Set the soundset of oCreature, see soundset.2da for possible values.
        /// </summary>
        public static void SetSoundset(uint oCreature, int nSoundset)
        {
            NWN.Core.NWScript.SetSoundset(oCreature, nSoundset);
        }

        /// <summary>
        ///  Ready a spell level for oCreature.<br/>
        ///  - nSpellLevel: 0-9<br/>
        ///  - nClassType: a CLASS_TYPE_* constant or CLASS_TYPE_INVALID to ready the spell level for all classes.
        /// </summary>
        public static void ReadySpellLevel(uint oCreature, int nSpellLevel, ClassType nClassType = ClassType.Invalid)
        {
            NWN.Core.NWScript.ReadySpellLevel(oCreature, nSpellLevel, (int)nClassType);
        }

        /// <summary>
        ///  Makes oCreature controllable by oPlayer, if player party control is enabled<br/>
        ///  Setting oPlayer=OBJECT_INVALID removes the override and reverts to regular party control behavior<br/>
        ///  NB: A creature is only controllable by one player, so if you set oPlayer to a non-Player object<br/>
        ///     (e.g. the module) it will disable regular party control for this creature
        /// </summary>
        public static void SetCommandingPlayer(uint oCreature, uint oPlayer)
        {
            NWN.Core.NWScript.SetCommandingPlayer(oCreature, oPlayer);
        }

        /// <summary>
        ///  Sets oPlayer&apos;s camera limits that override any client configuration limits<br/>
        ///  Value of -1.0 means use the client config instead<br/>
        ///  NB: Like all other camera settings, this is not saved when saving the game
        /// </summary>
        public static void SetCameraLimits(uint oPlayer, float fMinPitch = -1.0f, float fMaxPitch = -1.0f, float fMinDist = -1.0f, float fMaxDist = -1.0f)
        {
            NWN.Core.NWScript.SetCameraLimits(oPlayer, fMinPitch, fMaxPitch, fMinDist, fMaxDist);
        }

        /// <summary>
        ///  Applies sRegExp on sValue, returning an array containing all matching groups.<br/>
        ///  * The regexp is not bounded by default (so /t/ will match "test").<br/>
        ///  * A matching result with always return a JSON_ARRAY with the full match as the first element.<br/>
        ///  * All matching groups will be returned as additional elements, depth-first.<br/>
        ///  * A non-matching result will return a empty JSON_ARRAY.<br/>
        ///  * If there was an error, the function will return JSON_NULL, with a error string filled in.<br/>
        ///  * nSyntaxFlags is a mask of REGEXP_*<br/>
        ///  * nMatchFlags is a mask of REGEXP_MATCH_* and REGEXP_FORMAT_*.<br/>
        ///  Examples:<br/>
        ///  * RegExpMatch("[", "test value")             -> null (error: "The expression contained mismatched [ and ].")<br/>
        ///  * RegExpMatch("nothing", "test value")       -> []<br/>
        ///  * RegExpMatch("^test", "test value")         -> ["test"]<br/>
        ///  * RegExpMatch("^(test) (.+)$", "test value") -> ["test value", "test", "value"]
        /// </summary>
        public static Json RegExpMatch(string sRegExp, string sValue, RegExpGrammarType nSyntaxFlags = RegExpGrammarType.ECMAScript, RegExpFormatFlagType nMatchFlags = RegExpFormatFlagType.Default)
        {
            return NWN.Core.NWScript.RegExpMatch(sRegExp, sValue, (int)nSyntaxFlags, (int)nMatchFlags);
        }

        /// <summary>
        ///  Iterates sValue with sRegExp.<br/>
        ///  * Returns an array of arrays; where each sub-array contains first the full match and then all matched groups.<br/>
        ///  * Returns empty JSON_ARRAY if no matches are found.<br/>
        ///  * If there was an error, the function will return JSON_NULL, with a error string filled in.<br/>
        ///  * nSyntaxFlags is a mask of REGEXP_*<br/>
        ///  * nMatchFlags is a mask of REGEXP_MATCH_* and REGEXP_FORMAT_*.<br/>
        ///  Example: RegExpIterate("(\\d)(\\S+)", "1i 2am 3 4asentence"); -> [["1i", "1", "i"], ["2am", "2", "am"], ["4sentence", "4", "sentence"]]
        /// </summary>
        public static Json RegExpIterate(string sRegExp, string sValue, RegExpGrammarType nSyntaxFlags = RegExpGrammarType.ECMAScript, RegExpFormatFlagType nMatchFlags = RegExpFormatFlagType.Default)
        {
            return NWN.Core.NWScript.RegExpIterate(sRegExp, sValue, (int)nSyntaxFlags, (int)nMatchFlags);
        }

        /// <summary>
        ///  Replaces all matching sRegExp in sValue with sReplacement.<br/>
        ///  * Returns a empty string on error.<br/>
        ///  * Please see the format documentation for replacement patterns.<br/>
        ///  * nSyntaxFlags is a mask of REGEXP_*<br/>
        ///  * nMatchFlags is a mask of REGEXP_MATCH_* and REGEXP_FORMAT_*.<br/>
        ///  * FORMAT_DEFAULT replacement patterns:<br/>
        ///     $$    $<br/>
        ///     $&amp;    The matched substring.<br/>
        ///     $`    The portion of string that precedes the matched substring.<br/>
        ///     $&apos;    The portion of string that follows the matched substring.<br/>
        ///     $n    The nth capture, where n is a single digit in the range 1 to 9 and $n is not followed by a decimal digit.<br/>
        ///     $nn   The nnth capture, where nn is a two-digit decimal number in the range 01 to 99.<br/>
        ///  Example: RegExpReplace("a+", "vaaalue", "[$&amp;]")    => "v[aaa]lue"
        /// </summary>
        public static string RegExpReplace(string sRegExp, string sValue, string sReplacement, RegExpGrammarType nSyntaxFlags = RegExpGrammarType.ECMAScript, RegExpFormatFlagType nMatchFlags = RegExpFormatFlagType.Default)
        {
            return NWN.Core.NWScript.RegExpReplace(sRegExp, sValue, sReplacement, (int)nSyntaxFlags, (int)nMatchFlags);
        }

        /// <summary>
        ///  Get the contents of a file as string, as seen by the server&apos;s resman.<br/>
        ///  Note: If the output contains binary data it will only return data up to the first null byte.<br/>
        ///  - nResType: a RESTYPE_* constant.<br/>
        ///  - nFormat: one of RESMAN_FILE_CONTENTS_FORMAT_*<br/>
        ///  Returns "" if the file does not exist.
        /// </summary>
        public static string ResManGetFileContents(string sResRef, int nResType, ResmanFileContentsFormatType nFormat = ResmanFileContentsFormatType.Raw)
        {
            return NWN.Core.NWScript.ResManGetFileContents(sResRef, (int)nResType, (int)nFormat);
        }

        /// <summary>
        ///  Compile a script and place it in the server&apos;s CURRENTGAME: folder.<br/>
        ///  Note: Scripts will persist for as long as the module is running.<br/>
        ///  SinglePlayer / Saves: Scripts that overwrite existing module scripts will persist to the save file.<br/>
        ///                        New scripts, unknown to the module, will have to be re-compiled on module load when loading a save.<br/>
        ///  Returns "" on success or the error on failure.
        /// </summary>
        public static string CompileScript(string sScriptName, string sScriptData, bool bWrapIntoMain = false, bool bGenerateNDB = false)
        {
            return NWN.Core.NWScript.CompileScript(sScriptName, sScriptData, bWrapIntoMain ? 1 : 0, bGenerateNDB ? 1 : 0);
        }

        /// <summary>
        ///  Sets the object oPlayer&apos;s camera will be attached to.<br/>
        ///  - oTarget: A valid creature or placeable. If oTarget is OBJECT_INVALID, it will revert the camera back to oPlayer&apos;s character.<br/>
        ///             The target must be known to oPlayer&apos;s client, this means it must be in the same area and within visible distance.<br/>
        ///               - SetObjectVisibleDistance() can be used to increase this range.<br/>
        ///               - If the target is a creature, it also must be within the perception range of oPlayer and perceived.<br/>
        ///  - bFindClearView: if true, the client will attempt to find a camera position where oTarget is in view.<br/>
        ///  Notes:<br/>
        ///        - If oTarget gets destroyed while oPlayer&apos;s camera is attached to it, the camera will revert back to oPlayer&apos;s character.<br/>
        ///        - If oPlayer goes through a transition with its camera attached to a different object, it will revert back to oPlayer&apos;s character.<br/>
        ///        - The object the player&apos;s camera is attached to is not saved when saving the game.
        /// </summary>
        public static void AttachCamera(uint oPlayer, uint oTarget, bool bFindClearView = false)
        {
            NWN.Core.NWScript.AttachCamera(oPlayer, oTarget, bFindClearView ? 1 : 0);
        }

        /// <summary>
        ///  Get the current discoverability mask of oObject.<br/>
        ///  Returns -1 if oObject cannot have a discovery mask.
        /// </summary>
        public static ObjectUIDiscoveryType GetObjectUiDiscoveryMask(uint oObject)
        {
            return (ObjectUIDiscoveryType)NWN.Core.NWScript.GetObjectUiDiscoveryMask(oObject);
        }

        /// <summary>
        ///  Sets the discoverability mask on oObject.<br/>
        ///  This allows toggling areahilite (TAB key by default) and mouseover discovery in the area view.<br/>
        ///  * nMask is a bitmask of OBJECT_UI_DISCOVERY_*<br/>
        ///  Will currently only work on Creatures, Doors (Hilite only), Items and Useable Placeables.<br/>
        ///  Does not affect inventory items.
        /// </summary>
        public static void SetObjectUiDiscoveryMask(uint oObject, ObjectUIDiscoveryType nMask = ObjectUIDiscoveryType.Default)
        {
            NWN.Core.NWScript.SetObjectUiDiscoveryMask(oObject, (int)nMask);
        }

        /// <summary>
        ///  Sets a text override for the mouseover/tab-highlight text bubble of oObject.<br/>
        ///  Will currently only work on Creatures, Items and Useable Placeables.<br/>
        ///  * nMode is one of OBJECT_UI_TEXT_BUBBLE_OVERRIDE_*.
        /// </summary>
        public static void SetObjectTextBubbleOverride(uint oObject, ObjectUITextBubbleOverrideType nMode, string sText)
        {
            NWN.Core.NWScript.SetObjectTextBubbleOverride(oObject, (int)nMode, sText);
        }

        /// <summary>
        ///  Immediately unsets a VTs for the given object, with no lerp.<br/>
        ///  * nScope: one of OBJECT_VISUAL_TRANSFORM_DATA_SCOPE_, or -1 for all scopes<br/>
        ///  Returns TRUE only if transforms were successfully removed (valid object, transforms existed).
        /// </summary>
        public static bool ClearObjectVisualTransform(uint oObject, ObjectVisualTransformDataScopeType nScope = ObjectVisualTransformDataScopeType.All)
        {
            return NWN.Core.NWScript.ClearObjectVisualTransform(oObject, (int)nScope) == 1;
        }

        /// <summary>
        ///  Gets an optional vecror of specific gui events in the module OnPlayerGuiEvent event.<br/>
        ///  GUIEVENT_RADIAL_OPEN - World vector position of radial if on tile.
        /// </summary>
        public static Vector3 GetLastGuiEventVector()
        {
            return NWN.Core.NWScript.GetLastGuiEventVector();
        }

        /// <summary>
        ///  Sets oPlayer&amp;apos;s camera settings that override any client configuration settings<br/>
        ///  nFlags is a bitmask of CAMERA_FLAG_* constants;<br/>
        ///  NB: Like all other camera settings, this is not saved when saving the game
        /// </summary>
        public static void SetCameraFlags(uint oPlayer, CameraFlagType nFlags = CameraFlagType.None)
        {
            NWN.Core.NWScript.SetCameraFlags(oPlayer, (int)nFlags);
        }

        /// <summary>
        ///  Gets the light color in the area specified.<br/>
        ///  nColorType specifies the color type returned.<br/>
        ///     Valid values for nColorType are the AREA_LIGHT_COLOR_* values.<br/>
        ///  If no valid area (or object) is specified, it uses the area of caller.<br/>
        ///  If an object other than an area is specified, will use the area that the object is currently in.
        /// </summary>
        public static int GetAreaLightColor(AreaLightColorType nColorType, uint oArea = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetAreaLightColor((int)nColorType, oArea);
        }

        /// <summary>
        ///  Sets the light color in the area specified.<br/>
        ///  nColorType = AREA_LIGHT_COLOR_* specifies the color type.<br/>
        ///  nColor = FOG_COLOR_* specifies the color the fog is being set to.<br/>
        ///  The color can also be represented as a hex RGB number if specific color shades<br/>
        ///  are desired.<br/>
        ///  The format of a hex specified color would be 0xFFEEDD where<br/>
        ///  FF would represent the amount of red in the color<br/>
        ///  EE would represent the amount of green in the color<br/>
        ///  DD would represent the amount of blue in the color.<br/>
        ///  If no valid area (or object) is specified, it uses the area of caller.<br/>
        ///  If an object other than an area is specified, will use the area that the object is currently in.<br/>
        ///  If fFadeTime is above 0.0, it will fade to the new color in the amount of seconds specified.
        /// </summary>
        public static void SetAreaLightColor(AreaLightColorType nColorType, FogColorType nColor, uint oArea = OBJECT_INVALID, float fFadeTime = 0.0f)
        {
            NWN.Core.NWScript.SetAreaLightColor((int)nColorType, (int)nColor, oArea, fFadeTime);
        }

        /// <summary>
        ///  Gets the light direction of origin in the area specified.<br/>
        ///  nLightType specifies whether the Moon or Sun light direction is returned.<br/>
        ///     Valid values for nLightType are the AREA_LIGHT_DIRECTION_* values.<br/>
        ///  If no valid area (or object) is specified, it uses the area of caller.<br/>
        ///  If an object other than an area is specified, will use the area that the object is currently in.
        /// </summary>
        public static Vector3 GetAreaLightDirection(AreaLightDirectionType nLightType, uint oArea = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetAreaLightDirection((int)nLightType, oArea);
        }

        /// <summary>
        ///  Sets the light direction of origin in the area specified.<br/>
        ///  nLightType = AREA_LIGHT_DIRECTION_* specifies the light type.<br/>
        ///  vDirection = specifies the direction of origin of the light type, i.e. the direction the sun/moon is in from the area.<br/>
        ///  If no valid area (or object) is specified, it uses the area of caller.<br/>
        ///  If an object other than an area is specified, will use the area that the object is currently in.<br/>
        ///  If fFadeTime is above 0.0, it will fade to the new color in the amount of seconds specified.
        /// </summary>
        public static void SetAreaLightDirection(AreaLightDirectionType nLightType, Vector3 vDirection, uint oArea = OBJECT_INVALID, float fFadeTime = 0.0f)
        {
            NWN.Core.NWScript.SetAreaLightDirection((int)nLightType, vDirection, oArea, fFadeTime);
        }

        /// <summary>
        ///  This immediately aborts the running script.<br/>
        ///  - Will not emit an error to the server log by default.<br/>
        ///  - You can specify the optional sError to emit as a script error, which will be printed<br/>
        ///    to the log and sent to all players, just like any other script error.<br/>
        ///  - Will not terminate other script recursion (e.g. nested ExecuteScript()) will resume as if the<br/>
        ///    called script exited cleanly.<br/>
        ///  - This call will never return.
        /// </summary>
        public static void AbortRunningScript(string sError = "")
        {
            NWN.Core.NWScript.AbortRunningScript(sError);
        }

        /// <summary>
        ///  Generate a VM debug view into the current execution location.<br/>
        ///  - Names and symbols can only be resolved if debug information is available (NDB file).<br/>
        ///  - This call can be a slow call for large scripts.<br/>
        ///  - Setting bIncludeStack = true will include stack info in the output, which could be a<br/>
        ///    lot of data for large scripts. You can turn it off if you do not need the info.<br/>
        ///  Returned data format (JSON object):<br/>
        ///    "frames": array of stack frames:<br/>
        ///      "ip": instruction pointer into code<br/>
        ///      "bp", "sp": current base/stack pointer<br/>
        ///      "file", "line", "function": available only if NDB loaded correctly<br/>
        ///    "stack": abbreviated stack data (only if bIncludeStack is true)<br/>
        ///      "type": one of the nwscript object types, OR:<br/>
        ///      "type_unknown": hex code of AUX<br/>
        ///      "data": type-specific payload. Not all type info is rendered in the interest of brevity.<br/>
        ///              Only enough for you to re-identify which variable this might belong to.
        /// </summary>
        public static Json GetScriptBacktrace(bool bIncludeStack = true)
        {
            return NWN.Core.NWScript.GetScriptBacktrace(bIncludeStack ? 1 : 0);
        }
        /// <summary>
        ///  Mark the current location in code as a jump target, identified by sLabel.<br/>
        ///  - Returns 0 on initial invocation, but will return nRetVal if jumped-to by LongJmp.<br/>
        ///  - sLabel can be any valid string (including empty); though it is recommended to pick<br/>
        ///    something distinct. The responsibility of namespacing lies with you.<br/>
        ///  - Calling repeatedly with the same label will overwrite the previous jump location.<br/>
        ///    If you want to nest them, you need to manage nesting state externally.
        /// </summary>
        public static int SetJmp(string sLabel)
        {
            return NWN.Core.NWScript.SetJmp(sLabel);
        }

        /// <summary>
        ///  Jump execution back in time to the point where you called SetJmp with the same label.<br/>
        ///  - This function is a GREAT way to get really hard-to-debug stack under/overflows.<br/>
        ///  - Will not work across script runs or script recursion; only within the same script.<br/>
        ///    (However, it WILL work across includes - those go into the same script data in compilation)<br/>
        ///  - Will throw a script error if sLabel does not exist.<br/>
        ///  - Will throw a script error if no valid jump destination exists.<br/>
        ///  - You CAN jump to locations with compatible stack layout, including sibling functions.<br/>
        ///    For the script to successfully finish, the entire stack needs to be correct (either in code or<br/>
        ///    by jumping elsewhere again). Making sure this is the case is YOUR responsibility.<br/>
        ///  - The parameter nRetVal is passed to SetJmp, resuming script execution as if SetJmp returned<br/>
        ///    that value (instead of 0).<br/>
        ///    If you accidentally pass 0 as nRetVal, it will be silently rewritten to 1.<br/>
        ///    Any other integer value is valid, including negative ones.<br/>
        ///  - This call will never return.
        /// </summary>
        public static void LongJmp(string sLabel, int nRetVal = 1)
        {
            NWN.Core.NWScript.LongJmp(sLabel, nRetVal);
        }

        /// <summary>
        ///  Returns true if the given sLabel is a valid jump target at the current code location.
        /// </summary>
        public static bool GetIsValidJmp(string sLabel)
        {
            return NWN.Core.NWScript.GetIsValidJmp(sLabel) == 1;
        }

        /// <summary>
        ///  Create a Pacified effect, making the creature unable to attack anyone.
        /// </summary>
        public static Effect EffectPacified()
        {
            return NWN.Core.NWScript.EffectPacified();
        }

        /// <summary>
        ///  Get the current script recursion level.
        /// </summary>
        public static int GetScriptRecursionLevel()
        {
            return NWN.Core.NWScript.GetScriptRecursionLevel();
        }

        /// <summary>
        ///  Get the name of the script at a script recursion level.<br/>
        ///  - nRecursionLevel: Between 0 and &lt;= GetScriptRecursionLevel() or -1 for the current recursion level.<br/>
        ///  Returns the script name or &quot;&quot; on error.
        /// </summary>
        public static string GetScriptName(int nRecursionLevel = -1)
        {
            return NWN.Core.NWScript.GetScriptName(nRecursionLevel);
        }

        /// <summary>
        ///  Get the script chunk attached to a script recursion level.<br/>
        ///  - nRecursionLevel: Between 0 and &lt;= GetScriptRecursionLevel() or -1 for the current recursion level.<br/>
        ///  Returns the script chunk or &quot;&quot; on error / no script chunk attached.
        /// </summary>
        public static string GetScriptChunk(int nRecursionLevel = -1)
        {
            return NWN.Core.NWScript.GetScriptChunk(nRecursionLevel);
        }

        /// <summary>
        ///  Returns the patch postfix of oPlayer (i.e. the 29 out of &quot;87.8193.35-29 abcdef01&quot;).<br/>
        ///  Returns 0 if the given object isn&apos;t a player or did not advertise their build info, or the<br/>
        ///  player version is old enough not to send this bit of build info to the server.
        /// </summary>
        public static int GetPlayerBuildVersionPostfix(uint oPlayer)
        {
            return NWN.Core.NWScript.GetPlayerBuildVersionPostfix(oPlayer);
        }

        /// <summary>
        ///  Returns the patch commit sha1 of oPlayer (i.e. the &quot;abcdef01&quot; out of &quot;87.8193.35-29 abcdef01&quot;).<br/>
        ///  Returns &quot;&quot; if the given object isn&apos;t a player or did not advertise their build info, or the<br/>
        ///  player version is old enough not to send this bit of build info to the server.
        /// </summary>
        public static string GetPlayerBuildVersionCommitSha1(uint oPlayer)
        {
            return NWN.Core.NWScript.GetPlayerBuildVersionCommitSha1(oPlayer);
        }

        /// <summary>
        ///  In the spell script returns the feat used, or -1 if no feat was used.
        /// </summary>
        public static FeatType GetSpellFeatId()
        {
            return (FeatType)NWN.Core.NWScript.GetSpellFeatId();
        }

        /// <summary>
        ///  Returns the given effects Link ID. There is no guarantees about this identifier other than<br/>
        ///  it is unique and the same for all effects linked to it.
        /// </summary>
        public static string GetEffectLinkId(Effect eEffect)
        {
            return NWN.Core.NWScript.GetEffectLinkId(eEffect);
        }

        /// <summary>
        ///  If oCreature has nFeat, and nFeat is useable, returns the number of remaining uses left<br/>
        ///  or the maximum int value if the feat has unlimited uses (eg FEAT_KNOCKDOWN)<br/>
        ///  - nFeat: FEAT_*<br/>
        ///  - oCreature: Creature to check the feat of.
        /// </summary>
        public static int GetFeatRemainingUses(FeatType nFeat, uint oCreature = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetFeatRemainingUses((int)nFeat, oCreature);
        }

        /// <summary>
        ///  Change a tile in an area, it will also update the tile for all players in the area.<br/>
        ///  * Notes:<br/>
        ///    - For optimal use you should be familiar with how tilesets / .set files work.<br/>
        ///    - Will not update the height of non-creature objects.<br/>
        ///    - Creatures may get stuck on non-walkable terrain.<br/>
        /// <br/>
        ///  - locTile: The location of the tile.<br/>
        ///  - nTileID: the ID of the tile, for values see the .set file of the tileset.<br/>
        ///  - nOrientation: the orientation of the tile, 0-3.<br/>
        ///                  0 = Normal orientation<br/>
        ///                  1 = 90 degrees counterclockwise<br/>
        ///                  2 = 180 degrees counterclockwise<br/>
        ///                  3 = 270 degrees counterclockwise<br/>
        ///  - nHeight: the height of the tile.<br/>
        ///  - nFlags: a bitmask of SETTILE_FLAG_* constants.<br/>
        ///            - SETTILE_FLAG_RELOAD_GRASS: reloads the area&apos;s grass, use if your tile used to have grass or should have grass now.<br/>
        ///            - SETTILE_FLAG_RELOAD_BORDER: reloads the edge tile border, use if you changed a tile on the edge of the area.<br/>
        ///            - SETTILE_FLAG_RECOMPUTE_LIGHTING: recomputes the area&apos;s lighting and static shadows, use most of time.
        /// </summary>
        public static void SetTile(Location locTile, int nTileID, int nOrientation, int nHeight = 0, SetTileFlagType nFlags = SetTileFlagType.RecomputeLighting)
        {
            NWN.Core.NWScript.SetTile(locTile, nTileID, nOrientation, nHeight, (int)nFlags);
        }

        /// <summary>
        ///  Get the ID of the tile at location locTile.<br/>
        ///  Returns -1 on error.
        /// </summary>
        public static int GetTileID(Location locTile)
        {
            return NWN.Core.NWScript.GetTileID(locTile);
        }

        /// <summary>
        ///  Get the orientation of the tile at location locTile.<br/>
        ///  Returns -1 on error.
        /// </summary>
        public static int GetTileOrientation(Location locTile)
        {
            return NWN.Core.NWScript.GetTileOrientation(locTile);
        }

        /// <summary>
        ///  Get the height of the tile at location locTile.<br/>
        ///  Returns -1 on error.
        /// </summary>
        public static int GetTileHeight(Location locTile)
        {
            return NWN.Core.NWScript.GetTileHeight(locTile);
        }

        /// <summary>
        ///  All clients in oArea will reload the area&apos;s grass.<br/>
        ///  This can be used to update the grass of an area after changing a tile with SetTile() that will have or used to have grass.
        /// </summary>
        public static void ReloadAreaGrass(uint oArea)
        {
            NWN.Core.NWScript.ReloadAreaGrass(oArea);
        }

        /// <summary>
        ///  Set the state of the tile animation loops of the tile at location locTile.
        /// </summary>
        public static void SetTileAnimationLoops(Location locTile, bool bAnimLoop1, bool bAnimLoop2, bool bAnimLoop3)
        {
            NWN.Core.NWScript.SetTileAnimationLoops(locTile, bAnimLoop1 ? 1 : 0, bAnimLoop2 ? 1 : 0, bAnimLoop3 ? 1 : 0);
        }

        /// <summary>
        ///  Change multiple tiles in an area, it will also update the tiles for all players in the area.<br/>
        ///  Note: See SetTile() for additional information.<br/>
        ///  - oArea: the area to change one or more tiles of.<br/>
        ///  - jTileData: a JsonArray() with one or more JsonObject()s with the following keys:<br/>
        ///                - index: the index of the tile as a JsonInt()<br/>
        ///                         For example, a 3x3 area has the following tile indexes:<br/>
        ///                         6 7 8<br/>
        ///                         3 4 5<br/>
        ///                         0 1 2<br/>
        ///                - tileid: the ID of the tile as a JsonInt(), defaults to 0 if not set<br/>
        ///                - orientation: the orientation of the tile as JsonInt(), defaults to 0 if not set<br/>
        ///                - height: the height of the tile as JsonInt(), defaults to 0 if not set<br/>
        ///                - animloop1: the state of a tile animation, 1/0 as JsonInt(), defaults to the current value if not set<br/>
        ///                - animloop2: the state of a tile animation, 1/0 as JsonInt(), defaults to the current value if not set<br/>
        ///                - animloop3: the state of a tile animation, 1/0 as JsonInt(), defaults to the current value if not set<br/>
        ///  - nFlags: a bitmask of SETTILE_FLAG_* constants.<br/>
        ///  - sTileset: if not empty, it will also change the area&apos;s tileset<br/>
        ///              Warning: only use this if you really know what you&apos;re doing, it&apos;s very easy to break things badly.<br/>
        ///                       Make sure jTileData changes *all* tiles in the area and to a tile id that&apos;s supported by sTileset.
        /// </summary>
        public static void SetTileJson(uint oArea, Json jTileData, SetTileFlagType nFlags = SetTileFlagType.RecomputeLighting, string sTileset = "")
        {
            NWN.Core.NWScript.SetTileJson(oArea, jTileData, (int)nFlags, sTileset);
        }

        /// <summary>
        ///  All clients in oArea will reload the inaccesible border tiles.<br/>
        ///  This can be used to update the edge tiles after changing a tile with SetTile().
        /// </summary>
        public static void ReloadAreaBorder(uint oArea)
        {
            NWN.Core.NWScript.ReloadAreaBorder(oArea);
        }

        /// <summary>
        ///  Sets whether or not oCreatures&apos;s nIconId is flashing in their GUI icon bar.  If oCreature does not<br/>
        ///  have an icon associated with nIconId, nothing happens. This function does not add icons to <br/>
        ///  oCreatures&apos;s GUI icon bar. The icon will flash until the underlying effect is removed or this <br/>
        ///  function is called again with bFlashing = false.<br/>
        ///  - oCreature: Player object to affect<br/>
        ///  - nIconId: Referenced to effecticons.2da or EFFECT_ICON_*<br/>
        ///  - bFlashing: true to force an existing icon to flash, false to to stop.
        /// </summary>
        public static void SetEffectIconFlashing(uint oCreature, int nIconId, bool bFlashing = true)
        {
            NWN.Core.NWScript.SetEffectIconFlashing(oCreature, nIconId, bFlashing ? 1 : 0);
        }

        /// <summary>
        ///  Creates a bonus feat effect. These act like the Bonus Feat item property,<br/>
        ///  and do not work as feat prerequisites for levelup purposes.<br/>
        ///  - nFeat: FEAT_*
        /// </summary>
        public static Effect EffectBonusFeat(int nFeat)
        {
            return NWN.Core.NWScript.EffectBonusFeat(nFeat);
        }

        /// <summary>
        ///  Returns the INVENTORY_SLOT_* constant of the last item equipped.  Can only be used in the<br/>
        ///  module&apos;s OnPlayerEquip event.  Returns -1 on error.
        /// </summary>
        public static InventorySlotType GetPCItemLastEquippedSlot()
        {
            return (InventorySlotType)NWN.Core.NWScript.GetPCItemLastEquippedSlot();
        }

        /// <summary>
        ///  Returns the INVENTORY_SLOT_* constant of the last item unequipped.  Can only be used in the<br/>
        ///  module&apos;s OnPlayerUnequip event.  Returns -1 on error.
        /// </summary>
        public static InventorySlotType GetPCItemLastUnequippedSlot()
        {
            return (InventorySlotType)NWN.Core.NWScript.GetPCItemLastUnequippedSlot();
        }

        /// <summary>
        ///  Returns true if the last spell was cast spontaneously<br/>
        ///  eg; a Cleric casting SPELL_CURE_LIGHT_WOUNDS when it is not prepared, using another level 1 slot
        /// </summary>
        public static bool GetSpellCastSpontaneously()
        {
            return NWN.Core.NWScript.GetSpellCastSpontaneously() == 1;
        }

        /// <summary>
        ///  Reset the given sqlquery, readying it for re-execution after it has been stepped.<br/>
        ///  All existing binds are kept untouched, unless bClearBinds is true.<br/>
        ///  This command only works on successfully-prepared queries that have not errored out.
        /// </summary>
        public static void SqlResetQuery(SQLQuery sqlQuery, bool bClearBinds = false)
        {
            NWN.Core.NWScript.SqlResetQuery(sqlQuery, bClearBinds ? 1 : 0);
        }

        /// <summary>
        ///  Provides immunity to the effects of EffectTimeStop which allows actions during other creatures time stop effects.
        /// </summary>
        public static Effect EffectTimeStopImmunity()
        {
            return NWN.Core.NWScript.EffectTimeStopImmunity();
        }

        /// <summary>
        ///  Return the current game tick rate (mainloop iterations per second).<br/>
        ///  This is equivalent to graphics frames per second when the module is running inside a client.
        /// </summary>
        public static int GetTickRate()
        {
            return NWN.Core.NWScript.GetTickRate();
        }

        /// <summary>
        ///  Returns the level of the last spell cast. This value is only valid in a Spell script.
        /// </summary>
        public static int GetLastSpellLevel()
        {
            return NWN.Core.NWScript.GetLastSpellLevel();
        }

        /// <summary>
        ///  Returns the 32bit integer hash of sString<br/>
        ///  This hash is stable and will always have the same value for same input string, regardless of platform.<br/>
        ///  The hash algorithm is the same as the one used internally for strings in case statements, so you can do:<br/>
        ///     switch (HashString(sString))<br/>
        ///     {<br/>
        ///          case &quot;AAA&quot;:    HandleAAA(); break;<br/>
        ///          case &quot;BBB&quot;:    HandleBBB(); break;<br/>
        ///     }<br/>
        ///  NOTE: The exact algorithm used is XXH32(sString) ^ XXH32(&quot;&quot;). This means that HashString(&quot;&quot;) is 0.
        /// </summary>
        public static int HashString(string sString)
        {
            return NWN.Core.NWScript.HashString(sString);
        }

        /// <summary>
        ///  Returns the current microsecond counter value. This value is meaningless on its own, but can be subtracted<br/>
        ///  from other values returned by this function in the same script to get high resolution elapsed time:<br/>
        ///      int nMicrosecondsStart = GetMicrosecondCounter();<br/>
        ///      DoSomething();<br/>
        ///      int nElapsedMicroseconds = GetMicrosecondCounter() - nMicrosecondsStart;
        /// </summary>
        public static int GetMicrosecondCounter()
        {
            return NWN.Core.NWScript.GetMicrosecondCounter();
        }

        /// <summary>
        ///  Forces the creature to always walk.
        /// </summary>
        public static Effect EffectForceWalk()
        {
            return NWN.Core.NWScript.EffectForceWalk();
        }

        /// <summary>
        ///  Assign one of the available audio streams to play a specific file. This mechanism can be used<br/>
        ///  to replace regular music playback, and synchronize it between clients.<br/>
        ///  * There is currently no way to get playback state from clients.<br/>
        ///  * Audio streams play in the streams channel which has its own volume setting in the client.<br/>
        ///  * nStreamIdentifier is one of AUDIOSTREAM_IDENTIFIER_*.<br/>
        ///  * Currently, only MP3 CBR files are supported and properly seekable.<br/>
        ///  * Unlike regular music, audio streams do not pause on load screens.<br/>
        ///  * If fSeekOffset is at or beyond the end of the stream, the seek offset will wrap around, even if the file is configured not to loop.<br/>
        ///  * fFadeTime is in seconds to gradually fade in the audio instead of starting directly.<br/>
        ///  * Only one type of fading can be active at once, for example:<br/>
        ///    If you call StartAudioStream() with fFadeTime = 10.0f, any other audio stream functions with a fade time &gt;0.0f will have no effect<br/>
        ///    until StartAudioStream() is done fading.
        /// </summary>
        public static void StartAudioStream(uint oPlayer, int nStreamIdentifier, string sResRef, bool bLooping = false, float fFadeTime = 0.0f, float fSeekOffset = -1.0f, float fVolume = 1.0f)
        {
            NWN.Core.NWScript.StartAudioStream(oPlayer, nStreamIdentifier, sResRef, bLooping ? 1 : 0, fFadeTime, fSeekOffset, fVolume);
        }

        /// <summary>
        ///  Stops the given audio stream.<br/>
        ///  * fFadeTime is in seconds to gradually fade out the audio instead of stopping directly.<br/>
        ///  * Only one type of fading can be active at once, for example:<br/>
        ///    If you call StartAudioStream() with fFadeInTime = 10.0f, any other audio stream functions with a fade time &gt;0.0f will have no effect<br/>
        ///    until StartAudioStream() is done fading.<br/>
        ///  * Will do nothing if the stream is currently not in use.
        /// </summary>
        public static void StopAudioStream(uint oPlayer, int nStreamIdentifier, float fFadeTime = 0.0f)
        {
            NWN.Core.NWScript.StopAudioStream(oPlayer, nStreamIdentifier, fFadeTime);
        }

        /// <summary>
        ///  Un/pauses the given audio stream.<br/>
        ///  * fFadeTime is in seconds to gradually fade the audio out/in instead of pausing/resuming directly.<br/>
        ///  * Only one type of fading can be active at once, for example:<br/>
        ///    If you call StartAudioStream() with fFadeInTime = 10.0f, any other audio stream functions with a fade time &gt;0.0f will have no effect<br/>
        ///    until StartAudioStream() is done fading.<br/>
        ///  * Will do nothing if the stream is currently not in use.
        /// </summary>
        public static void SetAudioStreamPaused(uint oPlayer, int nStreamIdentifier, bool bPaused, float fFadeTime = 0.0f)
        {
            NWN.Core.NWScript.SetAudioStreamPaused(oPlayer, nStreamIdentifier, bPaused ? 1 : 0, fFadeTime);
        }

        /// <summary>
        ///  Change volume of audio stream.<br/>
        ///  * Volume is from 0.0 to 1.0.<br/>
        ///  * fFadeTime is in seconds to gradually change the volume.<br/>
        ///  * Only one type of fading can be active at once, for example:<br/>
        ///    If you call StartAudioStream() with fFadeInTime = 10.0f, any other audio stream functions with a fade time &gt;0.0f will have no effect<br/>
        ///    until StartAudioStream() is done fading.<br/>
        ///  * Subsequent calls to this function with fFadeTime &gt;0.0f while already fading the volume<br/>
        ///    will start the new fade with the previous&apos; fade&apos;s progress as starting point.<br/>
        ///  * Will do nothing if the stream is currently not in use.
        /// </summary>
        public static void SetAudioStreamVolume(uint oPlayer, int nStreamIdentifier, float fVolume = 1.0f, float fFadeTime = 0.0f)
        {
            NWN.Core.NWScript.SetAudioStreamVolume(oPlayer, nStreamIdentifier, fVolume, fFadeTime);
        }

        /// <summary>
        ///  Seek the audio stream to the given offset.<br/>
        ///  * When seeking at or beyond the end of a stream, the seek offset will wrap around, even if the file is configured not to loop.<br/>
        ///  * Will do nothing if the stream is currently not in use.<br/>
        ///  * Will do nothing if the stream is in ended state (reached end of file and looping is off). In this<br/>
        ///    case, you need to restart the stream.
        /// </summary>
        public static void SeekAudioStream(uint oPlayer, int nStreamIdentifier, float fSeconds)
        {
            NWN.Core.NWScript.SeekAudioStream(oPlayer, nStreamIdentifier, fSeconds);
        }

        /// <summary>
        ///  Sets the effect creator<br/>
        ///  - oCreator: The creator of the effect. Can be OBJECT_INVALID.
        /// </summary>
        public static Effect SetEffectCreator(Effect eEffect, uint oCreator)
        {
            return NWN.Core.NWScript.SetEffectCreator(eEffect, oCreator);
        }

        /// <summary>
        ///  Sets the effect caster level<br/>
        ///  - nCasterLevel: The caster level of the effect for the purposes of dispel magic and GetEffectCasterlevel. Must be &gt;= 0.
        /// </summary>
        public static Effect SetEffectCasterLevel(Effect eEffect, int nCasterLevel)
        {
            return NWN.Core.NWScript.SetEffectCasterLevel(eEffect, nCasterLevel);
        }

        /// <summary>
        ///  Sets the effect spell id<br/>
        ///  - nSpellId: The spell id for the purposes of effect stacking, dispel magic and GetEffectSpellId. Must be &gt;= -1 (-1 being invalid/no spell)
        /// </summary>
        public static Effect SetEffectSpellId(Effect eEffect, int nSpellId)
        {
            return NWN.Core.NWScript.SetEffectSpellId(eEffect, nSpellId);
        }

        /// <summary>
        ///  Retrieve the column count of a prepared query.  <br/>
        ///  * sqlQuery must be prepared before this function is called, but can be called before or after parameters are bound.<br/>
        ///  * If the prepared query contains no columns (such as with an UPDATE or INSERT query), 0 is returned.<br/>
        ///  * If a non-SELECT query contains a RETURNING clause, the number of columns in the RETURNING clause will be returned.<br/>
        ///  * A returned value greater than 0 does not guarantee the query will return rows.
        /// </summary>
        public static int SqlGetColumnCount(SQLQuery sqlQuery)
        {
            return NWN.Core.NWScript.SqlGetColumnCount(sqlQuery);
        }

        /// <summary>
        ///  Retrieve the column name of the Nth column of a prepared query.<br/>
        ///  * sqlQuery must be prepared before this function is called, but can be called before or after parameters are bound.<br/>
        ///  * If the prepared query contains no columns (such as with an UPDATE or INSERT query), an empty string is returned.<br/>
        ///  * If a non-SELECT query contains a RETURNING clause, the name of the nNth column in the RETURNING clause is returned.<br/>
        ///  * If nNth is out of range, an sqlite error is broadcast and an empty string is returned.<br/>
        ///  * The value of the AS clause will be returned, if the clause exists for the nNth column.<br/>
        ///  * A returned non-empty string does not guarantee the query will return rows.
        /// </summary>
        public static string SqlGetColumnName(SQLQuery sqlQuery, int nNth)
        {
            return NWN.Core.NWScript.SqlGetColumnName(sqlQuery, nNth);
        }

        /// <summary>
        ///  Gets the total number of spell abilities a creature has.
        /// </summary>
        public static int GetSpellAbilityCount(uint oCreature)
        {
            return NWN.Core.NWScript.GetSpellAbilityCount(oCreature);
        }

        /// <summary>
        ///  Gets the spell Id of the spell ability at the given index.<br/>
        ///  - nIndex: the index of the spell ability. Bounds: 0 &lt;= nIndex &lt; GetSpellAbilityCount()<br/>
        ///  Returns: a SPELL_* constant or -1 if the slot is not set.
        /// </summary>
        public static SpellType GetSpellAbilitySpell(uint oCreature, int nIndex)
        {
            return (SpellType)NWN.Core.NWScript.GetSpellAbilitySpell(oCreature, nIndex);
        }

        /// <summary>
        ///  Gets the caster level of the spell ability in the given slot. Returns 0 by default.<br/>
        ///  - nIndex: the index of the spell ability. Bounds: 0 &lt;= nIndex &lt; GetSpellAbilityCount()<br/>
        ///  Returns: the caster level or -1 if the slot is not set.
        /// </summary>
        public static int GetSpellAbilityCasterLevel(uint oCreature, int nIndex)
        {
            return NWN.Core.NWScript.GetSpellAbilityCasterLevel(oCreature, nIndex);
        }

        /// <summary>
        ///  Gets the ready state of a spell ability.<br/>
        ///  - nIndex: the index of the spell ability. Bounds: 0 &lt;= nIndex &lt; GetSpellAbilityCount()<br/>
        ///  Returns: true/FALSE or -1 if the slot is not set.
        /// </summary>
        public static bool GetSpellAbilityReady(uint oCreature, int nIndex)
        {
            return NWN.Core.NWScript.GetSpellAbilityReady(oCreature, nIndex) == 1;
        }

        /// <summary>
        ///  Set the ready state of a spell ability slot.<br/>
        ///  - nIndex: the index of the spell ability. Bounds: 0 &lt;= nIndex &lt; GetSpellAbilityCount()<br/>
        ///  - bReady: true to mark the slot ready.
        /// </summary>
        public static void SetSpellAbilityReady(uint oCreature, int nIndex, bool bReady = true)
        {
            NWN.Core.NWScript.SetSpellAbilityReady(oCreature, nIndex, bReady ? 1 : 0);
        }

        /// <summary>
        ///  Serializes the given JSON structure (which must be a valid template spec) into a template.<br/>
        ///  * The template will be stored in the TEMP: alias and currently NOT stored in savegames.<br/>
        ///  * The stored template will override anything currently available in the module.<br/>
        ///  * Supported GFF resource types are the same as TemplateToJson().<br/>
        ///    However, some types will not be read by the game (e.g. module.IFO is only read at module load).<br/>
        ///  * Returns true if the serialization was successful.<br/>
        ///  * Any target file in TEMP: will be overwritten, even if the serialisation is not successful.<br/>
        ///    JsonToTemplate(JSON_NULL, ..) can be used to delete a previously-generated file.
        /// </summary>
        public static int JsonToTemplate(Json jTemplateSpec, string sResRef, int nResType)
        {
            return NWN.Core.NWScript.JsonToTemplate(jTemplateSpec, sResRef, nResType);
        }

        /// <summary>
        ///  Modifies jObject in-place (with no memory copies of the full object).<br/>
        ///  jObject will have the key at sKey set to jValue.
        /// </summary>
        public static void JsonObjectSetInplace(Json jObject, string sKey, Json jValue)
        {
            NWN.Core.NWScript.JsonObjectSetInplace(jObject, sKey, jValue);
        }

        /// <summary>
        ///  Modifies jObject in-place (with no memory copies needed).<br/>
        ///  jObject will have the element at the key sKey removed.<br/>
        ///  Will do nothing if jObject is not a object, or sKey does not exist on the object.
        /// </summary>
        public static void JsonObjectDelInplace(Json jObject, string sKey)
        {
            NWN.Core.NWScript.JsonObjectDelInplace(jObject, sKey);
        }

        /// <summary>
        ///  Modifies jArray in-place (with no memory copies needed).<br/>
        ///  jArray will have jValue inserted at position nIndex.<br/>
        ///  All succeeding elements in the array will move by one.<br/>
        ///  By default (-1), inserts elements at the end of the array (&quot;push&quot;).<br/>
        ///  nIndex = 0 inserts at the beginning of the array.
        /// </summary>
        public static void JsonArrayInsertInplace(Json jArray, Json jValue, int nIndex = -1)
        {
            NWN.Core.NWScript.JsonArrayInsertInplace(jArray, jValue, nIndex);
        }

        /// <summary>
        ///  Modifies jArray in-place (with no memory copies needed).<br/>
        ///  jArray will have jValue set at position nIndex.<br/>
        ///  Will do nothing if jArray is not an array or nIndex is out of range.
        /// </summary>
        public static void JsonArraySetInplace(Json jArray, int nIndex, Json jValue)
        {
            NWN.Core.NWScript.JsonArraySetInplace(jArray, nIndex, jValue);
        }

        /// <summary>
        ///  Modifies jArray in-place (with no memory copies needed).<br/>
        ///  jArray will have the element at nIndex removed, and the array will be resized accordingly.<br/>
        ///  Will do nothing if jArray is not an array or nIndex is out of range.
        /// </summary>
        public static void JsonArrayDelInplace(Json jArray, int nIndex)
        {
            NWN.Core.NWScript.JsonArrayDelInplace(jArray, nIndex);
        }

        /// <summary>
        ///  Sets a grass override for nMaterialId in oArea.<br/>
        ///  * You can have multiple grass types per area by using different materials.<br/>
        ///  * You can add grass to areas that normally do not have grass, for example by calling this on the<br/>
        ///    wood surface material(5) for an inn area.<br/>
        /// <br/>
        ///    - nMaterialId: a surface material, see surfacemat.2da. 3 is the default grass material.<br/>
        ///    - sTexture: the grass texture, cannot be empty.<br/>
        ///    - fDensity: the density of the grass.<br/>
        ///    - fHeight: the height of the grass.<br/>
        ///    - vAmbientColor: the ambient color of the grass, xyz as RGB clamped to 0.0-1.0f per value.<br/>
        ///    - vDiffuseColor: the diffuse color of the grass, xyz as RGB clamped to 0.0-1.0f per value.
        /// </summary>
        public static void SetAreaGrassOverride(uint oArea, int nMaterialId, string sTexture, float fDensity, float fHeight, Vector3 vAmbientColor, Vector3 vDiffuseColor)
        {
            NWN.Core.NWScript.SetAreaGrassOverride(oArea, nMaterialId, sTexture, fDensity, fHeight, vAmbientColor, vDiffuseColor);
        }

        /// <summary>
        ///  Remove a grass override from oArea for nMaterialId.
        /// </summary>
        public static void RemoveAreaGrassOverride(uint oArea, int nMaterialId)
        {
            NWN.Core.NWScript.RemoveAreaGrassOverride(oArea, nMaterialId);
        }

        /// <summary>
        ///  Set to true to disable the default grass of oArea.
        /// </summary>
        public static void SetAreaDefaultGrassDisabled(uint oArea, bool bDisabled)
        {
            NWN.Core.NWScript.SetAreaDefaultGrassDisabled(oArea, bDisabled ? 1 : 0);
        }

        /// <summary>
        ///  Gets the NoRest area flag.<br/>
        ///  Returns true if resting is not allowed in the area.<br/>
        ///  Passing in OBJECT_INVALID to parameter oArea will result in operating on the area of the caller.
        /// </summary>
        public static bool GetAreaNoRestFlag(uint oArea = OBJECT_INVALID)
        {
            return NWN.Core.NWScript.GetAreaNoRestFlag(oArea) == 1;
        }

        /// <summary>
        ///  Sets the NoRest flag on an area.<br/>
        ///  Passing in OBJECT_INVALID to parameter oArea will result in operating on the area of the caller.
        /// </summary>
        public static void SetAreaNoRestFlag(bool bNoRestFlag, uint oArea = OBJECT_INVALID)
        {
            NWN.Core.NWScript.SetAreaNoRestFlag(bNoRestFlag ? 1 : 0, oArea);
        }

        /// <summary>
        ///  Sets the age of oCreature.
        /// </summary>
        public static void SetAge(uint oCreature, int nAge)
        {
            NWN.Core.NWScript.SetAge(oCreature, nAge);
        }

        /// <summary>
        ///  Gets the base number of attacks oCreature can make every round<br/>
        ///  Excludes additional effects such as haste, slow, spells, circle kick, attack modes, etc.<br/>
        ///  * bCheckOverridenValue - Checks for SetBaseAttackBonus() on the creature, if false will return the non-overriden version
        /// </summary>
        public static int GetAttacksPerRound(uint oCreature, bool bCheckOverridenValue = true)
        {
            return NWN.Core.NWScript.GetAttacksPerRound(oCreature, bCheckOverridenValue ? 1 : 0);
        }

        /// <summary>
        ///  Create an Enemy Attack Bonus effect. Creatures attacking the given creature with melee/ranged attacks or touch attacks get a bonus to hit.
        /// </summary>
        public static Effect EffectEnemyAttackBonus(int nBonus)
        {
            return NWN.Core.NWScript.EffectEnemyAttackBonus(nBonus);
        }

        /// <summary>
        ///  Set to true to disable the inaccessible tile border of oArea. Requires a client area reload to take effect.
        /// </summary>
        public static void SetAreaTileBorderDisabled(uint oArea, bool bDisabled)
        {
            NWN.Core.NWScript.SetAreaTileBorderDisabled(oArea, bDisabled ? 1 : 0);
        }
    }
}
