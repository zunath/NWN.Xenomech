using Anvil.API;
using System.Collections.Generic;

namespace XM.UI.Builder.Component
{
    public class NuiTogglesBuilder : NuiBuilderBase<NuiTogglesBuilder, NuiToggles>
    {
        public NuiTogglesBuilder()
            : base(new NuiToggles(NuiDirection.Horizontal, []))
        {
        }

        public NuiTogglesBuilder SetDirection(NuiDirection direction)
        {
            Element.Direction = direction;
            return this;
        }

        public NuiTogglesBuilder AddElement(string element)
        {
            Element.Elements.Add(element);
            return this;
        }

        public NuiTogglesBuilder AddElements(IEnumerable<string> elements)
        {
            Element.Elements.AddRange(elements);
            return this;
        }
    }

}
