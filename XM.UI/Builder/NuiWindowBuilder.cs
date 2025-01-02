using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder
{
    public class NuiWindowBuilder<TViewModel>: NuiBindable<TViewModel>
        where TViewModel: IViewModel
    {
        private readonly NuiWindow _window;

        public NuiWindowBuilder(NuiLayout root, string title, NuiEventCollection eventCollection) 
            : base(eventCollection)
        {
            _window = new NuiWindow(root, title);
            _window.Id = typeof(TViewModel).FullName;
        }

        public NuiWindowBuilder<TViewModel> SetBorder(bool border)
        {
            _window.Border = border;
            return this;
        }

        public NuiWindowBuilder<TViewModel> SetClosable(bool closable)
        {
            _window.Closable = closable;
            return this;
        }

        public NuiWindowBuilder<TViewModel> SetCollapsed(bool? collapsed)
        {
            _window.Collapsed = collapsed;
            return this;
        }

        public NuiWindowBuilder<TViewModel> SetInitialGeometry(float x, float y, float width, float height)
        {
            _window.Geometry = new NuiRect(x, y, width, height);
            return this;
        }

        public NuiWindowBuilder<TViewModel> SetResizable(bool resizable)
        {
            _window.Resizable = resizable;
            return this;
        }

        public NuiWindowBuilder<TViewModel> SetRoot(NuiLayout root)
        {
            _window.Root = root;
            return this;
        }

        public NuiWindowBuilder<TViewModel> SetTitle(string title)
        {
            _window.Title = title;
            return this;
        }

        public NuiWindowBuilder<TViewModel> SetTransparent(bool transparent)
        {
            _window.Transparent = transparent;
            return this;
        }

        public NuiWindowBuilder<TViewModel> SetAcceptsInput(bool acceptsInput)
        {
            _window.AcceptsInput = acceptsInput;
            return this;
        }

        public NuiWindowBuilder<TViewModel> BindBorder(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            _window.Border = bind;
            return this;
        }

        public NuiWindowBuilder<TViewModel> BindClosable(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            _window.Closable = bind;
            return this;
        }

        public NuiWindowBuilder<TViewModel> BindCollapsed(Expression<Func<TViewModel, bool?>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            _window.Collapsed = bind;
            return this;
        }

        public NuiWindowBuilder<TViewModel> BindInitialGeometry(Expression<Func<TViewModel, NuiRect>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiRect>(bindName);
            _window.Geometry = bind;
            return this;
        }

        public NuiWindowBuilder<TViewModel> BindResizable(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            _window.Resizable = bind;
            return this;
        }

        public NuiWindowBuilder<TViewModel> BindTitle(Expression<Func<TViewModel, string>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            _window.Title = bind;
            return this;
        }

        public NuiWindowBuilder<TViewModel> BindTransparent(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            _window.Transparent = bind;
            return this;
        }

        public NuiWindowBuilder<TViewModel> BindAcceptsInput(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            _window.AcceptsInput = bind;
            return this;
        }


        internal NuiWindow Build()
        {
            return _window;
        }
    }
}
