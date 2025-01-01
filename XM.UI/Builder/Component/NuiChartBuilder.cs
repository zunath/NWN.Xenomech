using Anvil.API;
using System.Collections.Generic;
using System.Linq;

namespace XM.UI.Builder.Component
{
    public class NuiChartBuilder : NuiBuilderBase<NuiChartBuilder, NuiChart>
    {
        public NuiChartBuilder()
            : base(new NuiChart())
        {
        }

        public NuiChartBuilder SetChartSlots(IEnumerable<NuiChartSlot> chartSlots)
        {
            Element.ChartSlots = chartSlots?.ToList();
            return this;
        }

        public NuiChartBuilder AddChartSlot(NuiChartSlot chartSlot)
        {
            Element.ChartSlots ??= new List<NuiChartSlot>();
            Element.ChartSlots.Add(chartSlot);
            return this;
        }
    }

}
