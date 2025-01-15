namespace XM.AI.BehaviorTree.Decorators
{
    public sealed class UntilFailed<TContext> : DecoratorBehavior<TContext>
    {
        public UntilFailed(IBehavior<TContext> child) : this("UntilFailed", child)
        {
        }

        public UntilFailed(string name, IBehavior<TContext> child) : base(name, child)
        {
        }

        protected override BehaviorStatus Update(TContext context)
        {
            var childStatus = Child.Tick(context);

            return childStatus == BehaviorStatus.Failed ? BehaviorStatus.Succeeded : BehaviorStatus.Running;
        }
    }
}
