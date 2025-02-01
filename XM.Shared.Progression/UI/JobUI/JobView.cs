using Anvil.Services;
using System.Linq.Expressions;
using System;
using Anvil.API;
using XM.Progression.Job;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;
using XM.UI.Builder.Layout;
using NRediSearch.Aggregation;
using Action = System.Action;

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
            _builder.CreateWindow(window =>
            {
                window.InitialGeometry(0, 0, 200, 200)
                    .Title(LocaleString.ChangeJob)
                    .IsClosable(true)
                    .IsResizable(true)
                    .IsCollapsible(WindowCollapsibleType.UserCollapsible)
                    .IsTransparent(false)
                    .Border(true)
                    .AcceptsInput(true)
                    .Root(BuildChangeJob);
            });


            return _builder.Build();
        }

        private void BuildChangeJob(NuiColumnBuilder<JobViewModel> col)
        {
            col.AddRow(BuildJobHeader);
            col.AddRow(BuildJobSelection);
            BuildResonanceNodes(col);
            BuildJobFilter(col);
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
                Expression<Func<JobViewModel, bool>> isEncouraged,
                Expression<Func<JobViewModel, Action>> onClick)
            {
                row.AddButtonImage(button =>
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
            }

            row.AddSpacer();
            BuildJobButton(JobType.Keeper, LocaleString.Keeper, model => model.IsKeeperEncouraged, model => model.OnClickKeeper);
            BuildJobButton(JobType.Mender, LocaleString.Mender, model => model.IsMenderEncouraged, model => model.OnClickMender);
            BuildJobButton(JobType.Brawler, LocaleString.Brawler, model => model.IsBrawlerEncouraged, model => model.OnClickBrawler);
            BuildJobButton(JobType.Beastmaster, LocaleString.Beastmaster, model => model.IsBeastmasterEncouraged, model => model.OnClickBeastmaster);
            BuildJobButton(JobType.Elementalist, LocaleString.Elementalist, model => model.IsElementalistEncouraged, model => model.OnClickElementalist);
            BuildJobButton(JobType.Techweaver, LocaleString.Techweaver, model => model.IsTechweaverEncouraged, model => model.OnClickTechweaver);
            BuildJobButton(JobType.Hunter, LocaleString.Hunter, model => model.IsHunterEncouraged, model => model.OnClickHunter);
            BuildJobButton(JobType.Nightstalker, LocaleString.Nightstalker, model => model.IsNightstalkerEncouraged, model => model.OnClickNightstalker);
            row.AddSpacer();
        }

        private void BuildResonanceNodes(NuiColumnBuilder<JobViewModel> col)
        {
            const int PipWidth = 22;
            const int PipHeight = 44;

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

        private void BuildJobFilter(NuiColumnBuilder<JobViewModel> col)
        {
            void BuildJobFilterButton(
                NuiRowBuilder<JobViewModel> row,
                JobType jobType,
                LocaleString name,
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
                        .IsEncouraged(isEncouraged)
                        .TooltipText(name)
                        .OnClick(onClick);
                });
            }

            col.AddRow(row =>
            {
                row.AddLabel(label =>
                {
                    label
                        .Label(LocaleString.AvailableAbilities)
                        .Height(20f);
                });
            });
            
            col.AddRow(row =>
            {
                row.AddSpacer();
                BuildJobFilterButton(row, JobType.Keeper, LocaleString.Keeper, model => model.IsKeeperFilterEncouraged, model => model.OnClickFilterKeeper);
                BuildJobFilterButton(row, JobType.Mender, LocaleString.Mender, model => model.IsMenderFilterEncouraged, model => model.OnClickFilterMender);
                BuildJobFilterButton(row, JobType.Brawler, LocaleString.Brawler, model => model.IsBrawlerFilterEncouraged, model => model.OnClickFilterBrawler);
                BuildJobFilterButton(row, JobType.Beastmaster, LocaleString.Beastmaster, model => model.IsBeastmasterFilterEncouraged, model => model.OnClickFilterBeastmaster);
                BuildJobFilterButton(row, JobType.Elementalist, LocaleString.Elementalist, model => model.IsElementalistFilterEncouraged, model => model.OnClickFilterElementalist);
                BuildJobFilterButton(row, JobType.Techweaver, LocaleString.Techweaver, model => model.IsTechweaverFilterEncouraged, model => model.OnClickFilterTechweaver);
                BuildJobFilterButton(row, JobType.Hunter, LocaleString.Hunter, model => model.IsHunterFilterEncouraged, model => model.OnClickFilterHunter);
                BuildJobFilterButton(row, JobType.Nightstalker, LocaleString.Nightstalker, model => model.IsNightstalkerFilterEncouraged, model => model.OnClickFilterNightstalker);
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
