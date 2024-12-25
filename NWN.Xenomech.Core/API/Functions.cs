using System.Numerics;
using NWN.Xenomech.Core.API.Enum;
using NWN.Xenomech.Core.API.Enum.Creature;
using NWN.Xenomech.Core.Interop;

namespace NWN.Xenomech.Core.API
{
    public partial class NWScript
    {
        // ReSharper disable once InconsistentNaming
        public static uint OBJECT_SELF => Internal.OBJECT_SELF;

        /// <summary>
        ///   Assign aActionToAssign to oActionSubject.
        ///   * No return value, but if an error occurs, the log file will contain
        ///   "AssignCommand failed."
        ///   (If the object doesn't exist, nothing happens.)
        /// </summary>
        public static void AssignCommand(uint oActionSubject, ActionDelegate aActionToAssign)
        {
            Internal.ClosureAssignCommand(oActionSubject, aActionToAssign);
        }

        /// <summary>
        ///   Delay aActionToDelay by fSeconds.
        ///   * No return value, but if an error occurs, the log file will contain
        ///   "DelayCommand failed.".
        ///   It is suggested that functions which create effects should not be used
        ///   as parameters to delayed actions.  Instead, the effect should be created in the
        ///   script and then passed into the action.  For example:
        ///   effect eDamage = EffectDamage(nDamage, DAMAGE_TYPE_MAGICAL);
        ///   DelayCommand(fDelay, ApplyEffectToObject(DURATION_TYPE_INSTANT, eDamage, oTarget);
        /// </summary>
        public static void DelayCommand(float fSeconds, ActionDelegate aActionToDelay)
        {
            Internal.ClosureDelayCommand(Internal.OBJECT_SELF, fSeconds, aActionToDelay);
        }


        /// <summary>
        ///   Do aActionToDo.
        /// </summary>
        public static void ActionDoCommand(ActionDelegate aActionToDo)
        {
            Internal.ClosureActionDoCommand(Internal.OBJECT_SELF, aActionToDo);
        }

