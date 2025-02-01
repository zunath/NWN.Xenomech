using Anvil.Services;
using System.Linq.Expressions;
using System;
using Anvil.API;
using XM.Progression.Job;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;
using XM.UI.Builder.Layout;
using Action = System.Action;
using NuiScrollbars = XM.Shared.API.NUI.NuiScrollbars;
using NuiAspect = XM.Shared.API.NUI.NuiAspect;
using NLog.Layouts;

namespace XM.Progression.UI.JobUI
{
    [ServiceBinding(typeof(IView))]
    internal class JobView: IView
    {
        private readonly NuiBuilder<JobViewModel> _builder = new();
        private readonly JobService _job;

        public JobView(JobService job)
        {
            _job = job;
        }

        public IViewModel CreateViewModel()
        {
            return new JobViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
            {
                window.InitialGeometry(0, 0, 200, 200)
                    .Title(LocaleString.ChangeJob)
                    .IsClosable(true)
                    .IsResizable(true)
                    .IsCollapsible(WindowCollapsibleType.UserCollapsible)
                    .IsTransparent(false)
                    .Border(true)
                    .AcceptsInput(true)
                    .Root(BuildWindow)

                    .DefinePartialView(JobViewModel.AvailableAbilitiesPartialId, BuildAvailableAbilitiesPartial)
                    .DefinePartialView(JobViewModel.EquippedAbilitiesPartialId, BuildEquippedAbilitiesPartial);
            }).Build();
        }

        private void BuildWindow(NuiColumnBuilder<JobViewModel> col)
        {
            col.AddRow(BuildJobHeader);
            col.AddRow(BuildJobSelection);
            BuildResonanceNodes(col);
            BuildAbilityNavigation(col);
            BuildMainView(col);
            BuildFooter(col);
        }

        private void BuildJobHeader(NuiRowBuilder<JobViewModel> row)
        {
            row.AddSpacer();
            row.AddLabel(label =>
            {
                label.Label(LocaleString.SelectJob)
                    .Height(20f)
                    .HorizontalAlign(NuiHAlign.Center)
                    .VerticalAlign(NuiVAlign.Middle);
            });
            row.AddSpacer();
        }

        private void BuildJobSelection(NuiRowBuilder<JobViewModel> row)
        {
            void BuildJobButton(
                JobType jobType, 
                LocaleString name,
                Expression<Func<JobViewModel, string>> jobLevel,
                Expression<Func<JobViewModel, bool>> isEncouraged,
                Expression<Func<JobViewModel, Action>> onClick)
            {
                row.AddColumn(col =>
                {
                    col.AddRow(buttonRow =>
                    {
                        buttonRow.AddButtonImage(button =>
                        {
                            var job = _job.GetJobDefinition(jobType);
                            button.ResRef(job.IconResref)
                                .Width(50f)
                                .Height(50f)
                                .Margin(4f)
                                .IsEncouraged(isEncouraged)
                                .TooltipText(name)
                                .OnClick(onClick);
                        });
                    });
                    col.AddRow(levelRow =>
                    {
                        levelRow.AddSpacer();
                        levelRow.AddLabel(label =>
                        {
                            label
                                .Label(jobLevel)
                                .Height(20f)
                                .VerticalAlign(NuiVAlign.Top)
                                .HorizontalAlign(NuiHAlign.Center);
                        });
                        levelRow.AddSpacer();
                    });
                });
            }

            row.AddSpacer();
            BuildJobButton(
                JobType.Keeper, 
                LocaleString.Keeper, 
                model => model.KeeperJobLevel,
                model => model.IsKeeperEncouraged, 
                model => model.OnClickKeeper);
            BuildJobButton(
                JobType.Mender, 
                LocaleString.Mender, 
                model => model.MenderJobLevel,
                model => model.IsMenderEncouraged, 
                model => model.OnClickMender);
            BuildJobButton(
                JobType.Brawler, 
                LocaleString.Brawler, 
                model => model.BrawlerJobLevel,
                model => model.IsBrawlerEncouraged, 
                model => model.OnClickBrawler);
            BuildJobButton(
                JobType.Beastmaster, 
                LocaleString.Beastmaster, 
                model => model.BeastmasterJobLevel,
                model => model.IsBeastmasterEncouraged, 
                model => model.OnClickBeastmaster);
            BuildJobButton(
                JobType.Elementalist, 
                LocaleString.Elementalist, 
                model => model.ElementalistJobLevel,
                model => model.IsElementalistEncouraged, 
                model => model.OnClickElementalist);
            BuildJobButton(
                JobType.Techweaver, 
                LocaleString.Techweaver, 
                model => model.TechweaverJobLevel,
                model => model.IsTechweaverEncouraged, 
                model => model.OnClickTechweaver);
            BuildJobButton(
                JobType.Hunter, 
                LocaleString.Hunter, 
                model => model.HunterJobLevel,
                model => model.IsHunterEncouraged, 
                model => model.OnClickHunter);
            BuildJobButton(
                JobType.Nightstalker, 
                LocaleString.Nightstalker, 
                model => model.NightstalkerJobLevel,
                model => model.IsNightstalkerEncouraged, 
                model => model.OnClickNightstalker);
            row.AddSpacer();
        }

