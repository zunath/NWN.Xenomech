using System;
using Anvil.API;
using XM.Shared.API.NUI;
using XM.UI.Builder.Component;
using XM.UI.Builder.Layout;

namespace XM.UI.Builder
{
    public class NuiListTemplateCellBuilder<TViewModel> 
        : NuiBindable<TViewModel>, INuiComponentBuilder
        where TViewModel : IViewModel
    {
        private INuiComponentBuilder _element;
        private float _width; // Default: auto
        private bool _isVariable = true; // Default: can grow

        public NuiListTemplateCellBuilder(NuiEventCollection registeredEvents) 
            : base(registeredEvents)
        {
        }

        public NuiListTemplateCellBuilder<TViewModel> AddGroup(Action<NuiGroupBuilder<TViewModel>> group)
        {
            var groupBuilder = new NuiGroupBuilder<TViewModel>(RegisteredEvents);
            group(groupBuilder);
            _element = groupBuilder;

            return this;
        }
        public NuiListTemplateCellBuilder<TViewModel> AddColumn(Action<NuiColumnBuilder<TViewModel>> group)
        {
            var columnBuilder = new NuiColumnBuilder<TViewModel>(RegisteredEvents);
            group(columnBuilder);
            _element = columnBuilder;

            return this;
        }
        public NuiListTemplateCellBuilder<TViewModel> AddRow(Action<NuiRowBuilder<TViewModel>> group)
        {
            var columnBuilder = new NuiRowBuilder<TViewModel>(RegisteredEvents);
            group(columnBuilder);
            _element = columnBuilder;

            return this;
        }

        public NuiListTemplateCellBuilder<TViewModel> Width(float width)
        {
            if (width < 0)
                throw new ArgumentOutOfRangeException(nameof(width), "Width must be 0 (auto) or greater than 1 for pixel width.");

            _width = width;
            return this;
        }

        public NuiListTemplateCellBuilder<TViewModel> IsVariable(bool isVariable)
        {
            _isVariable = isVariable;
            return this;
        }

        public Json Build()
        {
            if (_element == null)
                throw new InvalidOperationException("Element must be set for a NuiListTemplateCell.");

            var element = _element.Build();

            return Nui.ListTemplateCell(
                element,
                _width,
                _isVariable);
        }
    }
}