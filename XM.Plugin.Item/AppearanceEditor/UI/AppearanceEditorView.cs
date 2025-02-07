using Anvil.API;
using System.Linq.Expressions;
using System;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;
using XM.UI.Builder.Layout;
using Action = System.Action;
using NuiAspect = XM.Shared.API.NUI.NuiAspect;

namespace XM.Plugin.Item.AppearanceEditor.UI
{
    [ServiceBinding(typeof(IView))]
    internal class AppearanceEditorView : IView
    {
        private readonly NuiBuilder<AppearanceEditorViewModel> _builder = new();

        private const float MainColorChannelButtonSize = 72f;
        private const float PartColorChannelButtonSize = 16f;

        public IViewModel CreateViewModel()
        {
            return new AppearanceEditorViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
            {
                window
                    .IsResizable(true)
                    .IsCollapsible(WindowCollapsibleType.UserCollapsible)
                    .InitialGeometry(0f, 0f, 477f, 600f)
                    .Title(LocaleString.AppearanceEditor)

                    .DefinePartialView(AppearanceEditorViewModel.EditorHeaderPartial, BuildEditorHeader)

                    .DefinePartialView(AppearanceEditorViewModel.EditorMainPartial, BuildMainEditor)

                    .DefinePartialView(AppearanceEditorViewModel.EditorArmorPartial, BuildArmorEditor)

                    .DefinePartialView(AppearanceEditorViewModel.SettingsPartial, BuildSettings)

                    .DefinePartialView(AppearanceEditorViewModel.ArmorColorsClothLeather, partial =>
                    {
                        BuildColorPalette(partial, "gui_pal_tattoo");
                    })

                    .DefinePartialView(AppearanceEditorViewModel.ArmorColorsMetal, partial =>
                    {
                        BuildColorPalette(partial, "gui_pal_armor01");
                    })

                    .Root(BuildNavigation);

            }).Build();
        }

        private void BuildNavigation(NuiColumnBuilder<AppearanceEditorViewModel> col)
        {
            col.AddRow(row =>
            {
                row.AddSpacer();

                row.AddButtonSelect(button =>
                {
                    button
                        .Label(LocaleString.Appearance)
                        .Height(32f)
                        .IsSelected(model => model.IsAppearanceSelected)
                        .OnClick(model => model.OnSelectAppearance());
                });

                row.AddButtonSelect(button =>
                {
                    button
                        .Label(LocaleString.Equipment)
                        .Height(32f)
                        .IsSelected(model => model.IsEquipmentSelected)
                        .OnClick(model => model.OnSelectEquipment());
                });

                row.AddButtonSelect(button =>
                {
                    button
                        .Label(LocaleString.Settings)
                        .Height(32f)
                        .IsSelected(model => model.IsSettingsSelected)
                        .OnClick(model => model.OnSelectSettings())
                        .IsVisible(model => model.IsSettingsVisible);
                });

                row.AddSpacer();
            });

            col.AddRow(row =>
            {
                row.AddPartialPlaceholder(AppearanceEditorViewModel.MainPartialElement);
            });
        }

        private void BuildEditorHeader(NuiGroupBuilder<AppearanceEditorViewModel> partial)
        {
            partial.SetLayout(col =>
            {
                col.AddRow(row =>
                {
                    row.IsVisible(model => model.IsEquipmentSelected);

                    row.AddSpacer();

                    row.AddComboBox(comboBox =>
                    {
                        comboBox
                            .Option(LocaleString.Armor, 0)
                            .Option(LocaleString.Helmet, 1)
                            .Option(LocaleString.Cloak, 2)
                            .Option(LocaleString.WeaponMain, 3)
                            .Option(LocaleString.WeaponOff, 4)
                            .Selection(model => model.SelectedItemTypeIndex);
                    });

                    row.AddButton(button =>
                    {
                        button
                            .Label(LocaleString.Outfits)
                            .Height(32f)
                            .OnClick(model => model.OnClickOutfits());
                    });

                    row.AddSpacer();
                });

                col.AddRow(row =>
                {
                    row.AddPartialPlaceholder(AppearanceEditorViewModel.EditorPartialElement);
                });
            });
        }

