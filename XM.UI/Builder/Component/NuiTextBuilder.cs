using Anvil.API;

namespace XM.UI.Builder.Component
{
    public class NuiTextBuilder : NuiBuilderBase<NuiTextBuilder, NuiText>
    {
        public NuiTextBuilder()
            : base(new NuiText(string.Empty))
        {
        }

        public NuiTextBuilder WithText(string text)
        {
            Element.Text = text;
            return this;
        }

        public NuiTextBuilder WithBorder(bool border)
        {
            Element.Border = border;
            return this;
        }

        public NuiTextBuilder WithScrollbars(NuiScrollbars scrollbars)
        {
            Element.Scrollbars = scrollbars;
            return this;
        }
    }

}