        private const int PipWidth = 22;
        private const int PipHeight = 44;
        private void BuildResonanceNodes(NuiColumnBuilder<JobViewModel> col)
        {

            col.AddRow(row =>
            {
                row.AddSpacer();
                row.AddLabel(label =>
                {
                    label.Label(LocaleString.ResonanceNodes)
                        .Height(20f)
                        .HorizontalAlign(NuiHAlign.Center)
                        .VerticalAlign(NuiVAlign.Middle);
                });
                row.AddSpacer();
            });
            col.AddRow(row =>
            {
                row.AddSpacer();
                row.AddImage(image =>
                {
                    image
                        .ResRef(model => model.ResonancePip1)
                        .Width(PipWidth)
                        .Height(PipHeight)
                        .TooltipText(model => model.ResonancePipTooltip1);
                });
                row.AddImage(image =>
                {
                    image.ResRef(model => model.ResonancePip2)
                        .Width(PipWidth)
                        .Height(PipHeight)
                        .TooltipText(model => model.ResonancePipTooltip2);
                });
                row.AddImage(image =>
                {
                    image.ResRef(model => model.ResonancePip3)
                        .Width(PipWidth)
                        .Height(PipHeight)
                        .TooltipText(model => model.ResonancePipTooltip3);
                });
                row.AddImage(image =>
                {
                    image.ResRef(model => model.ResonancePip4)
                        .Width(PipWidth)
                        .Height(PipHeight)
                        .TooltipText(model => model.ResonancePipTooltip4);
                });
                row.AddImage(image =>
                {
                    image.ResRef(model => model.ResonancePip5)
                        .Width(PipWidth)
                        .Height(PipHeight)
                        .TooltipText(model => model.ResonancePipTooltip5);
                });
                row.AddImage(image =>
                {
                    image.ResRef(model => model.ResonancePip6)
                        .Width(PipWidth)
                        .Height(PipHeight)
                        .TooltipText(model => model.ResonancePipTooltip6);
                });
                row.AddImage(image =>
                {
                    image.ResRef(model => model.ResonancePip7)
                        .Width(PipWidth)
                        .Height(PipHeight)
                        .TooltipText(model => model.ResonancePipTooltip7);
                });
                row.AddImage(image =>
                {
                    image.ResRef(model => model.ResonancePip8)
                        .Width(PipWidth)
                        .Height(PipHeight)
                        .TooltipText(model => model.ResonancePipTooltip8);
                });
                row.AddImage(image =>
                {
                    image.ResRef(model => model.ResonancePip9)
                        .Width(PipWidth)
                        .Height(PipHeight)
                        .TooltipText(model => model.ResonancePipTooltip9);
                });
                row.AddImage(image =>
                {
                    image.ResRef(model => model.ResonancePip10)
                        .Width(PipWidth)
                        .Height(PipHeight)
                        .TooltipText(model => model.ResonancePipTooltip10);
                });
                row.AddSpacer();
            });
        }

        private void BuildAvailableAbilitiesPartial(NuiGroupBuilder<JobViewModel> group)
        {
            group
                .Border(false)
                .Scrollbars(NuiScrollbars.Auto)
                .SetLayout(layout =>
                {
                    BuildJobFilter(layout);
                    BuildAbilitySection(layout);
                });
        }

