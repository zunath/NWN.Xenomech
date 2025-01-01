using Anvil.API;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListPolyLineBuilder<TViewModel> : NuiDrawListItemBuilderBase<NuiDrawListPolyLineBuilder<TViewModel>, NuiDrawListPolyLine, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiDrawListPolyLineBuilder(Color color, bool fill, float lineThickness, List<float> points)
            : base(new NuiDrawListPolyLine(color, fill, lineThickness, points))
        {
        }

        public NuiDrawListPolyLineBuilder<TViewModel> SetPoints(List<float> points)
        {
            Element.Points = points;
            return this;
        }

        public NuiDrawListPolyLineBuilder<TViewModel> SetLineThickness(float lineThickness)
        {
            Element.LineThickness = lineThickness;
            return this;
        }

        public NuiDrawListPolyLineBuilder<TViewModel> SetFill(bool fill)
        {
            Element.Fill = fill;
            return this;
        }

        public NuiDrawListPolyLineBuilder<TViewModel> SetColor(Color color)
        {
            Element.Color = color;
            return this;
        }

        // todo: BindPoints


        public NuiDrawListPolyLineBuilder<TViewModel> BindLineThickness(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<float>(bindName);
            Element.LineThickness = bind;

            return this;
        }

        public NuiDrawListPolyLineBuilder<TViewModel> BindFill(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.Fill = bind;

            return this;
        }

        public NuiDrawListPolyLineBuilder<TViewModel> BindColor(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<Color>(bindName);
            Element.Color = bind;

            return this;
        }

    }

}
