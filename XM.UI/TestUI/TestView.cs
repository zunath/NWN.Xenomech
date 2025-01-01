using Anvil.API;
using XM.UI.Builder;

namespace XM.UI.TestUI
{
    internal class TestView
    {
        private readonly NuiBuilder _builder = new();

        public TestView()
        {
            _builder.CreateWindow(window =>
                {
                    window.SetTitle("Test Window");
                    window.SetResizable(true);
                })
            .AddColumn(col =>
                {
                    col
                        .AddButton(button =>
                        {
                            button.SetLabel("my new button");
                        })
                        .AddCheck(check =>
                        {
                            check.SetLabel("my check");
                            check.SetSelected(true);
                        })
                        
                        ;

                });
        }
    }
}
