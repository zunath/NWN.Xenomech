namespace XM.AI.BehaviorTree.Composites
{
    public sealed class RandomSequence<TContext> : Sequence<TContext>
    {
        private readonly IRandomProvider _randomProvider;

        public RandomSequence(IBehavior<TContext>[] children, IRandomProvider randomProvider = null)
            : this("RandomSequence", children, randomProvider)
        {
        }

        public RandomSequence(string name, IBehavior<TContext>[] children, IRandomProvider randomProvider = null) : base(name, children)
        {
            _randomProvider = randomProvider ?? RandomProvider.Default;
            _shuffledChildren = Children.Shuffle(_randomProvider);
        }

        private IBehavior<TContext>[] _shuffledChildren;

        protected override IBehavior<TContext> GetChild(int index)
        {
            return _shuffledChildren[index];
        }

        protected override void DoReset(BehaviorStatus status)
        {
            _shuffledChildren = Children.Shuffle(_randomProvider);
            base.DoReset(status);
        }
    }
}