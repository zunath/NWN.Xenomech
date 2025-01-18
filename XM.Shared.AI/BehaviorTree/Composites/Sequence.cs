namespace XM.AI.BehaviorTree.Composites
{
    public class Sequence<TContext> : CompositeBehavior<TContext>
    {
        private int _currentChildIndex;

        public Sequence(IBehavior<TContext>[] children) : this("Sequence", children)
        {
        }

        public Sequence(string name, IBehavior<TContext>[] children) : base(name, children)
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

                if (childStatus != BehaviorStatus.Succeeded)
                {
                    return childStatus;
                }

            } while (++_currentChildIndex < Children.Length);

            return BehaviorStatus.Succeeded;
        }

        protected override void DoReset(BehaviorStatus status)
        {
            _currentChildIndex = 0;
            base.DoReset(status);
        }
    }
}
