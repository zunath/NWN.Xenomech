namespace XM.AI.BehaviorTree.Behaviours
{
    public sealed class Wait<TContext> : BaseBehavior<TContext> where TContext : IClock
    {
        public readonly long WaitTimeInMilliseconds;
        private long? _initialTimestamp;

        public Wait(int waitTimeInMilliseconds) : this("Wait", waitTimeInMilliseconds)
        {

        }

        public Wait(string name, int waitTimeInMilliseconds) : base(name)
        {
            WaitTimeInMilliseconds = waitTimeInMilliseconds;
        }

        protected override BehaviorStatus Update(TContext context)
        {
            var currentTimeStamp = context.GetTimeStampInMilliseconds();

            if (_initialTimestamp == null)
            {
                _initialTimestamp = currentTimeStamp;
            }

            var elapsedMilliseconds = currentTimeStamp - _initialTimestamp;

            if (elapsedMilliseconds >= WaitTimeInMilliseconds)
            {
                return BehaviorStatus.Succeeded;
            }

            return BehaviorStatus.Running;
        }

        protected override void OnTerminate(BehaviorStatus status)
        {
            DoReset(status);
        }

        protected override void DoReset(BehaviorStatus status)
        {
            _initialTimestamp = null;
        }
    }
}
