using System;

namespace XM.AI.BehaviorTree
{
    public sealed class Clock : IClock
    {
        public long GetTimeStampInMilliseconds()
        {
            return TimeSpan.FromTicks(DateTime.UtcNow.Ticks).Milliseconds;
        }
    }
}
