using System;
using System.Threading;
using System.Threading.Tasks;

namespace XM.AI.BehaviorTree
{
    public sealed class BehaviorTreeRunner<TContext> : IDisposable where TContext : class
    {
        private readonly int _intervalInMilliseconds;
        private readonly TContext _context;
        private readonly IBehavior<TContext> _behaviorTree;
        private CancellationTokenSource _tokenSource;

        public BehaviorTreeRunner(IBehavior<TContext> behaviorTree, TContext context, int intervalInMilliseconds)
        {
            _intervalInMilliseconds = intervalInMilliseconds;
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _behaviorTree = behaviorTree ?? throw new ArgumentNullException(nameof(behaviorTree));
        }

        public Task<BehaviorStatus> RunToFailureOrSuccess()
        {
            return DoWork(status =>
                status == BehaviorStatus.Succeeded ||
                status == BehaviorStatus.Failed);
        }

        public Task<BehaviorStatus> RunUntilStopped()
        {
            return DoWork(status => false);
        }

        private async Task<BehaviorStatus> DoWork(Predicate<BehaviorStatus> shouldStop)
        {
            Stop();

            _tokenSource = new CancellationTokenSource();

            var status = await ExecuteCycle(_tokenSource.Token).ConfigureAwait(false);

            while (!shouldStop(status) && !_tokenSource.IsCancellationRequested)
            {
                status = await ExecuteCycle(_tokenSource.Token).ConfigureAwait(false);
            }

            return status;
        }

        public void Stop()
        {
            _tokenSource?.Cancel();
        }

        private async Task<BehaviorStatus> ExecuteCycle(CancellationToken token)
        {
            var behaviourStatus = _behaviorTree.Tick(_context);

            await Task.Delay(_intervalInMilliseconds, token).ConfigureAwait(false);

            return behaviourStatus;
        }

        public void Dispose()
        {
            _behaviorTree.Dispose();
        }
    }
}
