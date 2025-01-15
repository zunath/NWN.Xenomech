namespace XM.AI.BehaviorTree.Decorators
{
    public sealed class Inverter<TContext> : DecoratorBehavior<TContext>
    {
        public Inverter(IBehavior<TContext> child) : this("Inverter", child)
        {
        }

        public Inverter(string name, IBehavior<TContext> child) : base(name, child)
        {
        }

        protected override BehaviorStatus Update(TContext context)
        {
            var childStatus = Child.Tick(context);

            if (childStatus == BehaviorStatus.Failed)
            {
                return BehaviorStatus.Succeeded;
            }

            return childStatus == BehaviorStatus.Succeeded ? BehaviorStatus.Failed : childStatus;
        }
    }
}
