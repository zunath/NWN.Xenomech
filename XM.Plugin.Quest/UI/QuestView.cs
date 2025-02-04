using Anvil.API;
using Anvil.Services;
using NLog.Layouts;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;
using XM.UI.Builder.Layout;
using NuiDirection = XM.Shared.API.NUI.NuiDirection;
using NuiScrollbars = XM.Shared.API.NUI.NuiScrollbars;

namespace XM.Quest.UI
{
    [ServiceBinding(typeof(IView))]
    internal class QuestView : IView
    {
        private readonly NuiBuilder<QuestViewModel> _builder = new();
        public IViewModel CreateViewModel()
        {
            return new QuestViewModel();
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
                            row.AddColumn(BuildSearch);
                            row.AddColumn(BuildActiveQuestDetails);
                        });
                    });
            }).Build();
        }

        private void BuildSearch(NuiColumnBuilder<QuestViewModel> col)
        {
            col.AddRow(row =>
            {
                row.AddColumn(col =>
                {
                    col.AddRow(row =>
                    {
                        row.AddTextEdit(edit =>
                        {
                            edit
                                .Value(model => model.SearchText)
                                .Placeholder(LocaleString.SearchByName);
                        });

                        row.AddButton(button =>
                        {
                            button
                                .Label(LocaleString.X)
                                .Height(35f)
                                .Width(35f)
                                .OnClick(model => model.OnClearSearch);
                        });

                        row.AddButton(button =>
                        {
                            button
                                .Label(LocaleString.Search)
                                .Height(35f)
                                .Width(60f)
                                .OnClick(model => model.OnSearch);
                        });
                    });

                    col.AddList(list =>
                    {
                        list.AddTemplateCell(template =>
                        {
                            template.AddRow(row =>
                            {
                                row.AddButtonSelect(button =>
                                {
                                    button
                                        .IsSelected(model => model.QuestToggles)
                                        .Label(model => model.QuestNames)
                                        .TooltipText(model => model.QuestNames)
                                        .OnClick(model => model.OnSelectQuest);
                                });
                            });
                        });
                    }, model => model.QuestNames);
                });
            });
        }

        private void BuildActiveQuestDetails(NuiColumnBuilder<QuestViewModel> col)
        {
            col.AddRow(row =>
            {
                row.AddLabel(label =>
                {
                    label
                        .Label(model => model.ActiveQuestName)
                        .Height(20f)
                        .HorizontalAlign(NuiHAlign.Center)
                        .VerticalAlign(NuiVAlign.Top);
                });
            });

            col.AddRow(row =>
            {
                row.AddText(text =>
                {
                    text
                        .Text(model => model.ActiveQuestDescription)
                        .Border(true)
                        .Scrollbars(NuiScrollbars.Y);
                });
            });

            col.AddRow(row =>
            {
                row.AddSpacer();

                row.AddButton(button =>
                {
                    button
                        .Label(LocaleString.AbandonQuest)
                        .Height(32f)
                        .OnClick(model => model.OnAbandonQuest)
                        .IsEnabled(model => model.IsAbandonQuestEnabled);
                });

                row.AddSpacer();
            });
        }
    }
}
