using System.Collections.Generic;

namespace XM.AI.BehaviorTree.Node
{
    /// <summary>
    /// Selects the first node that succeeds. Tries successive nodes until it finds one that doesn't fail.
    /// </summary>
    public class SelectorNode : IParentBehaviorTreeNode
    {
        /// <summary>
        /// The name of the node.
        /// </summary>
        private string _name;

        /// <summary>
        /// List of child nodes.
        /// </summary>
        private readonly List<IBehaviorTreeNode> _children = new();

        public SelectorNode(string name)
        {
            _name = name;
        }

        public BehaviorTreeStatus Tick(TimeData time)
        {
            foreach (var child in _children)
            {
                var childStatus = child.Tick(time);
                if (childStatus != BehaviorTreeStatus.Failure)
                {
                    return childStatus;
                }
            }

            return BehaviorTreeStatus.Failure;
        }

        /// <summary>
        /// Add a child node to the selector.
        /// </summary>
        public void AddChild(IBehaviorTreeNode child)
        {
            _children.Add(child);
        }
    }
}
