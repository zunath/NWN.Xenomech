namespace XM.AI.BehaviorTree.Decorators
{
    public sealed class TimeLimit<TContext> : DecoratorBehavior<TContext> where TContext : IClock
    {
        private long? _initialTimestamp;
        public readonly long TimeLimitInMilliseconds;

        public TimeLimit(IBehavior<TContext> child, int timeLimitInMilliseconds) : this("TimeLimit", child, timeLimitInMilliseconds)
        {
        }

        public TimeLimit(string name, IBehavior<TContext> child, int timeLimitInMilliseconds) : base(name, child)
        {
            TimeLimitInMilliseconds = timeLimitInMilliseconds;
        }

        protected override BehaviorStatus Update(TContext context)
        {
            var currentTimeStamp = context.GetTimeStampInMilliseconds();

            if (_initialTimestamp == null)
            {
                _initialTimestamp = currentTimeStamp;
            }

            var elapsedMilliseconds = currentTimeStamp - _initialTimestamp;

            if (elapsedMilliseconds >= TimeLimitInMilliseconds)
            {
                return BehaviorStatus.Failed;
            }

            return Child.Tick(context);
        }

        protected override void OnTerminate(BehaviorStatus status)
        {
            _initialTimestamp = null;
        }

        protected override void DoReset(BehaviorStatus status)
        {
            _initialTimestamp = null;
            base.DoReset(status);
        }
    }
}
