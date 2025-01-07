using System;
using System.Linq.Expressions;
using Anvil.API;
using XM.Shared.API.NUI;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListLineBuilder<TViewModel>
        : NuiBindable<TViewModel>, INuiDrawListItemBuilder
        where TViewModel : IViewModel
    {
        private bool _isEnabled;
        private string _isEnabledBind;

        private Color _color;
        private string _colorBind;

        private float _lineThickness;
        private string _lineThicknessBind;

        private Vector2Int _a;
        private string _aBind;

        private Vector2Int _b;
        private string _bBind;

        private NuiDrawListItemOrderType _order = NuiDrawListItemOrderType.After;
        private NuiDrawListItemRenderType _render = NuiDrawListItemRenderType.Always;
        private bool _bindArrays;

        public NuiDrawListLineBuilder(NuiEventCollection registeredEvents)
            : base(registeredEvents)
        {
            _color = new Color(0, 0, 0);
            _a = new Vector2Int(0, 0);
            _b = new Vector2Int(0, 0);
        }

        public NuiDrawListLineBuilder<TViewModel> IsEnabled(bool isEnabled)
        {
            _isEnabled = isEnabled;
            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> IsEnabled(Expression<Func<TViewModel, bool>> expression)
        {
            _isEnabledBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> Color(Color color)
        {
            _color = color;
            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> Color(Expression<Func<TViewModel, Color>> expression)
        {
            _colorBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> LineThickness(float lineThickness)
        {
            _lineThickness = lineThickness;
            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> LineThickness(Expression<Func<TViewModel, float>> expression)
        {
            _lineThicknessBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> A(Vector2Int a)
        {
            _a = a;
            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> A(Expression<Func<TViewModel, Vector2Int>> expression)
        {
            _aBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> B(Vector2Int b)
        {
            _b = b;
            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> B(Expression<Func<TViewModel, Vector2Int>> expression)
        {
            _bBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> Order(NuiDrawListItemOrderType order)
        {
            _order = order;
            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> Render(NuiDrawListItemRenderType render)
        {
            _render = render;
            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> BindArrays(bool bindArrays)
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

            var lineThickness = string.IsNullOrWhiteSpace(_lineThicknessBind)
                ? JsonFloat(_lineThickness)
                : Nui.Bind(_lineThicknessBind);

            var a = string.IsNullOrWhiteSpace(_aBind)
                ? Nui.Vec(_a.X, _a.Y)
                : Nui.Bind(_aBind);

            var b = string.IsNullOrWhiteSpace(_bBind)
                ? Nui.Vec(_b.X, _b.Y)
                : Nui.Bind(_bBind);

            return Nui.DrawListLine(
                isEnabled,
                color,
                lineThickness,
                a,
                b,
                _order,
                _render,
                _bindArrays);
        }
    }
}
