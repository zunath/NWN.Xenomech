using System;

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
    }
}
