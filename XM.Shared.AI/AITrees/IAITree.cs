using System;

namespace XM.AI.AITrees
{
    internal interface IAITree
    {
        void Update(DateTime now);
        void AddFriendly(uint creature);
        void RemoveFriendly(uint creature);
        bool ToggleAI();
    }
}
