﻿using System;
using System.Collections.Generic;
using XM.AI.BehaviorTree.Node;

namespace XM.AI.BehaviorTree
{
    /// <summary>
    /// Fluent API for building a behaviour tree.
    /// </summary>
    public class BehaviorTreeBuilder
    {
        /// <summary>
        /// Last node created.
        /// </summary>
        private IBehaviorTreeNode _curNode = null;

        /// <summary>
        /// Stack node nodes that we are build via the fluent API.
        /// </summary>
        private readonly Stack<IParentBehaviorTreeNode> _parentNodeStack = new();

        /// <summary>
        /// Create an action node.
        /// </summary>
        public BehaviorTreeBuilder Do(string name, Func<TimeData, BehaviorTreeStatus> fn)
        {
            if (_parentNodeStack.Count <= 0)
            {
                throw new ApplicationException("Can't create an unnested ActionNode, it must be a leaf node.");
            }

            var actionNode = new ActionNode(name, fn);
            _parentNodeStack.Peek().AddChild(actionNode);
            return this;
        }

        /// <summary>
        /// Like an action node... but the function can return true/false and is mapped to success/failure.
        /// </summary>
        public BehaviorTreeBuilder Condition(string name, Func<TimeData, bool> fn)
        {
            return Do(name, t => fn(t) ? BehaviorTreeStatus.Success : BehaviorTreeStatus.Failure);
        }

        /// <summary>
        /// Create an inverter node that inverts the success/failure of its children.
        /// </summary>
        public BehaviorTreeBuilder Inverter(string name)
        {
            var inverterNode = new InverterNode(name);

            if (_parentNodeStack.Count > 0)
            {
                _parentNodeStack.Peek().AddChild(inverterNode);
            }

            _parentNodeStack.Push(inverterNode);
            return this;
        }

        /// <summary>
        /// Create a sequence node.
        /// </summary>
        public BehaviorTreeBuilder Sequence(string name)
        {
            var sequenceNode = new SequenceNode(name);

            if (_parentNodeStack.Count > 0)
            {
                _parentNodeStack.Peek().AddChild(sequenceNode);
            }

            _parentNodeStack.Push(sequenceNode);
            return this;
        }

        /// <summary>
        /// Create a parallel node.
        /// </summary>
        public BehaviorTreeBuilder Parallel(string name, int numRequiredToFail, int numRequiredToSucceed)
        {
            var parallelNode = new ParallelNode(name, numRequiredToFail, numRequiredToSucceed);

            if (_parentNodeStack.Count > 0)
            {
                _parentNodeStack.Peek().AddChild(parallelNode);
            }

            _parentNodeStack.Push(parallelNode);
            return this;
        }

        /// <summary>
        /// Create a selector node.
        /// </summary>
        public BehaviorTreeBuilder Selector(string name)
        {
            var selectorNode = new SelectorNode(name);

            if (_parentNodeStack.Count > 0)
            {
                _parentNodeStack.Peek().AddChild(selectorNode);
            }

            _parentNodeStack.Push(selectorNode);
            return this;
        }

        /// <summary>
        /// Splice a sub tree into the parent tree.
        /// </summary>
        public BehaviorTreeBuilder Splice(IBehaviorTreeNode subTree)
        {
            if (subTree == null)
            {
                throw new ArgumentNullException(nameof(subTree));
            }

            if (_parentNodeStack.Count <= 0)
            {
                throw new ApplicationException("Can't splice an unnested sub-tree, there must be a parent-tree.");
            }

            _parentNodeStack.Peek().AddChild(subTree);
            return this;
        }

        /// <summary>
        /// Build the actual tree.
        /// </summary>
        public IBehaviorTreeNode Build()
        {
            if (_curNode == null)
            {
                throw new ApplicationException("Can't create a behaviour tree with zero nodes");
            }
            return _curNode;
        }

        /// <summary>
        /// Ends a sequence of children.
        /// </summary>
        public BehaviorTreeBuilder End()
        {
            _curNode = _parentNodeStack.Pop();
            return this;
        }
    }
}
