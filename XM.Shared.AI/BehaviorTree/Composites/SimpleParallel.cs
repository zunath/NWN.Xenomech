using System;

namespace XM.AI.BehaviorTree.Composites
{
    public sealed class SimpleParallel<TContext> : CompositeBehavior<TContext>
    {
        private readonly IBehavior<TContext> _first;
        private readonly IBehavior<TContext> _second;
        private BehaviorStatus _firstStatus;
        private BehaviorStatus _secondStatus;
        private readonly Func<TContext, BehaviorStatus> _behave;
        public readonly SimpleParallelPolicy Policy;

        public SimpleParallel(SimpleParallelPolicy policy, IBehavior<TContext> first, IBehavior<TContext> second) : this("SimpleParallel", policy, first, second)
        {

        }

        public SimpleParallel(string name, SimpleParallelPolicy policy, IBehavior<TContext> first, IBehavior<TContext> second) : base(name, new[]{first, second})
        {
            Policy = policy;
            _first = first;
            _second = second;
            _behave = policy == SimpleParallelPolicy.BothMustSucceed ? (Func<TContext, BehaviorStatus>)BothMustSucceedBehaviour : OnlyOneMustSucceedBehaviour;
        }

        private BehaviorStatus OnlyOneMustSucceedBehaviour(TContext context)
        {
            if (_firstStatus == BehaviorStatus.Succeeded || _secondStatus == BehaviorStatus.Succeeded)
            {
                return BehaviorStatus.Succeeded;
            }

            if (_firstStatus == BehaviorStatus.Failed && _secondStatus == BehaviorStatus.Failed)
            {
                return BehaviorStatus.Failed;
            }

            return BehaviorStatus.Running;
        }

        private BehaviorStatus BothMustSucceedBehaviour(TContext context)
        {
            if (_firstStatus == BehaviorStatus.Succeeded && _secondStatus == BehaviorStatus.Succeeded)
            {
                return BehaviorStatus.Succeeded;
            }

            if (_firstStatus == BehaviorStatus.Failed || _secondStatus == BehaviorStatus.Failed)
            {
                return BehaviorStatus.Failed;
            }

            return BehaviorStatus.Running;
        }

        protected override BehaviorStatus Update(TContext context)
        {
            if (Status != BehaviorStatus.Running)
            {
                _firstStatus = _first.Tick(context);
                _secondStatus = _second.Tick(context);
            }
            else
            {
                if (_firstStatus == BehaviorStatus.Ready || _firstStatus == BehaviorStatus.Running)
                {
                    _firstStatus = _first.Tick(context);
                }

                if (_secondStatus == BehaviorStatus.Ready || _secondStatus == BehaviorStatus.Running)
                {
                    _secondStatus = _second.Tick(context);
                }
            }

            return _behave(context);
        }

        protected override void DoReset(BehaviorStatus status)
        {
            _firstStatus = BehaviorStatus.Ready;
            _secondStatus = BehaviorStatus.Ready;
            base.DoReset(status);
        }
    }
}
