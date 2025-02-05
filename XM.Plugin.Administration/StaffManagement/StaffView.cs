using Anvil.Services;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;

namespace XM.Plugin.Administration.StaffManagement
{
    [ServiceBinding(typeof(IView))]
    internal class StaffView: IView
    {
        private readonly NuiBuilder<StaffViewModel> _builder = new();

        public IViewModel CreateViewModel()
        {
            return new StaffViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
            {
                window
                    .IsResizable(true)
                    .IsCollapsible(WindowCollapsibleType.UserCollapsible)
                    .InitialGeometry(0f, 0f, 545f, 295f)
                    .Title(LocaleString.ManageStaff)

                    .Root(col =>
                    {

                    });
            }).Build();
        }
    }
}
