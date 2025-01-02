using System;
using Anvil.API;
using XM.UI.Builder;

namespace XM.UI
{
    public interface IView
    {
        Type ViewModel { get; }
        NuiBuildResult Build();
    }
}
