using System;
using System.Linq.Expressions;
using Anvil.API;
using XM.Shared.API.NUI;
using XM.Shared.Core;
using NuiAspect = XM.Shared.API.NUI.NuiAspect;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListImageBuilder<TViewModel>
        : NuiBindable<TViewModel>, INuiDrawListItemBuilder
        where TViewModel : IViewModel
    {
        private bool _isEnabled = true;
        private string _isEnabledBind;

        private string _resRef;
        private string _resRefBind;

        private NuiRect _position;
        private string _positionBind;

        private NuiAspect _aspect;
        private string _aspectBind;

        private NuiHAlign _horizontalAlign;
        private string _horizontalAlignBind;

        private NuiVAlign _verticalAlign;
        private string _verticalAlignBind;

        private NuiDrawListItemOrderType _order = NuiDrawListItemOrderType.After;
        private NuiDrawListItemRenderType _render = NuiDrawListItemRenderType.Always;
        private bool _bindArrays;

        private NuiRect _drawTextureRegion;
        private string _drawTextureRegionBind;
        private bool _hasDrawRegion;

        public NuiDrawListImageBuilder(NuiEventCollection registeredEvents)
            : base(registeredEvents)
        {
            _position = new NuiRect(0, 0, 0, 0);
        }

        public NuiDrawListImageBuilder<TViewModel> IsEnabled(bool isEnabled)
        {
            _isEnabled = isEnabled;
            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> IsEnabled(Expression<Func<TViewModel, bool>> expression)
        {
            _isEnabledBind = GetBindName(expression);
            return this;
        }
        public NuiDrawListImageBuilder<TViewModel> IsEnabled(Expression<Func<TViewModel, XMBindingList<bool>>> expression)
        {
            _isEnabledBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> ResRef(string resRef)
        {
            _resRef = resRef;
            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> ResRef(Expression<Func<TViewModel, string>> expression)
        {
            _resRefBind = GetBindName(expression);
            return this;
        }
        public NuiDrawListImageBuilder<TViewModel> ResRef(Expression<Func<TViewModel, XMBindingList<string>>> expression)
        {
            _resRefBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> Position(NuiRect position)
        {
            _position = position;
            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> Position(float x, float y, float width, float height)
        {
            _position = new NuiRect(x, y, width, height);
            return this;
        }


        public NuiDrawListImageBuilder<TViewModel> Position(Expression<Func<TViewModel, NuiRect>> expression)
        {
            _positionBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> Aspect(NuiAspect aspect)
        {
            _aspect = aspect;
            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> Aspect(Expression<Func<TViewModel, NuiAspect>> expression)
        {
            _aspectBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> HorizontalAlign(NuiHAlign horizontalAlign)
        {
            _horizontalAlign = horizontalAlign;
            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> HorizontalAlign(Expression<Func<TViewModel, NuiHAlign>> expression)
        {
            _horizontalAlignBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> VerticalAlign(NuiVAlign verticalAlign)
        {
            _verticalAlign = verticalAlign;
            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> VerticalAlign(Expression<Func<TViewModel, NuiVAlign>> expression)
        {
            _verticalAlignBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> Order(NuiDrawListItemOrderType order)
        {
            _order = order;
            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> Render(NuiDrawListItemRenderType render)
        {
            _render = render;
            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> BindArrays(bool bindArrays)
        {
            _bindArrays = bindArrays;
            return this;
        }

        public NuiDrawListImageBuilder<TViewModel> DrawTextureRegion(NuiRect drawTextureRegion)
        {
            _drawTextureRegion = drawTextureRegion;
            _hasDrawRegion = true;
            return this;
        }
        public NuiDrawListImageBuilder<TViewModel> DrawTextureRegion(Expression<Func<TViewModel, NuiRect>> expression)
        {
            _drawTextureRegionBind = GetBindName(expression);
            _hasDrawRegion = true;
            return this;
        }

        public Json Build()
        {
            var isEnabled = string.IsNullOrWhiteSpace(_isEnabledBind)
                ? JsonBool(_isEnabled)
                : Nui.Bind(_isEnabledBind);

            var resRef = string.IsNullOrWhiteSpace(_resRefBind)
                ? JsonString(_resRef)
                : Nui.Bind(_resRefBind);

            var position = string.IsNullOrWhiteSpace(_positionBind)
                ? Nui.Rect(_position.X, _position.Y, _position.Width, _position.Height)
                : Nui.Bind(_positionBind);

            var aspect = string.IsNullOrWhiteSpace(_aspectBind)
                ? JsonInt((int)_aspect)
                : Nui.Bind(_aspectBind);

            var horizontalAlign = string.IsNullOrWhiteSpace(_horizontalAlignBind)
                ? JsonInt((int)_horizontalAlign)
                : Nui.Bind(_horizontalAlignBind);

            var verticalAlign = string.IsNullOrWhiteSpace(_verticalAlignBind)
                ? JsonInt((int)_verticalAlign)
                : Nui.Bind(_verticalAlignBind);

            var drawTextureRegion = string.IsNullOrWhiteSpace(_drawTextureRegionBind)
                ? Nui.Rect(_drawTextureRegion.X, _drawTextureRegion.Y, _drawTextureRegion.Width, _drawTextureRegion.Height)
                : Nui.Bind(_drawTextureRegionBind);

            var element = Nui.DrawListImage(
                isEnabled,
                resRef,
                position,
                aspect,
                horizontalAlign,
                verticalAlign,
                _order,
                _render,
                _bindArrays);

            if (_hasDrawRegion)
            {
                element = Nui.DrawListImageRegion(element, drawTextureRegion);
            }

            return element;
        }
    }
}
