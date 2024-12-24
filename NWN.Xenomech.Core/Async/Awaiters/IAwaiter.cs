using System.Runtime.CompilerServices;

namespace NWN.Xenomech.Core.Async.Awaiters
{
    public interface IAwaiter : INotifyCompletion
    {
        public bool IsCompleted { get; }

        public void GetResult() { }
    }
}
