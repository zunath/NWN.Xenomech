using Anvil.API;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using XM.Shared.API.NUI;
using XM.Shared.Core;
using XM.Shared.Core.Localization;

namespace XM.UI.Builder.Component
{
    public class NuiComboBuilder<TViewModel> 
        : NuiBuilderBase<NuiComboBuilder<TViewModel>, TViewModel>
        where TViewModel : IViewModel
    {
        private int _selected;
        private string _selectedBind;
        private string _entriesBind;
        private readonly List<NuiComboEntryBuilder<TViewModel>> _options = new();

        public NuiComboBuilder(NuiEventCollection eventCollection)
            : base(eventCollection)
        {
        }

        public NuiComboBuilder<TViewModel> Option(LocaleString label, int value)
        {
            var optionBuilder = new NuiComboEntryBuilder<TViewModel>(RegisteredEvents);
            optionBuilder.Label(label);
            optionBuilder.Value(value);
            _options.Add(optionBuilder);

            return this;
        }

        public NuiComboBuilder<TViewModel> Option(Action<NuiComboEntryBuilder<TViewModel>> option)
        {
            var optionBuilder = new NuiComboEntryBuilder<TViewModel>(RegisteredEvents);
            option(optionBuilder);
            _options.Add(optionBuilder);

            return this;
        }

        public NuiComboBuilder<TViewModel> Selection(int selected)
        {
            _selected = selected;
            return this;
        }

        public NuiComboBuilder<TViewModel> Selection(Expression<Func<TViewModel, int>> expression)
        {
            _selectedBind = GetBindName(expression);
            return this;
        }

        public NuiComboBuilder<TViewModel> Option(Expression<Func<TViewModel, XMBindingList<NuiComboEntry>>> expression)
        {
            _entriesBind = GetBindName(expression);
            return this;
        }

        public override Json BuildEntity()
        {
            var options = JsonArray();
            if (string.IsNullOrWhiteSpace(_entriesBind))
            {
                foreach (var option in _options)
                {
                    options = JsonArrayInsert(options, option.BuildEntity());
                }
            }
            else
            {
                options = Nui.Bind(_entriesBind);
            }

            var selected = string.IsNullOrWhiteSpace(_selectedBind)
                ? JsonInt(_selected)
                : Nui.Bind(_selectedBind);

            return Nui.Combo(options, selected);
        }
    }
}