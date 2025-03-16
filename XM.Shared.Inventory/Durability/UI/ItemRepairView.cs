using Anvil.API;
using Anvil.Services;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;

namespace XM.Inventory.Durability.UI
{
    [ServiceBinding(typeof(IView))]
    internal class ItemRepairView: IView
    {
        private readonly NuiBuilder<ItemRepairViewModel> _builder = new();

        public IViewModel CreateViewModel()
        {
            return new ItemRepairViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
            {
                window
                    .IsCollapsible(WindowCollapsibleType.UserCollapsible)
                    .IsResizable(true)
                    .IsClosable(true)
                    .IsTransparent(false)
                    .Title(LocaleString.RepairItem)
                    .InitialGeometry(0, 0, 330, 330)
                    .Root(root =>
                    {
                        root.AddRow(row =>
                        {
                            row.AddSpacer();
                            row.AddLabel(label =>
                            {
                                label
                                    .HorizontalAlign(NuiHAlign.Center)
                                    .VerticalAlign(NuiVAlign.Middle)
                                    .Label(model => model.Name)
                                    .Height(26f);
                            });
                            row.AddSpacer();
                        });

                        root.AddRow(row =>
                        {
                            row.AddSpacer();
                            row.AddButtonImage(button =>
                            {
                                button
                                    .ResRef(model => model.ItemIconResref)
                                    .Width(64f)
                                    .Height(64f)
                                    .OnClick(model => model.OnSelectItem());
                            });
                            row.AddSpacer();
                        });

                        root.AddRow(row =>
                        {
                            row.AddSpacer();
                            row.AddButton(button =>
                            {
                                button
                                    .Label(model => model.RepairButtonText)
                                    .OnClick(model => model.OnRepairItem())
                                    .IsEnabled(model => model.IsRepairButtonEnabled);
                            });
                            row.AddSpacer();
                        });
                    });
            }).Build();
        }
    }
}
