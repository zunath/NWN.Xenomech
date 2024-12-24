namespace NWN.Xenomech.Core.Async.Awaiters
{
    public interface IAwaitable
    {
        IAwaiter GetAwaiter();
    }
}
