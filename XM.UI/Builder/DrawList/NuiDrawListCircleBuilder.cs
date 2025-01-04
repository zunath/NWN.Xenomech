using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListCircleBuilder<TViewModel> : NuiDrawListItemBuilderBase<NuiDrawListCircleBuilder<TViewModel>, NuiDrawListCircle, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiDrawListCircleBuilder(
            Color color,
            bool fill,
            float lineThickness,
            NuiRect rect,
            NuiEventCollection eventCollection)
            : base(new NuiDrawListCircle(color, fill, lineThickness, rect), eventCollection)
        {
        }

        public NuiDrawListCircleBuilder<TViewModel> Rect(NuiRect rect)
        {
            Element.Rect = rect;
            return this;
        }

        public NuiDrawListCircleBuilder<TViewModel> Rect(Expression<Func<TViewModel, NuiRect>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiRect>(bindName);
            Element.Rect = bind;

            return this;
        }

    }

}
