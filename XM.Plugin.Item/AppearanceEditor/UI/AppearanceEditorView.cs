using XM.UI;
using XM.UI.Builder;

namespace XM.Plugin.Item.AppearanceEditor.UI
{
    internal class AppearanceEditorView : IView
    {
        private readonly NuiBuilder<AppearanceEditorViewModel> _builder = new();

        public IViewModel CreateViewModel()
        {
            return new AppearanceEditorViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
            {

            }).Build();
        }
    }
}
