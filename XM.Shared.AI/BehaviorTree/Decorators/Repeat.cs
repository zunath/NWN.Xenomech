using System;

namespace XM.AI.BehaviorTree.Decorators
{
    public sealed class Repeat<TContext> : DecoratorBehavior<TContext>
    {
        public readonly int RepeatCount;
        public int Counter { get; private set; }

        public Repeat(IBehavior<TContext> child, int repeatCount) : this("Repeat", child, repeatCount)
        {
        }

        public Repeat(string name, IBehavior<TContext> child, int repeatCount) : base(name, child)
        {
            if (repeatCount < 1)
            {
                throw new ArgumentException("repeatCount must be at least one", nameof(repeatCount));
            }

            RepeatCount = repeatCount;
        }

        protected override BehaviorStatus Update(TContext context)
        {
            var childStatus = Child.Tick(context);

            if (childStatus == BehaviorStatus.Succeeded)
            {
                Counter++;

                if (Counter < RepeatCount)
                {
                    return BehaviorStatus.Running;
                }
            }

            return childStatus;
        }

        protected override void OnTerminate(BehaviorStatus status)
        {
            Counter = 0;
        }

        protected override void DoReset(BehaviorStatus status)
        {
            Counter = 0;
            base.DoReset(status);
        }
    }
}
