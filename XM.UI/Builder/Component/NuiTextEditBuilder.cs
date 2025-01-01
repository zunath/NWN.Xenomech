using Anvil.API;

namespace XM.UI.Builder.Component
{
    public class NuiTextEditBuilder : NuiBuilderBase<NuiTextEditBuilder, NuiTextEdit>
    {
        public NuiTextEditBuilder()
            : base(new NuiTextEdit(string.Empty, string.Empty, 1000, false))
        {
        }

        public NuiTextEditBuilder SetLabel(string label)
        {
            Element.Label = label;
            return this;
        }

        public NuiTextEditBuilder SetValue(string value)
        {
            Element.Value = value;
            return this;
        }

        public NuiTextEditBuilder SetMaxLength(ushort maxLength)
        {
            Element.MaxLength = maxLength;
            return this;
        }

        public NuiTextEditBuilder SetMultiLine(bool multiLine)
        {
            Element.MultiLine = multiLine;
            return this;
        }

        public NuiTextEditBuilder SetWordWrap(bool wordWrap)
        {
            Element.WordWrap = wordWrap;
            return this;
        }
    }
}
