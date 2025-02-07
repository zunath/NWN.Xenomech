using Anvil.Services;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;

namespace XM.Plugin.Item.OutfitEditor.UI
{
    [ServiceBinding(typeof(IView))]
    internal class OutfitEditorView: IView
    {
        private readonly NuiBuilder<OutfitEditorViewModel> _builder = new();

        public IViewModel CreateViewModel()
        {
            return new OutfitEditorViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
            {
                window
                    .IsResizable(true)
                    .IsCollapsible(WindowCollapsibleType.UserCollapsible)
                    .InitialGeometry(0f, 0f, 641f, 396f)
                    .Title(LocaleString.Outfits)

                    .Root(rootCol =>
                    {
                        rootCol.AddRow(rootRow =>
                        {
                            rootRow.AddColumn(col =>
                            {
                                col.AddRow(row =>
                                {
                                    row.AddList(list =>
                                    {
                                        list.AddTemplateCell(cell =>
                                        {
                                            cell.AddRow(row =>
                                            {
                                                row.AddButtonSelect(button =>
                                                {
                                                    button
                                                        .IsSelected(model => model.SlotToggles)
                                                        .Label(model => model.SlotNames)
                                                        .OnClick(model => model.OnClickSlot());
                                                });
                                            });
                                        });
                                    }, model => model.SlotNames);
                                });

                                col.AddRow(row =>
                                {
                                    row.AddButton(button =>
                                    {
                                        button
                                            .Label(LocaleString.New)
                                            .Height(35f)
                                            .OnClick(model => model.OnClickNew());
                                    });

                                    row.AddButton(button =>
                                    {
                                        button
                                            .Label(LocaleString.Delete)
                                            .Height(35f)
                                            .OnClick(model => model.OnClickDelete());
                                    });
                                });
                            });

                            rootRow.AddColumn(col =>
                            {
                                col.IsVisible(model => model.IsSlotLoaded);

                                col.AddRow(row =>
                                {
                                    row.AddSpacer();

                                    row.AddTextEdit(textEdit =>
                                    {
                                        textEdit
                                            .Placeholder(LocaleString.Name)
                                            .MaxLength(32)
                                            .Value(model => model.Name);
                                    });

                                    row.AddButton(button =>
                                    {
                                        button
                                            .Label(LocaleString.Save)
                                            .Height(35f)
                                            .IsEnabled(model => model.IsSaveEnabled)
                                            .OnClick(model => model.OnClickSave());
                                    });

                                    row.AddSpacer();
                                });

                                col.AddRow(row =>
                                {
                                    row.AddText(text =>
                                    {
                                        text
                                            .Text(model => model.Details);
                                    });
                                });

                                col.AddRow(row =>
                                {
                                    row.AddSpacer();

                                    row.AddButton(button =>
                                    {
                                        button
                                            .Label(LocaleString.StoreOutfit)
                                            .Height(35f)
                                            .OnClick(model => model.OnClickStoreOutfit());
                                    });

                                    row.AddButton(button =>
                                    {
                                        button
                                            .Label(LocaleString.LoadOutfit)
                                            .Height(35f)
                                            .OnClick(model => model.OnClickLoadOutfit());
                                    });

                                    row.AddSpacer();
                                });
                            });
                        });

                    });

            }).Build();
        }
    }
}
