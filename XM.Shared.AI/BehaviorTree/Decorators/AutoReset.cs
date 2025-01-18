namespace XM.AI.BehaviorTree.Decorators
{
    public sealed class AutoReset<TContext> : DecoratorBehavior<TContext>
    {
        public AutoReset(IBehavior<TContext> child) : this("AutoReset", child)
        {
        }

        public AutoReset(string name, IBehavior<TContext> child) : base(name, child)
        {
        }

        protected override BehaviorStatus Update(TContext context)
        {
            return Child.Tick(context);
        }

        protected override void OnTerminate(BehaviorStatus status)
        {
            Child.Reset();
        }
    }
}
