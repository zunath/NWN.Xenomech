using Anvil.Services;
using XM.Shared.API.NUI;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;

namespace XM.Plugin.Administration.StaffManagement
{
    [ServiceBinding(typeof(IView))]
    internal class ManageStaffView: IView
    {
        private readonly NuiBuilder<ManageStaffViewModel> _builder = new();

        public IViewModel CreateViewModel()
        {
            return new ManageStaffViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
            {
                window
                    .IsResizable(true)
                    .IsCollapsible(WindowCollapsibleType.UserCollapsible)
                    .InitialGeometry(0f, 0f, 545f, 295f)
                    .Title(LocaleString.ManageStaff)

                    .Root(root  =>
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
                                            cell.AddRow(row =>
                                            {
                                                row.AddButtonSelect(button =>
                                                {
                                                    button
                                                        .Label(model => model.Names)
                                                        .IsSelected(model => model.UserToggles)
                                                        .OnClick(model => model.OnSelectUser());
                                                });
                                            });
                                        });
                                    }, model => model.Names);
                                });

                                col.AddRow(row =>
                                {
                                    row.AddButton(button =>
                                    {
                                        button
                                            .Label(LocaleString.NewUser)
                                            .OnClick(model => model.OnClickNewUser());
                                    });

                                    row.AddButton(button =>
                                    {
                                        button
                                            .Label(LocaleString.DeleteUser)
                                            .OnClick(model => model.OnClickDeleteUser());
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
                                            .Value(model => model.ActiveUserName)
                                            .IsEnabled(model => model.IsUserSelected)
                                            .Placeholder(LocaleString.UserName);
                                    });

                                    row.Height(32f);
                                });

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
                                    row.AddOptions(options =>
                                    {
                                        options
                                            .AddOption(LocaleString.DM)
                                            .AddOption(LocaleString.Admin)
                                            .Selection(model => model.SelectedRoleId)
                                            .Direction(NuiDirection.Horizontal)
                                            .IsEnabled(model => model.IsUserSelected);
                                    });
                                });

                                col.AddRow(row =>
                                {
                                    row.AddButton(button =>
                                    {
                                        button
                                            .OnClick(model => model.OnClickSave())
                                            .Label(LocaleString.Save)
                                            .IsEnabled(model => model.IsUserSelected);
                                    });

                                    row.AddButton(button =>
                                    {
                                        button
                                            .OnClick(model => model.OnClickDiscardChanges())
                                            .Label(LocaleString.DiscardChanges)
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
