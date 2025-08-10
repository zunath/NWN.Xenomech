using Anvil.API;
using Anvil.Services;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;
using NuiDirection = XM.Shared.API.NUI.NuiDirection;
using NuiScrollbars = XM.Shared.API.NUI.NuiScrollbars;

namespace XM.Codex
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
                        // Top search bar spanning full width
                        root.AddRow(sr =>
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
                                    .Width(80f)
                                    .OnClick(model => model.OnSearch());
                            });
                        });

                        // Main content row with left navigation and right content
                        root.AddRow(row =>
                        {
                            // Left column
                            row.AddColumn(left =>
                            {
                                left.AddRow(row2 =>
                                {
                                    row2.AddComboBox(combo =>
                                    {
                                        combo
                                            .Option(model => model.CategoryOptions)
                                            .Selection(model => model.SelectedCategory)
                                            .Width(300f);
                                    });
                                });

                                left.AddRow(topicsRow =>
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


