namespace XM.AI.BehaviorTree.FluentBuilder
{
    public sealed class LeafBehaviorBuilder<TContext> : BehaviorBuilder<TContext>
    {
        public CreateBehavior<TContext> Factory { get; set; }
        public override IBehavior<TContext> Build() => Factory();
    }
}