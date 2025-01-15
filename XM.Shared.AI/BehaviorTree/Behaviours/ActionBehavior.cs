using System;

namespace XM.AI.BehaviorTree.Behaviours
{
    public sealed class ActionBehavior<TContext> : BaseBehavior<TContext>
    {
        private readonly Func<TContext, BehaviorStatus> _action;

        public ActionBehavior(string name, Func<TContext, BehaviorStatus> action) : base(name)
        {
            _action = action;
        }

        protected override BehaviorStatus Update(TContext context)
        {
            return _action(context);
        }
    }
}
