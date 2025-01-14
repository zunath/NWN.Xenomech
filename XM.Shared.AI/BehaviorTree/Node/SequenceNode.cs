using System.Collections.Generic;

namespace XM.AI.BehaviorTree.Node
{
    /// <summary>
    /// Runs child nodes in sequence, until one fails.
    /// </summary>
    public class SequenceNode : IParentBehaviorTreeNode
    {
        /// <summary>
        /// Name of the node.
        /// </summary>
        private string _name;

        /// <summary>
        /// List of child nodes.
        /// </summary>
        private readonly List<IBehaviorTreeNode> _children = new();

        public SequenceNode(string name)
        {
            _name = name;
        }

        public BehaviorTreeStatus Tick(TimeData time)
        {
            foreach (var child in _children)
            {
                var childStatus = child.Tick(time);
                if (childStatus != BehaviorTreeStatus.Success)
                {
                    return childStatus;
                }
            }

            return BehaviorTreeStatus.Success;
        }

        /// <summary>
        /// Add a child to the sequence.
        /// </summary>
        public void AddChild(IBehaviorTreeNode child)
        {
            _children.Add(child);
        }
    }
}
