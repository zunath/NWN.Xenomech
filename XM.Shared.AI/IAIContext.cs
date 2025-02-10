using Anvil.API;
using System;
using System.Collections.Generic;
using XM.Progression.Ability;
using XM.Shared.API.Constants;

namespace XM.AI
{
    internal interface IAIContext
    {
        uint Creature { get; }
        bool IsAIEnabled { get; }
        AIServiceCollection Services { get; }
        void Update(DateTime now);
        void AddFriendly(uint creature);
        void RemoveFriendly(uint creature);
        bool ToggleAI();
        public Location HomeLocation { get; }
        public AIFlag AIFlags { get; }
        HashSet<FeatType> GetFeatsByType(AbilityClassificationType classification, AITargetType targetType);
    }
}
