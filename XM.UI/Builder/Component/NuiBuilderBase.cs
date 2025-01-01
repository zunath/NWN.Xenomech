using Anvil.API;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace XM.UI.Builder.Component
{
    public abstract class NuiBuilderBase<TBuilder, TElement, TViewModel>
        where TBuilder : NuiBuilderBase<TBuilder, TElement, TViewModel>
        where TElement : NuiElement
        where TViewModel: IViewModel
    {
        protected readonly TElement Element;

        protected NuiBuilderBase(TElement element)
        {
            Element = element;
        }

        public TBuilder SetAspect(float? aspect)
        {
            Element.Aspect = aspect;
            return (TBuilder)this;
        }

        public TBuilder SetEnabled(bool enabled)
        {
            Element.Enabled = enabled;
            return (TBuilder)this;
        }

        public TBuilder SetForegroundColor(Color foregroundColor)
        {
            Element.ForegroundColor = foregroundColor;
            return (TBuilder)this;
        }

        public TBuilder SetHeight(float? height)
        {
            Element.Height = height;
            return (TBuilder)this;
        }

        public TBuilder SetId(string id)
        {
            Element.Id = id;
            return (TBuilder)this;
        }

        public TBuilder SetMargin(float? margin)
        {
            Element.Margin = margin;
            return (TBuilder)this;
        }

        public TBuilder SetPadding(float? padding)
        {
            Element.Padding = padding;
            return (TBuilder)this;
        }

        public TBuilder SetTooltip(string tooltip)
        {
            Element.Tooltip = tooltip;
            return (TBuilder)this;
        }

        public TBuilder SetVisible(bool visible)
        {
            Element.Visible = visible;
            return (TBuilder)this;
        }

        public TBuilder SetWidth(float? width)
        {
            Element.Width = width;
            return (TBuilder)this;
        }

        public TBuilder SetDrawList(List<NuiDrawListItem> drawList)
        {
            Element.DrawList = drawList;
            return (TBuilder)this;
        }

        public TBuilder SetScissor(bool scissor)
        {
            Element.Scissor = scissor;
            return (TBuilder)this;
        }

        public TBuilder SetDisabledTooltip(string disabledTooltip)
        {
            Element.DisabledTooltip = disabledTooltip;
            return (TBuilder)this;
        }

        public TBuilder SetEncouraged(bool encouraged)
        {
            Element.Encouraged = encouraged;
            return (TBuilder)this;
        }


        public TBuilder BindEnabled(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.Enabled = bind;
            return (TBuilder)this;
        }

        public TBuilder BindForegroundColor(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<Color>(bindName);
            Element.ForegroundColor = bind;
            return (TBuilder)this;
        }

        public TBuilder BindTooltip(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.Tooltip = bind;
            return (TBuilder)this;
        }

        public TBuilder BindVisible(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.Visible = bind;
            return (TBuilder)this;
        }


        public TBuilder BindScissor(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.Scissor = bind;
            return (TBuilder)this;
        }

        public TBuilder BindDisabledTooltip(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            Element.DisabledTooltip = bind;
            return (TBuilder)this;
        }

        public TBuilder BindEncouraged(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<bool>(bindName);
            Element.Encouraged = bind;
            return (TBuilder)this;
        }

        public TElement Build()
        {
            return Element;
        }

        protected string GetBindName<T>(Expression<Func<TViewModel, T>> expression)
        {
            return GetPropertyName(expression);
        }

        /// <summary>
        /// Retrieves the name of the property targeted in an expression.
        /// </summary>
        /// <typeparam name="TProperty">The type of property being targeted.</typeparam>
        /// <param name="expression">Expression to target the property.</param>
        /// <returns>The name of the property.</returns>
        private static string GetPropertyName<TProperty>(Expression<Func<TViewModel, TProperty>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException($"Expression '{expression}' refers to a method, not a property.");

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException($"Expression '{expression}' refers to a field, not a property.");

            //if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
            //    throw new ArgumentException($"Expression '{expression}' refers to a property that is not from type {type}.");

            return propInfo.Name;
        }
    }
}
