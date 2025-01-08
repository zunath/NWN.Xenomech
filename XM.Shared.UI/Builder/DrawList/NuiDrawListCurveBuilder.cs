using Anvil.API;
using System.Linq.Expressions;
using System;
using XM.Shared.API.NUI;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListCurveBuilder<TViewModel> 
        : NuiBindable<TViewModel>, INuiDrawListItemBuilder
        where TViewModel: IViewModel
    {
        private bool _isEnabled = true;
        private string _isEnabledBind;

        private Color _color;
        private string _colorBind;

        private float _lineThickness;
        private string _lineThicknessString;

        private Vector2Int _a;
        private string _aBind;

        private Vector2Int _b;
        private string _bBind;

        private Vector2Int _ctrl0;
        private string _ctrl0Bind;

        private Vector2Int _ctrl1;
        private string _ctrl1Bind;

        private NuiDrawListItemOrderType _order;
        private NuiDrawListItemRenderType _render;
        private bool _bindArrays;

        public NuiDrawListCurveBuilder(NuiEventCollection registeredEvents) : base(registeredEvents)
        {
            _color = new Color(0, 0, 0);
            _a = new Vector2Int(0, 0);
            _b = new Vector2Int(0, 0);
            _ctrl0 = new Vector2Int(0, 0);
            _ctrl1 = new Vector2Int(0, 0);
        }

        public NuiDrawListCurveBuilder<TViewModel> IsEnabled(bool isEnabled)
        {
            _isEnabled = isEnabled;
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> IsEnabled(Expression<Func<TViewModel, bool>> expression)
        {
            _isEnabledBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> Color(Color color)
        {
            _color = color;
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> Color(Expression<Func<TViewModel, Color>> expression)
        {
            _colorBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> LineThickness(float lineThickness)
        {
            _lineThickness = lineThickness;
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> LineThickness(Expression<Func<TViewModel, float>> expression)
        {
            _lineThicknessString = GetBindName(expression);
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> A(Vector2Int a)
        {
            _a = a;
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> A(Expression<Func<TViewModel, Vector2Int>> expression)
        {
            _aBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> B(Vector2Int b)
        {
            _b = b;
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> B(Expression<Func<TViewModel, Vector2Int>> expression)
        {
            _bBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> Ctrl0(Vector2Int ctrl0)
        {
            _ctrl0 = ctrl0;
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> Ctrl0(Expression<Func<TViewModel, Vector2Int>> expression)
        {
            _ctrl0Bind = GetBindName(expression);
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> Ctrl1(Vector2Int ctrl1)
        {
            _ctrl1 = ctrl1;
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> Ctrl1(Expression<Func<TViewModel, Vector2Int>> expression)
        {
            _ctrl1Bind = GetBindName(expression);
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> Order(NuiDrawListItemOrderType order)
        {
            _order = order;
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> Render(NuiDrawListItemRenderType render)
        {
            _render = render;
            return this;
        }

        public NuiDrawListCurveBuilder<TViewModel> BindArrays(bool bindArrays)
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

            var lineThickness = string.IsNullOrWhiteSpace(_lineThicknessString)
                ? JsonFloat(_lineThickness)
                : Nui.Bind(_lineThicknessString);

            var a = string.IsNullOrWhiteSpace(_aBind)
                ? Nui.Vec(_a.X, _a.Y)
                : Nui.Bind(_aBind);

            var b = string.IsNullOrWhiteSpace(_bBind)
                ? Nui.Vec(_b.X, _b.Y)
                : Nui.Bind(_bBind);

            var ctrl0 = string.IsNullOrWhiteSpace(_ctrl0Bind)
                ? Nui.Vec(_ctrl0.X, _ctrl0.Y)
                : Nui.Bind(_ctrl0Bind);

            var ctrl1 = string.IsNullOrWhiteSpace(_ctrl1Bind)
                ? Nui.Vec(_ctrl1.X, _ctrl1.Y)
                : Nui.Bind(_ctrl1Bind);

            return Nui.DrawListCurve(
                isEnabled, 
                color, 
                lineThickness, 
                a, 
                b, 
                ctrl0, 
                ctrl1, 
                _order, 
                _render, 
                _bindArrays);

        }

    }

}
