using Anvil.API;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace XM.UI.Builder.Component
{
    public class NuiComboBuilder<TViewModel> : NuiBuilderBase<NuiComboBuilder<TViewModel>, NuiCombo, TViewModel>
        where TViewModel: IViewModel
    {
        private readonly List<NuiComboEntry> _entries = new();

        public NuiComboBuilder()
            : base(new NuiCombo())
        {
        }

        public NuiComboBuilder<TViewModel> AddEntry(string label, int value)
        {
            var entry = new NuiComboEntry(label, value);

            _entries.Add(entry);

            return this;
        }

        public NuiComboBuilder<TViewModel> SetSelected(int selected)
        {
            Element.Selected = selected;
            Element.Entries = _entries;
            return this;
        }
        public NuiComboBuilder<TViewModel> BindSelected(Expression<Func<TViewModel, int>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<int>(bindName);
            Element.Selected = bind;
            Element.Entries = _entries;

            return this;
        }

    }
}
