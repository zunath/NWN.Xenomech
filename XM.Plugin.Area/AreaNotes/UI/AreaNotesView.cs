using Anvil.Services;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;

namespace XM.Plugin.Area.AreaNotes.UI
{
    [ServiceBinding(typeof(IView))]
    internal class AreaNotesView : IView
    {
        private readonly NuiBuilder<AreaNotesViewModel> _builder = new();

        public IViewModel CreateViewModel()
        {
            return new AreaNotesViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
            {
                window
                    .IsResizable(true)
                    .IsCollapsible(WindowCollapsibleType.UserCollapsible)
                    .InitialGeometry(0f, 0f, 1000f, 600f)
                    .Title(LocaleString.AreaNotes)

                    .Root(col =>
                    {
                        col.AddRow(row =>
                        {
                            row.AddTextEdit(textEdit =>
                            {
                                textEdit
                                    .Placeholder(LocaleString.Search)
                                    .Value(model => model.SearchText);
                            });
                            row.AddButton(button =>
                            {
                                button
                                    .Label(LocaleString.X)
                                    .Height(35f)
                                    .Width(35f)
                                    .OnClick(model => model.OnClickClearSearch);
                            });
                            row.AddButton(button =>
                            {
                                button
                                    .Label(LocaleString.Search)
                                    .Height(35f)
                                    .OnClick(model => model.OnClickSearch);
                            });
                        });

                        col.AddRow(row =>
                        {
                            row.AddColumn(colAreas =>
                            {
                                colAreas
                                    .AddRow(row =>
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
                                                            .Label(model => model.AreaNames)
                                                            .IsSelected(model => model.AreaToggled)
                                                            .OnClick(model => model.OnSelectNote);
                                                    });
                                                });
                                            });

                                        }, model => model.AreaNames);
                                    });
                            });

                            row.AddColumn(colAreas =>
                            {
                                colAreas
                                    .Height(300f)
                                    .AddRow(row =>
                                    {
                                        row.AddTextEdit(textEdit =>
                                        {
                                            textEdit
                                                .IsMultiLine(true)
                                                .MaxLength(AreaNotesViewModel.MaxNoteLength)
                                                .Value(model => model.PrivateText)
                                                .Height(205f)
                                                .Placeholder(LocaleString.DMOnlyNotes);
                                        });
                                    });

                                colAreas.AddRow(row =>
                                {
                                    row.AddTextEdit(textEdit =>
                                    {
                                        textEdit
                                            .IsMultiLine(true)
                                            .MaxLength(AreaNotesViewModel.MaxNoteLength)
                                            .Value(model => model.PublicText)
                                            .Height(205f)
                                            .Placeholder(LocaleString.PublicNotes);
                                    });
                                });

                                colAreas.AddRow(row =>
                                {
                                    row.AddSpacer();
                                });

                                colAreas.AddRow(row =>
                                {
                                    row.AddButton(button =>
                                    {
                                        button
                                            .OnClick(model => model.OnClickSave)
                                            .Label(LocaleString.Save)
                                            .Height(35f)
                                            .IsEnabled(model => model.IsSaveEnabled);
                                    });

                                    row.AddButton(button =>
                                    {
                                        button
                                            .OnClick(model => model.OnClickDiscardChanges)
                                            .Label(LocaleString.DiscardChanges)
                                            .Height(35f)
                                            .IsEnabled(model => model.IsSaveEnabled);
                                    });

                                    row.AddButton(button =>
                                    {
                                        button
                                            .OnClick(model => model.OnClickDeleteNote)
                                            .Label(LocaleString.DeleteNote)
                                            .Height(35f)
                                            .IsEnabled(model => model.IsDeleteEnabled);
                                    });
                                });
                            });
                        });

                    });

            }).Build();
        }
    }
}
