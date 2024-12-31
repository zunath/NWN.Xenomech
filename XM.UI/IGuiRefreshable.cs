using XM.UI.Event;

namespace XM.UI
{
    internal interface IGuiRefreshable<in T>
        where T: IGuiRefreshEvent
    {
        void Refresh(T payload);
    }
}
