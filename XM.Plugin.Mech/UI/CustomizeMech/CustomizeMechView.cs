using Anvil.API;
using Anvil.Services;
using NLog.Layouts;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;
using XM.UI.Builder.Layout;
using NuiAspect = XM.Shared.API.NUI.NuiAspect;
using NuiScrollbars = XM.Shared.API.NUI.NuiScrollbars;

namespace XM.Plugin.Mech.UI.CustomizeMech
{
    [ServiceBinding(typeof(IView))]
    internal class CustomizeMechView : IView
    {
        private readonly NuiBuilder<CustomizeMechViewModel> _builder = new();
        public IViewModel CreateViewModel()
        {
            return new CustomizeMechViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
            {
                window.IsResizable(true)
                    .IsCollapsible(WindowCollapsibleType.UserCollapsible)
                    .InitialGeometry(0, 0, 545f, 295f)
                    .Title(LocaleString.CustomizeMechs)
                    .Root(root =>
                    {
                        root.AddRow(row =>
                        {
                            row.AddColumn(BuildMechList);
                            row.AddColumn(BuildMechCustomizationPane);
                        });
                    });

            }).Build();
        }

        private void BuildMechList(NuiColumnBuilder<CustomizeMechViewModel> col)
        {
            col.AddRow(row =>
            {
                row.AddLabel(label =>
                {
                    label.Label(model => model.MechCountText)
                        .Height(25f)
                        .HorizontalAlign(NuiHAlign.Center);
                });
            });

            col.AddRow(row =>
            {
                row.AddList(list =>
                {
                    list.AddTemplateCell(cell =>
                    {
                        cell.AddRow(cellRow =>
                        {
                            cellRow.AddButtonSelect(button =>
                            {
                                button
                                    .IsSelected(model => model.MechToggles)
                                    .Label(model => model.MechNames)
                                    .TooltipText(model => model.MechNames)
                                    .OnClick(model => model.OnSelectMech());
                            });
                        });
                    });
                }, model => model.MechNames);
            });

            col.AddRow(row =>
            {
                row.AddButton(button =>
                {
                    button.Label(LocaleString.New)
                        .Height(32f)
                        .OnClick(model => model.OnNewMech());
                });
                row.AddButton(button =>
                {
                    button.Label(LocaleString.Delete)
                        .Height(32f)
                        .OnClick(model => model.OnDeleteMech());
                });
            });
        }

        private void BuildMechCustomizationPane(NuiColumnBuilder<CustomizeMechViewModel> col)
        {
            col.AddRow(row =>
            {
                row.AddGroup(group =>
                {
                    group.SetLayout(layout =>
                    {

                        layout.AddImage(image =>
                        {
                            image.ResRef("mech_outline1")
                                .ImageAspect(NuiAspect.Exact)
                                .HorizontalAlign(NuiHAlign.Left)
                                .VerticalAlign(NuiVAlign.Top);
                        })
                            .DrawList(drawList =>
                            {
                                drawList.AddImage(image =>
                                {
                                    image.ResRef("mech_head")
                                        .Position(320f, 50f, 128f, 128f);
                                    //.OnClick(model => model.OnSelectHead())
                                    //.TooltipText(LocaleString.Head);
                                });

                                drawList.AddImage(button =>
                                {
                                    button.ResRef("mech_larm")
                                        .Position(110f, 200f, 128f, 128f);
                                    //.OnClick(model => model.OnSelectLeftArm())
                                    //.TooltipText(LocaleString.MechLeftArm);
                                });

                                drawList.AddImage(button =>
                                {
                                    button.ResRef("mech_rarm")
                                        .Position(530f, 200f, 128f, 128f);
                                    //.OnClick(model => model.OnSelectRightArm())
                                    //.TooltipText(LocaleString.MechRightArm);
                                });

                                drawList.AddImage(button =>
                                {
                                    button.ResRef("mech_core")
                                        .Position(320f, 200f, 128f, 128f);
                                    //.OnClick(model => model.OnSelectCore())
                                    //.TooltipText(LocaleString.MechCore);
                                });

                                drawList.AddImage(button =>
                                {
                                    button.ResRef("mech_legs")
                                        .Position(320f, 500f, 128f, 128f);
                                    //.OnClick(model => model.OnSelectLegs())
                                    //.TooltipText(LocaleString.MechLegs);
                                });

                                drawList.AddImage(button =>
                                {
                                    button.ResRef("mech_generator")
                                        .Position(320f, 350f, 128f, 128f);
                                    //.OnClick(model => model.OnSelectGenerator())
                                    //.TooltipText(LocaleString.MechGenerator);
                                });

                                drawList.AddImage(button =>
                                {
                                    button.ResRef("mech_lweapon")
                                        .Position(110f, 350f, 128f, 128f);
                                    //.OnClick(model => model.OnSelectLeftWeapon())
                                    //.TooltipText(LocaleString.MechLeftWeapon);
                                });

                                drawList.AddImage(button =>
                                {
                                    button.ResRef("mech_rweapon")
                                        .Position(530f, 350f, 128f, 128f);
                                    //.OnClick(model => model.OnSelectRightWeapon())
                                    //.TooltipText(LocaleString.MechRightWeapon);
                                });
                            });
                    })
                        .Border(true)
                        .Scrollbars(NuiScrollbars.Both);
                });

            });
        }

        private void BuildMechSummaryPane(NuiColumnBuilder<CustomizeMechViewModel> col)
        {
            col.AddSpacer();
        }

    }
}
