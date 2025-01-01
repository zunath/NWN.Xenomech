using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Anvil.API;

namespace XM.UI
{
    internal class ModelElement<TElement, TViewModel>
        where TElement: NuiElement
        where TViewModel: IViewModel
    {
        public Dictionary<string, string> Binds { get; set; }
        public TElement Element { get; set; }

        public ModelElement(TElement element)
        {
            Element = element;
        }

        public void Set<TProperty>(Expression<Func<TElement, TProperty>> prop, TProperty value)
        {
            var propertyName = GuiHelper<TElement>.GetPropertyName(prop);
            typeof(TElement).GetProperty(propertyName)?.SetValue(Element, value);
        }

        public void Bind<TProperty>(
            Expression<Func<TElement, object>> prop,
            Expression<Func<TViewModel, TProperty>> vm)
        {
            var elementProp = GuiHelper<TElement>.GetPropertyName(prop);
            var vmProp = GuiHelper<TViewModel>.GetPropertyName(vm);

            Binds[elementProp] = vmProp;
        }
    }
}