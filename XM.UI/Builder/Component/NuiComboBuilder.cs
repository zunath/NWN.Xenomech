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

        public NuiComboBuilder<TViewModel> AddOption(string label, int value)
        {
            var entry = new NuiComboEntry(label, value);

            _entries.Add(entry);

            return this;
        }

        public NuiComboBuilder<TViewModel> SetSelected(int selected)
        {
            Element.Selected = selected;

            return this;
        }
        public NuiComboBuilder<TViewModel> BindSelected(Expression<Func<TViewModel, int>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<int>(bindName);
            Element.Selected = bind;

            return this;
        }

        public NuiComboBuilder<TViewModel> BindOptions(Expression<Func<TViewModel, List<NuiComboEntry>>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<List<NuiComboEntry>>(bindName);
            Element.Entries = bind;

            return this;
        }

        public override NuiCombo Build()
        {
            if (_entries.Count > 0)
                Element.Entries = _entries;

            return base.Build();
        }
    }
}
