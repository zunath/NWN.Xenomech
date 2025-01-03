using Anvil.API;
using System;
using XM.UI.Builder.Component;

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

        public NuiListTemplateCellBuilder<TViewModel> AddButton(Action<NuiButtonBuilder<TViewModel>> button)
        {
            var buttonBuilder = new NuiButtonBuilder<TViewModel>(RegisteredEvents);
            button(buttonBuilder);


            Element = buttonBuilder.Build();

            return this;
        }
        public NuiListTemplateCellBuilder<TViewModel> AddButtonImage(Action<NuiButtonImageBuilder<TViewModel>> buttonImage)
        {
            var buttonImageBuilder = new NuiButtonImageBuilder<TViewModel>(RegisteredEvents);
            buttonImage(buttonImageBuilder);

            Element = buttonImageBuilder.Build(); 

            return this;
        }

        public NuiListTemplateCellBuilder<TViewModel> AddButtonSelect(Action<NuiButtonSelectBuilder<TViewModel>> buttonSelect)
        {
            var buttonSelectBuilder = new NuiButtonSelectBuilder<TViewModel>(RegisteredEvents);
            buttonSelect(buttonSelectBuilder);

            Element = buttonSelectBuilder.Build(); 

            return this;
        }

        public NuiListTemplateCellBuilder<TViewModel> AddChart(Action<NuiChartBuilder<TViewModel>> chart)
        {
            var chartBuilder = new NuiChartBuilder<TViewModel>(RegisteredEvents);
            chart(chartBuilder);

            Element = chartBuilder.Build(); 

            return this;
        }

        public NuiListTemplateCellBuilder<TViewModel> AddCheck(Action<NuiCheckBuilder<TViewModel>> check)
        {
            var checkBuilder = new NuiCheckBuilder<TViewModel>(RegisteredEvents);
            check(checkBuilder);

            Element = checkBuilder.Build(); 

            return this;
        }

        public NuiListTemplateCellBuilder<TViewModel> AddColorPicker(Action<NuiColorPickerBuilder<TViewModel>> colorPicker)
        {
            var colorPickerBuilder = new NuiColorPickerBuilder<TViewModel>(RegisteredEvents);
            colorPicker(colorPickerBuilder);

            Element = colorPickerBuilder.Build(); 

            return this;
        }

        public NuiListTemplateCellBuilder<TViewModel> AddComboBox(Action<NuiComboBuilder<TViewModel>> comboBox)
        {
            var comboBoxBuilder = new NuiComboBuilder<TViewModel>(RegisteredEvents);
            comboBox(comboBoxBuilder);

            Element = comboBoxBuilder.Build(); 

            return this;
        }

        public NuiListTemplateCellBuilder<TViewModel> AddImage(Action<NuiImageBuilder<TViewModel>> image)
        {
            var imageBuilder = new NuiImageBuilder<TViewModel>(RegisteredEvents);
            image(imageBuilder);

            Element = imageBuilder.Build(); 

            return this;
        }

        public NuiListTemplateCellBuilder<TViewModel> AddLabel(Action<NuiLabelBuilder<TViewModel>> label)
        {
            var labelBuilder = new NuiLabelBuilder<TViewModel>(RegisteredEvents);
            label(labelBuilder);

            Element = labelBuilder.Build(); 

            return this;
        }

        public NuiListTemplateCellBuilder<TViewModel> AddOptions(Action<NuiOptionsBuilder<TViewModel>> options)
        {
            var optionsBuilder = new NuiOptionsBuilder<TViewModel>(RegisteredEvents);
            options(optionsBuilder);

            Element = optionsBuilder.Build(); 

            return this;
        }

        public NuiListTemplateCellBuilder<TViewModel> AddProgress(Action<NuiProgressBuilder<TViewModel>> progress)
        {
            var progressBuilder = new NuiProgressBuilder<TViewModel>(RegisteredEvents);
            progress(progressBuilder);

            Element = progressBuilder.Build(); 

            return this;
        }

        public NuiListTemplateCellBuilder<TViewModel> AddSlider(Action<NuiSliderBuilder<TViewModel>> slider)
        {
            var sliderBuilder = new NuiSliderBuilder<TViewModel>(RegisteredEvents);
            slider(sliderBuilder);

            Element = sliderBuilder.Build(); 

            return this;
        }

        public NuiListTemplateCellBuilder<TViewModel> AddSliderFloat(Action<NuiSliderFloatBuilder<TViewModel>> sliderFloat)
        {
            var sliderFloatBuilder = new NuiSliderFloatBuilder<TViewModel>(RegisteredEvents);
            sliderFloat(sliderFloatBuilder);

            Element = sliderFloatBuilder.Build(); 

            return this;
        }

        public NuiListTemplateCellBuilder<TViewModel> AddSpacer()
        {
            var spaceBuilder = new NuiSpacerBuilder<TViewModel>(RegisteredEvents);
            Element = spaceBuilder.Build(); 

            return this;
        }

        public NuiListTemplateCellBuilder<TViewModel> AddText(Action<NuiTextBuilder<TViewModel>> text)
        {
            var textBuilder = new NuiTextBuilder<TViewModel>(RegisteredEvents);
            text(textBuilder);

            Element = textBuilder.Build(); 

            return this;
        }

        public NuiListTemplateCellBuilder<TViewModel> AddTextEdit(Action<NuiTextEditBuilder<TViewModel>> textEdit)
        {
            var textEditBuilder = new NuiTextEditBuilder<TViewModel>(RegisteredEvents);
            textEdit(textEditBuilder);

            Element = textEditBuilder.Build(); 

            return this;
        }

        public NuiListTemplateCellBuilder<TViewModel> AddToggles(Action<NuiTogglesBuilder<TViewModel>> toggles)
        {
            var togglesBuilder = new NuiTogglesBuilder<TViewModel>(RegisteredEvents);
            toggles(togglesBuilder);

            Element = togglesBuilder.Build(); 

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