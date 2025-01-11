using System;
using System.Linq.Expressions;
using Anvil.API;
using Anvil.Services;
using XM.Progression.Job;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;
using XM.UI.Builder.Layout;
using NuiScrollbars = XM.Shared.API.NUI.NuiScrollbars;

namespace XM.Progression.UI.CharacterSheetUI
{
    [ServiceBinding(typeof(IView))]
    internal class CharacterSheetView: IView
    {
        private readonly NuiBuilder<CharacterSheetViewModel> _builder = new();
        private readonly JobService _job;

        public CharacterSheetView(JobService job)
        {
            _job = job;
        }

        public IViewModel CreateViewModel()
        {
            return new CharacterSheetViewModel();
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
                    .Title(LocaleString.CharacterSheet)
                    .InitialGeometry(0, 0, 800, 400)
                    .Root(root =>
                    {
                        root.AddRow(BuildNavigation);

                        root.AddRow(row =>
                        {
                            row.AddColumn(col =>
                            {
                                col.AddRow(row2 =>
                                {
                                    row2.AddPartialPlaceholder(CharacterSheetViewModel.MainView);
                                });
                            });

                            row.AddColumn(col =>
                            {
                                col.AddGroup(group =>
                                {
                                    group
                                        .Width(130f)
                                        .SetBorder(false)
                                        .SetScrollbars(NuiScrollbars.Y);
                                    BuildButtons(group);
                                });
                            });

                        });
                    })
                    .DefinePartialView(CharacterSheetViewModel.StatPartialId, BuildStatPartial)
                    .DefinePartialView(CharacterSheetViewModel.JobPartialId, BuildJobPartial)
                    .DefinePartialView(CharacterSheetViewModel.SettingsPartialId, BuildSettingsPartial);
            }).Build();
        }

        private void BuildNavigation(NuiRowBuilder<CharacterSheetViewModel> row)
        {
            row.AddToggles(toggle =>
            {
                toggle
                    .AddOption(LocaleString.Stat)
                    .AddOption(LocaleString.Job)
                    .AddOption(LocaleString.Settings)
                    .SelectedValue(model => model.SelectedTab)
                    .OnMouseDown(model => model.OnChangeTab);
            });
            row.AddSpacer();
        }

        private void BuildStatPartial(NuiGroupBuilder<CharacterSheetViewModel> group)
        {
            group.SetLayout(col =>
            {

            });
        }

        private void BuildSettingsPartial(NuiGroupBuilder<CharacterSheetViewModel> group)
        {
            group.SetLayout(col =>
            {

            });
        }

        private void BuildJobPartial(NuiGroupBuilder<CharacterSheetViewModel> partial)
        {
            partial
                .SetBorder(false)
                .SetScrollbars(NuiScrollbars.Auto)
                .SetLayout(layout =>
                {
                    layout.AddList(list =>
                    {
                        list.RowHeight(70f);
                        list.AddTemplateCell(cell =>
                        {
                            cell.Width(70f);
                            cell.IsVariable(false);
                            cell.AddGroup(group =>
                            {
                                group.SetLayout(cellLayout =>
                                {
                                    cellLayout.AddRow(cellLayoutRow =>
                                    {
                                        cellLayoutRow.AddColumn(col =>
                                        {
                                            col.AddGroup(imageGroup =>
                                            {
                                                imageGroup.SetLayout(layout =>
                                                {
                                                    layout.AddImage(image =>
                                                    {
                                                        image
                                                            .HorizontalAlign(NuiHAlign.Center)
                                                            .VerticalAlign(NuiVAlign.Top)
                                                            .ResRef(model => model.JobIcons)
                                                            .Height(64f)
                                                            .Width(64f);
                                                    });
                                                });
                                            });
                                        });
                                    });
                                });
                            });
                        }, model => model.JobNames);

                        list.AddTemplateCell(cell =>
                        {
                            cell.Width(16f);
                            cell.IsVariable(false);
                            cell.AddGroup(group =>
                            {
                                group.SetLayout(layout =>
                                {
                                    layout.AddSpacer();
                                });
                            });
                        }, model => model.JobNames);

                        list.AddTemplateCell(cell =>
                        {
                            cell.Width(200f);
                            cell.IsVariable(false);
                            cell.AddGroup(group =>
                            {
                                group.SetLayout(col =>
                                {
                                    col.AddRow(row =>
                                    {
                                        row.AddText(label =>
                                        {
                                            label
                                                .Text(model => model.JobNames);
                                        });
                                    });
                                    col.AddRow(row =>
                                    {
                                        row.AddText(label =>
                                        {
                                            label
                                                .Text(model => model.JobLevels);
                                        });
                                    });
                                });
                            });
                        }, model => model.JobNames);

                        list.AddTemplateCell(cell =>
                        {
                            cell.AddGroup(group =>
                            {
                                group.SetLayout(col =>
                                {
                                    col.AddRow(row =>
                                    {
                                        row.Height(10f);
                                        row.AddSpacer();
                                    });
                                    col.AddRow(row =>
                                    {
                                        row.AddProgress(progress =>
                                        {
                                            progress.Value(model => model.JobProgresses);
                                        });
                                    });
                                });
                            });
                        }, model => model.JobNames);
                    });
                });
        }


        private void BuildButtons(NuiGroupBuilder<CharacterSheetViewModel> group)
        {
            group.SetLayout(col =>
            {
                col.AddButton(button =>
                {
                    button
                        .Label(LocaleString.Appearance)
                        .Width(100f)
                        .OnClick(model => model.OnClickAppearance);
                });
                col.AddButton(button =>
                {
                    button
                        .Label(LocaleString.Quests)
                        .Width(100f)
                        .OnClick(model => model.OnClickQuests);
                });
                col.AddButton(button =>
                {
                    button
                        .Label(LocaleString.KeyItems)
                        .Width(100f)
                        .OnClick(model => model.OnClickKeyItems);
                });
                col.AddButton(button =>
                {
                    button
                        .Label(LocaleString.OpenTrash)
                        .Width(100f)
                        .OnClick(model => model.OnClickOpenTrash);
                });
            });
        }
    }
}
