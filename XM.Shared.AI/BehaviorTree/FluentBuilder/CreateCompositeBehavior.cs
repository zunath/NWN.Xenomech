namespace XM.AI.BehaviorTree.FluentBuilder
{
    public delegate IBehavior<TContext> CreateCompositeBehavior<TContext>(IBehavior<TContext>[] children);
}