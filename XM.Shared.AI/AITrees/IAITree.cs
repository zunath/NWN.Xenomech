using XM.AI.BehaviorTree;

namespace XM.AI.AITrees
{
    internal interface IAITree
    {
        IBehaviorTreeNode Tree { get; }
        void Update(float deltaTime);
    }
}
