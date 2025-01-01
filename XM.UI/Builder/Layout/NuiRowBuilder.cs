using Anvil.API;
using System;
using XM.UI.Builder.Component;

namespace XM.UI.Builder.Layout
{
    public class NuiRowBuilder<TViewModel> : NuiBuilderBase<NuiRowBuilder<TViewModel>, NuiRow, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiRowBuilder()
            : base(new NuiRow())
        {
        }

        public NuiRowBuilder<TViewModel> AddColumn(Action<NuiColumnBuilder<TViewModel>> column)
        {
            var columnBuilder = new NuiColumnBuilder<TViewModel>();
            column(columnBuilder);

            Element.Children.Add(columnBuilder.Build());

            return this;
        }

        public NuiRowBuilder<TViewModel> AddButton(Action<NuiButtonBuilder<TViewModel>> button)
        {
            var buttonBuilder = new NuiButtonBuilder<TViewModel>();
            button(buttonBuilder);

            Element.Children.Add(buttonBuilder.Build());

            return this;
        }

        public NuiRowBuilder<TViewModel> AddButtonImage(Action<NuiButtonImageBuilder<TViewModel>> buttonImage)
        {
            var buttonImageBuilder = new NuiButtonImageBuilder<TViewModel>();
            buttonImage(buttonImageBuilder);

            Element.Children.Add(buttonImageBuilder.Build());

            return this;
        }

        public NuiRowBuilder<TViewModel> AddButtonSelect(Action<NuiButtonSelectBuilder<TViewModel>> buttonSelect)
        {
            var buttonSelectBuilder = new NuiButtonSelectBuilder<TViewModel>();
            buttonSelect(buttonSelectBuilder);

            Element.Children.Add(buttonSelectBuilder.Build());

            return this;
        }

        public NuiRowBuilder<TViewModel> AddChart(Action<NuiChartBuilder<TViewModel>> chart)
        {
            var chartBuilder = new NuiChartBuilder<TViewModel>();
            chart(chartBuilder);

            Element.Children.Add(chartBuilder.Build());

            return this;
        }

        public NuiRowBuilder<TViewModel> AddCheck(Action<NuiCheckBuilder<TViewModel>> check)
        {
            var checkBuilder = new NuiCheckBuilder<TViewModel>();
            check(checkBuilder);

            Element.Children.Add(checkBuilder.Build());

            return this;
        }

        public NuiRowBuilder<TViewModel> AddColorPicker(Action<NuiColorPickerBuilder<TViewModel>> colorPicker)
        {
            var colorPickerBuilder = new NuiColorPickerBuilder<TViewModel>();
            colorPicker(colorPickerBuilder);

            Element.Children.Add(colorPickerBuilder.Build());

            return this;
        }

        public NuiRowBuilder<TViewModel> AddComboBox(Action<NuiComboBuilder<TViewModel>> comboBox)
        {
            var comboBoxBuilder = new NuiComboBuilder<TViewModel>();
            comboBox(comboBoxBuilder);

            Element.Children.Add(comboBoxBuilder.Build());

            return this;
        }

        public NuiRowBuilder<TViewModel> AddImage(Action<NuiImageBuilder<TViewModel>> image)
        {
            var imageBuilder = new NuiImageBuilder<TViewModel>();
            image(imageBuilder);

            Element.Children.Add(imageBuilder.Build());

            return this;
        }

        public NuiRowBuilder<TViewModel> AddLabel(Action<NuiLabelBuilder<TViewModel>> label)
        {
            var labelBuilder = new NuiLabelBuilder<TViewModel>();
            label(labelBuilder);

            Element.Children.Add(labelBuilder.Build());

            return this;
        }

        public NuiRowBuilder<TViewModel> AddOptions(Action<NuiOptionsBuilder<TViewModel>> options)
        {
            var optionsBuilder = new NuiOptionsBuilder<TViewModel>();
            options(optionsBuilder);

            Element.Children.Add(optionsBuilder.Build());

            return this;
        }

        public NuiRowBuilder<TViewModel> AddProgress(Action<NuiProgressBuilder<TViewModel>> progress)
        {
            var progressBuilder = new NuiProgressBuilder<TViewModel>();
            progress(progressBuilder);

            Element.Children.Add(progressBuilder.Build());

            return this;
        }

        public NuiRowBuilder<TViewModel> AddSlider(Action<NuiSliderBuilder<TViewModel>> slider)
        {
            var sliderBuilder = new NuiSliderBuilder<TViewModel>();
            slider(sliderBuilder);

            Element.Children.Add(sliderBuilder.Build());

            return this;
        }

        public NuiRowBuilder<TViewModel> AddSliderFloat(Action<NuiSliderFloatBuilder<TViewModel>> sliderFloat)
        {
            var sliderFloatBuilder = new NuiSliderFloatBuilder<TViewModel>();
            sliderFloat(sliderFloatBuilder);

            Element.Children.Add(sliderFloatBuilder.Build());

            return this;
        }

        public NuiRowBuilder<TViewModel> AddSpacer()
        {
            var spaceBuilder = new NuiSpacerBuilder<TViewModel>();
            Element.Children.Add(spaceBuilder.Build());

            return this;
        }

        public NuiRowBuilder<TViewModel> AddText(Action<NuiTextBuilder<TViewModel>> text)
        {
            var textBuilder = new NuiTextBuilder<TViewModel>();
            text(textBuilder);

            Element.Children.Add(textBuilder.Build());

            return this;
        }

        public NuiRowBuilder<TViewModel> AddTextEdit(Action<NuiTextEditBuilder<TViewModel>> textEdit)
        {
            var textEditBuilder = new NuiTextEditBuilder<TViewModel>();
            textEdit(textEditBuilder);

            Element.Children.Add(textEditBuilder.Build());

            return this;
        }

        public NuiRowBuilder<TViewModel> AddToggles(Action<NuiTogglesBuilder<TViewModel>> toggles)
        {
            var togglesBuilder = new NuiTogglesBuilder<TViewModel>();
            toggles(togglesBuilder);

            Element.Children.Add(togglesBuilder.Build());

            return this;
        }
    }

}
