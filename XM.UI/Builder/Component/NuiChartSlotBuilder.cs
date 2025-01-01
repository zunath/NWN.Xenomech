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

        public NuiChartSlotBuilder<TViewModel> SetChartType(NuiChartType chartType)
        {
            _chartType = chartType;
            return this;
        }

        public NuiChartSlotBuilder<TViewModel> SetLegend(string legend)
        {
            _legend = legend;
            return this;
        }

        public NuiChartSlotBuilder<TViewModel> SetColor(Color color)
        {
            _color = color;
            return this;
        }

        public NuiChartSlotBuilder<TViewModel> SetData(List<float> data)
        {
            _data = data;
            return this;
        }

        public NuiChartSlotBuilder<TViewModel> BindLegend(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<string>(bindName);
            _legend = bind;

            return this;
        }

        public NuiChartSlotBuilder<TViewModel> BindColor(Expression<Func<TViewModel, object>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<Color>(bindName);
            _color = bind;

            return this;
        }

        public NuiChartSlotBuilder<TViewModel> BindData(Expression<Func<TViewModel, object>> expression)
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
