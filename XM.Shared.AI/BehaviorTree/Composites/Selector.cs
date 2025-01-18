namespace XM.AI.BehaviorTree.Composites
{
    public class Selector<TContext> : CompositeBehavior<TContext>
    {
        private int _currentChildIndex;

        public Selector(IBehavior<TContext>[] children) : this("Selector", children)
        {
        }

        public Selector(string name, IBehavior<TContext>[] children) : base(name, children)
        {
        }

        protected virtual IBehavior<TContext> GetChild(int index)
        {
            return Children[index];
        }

        protected override BehaviorStatus Update(TContext context)
        {
            do
            {
                var childStatus = GetChild(_currentChildIndex).Tick(context);

                if (childStatus != BehaviorStatus.Failed)
                {
                    return childStatus;
                }

            } while (++_currentChildIndex < Children.Length);

            return BehaviorStatus.Failed;
        }

        protected override void DoReset(BehaviorStatus status)
        {
            _currentChildIndex = 0;
            base.DoReset(status);
        }
    }
}
