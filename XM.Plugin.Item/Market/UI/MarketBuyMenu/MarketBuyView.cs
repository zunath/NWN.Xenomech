using Anvil.API;
using Anvil.Services;
using NLog.Layouts;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;

namespace XM.Plugin.Item.Market.UI.MarketBuyMenu
{
    [ServiceBinding(typeof(IView))]
    internal class MarketBuyView : IView
    {
        private readonly NuiBuilder<MarketBuyViewModel> _builder = new();

        public IViewModel CreateViewModel()
        {
            return new MarketBuyViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
            {
                window.IsCollapsible(WindowCollapsibleType.UserCollapsible)
                    .IsResizable(true)
                    .IsClosable(true)
                    .IsTransparent(false)
                    .Title(model => model.WindowTitle)
                    .InitialGeometry(0, 0, 1000f, 1000f)

                    .Root(col =>
                    {
                        col.AddRow(row =>
                        {
                            row.AddTextEdit(textEdit =>
                            {
                                textEdit
                                    .Placeholder(LocaleString.Search)
                                    .Value(model => model.SearchText);
                            });

                            row.AddButton(button =>
                            {
                                button
                                    .Label(LocaleString.X)
                                    .Height(35f)
                                    .Width(35f)
                                    .OnClick(model => model.OnClickClearSearch());
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
                            row.AddColumn(col2 =>
                            {
                                col2.AddRow(row2 =>
                                {
                                    row2.AddButton(button =>
                                    {
                                        button
                                            .Label(LocaleString.ClearFilters)
                                            .Height(35f)
                                            .Width(180f)
                                            .OnClick(model => model.OnClickClearFilters());
                                    });
                                });

                                col2.AddRow(row2 =>
                                {
                                    row2.Width(180f);
                                    row2.AddList(template =>
                                    {
                                        template.RowHeight(32f);
                                        template.AddTemplateCell(cell =>
                                        {
                                            cell.AddRow(row =>
                                            {
                                                row.AddButtonSelect(button =>
                                                {
                                                    button
                                                        .Label(model => model.CategoryNames)
                                                        .IsSelected(model => model.CategoryToggles)
                                                        .Height(30f)
                                                        .OnClick(model => model.OnClickCategory());
                                                });
                                            });
                                        });
                                    }, model => model.CategoryNames);
                                });
                            });

                            row.AddColumn(col2 =>
                            {
                                col2.AddRow(row2 =>
                                {
                                    row2.AddList(template =>
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
                                            cell
                                                .IsVariable(false)
                                                .Width(120f)
                                                .AddRow(row =>
                                                {
                                                    row.AddLabel(label =>
                                                    {
                                                        label
                                                            .Label(model => model.ItemPriceNames);
                                                    });
                                                });
                                        });

                                        template.AddTemplateCell(cell =>
                                        {
                                            cell
                                                .Width(40f)
                                                .IsVariable(false)
                                                .AddRow(row =>
                                                {
                                                    row.AddButton(button =>
                                                    {
                                                        button
                                                            .Label(LocaleString.QuestionMark)
                                                            .Width(40f)
                                                            .Height(40f)
                                                            .OnClick(model => model.OnClickExamine());
                                                    });
                                                });
                                        });

                                        template.AddTemplateCell(cell =>
                                        {
                                            cell.AddRow(row =>
                                            {
                                                row.AddButton(button =>
                                                {
                                                    button
                                                        .Label(LocaleString.Buy)
                                                        .OnClick(model => model.OnClickBuy())
                                                        .IsEnabled(model => model.ItemBuyEnabled);
                                                });
                                            });
                                        });
                                    }, model => model.ItemNames);
                                });

                                col2.AddRow(row2 =>
                                {
                                    row2.AddSpacer();

                                    row2.AddButton(button =>
                                    {
                                        button
                                            .Label(LocaleString.LessThanSymbol)
                                            .Width(32f)
                                            .Height(35f)
                                            .OnClick(model => model.OnClickPreviousPage());
                                    });

                                    row2.AddComboBox(comboBox =>
                                    {
                                        comboBox
                                            .Option(model => model.PageNumbers)
                                            .Selection(model => model.SelectedPageIndex);
                                    });

                                    row2.AddButton(button =>
                                    {
                                        button
                                            .Label(LocaleString.GreaterThanSymbol)
                                            .Width(32f)
                                            .Height(35f)
                                            .OnClick(model => model.OnClickNextPage());
                                    });
                                    row2.AddSpacer();
                                });
                            });
                        });

                    });

            }).Build();
        }
    }
}
