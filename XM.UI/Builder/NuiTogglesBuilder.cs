using Anvil.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XM.UI.Builder
{
    public class NuiTogglesBuilder : NuiBuilderBase<NuiTogglesBuilder, NuiToggles>
    {
        public NuiTogglesBuilder(NuiDirection direction, List<string> elements)
            : base(new NuiToggles(direction, elements))
        {
        }

        public NuiTogglesBuilder WithDirection(NuiDirection direction)
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
