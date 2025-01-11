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

        private void BuildJobPartial(NuiGroupBuilder<CharacterSheetViewModel> group)
        {
            group
                .SetBorder(false)
                .SetScrollbars(NuiScrollbars.Auto)
                .SetLayout(col =>
                {
                    col.AddRow(row =>
                    {
                        BuildJobDetail(row, JobType.Keeper, model => model.KeeperLevel);
                        BuildJobDetail(row, JobType.Mender, model => model.MenderLevel);
                    });
                    col.AddRow(row =>
                    {
                        BuildJobDetail(row, JobType.Brawler, model => model.BrawlerLevel);
                        BuildJobDetail(row, JobType.Beastmaster, model => model.BeastmasterLevel);
                    });
                    col.AddRow(row =>
                    {
                        BuildJobDetail(row, JobType.Techweaver, model => model.TechweaverLevel);
                        BuildJobDetail(row, JobType.Elementalist, model => model.ElementalistLevel);
                    });
                    col.AddRow(row =>
                    {
                        BuildJobDetail(row, JobType.Nightstalker, model => model.NightstalkerLevel);
                        BuildJobDetail(row, JobType.Hunter, model => model.HunterLevel);
                    });
                });
        }

        private void BuildJobDetail(
            NuiRowBuilder<CharacterSheetViewModel> row, 
            JobType job,
            Expression<Func<CharacterSheetViewModel, string>> levelExpression)
        {
            var definition = _job.GetJobDefinition(job);

            row.AddSpacer();
            if (job == JobType.Invalid)
            {
                return;
            }

            row.AddColumn(col =>
            {
                col.AddImage(image =>
                {
                    image
                        .HorizontalAlign(NuiHAlign.Center)
                        .VerticalAlign(NuiVAlign.Top)
                        .ResRef(definition.IconResref)
                        .Height(64f)
                        .Width(64f);
                });
            });

            row.AddColumn(col =>
            {
                col.AddRow(row =>
                {
                    row.AddLabel(label =>
                    {
                        label
                            .Label(definition.Name)
                            .HorizontalAlign(NuiHAlign.Left)
                            .VerticalAlign(NuiVAlign.Top);
                    });

                    row.AddLabel(label =>
                    {
                        label
                            .Label(levelExpression);
                    });
                });
            });

            row.AddSpacer();
        }

        private void BuildButtons(NuiGroupBuilder<CharacterSheetViewModel> group)
        {
            group.SetLayout(col =>
            {
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
                        .Label(LocaleString.Appearance)
                        .Width(100f)
                        .OnClick(model => model.OnClickAppearance);
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
