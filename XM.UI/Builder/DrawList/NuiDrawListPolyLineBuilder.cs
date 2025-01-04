using Anvil.API;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListPolyLineBuilder<TViewModel> : NuiDrawListItemBuilderBase<NuiDrawListPolyLineBuilder<TViewModel>, NuiDrawListPolyLine, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiDrawListPolyLineBuilder()
            : base(new NuiDrawListPolyLine(
                Anvil.API.Color.FromRGBA(0), 
                false, 
                0f, 
                new List<float>()))
        {
        }

        public NuiDrawListPolyLineBuilder<TViewModel> Points(List<float> points)
        {
            Element.Points = points;
            return this;
        }

        // todo: BindPoints


        public NuiDrawListPolyLineBuilder<TViewModel> LineThickness(Expression<Func<TViewModel, float>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<float>(bindName);
            Element.LineThickness = bind;

            return this;
        }

        public NuiDrawListPolyLineBuilder<TViewModel> Fill(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.Fill = bind;

            return this;
        }

        public NuiDrawListPolyLineBuilder<TViewModel> Color(Expression<Func<TViewModel, Color>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<Color>(bindName);
            Element.Color = bind;

            return this;
        }

    }

}
