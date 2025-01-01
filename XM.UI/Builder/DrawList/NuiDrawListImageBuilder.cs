using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListImageBuilder<TViewModel> : NuiDrawListItemBuilderBase<NuiDrawListImageBuilder<TViewModel>, NuiDrawListImage, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiDrawListImageBuilder(string resRef, NuiRect rect)
            : base(new NuiDrawListImage(resRef, rect))
        {
        }

        public NuiDrawListImageBuilder<TViewModel> SetAspect(NuiAspect aspect)
        {
            Element.Aspect = aspect;
            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> SetHorizontalAlign(NuiHAlign horizontalAlign)
        {
            Element.HorizontalAlign = horizontalAlign;
            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> SetVerticalAlign(NuiVAlign verticalAlign)
        {
            Element.VerticalAlign = verticalAlign;
            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> SetRect(NuiRect rect)
        {
            Element.Rect = rect;
            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> SetResRef(string resRef)
        {
            Element.ResRef = resRef;
            return this;
        }
        public NuiDrawListImageBuilder<TViewModel> BindAspect(Expression<Func<TViewModel, NuiAspect>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiAspect>(bindName);
            Element.Aspect = bind;

            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> BindHorizontalAlign(Expression<Func<TViewModel, NuiHAlign>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiHAlign>(bindName);
            Element.HorizontalAlign = bind;

            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> BindVerticalAlign(Expression<Func<TViewModel, NuiVAlign>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiVAlign>(bindName);
            Element.VerticalAlign = bind;

            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> BindRect(Expression<Func<TViewModel, NuiRect>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiRect>(bindName);
            Element.Rect = bind;

            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> BindResRef(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.ResRef = bind;

            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> SetImageRegion(NuiRect? imageRegion)
        {
            Element.ImageRegion = imageRegion;
            return this;
        }
    }

}
