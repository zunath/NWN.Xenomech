using System;
using Anvil.API;
using System.Collections.Generic;

namespace XM.UI.Builder.Component
{
    public class NuiChartBuilder<TViewModel> : NuiBuilderBase<NuiChartBuilder<TViewModel>, NuiChart, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiChartBuilder(NuiEventCollection eventCollection)
            : base(new NuiChart(), eventCollection)
        {
        }

        public NuiChartBuilder<TViewModel> AddChartSlot(Action<NuiChartSlotBuilder<TViewModel>> chartSlot)
        {
            Element.ChartSlots ??= new List<NuiChartSlot>();

            var nuiChartSlotBuilder = new NuiChartSlotBuilder<TViewModel>(RegisteredEvents);
            chartSlot(nuiChartSlotBuilder);
            Element.ChartSlots.Add(nuiChartSlotBuilder.Build());
            return this;
        }
    }
}
