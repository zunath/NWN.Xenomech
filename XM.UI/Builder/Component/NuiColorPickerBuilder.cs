using Anvil.API;
using System.Linq.Expressions;
using System;

namespace XM.UI.Builder.Component
{
    public class NuiColorPickerBuilder<TViewModel> : NuiBuilderBase<NuiColorPickerBuilder<TViewModel>, NuiColorPicker, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiColorPickerBuilder()
            : base(new NuiColorPicker(Color.FromRGBA(0)))
        {
        }

        public NuiColorPickerBuilder<TViewModel> SetColor(Color color)
        {
            Element.Color = color;
            return this;
        }
        public NuiColorPickerBuilder<TViewModel> BindColor(Expression<Func<TViewModel, Color>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<Color>(bindName);
            Element.Color = bind;

            return this;
        }

    }


}
