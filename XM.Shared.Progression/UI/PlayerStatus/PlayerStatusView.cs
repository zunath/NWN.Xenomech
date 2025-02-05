using Anvil.Services;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;
using XM.UI.Builder.Layout;

namespace XM.Progression.UI.PlayerStatus
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
                    .Title(LocaleString.Empty)
                    .IsClosable(false)
                    .IsResizable(false)
                    .IsCollapsible(WindowCollapsibleType.Disabled)
                    .IsTransparent(true)
                    .Border(false)
                    .AcceptsInput(false)
                    .Root(BuildStatBars);
            });

            return _builder.Build();
        }

        private void BuildStatBars(NuiColumnBuilder<PlayerStatusViewModel> col)
        {
            col.AddProgress(progress =>
            {
                progress
                    .Value(model => model.EPProgress)
                    .ForegroundColor(model => model.EPBarColor)
                    .Padding(0)
                    .Height(15f)
                    .DrawList(drawList =>
                    {
                        drawList.AddText(text =>
                        {
                            text.Text(model => model.EPValue);
                            text.Bounds(15, -1, 110f, 50f);
                            text.Color(255, 255, 255);
                        });
                    });
            });

            col.AddProgress(progress =>
            {
                progress
                    .Value(model => model.TPProgress)
                    .ForegroundColor(model => model.TPBarColor)
                    .Padding(0)
                    .Height(15f)
                    .DrawList(drawList =>
                    {
                        drawList.AddText(text =>
                        {
                            text.Text(model => model.TPValue);
                            text.Bounds(15, -1, 110f, 50f);
                            text.Color(255, 255, 255);
                        });
                    });
            });
        }
    }
}
