using System.Collections.Generic;

namespace XM.AI.BehaviorTree.Node
{
    /// <summary>
    /// Runs childs nodes in parallel.
    /// </summary>
    public class ParallelNode : IParentBehaviorTreeNode
    {
        /// <summary>
        /// Name of the node.
        /// </summary>
        private string _name;

        /// <summary>
        /// List of child nodes.
        /// </summary>
        private readonly List<IBehaviorTreeNode> _children = new();

        /// <summary>
        /// Number of child failures required to terminate with failure.
        /// </summary>
        private readonly int _numRequiredToFail;

        /// <summary>
        /// Number of child successess require to terminate with success.
        /// </summary>
        private readonly int _numRequiredToSucceed;

        public ParallelNode(string name, int numRequiredToFail, int numRequiredToSucceed)
        {
            _name = name;
            _numRequiredToFail = numRequiredToFail;
            _numRequiredToSucceed = numRequiredToSucceed;
        }

        public BehaviorTreeStatus Tick(TimeData time)
        {
            var numChildrenSucceeded = 0;
            var numChildrenFailed = 0;

            foreach (var child in _children)
            {
                var childStatus = child.Tick(time);
                switch (childStatus)
                {
                    case BehaviorTreeStatus.Success: ++numChildrenSucceeded; break;
                    case BehaviorTreeStatus.Failure: ++numChildrenFailed; break;
                }
            }

            if (_numRequiredToSucceed > 0 && numChildrenSucceeded >= _numRequiredToSucceed)
            {
                return BehaviorTreeStatus.Success;
            }

            if (_numRequiredToFail > 0 && numChildrenFailed >= _numRequiredToFail)
            {
                return BehaviorTreeStatus.Failure;
            }

            return BehaviorTreeStatus.Running;
        }

        public void AddChild(IBehaviorTreeNode child)
        {
            _children.Add(child);
        }
    }
}