        private void BuildMainEditor(NuiGroupBuilder<AppearanceEditorViewModel> partial)
        {
            partial.SetLayout(col =>
            {
                col.AddRow(row =>
                {
                    row.AddLabel(label =>
                    {
                        label
                            .Label(LocaleString.NoItemIsEquippedOrTheEquippedItemCannotBeModified)
                            .IsVisible(model => model.DoesNotHaveItemEquipped);
                    });
                    row.Height(20f);
                    row.IsVisible(model => model.DoesNotHaveItemEquipped);
                });

                col.AddRow(row =>
                {
                    row.IsVisible(model => model.HasItemEquipped);

                    row.AddColumn(col2 =>
                    {
                        col2.AddRow(row2 =>
                        {
                            row2.AddList(list =>
                            {
                                list.AddTemplateCell(cell =>
                                {
                                    cell.AddRow(row =>
                                    {
                                        row.AddButtonSelect(button =>
                                        {
                                            button
                                                .Label(model => model.ColorCategoryOptions)
                                                .IsSelected(model => model.ColorCategorySelected)
                                                .OnClick(model => model.OnSelectColorCategory());
                                        });
                                    });
                                });
                            }, model => model.ColorCategoryOptions);
                        });

                        col2.AddRow(row2 =>
                        {
                            row2.AddList(list =>
                            {
                                list.AddTemplateCell(cell =>
                                {
                                    cell.AddRow(row =>
                                    {
                                        row.AddButtonSelect(button =>
                                        {
                                            button
                                                .Label(model => model.PartCategoryOptions)
                                                .IsSelected(model => model.PartCategorySelected)
                                                .OnClick(model => model.OnSelectPartCategory());
                                        });
                                    });
                                });
                            }, model => model.PartCategoryOptions);
                        });
                    });

                    row.AddColumn(col2 =>
                    {
                        col2.AddRow(row2 =>
                        {
                            row2.AddImage(image =>
                            {
                                image
                                    .ResRef(model => model.ColorSheetResref)
                                    .Height(176f)
                                    .Width(256f)
                                    .VerticalAlign(NuiVAlign.Top)
                                    .HorizontalAlign(NuiHAlign.Left)
                                    .ImageAspect(NuiAspect.ExactScaled)
                                    .OnMouseDown(model => model.OnSelectColor())
                                    .IsVisible(model => model.IsColorPickerVisible);
                            });
                        });

                        col2.AddRow(row2 =>
                        {
                            row2.AddList(list =>
                            {
                                //list.Width(256f);
                                list.AddTemplateCell(cell =>
                                {
                                    cell.AddRow(cellRow =>
                                    {
                                        cellRow.AddButtonSelect(button =>
                                        {
                                            button
                                                .Label(model => model.PartOptions)
                                                .IsSelected(model => model.PartSelected)
                                                .OnClick(model => model.OnSelectPart());
                                        });
                                    });
                                });
                            }, model => model.PartOptions);
                        });

                        col2.AddRow(row2 =>
                        {
                            row2.AddButton(button =>
                            {
                                button
                                    .Label(LocaleString.PreviousPart)
                                    .Height(32f)
                                    .Width(128f)
                                    .OnClick(model => model.OnPreviousPart());
                            });
                            row2.AddButton(button =>
                            {
                                button
                                    .Label(LocaleString.NextPart)
                                    .Height(32f)
                                    .Width(128f)
                                    .OnClick(model => model.OnNextPart());
                            });
                        });
                    });
                });
            });
        }
        private void BuildArmorEditor(NuiGroupBuilder<AppearanceEditorViewModel> partial)
        {
            void BuildMainColorChannels(NuiColumnBuilder<AppearanceEditorViewModel> col)
            {
                col.AddRow(row =>
                {
                    row.AddLabel(label =>
                    {
                        label
                            .Height(20f)
                            .Width(MainColorChannelButtonSize)
                            .Label(LocaleString.Leather)
                            .HorizontalAlign(NuiHAlign.Center)
                            .VerticalAlign(NuiVAlign.Top);
                    });

                    row.AddLabel(label =>
                    {
                        label
                            .Height(20f)
                            .Width(MainColorChannelButtonSize)
                            .Label(LocaleString.Cloth)
                            .HorizontalAlign(NuiHAlign.Center)
                            .VerticalAlign(NuiVAlign.Top);
                    });

                    row.AddLabel(label =>
                    {
                        label
                            .Height(20f)
                            .Width(MainColorChannelButtonSize)
                            .Label(LocaleString.Metal)
                            .HorizontalAlign(NuiHAlign.Center)
                            .VerticalAlign(NuiVAlign.Top);
                    });
                });

                col.AddRow(row =>
                {
                    CreateFilledButton(
                        row,
                        "gui_pal_tattoo",
                        model => model.GlobalLeather1Region,
                        MainColorChannelButtonSize,
                        4f,
                        model => model.OnClickColorTarget(ColorTarget.Global, ItemAppearanceArmorColorType.Leather1),
                        model => model.OnClickClearColor(ColorTarget.Invalid, ItemAppearanceArmorColorType.Leather1));
                    CreateFilledButton(
                        row,
                        "gui_pal_tattoo",
                        model => model.GlobalCloth1Region,
                        MainColorChannelButtonSize,
                        4f,
                        model => model.OnClickColorTarget(ColorTarget.Global, ItemAppearanceArmorColorType.Cloth1),
                        model => model.OnClickClearColor(ColorTarget.Invalid, ItemAppearanceArmorColorType.Cloth1));
                    CreateFilledButton(
                        row,
                        "gui_pal_armor01",
                        model => model.GlobalMetal1Region,
                        MainColorChannelButtonSize,
                        4f,
                        model => model.OnClickColorTarget(ColorTarget.Global, ItemAppearanceArmorColorType.Metal1),
                        model => model.OnClickClearColor(ColorTarget.Invalid, ItemAppearanceArmorColorType.Metal1));
                });
                col.AddRow(row =>
                {
                    CreateFilledButton(
                        row,
                        "gui_pal_tattoo",
                        model => model.GlobalLeather2Region,
                        MainColorChannelButtonSize,
                        4f,
                        model => model.OnClickColorTarget(ColorTarget.Global, ItemAppearanceArmorColorType.Leather2),
                        model => model.OnClickClearColor(ColorTarget.Invalid, ItemAppearanceArmorColorType.Leather2));
                    CreateFilledButton(
                        row,
                        "gui_pal_tattoo",
                        model => model.GlobalCloth2Region,
                        MainColorChannelButtonSize,
                        4f,
                        model => model.OnClickColorTarget(ColorTarget.Global, ItemAppearanceArmorColorType.Cloth2),
                        model => model.OnClickClearColor(ColorTarget.Invalid, ItemAppearanceArmorColorType.Cloth2));
                    CreateFilledButton(
                        row,
                        "gui_pal_armor01",
                        model => model.GlobalMetal2Region,
                        MainColorChannelButtonSize,
                        4f,
                        model => model.OnClickColorTarget(ColorTarget.Global, ItemAppearanceArmorColorType.Metal2),
                        model => model.OnClickClearColor(ColorTarget.Invalid, ItemAppearanceArmorColorType.Metal2));
                });
                col.AddRow(row =>
                {
                    row.AddSpacer()
                        .Height(32f);
                });
            }

            void CreatePartEditor(
                NuiColumnBuilder<AppearanceEditorViewModel> col,
                LocaleString partName,
                ItemAppearanceArmorModelType partType,
                ColorTarget colorTarget,
                Expression<Func<AppearanceEditorViewModel, XMBindingList<NuiComboEntry>>> optionsBinding,
                Expression<Func<AppearanceEditorViewModel, int>> selectionBinding,
                Expression<Func<AppearanceEditorViewModel, NuiRect>> leather1RegionBinding,
                Expression<Func<AppearanceEditorViewModel, NuiRect>> leather2RegionBinding,
                Expression<Func<AppearanceEditorViewModel, NuiRect>> cloth1RegionBinding,
                Expression<Func<AppearanceEditorViewModel, NuiRect>> cloth2RegionBinding,
                Expression<Func<AppearanceEditorViewModel, NuiRect>> metal1RegionBinding,
                Expression<Func<AppearanceEditorViewModel, NuiRect>> metal2RegionBinding)
            {
                col.AddRow(row =>
                {
                    row.AddLabel(label =>
                    {
                        label
                            .Label(partName)
                            .Height(PartColorChannelButtonSize)
                            .HorizontalAlign(NuiHAlign.Center)
                            .VerticalAlign(NuiVAlign.Middle);
                    });
                });

                col.AddRow(row =>
                {
                    row.AddButton(button =>
                    {
                        button
                            .Label(LocaleString.LessThanSymbol)
                            .Height(24f)
                            .Width(24f)
                            .Margin(0f)
                            .OnClick(model => model.OnClickAdjustArmorPart(partType, -1));
                    });

                    row.AddComboBox(button =>
                    {
                        button
                            .Height(24f)
                            .Width(100f)
                            .Margin(0f)
                            .Option(optionsBinding)
                            .Selection(selectionBinding);
                    });

                    row.AddButton(button =>
                    {
                        button
                            .Label(LocaleString.GreaterThanSymbol)
                            .Height(24f)
                            .Width(24f)
                            .Margin(0f)
                            .OnClick(model => model.OnClickAdjustArmorPart(partType, 1));
                    });
                        
                });

                col.AddRow(row =>
                {
                    row.AddSpacer();
                    CreateFilledButton(
                        row,
                        "gui_pal_tattoo",
                        leather1RegionBinding,
                        PartColorChannelButtonSize,
                        2f,
                        model => model.OnClickColorTarget(colorTarget, ItemAppearanceArmorColorType.Leather1),
                        model => model.OnClickClearColor(colorTarget, ItemAppearanceArmorColorType.Leather1));
                    CreateFilledButton(
                        row,
                        "gui_pal_tattoo",
                        cloth1RegionBinding,
                        PartColorChannelButtonSize,
                        2f,
                        model => model.OnClickColorTarget(colorTarget, ItemAppearanceArmorColorType.Cloth1),
                        model => model.OnClickClearColor(colorTarget, ItemAppearanceArmorColorType.Cloth1));
                    CreateFilledButton(
                        row,
                        "gui_pal_armor01",
                        metal1RegionBinding,
                        PartColorChannelButtonSize,
                        2f,
                        model => model.OnClickColorTarget(colorTarget, ItemAppearanceArmorColorType.Metal1),
                        model => model.OnClickClearColor(colorTarget, ItemAppearanceArmorColorType.Metal1));
                    row.AddSpacer();
                });

                col.AddRow(row =>
                {
                    row.AddSpacer();
                    CreateFilledButton(
                        row,
                        "gui_pal_tattoo",
                        leather2RegionBinding,
                        PartColorChannelButtonSize,
                        2f,
                        model => model.OnClickColorTarget(colorTarget, ItemAppearanceArmorColorType.Leather2),
                        model => model.OnClickClearColor(colorTarget, ItemAppearanceArmorColorType.Leather2));
                    CreateFilledButton(
                        row,
                        "gui_pal_tattoo",
                        cloth2RegionBinding,
                        PartColorChannelButtonSize,
                        2f,
                        model => model.OnClickColorTarget(colorTarget, ItemAppearanceArmorColorType.Cloth2),
                        model => model.OnClickClearColor(colorTarget, ItemAppearanceArmorColorType.Cloth2));
                    CreateFilledButton(
                        row,
                        "gui_pal_armor01",
                        metal2RegionBinding,
                        PartColorChannelButtonSize,
                        2f,
                        model => model.OnClickColorTarget(colorTarget, ItemAppearanceArmorColorType.Metal2),
                        model => model.OnClickClearColor(colorTarget, ItemAppearanceArmorColorType.Metal2));
                    row.AddSpacer();
                });
            }

            void CreateGap(NuiRowBuilder<AppearanceEditorViewModel> mainRow)
            {
                mainRow.AddColumn(col =>
                {
                    for (var x = 1; x <= 7; x++)
                    {
                        col.AddRow(row =>
                        {
                            row.AddSpacer()
                                .Width(6f)
                                .Height(6f);
                        });
                    }
                });
            }


            void BuildParts(NuiRowBuilder<AppearanceEditorViewModel> mainRow)
            {
                mainRow.AddColumn(col =>
                {
                    CreatePartEditor(
                        col,
                        LocaleString.LeftShoulder,
                        ItemAppearanceArmorModelType.LeftShoulder,
                        ColorTarget.LeftShoulder,
                        model => model.LeftShoulderOptions,
                        model => model.LeftShoulderSelection,
                        model => model.LeftShoulderLeather1Region,
                        model => model.LeftShoulderLeather2Region,
                        model => model.LeftShoulderCloth1Region,
                        model => model.LeftShoulderCloth2Region,
                        model => model.LeftShoulderMetal1Region,
                        model => model.LeftShoulderMetal2Region);
                    CreatePartEditor(
                        col,
                        LocaleString.LeftBicep,
                        ItemAppearanceArmorModelType.LeftBicep,
                        ColorTarget.LeftBicep,
                        model => model.LeftBicepOptions,
                        model => model.LeftBicepSelection,
                        model => model.LeftBicepLeather1Region,
                        model => model.LeftBicepLeather2Region,
                        model => model.LeftBicepCloth1Region,
                        model => model.LeftBicepCloth2Region,
                        model => model.LeftBicepMetal1Region,
                        model => model.LeftBicepMetal2Region);
                    CreatePartEditor(
                        col,
                        LocaleString.LeftForearm,
                        ItemAppearanceArmorModelType.LeftForearm,
                        ColorTarget.LeftForearm,
                        model => model.LeftForearmOptions,
                        model => model.LeftForearmSelection,
                        model => model.LeftForearmLeather1Region,
                        model => model.LeftForearmLeather2Region,
                        model => model.LeftForearmCloth1Region,
                        model => model.LeftForearmCloth2Region,
                        model => model.LeftForearmMetal1Region,
                        model => model.LeftForearmMetal2Region);
                    CreatePartEditor(
                        col,
                        LocaleString.LeftHand,
                        ItemAppearanceArmorModelType.LeftHand,
                        ColorTarget.LeftHand,
                        model => model.LeftHandOptions,
                        model => model.LeftHandSelection,
                        model => model.LeftHandLeather1Region,
                        model => model.LeftHandLeather2Region,
                        model => model.LeftHandCloth1Region,
                        model => model.LeftHandCloth2Region,
                        model => model.LeftHandMetal1Region,
                        model => model.LeftHandMetal2Region);
                    CreatePartEditor(
                        col,
                        LocaleString.LeftThigh,
                        ItemAppearanceArmorModelType.LeftThigh,
                        ColorTarget.LeftThigh,
                        model => model.LeftThighOptions,
                        model => model.LeftThighSelection,
                        model => model.LeftThighLeather1Region,
                        model => model.LeftThighLeather2Region,
                        model => model.LeftThighCloth1Region,
                        model => model.LeftThighCloth2Region,
                        model => model.LeftThighMetal1Region,
                        model => model.LeftThighMetal2Region);
                    CreatePartEditor(
                        col,
                        LocaleString.LeftShin,
                        ItemAppearanceArmorModelType.LeftShin,
                        ColorTarget.LeftShin,
                        model => model.LeftShinOptions,
                        model => model.LeftShinSelection,
                        model => model.LeftShinLeather1Region,
                        model => model.LeftShinLeather2Region,
                        model => model.LeftShinCloth1Region,
                        model => model.LeftShinCloth2Region,
                        model => model.LeftShinMetal1Region,
                        model => model.LeftShinMetal2Region);
                    CreatePartEditor(
                        col,
                        LocaleString.LeftFoot,
                        ItemAppearanceArmorModelType.LeftFoot,
                        ColorTarget.LeftFoot,
                        model => model.LeftFootOptions,
                        model => model.LeftFootSelection,
                        model => model.LeftFootLeather1Region,
                        model => model.LeftFootLeather2Region,
                        model => model.LeftFootCloth1Region,
                        model => model.LeftFootCloth2Region,
                        model => model.LeftFootMetal1Region,
                        model => model.LeftFootMetal2Region);
                });

                CreateGap(mainRow);

                mainRow.AddColumn(col =>
                {
                    CreatePartEditor(
                        col,
                        LocaleString.Neck,
                        ItemAppearanceArmorModelType.Neck,
                        ColorTarget.Neck,
                        model => model.NeckOptions,
                        model => model.NeckSelection,
                        model => model.NeckLeather1Region,
                        model => model.NeckLeather2Region,
                        model => model.NeckCloth1Region,
                        model => model.NeckCloth2Region,
                        model => model.NeckMetal1Region,
                        model => model.NeckMetal2Region);
                    CreatePartEditor(
                        col,
                        LocaleString.Chest,
                        ItemAppearanceArmorModelType.Torso,
                        ColorTarget.Chest,
                        model => model.ChestOptions,
                        model => model.ChestSelection,
                        model => model.ChestLeather1Region,
                        model => model.ChestLeather2Region,
                        model => model.ChestCloth1Region,
                        model => model.ChestCloth2Region,
                        model => model.ChestMetal1Region,
                        model => model.ChestMetal2Region);
                    CreatePartEditor(
                        col,
                        LocaleString.Belt,
                        ItemAppearanceArmorModelType.Belt,
                        ColorTarget.Belt,
                        model => model.BeltOptions,
                        model => model.BeltSelection,
                        model => model.BeltLeather1Region,
                        model => model.BeltLeather2Region,
                        model => model.BeltCloth1Region,
                        model => model.BeltCloth2Region,
                        model => model.BeltMetal1Region,
                        model => model.BeltMetal2Region);
                    CreatePartEditor(
                        col,
                        LocaleString.Pelvis,
                        ItemAppearanceArmorModelType.Pelvis,
                        ColorTarget.Pelvis,
                        model => model.PelvisOptions,
                        model => model.PelvisSelection,
                        model => model.PelvisLeather1Region,
                        model => model.PelvisLeather2Region,
                        model => model.PelvisCloth1Region,
                        model => model.PelvisCloth2Region,
                        model => model.PelvisMetal1Region,
                        model => model.PelvisMetal2Region);
                    CreatePartEditor(
                        col,
                        LocaleString.Robe,
                        ItemAppearanceArmorModelType.Robe,
                        ColorTarget.Robe,
                        model => model.RobeOptions,
                        model => model.RobeSelection,
                        model => model.RobeLeather1Region,
                        model => model.RobeLeather2Region,
                        model => model.RobeCloth1Region,
                        model => model.RobeCloth2Region,
                        model => model.RobeMetal1Region,
                        model => model.RobeMetal2Region);

                    col.AddRow(row =>
                    {
                        row.AddSpacer();
                    });
                    col.AddRow(row =>
                    {
                        row.AddSpacer();
                    });
                    col.AddRow(row =>
                    {
                        row.AddSpacer();
                    });
                });

                CreateGap(mainRow);

                mainRow.AddColumn(col =>
                {
                    CreatePartEditor(
                        col,
                        LocaleString.RightShoulder,
                        ItemAppearanceArmorModelType.RightShoulder,
                        ColorTarget.RightShoulder,
                        model => model.RightShoulderOptions,
                        model => model.RightShoulderSelection,
                        model => model.RightShoulderLeather1Region,
                        model => model.RightShoulderLeather2Region,
                        model => model.RightShoulderCloth1Region,
                        model => model.RightShoulderCloth2Region,
                        model => model.RightShoulderMetal1Region,
                        model => model.RightShoulderMetal2Region);
                    CreatePartEditor(
                        col,
                        LocaleString.RightBicep,
                        ItemAppearanceArmorModelType.RightBicep,
                        ColorTarget.RightBicep,
                        model => model.RightBicepOptions,
                        model => model.RightBicepSelection,
                        model => model.RightBicepLeather1Region,
                        model => model.RightBicepLeather2Region,
                        model => model.RightBicepCloth1Region,
                        model => model.RightBicepCloth2Region,
                        model => model.RightBicepMetal1Region,
                        model => model.RightBicepMetal2Region);
                    CreatePartEditor(
                        col,
                        LocaleString.RightForearm,
                        ItemAppearanceArmorModelType.RightForearm,
                        ColorTarget.RightForearm,
                        model => model.RightForearmOptions,
                        model => model.RightForearmSelection,
                        model => model.RightForearmLeather1Region,
                        model => model.RightForearmLeather2Region,
                        model => model.RightForearmCloth1Region,
                        model => model.RightForearmCloth2Region,
                        model => model.RightForearmMetal1Region,
                        model => model.RightForearmMetal2Region);
                    CreatePartEditor(
                        col,
                        LocaleString.RightHand,
                        ItemAppearanceArmorModelType.RightHand,
                        ColorTarget.RightHand,
                        model => model.RightHandOptions,
                        model => model.RightHandSelection,
                        model => model.RightHandLeather1Region,
                        model => model.RightHandLeather2Region,
                        model => model.RightHandCloth1Region,
                        model => model.RightHandCloth2Region,
                        model => model.RightHandMetal1Region,
                        model => model.RightHandMetal2Region);
                    CreatePartEditor(
                        col,
                        LocaleString.RightThigh,
                        ItemAppearanceArmorModelType.RightThigh,
                        ColorTarget.RightThigh,
                        model => model.RightThighOptions,
                        model => model.RightThighSelection,
                        model => model.RightThighLeather1Region,
                        model => model.RightThighLeather2Region,
                        model => model.RightThighCloth1Region,
                        model => model.RightThighCloth2Region,
                        model => model.RightThighMetal1Region,
                        model => model.RightThighMetal2Region);
                    CreatePartEditor(
                        col,
                        LocaleString.RightShin,
                        ItemAppearanceArmorModelType.RightShin,
                        ColorTarget.RightShin,
                        model => model.RightShinOptions,
                        model => model.RightShinSelection,
                        model => model.RightShinLeather1Region,
                        model => model.RightShinLeather2Region,
                        model => model.RightShinCloth1Region,
                        model => model.RightShinCloth2Region,
                        model => model.RightShinMetal1Region,
                        model => model.RightShinMetal2Region);
                    CreatePartEditor(
                        col,
                        LocaleString.RightFoot,
                        ItemAppearanceArmorModelType.RightFoot,
                        ColorTarget.RightFoot,
                        model => model.RightFootOptions,
                        model => model.RightFootSelection,
                        model => model.RightFootLeather1Region,
                        model => model.RightFootLeather2Region,
                        model => model.RightFootCloth1Region,
                        model => model.RightFootCloth2Region,
                        model => model.RightFootMetal1Region,
                        model => model.RightFootMetal2Region);
                });
            }

            void BuildFooter(NuiRowBuilder<AppearanceEditorViewModel> mainRow)
            {
                mainRow.AddColumn(mainCol =>
                {
                    mainCol.AddRow(row =>
                    {
                        row.AddButton(button =>
                        {
                            button
                                .Label(LocaleString.CopyToRightArrow)
                                .IsEnabled(model => model.IsCopyEnabled)
                                .OnClick(model => model.OnClickCopyToRight());

                        });
                    });
                });

                mainRow.AddColumn(mainCol =>
                {
                    mainCol.AddRow(row =>
                    {
                        row.AddSpacer();
                    });
                });

                mainRow.AddColumn(mainCol =>
                {
                    mainCol.AddRow(row =>
                    {
                        row.AddButton(button =>
                        {
                            button
                                .Label(LocaleString.CopyToLeftArrow)
                                .IsEnabled(model => model.IsCopyEnabled)
                                .OnClick(model => model.OnClickCopyToLeft());
                        });
                    });
                });
            }


            partial.SetLayout(mainCol =>
            {
                mainCol.AddRow(row =>
                {
                    row.AddLabel(label =>
                    {
                        label
                            .Label(LocaleString.NoItemIsEquippedOrTheEquippedItemCannotBeModified)
                            .IsVisible(model => model.DoesNotHaveItemEquipped);
                    });

                    row.Height(20f);
                    row.IsVisible(model => model.DoesNotHaveItemEquipped);
                });

                mainCol.AddRow(mainRow =>
                {
                    mainRow.IsVisible(model => model.HasItemEquipped);
                    mainRow.AddGroup(group =>
                    {
                        group.Border(false);
                        group.SetLayout(col =>
                        {
                            col.AddRow(row =>
                            {
                                row.AddLabel(label =>
                                {
                                    label
                                        .Label(LocaleString.SpaceCharacter)
                                        .Height(20f)
                                        .Width(MainColorChannelButtonSize);
                                });
                            });

                            col.AddRow(row =>
                            {
                                row.AddColumn(col2 =>
                                {
                                    col2.AddRow(row2 =>
                                    {
                                        row2.AddPartialPlaceholder(AppearanceEditorViewModel.ArmorColorElement);
                                    });
                                });

                                row.AddColumn(BuildMainColorChannels);
                            });

                            col.AddRow(row =>
                            {
                                BuildParts(row);
                                row.AddSpacer();
                            });

                            col.AddRow(row =>
                            {
                                BuildFooter(row);
                                row.AddSpacer();
                            });
                        });
                    });
                });
            });
        }