        private void BuildEquippedAbilitiesPartial(NuiGroupBuilder<JobViewModel> group)
        {
            group
                .Border(false)
                .Scrollbars(NuiScrollbars.Auto)
                .SetLayout(BuildEquippedAbilities);
        }
        private void BuildAbilityNavigation(NuiColumnBuilder<JobViewModel> col)
        {
            col.AddRow(row =>
            {
                row.AddSpacer();
                row.AddToggles(toggle =>
                {
                    toggle
                        .AddOption(LocaleString.AvailableAbilities)
                        .AddOption(LocaleString.EquippedAbilities)
                        .Height(32f)
                        .SelectedValue(model => model.SelectedTab)
                        .OnMouseDown(model => model.OnChangeTab);
                });
                row.AddSpacer();
            });
        }

        private void BuildMainView(NuiColumnBuilder<JobViewModel> col)
        {
            col.AddPartialPlaceholder(JobViewModel.MainView);
        }

        private void BuildFooter(NuiColumnBuilder<JobViewModel> col)
        {
            col.AddRow(row =>
            {
                row.AddButton(button =>
                {
                    button.Label(LocaleString.ChangeJob)
                        .OnClick(model => model.OnClickChangeJob);
                });
            });
        }

        private void BuildJobFilter(NuiColumnBuilder<JobViewModel> col)
        {
            void BuildJobFilterButton(
                NuiRowBuilder<JobViewModel> row,
                JobType jobType,
                LocaleString name,
                Expression<Func<JobViewModel, bool>> isEnabled,
                Expression<Func<JobViewModel, bool>> isEncouraged,
                Expression<Func<JobViewModel, Action>> onClick)
            {
                row.AddButtonImage(button =>
                {
                    var job = _job.GetJobDefinition(jobType);
                    button.ResRef(job.IconResref)
                        .Width(32)
                        .Height(32)
                        .Margin(2f)
                        .IsEnabled(isEnabled)
                        .IsEncouraged(isEncouraged)
                        .TooltipText(name)
                        .OnClick(onClick);
                });
            }
            
            col.AddRow(row =>
            {
                row.AddSpacer();
                BuildJobFilterButton(
                    row, 
                    JobType.Keeper, 
                    LocaleString.Keeper, 
                    model => model.IsKeeperFilterEnabled,
                    model => model.IsKeeperFilterEncouraged, 
                    model => model.OnClickFilterKeeper);
                BuildJobFilterButton(
                    row, JobType.Mender, 
                    LocaleString.Mender,
                    model => model.IsMenderFilterEnabled,
                    model => model.IsMenderFilterEncouraged, 
                    model => model.OnClickFilterMender);
                BuildJobFilterButton(
                    row, JobType.Brawler, 
                    LocaleString.Brawler,
                    model => model.IsBrawlerFilterEnabled,
                    model => model.IsBrawlerFilterEncouraged, 
                    model => model.OnClickFilterBrawler);
                BuildJobFilterButton(
                    row, JobType.Beastmaster, 
                    LocaleString.Beastmaster,
                    model => model.IsBeastmasterFilterEnabled,
                    model => model.IsBeastmasterFilterEncouraged, 
                    model => model.OnClickFilterBeastmaster);
                BuildJobFilterButton(
                    row, JobType.Elementalist, 
                    LocaleString.Elementalist,
                    model => model.IsElementalistFilterEnabled,
                    model => model.IsElementalistFilterEncouraged, 
                    model => model.OnClickFilterElementalist);
                BuildJobFilterButton(
                    row, JobType.Techweaver, 
                    LocaleString.Techweaver,
                    model => model.IsTechweaverFilterEnabled,
                    model => model.IsTechweaverFilterEncouraged, 
                    model => model.OnClickFilterTechweaver);
                BuildJobFilterButton(
                    row, JobType.Hunter, 
                    LocaleString.Hunter,
                    model => model.IsHunterFilterEnabled,
                    model => model.IsHunterFilterEncouraged, 
                    model => model.OnClickFilterHunter);
                BuildJobFilterButton(
                    row, JobType.Nightstalker, 
                    LocaleString.Nightstalker,
                    model => model.IsNightstalkerFilterEnabled,
                    model => model.IsNightstalkerFilterEncouraged, 
                    model => model.OnClickFilterNightstalker);
                row.AddSpacer();
            });
        }

        private void BuildAbilitySection(NuiColumnBuilder<JobViewModel> col)
        {
            BuildAvailableAbilityList(col);
            col.AddRow(row =>
            {
                row.AddColumn(BuildAbilityDetailPane);
            });
        }

