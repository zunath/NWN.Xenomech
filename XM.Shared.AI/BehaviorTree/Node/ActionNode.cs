using System;

namespace XM.AI.BehaviorTree.Node
{
    /// <summary>
    /// A behaviour tree leaf node for running an action.
    /// </summary>
    public class ActionNode : IBehaviorTreeNode
    {
        /// <summary>
        /// The name of the node.
        /// </summary>
        private string _name;

        /// <summary>
        /// Function to invoke for the action.
        /// </summary>
        private readonly Func<TimeData, BehaviorTreeStatus> _fn;


        public ActionNode(string name, Func<TimeData, BehaviorTreeStatus> fn)
        {
            _name = name;
            _fn = fn;
        }

        public BehaviorTreeStatus Tick(TimeData time)
        {
            return _fn(time);
        }
    }
}
