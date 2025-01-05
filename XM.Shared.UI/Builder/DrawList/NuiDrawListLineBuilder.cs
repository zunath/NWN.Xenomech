using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListLineBuilder<TViewModel> : NuiDrawListItemBuilderBase<NuiDrawListLineBuilder<TViewModel>, NuiDrawListLine, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiDrawListLineBuilder()
            : base(new NuiDrawListLine(
                Anvil.API.Color.FromRGBA(0), 
                false, 
                0f, 
                new NuiVector(), 
                new NuiVector()))
        {
        }

        public NuiDrawListLineBuilder<TViewModel> PointA(NuiVector pointA)
        {
            Element.PointA = pointA;
            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> PointB(NuiVector pointB)
        {
            Element.PointB = pointB;
            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> PointA(Expression<Func<TViewModel, NuiVector>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiVector>(bindName);
            Element.PointA = bind;

            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> PointB(Expression<Func<TViewModel, NuiVector>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiVector>(bindName);
            Element.PointB = bind;

            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> LineThickness(Expression<Func<TViewModel, float>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<float>(bindName);
            Element.LineThickness = bind;

            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> Fill(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.Fill = bind;

            return this;
        }

        public NuiDrawListLineBuilder<TViewModel> Color(Expression<Func<TViewModel, Color>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<Color>(bindName);
            Element.Color = bind;

            return this;
        }

    }

}
