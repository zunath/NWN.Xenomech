using XM.AI.BehaviorTree;

namespace XM.AI.AITrees
{
    internal abstract class AITreeBase: IAITree
    {
        public abstract IBehaviorTreeNode Tree { get; }

        protected uint Creature { get; }

        protected AITreeBase(uint creature)
        {
            Creature = creature;
        }

        public void Update(float deltaTime)
        {
            Tree.Tick(new TimeData(deltaTime));
        }
    }
}
