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

        public NuiComboBuilder(NuiEventCollection eventCollection)
            : base(new NuiCombo(), eventCollection)
        {
        }

        public NuiComboBuilder<TViewModel> Option(string label, int value)
        {
            var entry = new NuiComboEntry(label, value);

            _entries.Add(entry);

            return this;
        }

        public NuiComboBuilder<TViewModel> IsSelected(int selected)
        {
            Element.Selected = selected;

            return this;
        }
        public NuiComboBuilder<TViewModel> IsSelected(Expression<Func<TViewModel, int>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<int>(bindName);
            Element.Selected = bind;

            return this;
        }

        public NuiComboBuilder<TViewModel> Option(Expression<Func<TViewModel, List<NuiComboEntry>>> expression)
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
