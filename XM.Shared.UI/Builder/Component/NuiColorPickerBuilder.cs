using Anvil.API;
using System.Linq.Expressions;
using System;
using XM.Shared.API.NUI;

namespace XM.UI.Builder.Component
{
    public class NuiColorPickerBuilder<TViewModel> 
        : NuiBuilderBase<NuiColorPickerBuilder<TViewModel>, TViewModel>
        where TViewModel : IViewModel
    {
        private Color _color;
        private string _colorBind;

        public NuiColorPickerBuilder(NuiEventCollection eventCollection)
            : base(eventCollection)
        {
            _color = new Color(0, 0, 0);
        }

        public NuiColorPickerBuilder<TViewModel> Color(Color color)
        {
            _color = color;
            return this;
        }

        public NuiColorPickerBuilder<TViewModel> Color(Expression<Func<TViewModel, Color>> expression)
        {
            _colorBind = GetBindName(expression);
            return this;
        }

        public override Json BuildEntity()
        {
            var color = string.IsNullOrWhiteSpace(_colorBind)
                ? Nui.Color(_color.Red, _color.Green, _color.Blue, _color.Alpha)
                : Nui.Bind(_colorBind);

            return Nui.ColorPicker(color);
        }
    }
}