        /// <summary>
        ///   Get an integer between 0 and nMaxInteger-1.
        ///   Return value on error: 0
        /// </summary>
        public static int Random(int nMaxInteger)
        {
            NWNXPInvoke.StackPushInteger(nMaxInteger);
            NWNXPInvoke.CallBuiltIn(0);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Output sString to the log file.
        /// </summary>
        public static void PrintString(string sString)
        {
            NWNXPInvoke.StackPushString(sString);
            NWNXPInvoke.CallBuiltIn(1);
        }

        /// <summary>
        ///   Output a formatted float to the log file.
        ///   - nWidth should be a value from 0 to 18 inclusive.
        ///   - nDecimals should be a value from 0 to 9 inclusive.
        /// </summary>
        public static void PrintFloat(float fFloat, int nWidth = 18, int nDecimals = 9)
        {
            NWNXPInvoke.StackPushInteger(nDecimals);
            NWNXPInvoke.StackPushInteger(nWidth);
            NWNXPInvoke.StackPushFloat(fFloat);
            NWNXPInvoke.CallBuiltIn(2);
        }

        /// <summary>
        ///   Convert fFloat into a string.
        ///   - nWidth should be a value from 0 to 18 inclusive.
        ///   - nDecimals should be a value from 0 to 9 inclusive.
        /// </summary>
        public static string FloatToString(float fFloat, int nWidth = 18, int nDecimals = 9)
        {
            NWNXPInvoke.StackPushInteger(nDecimals);
            NWNXPInvoke.StackPushInteger(nWidth);
            NWNXPInvoke.StackPushFloat(fFloat);
            NWNXPInvoke.CallBuiltIn(3);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Output nInteger to the log file.
        /// </summary>
        public static void PrintInteger(int nInteger)
        {
            NWNXPInvoke.StackPushInteger(nInteger);
            NWNXPInvoke.CallBuiltIn(4);
        }

        /// <summary>
        ///   Output oObject's ID to the log file.
        /// </summary>
        public static void PrintObject(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(5);
        }

        /// <summary>
        ///   Clear all the actions of the caller.
        ///   * No return value, but if an error occurs, the log file will contain
        ///   "ClearAllActions failed.".
        ///   - nClearCombatState: if true, this will immediately clear the combat state
        ///   on a creature, which will stop the combat music and allow them to rest,
        ///   engage in dialog, or other actions that they would normally have to wait for.
        /// </summary>
        public static void ClearAllActions(bool nClearCombatState = false)
        {
            NWNXPInvoke.StackPushInteger(nClearCombatState ? 1 : 0);
            NWNXPInvoke.CallBuiltIn(9);
        }

        /// <summary>
        ///   Cause the caller to face fDirection.
        ///   - fDirection is expressed as anticlockwise degrees from Due East.
        ///   DIRECTION_EAST, DIRECTION_NORTH, DIRECTION_WEST and DIRECTION_SOUTH are
        ///   predefined. (0.0f=East, 90.0f=North, 180.0f=West, 270.0f=South)
        /// </summary>
        public static void SetFacing(float fDirection)
        {
            NWNXPInvoke.StackPushFloat(fDirection);
            NWNXPInvoke.CallBuiltIn(10);
        }

        /// <summary>
        ///   The action subject will generate a random location near its current location
        ///   and pathfind to it.  ActionRandomwalk never ends, which means it is necessary
        ///   to call ClearAllActions in order to allow a creature to perform any other action
        ///   once ActionRandomWalk has been called.
        ///   * No return value, but if an error occurs the log file will contain
        ///   "ActionRandomWalk failed."
        /// </summary>
        public static void ActionRandomWalk()
        {
            NWNXPInvoke.CallBuiltIn(20);
        }

        /// <summary>
        ///   The action subject will move to lDestination.
        ///   - lDestination: The object will move to this location.  If the location is
        ///   invalid or a path cannot be found to it, the command does nothing.
        ///   - bRun: If this is TRUE, the action subject will run rather than walk
        ///   * No return value, but if an error occurs the log file will contain
        ///   "MoveToPoint failed."
        /// </summary>
        public static void ActionMoveToLocation(Location lDestination, bool bRun = false)
        {
            NWNXPInvoke.StackPushInteger(bRun ? 1 : 0);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lDestination);
            NWNXPInvoke.CallBuiltIn(21);
        }

        /// <summary>
        ///   Cause the action subject to move to a certain distance from oMoveTo.
        ///   If there is no path to oMoveTo, this command will do nothing.
        ///   - oMoveTo: This is the object we wish the action subject to move to
        ///   - bRun: If this is TRUE, the action subject will run rather than walk
        ///   - fRange: This is the desired distance between the action subject and oMoveTo
        ///   * No return value, but if an error occurs the log file will contain
        ///   "ActionMoveToObject failed."
        /// </summary>
        public static void ActionMoveToObject(uint oMoveTo, bool bRun = false, float fRange = 1.0f)
        {
            NWNXPInvoke.StackPushFloat(fRange);
            NWNXPInvoke.StackPushInteger(bRun ? 1 : 0);
            NWNXPInvoke.StackPushObject(oMoveTo);
            NWNXPInvoke.CallBuiltIn(22);
        }

        /// <summary>
        ///   Cause the action subject to move to a certain distance away from oFleeFrom.
        ///   - oFleeFrom: This is the object we wish the action subject to move away from.
        ///   If oFleeFrom is not in the same area as the action subject, nothing will
        ///   happen.
        ///   - bRun: If this is TRUE, the action subject will run rather than walk
        ///   - fMoveAwayRange: This is the distance we wish the action subject to put
        ///   between themselves and oFleeFrom
        ///   * No return value, but if an error occurs the log file will contain
        ///   "ActionMoveAwayFromObject failed."
        /// </summary>
        public static void ActionMoveAwayFromObject(uint oFleeFrom, bool bRun = false, float fMoveAwayRange = 40.0f)
        {
            NWNXPInvoke.StackPushFloat(fMoveAwayRange);
            NWNXPInvoke.StackPushInteger(bRun ? 1 : 0);
            NWNXPInvoke.StackPushObject(oFleeFrom);
            NWNXPInvoke.CallBuiltIn(23);
        }

        /// <summary>
        ///   Get the direction in which oTarget is facing, expressed as a float between
        ///   0.0f and 360.0f
        ///   * Return value on error: -1.0f
        /// </summary>
        public static float GetFacing(uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(28);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        ///   Get the last attacker of oAttackee.  This should only be used ONLY in the
        ///   OnAttacked events for creatures, placeables and doors.
        ///   * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetLastAttacker(uint oAttackee = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oAttackee);
            NWNXPInvoke.CallBuiltIn(36);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Attack oAttackee.
        ///   - bPassive: If this is TRUE, attack is in passive mode.
        /// </summary>
        public static void ActionAttack(uint oAttackee, bool bPassive = false)
        {
            NWNXPInvoke.StackPushInteger(bPassive ? 1 : 0);
            NWNXPInvoke.StackPushObject(oAttackee);
            NWNXPInvoke.CallBuiltIn(37);
        }
        /// <summary>
        ///   Get the creature nearest to oTarget, subject to all the criteria specified.
        ///   - nFirstCriteriaType: CREATURE_TYPE_*
        ///   - nFirstCriteriaValue:
        ///   -> CLASS_TYPE_* if nFirstCriteriaType was CREATURE_TYPE_CLASS
        ///   -> SPELL_* if nFirstCriteriaType was CREATURE_TYPE_DOES_NOT_HAVE_SPELL_EFFECT
        ///   or CREATURE_TYPE_HAS_SPELL_EFFECT
        ///   -> TRUE or FALSE if nFirstCriteriaType was CREATURE_TYPE_IS_ALIVE
        ///   -> PERCEPTION_* if nFirstCriteriaType was CREATURE_TYPE_PERCEPTION
        ///   -> PLAYER_CHAR_IS_PC or PLAYER_CHAR_NOT_PC if nFirstCriteriaType was
        ///   CREATURE_TYPE_PLAYER_CHAR
        ///   -> RACIAL_TYPE_* if nFirstCriteriaType was CREATURE_TYPE_RACIAL_TYPE
        ///   -> REPUTATION_TYPE_* if nFirstCriteriaType was CREATURE_TYPE_REPUTATION
        ///   For example, to get the nearest PC, use:
        ///   (CREATURE_TYPE_PLAYER_CHAR, PLAYER_CHAR_IS_PC)
        ///   - oTarget: We're trying to find the creature of the specified type that is
        ///   nearest to oTarget
        ///   - nNth: We don't have to find the first nearest: we can find the Nth nearest...
        ///   - nSecondCriteriaType: This is used in the same way as nFirstCriteriaType to
        ///   further specify the type of creature that we are looking for.
        ///   - nSecondCriteriaValue: This is used in the same way as nFirstCriteriaValue
        ///   to further specify the type of creature that we are looking for.
        ///   - nThirdCriteriaType: This is used in the same way as nFirstCriteriaType to
        ///   further specify the type of creature that we are looking for.
        ///   - nThirdCriteriaValue: This is used in the same way as nFirstCriteriaValue to
        ///   further specify the type of creature that we are looking for.
        ///   * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetNearestCreature(CreatureType nFirstCriteriaType, int nFirstCriteriaValue,
            uint oTarget = OBJECT_INVALID, int nNth = 1, int nSecondCriteriaType = -1, int nSecondCriteriaValue = -1,
            int nThirdCriteriaType = -1, int nThirdCriteriaValue = -1)
        {
            NWNXPInvoke.StackPushInteger(nThirdCriteriaValue);
            NWNXPInvoke.StackPushInteger(nThirdCriteriaType);
            NWNXPInvoke.StackPushInteger(nSecondCriteriaValue);
            NWNXPInvoke.StackPushInteger(nSecondCriteriaType);
            NWNXPInvoke.StackPushInteger(nNth);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushInteger(nFirstCriteriaValue);
            NWNXPInvoke.StackPushInteger((int)nFirstCriteriaType);
            NWNXPInvoke.CallBuiltIn(38);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Add a speak action to the action subject.
        ///   - sStringToSpeak: String to be spoken
        ///   - nTalkVolume: TALKVOLUME_*
        /// </summary>
        public static void ActionSpeakString(string sStringToSpeak, TalkVolume nTalkVolume = TalkVolume.Talk)
        {
            NWNXPInvoke.StackPushInteger((int)nTalkVolume);
            NWNXPInvoke.StackPushString(sStringToSpeak);
            NWNXPInvoke.CallBuiltIn(39);
        }
        /// <summary>
        ///   Cause the action subject to play an animation
        ///   - nAnimation: ANIMATION_*
        ///   - fSpeed: Speed of the animation
        ///   - fDurationSeconds: Duration of the animation (this is not used for Fire and
        ///   Forget animations)
        /// </summary>
        public static void ActionPlayAnimation(Animation nAnimation, float fSpeed = 1.0f, float fDurationSeconds = 0.0f)
        {
            NWNXPInvoke.StackPushFloat(fDurationSeconds);
            NWNXPInvoke.StackPushFloat(fSpeed);
            NWNXPInvoke.StackPushInteger((int)nAnimation);
            NWNXPInvoke.CallBuiltIn(40);
        }

        /// <summary>
        ///   Change the direction in which the camera is facing
        ///   - fDirection is expressed as anticlockwise degrees from Due East.
        ///   (0.0f=East, 90.0f=North, 180.0f=West, 270.0f=South)
        ///   A value of -1.0f for any parameter will be ignored and instead it will
        ///   use the current camera value.
        ///   This can be used to change the way the camera is facing after the player
        ///   emerges from an area transition.
        ///   - nTransitionType: CAMERA_TRANSITION_TYPE_*  SNAP will immediately move the
        ///   camera to the new position, while the other types will result in the camera moving gradually into position
        ///   Pitch and distance are limited to valid values for the current camera mode:
        ///   Top Down: Distance = 5-20, Pitch = 1-50
        ///   Driving camera: Distance = 6 (can't be changed), Pitch = 1-62
        ///   Chase: Distance = 5-20, Pitch = 1-50
        ///   *** NOTE *** In NWN:Hordes of the Underdark the camera limits have been relaxed to the following:
        ///   Distance 1-25
        ///   Pitch 1-89
        /// </summary>
        public static void SetCameraFacing(float fDirection, float fDistance = -1.0f, float fPitch = -1.0f,
            CameraTransitionType nTransitionType = CameraTransitionType.Snap)
        {
            NWNXPInvoke.StackPushInteger((int)nTransitionType);
            NWNXPInvoke.StackPushFloat(fPitch);
            NWNXPInvoke.StackPushFloat(fDistance);
            NWNXPInvoke.StackPushFloat(fDirection);
            NWNXPInvoke.CallBuiltIn(45);
        }

        /// <summary>
        ///   Play sSoundName
        ///   - sSoundName: TBD - SS
        ///   This will play a mono sound from the location of the object running the command.
        /// </summary>
        public static void PlaySound(string sSoundName)
        {
            NWNXPInvoke.StackPushString(sSoundName);
            NWNXPInvoke.CallBuiltIn(46);
        }

        /// <summary>
        ///   Get the object at which the caller last cast a spell
        ///   * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetSpellTargetObject()
        {
            NWNXPInvoke.CallBuiltIn(47);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   This action casts a spell at oTarget.
        ///   - nSpell: SPELL_*
        ///   - oTarget: Target for the spell
        ///   - nMetaMagic: METAMAGIC_*
        ///   - bCheat: If this is TRUE, then the executor of the action doesn't have to be
        ///   able to cast the spell.
        ///   - nDomainLevel: The level of the spell if cast from a domain slot.
        ///     eg SPELL_HEAL can be spell level 5 on a cleric. Use 0 for no domain slot.
        ///   - nProjectilePathType: PROJECTILE_PATH_TYPE_*
        ///   - bInstantSpell: If this is TRUE, the spell is cast immediately. This allows
        ///   the end-user to simulate a high-level magic-user having lots of advance
        ///   warning of impending trouble
        /// - nClass: If set to a CLASS_TYPE_* it will cast using that class specifically.
        ///   CLASS_TYPE_INVALID will use spell abilities.
        /// - bSpontaneousCast: If set to TRUE will attempt to cast the given spell spontaneously,
        ///   ie a Cleric casting Cure Light Wounds using any level 1 slot. Needs a valid nClass set.
        /// </summary>
        public static void ActionCastSpellAtObject(
            Spell nSpell,
            uint oTarget,
            MetaMagic nMetaMagic = MetaMagic.Any,
            bool nCheat = false,
            int nDomainLevel = 0,
            ProjectilePathType nProjectilePathType = ProjectilePathType.Default,
            bool bInstantSpell = false,
            int nClass = -1,
            bool bSpontaneousCast = false)
        {
            NWNXPInvoke.StackPushInteger(bSpontaneousCast ? 1 : 0);
            NWNXPInvoke.StackPushInteger(nClass);
            NWNXPInvoke.StackPushInteger(bInstantSpell ? 1 : 0);
            NWNXPInvoke.StackPushInteger((int)nProjectilePathType);
            NWNXPInvoke.StackPushInteger(nDomainLevel);
            NWNXPInvoke.StackPushInteger(nCheat ? 1 : 0);
            NWNXPInvoke.StackPushInteger((int)nMetaMagic);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushInteger((int)nSpell);
            NWNXPInvoke.CallBuiltIn(48);
        }

        /// <summary>
        ///   Get oObject's local integer variable sVarName
        ///   * Return value on error: 0
        /// </summary>
        public static int GetLocalInt(uint oObject, string sVarName)
        {
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(51);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Get oObject's local boolean variable sVarName
        /// * Return value on error: false
        /// </summary>
        public static bool GetLocalBool(uint oObject, string sVarName)
        {
            return Convert.ToBoolean(GetLocalInt(oObject, sVarName));
        }

        /// <summary>
        ///   Get oObject's local float variable sVarName
        ///   * Return value on error: 0.0f
        /// </summary>
        public static float GetLocalFloat(uint oObject, string sVarName)
        {
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(52);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        ///   Get oObject's local string variable sVarName
        ///   * Return value on error: ""
        /// </summary>
        public static string GetLocalString(uint oObject, string sVarName)
        {
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(53);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Get oObject's local object variable sVarName
        ///   * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetLocalObject(uint oObject, string sVarName)
        {
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(54);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Set oObject's local integer variable sVarName to nValue
        /// </summary>
        public static void SetLocalInt(uint oObject, string sVarName, int nValue)
        {
            NWNXPInvoke.StackPushInteger(nValue);
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(55);
        }

        /// <summary>
        /// Set oObject's local boolean variable sVarName to nValue
        /// </summary>
        public static void SetLocalBool(uint oObject, string sVarName, bool nValue)
        {
            SetLocalInt(oObject, sVarName, Convert.ToInt32(nValue));
        }

        /// <summary>
        ///   Set oObject's local float variable sVarName to nValue
        /// </summary>
        public static void SetLocalFloat(uint oObject, string sVarName, float fValue)
        {
            NWNXPInvoke.StackPushFloat(fValue);
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(56);
        }

        /// <summary>
        ///   Set oObject's local string variable sVarName to nValue
        /// </summary>
        public static void SetLocalString(uint oObject, string sVarName, string sValue)
        {
            NWNXPInvoke.StackPushString(sValue);
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(57);
        }

        /// <summary>
        ///   Set oObject's local object variable sVarName to nValue
        /// </summary>
        public static void SetLocalObject(uint oObject, string sVarName, uint oValue)
        {
            NWNXPInvoke.StackPushObject(oValue);
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(58);
        }

        /// <summary>
        ///   Convert nInteger into a string.
        ///   * Return value on error: ""
        /// </summary>
        public static string IntToString(int nInteger)
        {
            NWNXPInvoke.StackPushInteger(nInteger);
            NWNXPInvoke.CallBuiltIn(92);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Get the total from rolling (nNumDice x d2 dice).
        ///   - nNumDice: If this is less than 1, the value 1 will be used.
        /// </summary>
        public static int d2(int nNumDice = 1)
        {
            NWNXPInvoke.StackPushInteger(nNumDice);
            NWNXPInvoke.CallBuiltIn(95);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the total from rolling (nNumDice x d3 dice).
        ///   - nNumDice: If this is less than 1, the value 1 will be used.
        /// </summary>
        public static int d3(int nNumDice = 1)
        {
            NWNXPInvoke.StackPushInteger(nNumDice);
            NWNXPInvoke.CallBuiltIn(96);
            return NWNXPInvoke.StackPopInteger();
        }
        /// <summary>
        ///   Get the total from rolling (nNumDice x d4 dice).
        ///   - nNumDice: If this is less than 1, the value 1 will be used.
        /// </summary>
        public static int d4(int nNumDice = 1)
        {
            NWNXPInvoke.StackPushInteger(nNumDice);
            NWNXPInvoke.CallBuiltIn(97);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the total from rolling (nNumDice x d6 dice).
        ///   - nNumDice: If this is less than 1, the value 1 will be used.
        /// </summary>
        public static int d6(int nNumDice = 1)
        {
            NWNXPInvoke.StackPushInteger(nNumDice);
            NWNXPInvoke.CallBuiltIn(98);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the total from rolling (nNumDice x d8 dice).
        ///   - nNumDice: If this is less than 1, the value 1 will be used.
        /// </summary>
        public static int d8(int nNumDice = 1)
        {
            NWNXPInvoke.StackPushInteger(nNumDice);
            NWNXPInvoke.CallBuiltIn(99);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the total from rolling (nNumDice x d10 dice).
        ///   - nNumDice: If this is less than 1, the value 1 will be used.
        /// </summary>
        public static int d10(int nNumDice = 1)
        {
            NWNXPInvoke.StackPushInteger(nNumDice);
            NWNXPInvoke.CallBuiltIn(100);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the total from rolling (nNumDice x d12 dice).
        ///   - nNumDice: If this is less than 1, the value 1 will be used.
        /// </summary>
        public static int d12(int nNumDice = 1)
        {
            NWNXPInvoke.StackPushInteger(nNumDice);
            NWNXPInvoke.CallBuiltIn(101);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the total from rolling (nNumDice x d20 dice).
        ///   - nNumDice: If this is less than 1, the value 1 will be used.
        /// </summary>
        public static int d20(int nNumDice = 1)
        {
            NWNXPInvoke.StackPushInteger(nNumDice);
            NWNXPInvoke.CallBuiltIn(102);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the total from rolling (nNumDice x d100 dice).
        ///   - nNumDice: If this is less than 1, the value 1 will be used.
        /// </summary>
        public static int d100(int nNumDice = 1)
        {
            NWNXPInvoke.StackPushInteger(nNumDice);
            NWNXPInvoke.CallBuiltIn(103);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the metamagic type (METAMAGIC_*) of the last spell cast by the caller
        ///   * Return value if the caster is not a valid object: -1
        /// </summary>
        public static int GetMetaMagicFeat()
        {
            NWNXPInvoke.CallBuiltIn(105);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Do a Fortitude Save check for the given DC
        ///   - oCreature
        ///   - nDC: Difficulty check
        ///   - nSaveType: SAVING_THROW_TYPE_*
        ///   - oSaveVersus
        ///   Returns: 0 if the saving throw roll failed
        ///   Returns: 1 if the saving throw roll succeeded
        ///   Returns: 2 if the target was immune to the save type specified
        ///   Note: If used within an Area of Effect Object Script (On Enter, OnExit, OnHeartbeat), you MUST pass
        ///   GetAreaOfEffectCreator() into oSaveVersus!!
        /// </summary>
        public static SavingThrowResultType FortitudeSave(uint oCreature, int nDC, SavingThrowType nSaveType = SavingThrowType.All,
            uint oSaveVersus = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oSaveVersus);
            NWNXPInvoke.StackPushInteger((int)nSaveType);
            NWNXPInvoke.StackPushInteger(nDC);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(108);
            return (SavingThrowResultType)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Does a Reflex Save check for the given DC
        ///   - oCreature
        ///   - nDC: Difficulty check
        ///   - nSaveType: SAVING_THROW_TYPE_*
        ///   - oSaveVersus
        ///   Returns: 0 if the saving throw roll failed
        ///   Returns: 1 if the saving throw roll succeeded
        ///   Returns: 2 if the target was immune to the save type specified
        ///   Note: If used within an Area of Effect Object Script (On Enter, OnExit, OnHeartbeat), you MUST pass
        ///   GetAreaOfEffectCreator() into oSaveVersus!!
        /// </summary>
        public static SavingThrowResultType ReflexSave(uint oCreature, int nDC, SavingThrowType nSaveType = SavingThrowType.All,
            uint oSaveVersus = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oSaveVersus);
            NWNXPInvoke.StackPushInteger((int)nSaveType);
            NWNXPInvoke.StackPushInteger(nDC);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(109);
            return (SavingThrowResultType)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Does a Will Save check for the given DC
        ///   - oCreature
        ///   - nDC: Difficulty check
        ///   - nSaveType: SAVING_THROW_TYPE_*
        ///   - oSaveVersus
        ///   Returns: 0 if the saving throw roll failed
        ///   Returns: 1 if the saving throw roll succeeded
        ///   Returns: 2 if the target was immune to the save type specified
        ///   Note: If used within an Area of Effect Object Script (On Enter, OnExit, OnHeartbeat), you MUST pass
        ///   GetAreaOfEffectCreator() into oSaveVersus!!
        /// </summary>
        public static SavingThrowResultType WillSave(uint oCreature, int nDC, SavingThrowType nSaveType = SavingThrowType.All,
            uint oSaveVersus = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oSaveVersus);
            NWNXPInvoke.StackPushInteger((int)nSaveType);
            NWNXPInvoke.StackPushInteger(nDC);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(110);
            return (SavingThrowResultType)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the DC to save against for a spell (10 + spell level + relevant ability
        ///   bonus).  This can be called by a creature or by an Area of Effect object.
        /// </summary>
        public static int GetSpellSaveDC()
        {
            NWNXPInvoke.CallBuiltIn(111);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the first object in nShape
        ///   - nShape: SHAPE_*
        ///   - fSize:
        ///   -> If nShape == SHAPE_SPHERE, this is the radius of the sphere
        ///   -> If nShape == SHAPE_SPELLCYLINDER, this is the length of the cylinder
        ///   Spell Cylinder's always have a radius of 1.5m.
        ///   -> If nShape == SHAPE_CONE, this is the widest radius of the cone
        ///   -> If nShape == SHAPE_SPELLCONE, this is the length of the cone in the
        ///   direction of lTarget. Spell cones are always 60 degrees with the origin
        ///   at OBJECT_SELF.
        ///   -> If nShape == SHAPE_CUBE, this is half the length of one of the sides of
        ///   the cube
        ///   - lTarget: This is the centre of the effect, usually GetSpellTargetLocation(),
        ///   or the end of a cylinder or cone.
        ///   - bLineOfSight: This controls whether to do a line-of-sight check on the
        ///   object returned. Line of sight check is done from origin to target object
        ///   at a height 1m above the ground
        ///   (This can be used to ensure that spell effects do not go through walls.)
        ///   - nObjectFilter: This allows you to filter out undesired object types, using
        ///   bitwise "or".
        ///   For example, to return only creatures and doors, the value for this
        ///   parameter would be OBJECT_TYPE_CREATURE | OBJECT_TYPE_DOOR
        ///   - vOrigin: This is only used for cylinders and cones, and specifies the
        ///   origin of the effect(normally the spell-caster's position).
        ///   Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetFirstObjectInShape(Shape nShape, float fSize, Location lTarget, bool bLineOfSight = false,
            ObjectType nObjectFilter = ObjectType.Creature, Vector3 vOrigin = default)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lTarget);
            NWNXPInvoke.StackPushFloat(fSize);
            NWNXPInvoke.StackPushInteger((int)nShape);
            NWNXPInvoke.StackPushInteger(bLineOfSight ? 1 : 0);
            NWNXPInvoke.StackPushInteger((int)nObjectFilter);
            NWNXPInvoke.StackPushVector(vOrigin);
            NWNXPInvoke.CallBuiltIn(128);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the next object in nShape
        ///   - nShape: SHAPE_*
        ///   - fSize:
        ///   -> If nShape == SHAPE_SPHERE, this is the radius of the sphere
        ///   -> If nShape == SHAPE_SPELLCYLINDER, this is the length of the cylinder.
        ///   Spell Cylinder's always have a radius of 1.5m.
        ///   -> If nShape == SHAPE_CONE, this is the widest radius of the cone
        ///   -> If nShape == SHAPE_SPELLCONE, this is the length of the cone in the
        ///   direction of lTarget. Spell cones are always 60 degrees with the origin
        ///   at OBJECT_SELF.
        ///   -> If nShape == SHAPE_CUBE, this is half the length of one of the sides of
        ///   the cube
        ///   - lTarget: This is the centre of the effect, usually GetSpellTargetLocation(),
        ///   or the end of a cylinder or cone.
        ///   - bLineOfSight: This controls whether to do a line-of-sight check on the
        ///   object returned. (This can be used to ensure that spell effects do not go
        ///   through walls.) Line of sight check is done from origin to target object
        ///   at a height 1m above the ground
        ///   - nObjectFilter: This allows you to filter out undesired object types, using
        ///   bitwise "or". For example, to return only creatures and doors, the value for
        ///   this parameter would be OBJECT_TYPE_CREATURE | OBJECT_TYPE_DOOR
        ///   - vOrigin: This is only used for cylinders and cones, and specifies the origin
        ///   of the effect (normally the spell-caster's position).
        ///   Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetNextObjectInShape(Shape nShape, float fSize, Location lTarget, bool bLineOfSight = false,
            ObjectType nObjectFilter = ObjectType.Creature, Vector3 vOrigin = default)
        {
            NWNXPInvoke.StackPushVector(vOrigin);
            NWNXPInvoke.StackPushInteger((int)nObjectFilter);
            NWNXPInvoke.StackPushInteger(bLineOfSight ? 1 : 0);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lTarget);
            NWNXPInvoke.StackPushFloat(fSize);
            NWNXPInvoke.StackPushInteger((int)nShape);
            NWNXPInvoke.CallBuiltIn(129);
            return NWNXPInvoke.StackPopObject();
        }


        /// <summary>
        ///   Causes object oObject to run the event evToRun. The script on the object that is
        ///   associated with the event specified will run.
        ///   Events can be created using the following event functions:
        ///   EventActivateItem() - This creates an OnActivateItem module event. The script for handling
        ///   this event can be set in Module Properties on the Event Tab.
        ///   EventConversation() - This creates on OnConversation creature event. The script for handling
        ///   this event can be set by viewing the Creature Properties on a
        ///   creature and then clicking on the Scripts Tab.
        ///   EventSpellCastAt()  - This creates an OnSpellCastAt event. The script for handling this
        ///   event can be set in the Scripts Tab of the Properties menu
        ///   for the object.
        ///   EventUserDefined()  - This creates on OnUserDefined event. The script for handling this event
        ///   can be set in the Scripts Tab of the Properties menu for the object/area/module.
        /// </summary>
        public static void SignalEvent(uint oObject, Event evToRun)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Event, evToRun);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(131);
        }

        /// <summary>
        ///   Create an event of the type nUserDefinedEventNumber
        ///   Note: This only creates the event. The event wont actually trigger until SignalEvent()
        ///   is called using this created UserDefined event as an argument.
        ///   For example:
        ///   SignalEvent(oObject, EventUserDefined(9999));
        ///   Once the event has been signaled. The script associated with the OnUserDefined event will
        ///   run on the object oObject.
        ///   To specify the OnUserDefined script that should run, view the object's Properties
        ///   and click on the Scripts Tab. Then specify a script for the OnUserDefined event.
        ///   From inside the OnUserDefined script call:
        ///   GetUserDefinedEventNumber() to retrieve the value of nUserDefinedEventNumber
        ///   that was used when the event was signaled.
        /// </summary>
        public static Event EventUserDefined(int nUserDefinedEventNumber)
        {
            NWNXPInvoke.StackPushInteger(nUserDefinedEventNumber);
            NWNXPInvoke.CallBuiltIn(132);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Event);
        }

        /// <summary>
        ///   Get the ability score of type nAbility for a creature (otherwise 0)
        ///   - oCreature: the creature whose ability score we wish to find out
        ///   - nAbilityType: ABILITY_*
        ///   - nBaseAbilityScore: if set to true will return the base ability score without
        ///   bonuses (e.g. ability bonuses granted from equipped items).
        ///   Return value on error: 0
        /// </summary>
        public static int GetAbilityScore(uint oCreature, AbilityType nAbilityType, bool nBaseAbilityScore = false)
        {
            NWNXPInvoke.StackPushInteger(nBaseAbilityScore ? 1 : 0);
            NWNXPInvoke.StackPushInteger((int)nAbilityType);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(139);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   * Returns TRUE if oCreature is a dead NPC, dead PC or a dying PC.
        /// </summary>
        public static bool GetIsDead(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(140);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Output vVector to the logfile.
        ///   - vVector
        ///   - bPrepend: if this is TRUE, the message will be prefixed with "PRINTVECTOR:"
        /// </summary>
        public static void PrintVector(Vector3 vVector, bool bPrepend = false)
        {
            NWNXPInvoke.StackPushInteger(bPrepend ? 1 : 0);
            NWNXPInvoke.StackPushVector(vVector);
            NWNXPInvoke.CallBuiltIn(141);
        }

        /// <summary>
        ///   Create a vector with the specified values for x, y and z
        /// </summary>
        public static Vector3 Vector3(float x = 0.0f, float y = 0.0f, float z = 0.0f)
        {
            NWNXPInvoke.StackPushFloat(z);
            NWNXPInvoke.StackPushFloat(y);
            NWNXPInvoke.StackPushFloat(x);
            NWNXPInvoke.CallBuiltIn(142);
            return NWNXPInvoke.StackPopVector();
        }

        /// <summary>
        ///   Cause the caller to face vTarget
        /// </summary>
        public static void SetFacingPoint(Vector3 vTarget)
        {
            NWNXPInvoke.StackPushVector(vTarget);
            NWNXPInvoke.CallBuiltIn(143);
        }

        /// <summary>
        ///   The caller will perform a Melee Touch Attack on oTarget
        ///   This is not an action, and it assumes the caller is already within range of
        ///   oTarget
        ///   * Returns 0 on a miss, 1 on a hit and 2 on a critical hit
        /// </summary>
        public static TouchAttackReturn TouchAttackMelee(uint oTarget, bool bDisplayFeedback = true)
        {
            NWNXPInvoke.StackPushInteger(bDisplayFeedback ? 1 : 0);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(146);
            return (TouchAttackReturn)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   The caller will perform a Ranged Touch Attack on oTarget
        ///   * Returns 0 on a miss, 1 on a hit and 2 on a critical hit
        /// </summary>
        public static TouchAttackReturn TouchAttackRanged(uint oTarget, bool bDisplayFeedback = true)
        {
            NWNXPInvoke.StackPushInteger(bDisplayFeedback ? 1 : 0);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(147);
            return (TouchAttackReturn)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the distance in metres between oObjectA and oObjectB.
        ///   * Return value if either object is invalid: 0.0f
        /// </summary>
        public static float GetDistanceBetween(uint oObjectA, uint oObjectB)
        {
            NWNXPInvoke.StackPushObject(oObjectB);
            NWNXPInvoke.StackPushObject(oObjectA);
            NWNXPInvoke.CallBuiltIn(151);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        ///   Set oObject's local location variable sVarname to lValue
        /// </summary>
        public static void SetLocalLocation(uint oObject, string sVarName, Location lValue)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lValue);
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(152);
        }

        /// <summary>
        ///   Get oObject's local location variable sVarname
        /// </summary>
        public static Location GetLocalLocation(uint oObject, string sVarName)
        {
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(153);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Location);
        }

        /// <summary>
        ///   Set whether oTarget's action stack can be modified
        /// </summary>
        public static void SetCommandable(bool nCommandable, uint oTarget = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushInteger(nCommandable ? 1 : 0);
            NWNXPInvoke.CallBuiltIn(162);
        }

        /// <summary>
        ///   Determine whether oTarget's action stack can be modified.
        /// </summary>
        public static bool GetCommandable(uint oTarget = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(163);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Get the number of hitdice for oCreature.
        ///   * Return value if oCreature is not a valid creature: 0
        /// </summary>
        public static int GetHitDice(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(166);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   The action subject will follow oFollow until a ClearAllActions() is called.
        ///   - oFollow: this is the object to be followed
        ///   - fFollowDistance: follow distance in metres
        ///   * No return value
        /// </summary>
        public static void ActionForceFollowObject(uint oFollow, float fFollowDistance = 0.0f)
        {
            NWNXPInvoke.StackPushFloat(fFollowDistance);
            NWNXPInvoke.StackPushObject(oFollow);
            NWNXPInvoke.CallBuiltIn(167);
        }

        /// <summary>
        ///   Get the Tag of oObject
        ///   * Return value if oObject is not a valid object: ""
        /// </summary>
        public static string GetTag(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(168);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Do a Spell Resistance check between oCaster and oTarget, returning TRUE if
        ///   the spell was resisted.
        ///   * Return value if oCaster or oTarget is an invalid object: FALSE
        ///   * Return value if spell cast is not a player spell: - 1
        ///   * Return value if spell resisted: 1
        ///   * Return value if spell resisted via magic immunity: 2
        ///   * Return value if spell resisted via spell absorption: 3
        /// </summary>
        public static int ResistSpell(uint oCaster, uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushObject(oCaster);
            NWNXPInvoke.CallBuiltIn(169);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   * Returns TRUE if oObject is listening for something
        /// </summary>
        public static bool GetIsListening(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(174);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Set whether oObject is listening.
        /// </summary>
        public static void SetListening(uint oObject, bool bValue)
        {
            NWNXPInvoke.StackPushInteger(bValue ? 1 : 0);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(175);
        }

        /// <summary>
        ///   Set the string for oObject to listen for.
        ///   Note: this does not set oObject to be listening.
        /// </summary>
        public static void SetListenPattern(uint oObject, string sPattern, int nNumber = 0)
        {
            NWNXPInvoke.StackPushInteger(nNumber);
            NWNXPInvoke.StackPushString(sPattern);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(176);
        }

        /// <summary>
        ///   Sit in oChair.
        ///   Note: Not all creatures will be able to sit and not all
        ///   objects can be sat on.
        ///   The object oChair must also be marked as usable in the toolset.
        ///   For Example: To get a player to sit in oChair when they click on it,
        ///   place the following script in the OnUsed event for the object oChair.
        ///   void main()
        ///   {
        ///   object oChair = OBJECT_SELF;
        ///   AssignCommand(GetLastUsedBy(),ActionSit(oChair));
        ///   }
        /// </summary>
        public static void ActionSit(uint oChair)
        {
            NWNXPInvoke.StackPushObject(oChair);
            NWNXPInvoke.CallBuiltIn(194);
        }

        /// <summary>
        ///   In an onConversation script this gets the number of the string pattern
        ///   matched (the one that triggered the script).
        ///   * Returns -1 if no string matched
        /// </summary>
        public static int GetListenPatternNumber()
        {
            NWNXPInvoke.CallBuiltIn(195);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Jump to an object ID, or as near to it as possible.
        /// </summary>
        public static void ActionJumpToObject(uint oToJumpTo, bool bWalkStraightLineToPoint = true)
        {
            NWNXPInvoke.StackPushInteger(bWalkStraightLineToPoint ? 1 : 0);
            NWNXPInvoke.StackPushObject(oToJumpTo);
            NWNXPInvoke.CallBuiltIn(196);
        }

        /// <summary>
        ///   Get the first waypoint with the specified tag.
        ///   * Returns OBJECT_INVALID if the waypoint cannot be found.
        /// </summary>
        public static uint GetWaypointByTag(string sWaypointTag)
        {
            NWNXPInvoke.StackPushString(sWaypointTag);
            NWNXPInvoke.CallBuiltIn(197);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the destination object for the given object.
        ///   All objects can hold a transition target, but only Doors and Triggers
        ///   will be made clickable by the game engine (This may change in the
        ///   future). You can set and query transition targets on other objects for
        ///   your own scripted purposes.
        ///   * Returns OBJECT_INVALID if oTransition does not hold a target.
        /// </summary>
        public static uint GetTransitionTarget(uint oTransition)
        {
            NWNXPInvoke.StackPushObject(oTransition);
            NWNXPInvoke.CallBuiltIn(198);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the nNth object with the specified tag.
        ///   - sTag
        ///   - nNth: the nth object with this tag may be requested
        ///   * Returns OBJECT_INVALID if the object cannot be found.
        ///   Note: The module cannot be retrieved by GetObjectByTag(), use GetModule() instead.
        /// </summary>
        public static uint GetObjectByTag(string sTag, int nNth = 0)
        {
            NWNXPInvoke.StackPushInteger(nNth);
            NWNXPInvoke.StackPushString(sTag);
            NWNXPInvoke.CallBuiltIn(200);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Do nothing for fSeconds seconds.
        /// </summary>
        public static void ActionWait(float fSeconds)
        {
            NWNXPInvoke.StackPushFloat(fSeconds);
            NWNXPInvoke.CallBuiltIn(202);
        }

        /// <summary>
        ///   Starts a conversation with oObjectToConverseWith - this will cause their
        ///   OnDialog event to fire.
        ///   - oObjectToConverseWith
        ///   - sDialogResRef: If this is blank, the creature's own dialogue file will be used
        ///   - bPrivateConversation
        ///   Turn off bPlayHello if you don't want the initial greeting to play
        /// </summary>
        public static void ActionStartConversation(uint oObjectToConverseWith, string sDialogResRef = "",
            bool bPrivateConversation = true, bool bPlayHello = true)
        {
            NWNXPInvoke.StackPushInteger(bPlayHello ? 1 : 0);
            NWNXPInvoke.StackPushInteger(bPrivateConversation ? 1 : 0);
            NWNXPInvoke.StackPushString(sDialogResRef);
            NWNXPInvoke.StackPushObject(oObjectToConverseWith);
            NWNXPInvoke.CallBuiltIn(204);
        }

        /// <summary>
        ///   Pause the current conversation.
        /// </summary>
        public static void ActionPauseConversation()
        {
            NWNXPInvoke.CallBuiltIn(205);
        }

        /// <summary>
        ///   Resume a conversation after it has been paused.
        /// </summary>
        public static void ActionResumeConversation()
        {
            NWNXPInvoke.CallBuiltIn(206);
        }
        /// <summary>
        ///   Get the creature that is currently sitting on the specified object.
        ///   - oChair
        ///   * Returns OBJECT_INVALID if oChair is not a valid placeable.
        /// </summary>
        public static uint GetSittingCreature(uint oChair)
        {
            NWNXPInvoke.StackPushObject(oChair);
            NWNXPInvoke.CallBuiltIn(210);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the creature that is going to attack oTarget.
        ///   Note: This value is cleared out at the end of every combat round and should
        ///   not be used in any case except when getting a "going to be attacked" shout
        ///   from the master creature (and this creature is a henchman)
        ///   * Returns OBJECT_INVALID if oTarget is not a valid creature.
        /// </summary>
        public static uint GetGoingToBeAttackedBy(uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(211);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   * Returns TRUE if oCreature is a Player Controlled character.
        /// </summary>
        public static bool GetIsPC(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(217);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   The caller will immediately speak sStringToSpeak (this is different from
        ///   ActionSpeakString)
        ///   - sStringToSpeak
        ///   - nTalkVolume: TALKVOLUME_*
        /// </summary>
        public static void SpeakString(string sStringToSpeak, TalkVolume nTalkVolume = TalkVolume.Talk)
        {
            NWNXPInvoke.StackPushInteger((int)nTalkVolume);
            NWNXPInvoke.StackPushString(sStringToSpeak);
            NWNXPInvoke.CallBuiltIn(221);
        }

        /// <summary>
        ///   Get the location of the caller's last spell target.
        /// </summary>
        public static Location GetSpellTargetLocation()
        {
            NWNXPInvoke.CallBuiltIn(222);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Location);
        }

        /// <summary>
        ///   Get the orientation value from lLocation.
        /// </summary>
        public static float GetFacingFromLocation(Location lLocation)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lLocation);
            NWNXPInvoke.CallBuiltIn(225);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        ///   Convert nInteger into a floating point number.
        /// </summary>
        public static float IntToFloat(int nInteger)
        {
            NWNXPInvoke.StackPushInteger(nInteger);
            NWNXPInvoke.CallBuiltIn(230);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        ///   Convert fFloat into the nearest integer.
        /// </summary>
        public static int FloatToInt(float fFloat)
        {
            NWNXPInvoke.StackPushFloat(fFloat);
            NWNXPInvoke.CallBuiltIn(231);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Convert sNumber into an integer.
        /// </summary>
        public static int StringToInt(string sNumber)
        {
            NWNXPInvoke.StackPushString(sNumber);
            NWNXPInvoke.CallBuiltIn(232);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Convert sNumber into a floating point number.
        /// </summary>
        public static float StringToFloat(string sNumber)
        {
            NWNXPInvoke.StackPushString(sNumber);
            NWNXPInvoke.CallBuiltIn(233);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        ///   Cast spell nSpell at lTargetLocation.
        ///   - nSpell: SPELL_*
        ///   - lTargetLocation
        /// - nMetaMagic: METAMAGIC_*. If nClass is specified, cannot be METAMAGIC_ANY.
        /// - bCheat: If this is TRUE, then the executor of the action doesn't have to be
        ///   able to cast the spell. Ignored if nClass is specified.
        /// - bCheat: If this is TRUE, then the executor of the action doesn't have to be
        ///   able to cast the spell.
        /// - nProjectilePathType: PROJECTILE_PATH_TYPE_*
        /// - bInstantSpell: If this is TRUE, the spell is cast immediately; this allows
        ///   the end-user to simulate
        ///   a high-level magic user having lots of advance warning of impending trouble.
        /// - nClass: If set to a CLASS_TYPE_* it will cast using that class specifically.
        ///   CLASS_TYPE_INVALID will use spell abilities.
        /// - bSpontaneousCast: If set to TRUE will attempt to cast the given spell spontaneously,
        ///   ie a Cleric casting Cure Light Wounds using any level 1 slot. Needs a valid nClass set.
        /// - nDomainLevel: The level of the spell if cast from a domain slot.
        ///   eg SPELL_HEAL can be spell level 5 on a cleric. Use 0 for no domain slot.
        /// </summary>
        public static void ActionCastSpellAtLocation(
            Spell nSpell,
            Location lTargetLocation,
            MetaMagic nMetaMagic = MetaMagic.Any,
            bool bCheat = false,
            ProjectilePathType nProjectilePathType = ProjectilePathType.Default,
            bool bInstantSpell = false,
            int nClass = -1,
            bool bSpontaneousCast = false,
            int nDomainLevel = 0)
        {
            NWNXPInvoke.StackPushInteger(nDomainLevel);
            NWNXPInvoke.StackPushInteger(bSpontaneousCast ? 1 : 0);
            NWNXPInvoke.StackPushInteger(nClass);
            NWNXPInvoke.StackPushInteger(bInstantSpell ? 1 : 0);
            NWNXPInvoke.StackPushInteger((int)nProjectilePathType);
            NWNXPInvoke.StackPushInteger(bCheat ? 1 : 0);
            NWNXPInvoke.StackPushInteger((int)nMetaMagic);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lTargetLocation);
            NWNXPInvoke.StackPushInteger((int)nSpell);
            NWNXPInvoke.CallBuiltIn(234);
        }

        /// <summary>
        ///   Get the PC that is involved in the conversation.
        ///   * Returns OBJECT_INVALID on error.
        /// </summary>
        public static uint GetPCSpeaker()
        {
            NWNXPInvoke.CallBuiltIn(238);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get a string from the talk table using nStrRef.
        /// </summary>
        public static string GetStringByStrRef(int nStrRef, Gender nGender = Gender.Male)
        {
            NWNXPInvoke.StackPushInteger((int)nGender);
            NWNXPInvoke.StackPushInteger(nStrRef);
            NWNXPInvoke.CallBuiltIn(239);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Causes the creature to speak a translated string.
        ///   - nStrRef: Reference of the string in the talk table
        ///   - nTalkVolume: TALKVOLUME_*
        /// </summary>
        public static void ActionSpeakStringByStrRef(int nStrRef, TalkVolume nTalkVolume = TalkVolume.Talk)
        {
            NWNXPInvoke.StackPushInteger((int)nTalkVolume);
            NWNXPInvoke.StackPushInteger(nStrRef);
            NWNXPInvoke.CallBuiltIn(240);
        }

        /// <summary>
        ///   Get the module.
        ///   * Return value on error: OBJECT_INVALID
        /// </summary>
        public static uint GetModule()
        {
            NWNXPInvoke.CallBuiltIn(242);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Create an event which triggers the "SpellCastAt" script
        ///   Note: This only creates the event. The event wont actually trigger until SignalEvent()
        ///   is called using this created SpellCastAt event as an argument.
        ///   For example:
        ///   SignalEvent(oCreature, EventSpellCastAt(oCaster, SPELL_MAGIC_MISSILE, TRUE));
        ///   This function doesn't cast the spell specified, it only creates an event so that
        ///   when the event is signaled on an object, the object will use its OnSpellCastAt script
        ///   to react to the spell being cast.
        ///   To specify the OnSpellCastAt script that should run, view the Object's Properties
        ///   and click on the Scripts Tab. Then specify a script for the OnSpellCastAt event.
        ///   From inside the OnSpellCastAt script call:
        ///   GetLastSpellCaster() to get the object that cast the spell (oCaster).
        ///   GetLastSpell() to get the type of spell cast (nSpell)
        ///   GetLastSpellHarmful() to determine if the spell cast at the object was harmful.
        /// </summary>
        public static Event EventSpellCastAt(uint oCaster, Spell nSpell, bool bHarmful = true)
        {
            NWNXPInvoke.StackPushInteger(bHarmful ? 1 : 0);
            NWNXPInvoke.StackPushInteger((int)nSpell);
            NWNXPInvoke.StackPushObject(oCaster);
            NWNXPInvoke.CallBuiltIn(244);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Event);
        }
        /// <summary>
        ///   This is for use in a "Spell Cast" script, it gets who cast the spell.
        ///   The spell could have been cast by a creature, placeable or door.
        ///   * Returns OBJECT_INVALID if the caller is not a creature, placeable or door.
        /// </summary>
        public static uint GetLastSpellCaster()
        {
            NWNXPInvoke.CallBuiltIn(245);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   This is for use in a "Spell Cast" script, it gets the ID of the spell that
        ///   was cast.
        /// </summary>
        public static int GetLastSpell()
        {
            NWNXPInvoke.CallBuiltIn(246);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   This is for use in a user-defined script, it gets the event number.
        /// </summary>
        public static int GetUserDefinedEventNumber()
        {
            NWNXPInvoke.CallBuiltIn(247);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// This is for use in a Spell script, it gets the ID of the spell that is being cast.
        /// If used in an Area of Effect script it will return the ID of the spell that generated the AOE effect.
        /// * Returns the spell ID (SPELL_*) or -1 if no spell was cast or on error
        /// </summary>
        public static int GetSpellId()
        {
            NWNXPInvoke.CallBuiltIn(248);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Generate a random name.
        ///   nNameType: The type of random name to be generated (NAME_*).
        /// </summary>
        public static string RandomName(Name nNameType = Name.FirstGenericMale)
        {
            NWNXPInvoke.StackPushInteger((int)nNameType);
            NWNXPInvoke.CallBuiltIn(249);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Set the name of oObject.
        /// - bOriginalName:  if set to true any new name specified via a SetName scripting command
        ///                   is ignored and the original object's name is returned instead.
        /// </summary>
        public static string GetName(uint oObject, bool bOriginalName = false)
        {
            NWNXPInvoke.StackPushInteger(bOriginalName ? 1 : 0);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(253);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Use this in a conversation script to get the person with whom you are conversing.
        ///   * Returns OBJECT_INVALID if the caller is not a valid creature.
        /// </summary>
        public static uint GetLastSpeaker()
        {
            NWNXPInvoke.CallBuiltIn(254);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Use this in an OnDialog script to start up the dialog tree.
        ///   - sResRef: if this is not specified, the default dialog file will be used
        ///   - oObjectToDialog: if this is not specified the person that triggered the
        ///   event will be used
        /// </summary>
        public static int BeginConversation(string sResRef = "", uint oObjectToDialog = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oObjectToDialog);
            NWNXPInvoke.StackPushString(sResRef);
            NWNXPInvoke.CallBuiltIn(255);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Use this in an OnPerception script to get the object that was perceived.
        ///   * Returns OBJECT_INVALID if the caller is not a valid creature.
        /// </summary>
        public static uint GetLastPerceived()
        {
            NWNXPInvoke.CallBuiltIn(256);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Use this in an OnPerception script to determine whether the object that was
        ///   perceived was heard.
        /// </summary>
        public static bool GetLastPerceptionHeard()
        {
            NWNXPInvoke.CallBuiltIn(257);
            return Convert.ToBoolean(NWNXPInvoke.StackPopInteger());
        }

        /// <summary>
        ///   Use this in an OnPerception script to determine whether the object that was
        ///   perceived has become inaudible.
        /// </summary>
        public static bool GetLastPerceptionInaudible()
        {
            NWNXPInvoke.CallBuiltIn(258);
            return Convert.ToBoolean(NWNXPInvoke.StackPopInteger());
        }

        /// <summary>
        ///   Use this in an OnPerception script to determine whether the object that was
        ///   perceived was seen.
        /// </summary>
        public static bool GetLastPerceptionSeen()
        {
            NWNXPInvoke.CallBuiltIn(259);
            return Convert.ToBoolean(NWNXPInvoke.StackPopInteger());
        }

        /// <summary>
        ///   Use this in an OnClosed script to get the object that closed the door or placeable.
        ///   * Returns OBJECT_INVALID if the caller is not a valid door or placeable.
        /// </summary>
        public static uint GetLastClosedBy()
        {
            NWNXPInvoke.CallBuiltIn(260);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Use this in an OnPerception script to determine whether the object that was
        ///   perceived has vanished.
        /// </summary>
        public static bool GetLastPerceptionVanished()
        {
            NWNXPInvoke.CallBuiltIn(261);
            return Convert.ToBoolean(NWNXPInvoke.StackPopInteger());
        }

        /// <summary>
        ///   Get the first object within oPersistentObject.
        ///   - oPersistentObject
        ///   - nResidentObjectType: OBJECT_TYPE_*
        ///   - nPersistentZone: PERSISTENT_ZONE_ACTIVE. [This could also take the value
        ///   PERSISTENT_ZONE_FOLLOW, but this is no longer used.]
        ///   * Returns OBJECT_INVALID if no object is found.
        /// </summary>
        public static uint GetFirstInPersistentObject(uint oPersistentObject = OBJECT_INVALID,
            ObjectType nResidentObjectType = ObjectType.Creature,
            PersistentZone nPersistentZone = PersistentZone.Active)
        {
            NWNXPInvoke.StackPushInteger((int)nPersistentZone);
            NWNXPInvoke.StackPushInteger((int)nResidentObjectType);
            NWNXPInvoke.StackPushObject(oPersistentObject);
            NWNXPInvoke.CallBuiltIn(262);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the next object within oPersistentObject.
        ///   - oPersistentObject
        ///   - nResidentObjectType: OBJECT_TYPE_*
        ///   - nPersistentZone: PERSISTENT_ZONE_ACTIVE. [This could also take the value
        ///   PERSISTENT_ZONE_FOLLOW, but this is no longer used.]
        ///   * Returns OBJECT_INVALID if no object is found.
        /// </summary>
        public static uint GetNextInPersistentObject(uint oPersistentObject = OBJECT_INVALID,
            ObjectType nResidentObjectType = ObjectType.Creature,
            PersistentZone nPersistentZone = PersistentZone.Active)
        {
            NWNXPInvoke.StackPushInteger((int)nPersistentZone);
            NWNXPInvoke.StackPushInteger((int)nResidentObjectType);
            NWNXPInvoke.StackPushObject(oPersistentObject);
            NWNXPInvoke.CallBuiltIn(263);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   This returns the creator of oAreaOfEffectObject.
        ///   * Returns OBJECT_INVALID if oAreaOfEffectObject is not a valid Area of Effect object.
        /// </summary>
        public static uint GetAreaOfEffectCreator(uint oAreaOfEffectObject = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oAreaOfEffectObject);
            NWNXPInvoke.CallBuiltIn(264);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Delete oObject's local integer variable sVarName
        /// </summary>
        public static void DeleteLocalInt(uint oObject, string sVarName)
        {
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(265);
        }

        /// <summary>
        /// Delete oObject's local boolean variable sVarName
        /// </summary>
        public static void DeleteLocalBool(uint oObject, string sVarName)
        {
            DeleteLocalInt(oObject, sVarName);
        }

        /// <summary>
        ///   Delete oObject's local float variable sVarName
        /// </summary>
        public static void DeleteLocalFloat(uint oObject, string sVarName)
        {
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(266);
        }

        /// <summary>
        ///   Delete oObject's local string variable sVarName
        /// </summary>
        public static void DeleteLocalString(uint oObject, string sVarName)
        {
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(267);
        }

        /// <summary>
        ///   Delete oObject's local object variable sVarName
        /// </summary>
        public static void DeleteLocalObject(uint oObject, string sVarName)
        {
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(268);
        }
        /// <summary>
        ///   Delete oObject's local location variable sVarName
        /// </summary>
        public static void DeleteLocalLocation(uint oObject, string sVarName)
        {
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(269);
        }

        /// <summary>
        ///   Convert oObject into a hexadecimal string.
        /// </summary>
        public static string ObjectToString(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(272);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   - oCreature
        ///   - nImmunityType: IMMUNITY_TYPE_*
        ///   - oVersus: if this is specified, then we also check for the race and
        ///   alignment of oVersus
        ///   * Returns TRUE if oCreature has immunity of type nImmunity versus oVersus.
        /// </summary>
        public static bool GetIsImmune(uint oCreature, ImmunityType nImmunityType, uint oVersus = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oVersus);
            NWNXPInvoke.StackPushInteger((int)nImmunityType);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(274);
            return NWNXPInvoke.StackPopInteger() == 1;
        }

        /// <summary>
        ///   Determine whether oEncounter is active.
        /// </summary>
        public static int GetEncounterActive(uint oEncounter = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oEncounter);
            NWNXPInvoke.CallBuiltIn(276);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Set oEncounter's active state to nNewValue.
        ///   - nNewValue: TRUE/FALSE
        ///   - oEncounter
        /// </summary>
        public static void SetEncounterActive(int nNewValue, uint oEncounter = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oEncounter);
            NWNXPInvoke.StackPushInteger(nNewValue);
            NWNXPInvoke.CallBuiltIn(277);
        }

        /// <summary>
        ///   Get the maximum number of times that oEncounter will spawn.
        /// </summary>
        public static int GetEncounterSpawnsMax(uint oEncounter = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oEncounter);
            NWNXPInvoke.CallBuiltIn(278);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Set the maximum number of times that oEncounter can spawn
        /// </summary>
        public static void SetEncounterSpawnsMax(int nNewValue, uint oEncounter = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oEncounter);
            NWNXPInvoke.StackPushInteger(nNewValue);
            NWNXPInvoke.CallBuiltIn(279);
        }

        /// <summary>
        ///   Get the number of times that oEncounter has spawned so far
        /// </summary>
        public static int GetEncounterSpawnsCurrent(uint oEncounter = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oEncounter);
            NWNXPInvoke.CallBuiltIn(280);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Set the number of times that oEncounter has spawned so far
        /// </summary>
        public static void SetEncounterSpawnsCurrent(int nNewValue, uint oEncounter = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oEncounter);
            NWNXPInvoke.StackPushInteger(nNewValue);
            NWNXPInvoke.CallBuiltIn(281);
        }

        /// <summary>
        ///   Set the value for a custom token.
        /// </summary>
        public static void SetCustomToken(int nCustomTokenNumber, string sTokenValue)
        {
            NWNXPInvoke.StackPushString(sTokenValue);
            NWNXPInvoke.StackPushInteger(nCustomTokenNumber);
            NWNXPInvoke.CallBuiltIn(284);
        }

        /// <summary>
        /// Determine whether oCreature has nFeat, optionally if nFeat is useable.
        /// - nFeat: FEAT_*
        /// - oCreature
        /// - bIgnoreUses: Will check if the creature has the given feat even if it has no uses remaining
        /// </summary>
        public static bool GetHasFeat(FeatType nFeat, uint oCreature, bool bIgnoreUses = false)
        {
            NWNXPInvoke.StackPushInteger(bIgnoreUses ? 1 : 0);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushInteger((int)nFeat);
            NWNXPInvoke.CallBuiltIn(285);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Determine whether oCreature has nSkill, and nSkill is useable.
        ///   - nSkill: SKILL_*
        ///   - oCreature
        /// </summary>
        public static bool GetHasSkill(NWNSkillType nSkill, uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushInteger((int)nSkill);
            NWNXPInvoke.CallBuiltIn(286);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Use nFeat on oTarget.
        ///   - nFeat: FEAT_*
        ///   - oTarget
        /// </summary>
        public static void ActionUseFeat(FeatType nFeat, uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushInteger((int)nFeat);
            NWNXPInvoke.CallBuiltIn(287);
        }

        /// <summary>
        ///   Runs the action "UseSkill" on the current creature
        ///   Use nSkill on oTarget.
        ///   - nSkill: SKILL_*
        ///   - oTarget
        ///   - nSubSkill: SUBSKILL_*
        ///   - oItemUsed: Item to use in conjunction with the skill
        /// </summary>
        public static void ActionUseSkill(NWNSkillType nSkill, uint oTarget, SubSkill nSubSkill = SubSkill.None,
            uint oItemUsed = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oItemUsed);
            NWNXPInvoke.StackPushInteger((int)nSubSkill);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushInteger((int)nSkill);
            NWNXPInvoke.CallBuiltIn(288);
        }

        /// <summary>
        ///   Determine whether oSource sees oTarget.
        ///   NOTE: This *only* works on creatures, as visibility lists are not
        ///   maintained for non-creature objects.
        /// </summary>
        public static bool GetObjectSeen(uint oTarget, uint oSource = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oSource);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(289);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Determine whether oSource hears oTarget.
        ///   NOTE: This *only* works on creatures, as visibility lists are not
        ///   maintained for non-creature objects.
        /// </summary>
        public static bool GetObjectHeard(uint oTarget, uint oSource = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oSource);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(290);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Use this in an OnPlayerDeath module script to get the last player that died.
        /// </summary>
        public static uint GetLastPlayerDied()
        {
            NWNXPInvoke.CallBuiltIn(291);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Use this in an OnItemLost script to get the item that was lost/dropped.
        ///   * Returns OBJECT_INVALID if the module is not valid.
        /// </summary>
        public static uint GetModuleItemLost()
        {
            NWNXPInvoke.CallBuiltIn(292);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Use this in an OnItemLost script to get the creature that lost the item.
        ///   * Returns OBJECT_INVALID if the module is not valid.
        /// </summary>
        public static uint GetModuleItemLostBy()
        {
            NWNXPInvoke.CallBuiltIn(293);
            return NWNXPInvoke.StackPopObject();
        }
        /// <summary>
        ///   Set the difficulty level of oEncounter.
        ///   - nEncounterDifficulty: ENCOUNTER_DIFFICULTY_*
        ///   - oEncounter
        /// </summary>
        public static void SetEncounterDifficulty(EncounterDifficulty nEncounterDifficulty,
            uint oEncounter = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oEncounter);
            NWNXPInvoke.StackPushInteger((int)nEncounterDifficulty);
            NWNXPInvoke.CallBuiltIn(296);
        }

        /// <summary>
        ///   Get the difficulty level of oEncounter.
        /// </summary>
        public static int GetEncounterDifficulty(uint oEncounter = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oEncounter);
            NWNXPInvoke.CallBuiltIn(297);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the distance between lLocationA and lLocationB.
        /// </summary>
        public static float GetDistanceBetweenLocations(Location lLocationA, Location lLocationB)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lLocationB);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lLocationA);
            NWNXPInvoke.CallBuiltIn(298);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        ///   Use this in spell scripts to get nDamage adjusted by oTarget's reflex and
        ///   evasion saves.
        ///   - nDamage
        ///   - oTarget
        ///   - nDC: Difficulty check
        ///   - nSaveType: SAVING_THROW_TYPE_*
        ///   - oSaveVersus
        /// </summary>
        public static int GetReflexAdjustedDamage(int nDamage, uint oTarget, int nDC,
            SavingThrowType nSaveType = SavingThrowType.All, uint oSaveVersus = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oSaveVersus);
            NWNXPInvoke.StackPushInteger((int)nSaveType);
            NWNXPInvoke.StackPushInteger(nDC);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushInteger(nDamage);
            NWNXPInvoke.CallBuiltIn(299);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Play nAnimation immediately.
        ///   - nAnimation: ANIMATION_*
        ///   - fSpeed
        ///   - fSeconds
        /// </summary>
        public static void PlayAnimation(Animation nAnimation, float fSpeed = 1.0f, float fSeconds = 0.0f)
        {
            NWNXPInvoke.StackPushFloat(fSeconds);
            NWNXPInvoke.StackPushFloat(fSpeed);
            NWNXPInvoke.StackPushInteger((int)nAnimation);
            NWNXPInvoke.CallBuiltIn(300);
        }

        /// <summary>
        ///   Create a Spell Talent.
        ///   - nSpell: SPELL_*
        /// </summary>
        public static Talent TalentSpell(Spell nSpell)
        {
            NWNXPInvoke.StackPushInteger((int)nSpell);
            NWNXPInvoke.CallBuiltIn(301);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Talent);
        }

        /// <summary>
        ///   Create a Feat Talent.
        ///   - nFeat: FEAT_*
        /// </summary>
        public static Talent TalentFeat(FeatType nFeat)
        {
            NWNXPInvoke.StackPushInteger((int)nFeat);
            NWNXPInvoke.CallBuiltIn(302);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Talent);
        }

        /// <summary>
        ///   Create a Skill Talent.
        ///   - nSkill: SKILL_*
        /// </summary>
        public static Talent TalentSkill(NWNSkillType nSkill)
        {
            NWNXPInvoke.StackPushInteger((int)nSkill);
            NWNXPInvoke.CallBuiltIn(303);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Talent);
        }

        /// <summary>
        ///   Determines whether oObject has any effects applied by nSpell
        ///   - nSpell: SPELL_*
        /// </summary>
        public static bool GetHasSpellEffect(Spell nSpell, uint oObject = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.StackPushInteger((int)nSpell);
            NWNXPInvoke.CallBuiltIn(304);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Get the spell (SPELL_*) that applied eSpellEffect.
        ///   * Returns -1 if eSpellEffect was applied outside a spell script.
        /// </summary>
        public static int GetEffectSpellId(Effect eSpellEffect)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Effect, eSpellEffect);
            NWNXPInvoke.CallBuiltIn(305);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Determine whether oCreature has tTalent.
        /// </summary>
        public static bool GetCreatureHasTalent(Talent tTalent, uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Talent, tTalent);
            NWNXPInvoke.CallBuiltIn(306);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Get a random talent of oCreature, within nCategory.
        ///   - nCategory: TALENT_CATEGORY_*
        /// </summary>
        public static Talent GetCreatureTalentRandom(TalentCategory nCategory, uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushInteger((int)nCategory);
            NWNXPInvoke.CallBuiltIn(307);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Talent);
        }

        /// <summary>
        ///   Get the best talent (i.e. closest to nCRMax without going over) of oCreature,
        ///   within nCategory.
        ///   - nCategory: TALENT_CATEGORY_*
        /// </summary>
        public static Talent GetCreatureTalentBest(TalentCategory nCategory, int nCRMax,
            uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushInteger(nCRMax);
            NWNXPInvoke.StackPushInteger((int)nCategory);
            NWNXPInvoke.CallBuiltIn(308);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Talent);
        }

        /// <summary>
        ///   Use tChosenTalent on oTarget.
        /// </summary>
        public static void ActionUseTalentOnObject(Talent tChosenTalent, uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Talent, tChosenTalent);
            NWNXPInvoke.CallBuiltIn(309);
        }

        /// <summary>
        ///   Use tChosenTalent at lTargetLocation.
        /// </summary>
        public static void ActionUseTalentAtLocation(Talent tChosenTalent, Location lTargetLocation)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lTargetLocation);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Talent, tChosenTalent);
            NWNXPInvoke.CallBuiltIn(310);
        }

        /// <summary>
        ///   * Returns TRUE if oCreature is of a playable racial type.
        /// </summary>
        public static bool GetIsPlayableRacialType(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(312);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Jump to lDestination.  The action is added to the TOP of the action queue.
        /// </summary>
        public static void JumpToLocation(Location lDestination)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lDestination);
            NWNXPInvoke.CallBuiltIn(313);
        }

        /// <summary>
        ///   Get the number of ranks that oTarget has in nSkill.
        ///   - nSkill: SKILL_*
        /// </summary>
        public static int GetSkillRank(NWNSkillType nSkill, uint oTarget = OBJECT_INVALID, bool nBaseSkillRank = false)
        {
            NWNXPInvoke.StackPushInteger(nBaseSkillRank ? 1 : 0);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushInteger((int)nSkill);
            NWNXPInvoke.CallBuiltIn(315);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the attack target of oCreature.
        ///   This only works when oCreature is in combat.
        /// </summary>
        public static uint GetAttackTarget(uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(316);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the attack type (SPECIAL_ATTACK_*) of oCreature's last attack.
        ///   This only works when oCreature is in combat.
        /// </summary>
        public static SpecialAttack GetLastAttackType(uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(317);
            return (SpecialAttack)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the attack mode (COMBAT_MODE_*) of oCreature's last attack.
        ///   This only works when oCreature is in combat.
        /// </summary>
        public static CombatMode GetLastAttackMode(uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(318);
            return (CombatMode)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Use this in a trigger's OnClick event script to get the object that last
        ///   clicked on it.
        ///   This is identical to GetEnteringObject.
        ///   GetClickingObject() should not be called from a placeable's OnClick event,
        ///   instead use GetPlaceableLastClickedBy();
        /// </summary>
        public static uint GetClickingObject()
        {
            NWNXPInvoke.CallBuiltIn(326);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Initialise oTarget to listen for the standard Associates commands.
        /// </summary>
        public static void SetAssociateListenPatterns(uint oTarget = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(327);
        }

        /// <summary>
        ///   Get the last weapon that oCreature used in an attack.
        ///   * Returns OBJECT_INVALID if oCreature did not attack, or has no weapon equipped.
        /// </summary>
        public static uint GetLastWeaponUsed(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(328);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Use oPlaceable.
        /// </summary>
        public static void ActionInteractObject(uint oPlaceable)
        {
            NWNXPInvoke.StackPushObject(oPlaceable);
            NWNXPInvoke.CallBuiltIn(329);
        }

        /// <summary>
        ///   Get the last object that used the placeable object that is calling this function.
        ///   * Returns OBJECT_INVALID if it is called by something other than a placeable or
        ///   a door.
        /// </summary>
        public static uint GetLastUsedBy()
        {
            NWNXPInvoke.CallBuiltIn(330);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the amount of damage of type nDamageType that has been dealt to the caller.
        ///   - nDamageType: DAMAGE_TYPE_*
        public static int GetDamageDealtByType(DamageType nDamageType)
        {
            NWNXPInvoke.StackPushInteger((int)nDamageType);
            NWNXPInvoke.CallBuiltIn(344);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the total amount of damage that has been dealt to the caller.
        /// </summary>
        public static int GetTotalDamageDealt()
        {
            NWNXPInvoke.CallBuiltIn(345);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the last object that damaged oObject
        ///   * Returns OBJECT_INVALID if the passed in object is not a valid object.
        /// </summary>
        public static uint GetLastDamager(uint oObject = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(346);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the last object that disarmed the trap on the caller.
        ///   * Returns OBJECT_INVALID if the caller is not a valid placeable, trigger or
        ///   door.
        /// </summary>
        public static uint GetLastDisarmed()
        {
            NWNXPInvoke.CallBuiltIn(347);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the last object that disturbed the inventory of the caller.
        ///   * Returns OBJECT_INVALID if the caller is not a valid creature or placeable.
        /// </summary>
        public static uint GetLastDisturbed()
        {
            NWNXPInvoke.CallBuiltIn(348);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the last object that locked the caller.
        ///   * Returns OBJECT_INVALID if the caller is not a valid door or placeable.
        /// </summary>
        public static uint GetLastLocked()
        {
            NWNXPInvoke.CallBuiltIn(349);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the last object that unlocked the caller.
        ///   * Returns OBJECT_INVALID if the caller is not a valid door or placeable.
        /// </summary>
        public static uint GetLastUnlocked()
        {
            NWNXPInvoke.CallBuiltIn(350);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   * Returns TRUE if tTalent is valid.
        /// </summary>
        public static bool GetIsTalentValid(Talent tTalent)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Talent, tTalent);
            NWNXPInvoke.CallBuiltIn(359);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Causes the action subject to move away from lMoveAwayFrom.
        /// </summary>
        public static void ActionMoveAwayFromLocation(Location lMoveAwayFrom, bool bRun = false,
            float fMoveAwayRange = 40.0f)
        {
            NWNXPInvoke.StackPushFloat(fMoveAwayRange);
            NWNXPInvoke.StackPushInteger(bRun ? 1 : 0);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lMoveAwayFrom);
            NWNXPInvoke.CallBuiltIn(360);
        }

        /// <summary>
        ///   Get the target that the caller attempted to attack - this should be used in
        ///   conjunction with GetAttackTarget(). This value is set every time an attack is
        ///   made, and is reset at the end of combat.
        ///   * Returns OBJECT_INVALID if the caller is not a valid creature.
        /// </summary>
        public static uint GetAttemptedAttackTarget()
        {
            NWNXPInvoke.CallBuiltIn(361);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the type (TALENT_TYPE_*) of tTalent.
        /// </summary>
        public static TalentType GetTypeFromTalent(Talent tTalent)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Talent, tTalent);
            NWNXPInvoke.CallBuiltIn(362);
            return (TalentType)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the ID of tTalent.  This could be a SPELL_*, FEAT_* or SKILL_*.
        /// </summary>
        public static int GetIdFromTalent(Talent tTalent)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Talent, tTalent);
            NWNXPInvoke.CallBuiltIn(363);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the public part of the CD Key that oPlayer used when logging in.
        ///   - nSinglePlayerCDKey: If set to TRUE, the player's public CD Key will
        ///   be returned when the player is playing in single player mode
        ///   (otherwise returns an empty string in single player mode).
        /// </summary>
        public static string GetPCPublicCDKey(uint oPlayer, bool nSinglePlayerCDKey = false)
        {
            NWNXPInvoke.StackPushInteger(nSinglePlayerCDKey ? 1 : 0);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(369);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Get the IP address from which oPlayer has connected.
        /// </summary>
        public static string GetPCIPAddress(uint oPlayer)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(370);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Get the name of oPlayer.
        /// </summary>
        public static string GetPCPlayerName(uint oPlayer)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(371);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Sets oPlayer and oTarget to like each other.
        /// </summary>
        public static void SetPCLike(uint oPlayer, uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(372);
        }
        /// <summary>
        ///   Sets oPlayer and oTarget to dislike each other.
        /// </summary>
        public static void SetPCDislike(uint oPlayer, uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(373);
        }

        /// <summary>
        ///   Send a server message (szMessage) to the oPlayer.
        /// </summary>
        public static void SendMessageToPC(uint oPlayer, string szMessage)
        {
            NWNXPInvoke.StackPushString(szMessage);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(374);
        }

        /// <summary>
        ///   Get the target at which the caller attempted to cast a spell.
        ///   This value is set every time a spell is cast and is reset at the end of
        ///   combat.
        ///   * Returns OBJECT_INVALID if the caller is not a valid creature.
        /// </summary>
        public static uint GetAttemptedSpellTarget()
        {
            NWNXPInvoke.CallBuiltIn(375);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the experience assigned in the journal editor for szPlotID.
        /// </summary>
        public static int GetJournalQuestExperience(string szPlotID)
        {
            NWNXPInvoke.StackPushString(szPlotID);
            NWNXPInvoke.CallBuiltIn(384);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Jump to oToJumpTo (the action is added to the top of the action queue).
        /// </summary>
        public static void JumpToObject(uint oToJumpTo, bool nWalkStraightLineToPoint = true)
        {
            NWNXPInvoke.StackPushInteger(nWalkStraightLineToPoint ? 1 : 0);
            NWNXPInvoke.StackPushObject(oToJumpTo);
            NWNXPInvoke.CallBuiltIn(385);
        }

        /// <summary>
        ///   Convert nInteger to hex, returning the hex value as a string.
        ///   * Return value has the format "0x????????" where each ? will be a hex digit
        ///   (8 digits in total).
        /// </summary>
        public static string IntToHexString(int nInteger)
        {
            NWNXPInvoke.StackPushInteger(nInteger);
            NWNXPInvoke.CallBuiltIn(396);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Get the starting location of the module.
        /// </summary>
        public static Location GetStartingLocation()
        {
            NWNXPInvoke.CallBuiltIn(411);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Location);
        }

        /// <summary>
        ///   * Returns TRUE if the weapon equipped is capable of damaging oVersus.
        /// </summary>
        public static bool GetIsWeaponEffective(uint oVersus = OBJECT_INVALID, bool bOffHand = false)
        {
            NWNXPInvoke.StackPushInteger(bOffHand ? 1 : 0);
            NWNXPInvoke.StackPushObject(oVersus);
            NWNXPInvoke.CallBuiltIn(422);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Use this in a SpellCast script to determine whether the spell was considered
        ///   harmful.
        ///   * Returns TRUE if the last spell cast was harmful.
        /// </summary>
        public static bool GetLastSpellHarmful()
        {
            NWNXPInvoke.CallBuiltIn(423);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Try to send oTarget to a new server defined by sIPaddress.
        ///   - oTarget
        ///   - sIPaddress: this can be numerical "192.168.0.84" or alphanumeric
        ///   "www.bioware.com". It can also contain a port "192.168.0.84:5121" or
        ///   "www.bioware.com:5121"; if the port is not specified, it will default to
        ///   5121.
        ///   - sPassword: login password for the destination server
        ///   - sWaypointTag: if this is set, after portalling the character will be moved
        ///   to this waypoint if it exists
        ///   - bSeamless: if this is set, the client wil not be prompted with the
        ///   information window telling them about the server, and they will not be
        ///   allowed to save a copy of their character if they are using a local vault
        ///   character.
        /// </summary>
        public static void ActivatePortal(uint oTarget, string sIPaddress = "", string sPassword = "",
            string sWaypointTag = "", bool bSeamless = false)
        {
            NWNXPInvoke.StackPushInteger(bSeamless ? 1 : 0);
            NWNXPInvoke.StackPushString(sWaypointTag);
            NWNXPInvoke.StackPushString(sPassword);
            NWNXPInvoke.StackPushString(sIPaddress);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(474);
        }

        /// <summary>
        ///   The action subject will fake casting a spell at oTarget; the conjure and cast
        ///   animations and visuals will occur, nothing else.
        ///   - nSpell
        ///   - oTarget
        ///   - nProjectilePathType: PROJECTILE_PATH_TYPE_*
        /// </summary>
        public static void ActionCastFakeSpellAtObject(Spell nSpell, uint oTarget,
            ProjectilePathType nProjectilePathType = ProjectilePathType.Default)
        {
            NWNXPInvoke.StackPushInteger((int)nProjectilePathType);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushInteger((int)nSpell);
            NWNXPInvoke.CallBuiltIn(501);
        }

        /// <summary>
        ///   The action subject will fake casting a spell at lLocation; the conjure and
        ///   cast animations and visuals will occur, nothing else.
        ///   - nSpell
        ///   - lTarget
        ///   - nProjectilePathType: PROJECTILE_PATH_TYPE_*
        /// </summary>
        public static void ActionCastFakeSpellAtLocation(Spell nSpell, Location lTarget,
            ProjectilePathType nProjectilePathType = ProjectilePathType.Default)
        {
            NWNXPInvoke.StackPushInteger((int)nProjectilePathType);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lTarget);
            NWNXPInvoke.StackPushInteger((int)nSpell);
            NWNXPInvoke.CallBuiltIn(502);
        }

        /// <summary>
        ///   Shut down the currently loaded module and start a new one (moving all
        ///   currently-connected players to the starting point.
        /// </summary>
        public static void StartNewModule(string sModuleName)
        {
            NWNXPInvoke.StackPushString(sModuleName);
            NWNXPInvoke.CallBuiltIn(509);
        }

        /// <summary>
        ///   Only if we are in a single player game, AutoSave the game.
        /// </summary>
        public static void DoSinglePlayerAutoSave()
        {
            NWNXPInvoke.CallBuiltIn(512);
        }

        /// <summary>
        ///   Get the game difficulty (GAME_DIFFICULTY_*).
        /// </summary>
        public static int GetGameDifficulty()
        {
            NWNXPInvoke.CallBuiltIn(513);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the current action (ACTION_*) that oObject is executing.
        /// </summary>
        public static ActionType GetCurrentAction(uint oObject = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(522);
            return (ActionType)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Display floaty text above the specified creature.
        ///   The text will also appear in the chat buffer of each player that receives the
        ///   floaty text.
        ///   - nStrRefToDisplay: String ref (therefore text is translated)
        ///   - oCreatureToFloatAbove
        ///   - bBroadcastToFaction: If this is TRUE then only creatures in the same faction
        ///   as oCreatureToFloatAbove
        ///   will see the floaty text, and only if they are within range (30 metres).
        ///   - bChatWindow:  If TRUE, the string reference will be displayed in oCreatureToFloatAbove's chat window
        /// </summary>
        public static void FloatingTextStrRefOnCreature(
            int nStrRefToDisplay,
            uint oCreatureToFloatAbove,
            bool bBroadcastToFaction = true,
            bool bChatWindow = true)
        {
            NWNXPInvoke.StackPushInteger(bChatWindow ? 1 : 0);
            NWNXPInvoke.StackPushInteger(bBroadcastToFaction ? 1 : 0);
            NWNXPInvoke.StackPushObject(oCreatureToFloatAbove);
            NWNXPInvoke.StackPushInteger(nStrRefToDisplay);
            NWNXPInvoke.CallBuiltIn(525);
        }

        /// <summary>
        ///   Display floaty text above the specified creature.
        ///   The text will also appear in the chat buffer of each player that receives the
        ///   floaty text.
        ///   - sStringToDisplay: String
        ///   - oCreatureToFloatAbove
        ///   - bBroadcastToFaction: If this is TRUE then only creatures in the same faction
        ///   as oCreatureToFloatAbove
        ///   will see the floaty text, and only if they are within range (30 metres).
        ///   - bChatWindow:  If TRUE, sStringToDisplay will be displayed in oCreatureToFloatAbove's chat window.
        /// </summary>
        public static void FloatingTextStringOnCreature(
            string sStringToDisplay,
            uint oCreatureToFloatAbove,
            bool bBroadcastToFaction = true,
            bool bChatWindow = true)
        {
            NWNXPInvoke.StackPushInteger(bChatWindow ? 1 : 0);
            NWNXPInvoke.StackPushInteger(bBroadcastToFaction ? 1 : 0);
            NWNXPInvoke.StackPushObject(oCreatureToFloatAbove);
            NWNXPInvoke.StackPushString(sStringToDisplay);
            NWNXPInvoke.CallBuiltIn(526);
        }

        /// <summary>
        ///   * Returns TRUE if a specific key is required to open the lock on oObject.
        /// </summary>
        public static bool GetLockKeyRequired(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(537);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Get the tag of the key that will open the lock on oObject.
        /// </summary>
        public static string GetLockKeyTag(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(538);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   * Returns TRUE if the lock on oObject is lockable.
        /// </summary>
        public static bool GetLockLockable(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(539);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Get the DC for unlocking oObject.
        /// </summary>
        public static int GetLockUnlockDC(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(540);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the DC for locking oObject.
        /// </summary>
        public static int GetLockLockDC(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(541);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   - nFeat: FEAT_*
        ///   - oObject
        ///   * Returns TRUE if oObject has effects on it originating from nFeat.
        /// </summary>
        public static int GetHasFeatEffect(int nFeat, uint oObject = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.StackPushInteger(nFeat);
            NWNXPInvoke.CallBuiltIn(543);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Set the status of the illumination for oPlaceable.
        ///   - oPlaceable
        ///   - bIlluminate: if this is TRUE, oPlaceable's illumination will be turned on.
        ///   If this is FALSE, oPlaceable's illumination will be turned off.
        ///   Note: You must call RecomputeStaticLighting() after calling this function in
        ///   order for the changes to occur visually for the players.
        ///   SetPlaceableIllumination() buffers the illumination changes, which are then
        ///   sent out to the players once RecomputeStaticLighting() is called.  As such,
        ///   it is best to call SetPlaceableIllumination() for all the placeables you wish
        ///   to set the illumination on, and then call RecomputeStaticLighting() once after
        ///   all the placeable illumination has been set.
        ///   * If oPlaceable is not a placeable object, or oPlaceable is a placeable that
        ///   doesn't have a light, nothing will happen.
        /// </summary>
        public static void SetPlaceableIllumination(uint oPlaceable = OBJECT_INVALID, bool bIlluminate = true)
        {
            NWNXPInvoke.StackPushInteger(bIlluminate ? 1 : 0);
            NWNXPInvoke.StackPushObject(oPlaceable);
            NWNXPInvoke.CallBuiltIn(544);
        }

        /// <summary>
        ///   * Returns TRUE if the illumination for oPlaceable is on
        /// </summary>
        public static bool GetPlaceableIllumination(uint oPlaceable = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oPlaceable);
            NWNXPInvoke.CallBuiltIn(545);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   - oPlaceable
        ///   - nPlaceableAction: PLACEABLE_ACTION_*
        ///   * Returns TRUE if nPlacebleAction is valid for oPlaceable.
        /// </summary>
        public static int GetIsPlaceableObjectActionPossible(uint oPlaceable, int nPlaceableAction)
        {
            NWNXPInvoke.StackPushInteger(nPlaceableAction);
            NWNXPInvoke.StackPushObject(oPlaceable);
            NWNXPInvoke.CallBuiltIn(546);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   The caller performs nPlaceableAction on oPlaceable.
        ///   - oPlaceable
        ///   - nPlaceableAction: PLACEABLE_ACTION_*
        /// </summary>
        public static void DoPlaceableObjectAction(uint oPlaceable, int nPlaceableAction)
        {
            NWNXPInvoke.StackPushInteger(nPlaceableAction);
            NWNXPInvoke.StackPushObject(oPlaceable);
            NWNXPInvoke.CallBuiltIn(547);
        }

        /// <summary>
        ///   Force all the characters of the players who are currently in the game to
        ///   be exported to their respective directories i.e. LocalVault/ServerVault/ etc.
        /// </summary>
        public static void ExportAllCharacters()
        {
            NWNXPInvoke.CallBuiltIn(557);
        }

        /// <summary>
        ///   Write sLogEntry as a timestamped entry into the log file
        /// </summary>
        public static void WriteTimestampedLogEntry(string sLogEntry)
        {
            NWNXPInvoke.StackPushString(sLogEntry);
            NWNXPInvoke.CallBuiltIn(560);
        }

        /// <summary>
        ///   Get the module's name in the language of the server that's running it.
        ///   * If there is no entry for the language of the server, it will return an
        ///   empty string
        /// </summary>
        public static string GetModuleName()
        {
            NWNXPInvoke.CallBuiltIn(561);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   End the currently running game, play sEndMovie then return all players to the
        ///   game's main menu.
        /// </summary>
        public static void EndGame(string sEndMovie)
        {
            NWNXPInvoke.StackPushString(sEndMovie);
            NWNXPInvoke.CallBuiltIn(564);
        }

        /// <summary>
        ///   Counterspell oCounterSpellTarget.
        /// </summary>
        public static void ActionCounterSpell(uint oCounterSpellTarget)
        {
            NWNXPInvoke.StackPushObject(oCounterSpellTarget);
            NWNXPInvoke.CallBuiltIn(566);
        }

        /// <summary>
        ///   Get the duration (in seconds) of the sound attached to nStrRef
        ///   * Returns 0.0f if no duration is stored or if no sound is attached
        /// </summary>
        public static float GetStrRefSoundDuration(int nStrRef)
        {
            NWNXPInvoke.StackPushInteger(nStrRef);
            NWNXPInvoke.CallBuiltIn(571);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        ///   SpawnScriptDebugger() will cause the script debugger to be executed
        ///   after this command is executed!
        ///   In order to compile the script for debugging go to Tools->Options->Script Editor
        ///   and check the box labeled "Generate Debug Information When Compiling Scripts"
        ///   After you have checked the above box, recompile the script that you want to debug.
        ///   If the script file isn't compiled for debugging, this command will do nothing.
        ///   Remove any SpawnScriptDebugger() calls once you have finished
        ///   debugging the script.
        /// </summary>
        public static void SpawnScriptDebugger()
        {
            NWNXPInvoke.CallBuiltIn(578);
        }

        /// <summary>
        ///   This stores a float out to the specified campaign database
        ///   The database name IS case sensitive and it must be the same for both set and get functions.
        ///   The var name must be unique across the entire database, regardless of the variable type.
        ///   If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static void SetCampaignFloat(string sCampaignName, string sVarName, float flFloat,
            uint oPlayer = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.StackPushFloat(flFloat);
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushString(sCampaignName);
            NWNXPInvoke.CallBuiltIn(589);
        }

        /// <summary>
        ///   This stores an int out to the specified campaign database
        ///   The database name IS case sensitive and it must be the same for both set and get functions.
        ///   The var name must be unique across the entire database, regardless of the variable type.
        ///   If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static void SetCampaignInt(string sCampaignName, string sVarName, int nInt,
            uint oPlayer = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.StackPushInteger(nInt);
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushString(sCampaignName);
            NWNXPInvoke.CallBuiltIn(590);
        }

        /// <summary>
        ///   This stores a vector out to the specified campaign database
        ///   The database name IS case sensitive and it must be the same for both set and get functions.
        ///   The var name must be unique across the entire database, regardless of the variable type.
        ///   If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static void SetCampaignVector(string sCampaignName, string sVarName, Vector3 vVector,
            uint oPlayer = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.StackPushVector(vVector);
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushString(sCampaignName);
            NWNXPInvoke.CallBuiltIn(591);
        }

        /// <summary>
        ///   This stores a location out to the specified campaign database
        ///   The database name IS case sensitive and it must be the same for both set and get functions.
        ///   The var name must be unique across the entire database, regardless of the variable type.
        ///   If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static void SetCampaignLocation(string sCampaignName, string sVarName, Location locLocation,
            uint oPlayer = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, locLocation);
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushString(sCampaignName);
            NWNXPInvoke.CallBuiltIn(592);
        }

        /// <summary>
        ///   This stores a string out to the specified campaign database
        ///   The database name IS case sensitive and it must be the same for both set and get functions.
        ///   The var name must be unique across the entire database, regardless of the variable type.
        ///   If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static void SetCampaignString(string sCampaignName, string sVarName, string sString,
            uint oPlayer = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.StackPushString(sString);
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushString(sCampaignName);
            NWNXPInvoke.CallBuiltIn(593);
        }

        /// <summary>
        ///   This will delete the entire campaign database if it exists.
        /// </summary>
        public static void DestroyCampaignDatabase(string sCampaignName)
        {
            NWNXPInvoke.StackPushString(sCampaignName);
            NWNXPInvoke.CallBuiltIn(594);
        }
        /// <summary>
        ///   This will read a float from the  specified campaign database
        ///   The database name IS case sensitive and it must be the same for both set and get functions.
        ///   The var name must be unique across the entire database, regardless of the variable type.
        ///   If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static float GetCampaignFloat(string sCampaignName, string sVarName, uint oPlayer = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushString(sCampaignName);
            NWNXPInvoke.CallBuiltIn(595);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        ///   This will read an int from the  specified campaign database
        ///   The database name IS case sensitive and it must be the same for both set and get functions.
        ///   The var name must be unique across the entire database, regardless of the variable type.
        ///   If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static int GetCampaignInt(string sCampaignName, string sVarName, uint oPlayer = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushString(sCampaignName);
            NWNXPInvoke.CallBuiltIn(596);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   This will read a vector from the  specified campaign database
        ///   The database name IS case sensitive and it must be the same for both set and get functions.
        ///   The var name must be unique across the entire database, regardless of the variable type.
        ///   If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static Vector3 GetCampaignVector(string sCampaignName, string sVarName, uint oPlayer = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushString(sCampaignName);
            NWNXPInvoke.CallBuiltIn(597);
            return NWNXPInvoke.StackPopVector();
        }

        /// <summary>
        ///   This will read a location from the  specified campaign database
        ///   The database name IS case sensitive and it must be the same for both set and get functions.
        ///   The var name must be unique across the entire database, regardless of the variable type.
        ///   If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static Location GetCampaignLocation(string sCampaignName, string sVarName, uint oPlayer = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushString(sCampaignName);
            NWNXPInvoke.CallBuiltIn(598);
            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Location);
        }

        /// <summary>
        ///   This will read a string from the  specified campaign database
        ///   The database name IS case sensitive and it must be the same for both set and get functions.
        ///   The var name must be unique across the entire database, regardless of the variable type.
        ///   If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static string GetCampaignString(string sCampaignName, string sVarName, uint oPlayer = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushString(sCampaignName);
            NWNXPInvoke.CallBuiltIn(599);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   This will remove ANY campaign variable. Regardless of type.
        ///   Note that by normal database standards, deleting does not actually removed the entry from
        ///   the database, but flags it as deleted. Do not expect the database files to shrink in size
        ///   from this command. If you want to 'pack' the database, you will have to do it externally
        ///   from the game.
        /// </summary>
        public static void DeleteCampaignVariable(string sCampaignName, string sVarName, uint oPlayer = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushString(sCampaignName);
            NWNXPInvoke.CallBuiltIn(601);
        }

        /// <summary>
        ///   Stores an object with the given id.
        ///   NOTE: this command can be used for storing Creatures, Items, Placeables, Waypoints, Stores, Doors, Triggers, Encounters.
        ///   Returns 0 if it failled, 1 if it worked.
        ///   If bSaveObjectState is TRUE, local vars, effects, action queue, and transition info (triggers, doors) are saved out
        ///   (except for Combined Area Format, which always has object state saved out).
        /// </summary>
        public static int StoreCampaignObject(string sCampaignName, string sVarName, uint oObject, uint oPlayer = OBJECT_INVALID, bool bSaveObjectState = false)
        {
            NWNXPInvoke.StackPushInteger(bSaveObjectState ? 1 : 0);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushString(sCampaignName);
            NWNXPInvoke.CallBuiltIn(602);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Use RetrieveCampaign with the given id to restore it.
        ///   If you specify an owner, the object will try to be created in their repository
        ///   If the owner can't handle the item (or if it's a creature) it will be created on the ground.
        ///   If bLoadObjectState is TRUE, local vars, effects, action queue, and transition info (triggers, doors) are read in.
        /// </summary>
        public static uint RetrieveCampaignObject(string sCampaignName, string sVarName, Location locLocation, uint oOwner = OBJECT_INVALID, uint oPlayer = OBJECT_INVALID, bool bLoadObjectState = false)
        {
            NWNXPInvoke.StackPushInteger(bLoadObjectState ? 1 : 0);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.StackPushObject(oOwner);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, locLocation);
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushString(sCampaignName);
            NWNXPInvoke.CallBuiltIn(603);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        /// This stores a json out to the specified campaign database
        /// The database name:
        ///  - is case insensitive and it must be the same for both set and get functions.
        ///  - can only contain alphanumeric characters, no spaces.
        /// The var name must be unique across the entire database, regardless of the variable type.
        /// If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static void SetCampaignJson(string sCampaignName, string sVarName, Json jValue, uint oPlayer = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jValue);
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushString(sCampaignName);

            NWNXPInvoke.CallBuiltIn(1002);
        }

        /// <summary>
        /// This will read a json from the  specified campaign database
        /// The database name:
        ///  - is case insensitive and it must be the same for both set and get functions.
        ///  - can only contain alphanumeric characters, no spaces.
        /// The var name must be unique across the entire database, regardless of the variable type.
        /// If you want a variable to pertain to a specific player in the game, provide a player object.
        /// </summary>
        public static Json GetCampaignJson(string sCampaignName, string sVarName, uint oPlayer = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.StackPushString(sVarName);
            NWNXPInvoke.StackPushString(sCampaignName);
            NWNXPInvoke.CallBuiltIn(1003);

            return NWNXPInvoke.StackPopGameDefinedStructure((int)EngineStructure.Json);
        }

        /// <summary>
        ///   Gets the length of the specified wavefile, in seconds
        ///   Only works for sounds used for dialog.
        /// </summary>
        public static float GetDialogSoundLength(int nStrRef)
        {
            NWNXPInvoke.StackPushInteger(nStrRef);
            NWNXPInvoke.CallBuiltIn(694);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        ///   Gets a value from a 2DA file on the server and returns it as a string
        ///   avoid using this function in loops
        ///   - s2DA: the name of the 2da file, 16 chars max
        ///   - sColumn: the name of the column in the 2da
        ///   - nRow: the row in the 2da
        ///   * returns an empty string if file, row, or column not found
        /// </summary>
        public static string Get2DAString(string s2DA, string sColumn, int nRow)
        {
            NWNXPInvoke.StackPushInteger(nRow);
            NWNXPInvoke.StackPushString(sColumn);
            NWNXPInvoke.StackPushString(s2DA);
            NWNXPInvoke.CallBuiltIn(710);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Force the character of the player specified to be exported to its respective directory
        ///   i.e. LocalVault/ServerVault/ etc.
        /// </summary>
        public static void ExportSingleCharacter(uint oPlayer)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(719);
        }

        /// <summary>
        ///   This will play a sound that is associated with a stringRef, it will be a mono sound from the location of the object
        ///   running the command.
        ///   if nRunAsAction is off then the sound is forced to play intantly.
        /// </summary>
        public static void PlaySoundByStrRef(int nStrRef, bool nRunAsAction = true)
        {
            NWNXPInvoke.StackPushInteger(nRunAsAction ? 1 : 0);
            NWNXPInvoke.StackPushInteger(nStrRef);
            NWNXPInvoke.CallBuiltIn(720);
        }

        /// <summary>
        ///   Get the XP scale being used for the module.
        /// </summary>
        public static int GetModuleXPScale()
        {
            NWNXPInvoke.CallBuiltIn(817);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Set the XP scale used by the module.
        ///   - nXPScale: The XP scale to be used. Must be between 0 and 200.
        /// </summary>
        public static void SetModuleXPScale(int nXPScale)
        {
            NWNXPInvoke.StackPushInteger(nXPScale);
            NWNXPInvoke.CallBuiltIn(818);
        }

        /// <summary>
        ///   Gets the attack bonus limit.
        ///   - The default value is 20.
        /// </summary>
        public static int GetAttackBonusLimit()
        {
            NWNXPInvoke.CallBuiltIn(872);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Gets the damage bonus limit.
        ///   - The default value is 100.
        /// </summary>
        public static int GetDamageBonusLimit()
        {
            NWNXPInvoke.CallBuiltIn(873);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Gets the saving throw bonus limit.
        ///   - The default value is 20.
        /// </summary>
        public static int GetSavingThrowBonusLimit()
        {
            NWNXPInvoke.CallBuiltIn(874);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Gets the ability bonus limit.
        ///   - The default value is 12.
        /// </summary>
        public static int GetAbilityBonusLimit()
        {
            NWNXPInvoke.CallBuiltIn(875);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Gets the ability penalty limit.
        ///   - The default value is 30.
        /// </summary>
        public static int GetAbilityPenaltyLimit()
        {
            NWNXPInvoke.CallBuiltIn(876);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Gets the skill bonus limit.
        ///   - The default value is 50.
        /// </summary>
        public static int GetSkillBonusLimit()
        {
            NWNXPInvoke.CallBuiltIn(877);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Sets the attack bonus limit.
        ///   - The minimum value is 0.
        /// </summary>
        public static void SetAttackBonusLimit(int nNewLimit)
        {
            NWNXPInvoke.StackPushInteger(nNewLimit);
            NWNXPInvoke.CallBuiltIn(878);
        }

        /// <summary>
        ///   Sets the damage bonus limit.
        ///   - The minimum value is 0.
        /// </summary>
        public static void SetDamageBonusLimit(int nNewLimit)
        {
            NWNXPInvoke.StackPushInteger(nNewLimit);
            NWNXPInvoke.CallBuiltIn(879);
        }

        /// <summary>
        ///   Sets the saving throw bonus limit.
        ///   - The minimum value is 0.
        /// </summary>
        public static void SetSavingThrowBonusLimit(int nNewLimit)
        {
            NWNXPInvoke.StackPushInteger(nNewLimit);
            NWNXPInvoke.CallBuiltIn(880);
        }

        /// <summary>
        ///   Sets the ability bonus limit.
        ///   - The minimum value is 0.
        /// </summary>
        public static void SetAbilityBonusLimit(int nNewLimit)
        {
            NWNXPInvoke.StackPushInteger(nNewLimit);
            NWNXPInvoke.CallBuiltIn(881);
        }

        /// <summary>
        ///   Sets the ability penalty limit.
        ///   - The minimum value is 0.
        /// </summary>
        public static void SetAbilityPenaltyLimit(int nNewLimit)
        {
            NWNXPInvoke.StackPushInteger(nNewLimit);
            NWNXPInvoke.CallBuiltIn(882);
        }

        /// <summary>
        ///   Sets the skill bonus limit.
        ///   - The minimum value is 0.
        /// </summary>
        public static void SetSkillBonusLimit(int nNewLimit)
        {
            NWNXPInvoke.StackPushInteger(nNewLimit);
            NWNXPInvoke.CallBuiltIn(883);
        }

        /// <summary>
        ///   Get if oPlayer is currently connected over a relay (instead of directly).
        ///   Returns FALSE for any other object, including OBJECT_INVALID.
        /// </summary>
        public static int GetIsPlayerConnectionRelayed(uint oPlayer)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(884);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Returns the event script for the given object and handler.
        ///   Will return "" if unset, the object is invalid, or the object cannot
        ///   have the requested handler.
        /// </summary>
        public static string GetEventScript(uint oObject, EventScript nHandler)
        {
            NWNXPInvoke.StackPushInteger((int)nHandler);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(885);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Sets the given event script for the given object and handler.
        ///   Returns 1 on success, 0 on failure.
        ///   Will fail if oObject is invalid or does not have the requested handler.
        /// </summary>
        public static int SetEventScript(uint oObject, EventScript nHandler, string sScript)
        {
            NWNXPInvoke.StackPushString(sScript);
            NWNXPInvoke.StackPushInteger((int)nHandler);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(886);
            return NWNXPInvoke.StackPopInteger();
        }
        /// <summary>
        ///   Gets a visual transform on the given object.
        ///   - oObject can be any valid Creature, Placeable, Item or Door.
        ///   - nTransform is one of OBJECT_VISUAL_TRANSFORM_*
        public static float GetObjectVisualTransform(uint oObject, ObjectVisualTransform nTransform)
        {
            NWNXPInvoke.StackPushInteger((int)nTransform);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(887);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        /// Sets a visual transform on the given object.
        /// - oObject can be any valid Creature, Placeable, Item or Door.
        /// - nTransform is one of OBJECT_VISUAL_TRANSFORM_*
        public static float SetObjectVisualTransform(
            uint oObject,
            ObjectVisualTransform nTransform,
            float fValue,
            Lerp nLerpType = Lerp.None,
            float fLerpDuration = 0.0f,
            bool bPauseWithGame = true,
            ObjectVisualTransformDataScopeType nScope = ObjectVisualTransformDataScopeType.Base,
            ObjectVisualTransformBehaviorType nBehaviorFlags = ObjectVisualTransformBehaviorType.Default,
            int nRepeats = 0)
        {
            NWNXPInvoke.StackPushInteger(nRepeats);
            NWNXPInvoke.StackPushInteger((int)nBehaviorFlags);
            NWNXPInvoke.StackPushInteger((int)nScope);
            NWNXPInvoke.StackPushInteger(bPauseWithGame ? 1 : 0);
            NWNXPInvoke.StackPushFloat(fLerpDuration);
            NWNXPInvoke.StackPushInteger((int)nLerpType);
            NWNXPInvoke.StackPushFloat(fValue);
            NWNXPInvoke.StackPushInteger((int)nTransform);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(888);
            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        ///   Sets an integer material shader uniform override.
        ///   - sMaterial needs to be a material on that object.
        ///   - sParam needs to be a valid shader parameter already defined on the material.
        /// </summary>
        public static void SetMaterialShaderUniformInt(uint oObject, string sMaterial, string sParam, int nValue)
        {
            NWNXPInvoke.StackPushInteger(nValue);
            NWNXPInvoke.StackPushString(sParam);
            NWNXPInvoke.StackPushString(sMaterial);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(889);
        }

        /// <summary>
        ///   Sets a vec4 material shader uniform override.
        ///   - sMaterial needs to be a material on that object.
        ///   - sParam needs to be a valid shader parameter already defined on the material.
        ///   - You can specify a single float value to set just a float, instead of a vec4.
        /// </summary>
        public static void SetMaterialShaderUniformVec4(uint oObject, string sMaterial, string sParam, float fValue1,
            float fValue2 = 0.0f, float fValue3 = 0.0f, float fValue4 = 0.0f)
        {
            NWNXPInvoke.StackPushFloat(fValue4);
            NWNXPInvoke.StackPushFloat(fValue3);
            NWNXPInvoke.StackPushFloat(fValue2);
            NWNXPInvoke.StackPushFloat(fValue1);
            NWNXPInvoke.StackPushString(sParam);
            NWNXPInvoke.StackPushString(sMaterial);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(890);
        }

        /// <summary>
        ///   Resets material shader parameters on the given object:
        ///   - Supply a material to only reset shader uniforms for meshes with that material.
        ///   - Supply a parameter to only reset shader uniforms of that name.
        ///   - Supply both to only reset shader uniforms of that name on meshes with that material.
        /// </summary>
        public static void ResetMaterialShaderUniforms(uint oObject, string sMaterial = "", string sParam = "")
        {
            NWNXPInvoke.StackPushString(sParam);
            NWNXPInvoke.StackPushString(sMaterial);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(891);
        }

        /// <summary>
        ///   Vibrate the player's device or controller. Does nothing if vibration is not supported.
        ///   - nMotor is one of VIBRATOR_MOTOR_*
        public static void Vibrate(uint oPlayer, int nMotor, float fStrength, float fSeconds)
        {
            NWNXPInvoke.StackPushFloat(fSeconds);
            NWNXPInvoke.StackPushFloat(fStrength);
            NWNXPInvoke.StackPushInteger(nMotor);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(892);
        }

        /// <summary>
        ///   Unlock an achievement for the given player who must be logged in.
        ///   - sId is the achievement ID on the remote server
        ///   - nLastValue is the previous value of the associated achievement stat
        ///   - nCurValue is the current value of the associated achievement stat
        ///   - nMaxValue is the maximum value of the associate achievement stat
        /// </summary>
        public static void UnlockAchievement(uint oPlayer, string sId, int nLastValue = 0, int nCurValue = 0,
            int nMaxValue = 0)
        {
            NWNXPInvoke.StackPushInteger(nMaxValue);
            NWNXPInvoke.StackPushInteger(nCurValue);
            NWNXPInvoke.StackPushInteger(nLastValue);
            NWNXPInvoke.StackPushString(sId);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(893);
        }

        /// <summary>
        ///   Execute a script chunk.
        ///   The script chunk runs immediately, same as ExecuteScript().
        ///   The script is jitted in place and currently not cached: Each invocation will recompile the script chunk.
        ///   Note that the script chunk will run as if a separate script. This is not eval().
        ///   By default, the script chunk is wrapped into void main() {}. Pass in bWrapIntoMain = FALSE to override.
        ///   Returns "" on success, or the compilation error.
        /// </summary>
        public static string ExecuteScriptChunk(string sScriptChunk, uint oObject, bool bWrapIntoMain = true)
        {
            NWNXPInvoke.StackPushInteger(bWrapIntoMain ? 1 : 0);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.StackPushString(sScriptChunk);
            NWNXPInvoke.CallBuiltIn(894);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Returns a UUID. This UUID will not be associated with any object.
        ///   The generated UUID is currently a v4.
        /// </summary>
        public static string GetRandomUUID()
        {
            NWNXPInvoke.CallBuiltIn(895);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Returns the given objects' UUID. This UUID is persisted across save boundaries,
        ///   like Save/RestoreCampaignObject and save games.
        ///   Thus, reidentification is only guaranteed in scenarios where players cannot introduce
        ///   new objects (i.e. servervault servers).
        ///   UUIDs are guaranteed to be unique in any single running game.
        ///   If a loaded object would collide with a UUID already present in the game, the
        ///   object receives no UUID and a warning is emitted to the log. Requesting a UUID
        ///   for the new object will generate a random one.
        ///   This UUID is useful to, for example:
        ///   - Safely identify servervault characters
        ///   - Track serialisable objects (like items or creatures) as they are saved to the
        ///   campaign DB - i.e. persistent storage chests or dropped items.
        ///   - Track objects across multiple game instances (in trusted scenarios).
        ///   Currently, the following objects can carry UUIDs:
        ///   Items, Creatures, Placeables, Triggers, Doors, Waypoints, Stores,
        ///   Encounters, Areas.
        ///   Will return "" (empty string) when the given object cannot carry a UUID.
        /// </summary>
        public static string GetObjectUUID(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(896);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        ///   Forces the given object to receive a new UUID, discarding the current value.
        /// </summary>
        public static void ForceRefreshObjectUUID(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(897);
        }

        /// <summary>
        ///   Looks up a object on the server by it's UUID.
        ///   Returns OBJECT_INVALID if the UUID is not on the server.
        /// </summary>
        public static uint GetObjectByUUID(string sUUID)
        {
            NWNXPInvoke.StackPushString(sUUID);
            NWNXPInvoke.CallBuiltIn(898);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Do not call. This does nothing on this platform except to return an error.
        /// </summary>
        public static void Reserved899()
        {
            NWNXPInvoke.CallBuiltIn(899);
        }

        /// <summary>
        /// Makes oPC load texture sNewName instead of sOldName.
        /// If oPC is OBJECT_INVALID, it will apply the override to all active players
        /// Setting sNewName to "" will clear the override and revert to original.
        /// void SetTextureOverride();
        /// </summary>
        public static void SetTextureOverride(string OldName, string NewName = "", uint PC = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(PC);
            NWNXPInvoke.StackPushString(NewName);
            NWNXPInvoke.StackPushString(OldName);
            NWNXPInvoke.CallBuiltIn(900);
        }

        /// <summary>
        /// Displays sMsg on oPC's screen.
        /// The message is displayed on top of whatever is on the screen, including UI elements
        /// nX, nY - coordinates of the first character to be displayed. The value is in terms
        /// of character 'slot' relative to the nAnchor anchor point.
        /// If the number is negative, it is applied from the bottom/right.
        /// nAnchor - SCREEN_ANCHOR_* constant
        /// fLife - Duration in seconds until the string disappears.
        /// nRGBA, nRGBA2 - Colors of the string in 0xRRGGBBAA format. String starts at nRGBA,
        /// but as it nears end of life, it will slowly blend into nRGBA2.
        /// nID - Optional ID of a string. If not 0, subsequent calls to PostString will
        /// remove the old string with the same ID, even if it's lifetime has not elapsed.
        /// Only positive values are allowed.
        /// sFont - If specified, use this custom font instead of default console font.
        /// </summary>
        public static void PostString(uint PC, string Msg, int X = 0, int Y = 0, ScreenAnchor anchor = ScreenAnchor.TopLeft,
            float life = 10.0f, int RGBA = 2147418367, int RGBA2 = 2147418367, int ID = 0, string font = "")
        {
            NWNXPInvoke.StackPushString(font);
            NWNXPInvoke.StackPushInteger(ID);
            NWNXPInvoke.StackPushInteger(RGBA2);
            NWNXPInvoke.StackPushInteger(RGBA);
            NWNXPInvoke.StackPushFloat(life);
            NWNXPInvoke.StackPushInteger((int)anchor);
            NWNXPInvoke.StackPushInteger(Y);
            NWNXPInvoke.StackPushInteger(X);
            NWNXPInvoke.StackPushString(Msg);
            NWNXPInvoke.StackPushObject(PC);
            NWNXPInvoke.CallBuiltIn(901);
        }

        /// <summary>
        /// Returns oCreature's spell school specialization in nClass (SPELL_SCHOOL_* constants)
        /// Unless custom content is used, only Wizards have spell schools
        /// Returns -1 on error
        /// </summary>
        public static SpellSchool GetSpecialization(uint creature, ClassType playerClass)
        {
            NWNXPInvoke.StackPushInteger((int)playerClass);
            NWNXPInvoke.StackPushObject(creature);
            NWNXPInvoke.CallBuiltIn(902);
            return (SpellSchool)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Returns oCreature's domain in nClass (DOMAIN_* constants)
        /// nDomainIndex - 1 or 2
        /// Unless custom content is used, only Clerics have domains
        /// Returns -1 on error
        /// </summary>
        public static ClericDomain GetDomain(uint creature, int DomainIndex = 1, ClassType playerClass = ClassType.Cleric)
        {
            NWNXPInvoke.StackPushInteger((int)playerClass);
            NWNXPInvoke.StackPushObject(creature);
            NWNXPInvoke.CallBuiltIn(903);
            return (ClericDomain)NWNXPInvoke.StackPopInteger();
        }
        /// <summary>
        /// Queue an action to use an active item property.
        /// * oItem - item that has the item property to use
        /// * ip - item property to use
        /// * object oTarget - target
        /// * nSubPropertyIndex - specify if your itemproperty has subproperties (such as subradial spells)
        /// * bDecrementCharges - decrement charges if item property is limited
        /// </summary>
        public static void ActionUseItemOnObject(uint oItem, IntPtr ip, uint oTarget, int nSubPropertyIndex = 0, bool bDecrementCharges = true)
        {
            NWNXPInvoke.StackPushInteger(bDecrementCharges ? 1 : 0);
            NWNXPInvoke.StackPushInteger(nSubPropertyIndex);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.ItemProperty, ip);
            NWNXPInvoke.StackPushObject(oItem);
            NWNXPInvoke.CallBuiltIn(910);
        }

        /// <summary>
        /// Queue an action to use an active item property.
        /// * oItem - item that has the item property to use
        /// * ip - item property to use
        /// * location lTarget - target location (must be in the same area as item possessor)
        /// * nSubPropertyIndex - specify if your itemproperty has subproperties (such as subradial spells)
        /// * bDecrementCharges - decrement charges if item property is limited
        /// </summary>
        public static void ActionUseItemAtLocation(uint oItem, IntPtr ip, IntPtr lTarget, int nSubPropertyIndex = 0, bool bDecrementCharges = true)
        {
            NWNXPInvoke.StackPushInteger(bDecrementCharges ? 1 : 0);
            NWNXPInvoke.StackPushInteger(nSubPropertyIndex);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lTarget);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.ItemProperty, ip);
            NWNXPInvoke.StackPushObject(oItem);
            NWNXPInvoke.CallBuiltIn(911);
        }

        /// <summary>
        /// Makes oPC enter a targeting mode, letting them select an object as a target
        /// If a PC selects a target, it will trigger the module OnPlayerTarget event.
        /// </summary>
        public static void EnterTargetingMode(uint oPC, ObjectType nValidObjectTypes = ObjectType.All, MouseCursor nMouseCursorId = MouseCursor.Magic, MouseCursor nBadTargetCursor = MouseCursor.NoMagic)
        {
            NWNXPInvoke.StackPushInteger((int)nBadTargetCursor);
            NWNXPInvoke.StackPushInteger((int)nMouseCursorId);
            NWNXPInvoke.StackPushInteger((int)nValidObjectTypes);
            NWNXPInvoke.StackPushObject(oPC);
            NWNXPInvoke.CallBuiltIn(912);
        }

        /// <summary>
        /// Gets the target object in the module OnPlayerTarget event.
        /// Returns the area object when the target is the ground.
        /// </summary>
        public static uint GetTargetingModeSelectedObject()
        {
            NWNXPInvoke.CallBuiltIn(913);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        /// Gets the target position in the module OnPlayerTarget event.
        /// </summary>
        public static Vector3 GetTargetingModeSelectedPosition()
        {
            NWNXPInvoke.CallBuiltIn(914);
            return NWNXPInvoke.StackPopVector();
        }

        /// <summary>
        /// Gets the player object that triggered the OnPlayerTarget event.
        /// </summary>
        public static uint GetLastPlayerToSelectTarget()
        {
            NWNXPInvoke.CallBuiltIn(915);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        /// Sets oObject's hilite color to nColor
        /// The nColor format is 0xRRGGBB; -1 clears the color override.
        /// </summary>
        public static void SetObjectHiliteColor(uint oObject, int nColor = -1)
        {
            NWNXPInvoke.StackPushInteger(nColor);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(916);
        }

        /// <summary>
        /// Sets the cursor (MOUSECURSOR_*) to use when hovering over oObject
        /// </summary>
        public static void SetObjectMouseCursor(uint oObject, MouseCursor nCursor = MouseCursor.Invalid)
        {
            NWNXPInvoke.StackPushInteger((int)nCursor);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(917);
        }

        /// <summary>
        /// Overrides a given strref to always return sValue instead of what is in the TLK file.
        /// Setting sValue to "" will delete the override
        /// </summary>
        public static void SetTlkOverride(int nStrRef, string sValue = "")
        {
            NWNXPInvoke.StackPushString(sValue);
            NWNXPInvoke.StackPushInteger(nStrRef);
            NWNXPInvoke.CallBuiltIn(953);
        }

        /// <summary>
        ///  Returns the column name of s2DA at nColumn index (starting at 0).
        /// Returns "" if column nColumn doesn't exist (at end).
        /// </summary>
        public static string Get2DAColumn(string s2DA, int nColumnIdx)
        {
            NWNXPInvoke.StackPushInteger(nColumnIdx);
            NWNXPInvoke.StackPushString(s2DA);
            NWNXPInvoke.CallBuiltIn(1034);
            return NWNXPInvoke.StackPopString();
        }

        /// <summary>
        /// Returns the number of defined rows in the 2da s2DA.
        /// </summary>
        public static int Get2DARowCount(string s2DA)
        {
            NWNXPInvoke.StackPushString(s2DA);
            NWNXPInvoke.CallBuiltIn(1035);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Sets the spell targeting data manually for the player. This data is usually specified in spells.2da.
        /// This data persists through spell casts; you're overwriting the entry in spells.2da for this session.
        /// In multiplayer, these need to be reapplied when a player rejoins.
        /// - nSpell: SPELL_*
        public static void SetSpellTargetingData(uint oPlayer, Spell nSpell, int nShape, float fSizeX, float fSizeY, int nFlags)
        {
            NWNXPInvoke.StackPushInteger(nFlags);
            NWNXPInvoke.StackPushFloat(fSizeY);
            NWNXPInvoke.StackPushFloat(fSizeX);
            NWNXPInvoke.StackPushInteger(nShape);
            NWNXPInvoke.StackPushInteger((int)nSpell);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1041);
        }

        /// <summary>
        /// Sets the spell targeting data which is used for the next call to EnterTargetingMode() for this player.
        /// If the shape is set to SPELL_TARGETING_SHAPE_NONE and the range is provided, the dotted line range indicator will still appear.
        /// - nShape: SPELL_TARGETING_SHAPE_*
        public static void SetEnterTargetingModeData(
            uint oPlayer,
            int nShape,
            float fSizeX,
            float fSizeY,
            int nFlags,
            float fRange = 0.0f,
            Spell nSpell = Spell.AllSpells,
            FeatType nFeat = FeatType.Invalid)
        {
            NWNXPInvoke.StackPushInteger((int)nFeat);
            NWNXPInvoke.StackPushInteger((int)nSpell);
            NWNXPInvoke.StackPushFloat(fRange);
            NWNXPInvoke.StackPushInteger(nFlags);
            NWNXPInvoke.StackPushFloat(fSizeY);
            NWNXPInvoke.StackPushFloat(fSizeX);
            NWNXPInvoke.StackPushInteger(nShape);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1042);
        }

        /// <summary>
        /// Gets the number of memorized spell slots for a given spell level.
        /// - nClassType: a CLASS_TYPE_* constant. Must be a MemorizesSpells class.
        /// - nSpellLevel: the spell level, 0-9.
        /// Returns: the number of spell slots.
        /// </summary>
        public static int GetMemorizedSpellCountByLevel(uint oCreature, ClassType nClassType, int nSpellLevel)
        {
            NWNXPInvoke.StackPushInteger(nSpellLevel);
            NWNXPInvoke.StackPushInteger((int)nClassType);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1043);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Gets the spell id of a memorized spell slot.
        /// - nClassType: a CLASS_TYPE_* constant. Must be a MemorizesSpells class.
        /// - nSpellLevel: the spell level, 0-9.
        /// - nIndex: the index of the spell slot. Bounds: 0 <= nIndex < GetMemorizedSpellCountByLevel()
        /// Returns: a SPELL_* constant or -1 if the slot is not set.
        /// </summary>
        public static int GetMemorizedSpellId(uint oCreature, ClassType nClassType, int nSpellLevel, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushInteger(nSpellLevel);
            NWNXPInvoke.StackPushInteger((int)nClassType);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1044);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Gets the ready state of a memorized spell slot.
        /// - nClassType: a CLASS_TYPE_* constant. Must be a MemorizesSpells class.
        /// - nSpellLevel: the spell level, 0-9.
        /// - nIndex: the index of the spell slot. Bounds: 0 <= nIndex < GetMemorizedSpellCountByLevel()
        /// Returns: TRUE/FALSE or -1 if the slot is not set.
        /// </summary>
        public static int GetMemorizedSpellReady(uint oCreature, ClassType nClassType, int nSpellLevel, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushInteger(nSpellLevel);
            NWNXPInvoke.StackPushInteger((int)nClassType);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1045);
            return NWNXPInvoke.StackPopInteger();
        }
        /// <summary>
        /// Gets the metamagic of a memorized spell slot.
        /// - nClassType: a CLASS_TYPE_* constant. Must be a MemorizesSpells class.
        /// - nSpellLevel: the spell level, 0-9.
        /// - nIndex: the index of the spell slot. Bounds: 0 <= nIndex < GetMemorizedSpellCountByLevel()
        /// Returns: a METAMAGIC_* constant or -1 if the slot is not set.
        /// </summary>
        public static int GetMemorizedSpellMetaMagic(uint oCreature, ClassType nClassType, int nSpellLevel, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushInteger(nSpellLevel);
            NWNXPInvoke.StackPushInteger((int)nClassType);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1046);

            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Gets if the memorized spell slot has a domain spell.
        /// - nClassType: a CLASS_TYPE_* constant. Must be a MemorizesSpells class.
        /// - nSpellLevel: the spell level, 0-9.
        /// - nIndex: the index of the spell slot. Bounds: 0 <= nIndex < GetMemorizedSpellCountByLevel()
        /// Returns: TRUE/FALSE or -1 if the slot is not set.
        /// </summary>
        public static int GetMemorizedSpellIsDomainSpell(uint oCreature, ClassType nClassType, int nSpellLevel, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushInteger(nSpellLevel);
            NWNXPInvoke.StackPushInteger((int)nClassType);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1047);

            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Set a memorized spell slot.
        /// - nClassType: a CLASS_TYPE_* constant. Must be a MemorizesSpells class.
        /// - nSpellLevel: the spell level, 0-9.
        /// - nIndex: the index of the spell slot. Bounds: 0 <= nIndex < GetMemorizedSpellCountByLevel()
        /// - nSpellId: a SPELL_* constant.
        /// - bReady: TRUE to mark the slot ready.
        /// - nMetaMagic: a METAMAGIC_* constant.
        /// - bIsDomainSpell: TRUE for a domain spell.
        /// </summary>
        public static void SetMemorizedSpell(
            uint oCreature,
            ClassType nClassType,
            int nSpellLevel,
            int nIndex,
            Spell nSpellId,
            bool bReady = true,
            MetaMagic nMetaMagic = MetaMagic.None,
            bool bIsDomainSpell = false)
        {
            NWNXPInvoke.StackPushInteger(bIsDomainSpell ? 1 : 0);
            NWNXPInvoke.StackPushInteger((int)nMetaMagic);
            NWNXPInvoke.StackPushInteger(bReady ? 1 : 0);
            NWNXPInvoke.StackPushInteger((int)nSpellId);
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushInteger(nSpellLevel);
            NWNXPInvoke.StackPushInteger((int)nClassType);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1048);
        }

        /// <summary>
        /// Set the ready state of a memorized spell slot.
        /// - nClassType: a CLASS_TYPE_* constant. Must be a MemorizesSpells class.
        /// - nSpellLevel: the spell level, 0-9.
        /// - nIndex: the index of the spell slot. Bounds: 0 <= nIndex < GetMemorizedSpellCountByLevel()
        /// - bReady: TRUE to mark the slot ready.
        /// </summary>
        public static void SetMemorizedSpellReady(uint oCreature, ClassType nClassType, int nSpellLevel, int nIndex, bool bReady)
        {
            NWNXPInvoke.StackPushInteger(bReady ? 1 : 0);
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushInteger(nSpellLevel);
            NWNXPInvoke.StackPushInteger((int)nClassType);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1049);
        }

        /// <summary>
        /// Clear a specific memorized spell slot.
        /// - nClassType: a CLASS_TYPE_* constant. Must be a MemorizesSpells class.
        /// - nSpellLevel: the spell level, 0-9.
        /// - nIndex: the index of the spell slot. Bounds: 0 <= nIndex < GetMemorizedSpellCountByLevel()
        /// </summary>
        public static void ClearMemorizedSpell(uint oCreature, ClassType nClassType, int nSpellLevel, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushInteger(nSpellLevel);
            NWNXPInvoke.StackPushInteger((int)nClassType);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1050);
        }

        /// <summary>
        /// Clear all memorized spell slots of a specific spell id, including metamagic'd ones.
        /// - nClassType: a CLASS_TYPE_* constant. Must be a MemorizesSpells class.
        /// - nSpellId: a SPELL_* constant.
        /// </summary>
        public static void ClearMemorizedSpellBySpellId(uint oCreature, ClassType nClassType, int nSpellId)
        {
            NWNXPInvoke.StackPushInteger(nSpellId);
            NWNXPInvoke.StackPushInteger((int)nClassType);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1051);
        }

        /// <summary>
        ///  Gets the number of known spells for a given spell level.
        /// - nClassType: a CLASS_TYPE_* constant. Must be a SpellBookRestricted class.
        /// - nSpellLevel: the spell level, 0-9.
        /// Returns: the number of known spells.
        /// </summary>
        public static int GetKnownSpellCount(uint oCreature, ClassType nClassType, int nSpellLevel)
        {
            NWNXPInvoke.StackPushInteger(nSpellLevel);
            NWNXPInvoke.StackPushInteger((int)nClassType);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1052);

            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Gets the spell id of a known spell.
        /// - nClassType: a CLASS_TYPE_* constant. Must be a SpellBookRestricted class.
        /// - nSpellLevel: the spell level, 0-9.
        /// - nIndex: the index of the known spell. Bounds: 0 <= nIndex < GetKnownSpellCount()
        /// Returns: a SPELL_* constant or -1 on error.
        /// </summary>
        public static int GetKnownSpellId(uint oCreature, ClassType nClassType, int nSpellLevel, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushInteger(nSpellLevel);
            NWNXPInvoke.StackPushInteger((int)nClassType);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1053);

            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Gets if a spell is in the known spell list.
        /// - nClassType: a CLASS_TYPE_* constant. Must be a SpellBookRestricted class.
        /// - nSpellId: a SPELL_* constant.
        /// Returns: TRUE if the spell is in the known spell list.
        /// </summary>
        public static bool GetIsInKnownSpellList(uint oCreature, ClassType nClassType, Spell nSpellId)
        {
            NWNXPInvoke.StackPushInteger((int)nSpellId);
            NWNXPInvoke.StackPushInteger((int)nClassType);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1054);

            return NWNXPInvoke.StackPopInteger() == 1;
        }

        /// <summary>
        /// Gets the amount of uses a spell has left.
        /// - nClassType: a CLASS_TYPE_* constant.
        /// - nSpellid: a SPELL_* constant.
        /// - nMetaMagic: a METAMAGIC_* constant.
        /// - nDomainLevel: the domain level, if a domain spell.
        /// Returns: the amount of spell uses left.
        /// </summary>
        public static int GetSpellUsesLeft(
            uint oCreature,
            ClassType nClassType,
            Spell nSpellId,
            MetaMagic nMetaMagic = MetaMagic.None,
            int nDomainLevel = 0)
        {
            NWNXPInvoke.StackPushInteger(nDomainLevel);
            NWNXPInvoke.StackPushInteger((int)nMetaMagic);
            NWNXPInvoke.StackPushInteger((int)nSpellId);
            NWNXPInvoke.StackPushInteger((int)nClassType);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1055);

            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Gets the spell level at which a class gets a spell.
        /// - nClassType: a CLASS_TYPE_* constant.
        /// - nSpellId: a SPELL_* constant.
        /// Returns: the spell level or -1 if the class does not get the spell.
        /// </summary>
        public static int GetSpellLevelByClass(ClassType nClassType, Spell nSpellId)
        {
            NWNXPInvoke.StackPushInteger((int)nSpellId);
            NWNXPInvoke.StackPushInteger((int)nClassType);
            NWNXPInvoke.CallBuiltIn(1056);

            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Replaces oObject's animation sOld with sNew.
        /// Specifying sNew = "" will restore the original animation.
        /// </summary>
        public static void ReplaceObjectAnimation(uint oObject, string sOld, string sNew = "")
        {
            NWNXPInvoke.StackPushString(sNew);
            NWNXPInvoke.StackPushString(sOld);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(1057);
        }

        /// <summary>
        /// Sets the distance (in meters) at which oObject info will be sent to clients (default 45.0)
        /// This is still subject to other limitations, such as perception ranges for creatures
        /// Note: Increasing visibility ranges of many objects can have a severe negative effect on
        ///       network latency and server performance, and rendering additional objects will
        ///       impact graphics performance of clients. Use cautiously.
        /// </summary>
        public static void SetObjectVisibleDistance(uint oObject, float fDistance = 45.0f)
        {
            NWNXPInvoke.StackPushFloat(fDistance);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(1058);
        }

        /// <summary>
        /// Gets oObject's visible distance, as set by SetObjectVisibleDistance()
        /// Returns -1.0f on error
        /// </summary>
        public static float GetObjectVisibleDistance(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(1059);

            return NWNXPInvoke.StackPopFloat();
        }

        /// <summary>
        /// Sets the active game pause state - same as if the player requested pause.
        /// </summary>
        public static void SetGameActivePause(bool bState)
        {
            NWNXPInvoke.StackPushInteger(bState ? 1 : 0);
            NWNXPInvoke.CallBuiltIn(1060);
        }

        /// <summary>
        /// Returns >0 if the game is currently paused:
        /// - 0: Game is not paused.
        /// - 1: Timestop
        /// - 2: Active Player Pause (optionally on top of timestop)
        /// </summary>
        public static int GetGamePauseState()
        {
            NWNXPInvoke.CallBuiltIn(1061);

            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Set the gender of oCreature.
        /// - nGender: a GENDER_* constant.
        /// </summary>
        public static void SetGender(uint oCreature, Gender nGender)
        {
            NWNXPInvoke.StackPushInteger((int)nGender);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1062);
        }

        /// <summary>
        /// Get the soundset of oCreature.
        /// Returns -1 on error.
        /// </summary>
        public static int GetSoundset(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1063);

            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Set the soundset of oCreature, see soundset.2da for possible values.
        /// </summary>
        public static void SetSoundset(uint oCreature, int nSoundset)
        {
            NWNXPInvoke.StackPushInteger(nSoundset);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1064);
        }

        /// <summary>
        /// Ready a spell level for oCreature.
        /// - nSpellLevel: 0-9
        /// - nClassType: a CLASS_TYPE_* constant or CLASS_TYPE_INVALID to ready the spell level for all classes.
        /// </summary>
        public static void ReadySpellLevel(uint oCreature, int nSpellLevel, ClassType nClassType = ClassType.Invalid)
        {
            NWNXPInvoke.StackPushInteger((int)nClassType);
            NWNXPInvoke.StackPushInteger(nSpellLevel);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1065);
        }

        /// <summary>
        /// Makes oCreature controllable by oPlayer, if player party control is enabled
        /// Setting oPlayer=OBJECT_INVALID removes the override and reverts to regular party control behavior
        /// NB: A creature is only controllable by one player, so if you set oPlayer to a non-Player object
        ///    (e.g. the module) it will disable regular party control for this creature
        /// </summary>
        public static void SetCommandingPlayer(uint oCreature, uint oPlayer)
        {
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1066);
        }

        /// <summary>
        /// Sets oPlayer's camera limits that override any client configuration limits
        /// Value of -1.0 means use the client config instead
        /// NB: Like all other camera settings, this is not saved when saving the game
        /// </summary>
        public static void SetCameraLimits(
            uint oPlayer,
            float fMinPitch = -1.0f,
            float fMaxPitch = -1.0f,
            float fMinDist = -1.0f,
            float fMaxDist = -1.0f)
        {
            NWNXPInvoke.StackPushFloat(fMaxDist);
            NWNXPInvoke.StackPushFloat(fMinDist);
            NWNXPInvoke.StackPushFloat(fMaxPitch);
            NWNXPInvoke.StackPushFloat(fMinPitch);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1067);
        }

        /// <summary>
        /// Sets the object oPlayer's camera will be attached to.
        /// - oTarget: A valid creature or placeable. If oTarget is OBJECT_INVALID, it will revert the camera back to oPlayer's character.
        ///            The target must be known to oPlayer's client, this means it must be in the same area and within visible distance.
        ///              - SetObjectVisibleDistance() can be used to increase this range.
        ///              - If the target is a creature, it also must be within the perception range of oPlayer and perceived.
        /// - bFindClearView: if TRUE, the client will attempt to find a camera position where oTarget is in view.
        /// Notes:
        ///       - If oTarget gets destroyed while oPlayer's camera is attached to it, the camera will revert back to oPlayer's character.
        ///       - If oPlayer goes through a transition with its camera attached to a different object, it will revert back to oPlayer's character.
        ///       - The object the player's camera is attached to is not saved when saving the game.
        /// </summary>
        public static void AttachCamera(uint oPlayer, uint oTarget, bool bFindClearView = false)
        {
            NWNXPInvoke.StackPushInteger(bFindClearView ? 1 : 0);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1073);
        }

        /// <summary>
        /// Get the current discoverability mask of oObject.
        /// Returns -1 if oObject cannot have a discovery mask.
        /// </summary>
        public static int GetObjectUiDiscoveryMask(uint oObject)
        {
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(1074);

            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Sets the discoverability mask on oObject.
        /// This allows toggling areahilite (TAB key by default) and mouseover discovery in the area view.
        /// * nMask is a bitmask of OBJECT_UI_DISCOVERY_*
        public static void SetObjectUiDiscoveryMask(uint oObject, ObjectUIDiscoveryType nMask = ObjectUIDiscoveryType.Default)
        {
            NWNXPInvoke.StackPushInteger((int)nMask);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(1075);
        }

        /// <summary>
        /// Sets a text override for the mouseover/tab-highlight text bubble of oObject.
        /// Will currently only work on Creatures, Items and Useable Placeables.
        /// * nMode is one of OBJECT_UI_TEXT_BUBBLE_OVERRIDE_*.
        /// </summary>
        public static void SetObjectTextBubbleOverride(uint oObject, ObjectUITextBubbleOverrideType nMode, string sText)
        {
            NWNXPInvoke.StackPushString(sText);
            NWNXPInvoke.StackPushInteger((int)nMode);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(1076);
        }

        /// <summary>
        /// Immediately unsets a VTs for the given object, with no lerp.
        /// * nScope: one of OBJECT_VISUAL_TRANSFORM_DATA_SCOPE_, or -1 for all scopes
        /// Returns TRUE only if transforms were successfully removed (valid object, transforms existed).
        /// </summary>
        public static bool ClearObjectVisualTransform(uint oObject, ObjectVisualTransformDataScopeType nScope = ObjectVisualTransformDataScopeType.Invalid)
        {
            NWNXPInvoke.StackPushInteger((int)nScope);
            NWNXPInvoke.StackPushObject(oObject);
            NWNXPInvoke.CallBuiltIn(1077);

            return NWNXPInvoke.StackPopInteger() == 1;
        }

        /// <summary>
        /// Gets an optional vector of specific gui events in the module OnPlayerGuiEvent event.
        /// GUIEVENT_RADIAL_OPEN - World vector position of radial if on tile.
        /// </summary>
        /// <returns></returns>
        public static Vector3 GetLastGuiEventVector()
        {
            NWNXPInvoke.CallBuiltIn(1078);

            return NWNXPInvoke.StackPopVector();
        }

        /// <summary>
        /// Sets oPlayer's camera settings that override any client configuration settings
        /// nFlags is a bitmask of CAMERA_FLAG_* constants;
        /// NB: Like all other camera settings, this is not saved when saving the game
        /// </summary>
        public static void SetCameraFlags(uint oPlayer, int nFlags = 0)
        {
            NWNXPInvoke.StackPushInteger(nFlags);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(1079);
        }

        /// <summary>
        /// In the spell script returns the feat used, or -1 if no feat was used
        /// </summary>
        public static int GetSpellFeatId()
        {
            NWNXPInvoke.CallBuiltIn(1095);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// If oCreature has nFeat, and nFeat is useable, returns the number of remaining uses left
        /// or the maximum int value if the feat has unlimited uses (eg FEAT_KNOCKDOWN)
        /// - nFeat: FEAT_*
        /// - oCreature: Creature to check the feat of
        /// </summary>
        public static int GetFeatRemainingUses(FeatType nFeat, uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushInteger((int)nFeat);
            NWNXPInvoke.CallBuiltIn(1097);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Change a tile in an area, it will also update the tile for all players in the area.
        /// * Notes:
        ///   - For optimal use you should be familiar with how tilesets / .set files work.
        ///   - Will not update the height of non-creature objects.
        ///   - Creatures may get stuck on non-walkable terrain.
        /// </summary>
        public static void SetTile(
            Location locTile,
            int nTileID,
            int nOrientation,
            int nHeight = 0,
            SetTileFlagType nFlags = SetTileFlagType.RecomputeLighting)
        {
            NWNXPInvoke.StackPushInteger((int)nFlags);
            NWNXPInvoke.StackPushInteger(nHeight);
            NWNXPInvoke.StackPushInteger(nOrientation);
            NWNXPInvoke.StackPushInteger(nTileID);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, locTile);
            NWNXPInvoke.CallBuiltIn(1098);
        }

        /// <summary>
        ///  Get the ID of the tile at location locTile.
        /// Returns -1 on error.
        /// </summary>
        public static int GetTileID(Location locTile)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, locTile);
            NWNXPInvoke.CallBuiltIn(1099);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Get the orientation of the tile at location locTile.
        /// Returns -1 on error.
        /// </summary>
        public static int GetTileOrientation(Location locTile)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, locTile);
            NWNXPInvoke.CallBuiltIn(1100);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Get the height of the tile at location locTile.
        /// Returns -1 on error.
        /// </summary>
        public static int GetTileHeight(Location locTile)
        {
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, locTile);
            NWNXPInvoke.CallBuiltIn(1101);
            return NWNXPInvoke.StackPopInteger();
        }
        /// <summary>
        /// All clients in oArea will reload the area's grass.
        /// This can be used to update the grass of an area after changing a tile with SetTile() that will have or used to have grass.
        /// </summary>
        public static void ReloadAreaGrass(uint oArea)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(1102);
        }

        /// <summary>
        /// Set the state of the tile animation loops of the tile at location locTile.
        /// </summary>
        public static void SetTileAnimationLoops(Location locTile, bool bAnimLoop1, bool bAnimLoop2, bool bAnimLoop3)
        {
            NWNXPInvoke.StackPushInteger(bAnimLoop3 ? 1 : 0);
            NWNXPInvoke.StackPushInteger(bAnimLoop2 ? 1 : 0);
            NWNXPInvoke.StackPushInteger(bAnimLoop1 ? 1 : 0);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, locTile);
            NWNXPInvoke.CallBuiltIn(1103);
        }

        /// <summary>
        /// Change multiple tiles in an area, it will also update the tiles for all players in the area.
        /// Note: See SetTile() for additional information.
        /// - oArea: the area to change one or more tiles of.
        /// - jTileData: a JsonArray() with one or more JsonObject()s with the following keys:
        ///               - index: the index of the tile as a JsonInt()
        ///                        For example, a 3x3 area has the following tile indexes:
        ///                        6 7 8
        ///                        3 4 5
        ///                        0 1 2
        ///               - tileid: the ID of the tile as a JsonInt(), defaults to 0 if not set
        ///               - orientation: the orientation of the tile as JsonInt(), defaults to 0 if not set
        ///               - height: the height of the tile as JsonInt(), defaults to 0 if not set
        ///               - animloop1: the state of a tile animation, 1/0 as JsonInt(), defaults to the current value if not set
        ///               - animloop2: the state of a tile animation, 1/0 as JsonInt(), defaults to the current value if not set
        ///               - animloop3: the state of a tile animation, 1/0 as JsonInt(), defaults to the current value if not set
        /// - nFlags: a bitmask of SETTILE_FLAG_* constants.
        /// - sTileset: if not empty, it will also change the area's tileset
        ///             Warning: only use this if you really know what you're doing, it's very easy to break things badly.
        ///                      Make sure jTileData changes *all* tiles in the area and to a tile id that's supported by sTileset.
        /// </summary>
        public static void SetTileJson(
            uint oArea,
            Json jTileData,
            SetTileFlagType nFlags = SetTileFlagType.RecomputeLighting,
            string sTileset = "")
        {
            NWNXPInvoke.StackPushString(sTileset);
            NWNXPInvoke.StackPushInteger((int)nFlags);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Json, jTileData);
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(1104);
        }

        /// <summary>
        /// All clients in oArea will reload the inaccessible border tiles.
        /// This can be used to update the edge tiles after changing a tile with SetTile().
        /// </summary>
        public static void ReloadAreaBorder(uint oArea)
        {
            NWNXPInvoke.StackPushObject(oArea);
            NWNXPInvoke.CallBuiltIn(1105);
        }

        /// <summary>
        /// Sets whether or not oCreature's nIconId is flashing in their GUI icon bar.  If oCreature does not
        /// have an icon associated with nIconId, nothing happens. This function does not add icons to 
        /// oCreature's GUI icon bar. The icon will flash until the underlying effect is removed or this 
        /// function is called again with bFlashing = FALSE.
        /// - oCreature: Player object to affect
        /// - nIconId: Referenced to effecticons.2da or EFFECT_ICON_*
        /// - bFlashing: TRUE to force an existing icon to flash, FALSE to stop.
        /// </summary>
        public static void SetEffectIconFlashing(uint oCreature, int nIconId, bool bFlashing = true)
        {
            NWNXPInvoke.StackPushInteger(bFlashing ? 1 : 0);
            NWNXPInvoke.StackPushInteger(nIconId);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1106);
        }

        /// <summary>
        /// Returns the INVENTORY_SLOT_* constant of the last item equipped.  Can only be used in the
        /// module's OnPlayerEquip event.  Returns -1 on error.
        /// </summary>
        public static InventorySlot GetPCItemLastEquippedSlot()
        {
            NWNXPInvoke.CallBuiltIn(1108);
            return (InventorySlot)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Returns the INVENTORY_SLOT_* constant of the last item unequipped.  Can only be used in the
        /// module's OnPlayerUnequip event.  Returns -1 on error.
        /// </summary>
        public static InventorySlot GetPCItemLastUnequippedSlot()
        {
            NWNXPInvoke.CallBuiltIn(1109);
            return (InventorySlot)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Returns TRUE if the last spell was cast spontaneously
        /// eg; a Cleric casting SPELL_CURE_LIGHT_WOUNDS when it is not prepared, using another level 1 slot
        /// </summary>
        public static bool GetSpellCastSpontaneously()
        {
            NWNXPInvoke.CallBuiltIn(1110);
            return NWNXPInvoke.StackPopInteger() == 1;
        }

        /// <summary>
        /// Return the current game tick rate (mainloop iterations per second).
        /// This is equivalent to graphics frames per second when the module is running inside a client.
        /// </summary>
        public static int GetTickRate()
        {
            NWNXPInvoke.CallBuiltIn(1113);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Returns the level of the last spell cast. This value is only valid in a Spell script.
        /// </summary>
        public static int GetLastSpellLevel()
        {
            NWNXPInvoke.CallBuiltIn(1114);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        /// Returns the 32bit integer hash of sString
        /// This hash is stable and will always have the same value for the same input string, regardless of platform.
        /// The hash algorithm is the same as the one used internally for strings in case statements, so you can do:
        ///    switch (HashString(sString))
        ///    {
        ///         case "AAA":    HandleAAA(); break;
        ///         case "BBB":    HandleBBB(); break;
        ///    }
        /// NOTE: The exact algorithm used is XXH32(sString) ^ XXH32(""). This means that HashString("") is 0.
        /// </summary>
        public static int HashString(string sString)
        {
            NWNXPInvoke.StackPushString(sString);
            NWNXPInvoke.CallBuiltIn(1115);
            return NWNXPInvoke.StackPopInteger();
        }

        public static int GetMicrosecondCounter()
        {
            NWNXPInvoke.CallBuiltIn(1116);
            return NWNXPInvoke.StackPopInteger();
        }

    }
}