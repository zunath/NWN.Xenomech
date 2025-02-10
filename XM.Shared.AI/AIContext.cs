using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.API;
using XM.AI.Scorer;
using XM.Progression.Ability;
using XM.Shared.API.Constants;

namespace XM.AI
{
    internal class AIContext: IAIContext
    {
        public bool IsAIEnabled { get; private set; }
        public AIServiceCollection Services { get; }
        public uint Creature { get; }
        private readonly HashSet<uint> _nearbyFriendlies = new();
        private readonly IAIScorer _scorer;
        private readonly CachedCreatureFeats _abilitiesByClassification;
        public Location HomeLocation { get; }
        public AIFlag AIFlags { get; }

        public AIContext(
            uint creature,
            AIFlag aiFlags,
            CachedCreatureFeats abilitiesByClassification,
            AIServiceCollection services)
        {
            IsAIEnabled = true;
            Creature = creature;
            AIFlags = aiFlags;
            _abilitiesByClassification = abilitiesByClassification;
            _scorer = new StandardScorer(this);
            Services = services;
            HomeLocation = GetLocation(creature);
        }

        public void Update(DateTime now)
        {
            _scorer.Update();
        }

        public HashSet<uint> GetNearbyFriendlies()
        {
            return _nearbyFriendlies.ToHashSet();
        }

        public void AddFriendly(uint creature)
        {
            if (GetIsEnemy(Creature, creature))
                return;

            if (Creature == creature)
                return;

            _nearbyFriendlies.Add(creature);
        }

        public void RemoveFriendly(uint creature)
        {
            _nearbyFriendlies.Remove(creature);
        }

        public bool ToggleAI()
        {
            IsAIEnabled = !IsAIEnabled;
            return IsAIEnabled;
        }

        public HashSet<FeatType> GetFeatsByType(AbilityClassificationType classification, AITargetType targetType)
        {
            if (!_abilitiesByClassification.ContainsKey(classification))
                return new HashSet<FeatType>();

            if (!_abilitiesByClassification[classification].ContainsKey(targetType))
                return new HashSet<FeatType>();

            return _abilitiesByClassification[classification][targetType];
        }

    }
}