        private void CreateFilledButton(
            NuiRowBuilder<AppearanceEditorViewModel> component,
            string texture,
            Expression<Func<AppearanceEditorViewModel, NuiRect>> regionBind,
            float buttonSize,
            float drawOffset,
            Expression<Func<AppearanceEditorViewModel, Action>> onClickBind,
            Expression<Func<AppearanceEditorViewModel, Action>> onClickClearColor,
            NuiRect? staticRegion = null)
        {
            component.AddButton(button =>
            {
                button
                    .Label(LocaleString.Empty)
                    .Width(buttonSize)
                    .Height(buttonSize)
                    .Margin(0f)
                    .IsEncouraged(true)
                    .DrawList(drawList =>
                    {
                        drawList.AddImage(image =>
                        {
                            image
                                .ResRef(texture)
                                .Position(drawOffset, drawOffset, buttonSize - drawOffset * 2f,
                                    buttonSize - drawOffset * 2f)
                                .Aspect(NuiAspect.Stretch)
                                .HorizontalAlign(NuiHAlign.Left)
                                .VerticalAlign(NuiVAlign.Top);

                            if (regionBind != null)
                            {
                                image.DrawTextureRegion(regionBind);
                            }

                            if (staticRegion != null)
                            {
                                image.DrawTextureRegion((NuiRect)staticRegion);
                            }
                        });
                    })
                    .OnClick(onClickBind)
                    .OnMouseDown(onClickClearColor);

            });
        }

