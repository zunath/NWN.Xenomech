using Anvil.Services;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;

namespace XM.Plugin.Administration.BugReport
{
    [ServiceBinding(typeof(IView))]
    internal class BugReportView: IView
    {
        private readonly NuiBuilder<BugReportViewModel> _builder = new();

        public IViewModel CreateViewModel()
        {
            return new BugReportViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
            {
                window
                    .IsResizable(true)
                    .IsCollapsible(WindowCollapsibleType.UserCollapsible)
                    .InitialGeometry(0f, 0f, 829f, 453f)
                    .Title(LocaleString.SubmitBugReport)

                    .Root(col =>
                    {
                        col.AddRow(row =>
                        {
                            row.AddLabel(label =>
                            {
                                label
                                    .Label(LocaleString.PleaseEnterAsMuchInformationAsPossibleRegardingTheEncounteredBug)
                                    .Width(800f);
                            });

                            row.Height(20f);
                        });

                        col.AddRow(row =>
                        {
                            row.AddTextEdit(textEdit =>
                            {
                                textEdit
                                    .IsMultiLine(true)
                                    .MaxLength(BugReportViewModel.MaxBugReportLength)
                                    .Value(model => model.BugReportText)
                                    .Height(300f);
                            });
                        });

                        col.AddRow(row =>
                        {
                            row.AddSpacer();

                            row.AddButton(button =>
                            {
                                button
                                    .Height(35f)
                                    .Label(LocaleString.SubmitBugReport)
                                    .OnClick(model => model.OnClickSubmit());
                            });

                            row.AddButton(button =>
                            {
                                button
                                    .Height(35f)
                                    .Label(LocaleString.Cancel)
                                    .OnClick(model => model.OnClickCancel());
                            });

                            row.AddSpacer();
                        });
                    });


            }).Build();
        }
    }
}
