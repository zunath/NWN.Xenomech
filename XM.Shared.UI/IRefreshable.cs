using XM.Shared.Core.EventManagement;

namespace XM.UI
{
    public interface IRefreshable<T>
        where T: IXMEvent
    {
        void Refresh(T @event);
    }
}
