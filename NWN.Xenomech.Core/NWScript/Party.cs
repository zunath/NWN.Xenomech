using NWN.Xenomech.Core.Interop;

namespace NWN.Xenomech.Core.NWScript
{
    public partial class NWScript
    {
        /// <summary>
        /// Add oPC to oPartyLeader's party. This will only work on two PCs.
        /// - oPC: player to add to a party
        /// - oPartyLeader: player already in the party
        /// </summary>
        public static void AddToParty(uint oPC, uint oPartyLeader)
        {
            NWNXPInvoke.StackPushObject(oPartyLeader);
            NWNXPInvoke.StackPushObject(oPC);
            NWNXPInvoke.CallBuiltIn(572);
        }

        /// <summary>
        /// Remove oPC from their current party. This will only work on a PC.
        /// - oPC: removes this player from whatever party they're currently in.
        /// </summary>
        public static void RemoveFromParty(uint oPC)
        {
            NWNXPInvoke.StackPushObject(oPC);
            NWNXPInvoke.CallBuiltIn(573);
        }

        /// <summary>
        /// Make the corresponding panel button on the player's client start or stop flashing.
        /// - oPlayer: the player controlling the client
        /// - nButton: PANEL_BUTTON_* constant indicating the button
        /// - nEnableFlash: TRUE to start flashing; FALSE to stop flashing
        /// </summary>
        public static void SetPanelButtonFlash(uint oPlayer, int nButton, int nEnableFlash)
        {
            NWNXPInvoke.StackPushInteger(nEnableFlash);
            NWNXPInvoke.StackPushInteger(nButton);
            NWNXPInvoke.StackPushObject(oPlayer);
            NWNXPInvoke.CallBuiltIn(521);
        }
    }
}