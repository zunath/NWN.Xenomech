namespace XM.AI.BehaviorTree.Decorators
{
    public sealed class UntilSuccess<TContext> : DecoratorBehavior<TContext>
    {
        public UntilSuccess(IBehavior<TContext> child) : this("UntilSuccess", child)
        {
        }

        public UntilSuccess(string name, IBehavior<TContext> child) : base(name, child)
        {
        }

        protected override BehaviorStatus Update(TContext context)
        {
            var childStatus = Child.Tick(context);

            return childStatus == BehaviorStatus.Succeeded ? BehaviorStatus.Succeeded : BehaviorStatus.Running;
        }
    }
}