using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using Anvil.API;
using XM.Shared.API.NUI;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListPolyLineBuilder<TViewModel>
        : NuiBindable<TViewModel>, INuiDrawListItemBuilder
        where TViewModel : IViewModel
    {
        private bool _isEnabled;
        private string _isEnabledBind;

        private Color _color;
        private string _colorBind;

        private bool _fill;
        private string _fillBind;

        private float _lineThickness;
        private string _lineThicknessBind;

        private List<Vector2Int> _points = new();
        private string _pointsBind;

        private NuiDrawListItemOrderType _order = NuiDrawListItemOrderType.After;
        private NuiDrawListItemRenderType _render = NuiDrawListItemRenderType.Always;
        private bool _bindArrays;

        public NuiDrawListPolyLineBuilder(NuiEventCollection registeredEvents)
            : base(registeredEvents)
        {
            _color = new Color(0, 0, 0);
        }

        public NuiDrawListPolyLineBuilder<TViewModel> IsEnabled(bool isEnabled)
        {
            _isEnabled = isEnabled;
            return this;
        }

        public NuiDrawListPolyLineBuilder<TViewModel> IsEnabled(Expression<Func<TViewModel, bool>> expression)
        {
            _isEnabledBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListPolyLineBuilder<TViewModel> Color(Color color)
        {
            _color = color;
            return this;
        }

        public NuiDrawListPolyLineBuilder<TViewModel> Color(Expression<Func<TViewModel, Color>> expression)
        {
            _colorBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListPolyLineBuilder<TViewModel> Fill(bool fill)
        {
            _fill = fill;
            return this;
        }

        public NuiDrawListPolyLineBuilder<TViewModel> Fill(Expression<Func<TViewModel, bool>> expression)
        {
            _fillBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListPolyLineBuilder<TViewModel> LineThickness(float lineThickness)
        {
            _lineThickness = lineThickness;
            return this;
        }

        public NuiDrawListPolyLineBuilder<TViewModel> LineThickness(Expression<Func<TViewModel, float>> expression)
        {
            _lineThicknessBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListPolyLineBuilder<TViewModel> Points(List<Vector2Int> points)
        {
            _points = points;
            return this;
        }

        public NuiDrawListPolyLineBuilder<TViewModel> Points(Expression<Func<TViewModel, List<Vector2Int>>> expression)
        {
            _pointsBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListPolyLineBuilder<TViewModel> Order(NuiDrawListItemOrderType order)
        {
            _order = order;
            return this;
        }

        public NuiDrawListPolyLineBuilder<TViewModel> Render(NuiDrawListItemRenderType render)
        {
            _render = render;
            return this;
        }

        public NuiDrawListPolyLineBuilder<TViewModel> BindArrays(bool bindArrays)
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

            var fill = string.IsNullOrWhiteSpace(_fillBind)
                ? JsonBool(_fill)
                : Nui.Bind(_fillBind);

            var lineThickness = string.IsNullOrWhiteSpace(_lineThicknessBind)
                ? JsonFloat(_lineThickness)
                : Nui.Bind(_lineThicknessBind);

            var points = JsonArray();

            if (string.IsNullOrWhiteSpace(_pointsBind))
            {
                foreach (var point in _points)
                {
                    points = JsonArrayInsert(points, Nui.Vec(point.X, point.Y));
                }
            }
            else
            {
                points = Nui.Bind(_pointsBind);
            }

            return Nui.DrawListPolyLine(
                isEnabled,
                color,
                fill,
                lineThickness,
                points,
                _order,
                _render,
                _bindArrays);
        }
    }
}
