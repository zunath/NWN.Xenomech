using Anvil.Services;
using XM.UI;
using XM.UI.Builder;

namespace XM.Progression.UI
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
                window.Title("new window");
                window.Root(root =>
                {
                    root.AddButton(button =>
                    {
                        button.Label("My button");
                    });
                });
            }).Build();


            //return _builder.CreateWindow(window =>
            //{
            //    window
            //        .DefinePartialView("partial_test", group =>
            //        {
            //            group.SetLayout(col =>
            //            {
            //                col.AddLabel(label =>
            //                {
            //                    label.Label("from partial_test");
            //                });
            //            });
            //        })
            //        .Title("Test Window")
            //        .IsResizable(true)
            //        .InitialGeometry(0, 0, 200f, 200f)
            //        .Root(col =>
            //        {
            //            col
            //                .AddRow(row =>
            //                {
            //                    row
            //                        .AddSpacer()
            //                        .AddButton(button =>
            //                        {
            //                            button
            //                                .Label(model => model.TestProp1)
            //                                .OnClick(model => model.TestMethodToRun);

            //                        })
            //                        .AddCheck(check =>
            //                        {
            //                            check.Label("my check");
            //                            check.Selected(true);
            //                            check.OnClick(model => model.TestMethodToRun2);
            //                        })
            //                        .AddSpacer();
            //                })
            //                .AddRow(row =>
            //                {
            //                    row.AddList(list =>
            //                    {
            //                        list.RowHeight(40f);
            //                        list.AddTemplateCell(template =>
            //                        {
            //                            template.AddGroup(group =>
            //                            {
            //                                group.SetLayout(col =>
            //                                {
            //                                    col.AddRow(row =>
            //                                    {
            //                                        row.AddButton(button =>
            //                                        {
            //                                            button.Label("button 1");
            //                                            button.Height(20f);
            //                                        });

            //                                        row.AddButton(button =>
            //                                        {
            //                                            button.Label("button 2");
            //                                            button.Height(20f);
            //                                        });
            //                                    });
            //                                });
            //                            });


            //                        }, model => model.ButtonNames);

            //                    });
            //                });
            //        });

            //}).Build();
        }
    }
}
