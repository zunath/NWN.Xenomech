using Anvil.API;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Action = System.Action;

namespace XM.UI.Builder.Component
{
    public abstract class NuiBuilderBase<TBuilder, TElement, TViewModel>: NuiBindable<TViewModel>
        where TBuilder : NuiBuilderBase<TBuilder, TElement, TViewModel>
        where TElement : NuiElement
        where TViewModel: IViewModel
    {
        protected readonly TElement Element;

        protected NuiBuilderBase(TElement element, NuiEventCollection eventCollection) 
            : base(eventCollection)
        {
            Element = element;
        }


        public TBuilder Aspect(float? aspect)
        {
            Element.Aspect = aspect;
            return (TBuilder)this;
        }

        public TBuilder IsEnabled(bool enabled)
        {
            Element.Enabled = enabled;
            return (TBuilder)this;
        }

        public TBuilder ForegroundColor(Color foregroundColor)
        {
            Element.ForegroundColor = foregroundColor;
            return (TBuilder)this;
        }

        public TBuilder Height(float? height)
        {
            Element.Height = height;
            return (TBuilder)this;
        }

        public TBuilder Id(string id)
        {
            Element.Id = id;
            return (TBuilder)this;
        }

        public TBuilder Margin(float? margin)
        {
            Element.Margin = margin;
            return (TBuilder)this;
        }

        public TBuilder Padding(float? padding)
        {
            Element.Padding = padding;
            return (TBuilder)this;
        }

        public TBuilder TooltipText(string tooltip)
        {
            Element.Tooltip = tooltip;
            return (TBuilder)this;
        }

        public TBuilder IsVisible(bool visible)
        {
            Element.Visible = visible;
            return (TBuilder)this;
        }

        public TBuilder Width(float? width)
        {
            Element.Width = width;
            return (TBuilder)this;
        }

        public TBuilder DrawList(List<NuiDrawListItem> drawList)
        {
            Element.DrawList = drawList;
            return (TBuilder)this;
        }

        public TBuilder Scissor(bool scissor)
        {
            Element.Scissor = scissor;
            return (TBuilder)this;
        }

        public TBuilder DisabledTooltipText(string disabledTooltip)
        {
            Element.DisabledTooltip = disabledTooltip;
            return (TBuilder)this;
        }

        public TBuilder IsEncouraged(bool encouraged)
        {
            Element.Encouraged = encouraged;
            return (TBuilder)this;
        }

        

        public TBuilder IsEnabled(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.Enabled = bind;
            return (TBuilder)this;
        }

        public TBuilder ForegroundColor(Expression<Func<TViewModel, Color>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<Color>(bindName);
            Element.ForegroundColor = bind;
            return (TBuilder)this;
        }

        public TBuilder TooltipText(Expression<Func<TViewModel, string>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.Tooltip = bind;
            return (TBuilder)this;
        }

        public TBuilder IsVisible(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.Visible = bind;
            return (TBuilder)this;
        }


        public TBuilder Scissor(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.Scissor = bind;
            return (TBuilder)this;
        }

        public TBuilder DisabledTooltipText(Expression<Func<TViewModel, string>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.DisabledTooltip = bind;
            return (TBuilder)this;
        }

        public TBuilder IsEncouraged(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.Encouraged = bind;
            return (TBuilder)this;
        }

        private void BindAction(NuiEventType eventType, Expression<Func<TViewModel, Action>> expression)
        {
            if (string.IsNullOrWhiteSpace(Element.Id))
                Element.Id = Guid.NewGuid().ToString();

            RaisesNuiEvents = true;
            var methodName = GetBindName(expression);

            if (!RegisteredEvents.ContainsKey(Element.Id))
                RegisteredEvents[Element.Id] = new Dictionary<NuiEventType, string>();

            RegisteredEvents[Element.Id][eventType] = methodName;
        }

        public TBuilder OnClick(Expression<Func<TViewModel, Action>> expression)
        {
            BindAction(NuiEventType.Click, expression);
            return (TBuilder)this;
        }

        public TBuilder OnBlur(Expression<Func<TViewModel, Action>> expression)
        {
            BindAction(NuiEventType.Blur, expression);
            return (TBuilder)this;
        }

        public TBuilder OnMouseDown(Expression<Func<TViewModel, Action>> expression)
        {
            BindAction(NuiEventType.MouseDown, expression);
            return (TBuilder)this;
        }

        public TBuilder OnMouseUp(Expression<Func<TViewModel, Action>> expression)
        {
            BindAction(NuiEventType.MouseUp, expression);
            return (TBuilder)this;
        }

        public virtual TElement Build()
        {
            // Events only get raised in NUI if the element has an Id.
            // Ensure one is assigned if it wasn't set previously.
            if (string.IsNullOrWhiteSpace(Element.Id) &&
                RaisesNuiEvents)
            {
                Element.Id = Guid.NewGuid().ToString();
            }

            return Element;
        }

    }
}
