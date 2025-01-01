using Anvil.API;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace XM.UI.Builder.Component
{
    public class NuiTogglesBuilder<TViewModel> : NuiBuilderBase<NuiTogglesBuilder<TViewModel>, NuiToggles, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiTogglesBuilder()
            : base(new NuiToggles(NuiDirection.Horizontal, []))
        {
        }

        public NuiTogglesBuilder<TViewModel> SetDirection(NuiDirection direction)
        {
            Element.Direction = direction;
            return this;
        }

        public NuiTogglesBuilder<TViewModel> AddElement(string element)
        {
            Element.Elements.Add(element);
            return this;
        }

        public NuiTogglesBuilder<TViewModel> AddElements(IEnumerable<string> elements)
        {
            Element.Elements.AddRange(elements);
            return this;
        }
    }

}
