using System;
using XM.AI.BehaviorTree;
using XM.AI.Context;
using XM.AI.Enmity;
using XM.Progression.Stat;

namespace XM.AI.AITrees
{
    internal abstract class AITreeBase: IAITree
    {
        protected CreatureAIContext Context { get; }
        public IBehavior<CreatureAIContext> Tree { get; }
        public void Update(DateTime now)
        {
            Tree.Tick(Context);
        }

        public void AddFriendly(uint creature)
        {
            if (GetIsEnemy(Context.Creature, creature))
                return;

            if (Context.Creature == creature)
                return;

            Context.NearbyFriendlies.Add(creature);
        }

        public void RemoveFriendly(uint creature)
        {
            Context.NearbyFriendlies.Remove(creature);
        }

        public bool ToggleAI()
        {
            Context.IsAIEnabled = !Context.IsAIEnabled;
            return Context.IsAIEnabled;
        }

        protected AITreeBase(
            uint creature, 
            AIService ai, 
            EnmityService enmity,
            StatService stat)
        {
            Context = new CreatureAIContext(creature, ai, enmity, stat);
            Tree = CreateTree();
        }

        protected abstract IBehavior<CreatureAIContext> CreateTree();

    }
}