        private void BuildSettings(NuiGroupBuilder<AppearanceEditorViewModel> partial)
        {
            partial.SetLayout(col =>
            {
                col.AddRow(row =>
                {
                    row.AddSpacer();
                    row.AddCheck(check =>
                    {
                        check
                            .Label(LocaleString.ShowHelmet)
                            .Height(26f)
                            .Selected(model => model.ShowHelmet);
                    });
                    row.AddSpacer();
                });
                col.AddRow(row =>
                {
                    row.AddSpacer();
                    row.AddCheck(check =>
                    {
                        check
                            .Label(LocaleString.ShowCloak)
                            .Height(26f)
                            .Selected(model => model.ShowCloak);
                    });
                    row.AddSpacer();
                });

                col.AddRow(row =>
                {
                    row.AddSpacer();

                    row.AddButton(button =>
                    {
                        button
                            .Label(LocaleString.IncreaseHeight)
                            .Height(32f)
                            .Width(128f)
                            .OnClick(model => model.OnIncreaseAppearanceScale());
                    });
                    row.AddButton(button =>
                    {
                        button
                            .Label(LocaleString.DecreaseHeight)
                            .Height(32f)
                            .Width(128f)
                            .OnClick(model => model.OnDecreaseAppearanceScale());
                    });

                    row.AddSpacer();
                });

                col.AddRow(row =>
                {
                    row.AddSpacer();
                    row.AddButton(button =>
                    {
                        button
                            .Label(LocaleString.Save)
                            .Height(32f)
                            .OnClick(model => model.OnClickSaveSettings());
                    });
                    row.AddSpacer();
                });
            });
        }

