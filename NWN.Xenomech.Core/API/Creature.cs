using NWN.Xenomech.Core.API.Enum;
using NWN.Xenomech.Core.API.Enum.Creature;
using NWN.Xenomech.Core.Interop;

namespace NWN.Xenomech.Core.API
{
    public partial class NWScript
    {/// <summary>
        ///   Returns the footstep type of the creature specified.
        /// </summary>
        public static FootstepType GetFootstepType(uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(788);
            return (FootstepType)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Sets the footstep type of the creature specified.
        /// </summary>
        public static void SetFootstepType(FootstepType nFootstepType, uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushInteger((int)nFootstepType);
            NWNXPInvoke.CallBuiltIn(789);
        }

        /// <summary>
        ///   Returns the Wing type of the creature specified.
        /// </summary>
        public static WingType GetCreatureWingType(uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(790);
            return (WingType)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Sets the Wing type of the creature specified.
        /// </summary>
        public static void SetCreatureWingType(WingType nWingType, uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushInteger((int)nWingType);
            NWNXPInvoke.CallBuiltIn(791);
        }

        /// <summary>
        ///   Returns the model number being used for the body part and creature specified.
        /// </summary>
        public static int GetCreatureBodyPart(CreaturePart nPart, uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushInteger((int)nPart);
            NWNXPInvoke.CallBuiltIn(792);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Sets the body part model to be used on the creature specified.
        /// </summary>
        public static void SetCreatureBodyPart(CreaturePart nPart, int nModelNumber, uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushInteger(nModelNumber);
            NWNXPInvoke.StackPushInteger((int)nPart);
            NWNXPInvoke.CallBuiltIn(793);
        }

        /// <summary>
        ///   Returns the Tail type of the creature specified.
        /// </summary>
        public static TailType GetCreatureTailType(uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(794);
            return (TailType)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Sets the Tail type of the creature specified.
        /// </summary>
        public static void SetCreatureTailType(TailType nTailType, uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushInteger((int)nTailType);
            NWNXPInvoke.CallBuiltIn(795);
        }

        /// <summary>
        ///   Returns the creature's currently set PhenoType (body type).
        /// </summary>
        public static PhenoType GetPhenoType(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(778);
            return (PhenoType)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Sets the creature's PhenoType (body type) to the type specified.
        /// </summary>
        public static void SetPhenoType(PhenoType nPhenoType, uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushInteger((int)nPhenoType);
            NWNXPInvoke.CallBuiltIn(779);
        }

        /// <summary>
        ///   Is this creature able to be disarmed?
        /// </summary>
        public static bool GetIsCreatureDisarmable(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(773);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Returns the class that the spellcaster cast the spell as.
        /// </summary>
        public static ClassType GetLastSpellCastClass()
        {
            NWNXPInvoke.CallBuiltIn(754);
            return (ClassType)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Sets the number of base attacks each round for the specified creature.
        /// </summary>
        public static void SetBaseAttackBonus(int nBaseAttackBonus, uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushInteger(nBaseAttackBonus);
            NWNXPInvoke.CallBuiltIn(755);
        }

        /// <summary>
        ///   Restores the number of base attacks back to its original state.
        /// </summary>
        public static void RestoreBaseAttackBonus(uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(756);
        }

        /// <summary>
        ///   Sets the creature's appearance type to the value specified.
        /// </summary>
        public static void SetCreatureAppearanceType(uint oCreature, AppearanceType nAppearanceType)
        {
            NWNXPInvoke.StackPushInteger((int)nAppearanceType);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(765);
        }

        /// <summary>
        ///   Returns the default package selected for this creature to level up with.
        /// </summary>
        public static int GetCreatureStartingPackage(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(766);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Returns the spell resistance of the specified creature.
        /// </summary>
        public static int GetSpellResistance(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(749);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Sets the lootable state of a *living* NPC creature.
        /// </summary>
        public static void SetLootable(uint oCreature, bool bLootable)
        {
            NWNXPInvoke.StackPushInteger(bLootable ? 1 : 0);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(740);
        }

        /// <summary>
        ///   Returns the lootable state of a creature.
        /// </summary>
        public static bool GetLootable(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(741);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Gets the status of ACTION_MODE_* modes on a creature.
        /// </summary>
        public static bool GetActionMode(uint oCreature, ActionMode nMode)
        {
            NWNXPInvoke.StackPushInteger((int)nMode);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(735);
            return NWNXPInvoke.StackPopInteger() == 1;
        }

        /// <summary>
        ///   Sets the status of modes ACTION_MODE_* on a creature.
        /// </summary>
        public static void SetActionMode(uint oCreature, ActionMode nMode, bool nStatus)
        {
            NWNXPInvoke.StackPushInteger(nStatus ? 1 : 0);
            NWNXPInvoke.StackPushInteger((int)nMode);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(736);
        }
        /// <summary>
        ///   Returns the current arcane spell failure factor of a creature
        /// </summary>
        public static int GetArcaneSpellFailure(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(737);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Set the name of oCreature's sub race to sSubRace.
        /// </summary>
        public static void SetSubRace(uint oCreature, string sSubRace)
        {
            NWNXPInvoke.StackPushString(sSubRace);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(721);
        }

        /// <summary>
        ///   Set the name of oCreature's Deity to sDeity.
        /// </summary>
        public static void SetDeity(uint oCreature, string sDeity)
        {
            NWNXPInvoke.StackPushString(sDeity);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(722);
        }

        /// <summary>
        ///   Returns TRUE if the creature oCreature is currently possessed by a DM character.
        /// </summary>
        public static bool GetIsDMPossessed(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(723);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Increment the remaining uses per day for this creature by one.
        /// </summary>
        public static void IncrementRemainingFeatUses(uint oCreature, FeatType nFeat)
        {
            NWNXPInvoke.StackPushInteger((int)nFeat);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(718);
        }

        /// <summary>
        ///   Gets the current AI Level that the creature is running at.
        /// </summary>
        public static AILevel GetAILevel(uint oTarget = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(712);
            return (AILevel)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Sets the current AI Level of the creature to the value specified.
        /// </summary>
        public static void SetAILevel(uint oTarget, AILevel nAILevel)
        {
            NWNXPInvoke.StackPushInteger((int)nAILevel);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(713);
        }

        /// <summary>
        ///   This will return TRUE if the creature running the script is a familiar currently possessed by its master.
        /// </summary>
        public static bool GetIsPossessedFamiliar(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(714);
            return NWNXPInvoke.StackPopInteger() == 1;
        }

        /// <summary>
        ///   This will cause a Player Creature to unpossess his/her familiar.
        /// </summary>
        public static void UnpossessFamiliar(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(715);
        }

        /// <summary>
        ///   Get the immortal flag on a creature
        /// </summary>
        public static bool GetImmortal(uint oTarget = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(708);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Does a single attack on every hostile creature within 10ft. of the attacker.
        /// </summary>
        public static void DoWhirlwindAttack(bool bDisplayFeedback = true, bool bImproved = false)
        {
            NWNXPInvoke.StackPushInteger(bImproved ? 1 : 0);
            NWNXPInvoke.StackPushInteger(bDisplayFeedback ? 1 : 0);
            NWNXPInvoke.CallBuiltIn(709);
        }

        /// <summary>
        ///   Returns the base attack bonus for the given creature.
        /// </summary>
        public static int GetBaseAttackBonus(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(699);
            return NWNXPInvoke.StackPopInteger();
        }
        /// <summary>
        ///   Set a creature's immortality flag.
        /// </summary>
        public static void SetImmortal(uint oCreature, bool bImmortal)
        {
            NWNXPInvoke.StackPushInteger(bImmortal ? 1 : 0);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(700);
        }

        /// <summary>
        ///   Returns true if 1d20 roll + skill rank is greater than or equal to difficulty.
        /// </summary>
        public static bool GetIsSkillSuccessful(uint oTarget, NWNSkillType nSkill, int nDifficulty)
        {
            NWNXPInvoke.StackPushInteger(nDifficulty);
            NWNXPInvoke.StackPushInteger((int)nSkill);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(689);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Decrement the remaining uses per day for this creature by one.
        /// </summary>
        public static void DecrementRemainingFeatUses(uint oCreature, int nFeat)
        {
            NWNXPInvoke.StackPushInteger(nFeat);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(580);
        }

        /// <summary>
        ///   Decrement the remaining uses per day for this creature by one.
        /// </summary>
        public static void DecrementRemainingSpellUses(uint oCreature, int nSpell)
        {
            NWNXPInvoke.StackPushInteger(nSpell);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(581);
        }

        /// <summary>
        ///   Returns the stealth mode of the specified creature.
        /// </summary>
        public static StealthMode GetStealthMode(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(574);
            return (StealthMode)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Returns the detection mode of the specified creature.
        /// </summary>
        public static DetectMode GetDetectMode(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(575);
            return (DetectMode)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Returns the defensive casting mode of the specified creature.
        /// </summary>
        public static CastingMode GetDefensiveCastingMode(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(576);
            return (CastingMode)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Returns the appearance type of the specified creature.
        /// </summary>
        public static AppearanceType GetAppearanceType(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(577);
            return (AppearanceType)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the last hostile actor for a victim.
        /// </summary>
        public static uint GetLastHostileActor(uint oVictim = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oVictim);
            NWNXPInvoke.CallBuiltIn(556);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the number of Hitdice worth of Turn Resistance for an undead.
        /// </summary>
        public static int GetTurnResistanceHD(uint oUndead = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oUndead);
            NWNXPInvoke.CallBuiltIn(478);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the size of a creature.
        /// </summary>
        public static CreatureSize GetCreatureSize(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(479);
            return (CreatureSize)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Causes all creatures within 10 meters to stop and become neutral.
        /// </summary>
        public static void SurrenderToEnemies()
        {
            NWNXPInvoke.CallBuiltIn(476);
        }

        /// <summary>
        ///   Determine whether oSource has a friendly reaction towards oTarget.
        /// </summary>
        public static int GetIsReactionTypeFriendly(uint oTarget, uint oSource = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oSource);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(469);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Determine whether oSource has a neutral reaction towards oTarget.
        /// </summary>
        public static int GetIsReactionTypeNeutral(uint oTarget, uint oSource = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oSource);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(470);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Determine whether oSource has a hostile reaction towards oTarget.
        /// </summary>
        public static bool GetIsReactionTypeHostile(uint oTarget, uint oSource = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oSource);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(471);
            return NWNXPInvoke.StackPopInteger() == 1;
        }

        /// <summary>
        ///   Take gold from a creature.
        /// </summary>
        public static void TakeGoldFromCreature(int nAmount, uint oCreatureToTakeFrom, bool bDestroy = false)
        {
            NWNXPInvoke.StackPushInteger(bDestroy ? 1 : 0);
            NWNXPInvoke.StackPushObject(oCreatureToTakeFrom);
            NWNXPInvoke.StackPushInteger(nAmount);
            NWNXPInvoke.CallBuiltIn(444);
        }

        /// <summary>
        ///   Get the object that killed the caller.
        /// </summary>
        public static uint GetLastKiller()
        {
            NWNXPInvoke.CallBuiltIn(437);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Returns TRUE if oCreature is the Dungeon Master.
        /// </summary>
        public static bool GetIsDM(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(420);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Get the object ID of the player who last pressed the respawn button.
        /// </summary>
        public static uint GetLastRespawnButtonPresser()
        {
            NWNXPInvoke.CallBuiltIn(419);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   The creature will equip the armor in its possession that has the highest armor class.
        /// </summary>
        public static void ActionEquipMostEffectiveArmor()
        {
            NWNXPInvoke.CallBuiltIn(404);
        }

        /// <summary>
        ///   * Returns TRUE if oCreature was spawned from an encounter.
        /// </summary>
        public static int GetIsEncounterCreature(uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(409);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   The creature will equip the melee weapon in its possession that can do the most damage.
        /// </summary>
        public static void ActionEquipMostDamagingMelee(uint oVersus = OBJECT_INVALID, bool bOffHand = false)
        {
            NWNXPInvoke.StackPushInteger(bOffHand ? 1 : 0);
            NWNXPInvoke.StackPushObject(oVersus);
            NWNXPInvoke.CallBuiltIn(399);
        }

        /// <summary>
        ///   The creature will equip the ranged weapon in its possession that can do the most damage.
        /// </summary>
        public static void ActionEquipMostDamagingRanged(uint oVersus = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oVersus);
            NWNXPInvoke.CallBuiltIn(400);
        }

        /// <summary>
        ///   Gives nXpAmount to oCreature.
        /// </summary>
        public static void GiveXPToCreature(uint oCreature, int nXpAmount)
        {
            NWNXPInvoke.StackPushInteger(nXpAmount);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(393);
        }

        /// <summary>
        ///   Sets oCreature's experience to nXpAmount.
        /// </summary>
        public static void SetXP(uint oCreature, int nXpAmount)
        {
            NWNXPInvoke.StackPushInteger(nXpAmount);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(394);
        }

        /// <summary>
        ///   Get oCreature's experience.
        /// </summary>
        public static int GetXP(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(395);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Force the action subject to move to lDestination.
        /// </summary>
        public static void ActionForceMoveToLocation(Location lDestination, bool bRun = false, float fTimeout = 30.0f)
        {
            NWNXPInvoke.StackPushFloat(fTimeout);
            NWNXPInvoke.StackPushInteger(bRun ? 1 : 0);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lDestination);
            NWNXPInvoke.CallBuiltIn(382);
        }

        /// <summary>
        ///   Force the action subject to move to oMoveTo.
        /// </summary>
        public static void ActionForceMoveToObject(uint oMoveTo, bool bRun = false, float fRange = 1.0f, float fTimeout = 30.0f)
        {
            NWNXPInvoke.StackPushFloat(fTimeout);
            NWNXPInvoke.StackPushFloat(fRange);
            NWNXPInvoke.StackPushInteger(bRun ? 1 : 0);
            NWNXPInvoke.StackPushObject(oMoveTo);
            NWNXPInvoke.CallBuiltIn(383);
        }

        /// <summary>
        ///   Get the last creature that opened the caller.
        /// </summary>
        public static uint GetLastOpenedBy()
        {
            NWNXPInvoke.CallBuiltIn(376);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Determines the number of times that oCreature has nSpell memorized.
        /// </summary>
        public static int GetHasSpell(Spell nSpell, uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushInteger((int)nSpell);
            NWNXPInvoke.CallBuiltIn(377);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the gender of oCreature.
        /// </summary>
        public static Gender GetGender(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(358);
            return (Gender)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the type of disturbance that caused the caller's OnInventoryDisturbed script to fire.
        /// </summary>
        public static DisturbType GetInventoryDisturbType()
        {
            NWNXPInvoke.CallBuiltIn(352);
            return (DisturbType)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the item that caused the caller's OnInventoryDisturbed script to fire.
        /// </summary>
        public static uint GetInventoryDisturbItem()
        {
            NWNXPInvoke.CallBuiltIn(353);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the class of a creature based on class position.
        /// </summary>
        public static ClassType GetClassByPosition(int nClassPosition, uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushInteger(nClassPosition);
            NWNXPInvoke.CallBuiltIn(341);
            return (ClassType)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the level of a creature's class based on class position.
        /// </summary>
        public static int GetLevelByPosition(int nClassPosition, uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushInteger(nClassPosition);
            NWNXPInvoke.CallBuiltIn(342);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Determine the levels that oCreature holds in nClassType.
        /// </summary>
        public static int GetLevelByClass(ClassType nClassType, uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushInteger((int)nClassType);
            NWNXPInvoke.CallBuiltIn(343);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Returns the ability modifier for the specified ability.
        /// </summary>
        public static int GetAbilityModifier(AbilityType nAbility, uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushInteger((int)nAbility);
            NWNXPInvoke.CallBuiltIn(331);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   * Returns TRUE if oCreature is in combat.
        /// </summary>
        public static bool GetIsInCombat(uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(320);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Give nGP gold to oCreature.
        /// </summary>
        public static void GiveGoldToCreature(uint oCreature, int nGP)
        {
            NWNXPInvoke.StackPushInteger(nGP);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(322);
        }

        /// <summary>
        ///   Get the creature nearest to lLocation, subject to all the criteria specified.
        /// </summary>
        public static uint GetNearestCreatureToLocation(CreatureType nFirstCriteriaType, bool nFirstCriteriaValue,
            Location lLocation, int nNth = 1, int nSecondCriteriaType = -1, int nSecondCriteriaValue = -1,
            int nThirdCriteriaType = -1, int nThirdCriteriaValue = -1)
        {
            NWNXPInvoke.StackPushInteger(nThirdCriteriaValue);
            NWNXPInvoke.StackPushInteger(nThirdCriteriaType);
            NWNXPInvoke.StackPushInteger(nSecondCriteriaValue);
            NWNXPInvoke.StackPushInteger(nSecondCriteriaType);
            NWNXPInvoke.StackPushInteger(nNth);
            NWNXPInvoke.StackPushGameDefinedStructure((int)EngineStructure.Location, lLocation);
            NWNXPInvoke.StackPushInteger(nFirstCriteriaValue ? 1 : 0);
            NWNXPInvoke.StackPushInteger((int)nFirstCriteriaType);
            NWNXPInvoke.CallBuiltIn(226);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the level at which this creature cast its last spell (or spell-like ability).
        /// </summary>
        public static int GetCasterLevel(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(84);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the racial type of oCreature.
        /// </summary>
        public static RacialType GetRacialType(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(107);
            return (RacialType)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Gets the total number of spell abilities a creature has.
        /// </summary>
        public static int GetSpellAbilityCount(uint oCreature)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1128);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Gets the spell Id of the spell ability at the given index.
        /// </summary>
        public static Spell GetSpellAbilitySpell(uint oCreature, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1129);
            return (Spell)NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Gets the caster level of the spell ability in the given slot.
        /// </summary>
        public static int GetSpellAbilityCasterLevel(uint oCreature, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1130);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Gets the ready state of a spell ability.
        /// </summary>
        public static int GetSpellAbilityReady(uint oCreature, int nIndex)
        {
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1131);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Set the ready state of a spell ability slot.
        /// </summary>
        public static void SetSpellAbilityReady(uint oCreature, int nIndex, bool bReady = true)
        {
            NWNXPInvoke.StackPushInteger(bReady ? 1 : 0);
            NWNXPInvoke.StackPushInteger(nIndex);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1132);
        }

        /// <summary>
        ///   Sets the age of oCreature.
        /// </summary>
        public static void SetAge(uint oCreature, int nAge)
        {
            NWNXPInvoke.StackPushInteger(nAge);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1144);
        }

        /// <summary>
        ///   Gets the base number of attacks oCreature can make every round.
        /// </summary>
        public static int GetAttacksPerRound(uint oCreature, bool bCheckOverridenValue = true)
        {
            NWNXPInvoke.StackPushInteger(bCheckOverridenValue ? 1 : 0);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.CallBuiltIn(1145);
            return NWNXPInvoke.StackPopInteger();
        }
    }
}