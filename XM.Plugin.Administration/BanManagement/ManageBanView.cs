using Anvil.Services;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;

namespace XM.Plugin.Administration.BanManagement
{
    [ServiceBinding(typeof(IView))]
    internal class ManageBanView: IView
    {
        private readonly NuiBuilder<ManageBanViewModel> _builder = new();

        public IViewModel CreateViewModel()
        {
            return new ManageBanViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
            {
                window
                    .IsResizable(true)
                    .IsCollapsible(WindowCollapsibleType.UserCollapsible)
                    .InitialGeometry(0f, 0f, 545f, 295f)
                    .Title(LocaleString.ManageBans)

                    .Root(root =>
                    {
                        root.AddRow(rootRow =>
                        {
                            rootRow.AddColumn(col =>
                            {
                                col.AddRow(row =>
                                {
                                    row.AddSpacer();

                                    row.AddLabel(label =>
                                    {
                                        label
                                            .Label(model => model.StatusText)
                                            .ForegroundColor(model => model.StatusColor)
                                            .Height(20f);
                                    });

                                    row.AddSpacer();
                                });

                                col.AddRow(row =>
                                {
                                    row.AddList(list =>
                                    {
                                        list.AddTemplateCell(cell =>
                                        {
                                            cell
                                                .AddRow(row =>
                                                {
                                                    row.AddButtonSelect(button =>
                                                    {
                                                        button
                                                            .Label(model => model.CDKeys)
                                                            .IsSelected(model => model.UserToggles)
                                                            .OnClick(model => model.OnSelectUser);
                                                    });
                                                });
                                        });
                                    }, model => model.CDKeys);
                                });

                                col.AddRow(row =>
                                {
                                    row.AddButton(button =>
                                    {
                                        button
                                            .Label(LocaleString.NewUser)
                                            .OnClick(model => model.OnClickNewUser);
                                    });

                                    row.AddButton(button =>
                                    {
                                        button
                                            .Label(LocaleString.DeleteUser)
                                            .OnClick(model => model.OnClickDeleteUser);
                                    });
                                });
                            });

                            rootRow.AddColumn(col =>
                            {
                                col.Height(300f);

                                col.AddRow(row =>
                                {
                                    row.AddTextEdit(textEdit =>
                                    {
                                        textEdit
                                            .Value(model => model.ActiveUserCDKey)
                                            .IsEnabled(model => model.IsUserSelected)
                                            .Placeholder(LocaleString.CDKey8Chars)
                                            .MaxLength(8);
                                    });

                                    row.Height(32f);
                                });

                                col.AddRow(row =>
                                {
                                    row.AddTextEdit(textEdit =>
                                    {
                                        textEdit
                                            .Value(model => model.ActiveBanReason)
                                            .IsEnabled(model => model.IsUserSelected)
                                            .Placeholder(LocaleString.BanReason);
                                    });

                                    row.Height(32f);
                                });

                                col.AddRow(row =>
                                {
                                    row.AddButton(button =>
                                    {
                                        button
                                            .Label(LocaleString.Save)
                                            .OnClick(model => model.OnClickSave)
                                            .IsEnabled(model => model.IsUserSelected);
                                    });

                                    row.AddButton(button =>
                                    {
                                        button
                                            .Label(LocaleString.DiscardChanges)
                                            .OnClick(model => model.OnClickDiscardChanges)
                                            .IsEnabled(model => model.IsUserSelected);
                                    });
                                });
                            });
                        });

                    });

            }).Build();
        }
    }
}
