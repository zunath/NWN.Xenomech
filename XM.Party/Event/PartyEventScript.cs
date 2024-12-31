namespace XM.Party.Event
{
    internal class PartyEventScript
    {
        // Associates
        public const string OnAddAssociateBefore = "asso_add_bef";
        public const string OnAddAssociateAfter = "asso_add_aft";
        public const string OnRemoveAssociateBefore = "asso_rem_bef";
        public const string OnRemoveAssociateAfter = "asso_rem_aft";

        // Party
        public const string OnPartyLeaveBefore = "pty_leave_bef";
        public const string OnPartyLeaveAfter = "pty_leave_aft";
        public const string OnPartyKickBefore = "pty_kick_bef";
        public const string OnPartyKickAfter = "pty_kick_aft";
        public const string OnPartyTransferLeadershipBefore = "pty_chgldr_bef";
        public const string OnPartyTransferLeadershipAfter = "pty_chgldr_aft";
        public const string OnPartyInviteBefore = "pty_invite_bef";
        public const string OnPartyInviteAfter = "pty_invite_aft";
        public const string OnPartyIgnoreInvitationBefore = "pty_ignore_bef";
        public const string OnPartyIgnoreInvitationAfter = "pty_ignore_aft";
        public const string OnPartyAcceptInvitationBefore = "pty_accept_bef";
        public const string OnPartyAcceptInvitationAfter = "pty_accept_aft";
        public const string OnPartyRejectInvitationBefore = "pty_reject_bef";
        public const string OnPartyRejectInvitationAfter = "pty_reject_aft";
        public const string OnPartyKickHenchmanBefore = "pty_kickhen_bef";
        public const string OnPartyKickHenchmanAfter = "pty_kickhen_aft";
    }
}
