using Anvil.API;
using System;
using XM.AI.BehaviorTree;
using XM.AI.Context;
using XM.AI.Enmity;
using XM.Progression.Stat;

namespace XM.AI.AITrees
{
    internal abstract class AITreeBase: IAITree
    {
        protected CreatureAIContext Creature { get; }
        public IBehavior<CreatureAIContext> Tree { get; }
        public void Update(DateTime now)
        {
            Tree.Tick(Creature);
        }

        protected AITreeBase(
            uint creature, 
            AIService ai, 
            EnmityService enmity,
            StatService stat)
        {
            Creature = new CreatureAIContext(creature, ai, enmity, stat);
            Tree = CreateTree();
        }

        protected abstract IBehavior<CreatureAIContext> CreateTree();

    }
}
