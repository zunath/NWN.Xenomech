using System;
using System.Collections.Generic;

namespace XM.AI.BehaviorTree.FluentBuilder
{
    public static class FluentBuilder
    {
        public static FluentBuilder<T> Create<T>()
        {
            return new FluentBuilder<T>();
        }
    }

    public sealed class FluentBuilder<TContext>
    {
        private readonly Stack<CompositeBehaviorBuilder<TContext>> _parentNodeStack = new();
        private BehaviorBuilder<TContext> _currentBehaviorBuilder;


        public FluentBuilder<TContext> End()
        {
            _currentBehaviorBuilder = _parentNodeStack.Pop();
            return this;
        }

        public FluentBuilder<TContext> PushComposite(CreateCompositeBehavior<TContext> behaviorFactory)
        {
            var newNode = new CompositeBehaviorBuilder<TContext>
            {
                Factory = behaviorFactory
            };

            if (_parentNodeStack.Count > 0)
            {
                var parentNode = _parentNodeStack.Peek();
                parentNode.Children.Add(newNode);
            }

            _parentNodeStack.Push(newNode);

            return this;
        }

        public FluentBuilder<TContext> PushLeaf(CreateBehavior<TContext> behaviorFactory)
        {
            var parentNode = _parentNodeStack.Peek();
            parentNode.Children.Add(new LeafBehaviorBuilder<TContext>{Factory = behaviorFactory});

            return this;
        }

        public IBehavior<TContext> Build()
        {
            if (_currentBehaviorBuilder == null)
            {
                throw new InvalidOperationException("Tree must contain at least one node");
            }

            return _currentBehaviorBuilder.Build();
        }
    }
}
