using Anvil.API;
using System.Collections.Generic;

namespace XM.UI.Builder.Component
{
    public class NuiTogglesBuilder<TViewModel> : NuiBuilderBase<NuiTogglesBuilder<TViewModel>, NuiToggles, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiTogglesBuilder(NuiEventCollection eventCollection)
            : base(new NuiToggles(NuiDirection.Horizontal, []), eventCollection)
        {
        }

        public NuiTogglesBuilder<TViewModel> Direction(NuiDirection direction)
        {
            Element.Direction = direction;
            return this;
        }

        public NuiTogglesBuilder<TViewModel> Option(string element)
        {
            Element.Elements.Add(element);
            return this;
        }

        public NuiTogglesBuilder<TViewModel> Option(IEnumerable<string> elements)
        {
            Element.Elements.AddRange(elements);
            return this;
        }
    }

}
