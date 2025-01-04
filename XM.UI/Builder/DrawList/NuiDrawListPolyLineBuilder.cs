using Anvil.API;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListPolyLineBuilder<TViewModel> : NuiDrawListItemBuilderBase<NuiDrawListPolyLineBuilder<TViewModel>, NuiDrawListPolyLine, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiDrawListPolyLineBuilder(
            Color color, 
            bool fill, 
            float lineThickness, 
            List<float> points,
            NuiEventCollection eventCollection)
            : base(new NuiDrawListPolyLine(color, fill, lineThickness, points), eventCollection)
        {
        }

        public NuiDrawListPolyLineBuilder<TViewModel> Points(List<float> points)
        {
            Element.Points = points;
            return this;
        }

        // todo: BindPoints


        public NuiDrawListPolyLineBuilder<TViewModel> LineThickness(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<float>(bindName);
            Element.LineThickness = bind;

            return this;
        }

        public NuiDrawListPolyLineBuilder<TViewModel> Fill(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.Fill = bind;

            return this;
        }

        public NuiDrawListPolyLineBuilder<TViewModel> Color(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<Color>(bindName);
            Element.Color = bind;

            return this;
        }

    }

}
