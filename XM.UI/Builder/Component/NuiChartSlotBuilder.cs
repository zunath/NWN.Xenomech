using Anvil.API;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace XM.UI.Builder.Component
{
    public class NuiChartSlotBuilder<TViewModel>: NuiBindable<TViewModel>
        where TViewModel: IViewModel
    {
        private NuiChartType _chartType;
        private NuiProperty<string> _legend = string.Empty;
        private NuiProperty<Color> _color = new Color(255, 255, 255); // Default to white
        private NuiProperty<List<float>> _data = new List<float>();

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

        public NuiChartSlotBuilder<TViewModel> Legend(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            _legend = bind;

            return this;
        }

        public NuiChartSlotBuilder<TViewModel> Color(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<Color>(bindName);
            _color = bind;

            return this;
        }

        public NuiChartSlotBuilder<TViewModel> Data(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<List<float>>(bindName);
            _data = bind;

            return this;
        }


        public NuiChartSlot Build()
        {
            return new NuiChartSlot(_chartType, _legend, _color, _data);
        }

    }

}
