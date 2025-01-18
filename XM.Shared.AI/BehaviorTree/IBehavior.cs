using System;

namespace XM.AI.BehaviorTree
{
    public interface IBehavior<in TContext> : IDisposable
    {
        string Name { get; }
        BehaviorStatus Status { get; }
        BehaviorStatus Tick(TContext context);
        void Reset();
    }
}