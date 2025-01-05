using XM.UI;
using XM.UI.Builder;
using XM.UI.Builder.Layout;

namespace XM.Progression.UI.PlayerStatusUI
{
    //[ServiceBinding(typeof(IView))]
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
                    .InitialGeometry(0, 0, 180f, 70f)
                    .Title(string.Empty)
                    .IsClosable(false)
                    .IsResizable(false)
                    .IsCollapsed(false)
                    .IsTransparent(false)
                    .Border(true)
                    .AcceptsInput(false);
            })
            .SetRoot(col =>
            {
                HPBar(col);
                EPBar(col);
            });

            return _builder.Build();
        }

        private void HPBar(NuiColumnBuilder<PlayerStatusViewModel> col)
        {
            col.AddRow(row =>
            {
                row.AddProgress(progress =>
                {
                    progress
                        .Value(model => model.Bar1Progress)
                        .ForegroundColor(model => model.Bar1Color)
                        .Height(20f)
                        .DrawList(drawList =>
                        {
                            drawList.AddText(text =>
                            {
                                text.Text(model => model.Bar1Label);
                                text.Bounds(5, 2, 110f, 50f);
                                text.Color(255, 255, 255);
                            });

                            drawList.AddText(text =>
                            {
                                text.Text(model => model.Bar1Value);
                                text.Bounds(model => model.RelativeValuePosition);
                                text.Color(255, 255, 255);
                            });
                        });
                });
            });
        }

        private void EPBar(NuiColumnBuilder<PlayerStatusViewModel> col)
        {
            col.AddRow(row =>
            {
                row.AddProgress(progress =>
                {
                    progress
                        .Value(model => model.Bar1Progress)
                        .ForegroundColor(model => model.Bar2Color)
                        .Height(20f)
                        .DrawList(drawList =>
                        {
                            drawList.AddText(text =>
                            {
                                text.Text(model => model.Bar2Label);
                                text.Bounds(5, 2, 110f, 50f);
                                text.Color(255, 255, 255);
                            });

                            drawList.AddText(text =>
                            {
                                text.Text(model => model.Bar2Value);
                                text.Bounds(model => model.RelativeValuePosition);
                                text.Color(255, 255, 255);
                            });
                        });
                });
            });
        }
    }
}
