namespace XM.AI.BehaviorTree.Decorators
{
    public sealed class Succeeder<TContext> : DecoratorBehavior<TContext>
    {
        public Succeeder(IBehavior<TContext> child) : this("Succeeder", child)
        {
        }

        public Succeeder(string name, IBehavior<TContext> child) : base(name, child)
        {
        }

        protected override BehaviorStatus Update(TContext context)
        {
            var childStatus = Child.Tick(context);

            if (childStatus == BehaviorStatus.Succeeded || childStatus == BehaviorStatus.Failed)
            {
                return BehaviorStatus.Succeeded;
            }

            return childStatus;
        }
    }
}
