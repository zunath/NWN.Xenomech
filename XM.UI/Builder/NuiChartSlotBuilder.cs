using Anvil.API;
using System.Collections.Generic;

namespace XM.UI.Builder
{
    public class NuiChartSlotBuilder
    {
        private NuiChartType _chartType;
        private NuiProperty<string> _legend = string.Empty;
        private NuiProperty<Color> _color = new Color(255, 255, 255); // Default to white
        private NuiProperty<List<float>> _data = new List<float>();

        public NuiChartSlotBuilder WithChartType(NuiChartType chartType)
        {
            _chartType = chartType;
            return this;
        }

        public NuiChartSlotBuilder WithLegend(string legend)
        {
            _legend = legend;
            return this;
        }

        public NuiChartSlotBuilder WithColor(Color color)
        {
            _color = color;
            return this;
        }

        public NuiChartSlotBuilder WithData(List<float> data)
        {
            _data = data;
            return this;
        }

        public NuiChartSlot Build()
        {
            return new NuiChartSlot(_chartType, _legend, _color, _data);
        }
    }

}