        private void BuildColorPalette(NuiGroupBuilder<AppearanceEditorViewModel> group, string texture)
        {
            group.SetLayout(col =>
            {
                col.AddRow(row =>
                {
                    row.AddLabel(label =>
                    {
                        label
                            .Height(20f)
                            .Label(model => model.ColorTargetText)
                            .IsVisible(model => model.IsEquipmentSelected);
                    });
                });


                const int UIColorsPerRow = 20;
                const int ColorTotalCount = 176;
                const int RowCount = 1 + ColorTotalCount / UIColorsPerRow;

                for (var y = 0; y < RowCount; ++y)
                {
                    var yCopy = y;
                    col.AddRow(uiRow =>
                    {
                        for (var x = 0; x < UIColorsPerRow; ++x)
                        {
                            var paletteIndex = yCopy * UIColorsPerRow + x;
                            if (paletteIndex >= ColorTotalCount)
                                break;

                            var row = paletteIndex / AppearanceEditorViewModel.TextureColorsPerRow;
                            var offset = paletteIndex % AppearanceEditorViewModel.TextureColorsPerRow;

                            var region = new NuiRect(
                                offset * AppearanceEditorViewModel.ColorSize + 2,
                                row * AppearanceEditorViewModel.ColorSize + 2,
                                AppearanceEditorViewModel.ColorSize - 4,
                                AppearanceEditorViewModel.ColorSize - 4);

                            CreateFilledButton(
                                uiRow,
                                texture,
                                null,
                                PartColorChannelButtonSize,
                                2f,
                                model => model.OnClickColorPalette(paletteIndex),
                                model => model.OnClickClearColor(ColorTarget.Invalid, ItemAppearanceArmorColorType.Cloth1),
                                region);
                        }
                    });
                }
            });
        }
    }
}
