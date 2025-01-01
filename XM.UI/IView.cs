using Anvil.API;

namespace XM.UI
{
    public interface IView
    {
        IViewModel CreateViewModel(uint player);
        NuiWindow Build();
    }
}
