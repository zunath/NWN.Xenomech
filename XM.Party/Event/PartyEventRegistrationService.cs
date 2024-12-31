using System.Collections.Generic;
using Anvil.Services;
using NWN.Core.NWNX;
using XM.Core.EventManagement;
using XM.Core.EventManagement.NWNXEvent;

namespace XM.Party.Event
{
    [ServiceBinding(typeof(PartyEventRegistrationService))]
    [ServiceBinding(typeof(INWNXOnModulePreload))]
    internal class PartyEventRegistrationService :
        EventRegistrationServiceBase,
        INWNXOnModulePreload
    {
        [Inject]
        public IList<IOnAddAssociateBefore> OnAddAssociateBeforeSubscriptions { get; set; }

        [ScriptHandler(PartyEventScript.OnAddAssociateBefore)]
        public void HandleOnAddAssociateBefore() => HandleEvent(OnAddAssociateBeforeSubscriptions,
            (subscription) => subscription.OnAddAssociateBefore());

        [Inject]
        public IList<IOnAddAssociateAfter> OnAddAssociateAfterSubscriptions { get; set; }

        [ScriptHandler(PartyEventScript.OnAddAssociateAfter)]
        public void HandleOnAddAssociateAfter() => HandleEvent(OnAddAssociateAfterSubscriptions,
            (subscription) => subscription.OnAddAssociateAfter());

        [Inject]
        public IList<IOnRemoveAssociateBefore> OnRemoveAssociateBeforeSubscriptions { get; set; }

        [ScriptHandler(PartyEventScript.OnRemoveAssociateBefore)]
        public void HandleOnRemoveAssociateBefore() => HandleEvent(OnRemoveAssociateBeforeSubscriptions,
            (subscription) => subscription.OnRemoveAssociateBefore());

        [Inject]
        public IList<IOnRemoveAssociateAfter> OnRemoveAssociateAfterSubscriptions { get; set; }

        [ScriptHandler(PartyEventScript.OnRemoveAssociateAfter)]
        public void HandleOnRemoveAssociateAfter() => HandleEvent(OnRemoveAssociateAfterSubscriptions,
            (subscription) => subscription.OnRemoveAssociateAfter());


        [Inject]
        public IList<IOnPartyLeaveBefore> OnPartyLeaveBeforeSubscriptions { get; set; }

        [ScriptHandler(PartyEventScript.OnPartyLeaveBefore)]
        public void HandleOnPartyLeaveBefore() => HandleEvent(OnPartyLeaveBeforeSubscriptions,
            (subscription) => subscription.OnPartyLeaveBefore());

        [Inject]
        public IList<IOnPartyLeaveAfter> OnPartyLeaveAfterSubscriptions { get; set; }

        [ScriptHandler(PartyEventScript.OnPartyLeaveAfter)]
        public void HandleOnPartyLeaveAfter() => HandleEvent(OnPartyLeaveAfterSubscriptions,
            (subscription) => subscription.OnPartyLeaveAfter());

        [Inject]
        public IList<IOnPartyKickBefore> OnPartyKickBeforeSubscriptions { get; set; }

        [ScriptHandler(PartyEventScript.OnPartyKickBefore)]
        public void HandleOnPartyKickBefore() => HandleEvent(OnPartyKickBeforeSubscriptions,
            (subscription) => subscription.OnPartyKickBefore());

        [Inject]
        public IList<IOnPartyKickAfter> OnPartyKickAfterSubscriptions { get; set; }

        [ScriptHandler(PartyEventScript.OnPartyKickAfter)]
        public void HandleOnPartyKickAfter() => HandleEvent(OnPartyKickAfterSubscriptions,
            (subscription) => subscription.OnPartyKickAfter());

        [Inject]
        public IList<IOnPartyTransferLeadershipBefore> OnPartyTransferLeadershipBeforeSubscriptions { get; set; }

        [ScriptHandler(PartyEventScript.OnPartyTransferLeadershipBefore)]
        public void HandleOnPartyTransferLeadershipBefore() => HandleEvent(OnPartyTransferLeadershipBeforeSubscriptions,
            (subscription) => subscription.OnPartyTransferLeadershipBefore());

        [Inject]
        public IList<IOnPartyTransferLeadershipAfter> OnPartyTransferLeadershipAfterSubscriptions { get; set; }

        [ScriptHandler(PartyEventScript.OnPartyTransferLeadershipAfter)]
        public void HandleOnPartyTransferLeadershipAfter() => HandleEvent(OnPartyTransferLeadershipAfterSubscriptions,
            (subscription) => subscription.OnPartyTransferLeadershipAfter());

        [Inject]
        public IList<IOnPartyInviteBefore> OnPartyInviteBeforeSubscriptions { get; set; }

        [ScriptHandler(PartyEventScript.OnPartyInviteBefore)]
        public void HandleOnPartyInviteBefore() => HandleEvent(OnPartyInviteBeforeSubscriptions,
            (subscription) => subscription.OnPartyInviteBefore());

        [Inject]
        public IList<IOnPartyInviteAfter> OnPartyInviteAfterSubscriptions { get; set; }

        [ScriptHandler(PartyEventScript.OnPartyInviteAfter)]
        public void HandleOnPartyInviteAfter() => HandleEvent(OnPartyInviteAfterSubscriptions,
            (subscription) => subscription.OnPartyInviteAfter());

        [Inject]
        public IList<IOnPartyIgnoreInvitationBefore> OnPartyIgnoreInvitationBeforeSubscriptions { get; set; }

        [ScriptHandler(PartyEventScript.OnPartyIgnoreInvitationBefore)]
        public void HandleOnPartyIgnoreInvitationBefore() => HandleEvent(OnPartyIgnoreInvitationBeforeSubscriptions,
            (subscription) => subscription.OnPartyIgnoreInvitationBefore());

        [Inject]
        public IList<IOnPartyIgnoreInvitationAfter> OnPartyIgnoreInvitationAfterSubscriptions { get; set; }

        [ScriptHandler(PartyEventScript.OnPartyIgnoreInvitationAfter)]
        public void HandleOnPartyIgnoreInvitationAfter() => HandleEvent(OnPartyIgnoreInvitationAfterSubscriptions,
            (subscription) => subscription.OnPartyIgnoreInvitationAfter());

        [Inject]
        public IList<IOnPartyAcceptInvitationBefore> OnPartyAcceptInvitationBeforeSubscriptions { get; set; }

        [ScriptHandler(PartyEventScript.OnPartyAcceptInvitationBefore)]
        public void HandleOnPartyAcceptInvitationBefore() => HandleEvent(OnPartyAcceptInvitationBeforeSubscriptions,
            (subscription) => subscription.OnPartyAcceptInvitationBefore());

        [Inject]
        public IList<IOnPartyAcceptInvitationAfter> OnPartyAcceptInvitationAfterSubscriptions { get; set; }

        [ScriptHandler(PartyEventScript.OnPartyAcceptInvitationAfter)]
        public void HandleOnPartyAcceptInvitationAfter() => HandleEvent(OnPartyAcceptInvitationAfterSubscriptions,
            (subscription) => subscription.OnPartyAcceptInvitationAfter());

        [Inject]
        public IList<IOnPartyRejectInvitationBefore> OnPartyRejectInvitationBeforeSubscriptions { get; set; }

        [ScriptHandler(PartyEventScript.OnPartyRejectInvitationBefore)]
        public void HandleOnPartyRejectInvitationBefore() => HandleEvent(OnPartyRejectInvitationBeforeSubscriptions,
            (subscription) => subscription.OnPartyRejectInvitationBefore());

        [Inject]
        public IList<IOnPartyRejectInvitationAfter> OnPartyRejectInvitationAfterSubscriptions { get; set; }

        [ScriptHandler(PartyEventScript.OnPartyRejectInvitationAfter)]
        public void HandleOnPartyRejectInvitationAfter() => HandleEvent(OnPartyRejectInvitationAfterSubscriptions,
            (subscription) => subscription.OnPartyRejectInvitationAfter());

        [Inject]
        public IList<IOnPartyKickHenchmanBefore> OnPartyKickHenchmanBeforeSubscriptions { get; set; }

        [ScriptHandler(PartyEventScript.OnPartyKickHenchmanBefore)]
        public void HandleOnPartyKickHenchmanBefore() => HandleEvent(OnPartyKickHenchmanBeforeSubscriptions,
            (subscription) => subscription.OnPartyKickHenchmanBefore());

        [Inject]
        public IList<IOnPartyKickHenchmanAfter> OnPartyKickHenchmanAfterSubscriptions { get; set; }

        [ScriptHandler(PartyEventScript.OnPartyKickHenchmanAfter)]
        public void HandleOnPartyKickHenchmanAfter() => HandleEvent(OnPartyKickHenchmanAfterSubscriptions,
            (subscription) => subscription.OnPartyKickHenchmanAfter());


        public void OnModulePreload()
        {
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ADD_ASSOCIATE_BEFORE, PartyEventScript.OnAddAssociateBefore);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_ADD_ASSOCIATE_AFTER, PartyEventScript.OnAddAssociateAfter);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_REMOVE_ASSOCIATE_BEFORE, PartyEventScript.OnRemoveAssociateBefore);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_REMOVE_ASSOCIATE_AFTER, PartyEventScript.OnRemoveAssociateAfter);

            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_LEAVE_BEFORE, PartyEventScript.OnPartyLeaveBefore);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_LEAVE_AFTER, PartyEventScript.OnPartyLeaveAfter);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_KICK_BEFORE, PartyEventScript.OnPartyKickBefore);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_KICK_AFTER, PartyEventScript.OnPartyKickAfter);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_TRANSFER_LEADERSHIP_BEFORE, PartyEventScript.OnPartyTransferLeadershipBefore);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_TRANSFER_LEADERSHIP_AFTER, PartyEventScript.OnPartyTransferLeadershipAfter);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_INVITE_BEFORE, PartyEventScript.OnPartyInviteBefore);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_INVITE_AFTER, PartyEventScript.OnPartyInviteAfter);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_IGNORE_INVITATION_BEFORE, PartyEventScript.OnPartyIgnoreInvitationBefore);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_IGNORE_INVITATION_AFTER, PartyEventScript.OnPartyIgnoreInvitationAfter);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_ACCEPT_INVITATION_BEFORE, PartyEventScript.OnPartyAcceptInvitationBefore);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_ACCEPT_INVITATION_AFTER, PartyEventScript.OnPartyAcceptInvitationAfter);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_REJECT_INVITATION_BEFORE, PartyEventScript.OnPartyRejectInvitationBefore);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_REJECT_INVITATION_AFTER, PartyEventScript.OnPartyRejectInvitationAfter);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_KICK_HENCHMAN_BEFORE, PartyEventScript.OnPartyKickHenchmanBefore);
            EventsPlugin.SubscribeEvent(EventsPlugin.NWNX_ON_PARTY_KICK_HENCHMAN_AFTER, PartyEventScript.OnPartyKickHenchmanAfter);

        }
    }
}