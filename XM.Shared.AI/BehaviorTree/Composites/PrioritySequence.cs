namespace XM.AI.BehaviorTree.Composites
{
    public sealed class PrioritySequence<TContext> : CompositeBehavior<TContext>
    {
        public PrioritySequence(IBehavior<TContext>[] children) : this("PrioritySequence", children)
        {
        }

        public PrioritySequence(string name, IBehavior<TContext>[] children) : base(name, children)
        {
        }

        protected override BehaviorStatus Update(TContext context)
        {
            for (var i = 0; i < Children.Length; i++)
            {
                var childStatus = Children[i].Tick(context);

                if (childStatus != BehaviorStatus.Succeeded)
                {
                    for (var j = i + 1; j < Children.Length; j++)
                    {
                        Children[j].Reset();
                    }

                    return childStatus;
                }
            }

            return BehaviorStatus.Succeeded;
        }
    }
}
