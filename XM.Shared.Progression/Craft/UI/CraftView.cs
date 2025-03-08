using System;
using Anvil.Services;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;

namespace XM.Progression.Craft.UI
{
    [ServiceBinding(typeof(CraftView))]
    internal class CraftView: IView
    {
        private readonly NuiBuilder<CraftViewModel> _builder = new();
        public IViewModel CreateViewModel()
        {
            return new CraftViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
            {
                window
                    .IsCollapsible(WindowCollapsibleType.UserCollapsible)
                    .IsResizable(true)
                    .IsTransparent(false)
                    .Title(model => model.Title)
                    .InitialGeometry(0, 0, 800, 400)
                    .Root(root =>
                    {

                    });

            }).Build();

        }
    }
}
