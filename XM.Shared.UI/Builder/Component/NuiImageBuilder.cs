using Anvil.API;
using System.Linq.Expressions;
using System;
using XM.Shared.API.NUI;
using NuiAspect = XM.Shared.API.NUI.NuiAspect;

namespace XM.UI.Builder.Component
{
    public class NuiImageBuilder<TViewModel> : NuiBuilderBase<NuiImageBuilder<TViewModel>, TViewModel>
        where TViewModel : IViewModel
    {
        private NuiHAlign _horizontalAlign;
        private string _horizontalAlignBind;

        private NuiVAlign _verticalAlign;
        private string _verticalAlignBind;

        private NuiAspect _imageAspect;
        private string _imageAspectBind;

        private NuiRect _imageRegion;
        private string _imageRegionBind;

        private string _resRef;
        private string _resRefBind;

        public NuiImageBuilder(NuiEventCollection eventCollection)
            : base(eventCollection)
        {
        }

        public NuiImageBuilder<TViewModel> HorizontalAlign(NuiHAlign horizontalAlign)
        {
            _horizontalAlign = horizontalAlign;
            return this;
        }

        public NuiImageBuilder<TViewModel> VerticalAlign(NuiVAlign verticalAlign)
        {
            _verticalAlign = verticalAlign;
            return this;
        }

        public NuiImageBuilder<TViewModel> ImageAspect(NuiAspect aspect)
        {
            _imageAspect = aspect;
            return this;
        }

        public NuiImageBuilder<TViewModel> ImageRegion(NuiRect region)
        {
            _imageRegion = region;
            return this;
        }

        public NuiImageBuilder<TViewModel> ResRef(string resRef)
        {
            _resRef = resRef;
            return this;
        }

        public NuiImageBuilder<TViewModel> HorizontalAlign(Expression<Func<TViewModel, NuiHAlign>> expression)
        {
            _horizontalAlignBind = GetBindName(expression);
            return this;
        }

        public NuiImageBuilder<TViewModel> VerticalAlign(Expression<Func<TViewModel, NuiVAlign>> expression)
        {
            _verticalAlignBind = GetBindName(expression);
            return this;
        }

        public NuiImageBuilder<TViewModel> ImageAspect(Expression<Func<TViewModel, NuiAspect>> expression)
        {
            _imageAspectBind = GetBindName(expression);
            return this;
        }

        public NuiImageBuilder<TViewModel> ImageRegion(Expression<Func<TViewModel, NuiRect>> expression)
        {
            _imageRegionBind = GetBindName(expression);
            return this;
        }

        public NuiImageBuilder<TViewModel> ResRef(Expression<Func<TViewModel, string>> expression)
        {
            _resRefBind = GetBindName(expression);
            return this;
        }
        public NuiImageBuilder<TViewModel> ResRef(Expression<Func<TViewModel, GuiBindingList<string>>> expression)
        {
            _resRefBind = GetBindName(expression);
            return this;
        }

        public override Json BuildEntity()
        {
            var horizontalAlign = string.IsNullOrWhiteSpace(_horizontalAlignBind)
                ? JsonInt((int)_horizontalAlign)
                : Nui.Bind(_horizontalAlignBind);

            var verticalAlign = string.IsNullOrWhiteSpace(_verticalAlignBind)
                ? JsonInt((int)_verticalAlign)
                : Nui.Bind(_verticalAlignBind);

            var imageAspect = string.IsNullOrWhiteSpace(_imageAspectBind)
                ? JsonInt((int)_imageAspect)
                : Nui.Bind(_imageAspectBind);

            var imageRegion = string.IsNullOrWhiteSpace(_imageRegionBind)
                ? Nui.Rect(_imageRegion.X, _imageRegion.Y, _imageRegion.Width, _imageRegion.Height)
                : Nui.Bind(_imageRegionBind);

            var resRef = string.IsNullOrWhiteSpace(_resRefBind)
                ? JsonString(_resRef)
                : Nui.Bind(_resRefBind);

            var element = Nui.Image(
                resRef, 
                imageAspect,
                horizontalAlign, 
                verticalAlign);

            element = Nui.ImageRegion(element, imageRegion);


            return element;
        }
    }
}
