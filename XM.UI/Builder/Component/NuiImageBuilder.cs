using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiImageBuilder<TViewModel> : NuiBuilderBase<NuiImageBuilder<TViewModel>, NuiImage, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiImageBuilder()
            : base(new NuiImage(string.Empty))
        {
        }

        public NuiImageBuilder<TViewModel> SetHorizontalAlign(NuiHAlign horizontalAlign)
        {
            Element.HorizontalAlign = horizontalAlign;
            return this;
        }

        public NuiImageBuilder<TViewModel> SetVerticalAlign(NuiVAlign verticalAlign)
        {
            Element.VerticalAlign = verticalAlign;
            return this;
        }

        public NuiImageBuilder<TViewModel> SetImageAspect(NuiAspect aspect)
        {
            Element.ImageAspect = aspect;
            return this;
        }

        public NuiImageBuilder<TViewModel> SetImageRegion(NuiRect region)
        {
            Element.ImageRegion = region;
            return this;
        }

        public NuiImageBuilder<TViewModel> SetResRef(string resRef)
        {
            Element.ResRef = resRef;
            return this;
        }
        public NuiImageBuilder<TViewModel> BindHorizontalAlign(Expression<Func<TViewModel, NuiHAlign>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiHAlign>(bindName);
            Element.HorizontalAlign = bind;

            return this;
        }

        public NuiImageBuilder<TViewModel> BindVerticalAlign(Expression<Func<TViewModel, NuiVAlign>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiVAlign>(bindName);
            Element.VerticalAlign = bind;

            return this;
        }

        public NuiImageBuilder<TViewModel> BindImageAspect(Expression<Func<TViewModel, NuiAspect>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiAspect>(bindName);
            Element.ImageAspect = bind;

            return this;
        }

        public NuiImageBuilder<TViewModel> BindImageRegion(Expression<Func<TViewModel, NuiRect>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiRect>(bindName);
            Element.ImageRegion = bind;

            return this;
        }

        public NuiImageBuilder<TViewModel> BindResRef(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.ResRef = bind;

            return this;
        }

    }

}
