using System;

namespace XM.AI.BehaviorTree
{
    public abstract class BaseBehavior<TContext> : IBehavior<TContext>
    {
        public string Name { get; }
        public BehaviorStatus Status { get; private set; } = BehaviorStatus.Ready;

        protected BaseBehavior(string name)
        {
            Name = name;
        }

        public BehaviorStatus Tick(TContext context)
        {
            if (Status == BehaviorStatus.Ready)
            {
                OnInitialize();
            }

            Status = Update(context);

            if (Status == BehaviorStatus.Ready)
            {
                throw new InvalidOperationException("Ready status should not be returned by Behaviour Update Method");
            }

            if (Status != BehaviorStatus.Running)
            {
                OnTerminate(Status);
            }

            return Status;
        }

        public void Reset()
        {
            if (Status == BehaviorStatus.Ready)
            {
                return;
            }

            DoReset(Status);
            Status = BehaviorStatus.Ready;
        }

        protected abstract BehaviorStatus Update(TContext context);

        protected virtual void OnTerminate(BehaviorStatus status)
        {
        }

        protected virtual void OnInitialize()
        {
        }

        protected virtual void DoReset(BehaviorStatus status)
        {
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}