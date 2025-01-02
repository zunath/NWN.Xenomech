using Anvil.API;
using XM.UI.Builder;

namespace XM.UI
{
    public interface IView
    {
        IViewModel CreateViewModel();
        NuiBuildResult Build();
    }
}
