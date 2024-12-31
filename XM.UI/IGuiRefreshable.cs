using XM.UI.Event;

namespace XM.UI
{
    public interface IGuiRefreshable<in T>
        where T: IGuiRefreshEvent
    {
        void Refresh(T payload);
    }
}
