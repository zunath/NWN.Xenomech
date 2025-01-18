using System;
using System.Linq;

namespace XM.AI.BehaviorTree.Composites
{
    public abstract class CompositeBehavior<TContext> : BaseBehavior<TContext>
    {
        public IBehavior<TContext>[] Children { get; }

        protected CompositeBehavior(string name, IBehavior<TContext>[] children) : base(name)
        {
            if (children == null)
            {
                throw new ArgumentNullException(nameof(children));
            }

            if (children.Length == 0)
            {
                throw new ArgumentException("Must have at least one child", nameof(children));
            }

            if (children.Any(x => x == null))
            {
                throw new ArgumentException("Children cannot contain null elements", nameof(children));
            }

            Children = children;
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            foreach (var child in Children)
            {
                child.Dispose();
            }
        }

        protected override void OnTerminate(BehaviorStatus status)
        {
            DoReset(status);
        }

        protected override void DoReset(BehaviorStatus status)
        {
            ResetChildren();
        }

        private void ResetChildren()
        {
            foreach (var child in Children)
            {
                child.Reset();
            }
        }
    }
}
