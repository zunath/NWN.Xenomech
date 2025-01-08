using Anvil.API;
using System.Linq.Expressions;
using System;
using XM.Shared.API.NUI;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListArcBuilder<TViewModel> : NuiBindable<TViewModel>, INuiDrawListItemBuilder
        where TViewModel: IViewModel
    {
        private bool _isEnabled = true;
        private string _isEnabledBind;

        private Color _color;
        private string _colorBind;

        private bool _filled;
        private string _filledBind;

        private float _lineThickness;
        private string _lineThicknessBind;

        private Vector2Int _center;
        private string _centerBind;

        private float _radius;
        private string _radiusBind;

        private float _aMin;
        private string _aMinBind;

        private float _aMax;
        private string _aMaxBind;

        private NuiDrawListItemOrderType _order;
        private NuiDrawListItemRenderType _render;
        private bool _bindArrays;

        public NuiDrawListArcBuilder(NuiEventCollection registeredEvents)
            : base(registeredEvents)
        {
            _color = new Color(0, 0, 0);
            _center = new Vector2Int(0, 0);
        }

        public NuiDrawListArcBuilder<TViewModel> IsEnabled(bool isEnabled)
        {
            _isEnabled = isEnabled;
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> IsEnabled(Expression<Func<TViewModel, bool>> expression)
        {
            _isEnabledBind = GetBindName(expression);
            return this;
        }
        public NuiDrawListArcBuilder<TViewModel> Color(Color color)
        {
            _color = color;
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> Color(Expression<Func<TViewModel, Color>> expression)
        {
            _colorBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> Filled(bool filled)
        {
            _filled = filled;
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> Filled(Expression<Func<TViewModel, bool>> expression)
        {
            _filledBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> LineThickness(float lineThickness)
        {
            _lineThickness = lineThickness;
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> LineThickness(Expression<Func<TViewModel, float>> expression)
        {
            _lineThicknessBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> Center(Vector2Int center)
        {
            _center = center;
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> Center(Expression<Func<TViewModel, Vector2Int>> expression)
        {
            _centerBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> Radius(float radius)
        {
            _radius = radius;
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> Radius(Expression<Func<TViewModel, float>> expression)
        {
            _radiusBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> AMin(float aMin)
        {
            _aMin = aMin;
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> AMin(Expression<Func<TViewModel, float>> expression)
        {
            _aMinBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> AMax(float aMax)
        {
            _aMax = aMax;
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> AMax(Expression<Func<TViewModel, float>> expression)
        {
            _aMaxBind = GetBindName(expression);
            return this;
        }
        public NuiDrawListArcBuilder<TViewModel> Order(NuiDrawListItemOrderType order)
        {
            _order = order;
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> Render(NuiDrawListItemRenderType render)
        {
            _render = render;
            return this;
        }

        public NuiDrawListArcBuilder<TViewModel> BindArrays(bool bindArrays)
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

            var center = string.IsNullOrWhiteSpace(_centerBind)
                ? Nui.Vec(_center.X, _center.Y)
                : Nui.Bind(_centerBind);

            var radius = string.IsNullOrWhiteSpace(_radiusBind)
                ? JsonFloat(_radius)
                : Nui.Bind(_radiusBind);

            var aMin = string.IsNullOrWhiteSpace(_aMinBind)
                ? JsonFloat(_aMin)
                : Nui.Bind(_aMinBind);

            var aMax = string.IsNullOrWhiteSpace(_aMaxBind)
                ? JsonFloat(_aMax)
                : Nui.Bind(_aMaxBind);


            return Nui.DrawListArc(
                isEnabled,
                color,
                filled,
                lineThickness,
                center,
                radius,
                aMin,
                aMax,
                _order,
                _render,
                _bindArrays);
        }

    }

}
