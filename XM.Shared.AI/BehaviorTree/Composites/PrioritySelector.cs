namespace XM.AI.BehaviorTree.Composites
{
    public sealed class PrioritySelector<TContext> : CompositeBehavior<TContext>
    {
        public PrioritySelector(IBehavior<TContext>[] children) : this("PrioritySelector", children)
        {
        }

        public PrioritySelector(string name, IBehavior<TContext>[] children) : base(name, children)
        {
        }

        protected override BehaviorStatus Update(TContext context)
        {
            for (var i = 0; i < Children.Length; i++)
            {
                var childStatus = Children[i].Tick(context);

                if (childStatus != BehaviorStatus.Failed)
                {
                    for (var j = i+1; j < Children.Length; j++)
                    {
                        Children[j].Reset();
                    }

                    return childStatus;
                }
            }

            return BehaviorStatus.Failed;
        }
    }
}
