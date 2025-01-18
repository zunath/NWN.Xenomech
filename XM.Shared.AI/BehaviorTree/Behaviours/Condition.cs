using System;

namespace XM.AI.BehaviorTree.Behaviours
{
    // ReSharper disable once ClassCanBeSealed.Global
    public class Condition<TContext> : BaseBehavior<TContext>
    {
        private readonly Func<TContext, bool> _predicate;

        public Condition(Func<TContext, bool> predicate) : this(null, predicate)
        {
        }

        public Condition(string name, Func<TContext, bool> predicate) : base(name ?? "Condition")
        {
            _predicate = predicate;
        }

        protected override BehaviorStatus Update(TContext context)
        {
            return _predicate(context) ? BehaviorStatus.Succeeded : BehaviorStatus.Failed;
        }
    }
}
