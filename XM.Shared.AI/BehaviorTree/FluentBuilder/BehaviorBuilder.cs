namespace XM.AI.BehaviorTree.FluentBuilder
{
    public abstract class BehaviorBuilder<TContext>
    {
        public abstract IBehavior<TContext> Build();
    }
}