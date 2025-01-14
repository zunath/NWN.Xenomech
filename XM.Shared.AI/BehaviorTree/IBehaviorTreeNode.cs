namespace XM.AI.BehaviorTree
{
    /// <summary>
    /// Interface for behaviour tree nodes.
    /// </summary>
    public interface IBehaviorTreeNode
    {
        /// <summary>
        /// Update the time of the behaviour tree.
        /// </summary>
        BehaviorTreeStatus Tick(TimeData time);
    }
}
