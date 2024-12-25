
using NWN.Xenomech.Core.Interop;

namespace NWN.Xenomech.Core.API
{
    public partial class NWScript
    {
        /// <summary>
        /// Add a journal quest entry to oCreature.
        /// - szPlotID: the plot identifier used in the toolset's Journal Editor
        /// - nState: the state of the plot as seen in the toolset's Journal Editor
        /// - oCreature: the target creature
        /// - bAllPartyMembers: If TRUE, the entry will show up in the journal of everyone in the party
        /// - bAllPlayers: If TRUE, the entry will show up in the journal of everyone in the world
        /// - bAllowOverrideHigher: If TRUE, allows setting the state to a lower number than the current state
        /// </summary>
        public static void AddJournalQuestEntry(string szPlotID, int nState, uint oCreature,
            bool bAllPartyMembers = true, bool bAllPlayers = false, bool bAllowOverrideHigher = false)
        {
            NWNXPInvoke.StackPushInteger(bAllowOverrideHigher ? 1 : 0);
            NWNXPInvoke.StackPushInteger(bAllPlayers ? 1 : 0);
            NWNXPInvoke.StackPushInteger(bAllPartyMembers ? 1 : 0);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushInteger(nState);
            NWNXPInvoke.StackPushString(szPlotID);
            NWNXPInvoke.CallBuiltIn(367);
        }

        /// <summary>
        /// Remove a journal quest entry from oCreature.
        /// - szPlotID: the plot identifier used in the toolset's Journal Editor
        /// - oCreature: the target creature
        /// - bAllPartyMembers: If TRUE, the entry will be removed from the journal of everyone in the party
        /// - bAllPlayers: If TRUE, the entry will be removed from the journal of everyone in the world
        /// </summary>
        public static void RemoveJournalQuestEntry(string szPlotID, uint oCreature, bool bAllPartyMembers = true,
            bool bAllPlayers = false)
        {
            NWNXPInvoke.StackPushInteger(bAllPlayers ? 1 : 0);
            NWNXPInvoke.StackPushInteger(bAllPartyMembers ? 1 : 0);
            NWNXPInvoke.StackPushObject(oCreature);
            NWNXPInvoke.StackPushString(szPlotID);
            NWNXPInvoke.CallBuiltIn(368);
        }
    }
}
