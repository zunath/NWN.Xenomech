using Anvil.API;
using System;
using XM.AI.BehaviorTree;
using XM.AI.Context;

namespace XM.AI.AITrees
{
    internal abstract class AITreeBase: IAITree
    {
        private const int FrequencySeconds = 2;
        protected CreatureAIContext Creature { get; }

        private DateTime _lastTick = DateTime.UtcNow;

        public IBehavior<CreatureAIContext> Tree { get; }
        public void Update(DateTime now)
        {
            if (now - _lastTick > TimeSpan.FromSeconds(FrequencySeconds))
            {
                Tree.Tick(Creature);
                _lastTick = now;
            }
        }

        protected AITreeBase(uint creature, AIService ai)
        {
            Creature = new CreatureAIContext(creature, ai);
            Tree = CreateTree();
        }

        protected abstract IBehavior<CreatureAIContext> CreateTree();

    }
}
