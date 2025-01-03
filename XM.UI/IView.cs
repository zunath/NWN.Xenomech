using XM.UI.Builder;

namespace XM.UI
{
    public interface IView
    {
        IViewModel CreateViewModel();
        NuiBuiltWindow Build();
    }
}
