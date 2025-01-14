using System;

namespace XM.AI.BehaviorTree.Node
{
    /// <summary>
    /// Decorator node that inverts the success/failure of its child.
    /// </summary>
    public class InverterNode : IParentBehaviorTreeNode
    {
        /// <summary>
        /// Name of the node.
        /// </summary>
        private string _name;

        /// <summary>
        /// The child to be inverted.
        /// </summary>
        private IBehaviorTreeNode _childNode;

        public InverterNode(string name)
        {
            _name = name;
        }

        public BehaviorTreeStatus Tick(TimeData time)
        {
            if (_childNode == null)
            {
                throw new ApplicationException("InverterNode must have a child node!");
            }

            var result = _childNode.Tick(time);
            if (result == BehaviorTreeStatus.Failure)
            {
                return BehaviorTreeStatus.Success;
            }
            else if (result == BehaviorTreeStatus.Success)
            {
                return BehaviorTreeStatus.Failure;
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// Add a child to the parent node.
        /// </summary>
        public void AddChild(IBehaviorTreeNode child)
        {
            if (_childNode != null)
            {
                throw new ApplicationException("Can't add more than a single child to InverterNode!");
            }

            _childNode = child;
        }
    }
}
