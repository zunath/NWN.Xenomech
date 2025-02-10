using System;
using System.Collections.Generic;
using XM.Progression.Ability;
using XM.Shared.API.Constants;

namespace XM.AI
{
    internal interface IAIContext
    {
        uint Creature { get; }
        AIServiceCollection Services { get; }
        void Update(DateTime now);
        void AddFriendly(uint creature);
        void RemoveFriendly(uint creature);
        bool ToggleAI();
        HashSet<FeatType> GetFeatsByType(AbilityClassificationType classification, AITargetType targetType);
    }
}
