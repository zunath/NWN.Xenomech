using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using NWN.Core.NWNX;
using XM.Shared.Core.EventManagement;
using XM.Shared.Core.Localization;

namespace XM.Shared.Core.Party
{
    [ServiceBinding(typeof(PartyService))]
    public class PartyService
    {
        private const int PartyMemberLimit = 6;
        private readonly Dictionary<Guid, PartyDetail> _parties = new();
        private readonly Dictionary<uint, Guid> _creatureToParty = new();

        private readonly XMEventService _event;

        public PartyService(XMEventService @event)
        {
            _event = @event;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _event.Subscribe<ModuleEvent.OnPlayerLeave>(OnModuleLeave);
            _event.Subscribe<NWNXEvent.OnPartyInviteBefore>(OnPartyInviteBefore);
            _event.Subscribe<NWNXEvent.OnPartyAcceptInvitationBefore>(OnPartyAcceptInvitationBefore);
            _event.Subscribe<NWNXEvent.OnAddAssociateBefore>(OnAddAssociateBefore);
            _event.Subscribe<NWNXEvent.OnRemoveAssociateBefore>(OnRemoveAssociateBefore);
            _event.Subscribe<NWNXEvent.OnPartyLeaveBefore>(OnPartyLeaveBefore);
            _event.Subscribe<NWNXEvent.OnPartyTransferLeadershipBefore>(OnPartyTransferLeadershipBefore);
            _event.Subscribe<NWNXEvent.OnPartyKickBefore>(OnPartyKickBefore);
        }

        private void OnModuleLeave(uint objectSelf)
        {
            var creature = GetExitingObject();
            RemoveCreatureFromParty(creature);
        }

        private void OnPartyKickBefore(uint kicker)
        {
            var member = StringToObject(EventsPlugin.GetEventData("KICKED"));
            RemoveCreatureFromParty(member);
        }

        private void OnPartyInviteBefore(uint inviter)
        {
            if (!_creatureToParty.ContainsKey(inviter))
                return;

            var partyId = _creatureToParty[inviter];
            var party = _parties[partyId];
            var members = party.Members.Count;

            if (members >= PartyMemberLimit)
            {
                SendMessageToPC(inviter, LocaleString.YourPartyIsFull.ToLocalizedString());
                EventsPlugin.SkipEvent();
            }
        }

        private void OnPartyAcceptInvitationBefore(uint objectSelf)
        {
            var creature = OBJECT_SELF;
            var requester = StringToObject(EventsPlugin.GetEventData("INVITED_BY"));

            AddToParty(requester, creature);
        }

        private void OnAddAssociateBefore(uint objectSelf)
        {
            var owner = OBJECT_SELF;
            var associate = StringToObject(EventsPlugin.GetEventData("ASSOCIATE_OBJECT_ID"));

            AddToParty(owner, associate);
        }

        private void OnRemoveAssociateBefore(uint objectSelf)
        {
            var associate = StringToObject(EventsPlugin.GetEventData("ASSOCIATE_OBJECT_ID"));
            RemoveCreatureFromParty(associate);
        }

        private void OnPartyLeaveBefore(uint objectSelf)
        {
            var creature = StringToObject(EventsPlugin.GetEventData("LEAVING"));
            RemoveCreatureFromParty(creature);
        }

        private void OnPartyTransferLeadershipBefore(uint objectSelf)
        {
            var creature = StringToObject(EventsPlugin.GetEventData("NEW_LEADER"));
            var partyId = _creatureToParty[creature];
            var party = _parties[partyId];
            party.Leader = creature;
        }

