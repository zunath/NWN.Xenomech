using Anvil.API;
using System.Collections.Generic;
using System.Linq;

namespace XM.UI.Builder.Component
{
    public class NuiChartBuilder<TViewModel> : NuiBuilderBase<NuiChartBuilder<TViewModel>, NuiChart, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiChartBuilder()
            : base(new NuiChart())
        {
        }

        public NuiChartBuilder<TViewModel> SetChartSlots(IEnumerable<NuiChartSlot> chartSlots)
        {
            Element.ChartSlots = chartSlots?.ToList();
            return this;
        }

        public NuiChartBuilder<TViewModel> AddChartSlot(NuiChartSlot chartSlot)
        {
            Element.ChartSlots ??= new List<NuiChartSlot>();
            Element.ChartSlots.Add(chartSlot);
            return this;
        }
    }

}
