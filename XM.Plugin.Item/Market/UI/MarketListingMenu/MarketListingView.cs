using Anvil.API;
using Anvil.Services;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;

namespace XM.Plugin.Item.Market.UI.MarketListingMenu
{
    [ServiceBinding(typeof(IView))]
    internal class MarketListingView : IView
    {
        private readonly NuiBuilder<MarketListingViewModel> _builder = new();

        public IViewModel CreateViewModel()
        {
            return new MarketListingViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
            {
                window
                    .IsResizable(true)
                    .IsCollapsible(WindowCollapsibleType.UserCollapsible)
                    .InitialGeometry(0, 0, 545f, 295.5f)
                    .Title(model => model.WindowTitle)
                    .Root(col =>
                    {
                        col.AddRow(row =>
                        {
                            row.AddTextEdit(textEdit =>
                            {
                                textEdit
                                    .Placeholder(LocaleString.ItemName)
                                    .Value(model => model.SearchText);
                            });

                            row.AddButton(button =>
                            {
                                button
                                    .Label(LocaleString.X)
                                    .Height(35f)
                                    .Width(35f)
                                    .OnClick(model => model.OnClickClear());
                            });

                            row.AddButton(button =>
                            {
                                button
                                    .Label(LocaleString.Search)
                                    .Height(35f)
                                    .OnClick(model => model.OnClickSearch());
                            });
                        });

                        col.AddRow(row =>
                        {
                            row.AddButton(button =>
                            {
                                button
                                    .Label(LocaleString.AddItem)
                                    .Height(35f)
                                    .OnClick(model => model.OnClickAddItem())
                                    .IsEnabled(model => model.IsAddItemEnabled);
                            });

                            row.AddButton(button =>
                            {
                                button
                                    .Label(model => model.ShopTill)
                                    .Height(35f)
                                    .OnClick(model => model.OnClickShopTill())
                                    .IsEnabled(model => model.IsShopTillEnabled);
                            });

                            row.AddLabel(label =>
                            {
                                label
                                    .Label(model => model.ListCount)
                                    .Height(35f)
                                    .HorizontalAlign(NuiHAlign.Left);
                            });
                        });

                        col.AddRow(row =>
                        {
                            row.AddList(template =>
                            {
                                template.RowHeight(40f);

                                template.AddTemplateCell(cell =>
                                {
                                    cell
                                        .Width(40f)
                                        .IsVariable(false)
                                        .AddGroup(group =>
                                        {
                                            group.SetLayout(layout =>
                                            {
                                                layout.AddImage(image =>
                                                {
                                                    image
                                                        .ResRef(model => model.ItemIconResrefs)
                                                        .HorizontalAlign(NuiHAlign.Center)
                                                        .VerticalAlign(NuiVAlign.Top)
                                                        .TooltipText(model => model.ItemNames);
                                                });
                                            });
                                        });

                                });

                                template.AddTemplateCell(cell =>
                                {
                                    cell.AddRow(row =>
                                    {
                                        row.AddText(text =>
                                        {
                                            text
                                                .Text(model => model.ItemNames)
                                                .TooltipText(model => model.ItemNames);
                                        });
                                    });
                                });

                                template.AddTemplateCell(cell =>
                                {
                                    cell.Width(120f);
                                    cell.IsVariable(false);

                                    cell.AddRow(row =>
                                    {
                                        row.AddButton(button =>
                                        {
                                            button
                                                .Label(model => model.ItemPriceNames)
                                                .OnClick(model => model.OnClickChangePrice());
                                        });
                                    });

                                });

                                template.AddTemplateCell(cell =>
                                {
                                    cell
                                        .Width(50f)
                                        .AddRow(row =>
                                        {
                                            row.AddCheck(check =>
                                            {
                                                check
                                                    .Selected(model => model.ItemListed)
                                                    .IsEnabled(model => model.ListingCheckboxEnabled)
                                                    .Label(LocaleString.ForSale);
                                            });

                                        });

                                });

                                template.AddTemplateCell(cell =>
                                {
                                    cell
                                        .Width(50f)
                                        .AddRow(row =>
                                        {
                                            row.AddButton(button =>
                                            {
                                                button
                                                    .Label(LocaleString.Remove)
                                                    .OnClick(model => model.OnClickRemove());
                                            });
                                        });
                                });
                            }, model => model.ItemNames);
                        });

                        col.AddRow(row =>
                        {
                            row.AddSpacer();

                            row.AddButton(button =>
                            {
                                button
                                    .Label(LocaleString.SaveChanges)
                                    .OnClick(model => model.OnClickSaveChanges());
                            });

                            row.AddSpacer();
                        });
                    });
            }).Build();
        }
    }
}
