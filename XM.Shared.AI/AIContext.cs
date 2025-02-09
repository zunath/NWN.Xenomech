using System;
using System.Collections.Generic;
using System.Linq;
using XM.AI.Scorer;

namespace XM.AI
{
    internal class AIContext: IAIContext
    {
        public bool IsAIEnabled { get; private set; }
        public AIServiceCollection Services { get; }
        public uint Creature { get; }
        private readonly HashSet<uint> _nearbyFriendlies = new();
        private readonly IAIScorer _scorer;

        public AIContext(
            uint creature, 
            AIServiceCollection services)
        {
            Creature = creature;
            _scorer = new StandardScorer(this);
            Services = services;
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
    }
}
