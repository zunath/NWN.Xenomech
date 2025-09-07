using Anvil.API;
using Anvil.Services;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;
using XM.UI.Builder.Layout;

namespace XM.Plugin.Mech.UI.CustomizeMech
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
                    .Title(LocaleString.CustomizeMechs)
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
            col.AddRow(row =>
            {
                row.AddLabel(label =>
                {
                    label.Label(LocaleString.Mechs)
                        .Height(25f)
                        .HorizontalAlign(NuiHAlign.Center);
                });
            });

            col.AddRow(row =>
            {
                row.AddList(list =>
                {
                    list.AddTemplateCell(cell =>
                    {
                        cell.AddRow(cellRow =>
                        {
                            cellRow.AddButtonSelect(button =>
                            {
                                button
                                    .IsSelected(model => model.MechToggles)
                                    .Label(model => model.MechNames)
                                    .TooltipText(model => model.MechNames)
                                    .OnClick(model => model.OnSelectMech());
                            });
                        });
                    });
                }, model => model.MechNames);
            });
        }

        private void BuildMechCustomizationPane(NuiColumnBuilder<CustomizeMechViewModel> col)
        {
            col.AddSpacer();
        }

        private void BuildMechSummaryPane(NuiColumnBuilder<CustomizeMechViewModel> col)
        {
            col.AddSpacer();
        }

    }
}
