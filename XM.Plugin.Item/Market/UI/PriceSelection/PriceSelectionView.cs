using Anvil.API;
using Anvil.Services;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;

namespace XM.Plugin.Item.Market.UI.PriceSelection
{
    [ServiceBinding(typeof(IView))]
    internal class PriceSelectionView: IView
    {
        private readonly NuiBuilder<PriceSelectionViewModel> _builder = new();

        public IViewModel CreateViewModel()
        {
            return new PriceSelectionViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
            {
                window
                    .IsResizable(true)
                    .IsCollapsible(WindowCollapsibleType.Disabled)
                    .InitialGeometry(0f, 0f, 400f, 240f)
                    .Title(LocaleString.ChangePrice)

                    .Root(col =>
                    {
                        col.AddRow(row =>
                        {
                            row.AddLabel(label =>
                            {
                                label
                                    .Height(26f)
                                    .HorizontalAlign(NuiHAlign.Center)
                                    .VerticalAlign(NuiVAlign.Top)
                                    .Label(model => model.ItemName);
                            });
                        });

                        col.AddRow(row =>
                        {
                            row.AddTextEdit(textEdit =>
                            {
                                textEdit.Value(model => model.Price);
                            });
                        });

                        col.AddRow(row =>
                        {
                            row.AddButton(button =>
                            {
                                button
                                    .Label(LocaleString.Save)
                                    .OnClick(model => model.OnClickSave());
                            });
                            row.AddButton(button =>
                            {
                                button
                                    .Label(LocaleString.Cancel)
                                    .OnClick(model => model.OnClickCancel());
                            });
                        });
                    });
            }).Build();
        }
    }
}
