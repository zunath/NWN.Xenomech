using Anvil.API;
using System;
using XM.UI.Builder.Component;
using XM.UI.Builder.Layout;

namespace XM.UI.Builder
{
    public class NuiListTemplateCellBuilder<TViewModel>
        where TViewModel: IViewModel
    {
        private NuiElement Element { get; set; }
        private bool _variableSize;
        private float _width;

        private NuiEventCollection RegisteredEvents { get; }

        public NuiListTemplateCellBuilder(NuiEventCollection eventCollection)
        {
            RegisteredEvents = eventCollection;
        }

        public NuiListTemplateCellBuilder<TViewModel> SetVariableSize(bool variableSize)
        {
            _variableSize = variableSize;
            return this;
        }

        public NuiListTemplateCellBuilder<TViewModel> Width(float width)
        {
            _width = width;
            return this;
        }

        public NuiListTemplateCellBuilder<TViewModel> AddGroup(Action<NuiGroupBuilder<TViewModel>> group)
        {
            var groupBuilder = new NuiGroupBuilder<TViewModel>(RegisteredEvents);
            group(groupBuilder);

            Element = groupBuilder.Build();

            return this;
        }

        public NuiListTemplateCell Build()
        {
            return new NuiListTemplateCell(Element)
            {
                VariableSize = _variableSize,
                Width = _width
            };
        }
    }
}