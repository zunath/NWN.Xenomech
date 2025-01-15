namespace XM.AI.BehaviorTree.Decorators
{
    public sealed class RateLimiter<TContext> : DecoratorBehavior<TContext> where TContext : IClock
    {
        private long? _previousTimestamp;
        private BehaviorStatus _previousChildStatus;
        public readonly long IntervalInMilliseconds;

        public RateLimiter(IBehavior<TContext> child, int intervalInMilliseconds) : this("RateLimiter", child, intervalInMilliseconds)
        {
        }

        public RateLimiter(string name, IBehavior<TContext> child, int intervalInMilliseconds) : base(name, child)
        {
            IntervalInMilliseconds = intervalInMilliseconds;
        }

        protected override BehaviorStatus Update(TContext context)
        {
            var currentTimeStamp = context.GetTimeStampInMilliseconds();

            var elapsedMilliseconds = currentTimeStamp - _previousTimestamp;

            if (_previousTimestamp == null || elapsedMilliseconds >= IntervalInMilliseconds)
            {
                _previousChildStatus = Child.Tick(context);

                if (_previousChildStatus != BehaviorStatus.Running)
                {
                    _previousTimestamp = currentTimeStamp;
                }
            }

            return _previousChildStatus;
        }
    }
}
