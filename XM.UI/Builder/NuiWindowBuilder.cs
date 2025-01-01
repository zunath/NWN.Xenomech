using Anvil.API;

namespace XM.UI.Builder
{
    public class NuiWindowBuilder
    {
        private readonly NuiWindow _window;

        public NuiWindowBuilder(NuiLayout root, string title)
        {
            _window = new NuiWindow(root, title);
        }

        public NuiWindowBuilder SetBorder(bool border)
        {
            _window.Border = border;
            return this;
        }

        public NuiWindowBuilder SetClosable(bool closable)
        {
            _window.Closable = closable;
            return this;
        }

        public NuiWindowBuilder SetCollapsed(bool? collapsed)
        {
            _window.Collapsed = collapsed;
            return this;
        }

        public NuiWindowBuilder SetGeometry(NuiRect geometry)
        {
            _window.Geometry = geometry;
            return this;
        }

        public NuiWindowBuilder SetId(string? id)
        {
            _window.Id = id;
            return this;
        }

        public NuiWindowBuilder SetResizable(bool resizable)
        {
            _window.Resizable = resizable;
            return this;
        }

        public NuiWindowBuilder SetRoot(NuiLayout root)
        {
            _window.Root = root;
            return this;
        }

        public NuiWindowBuilder SetTitle(string title)
        {
            _window.Title = title;
            return this;
        }

        public NuiWindowBuilder SetTransparent(bool transparent)
        {
            _window.Transparent = transparent;
            return this;
        }

        public NuiWindowBuilder SetAcceptsInput(bool acceptsInput)
        {
            _window.AcceptsInput = acceptsInput;
            return this;
        }

        public NuiWindow Build()
        {
            return _window;
        }
    }
}
