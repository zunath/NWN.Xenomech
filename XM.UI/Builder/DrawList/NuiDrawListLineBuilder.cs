using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListLineBuilder<TViewModel> : NuiDrawListItemBuilderBase<NuiDrawListLineBuilder<TViewModel>, NuiDrawListLine, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiDrawListLineBuilder(Color color, bool fill, float lineThickness, NuiVector pointA, NuiVector pointB)
            : base(new NuiDrawListLine(color, fill, lineThickness, pointA, pointB))
        {
        }

        public NuiDrawListLineBuilder<TViewModel> SetPointA(NuiVector pointA)
        {
            Element.PointA = pointA;
            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> SetPointB(NuiVector pointB)
        {
            Element.PointB = pointB;
            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> SetLineThickness(float lineThickness)
        {
            Element.LineThickness = lineThickness;
            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> SetFill(bool fill)
        {
            Element.Fill = fill;
            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> SetColor(Color color)
        {
            Element.Color = color;
            return this;
        }
        public NuiDrawListLineBuilder<TViewModel> BindPointA(Expression<Func<TViewModel, NuiVector>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiVector>(bindName);
            Element.PointA = bind;

            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> BindPointB(Expression<Func<TViewModel, NuiVector>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiVector>(bindName);
            Element.PointB = bind;

            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> BindLineThickness(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<float>(bindName);
            Element.LineThickness = bind;

            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> BindFill(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.Fill = bind;

            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> BindColor(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<Color>(bindName);
            Element.Color = bind;

            return this;
        }

    }

}
