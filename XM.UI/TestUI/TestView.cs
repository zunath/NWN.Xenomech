using System;
using Anvil.API;
using Anvil.Services;
using XM.UI.Builder;

namespace XM.UI.TestUI
{
    [ServiceBinding(typeof(IView))]
    internal class TestView: IView
    {
        private readonly NuiBuilder<TestViewModel> _builder = new();

        public TestView()
        {
            _builder.CreateWindow(window =>
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
                                    button.SetLabel("my new button");
                                    button.BindLabel(model => model.TestProp1);
                                })
                                .AddCheck(check =>
                                {
                                    check.SetLabel("my check");
                                    check.SetSelected(true);
                                })
                                .AddSpacer();
                        })
                        ;

                });


        }

        [ScriptHandler("bread_test")]
        public void ShowWindow()
        {
            var window = _builder.Build();
            var json = JsonUtility.ToJson(window);
            Console.WriteLine(json);
            var player = GetLastUsedBy();
            NuiCreate(player, JsonParse(json), window.Id, "");
        }

        public IViewModel CreateViewModel(uint player)
        {
            return new TestViewModel();
        }
    }
}
