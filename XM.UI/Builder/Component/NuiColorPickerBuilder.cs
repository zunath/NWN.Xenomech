using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiColorPickerBuilder<TViewModel> : NuiBuilderBase<NuiColorPickerBuilder<TViewModel>, NuiColorPicker, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiColorPickerBuilder(NuiEventCollection eventCollection)
            : base(new NuiColorPicker(Anvil.API.Color.FromRGBA(0)), eventCollection)
        {
        }

        public NuiColorPickerBuilder<TViewModel> Color(Color color)
        {
            Element.Color = color;
            return this;
        }
        public NuiColorPickerBuilder<TViewModel> Color(Expression<Func<TViewModel, Color>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<Color>(bindName);
            Element.Color = bind;

            return this;
        }

    }


}
