using Anvil.Services;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;
using XM.UI.Builder.Layout;

namespace XM.Plugin.Mech.UI
{
    [ServiceBinding(typeof(IView))]
    internal class CustomizeMechView: IView
    {
        private readonly NuiBuilder<CustomizeMechViewModel> _builder = new();
        public IViewModel CreateViewModel()
        {
            return new CustomizeMechViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
            {
                window.IsResizable(true)
                    .IsCollapsible(WindowCollapsibleType.UserCollapsible)
                    .InitialGeometry(0, 0, 545f, 295f)
                    .Title(LocaleString.Quests)
                    .Root(root =>
                    {
                        root.AddRow(row =>
                        {
                            row.AddColumn(BuildMechList);
                            row.AddColumn(BuildMechCustomizationPane);
                            row.AddColumn(BuildMechSummaryPane);
                        });
                    });

            }).Build();
        }

        private void BuildMechList(NuiColumnBuilder<CustomizeMechViewModel> col)
        {

        }

        private void BuildMechCustomizationPane(NuiColumnBuilder<CustomizeMechViewModel> col)
        {

        }

        private void BuildMechSummaryPane(NuiColumnBuilder<CustomizeMechViewModel> col)
        {

        }

    }
}
