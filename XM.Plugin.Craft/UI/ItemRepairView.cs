using Anvil.API;
using Anvil.Services;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;

namespace XM.Plugin.Craft.UI
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
                    .InitialGeometry(0, 0, 360, 400)
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
                                    .Height(128f)
                                    .OnClick(model => model.OnSelectItem());
                            });
                            row.AddSpacer();
                        });

                        // Repair amount selector
                        root.AddRow(row =>
                        {
                            row.AddSpacer();
                            row.AddLabel(label =>
                            {
                                label
                                    .HorizontalAlign(NuiHAlign.Right)
                                    .VerticalAlign(NuiVAlign.Middle)
                                    .Label(LocaleString.RepairAmount)
                                    .Width(110f)
                                    .Height(26f);
                            });
                            row.AddSlider(slider =>
                            {
                                slider
                                    .Value(model => model.SelectedRepairPoints)
                                    .Min(model => model.MinRepairPoints)
                                    .Max(model => model.MaxRepairPoints)
                                    .Step(1);
                            });
                            row.AddLabel(label =>
                            {
                                label
                                    .HorizontalAlign(NuiHAlign.Left)
                                    .VerticalAlign(NuiVAlign.Middle)
                                    .Label(model => model.SelectedRepairPoints)
                                    .Width(50f)
                                    .Height(26f);
                            });
                            row.AddSpacer();
                        });

                        // Price display
                        root.AddRow(row =>
                        {
                            row.AddSpacer();
                            row.AddLabel(label =>
                            {
                                label
                                    .HorizontalAlign(NuiHAlign.Center)
                                    .VerticalAlign(NuiVAlign.Middle)
                                    .Label(model => model.Price)
                                    .Height(26f);
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
