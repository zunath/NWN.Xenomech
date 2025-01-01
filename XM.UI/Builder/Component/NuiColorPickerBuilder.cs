using Anvil.API;

namespace XM.UI.Builder.Component
{
    public class NuiColorPickerBuilder : NuiBuilderBase<NuiColorPickerBuilder, NuiColorPicker>
    {
        public NuiColorPickerBuilder()
            : base(new NuiColorPicker(Color.FromRGBA(0)))
        {
        }

        public NuiColorPickerBuilder SetColor(Color color)
        {
            Element.Color = color;
            return this;
        }
    }


}
