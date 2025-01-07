using System;
using Anvil.API;
using System.Collections.Generic;
using XM.Shared.API.NUI;

namespace XM.UI.Builder.Component
{
    public class NuiChartBuilder<TViewModel> 
        : NuiBuilderBase<NuiChartBuilder<TViewModel>, TViewModel>
        where TViewModel : IViewModel
    {
        private readonly List<NuiChartSlotBuilder<TViewModel>> _chartSlots = new();

        public NuiChartBuilder(NuiEventCollection eventCollection)
            : base(eventCollection)
        {
        }

        public NuiChartBuilder<TViewModel> AddChartSlot(Action<NuiChartSlotBuilder<TViewModel>> chartSlot)
        {
            var nuiChartSlotBuilder = new NuiChartSlotBuilder<TViewModel>(RegisteredEvents);
            chartSlot(nuiChartSlotBuilder);
            _chartSlots.Add(nuiChartSlotBuilder);
            return this;
        }

        public override Json BuildEntity()
        {
            var chartSlots = JsonArray();

            foreach (var slot in _chartSlots)
            {
                chartSlots = JsonArrayInsert(chartSlots, slot.Build());
            }

            return Nui.Chart(chartSlots);
        }
    }
}