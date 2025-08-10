using Anvil.API;
using Anvil.Services;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;

namespace XM.Plugin.Craft.UI
{
    [ServiceBinding(typeof(IView))]
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
                    .InitialGeometry(0, 0, 545f, 600f)
                    .Root(root =>
                    {
                        root.AddRow(row =>
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

                        root.AddRow(row =>
                        {
                            row.AddComboBox(comboBox =>
                            {
                                comboBox
                                    .Selection(model => model.SelectedCategoryId)
                                    .Option(model => model.Categories)
                                    .Width(300f);
                            });
                            row.AddSpacer();
                        });

                        root.AddRow(row =>
                        {
                            row.AddColumn(col =>
                            {
                                col.AddRow(row =>
                                {
                                    row.AddList(template =>
                                    {
                                        template.AddTemplateCell(cell =>
                                        {
                                            cell.AddRow(cellRow =>
                                            {
                                                cellRow.AddButtonSelect(button =>
                                                {
                                                    button
                                                        .IsSelected(model => model.RecipeToggles)
                                                        .Label(model => model.RecipeNames)
                                                        .OnClick(model => model.OnSelectRecipe())
                                                        .ForegroundColor(model => model.RecipeColors);
                                                });
                                            });
                                        });

                                    }, model => model.RecipeNames);
                                });

                                col.AddRow(row =>
                                {
                                    row.AddSpacer();

                                    row.AddButton(button =>
                                    {
                                        button
                                            .Label(LocaleString.LessThanSymbol)
                                            .Width(32f)
                                            .Height(35f)
                                            .OnClick(model => model.OnClickPreviousPage());
                                    });

                                    row.AddComboBox(comboBox =>
                                    {
                                        comboBox
                                            .Option(model => model.PageNumbers)
                                            .Selection(model => model.SelectedPageIndex);
                                    });

                                    row.AddButton(button =>
                                    {
                                        button
                                            .Label(LocaleString.GreaterThanSymbol)
                                            .Width(32f)
                                            .Height(35f)
                                            .OnClick(model => model.OnClickNextPage());
                                    });

                                    row.AddSpacer();
                                });
                            });

                            row.AddColumn(col =>
                            {
                                col.AddRow(row =>
                                {
                                    row.AddSpacer();

                                    row.AddLabel(label =>
                                    {
                                        label
                                            .Label(model => model.RecipeName)
                                            .HorizontalAlign(NuiHAlign.Left)
                                            .VerticalAlign(NuiVAlign.Top)
                                            .Height(26f);
                                    });

                                    row.AddSpacer();
                                });

                                col.AddRow(row =>
                                {
                                    row.AddSpacer();

                                    row.AddLabel(label =>
                                    {
                                        label
                                            .Label(model => model.RecipeLevel)
                                            .HorizontalAlign(NuiHAlign.Left)
                                            .VerticalAlign(NuiVAlign.Top)
                                            .Height(26f);
                                    });

                                    row.AddSpacer();
                                });

                                col.AddRow(row =>
                                {
                                    row.AddList(template =>
                                    {
                                        template.AddTemplateCell(cell =>
                                        {
                                            cell.AddRow(cellRow =>
                                            {
                                                cellRow.AddLabel(label =>
                                                {
                                                    label
                                                        .Label(model => model.RecipeDetails)
                                                        .ForegroundColor(model => model.RecipeDetailColors)
                                                        .HorizontalAlign(NuiHAlign.Left)
                                                        .VerticalAlign(NuiVAlign.Middle);
                                                });
                                            });
                                        });
                                    }, model => model.RecipeDetails);
                                });

                                col.AddRow(row =>
                                {
                                    row.AddSpacer();

                                    row.AddButton(button =>
                                    {
                                        button
                                            .Label(LocaleString.Craft)
                                            .IsEnabled(model => model.CanCraftRecipe)
                                            .Height(35f)
                                            .OnClick(model => model.OnClickCraft());
                                    });

                                    row.AddSpacer();
                                });
                            });

                        });
                    });

            }).Build();

        }
    }
}
