using Anvil.Services;
using XM.UI;
using XM.UI.Builder;
using XM.UI.Builder.Layout;

namespace XM.Progression.UI.PlayerStatusUI
{
    [ServiceBinding(typeof(IView))]
    internal class PlayerStatusView: IView
    {
        private readonly NuiBuilder<PlayerStatusViewModel> _builder = new();

        public IViewModel CreateViewModel()
        {
            return new PlayerStatusViewModel();
        }

        public NuiBuiltWindow Build()
        {
            _builder.CreateWindow(window =>
            {
                window
                    .InitialGeometry(0, 0, 90f, 35f)
                    .Title(string.Empty)
                    .IsClosable(false)
                    .IsResizable(false)
                    .IsCollapsible(false)
                    .IsTransparent(true)
                    .Border(false)
                    .AcceptsInput(false)
                    .Root(EPBar);
            });

            return _builder.Build();
        }

        private void EPBar(NuiColumnBuilder<PlayerStatusViewModel> col)
        {
            col.AddRow(row =>
            {
                row.AddProgress(progress =>
                {
                    progress
                        .Value(model => model.EPProgress)
                        .ForegroundColor(model => model.EPBarColor)
                        .Height(20f)
                        .DrawList(drawList =>
                        {
                            drawList.AddText(text =>
                            {
                                text.Text(model => model.EPValue);
                                text.Bounds(15, 2, 110f, 50f);
                                text.Color(255, 255, 255);
                            });
                        });
                });
            });
        }
    }
}
