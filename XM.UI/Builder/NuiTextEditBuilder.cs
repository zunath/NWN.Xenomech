using Anvil.API;

namespace XM.UI.Builder
{
    public class NuiTextEditBuilder : NuiBuilderBase<NuiTextEditBuilder, NuiTextEdit>
    {
        public NuiTextEditBuilder(string label, string value, ushort maxLength, bool multiLine)
            : base(new NuiTextEdit(label, value, maxLength, multiLine))
        {
        }

        public NuiTextEditBuilder WithLabel(string label)
        {
            Element.Label = label;
            return this;
        }

        public NuiTextEditBuilder WithValue(string value)
        {
            Element.Value = value;
            return this;
        }

        public NuiTextEditBuilder WithMaxLength(ushort maxLength)
        {
            Element.MaxLength = maxLength;
            return this;
        }

        public NuiTextEditBuilder WithMultiLine(bool multiLine)
        {
            Element.MultiLine = multiLine;
            return this;
        }

        public NuiTextEditBuilder WithWordWrap(bool wordWrap)
        {
            Element.WordWrap = wordWrap;
            return this;
        }
    }
}
