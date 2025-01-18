namespace XM.AI.BehaviorTree.Decorators
{
    public sealed class Failer<TContext> : DecoratorBehavior<TContext>
    {
        public Failer(IBehavior<TContext> child) : this("Failer", child)
        {
        }

        public Failer(string name, IBehavior<TContext> child) : base(name, child)
        {
        }

        protected override BehaviorStatus Update(TContext context)
        {
            var childStatus = Child.Tick(context);

            if (childStatus == BehaviorStatus.Succeeded || childStatus == BehaviorStatus.Failed)
            {
                return BehaviorStatus.Failed;
            }

            return childStatus;
        }
    }
}