        private void BuildAvailableAbilityList(NuiColumnBuilder<JobViewModel> col)
        {
            const float PipOffsetX = 100f;
            const float PipOffsetY = 9f;

            col.AddList(list =>
            {
                list.RowHeight(40f);

                list.AddTemplateCell(cell =>
                {
                    cell.AddRow(row =>
                    {
                        row.AddButtonSelect(button =>
                        {
                            button
                                .Margin(4f)
                                .Height(32f)
                                .IsSelected(model => model.AbilityToggles)
                                .Label(model => model.AbilityNames)
                                .ForegroundColor(model => model.AbilityColors)
                                .OnClick(model => model.OnClickAbility)
                                .DrawList(drawList =>
                                {
                                    drawList.AddImage(image =>
                                    {
                                        image
                                            .ResRef(model => model.AbilityIcons)
                                            .VerticalAlign(NuiVAlign.Top)
                                            .HorizontalAlign(NuiHAlign.Center)
                                            .Aspect(NuiAspect.Fit)
                                            .Position(4f, 4f, 32f, 32f);
                                    });
                                    drawList.AddText(text =>
                                    {
                                        text
                                            .Text(model => model.AbilityLevels)
                                            .Bounds(50f, 12f, 50f, 32f)
                                            .Color(255, 255, 255);
                                    });
                                    drawList.AddImage(pip1 =>
                                    {
                                        pip1
                                            .ResRef(JobViewModel.PipLit)
                                            .VerticalAlign(NuiVAlign.Top)
                                            .HorizontalAlign(NuiHAlign.Center)
                                            .Aspect(NuiAspect.Fit)
                                            .IsEnabled(model => model.AbilityPip1Enabled)
                                            .Position(PipOffsetX, PipOffsetY, PipWidth/2f, PipHeight/2f);
                                    });
                                    drawList.AddImage(pip2 =>
                                    {
                                        pip2
                                            .ResRef(JobViewModel.PipLit)
                                            .VerticalAlign(NuiVAlign.Top)
                                            .HorizontalAlign(NuiHAlign.Center)
                                            .Aspect(NuiAspect.Fit)
                                            .IsEnabled(model => model.AbilityPip2Enabled)
                                            .Position(PipOffsetX + (PipWidth/2f), PipOffsetY, PipWidth/2f, PipHeight/2f);
                                    });
                                    drawList.AddImage(pip3 =>
                                    {
                                        pip3
                                            .ResRef(JobViewModel.PipLit)
                                            .VerticalAlign(NuiVAlign.Top)
                                            .HorizontalAlign(NuiHAlign.Center)
                                            .Aspect(NuiAspect.Fit)
                                            .IsEnabled(model => model.AbilityPip3Enabled)
                                            .Position(PipOffsetX + (PipWidth/2f * 2), PipOffsetY, PipWidth/2f, PipHeight/2f);
                                    });
                                    drawList.AddImage(pip4 =>
                                    {
                                        pip4
                                            .ResRef(JobViewModel.PipLit)
                                            .VerticalAlign(NuiVAlign.Top)
                                            .HorizontalAlign(NuiHAlign.Center)
                                            .Aspect(NuiAspect.Fit)
                                            .IsEnabled(model => model.AbilityPip4Enabled)
                                            .Position(PipOffsetX + (PipWidth/2f * 3), PipOffsetY, PipWidth/2f, PipHeight/2f);
                                    });
                                });
                        });
                    });
                }, model => model.AbilityNames);
            });
        }

        private void BuildAbilityDetailPane(NuiColumnBuilder<JobViewModel> col)
        {
            col.AddRow(row =>
            {
                row.AddLabel(label =>
                {
                    label.Height(20f)
                        .Label(LocaleString.Description);
                });
            });
            col.AddRow(row =>
            {
                row.AddText(text =>
                {
                    text
                        .Scrollbars(NuiScrollbars.Auto)
                        .Text(model => model.SelectedAbilityDescription)
                        .Border(true);
                });
            });
            col.AddRow(row =>
            {
                row.AddSpacer();
                row.AddButton(button =>
                {
                    button
                        .Label(model => model.EquipUnequipButtonText)
                        .Height(32f)
                        .IsEnabled(model => model.IsEquipUnequipEnabled)
                        .OnClick(model => model.OnEquipUnequipAbility);
                });
                row.AddSpacer();
            });
        }


        private void BuildEquippedAbilities(NuiColumnBuilder<JobViewModel> col)
        {
            col.AddLabel(label =>
            {
                label.Label(LocaleString.EquippedAbilities)
                    .Height(20f);
            });
        }

    }
}
