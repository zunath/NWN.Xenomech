using Anvil.API;
using Anvil.Services;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;
using NuiDirection = XM.Shared.API.NUI.NuiDirection;
using NuiScrollbars = XM.Shared.API.NUI.NuiScrollbars;

namespace XM.UI.Codex
{
    [ServiceBinding(typeof(IView))]
    internal class CodexView : IView
    {
        private readonly NuiBuilder<CodexViewModel> _builder = new();

        public IViewModel CreateViewModel()
        {
            return new CodexViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
            {
                window
                    .IsResizable(true)
                    .IsCollapsible(WindowCollapsibleType.UserCollapsible)
                    .InitialGeometry(0f, 0f, 900f, 550f)
                    .Title(LocaleString.Codex)
                    .Root(root =>
                    {
                        root.AddRow(row =>
                        {
                            // Left column: search + categories + topics
                            row.AddColumn(left =>
                            {
                                left.AddRow(sr =>
                                {
                                    sr.AddTextEdit(edit =>
                                    {
                                        edit
                                            .Placeholder(LocaleString.Search)
                                            .Value(model => model.SearchText);
                                    });
                                    sr.AddButton(btn =>
                                    {
                                        btn
                                            .Label(LocaleString.X)
                                            .Height(35f)
                                            .Width(35f)
                                            .OnClick(model => model.OnClearSearch());
                                    });
                                    sr.AddButton(btn =>
                                    {
                                        btn
                                            .Label(LocaleString.Search)
                                            .Height(35f)
                                            .Width(60f)
                                            .OnClick(model => model.OnSearch());
                                    });
                                });

                                // Categories list
                                left.AddGroup(group =>
                                {
                                    group
                                        .Height(200f)
                                        .Border(true)
                                        .Scrollbars(NuiScrollbars.Y)
                                        .SetLayout(layout =>
                                        {
                                            layout.AddRow(catRow =>
                                            {
                                                catRow.AddList(list =>
                                                {
                                                    list.AddTemplateCell(tpl =>
                                                    {
                                                        tpl.AddRow(r =>
                                                        {
                                                            r.AddButtonSelect(btn =>
                                                            {
                                                                btn
                                                                    .IsSelected(model => model.CategoryToggles)
                                                                    .Label(model => model.Categories)
                                                                    .OnClick(model => model.OnSelectCategory());
                                                            });
                                                        });
                                                    });
                                                }, model => model.Categories);
                                            });
                                        });
                                });

                                // Topics list
                                left.AddGroup(group =>
                                {
                                    group
                                        .Height(280f)
                                        .Border(true)
                                        .Scrollbars(NuiScrollbars.Y)
                                        .SetLayout(layout =>
                                        {
                                            layout.AddRow(topicsRow =>
                                            {
                                                topicsRow.AddList(list =>
                                                {
                                                    list.AddTemplateCell(tpl =>
                                                    {
                                                        tpl.AddRow(r =>
                                                        {
                                                            r.AddButtonSelect(btn =>
                                                            {
                                                                btn
                                                                    .IsSelected(model => model.TopicToggles)
                                                                    .Label(model => model.TopicTitles)
                                                                    .TooltipText(model => model.TopicTitles)
                                                                    .OnClick(model => model.OnSelectTopic());
                                                            });
                                                        });
                                                    });
                                                }, model => model.TopicTitles);
                                            });
                                        });
                                });
                            });

                            // Right column: topic content
                            row.AddColumn(right =>
                            {
                                right.AddRow(titleRow =>
                                {
                                    titleRow.AddLabel(lbl =>
                                    {
                                        lbl
                                            .Label(LocaleString.Description)
                                            .HorizontalAlign(NuiHAlign.Center)
                                            .VerticalAlign(NuiVAlign.Middle)
                                            .Height(26f);
                                    });
                                });

                                right.AddRow(contentRow =>
                                {
                                    contentRow.AddText(txt =>
                                    {
                                        txt
                                            .Text(model => model.TopicContent)
                                            .Border(true)
                                            .Scrollbars(NuiScrollbars.Y);
                                    });
                                });
                            });
                        });
                    });
            }).Build();
        }
    }
}


