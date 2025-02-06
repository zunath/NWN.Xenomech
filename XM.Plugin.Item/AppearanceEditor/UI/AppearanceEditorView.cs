using Anvil.API;
using System.Linq.Expressions;
using System;
using Anvil.Services;
using XM.Shared.API.Constants;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;
using XM.UI.Builder.Component;
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
                        .OnClick(model => model.OnSelectAppearance);
                });

                row.AddButtonSelect(button =>
                {
                    button
                        .Label(LocaleString.Equipment)
                        .Height(32f)
                        .IsSelected(model => model.IsEquipmentSelected)
                        .OnClick(model => model.OnSelectEquipment);
                });

                row.AddButtonSelect(button =>
                {
                    button
                        .Label(LocaleString.Settings)
                        .Height(32f)
                        .IsSelected(model => model.IsAppearanceSelected)
                        .OnClick(model => model.OnSelectAppearance)
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
                            .OnClick(model => model.OnClickOutfits);
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
                                                .OnClick(model => model.OnSelectColorCategory);
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
                                                .OnClick(model => model.OnSelectPartCategory);
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
                                    .OnMouseDown(model => model.OnSelectColor)
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
                                                .OnClick(model => model.OnSelectPart);
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
                                    .OnClick(model => model.OnPreviousPart);
                            });
                            row2.AddButton(button =>
                            {
                                button
                                    .Label(LocaleString.NextPart)
                                    .Height(32f)
                                    .Width(128f)
                                    .OnClick(model => model.OnNextPart);
                            });
                        });
                    });
                });
            });
        }
        private void BuildArmorEditor(NuiGroupBuilder<AppearanceEditorViewModel> partial)
        {
            partial.SetLayout(layout =>
            {

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
                            .OnClick(model => model.OnIncreaseAppearanceScale);
                    });
                    row.AddButton(button =>
                    {
                        button
                            .Label(LocaleString.DecreaseHeight)
                            .Height(32f)
                            .Width(128f)
                            .OnClick(model => model.OnDecreaseAppearanceScale);
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
                            .OnClick(model => model.OnClickSaveSettings);
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
