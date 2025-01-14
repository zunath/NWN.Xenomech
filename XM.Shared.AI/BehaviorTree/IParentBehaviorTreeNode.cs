namespace XM.AI.BehaviorTree
{
    /// <summary>
    /// Interface for behaviour tree nodes.
    /// </summary>
    public interface IParentBehaviorTreeNode : IBehaviorTreeNode
    {
        /// <summary>
        /// Add a child to the parent node.
        /// </summary>
        void AddChild(IBehaviorTreeNode child);
    }
}
