using System;
using System.Linq.Expressions;
using Anvil.API;
using Anvil.Services;
using NLog.Layouts;
using XM.Progression.Job;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;
using XM.UI.Builder.Layout;
using NuiAspect = XM.Shared.API.NUI.NuiAspect;
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
                        });
                    })
                    .DefinePartialView(CharacterSheetViewModel.StatPartialId, BuildCharacterPartial)
                    .DefinePartialView(CharacterSheetViewModel.JobPartialId, BuildJobPartial)
                    .DefinePartialView(CharacterSheetViewModel.SettingsPartialId, BuildSettingsPartial);
            }).Build();
        }

        private void BuildNavigation(NuiRowBuilder<CharacterSheetViewModel> row)
        {
            row.AddToggles(toggle =>
            {
                toggle
                    .AddOption(LocaleString.Character)
                    .AddOption(LocaleString.Job)
                    .AddOption(LocaleString.Settings)
                    .SelectedValue(model => model.SelectedTab)
                    .OnMouseDown(model => model.OnChangeTab);
            });
            row.AddSpacer();
        }

        private void BuildCharacterPartial(NuiGroupBuilder<CharacterSheetViewModel> group)
        {
            void BuildPortrait(NuiColumnBuilder<CharacterSheetViewModel> col)
            {
                col.AddRow(row =>
                {
                    row.AddSpacer();
                    row.AddLabel(label =>
                    {
                        label
                            .Label(model => model.CharacterName)
                            .Height(20f)
                            .HorizontalAlign(NuiHAlign.Center)
                            .VerticalAlign(NuiVAlign.Middle);
                    });
                    row.AddSpacer();
                });

                col.AddImage(image =>
                {
                    image
                        .ResRef(model => model.PortraitResref)
                        .VerticalAlign(NuiVAlign.Top)
                        .HorizontalAlign(NuiHAlign.Center)
                        .ImageAspect(NuiAspect.ExactScaled)
                        .Width(128f)
                        .Height(200f);
                });

                col.AddGroup(BuildButtons);
            }

            void BuildStatEntry(
                NuiColumnBuilder<CharacterSheetViewModel> layout,
                LocaleString label, 
                Expression<Func<CharacterSheetViewModel, string>> value,
                string iconResref,
                Color color = default)
            {
                layout.AddRow(row =>
                {

                    row.AddImage(image =>
                    {
                        image.ResRef(iconResref)
                            .ImageAspect(NuiAspect.Fit)
                            .VerticalAlign(NuiVAlign.Top)
                            .HorizontalAlign(NuiHAlign.Center)
                            .Width(64f)
                            .Height(64f);
                    });
                    row.AddLabel(statName =>
                    {
                        statName.Label(label)
                            .HorizontalAlign(NuiHAlign.Left)
                            .Width(140f);

                        if (color != default)
                            statName.ForegroundColor(color);
                    });
                    row.AddLabel(statValue =>
                    {
                        statValue.Label(value)
                            .HorizontalAlign(NuiHAlign.Left);

                        if (color != default)
                            statValue.ForegroundColor(color);
                    });
                });
            }

            void BuildStatsColumn1(NuiColumnBuilder<CharacterSheetViewModel> col)
            {

                col.AddRow(row =>
                {
                    row.AddSpacer();
                    row.AddLabel(label =>
                    {
                        label
                            .Label(LocaleString.Attributes)
                            .Height(20f)
                            .HorizontalAlign(NuiHAlign.Center)
                            .VerticalAlign(NuiVAlign.Middle);
                    });
                    row.AddSpacer();
                });
                col.AddGroup(group =>
                {
                    group.Border(true);
                    group.Scrollbars(NuiScrollbars.Y);
                    group.SetLayout(col =>
                    {
                        BuildStatEntry(col, LocaleString.HP, model => model.HP, "icon_hp", new Color(255, 80, 80));
                        BuildStatEntry(col, LocaleString.HPRegen, model => model.HPRegen, "icon_hpregen", new Color(255, 80, 80));
                        BuildStatEntry(col, LocaleString.EP, model => model.EP, "icon_ep", new Color(0, 138, 250));
                        BuildStatEntry(col, LocaleString.EPRegen, model => model.EPRegen, "icon_epregen", new Color(0, 138, 250));
                        BuildStatEntry(col, LocaleString.Might, model => model.Might, "icon_might");
                        BuildStatEntry(col, LocaleString.Perception, model => model.Perception, "icon_perception");
                        BuildStatEntry(col, LocaleString.Vitality, model => model.Vitality, "icon_vitality");
                        BuildStatEntry(col, LocaleString.Willpower, model => model.Willpower, "icon_willpower");
                        BuildStatEntry(col, LocaleString.Agility, model => model.Agility, "icon_agility");
                        BuildStatEntry(col, LocaleString.Social, model => model.Social, "icon_social");
                        BuildStatEntry(col, LocaleString.RecastReduction, model => model.RecastReduction, "icon_recast");
                    });
                });

            }

            void BuildStatsColumn2(NuiColumnBuilder<CharacterSheetViewModel> col)
            {
                col.AddRow(row =>
                {
                    row.AddLabel(label =>
                    {
                        label
                            .Label(LocaleString.Combat)
                            .Height(20f)
                            .HorizontalAlign(NuiHAlign.Center)
                            .VerticalAlign(NuiVAlign.Middle);
                    });
                });
                col.AddGroup(group =>
                {
                    group.Border(true);
                    group.Scrollbars(NuiScrollbars.Y);
                    group.SetLayout(col =>
                    {
                        BuildStatEntry(col, LocaleString.MainHand, model => model.MainHand, "icon_mainhand");
                        BuildStatEntry(col, LocaleString.OffHand, model => model.OffHand, "icon_offhand");
                        BuildStatEntry(col, LocaleString.Attack, model => model.Attack, "icon_attack");
                        BuildStatEntry(col, LocaleString.EtherAttack, model => model.EtherAttack, "icon_ethattack");
                        BuildStatEntry(col, LocaleString.Accuracy, model => model.Accuracy, "icon_accuracy");
                        BuildStatEntry(col, LocaleString.Evasion, model => model.Evasion, "icon_evasion");
                        BuildStatEntry(col, LocaleString.Defense, model => model.Defense, "icon_defense");
                        BuildStatEntry(col, LocaleString.FireResist, model => model.FireResist, "resist_fire");
                        BuildStatEntry(col, LocaleString.IceResist, model => model.IceResist, "resist_ice");
                        BuildStatEntry(col, LocaleString.EarthResist, model => model.EarthResist, "resist_earth");
                        BuildStatEntry(col, LocaleString.WindResist, model => model.WindResist, "resist_wind");
                        BuildStatEntry(col, LocaleString.WaterResist, model => model.WaterResist, "resist_water");
                        BuildStatEntry(col, LocaleString.LightningResist, model => model.LightningResist, "resist_lightning");
                        BuildStatEntry(col, LocaleString.MindResist, model => model.MindResist, "resist_mind");
                        BuildStatEntry(col, LocaleString.LightResist, model => model.LightResist, "resist_light");
                        BuildStatEntry(col, LocaleString.DarknessResist, model => model.DarknessResist, "resist_darkness");
                    });
                });
            }

            group.SetLayout(layoutCol =>
            {
                layoutCol.AddRow(layoutRow =>
                {
                    layoutRow.AddColumn(BuildPortrait);
                    layoutRow.AddColumn(BuildStatsColumn1);
                    layoutRow.AddColumn(BuildStatsColumn2);
                });
                
            });
        }

        private void BuildSettingsPartial(NuiGroupBuilder<CharacterSheetViewModel> group)
        {
            group.SetLayout(layoutCol =>
            {
                layoutCol.AddRow(row =>
                {
                    row.AddSpacer();
                    row.AddCheck(check =>
                    {
                        check
                            .Label(LocaleString.DisplayServerResetReminders)
                            .Selected(model => model.IsDisplayServerResetRemindersChecked)
                            .OnMouseDown(model => model.OnClickDisplayServerReminders)
                            .Width(500f);

                    });
                    row.AddSpacer();
                });
            });
        }

        private void BuildJobPartial(NuiGroupBuilder<CharacterSheetViewModel> partial)
        {
            partial
                .Border(false)
                .Scrollbars(NuiScrollbars.Auto)
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
            const float ButtonWidth = 125f;

            group.Scrollbars(NuiScrollbars.Auto);
            group.Border(false);
            group.SetLayout(col =>
            {
                col.AddButton(button =>
                {
                    button
                        .Label(LocaleString.Appearance)
                        .Width(ButtonWidth)
                        .OnClick(model => model.OnClickAppearance);
                });
                col.AddButton(button =>
                {
                    button
                        .Label(LocaleString.Quests)
                        .Width(ButtonWidth)
                        .OnClick(model => model.OnClickQuests);
                });
                col.AddButton(button =>
                {
                    button
                        .Label(LocaleString.KeyItems)
                        .Width(ButtonWidth)
                        .OnClick(model => model.OnClickKeyItems);
                });
                col.AddButton(button =>
                {
                    button
                        .Label(LocaleString.OpenTrash)
                        .Width(ButtonWidth)
                        .OnClick(model => model.OnClickOpenTrash);
                });
            });
        }
    }
}
