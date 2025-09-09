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
                                        .VerticalAlign(NuiVAlign.Top)
                                        .OnMouseDown(model => model.OnClickMechOutline());
                                })
                                .DrawList(drawList =>
                                {
                                    drawList.AddImage(image =>
                                    {
                                        image.ResRef(model => model.MechHeadResref)
                                            .Position(CustomizeMechViewModel.HeadCoordinates);
                                    });

                                    drawList.AddImage(button =>
                                    {
                                        button.ResRef(model => model.MechLeftArmResref)
                                            .Position(CustomizeMechViewModel.LeftArmCoordinates);
                                    });

                                    drawList.AddImage(button =>
                                    {
                                        button.ResRef(model => model.MechRightArmResref)
                                            .Position(CustomizeMechViewModel.RightArmCoordinates);
                                    });

                                    drawList.AddImage(button =>
                                    {
                                        button.ResRef(model => model.MechCoreResref)
                                            .Position(CustomizeMechViewModel.CoreCoordinates);
                                    });

                                    drawList.AddImage(button =>
                                    {
                                        button.ResRef(model => model.MechLegsResref)
                                            .Position(CustomizeMechViewModel.LegsCoordinates);
                                    });

                                    drawList.AddImage(button =>
                                    {
                                        button.ResRef(model => model.MechGeneratorResref)
                                            .Position(CustomizeMechViewModel.GeneratorCoordinates);
                                    });

                                    drawList.AddImage(button =>
                                    {
                                        button.ResRef(model => model.MechLeftWeaponResref)
                                            .Position(CustomizeMechViewModel.LeftWeaponCoordinates);
                                    });

                                    drawList.AddImage(button =>
                                    {
                                        button.ResRef(model => model.MechRightWeaponResref)
                                            .Position(CustomizeMechViewModel.RightWeaponCoordinates);
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
