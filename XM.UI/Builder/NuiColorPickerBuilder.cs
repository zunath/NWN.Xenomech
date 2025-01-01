using Anvil.API;

namespace XM.UI.Builder
{
    public class NuiColorPickerBuilder : NuiBuilderBase<NuiColorPickerBuilder, NuiColorPicker>
    {
        public NuiColorPickerBuilder(Color color)
            : base(new NuiColorPicker(color))
        {
        }

        public NuiColorPickerBuilder WithColor(Color color)
        {
            Element.Color = color;
            return this;
        }
    }


}
