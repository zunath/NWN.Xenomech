using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiImageBuilder<TViewModel> : NuiBuilderBase<NuiImageBuilder<TViewModel>, NuiImage, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiImageBuilder(NuiEventCollection eventCollection)
            : base(new NuiImage(string.Empty), eventCollection)
        {
        }

        public NuiImageBuilder<TViewModel> HorizontalAlign(NuiHAlign horizontalAlign)
        {
            Element.HorizontalAlign = horizontalAlign;
            return this;
        }

        public NuiImageBuilder<TViewModel> VerticalAlign(NuiVAlign verticalAlign)
        {
            Element.VerticalAlign = verticalAlign;
            return this;
        }

        public NuiImageBuilder<TViewModel> ImageAspect(NuiAspect aspect)
        {
            Element.ImageAspect = aspect;
            return this;
        }

        public NuiImageBuilder<TViewModel> ImageRegion(NuiRect region)
        {
            Element.ImageRegion = region;
            return this;
        }

        public NuiImageBuilder<TViewModel> ResRef(string resRef)
        {
            Element.ResRef = resRef;
            return this;
        }
        public NuiImageBuilder<TViewModel> HorizontalAlign(Expression<Func<TViewModel, NuiHAlign>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiHAlign>(bindName);
            Element.HorizontalAlign = bind;

            return this;
        }

        public NuiImageBuilder<TViewModel> VerticalAlign(Expression<Func<TViewModel, NuiVAlign>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiVAlign>(bindName);
            Element.VerticalAlign = bind;

            return this;
        }

        public NuiImageBuilder<TViewModel> ImageAspect(Expression<Func<TViewModel, NuiAspect>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiAspect>(bindName);
            Element.ImageAspect = bind;

            return this;
        }

        public NuiImageBuilder<TViewModel> ImageRegion(Expression<Func<TViewModel, NuiRect>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<NuiRect>(bindName);
            Element.ImageRegion = bind;

            return this;
        }

        public NuiImageBuilder<TViewModel> ResRef(Expression<Func<TViewModel, string>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.ResRef = bind;

            return this;
        }

    }

}
