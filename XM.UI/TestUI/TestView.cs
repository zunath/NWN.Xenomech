using Anvil.Services;
using XM.UI.Builder;

namespace XM.UI.TestUI
{
    [ServiceBinding(typeof(IView))]
    internal class TestView : IView
    {
        private readonly NuiBuilder<TestViewModel> _builder = new();

        public IViewModel CreateViewModel()
        {
            return new TestViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
                {
                    window
                        .SetTitle("Test Window")
                        .SetResizable(true)
                        .SetInitialGeometry(0, 0, 200f, 200f);
                })
                .SetRoot(col =>
                {
                    col
                        .AddRow(row =>
                        {
                            row
                                .AddSpacer()
                                .AddButton(button =>
                                {
                                    button
                                        .Label(model => model.TestProp1)
                                        .OnClick(model => model.TestMethodToRun);

                                })
                                .AddCheck(check =>
                                {
                                    check.Label("my check");
                                    check.Selected(true);
                                    check.OnClick(model => model.TestMethodToRun2);
                                })
                                .AddSpacer();
                        })
                        .AddRow(row =>
                        {
                            row.AddList(list =>
                            {
                                list.AddTemplate(template =>
                                {
                                    template.AddButton(button =>
                                    {
                                        button.Label(model => model.ButtonNames);
                                    });
                                })
                                    .RowCount(model => model.ButtonNames);
                                
                            });
                        })
                        ;

                }).Build();
        }
    }
}
