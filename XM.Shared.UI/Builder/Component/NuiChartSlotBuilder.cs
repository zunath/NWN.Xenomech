using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Anvil.API;
using XM.Shared.API.NUI;
using NuiChartType = XM.Shared.API.NUI.NuiChartType;

namespace XM.UI.Builder.Component
{
    public class NuiChartSlotBuilder<TViewModel>
        : NuiBindable<TViewModel>, INuiComponentBuilder
        where TViewModel: IViewModel
    {
        private NuiChartType _chartType;
        
        private string _legend = string.Empty;
        private string _legendBind;

        private Color _color = new(255, 255, 255);
        private string _colorBind;

        private List<float> _data = new();
        private string _dataBind;

        public NuiChartSlotBuilder(NuiEventCollection registeredEvents) 
            : base(registeredEvents)
        {
        }

        public NuiChartSlotBuilder<TViewModel> ChartType(NuiChartType chartType)
        {
            _chartType = chartType;
            return this;
        }

        public NuiChartSlotBuilder<TViewModel> Legend(string legend)
        {
            _legend = legend;
            return this;
        }

        public NuiChartSlotBuilder<TViewModel> Color(Color color)
        {
            _color = color;
            return this;
        }

        public NuiChartSlotBuilder<TViewModel> Data(List<float> data)
        {
            _data = data;
            return this;
        }

        public NuiChartSlotBuilder<TViewModel> Legend(Expression<Func<TViewModel, string>> expression)
        {
            var bindName = GetBindName(expression);
            _legendBind = bindName;

            return this;
        }

        public NuiChartSlotBuilder<TViewModel> Color(Expression<Func<TViewModel, Color>> expression)
        {
            var bindName = GetBindName(expression);
            _colorBind = bindName;

            return this;
        }

        public NuiChartSlotBuilder<TViewModel> Data(Expression<Func<TViewModel, float>> expression)
        {
            var bindName = GetBindName(expression);
            _dataBind = bindName;

            return this;
        }


        public Json Build()
        {
            var legend = string.IsNullOrWhiteSpace(_legendBind)
                ? JsonString(_legend)
                : Nui.Bind(_legendBind);

            var color = string.IsNullOrWhiteSpace(_colorBind)
                ? Nui.Color(_color.Red, _color.Green, _color.Blue, _color.Alpha)
                : Nui.Bind(_colorBind);
            
            var data = JsonArray();

            if (string.IsNullOrWhiteSpace(_dataBind))
            {
                foreach (var d in _data)
                {
                    data = JsonArrayInsert(data, JsonFloat(d));
                }
            }
            else
            {
                data = Nui.Bind(_dataBind);
            }

            return Nui.ChartSlot(_chartType, legend, color, data);
        }

    }

}
