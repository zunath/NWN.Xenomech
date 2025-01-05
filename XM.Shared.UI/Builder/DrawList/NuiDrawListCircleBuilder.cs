using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListCircleBuilder<TViewModel> : NuiDrawListItemBuilderBase<NuiDrawListCircleBuilder<TViewModel>, NuiDrawListCircle, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiDrawListCircleBuilder()
            : base(new NuiDrawListCircle(
                Anvil.API.Color.FromRGBA(0), 
                false,
                0f, 
                new NuiRect()))
        {
        }

        public NuiDrawListCircleBuilder<TViewModel> Bounds(float x, float y, float width, float height)
        {
            Element.Rect = new NuiRect(x, y, width, height);
            return this;
        }

        public NuiDrawListCircleBuilder<TViewModel> Bounds(NuiRect rect)
        {
            Element.Rect = rect;
            return this;
        }

        public NuiDrawListCircleBuilder<TViewModel> Bounds(Expression<Func<TViewModel, NuiRect>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiRect>(bindName);
            Element.Rect = bind;

            return this;
        }

    }

}
