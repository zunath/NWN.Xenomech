namespace XM.AI.BehaviorTree.Decorators
{
    public abstract class DecoratorBehavior<TContext> : BaseBehavior<TContext>
    {
        public readonly IBehavior<TContext> Child;

        protected DecoratorBehavior(string name, IBehavior<TContext> child) : base(name)
        {
            Child = child;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Child.Dispose();
            }
        }

        protected override void DoReset(BehaviorStatus status)
        {
            Child.Reset();
        }
    }
}
