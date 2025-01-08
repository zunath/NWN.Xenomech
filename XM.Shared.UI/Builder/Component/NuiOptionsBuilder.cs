using Anvil.API;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using XM.Shared.API.NUI;
using NuiDirection = XM.Shared.API.NUI.NuiDirection;

namespace XM.UI.Builder.Component
{
    public class NuiOptionsBuilder<TViewModel> : NuiBuilderBase<NuiOptionsBuilder<TViewModel>, TViewModel>
        where TViewModel : IViewModel
    {
        private NuiDirection _direction;
        private readonly List<string> _optionLabels = new();

        private int _selection;
        private string _selectionBind;

        public NuiOptionsBuilder(NuiEventCollection eventCollection)
            : base(eventCollection)
        {
        }

        public NuiOptionsBuilder<TViewModel> Direction(NuiDirection direction)
        {
            _direction = direction;
            return this;
        }

        public NuiOptionsBuilder<TViewModel> Option(string option)
        {
            _optionLabels.Add(option);
            return this;
        }

        public NuiOptionsBuilder<TViewModel> Selection(int selection)
        {
            _selection = selection;
            return this;
        }

        public NuiOptionsBuilder<TViewModel> Selection(Expression<Func<TViewModel, int>> expression)
        {
            _selectionBind = GetBindName(expression);
            return this;
        }

        public override Json BuildEntity()
        {
            var selection = string.IsNullOrWhiteSpace(_selectionBind)
                ? JsonInt(_selection)
                : Nui.Bind(_selectionBind);

            var optionLabels = JsonArray();

            foreach (var option in _optionLabels)
            {
                optionLabels = JsonArrayInsert(optionLabels, JsonString(option));
            }

            return Nui.Options(_direction, optionLabels, selection);
        }
    }
}