        private void AddToParty(uint requester, uint creature)
        {
            // This is a brand new party.
            // Add both the requester and the creature to the cache.
            // Mark the requester as the party leader.
            if (!_creatureToParty.ContainsKey(requester))
            {
                var partyId = Guid.NewGuid();

                _parties[partyId] = new PartyDetail
                {
                    Members =
                    {
                        requester,
                        creature
                    },
                    Leader = requester
                };

                _creatureToParty[creature] = partyId;
                _creatureToParty[requester] = partyId;
            }
            // This is an existing party.
            // Add the creature to the party cache.
            else
            {
                var partyId = _creatureToParty[requester];
                var party = _parties[partyId];

                if (GetIsPC(creature))
                {
                    if (party.Members.Count >= PartyMemberLimit)
                    {
                        SendMessageToPC(creature, LocaleString.ThatPartyIsFull.ToLocalizedString());
                        SendMessageToPC(requester, LocaleString.YourPartyIsFull.ToLocalizedString());
                        DelayCommand(0f, () => RemoveFromParty(creature));
                        return;
                    }

                    party.Members.Add(creature);
                }
                else
                {
                    party.Associates.Add(creature);
                }

                _creatureToParty[creature] = partyId;
            }
        }

        /// <summary>
        /// Removes a creature from a party.
        /// If this would lead to an empty party, or a party with one member, the party gets disbanded.
        /// Otherwise if the leader leaves, a new one is assigned.
        /// </summary>
        /// <param name="creature">The creature being removed from the party.</param>
        private void RemoveCreatureFromParty(uint creature)
        {
            if (!_creatureToParty.ContainsKey(creature)) return;

            var partyId = _creatureToParty[creature];
            var party = _parties[partyId];

            // Remove this creature from the caches.

            if (GetIsPC(creature))
            {
                party.Members.Remove(creature);
            }
            else
            {
                party.Associates.Remove(creature);
            }
            
            _creatureToParty.Remove(creature);

            // If there is now only one party member (or fewer)
            // Party needs to be disbanded and caches updated.
            if (party.Members.Count <= 1)
            {
                foreach (var member in party.Members)
                {
                    _creatureToParty.Remove(member);
                }

                _parties.Remove(partyId);
                return;
            }

            // The party is still valid but the creature who left was its leader. 
            // Swap leadership to the next person in the party list.
            var nextMember = party.Members.First();
            if (party.Leader == creature)
            {
                party.Leader = nextMember;
            }
        }

        /// <summary>
        /// Retrieves all of the members in a creature's party.
        /// </summary>
        /// <param name="creature">The creature to check.</param>
        /// <param name="includeAssociates">If true, include associates like beasts. If false, only include players.</param>
        /// <returns>A list of party members.</returns>
        private List<uint> GetAllPartyMembers(uint creature, bool includeAssociates)
        {
            // Creature isn't in a party. Simply return them in a list.
            if (!_creatureToParty.ContainsKey(creature))
            {
                return [creature];
            }

            var partyId = _creatureToParty[creature];
            var party = _parties[partyId];

            var members = new List<uint>();
            members.AddRange(party.Members);

            if (includeAssociates)
            {
                members.AddRange(party.Associates);
            }

            return members;
        }

        /// <summary>
        /// Retrieves all of the members in a creature's party who are within the specified range from creature.
        /// </summary>
        /// <param name="creature">The creature to check and use as a distance check.</param>
        /// <param name="distance">The amount of distance to use.</param>
        /// <param name="includeAssociates">If true, associates like beasts will be included. If false, only players will be included.</param>
        /// <returns>A list of party members within the specified distance.</returns>
        public List<uint> GetAllPartyMembersWithinRange(uint creature, float distance, bool includeAssociates = true)
        {
            if (distance <= 0.0f) 
                distance = 0.0f;
            var members = GetAllPartyMembers(creature, includeAssociates);

            var result = new List<uint>();
            foreach (var member in members)
            {
                // Not in the same area
                if (GetArea(member) != GetArea(creature)) continue;

                // This is the creature we're checking. They should be included.
                if (member == creature)
                {
                    result.Add(member);
                    continue;
                }

                // Distance is too great.
                if (GetDistanceBetween(member, creature) > distance) continue;

                result.Add(member);
            }

            return result;
        }

        /// <summary>
        /// Determines if a creature is in the party of another creature.
        /// </summary>
        /// <param name="creature">The creature whose party will be checked</param>
        /// <param name="toCheck">The creature to determine if is in party</param>
        /// <returns>true if in party, false otherwise</returns>
        public bool IsInParty(uint creature, uint toCheck)
        {
            var members = GetAllPartyMembers(creature, true);
            return members.Contains(toCheck);
        }

    }
}
