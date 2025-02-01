using System;
using System.Linq.Expressions;
using Anvil.API;
using XM.Shared.API.NUI;
using XM.Shared.Core;
using XM.Shared.Core.Localization;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListTextBuilder<TViewModel>
        : NuiBindable<TViewModel>, INuiDrawListItemBuilder
        where TViewModel : IViewModel
    {
        private bool _isEnabled = true;
        private string _isEnabledBind;

        private Color _color;
        private string _colorBind;

        private NuiRect _rect;
        private string _rectBind;

        private LocaleString _text;
        private string _textBind;

        private NuiDrawListItemOrderType _order = NuiDrawListItemOrderType.After;
        private NuiDrawListItemRenderType _render = NuiDrawListItemRenderType.Always;
        private bool _bindArrays;

        public NuiDrawListTextBuilder(NuiEventCollection registeredEvents)
            : base(registeredEvents)
        {
            _color = new Color(0, 0, 0);
            _rect = new NuiRect(0, 0, 0, 0);
        }

        public NuiDrawListTextBuilder<TViewModel> IsEnabled(bool isEnabled)
        {
            _isEnabled = isEnabled;
            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> IsEnabled(Expression<Func<TViewModel, bool>> expression)
        {
            _isEnabledBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> Color(byte r, byte g, byte b, byte alpha = 255)
        {
            _color = new Color(r, g, b, alpha);
            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> Color(Color color)
        {
            _color = color;
            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> Color(Expression<Func<TViewModel, Color>> expression)
        {
            _colorBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> Bounds(float x, float y, float w, float h)
        {
            _rect = new NuiRect(x, y, w, h);
            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> Bounds(NuiRect rect)
        {
            _rect = rect;
            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> Bounds(Expression<Func<TViewModel, NuiRect>> expression)
        {
            _rectBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> Text(LocaleString text)
        {
            _text = text;
            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> Text(Expression<Func<TViewModel, string>> expression)
        {
            _textBind = GetBindName(expression);
            return this;
        }
        public NuiDrawListTextBuilder<TViewModel> Text(Expression<Func<TViewModel, XMBindingList<string>>> expression)
        {
            _textBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> Order(NuiDrawListItemOrderType order)
        {
            _order = order;
            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> Render(NuiDrawListItemRenderType render)
        {
            _render = render;
            return this;
        }

        public NuiDrawListTextBuilder<TViewModel> BindArrays(bool bindArrays)
        {
            _bindArrays = bindArrays;
            return this;
        }

        public Json Build()
        {
            var isEnabled = string.IsNullOrWhiteSpace(_isEnabledBind)
                ? JsonBool(_isEnabled)
                : Nui.Bind(_isEnabledBind);

            var color = string.IsNullOrWhiteSpace(_colorBind)
                ? Nui.Color(_color.Red, _color.Green, _color.Blue, _color.Alpha)
                : Nui.Bind(_colorBind);

            var rect = string.IsNullOrWhiteSpace(_rectBind)
                ? Nui.Rect(_rect.X, _rect.Y, _rect.Width, _rect.Height)
                : Nui.Bind(_rectBind);

            var text = string.IsNullOrWhiteSpace(_textBind)
                ? JsonString(_text.ToLocalizedString())
                : Nui.Bind(_textBind);

            return Nui.DrawListText(
                isEnabled,
                color,
                rect,
                text,
                _order,
                _render,
                _bindArrays);
        }
    }
}
