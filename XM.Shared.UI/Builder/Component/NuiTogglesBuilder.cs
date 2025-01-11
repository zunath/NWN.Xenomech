﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Anvil.API;
using XM.Shared.API.NUI;
using XM.Shared.Core.Localization;
using NuiDirection = XM.Shared.API.NUI.NuiDirection;

namespace XM.UI.Builder.Component
{
    public class NuiTogglesBuilder<TViewModel> : NuiBuilderBase<NuiTogglesBuilder<TViewModel>, TViewModel>
        where TViewModel : IViewModel
    {
        private NuiDirection _direction;
        private readonly List<LocaleString> _optionLabels = new();

        private int _selectedValue;
        private string _selectedValueBind;


        public NuiTogglesBuilder(NuiEventCollection eventCollection) 
            : base(eventCollection)
        {
        }

        public NuiTogglesBuilder<TViewModel> AddOption(LocaleString label)
        {
            _optionLabels.Add(label);
            return this;
        }

        public NuiTogglesBuilder<TViewModel> Direction(NuiDirection direction)
        {
            _direction = direction;
            return this;
        }

        public NuiTogglesBuilder<TViewModel> SelectedValue(int selectedValue)
        {
            _selectedValue = selectedValue;
            return this;
        }

        public NuiTogglesBuilder<TViewModel> SelectedValue(Expression<Func<TViewModel, int>> expression)
        {
            _selectedValueBind = GetBindName(expression);
            return this;
        }

        public override Json BuildEntity()
        {
            var selectedValue = string.IsNullOrWhiteSpace(_selectedValueBind)
                ? JsonInt(_selectedValue)
                : Nui.Bind(_selectedValueBind);

            var optionLabels = JsonArray();
            foreach (var option in _optionLabels)
            {
                var optionText = Locale.GetString(option);
                optionLabels = JsonArrayInsert(optionLabels, JsonString(optionText));
            }

            return Nui.Toggles(_direction, optionLabels, selectedValue);
        }
    }
}