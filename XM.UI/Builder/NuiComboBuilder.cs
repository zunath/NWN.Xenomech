using Anvil.API;
using System.Collections.Generic;

namespace XM.UI.Builder
{
    public class NuiComboBuilder : NuiBuilderBase<NuiComboBuilder, NuiCombo>
    {
        private readonly List<NuiComboEntry> _entries = new();

        public NuiComboBuilder()
            : base(new NuiCombo())
        {
        }

        public NuiComboBuilder AddEntry(string label, int value)
        {
            var entry = new NuiComboEntry(label, value);

            _entries.Add(entry);

            return this;
        }

        public NuiComboBuilder WithSelected(int selected)
        {
            Element.Selected = selected;
            Element.Entries = _entries;
            return this;
        }
    }
}
