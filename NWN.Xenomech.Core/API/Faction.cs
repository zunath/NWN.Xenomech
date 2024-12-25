using NWN.Xenomech.Core.API.Enum;
using NWN.Xenomech.Core.Interop;

namespace NWN.Xenomech.Core.API
{
    public partial class NWScript
    {/// <summary>
     ///   Get the weakest member of oFactionMember's faction.
     /// </summary>
        public static uint GetFactionWeakestMember(uint oFactionMember = OBJECT_INVALID, bool bMustBeVisible = true)
        {
            NWNXPInvoke.StackPushInteger(bMustBeVisible ? 1 : 0);
            NWNXPInvoke.StackPushObject(oFactionMember);
            NWNXPInvoke.CallBuiltIn(181);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the strongest member of oFactionMember's faction.
        /// </summary>
        public static uint GetFactionStrongestMember(uint oFactionMember = OBJECT_INVALID, bool bMustBeVisible = true)
        {
            NWNXPInvoke.StackPushInteger(bMustBeVisible ? 1 : 0);
            NWNXPInvoke.StackPushObject(oFactionMember);
            NWNXPInvoke.CallBuiltIn(182);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the member of oFactionMember's faction that has taken the most hit points of damage.
        /// </summary>
        public static uint GetFactionMostDamagedMember(uint oFactionMember = OBJECT_INVALID, bool bMustBeVisible = true)
        {
            NWNXPInvoke.StackPushInteger(bMustBeVisible ? 1 : 0);
            NWNXPInvoke.StackPushObject(oFactionMember);
            NWNXPInvoke.CallBuiltIn(183);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the member of oFactionMember's faction that has taken the fewest hit points of damage.
        /// </summary>
        public static uint GetFactionLeastDamagedMember(uint oFactionMember = OBJECT_INVALID, bool bMustBeVisible = true)
        {
            NWNXPInvoke.StackPushInteger(bMustBeVisible ? 1 : 0);
            NWNXPInvoke.StackPushObject(oFactionMember);
            NWNXPInvoke.CallBuiltIn(184);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the amount of gold held by oFactionMember's faction.
        /// </summary>
        public static int GetFactionGold(uint oFactionMember)
        {
            NWNXPInvoke.StackPushObject(oFactionMember);
            NWNXPInvoke.CallBuiltIn(185);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the average reputation of oSourceFactionMember's faction towards oTarget.
        /// </summary>
        public static int GetFactionAverageReputation(uint oSourceFactionMember, uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushObject(oSourceFactionMember);
            NWNXPInvoke.CallBuiltIn(186);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the average good/evil alignment of oFactionMember's faction.
        /// </summary>
        public static int GetFactionAverageGoodEvilAlignment(uint oFactionMember)
        {
            NWNXPInvoke.StackPushObject(oFactionMember);
            NWNXPInvoke.CallBuiltIn(187);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the average law/chaos alignment of oFactionMember's faction.
        /// </summary>
        public static int GetFactionAverageLawChaosAlignment(uint oFactionMember)
        {
            NWNXPInvoke.StackPushObject(oFactionMember);
            NWNXPInvoke.CallBuiltIn(188);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the average level of the members of the faction.
        /// </summary>
        public static int GetFactionAverageLevel(uint oFactionMember)
        {
            NWNXPInvoke.StackPushObject(oFactionMember);
            NWNXPInvoke.CallBuiltIn(189);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the average XP of the members of the faction.
        /// </summary>
        public static int GetFactionAverageXP(uint oFactionMember)
        {
            NWNXPInvoke.StackPushObject(oFactionMember);
            NWNXPInvoke.CallBuiltIn(190);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the most frequent class in the faction.
        /// </summary>
        public static int GetFactionMostFrequentClass(uint oFactionMember)
        {
            NWNXPInvoke.StackPushObject(oFactionMember);
            NWNXPInvoke.CallBuiltIn(191);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Get the object faction member with the lowest armor class.
        /// </summary>
        public static uint GetFactionWorstAC(uint oFactionMember = OBJECT_INVALID, bool bMustBeVisible = true)
        {
            NWNXPInvoke.StackPushInteger(bMustBeVisible ? 1 : 0);
            NWNXPInvoke.StackPushObject(oFactionMember);
            NWNXPInvoke.CallBuiltIn(192);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the object faction member with the highest armor class.
        /// </summary>
        public static uint GetFactionBestAC(uint oFactionMember = OBJECT_INVALID, bool bMustBeVisible = true)
        {
            NWNXPInvoke.StackPushInteger(bMustBeVisible ? 1 : 0);
            NWNXPInvoke.StackPushObject(oFactionMember);
            NWNXPInvoke.CallBuiltIn(193);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the reputation of oSource towards oTarget.
        /// </summary>
        public static int GetReputation(uint oSource, uint oTarget)
        {
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.StackPushObject(oSource);
            NWNXPInvoke.CallBuiltIn(208);
            return NWNXPInvoke.StackPopInteger();
        }
        /// <summary>
        ///   Adjust how oSourceFactionMember's faction feels about oTarget by the
        ///   specified amount.
        ///   Note: This adjusts Faction Reputation, how the entire faction that
        ///   oSourceFactionMember is in, feels about oTarget.
        ///   * No return value
        ///   Note: You can't adjust a player character's (PC) faction towards
        ///   NPCs, so attempting to make an NPC hostile by passing in a PC object
        ///   as oSourceFactionMember in the following call will fail:
        ///   AdjustReputation(oNPC,oPC,-100);
        ///   Instead you should pass in the PC object as the first
        ///   parameter as in the following call which should succeed:
        ///   AdjustReputation(oPC,oNPC,-100);
        ///   Note: Will fail if oSourceFactionMember is a plot object.
        /// </summary>
        public static void AdjustReputation(uint oTarget, uint oSourceFactionMember, int nAdjustment)
        {
            NWNXPInvoke.StackPushInteger(nAdjustment);
            NWNXPInvoke.StackPushObject(oSourceFactionMember);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(209);
        }

        /// <summary>
        ///   * Returns TRUE if oSource considers oTarget as an enemy.
        /// </summary>
        public static bool GetIsEnemy(uint oTarget, uint oSource = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oSource);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(235);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   * Returns TRUE if oSource considers oTarget as a friend.
        /// </summary>
        public static bool GetIsFriend(uint oTarget, uint oSource = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oSource);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(236);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   * Returns TRUE if oSource considers oTarget as neutral.
        /// </summary>
        public static bool GetIsNeutral(uint oTarget, uint oSource = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oSource);
            NWNXPInvoke.StackPushObject(oTarget);
            NWNXPInvoke.CallBuiltIn(237);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Get the player leader of the faction of which oMemberOfFaction is a member.
        ///   * Returns OBJECT_INVALID if oMemberOfFaction is not a valid creature,
        ///   or oMemberOfFaction is a member of a NPC faction.
        /// </summary>
        public static uint GetFactionLeader(uint oMemberOfFaction)
        {
            NWNXPInvoke.StackPushObject(oMemberOfFaction);
            NWNXPInvoke.CallBuiltIn(562);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Set how nStandardFaction feels about oCreature.
        ///   - nStandardFaction: STANDARD_FACTION_*
        ///   - nNewReputation: 0-100 (inclusive)
        ///   - oCreature
        /// </summary>
        public static void SetStandardFactionReputation(StandardFaction nStandardFaction, int nNewReputation,
            uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushInteger(nNewReputation);
            NWNXPInvoke.StackPushInteger((int)nStandardFaction);
            NWNXPInvoke.CallBuiltIn(523);
        }

        /// <summary>
        ///   Find out how nStandardFaction feels about oCreature.
        ///   - nStandardFaction: STANDARD_FACTION_*
        ///   - oCreature
        ///   Returns -1 on an error.
        ///   Returns 0-100 based on the standing of oCreature within the faction nStandardFaction.
        ///   0-10   :  Hostile.
        ///   11-89  :  Neutral.
        ///   90-100 :  Friendly.
        /// </summary>
        public static int GetStandardFactionReputation(StandardFaction nStandardFaction, uint oCreature = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushInteger((int)nStandardFaction);
            NWNXPInvoke.CallBuiltIn(524);
            return NWNXPInvoke.StackPopInteger();
        }

        /// <summary>
        ///   Make oCreatureToChange join one of the standard factions.
        ///   ** This will only work on an NPC **
        ///   - nStandardFaction: STANDARD_FACTION_*
        /// </summary>
        public static void ChangeToStandardFaction(uint oCreatureToChange, StandardFaction nStandardFaction)
        {
            NWNXPInvoke.StackPushInteger((int)nStandardFaction);
            NWNXPInvoke.StackPushObject(oCreatureToChange);
            NWNXPInvoke.CallBuiltIn(412);
        }

        /// <summary>
        ///   Get the first member of oMemberOfFaction's faction (start to cycle through
        ///   oMemberOfFaction's faction).
        ///   * Returns OBJECT_INVALID if oMemberOfFaction's faction is invalid.
        /// </summary>
        public static uint GetFirstFactionMember(uint oMemberOfFaction, bool bPCOnly = true)
        {
            NWNXPInvoke.StackPushInteger(bPCOnly ? 1 : 0);
            NWNXPInvoke.StackPushObject(oMemberOfFaction);
            NWNXPInvoke.CallBuiltIn(380);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   Get the next member of oMemberOfFaction's faction (continue to cycle through
        ///   oMemberOfFaction's faction).
        ///   * Returns OBJECT_INVALID if oMemberOfFaction's faction is invalid.
        /// </summary>
        public static uint GetNextFactionMember(uint oMemberOfFaction, bool bPCOnly = true)
        {
            NWNXPInvoke.StackPushInteger(bPCOnly ? 1 : 0);
            NWNXPInvoke.StackPushObject(oMemberOfFaction);
            NWNXPInvoke.CallBuiltIn(381);
            return NWNXPInvoke.StackPopObject();
        }

        /// <summary>
        ///   * Returns TRUE if the Faction Ids of the two objects are the same
        /// </summary>
        public static bool GetFactionEqual(uint oFirstObject, uint oSecondObject = OBJECT_INVALID)
        {
            NWNXPInvoke.StackPushObject(oSecondObject);
            NWNXPInvoke.StackPushObject(oFirstObject);
            NWNXPInvoke.CallBuiltIn(172);
            return NWNXPInvoke.StackPopInteger() != 0;
        }

        /// <summary>
        ///   Make oObjectToChangeFaction join the faction of oMemberOfFactionToJoin.
        ///   NB. ** This will only work for two NPCs **
        /// </summary>
        public static void ChangeFaction(uint oObjectToChangeFaction, uint oMemberOfFactionToJoin)
        {
            NWNXPInvoke.StackPushObject(oMemberOfFactionToJoin);
            NWNXPInvoke.StackPushObject(oObjectToChangeFaction);
            NWNXPInvoke.CallBuiltIn(173);
        }

    }
}