using Anvil.API;
using System.Linq.Expressions;
using System;
using XM.Shared.API.NUI;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListCircleBuilder<TViewModel>
        : NuiBindable<TViewModel>, INuiDrawListItemBuilder
        where TViewModel : IViewModel
    {
        private bool _isEnabled;
        private string _isEnabledBind;

        private Color _color;
        private string _colorBind;

        private bool _filled;
        private string _filledBind;

        private float _lineThickness;
        private string _lineThicknessBind;

        private NuiRect _rect;
        private string _rectBind;

        private NuiDrawListItemOrderType _order;
        private NuiDrawListItemRenderType _render;
        private bool _bindArrays;

        public NuiDrawListCircleBuilder(NuiEventCollection registeredEvents)
            : base(registeredEvents)
        {
        }

        public NuiDrawListCircleBuilder<TViewModel> IsEnabled(bool isEnabled)
        {
            _isEnabled = isEnabled;
            return this;
        }

        public NuiDrawListCircleBuilder<TViewModel> IsEnabled(Expression<Func<TViewModel, bool>> expression)
        {
            _isEnabledBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListCircleBuilder<TViewModel> Color(Color color)
        {
            _color = color;
            return this;
        }

        public NuiDrawListCircleBuilder<TViewModel> Color(Expression<Func<TViewModel, Color>> expression)
        {
            _colorBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListCircleBuilder<TViewModel> Filled(bool filled)
        {
            _filled = filled;
            return this;
        }

        public NuiDrawListCircleBuilder<TViewModel> Filled(Expression<Func<TViewModel, bool>> expression)
        {
            _filledBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListCircleBuilder<TViewModel> LineThickness(float lineThickness)
        {
            _lineThickness = lineThickness;
            return this;
        }

        public NuiDrawListCircleBuilder<TViewModel> LineThickness(Expression<Func<TViewModel, float>> expression)
        {
            _lineThicknessBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListCircleBuilder<TViewModel> Rect(NuiRect rect)
        {
            _rect = rect;
            return this;
        }

        public NuiDrawListCircleBuilder<TViewModel> Rect(Expression<Func<TViewModel, NuiRect>> expression)
        {
            _rectBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListCircleBuilder<TViewModel> Order(NuiDrawListItemOrderType order)
        {
            _order = order;
            return this;
        }

        public NuiDrawListCircleBuilder<TViewModel> Render(NuiDrawListItemRenderType render)
        {
            _render = render;
            return this;
        }

        public NuiDrawListCircleBuilder<TViewModel> BindArrays(bool bindArrays)
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

            var filled = string.IsNullOrWhiteSpace(_filledBind)
                ? JsonBool(_filled)
                : Nui.Bind(_filledBind);

            var lineThickness = string.IsNullOrWhiteSpace(_lineThicknessBind)
                ? JsonFloat(_lineThickness)
                : Nui.Bind(_lineThicknessBind);

            var rect = string.IsNullOrWhiteSpace(_rectBind)
                ? Nui.Rect(_rect.X, _rect.Y, _rect.Width, _rect.Height)
                : Nui.Bind(_rectBind);

            return Nui.DrawListCircle(
                isEnabled, 
                color, 
                filled, 
                lineThickness, 
                rect, 
                _order, 
                _render, 
                _bindArrays);
        }
    }
}
