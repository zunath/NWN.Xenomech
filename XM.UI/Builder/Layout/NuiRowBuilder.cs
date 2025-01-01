using Anvil.API;
using System;
using XM.UI.Builder.Component;

namespace XM.UI.Builder.Layout
{
    public class NuiRowBuilder : NuiBuilderBase<NuiRowBuilder, NuiRow>
    {
        public NuiRowBuilder()
            : base(new NuiRow())
        {
        }

        public NuiRowBuilder AddColumn(Action<NuiColumnBuilder> column)
        {
            var columnBuilder = new NuiColumnBuilder();
            column(columnBuilder);

            Element.Children.Add(columnBuilder.Build());

            return this;
        }

        public NuiRowBuilder AddButton(Action<NuiButtonBuilder> button)
        {
            var buttonBuilder = new NuiButtonBuilder();
            button(buttonBuilder);

            Element.Children.Add(buttonBuilder.Build());

            return this;
        }

        public NuiRowBuilder AddButtonImage(Action<NuiButtonImageBuilder> buttonImage)
        {
            var buttonImageBuilder = new NuiButtonImageBuilder();
            buttonImage(buttonImageBuilder);

            Element.Children.Add(buttonImageBuilder.Build());

            return this;
        }

        public NuiRowBuilder AddButtonSelect(Action<NuiButtonSelectBuilder> buttonSelect)
        {
            var buttonSelectBuilder = new NuiButtonSelectBuilder();
            buttonSelect(buttonSelectBuilder);

            Element.Children.Add(buttonSelectBuilder.Build());

            return this;
        }

        public NuiRowBuilder AddChart(Action<NuiChartBuilder> chart)
        {
            var chartBuilder = new NuiChartBuilder();
            chart(chartBuilder);

            Element.Children.Add(chartBuilder.Build());

            return this;
        }

        public NuiRowBuilder AddCheck(Action<NuiCheckBuilder> check)
        {
            var checkBuilder = new NuiCheckBuilder();
            check(checkBuilder);

            Element.Children.Add(checkBuilder.Build());

            return this;
        }

        public NuiRowBuilder AddColorPicker(Action<NuiColorPickerBuilder> colorPicker)
        {
            var colorPickerBuilder = new NuiColorPickerBuilder();
            colorPicker(colorPickerBuilder);

            Element.Children.Add(colorPickerBuilder.Build());

            return this;
        }

        public NuiRowBuilder AddComboBox(Action<NuiComboBuilder> comboBox)
        {
            var comboBoxBuilder = new NuiComboBuilder();
            comboBox(comboBoxBuilder);

            Element.Children.Add(comboBoxBuilder.Build());

            return this;
        }

        public NuiRowBuilder AddImage(Action<NuiImageBuilder> image)
        {
            var imageBuilder = new NuiImageBuilder();
            image(imageBuilder);

            Element.Children.Add(imageBuilder.Build());

            return this;
        }

        public NuiRowBuilder AddLabel(Action<NuiLabelBuilder> label)
        {
            var labelBuilder = new NuiLabelBuilder();
            label(labelBuilder);

            Element.Children.Add(labelBuilder.Build());

            return this;
        }

        public NuiRowBuilder AddOptions(Action<NuiOptionsBuilder> options)
        {
            var optionsBuilder = new NuiOptionsBuilder();
            options(optionsBuilder);

            Element.Children.Add(optionsBuilder.Build());

            return this;
        }

        public NuiRowBuilder AddProgress(Action<NuiProgressBuilder> progress)
        {
            var progressBuilder = new NuiProgressBuilder();
            progress(progressBuilder);

            Element.Children.Add(progressBuilder.Build());

            return this;
        }

        public NuiRowBuilder AddSlider(Action<NuiSliderBuilder> slider)
        {
            var sliderBuilder = new NuiSliderBuilder();
            slider(sliderBuilder);

            Element.Children.Add(sliderBuilder.Build());

            return this;
        }

        public NuiRowBuilder AddSliderFloat(Action<NuiSliderFloatBuilder> sliderFloat)
        {
            var sliderFloatBuilder = new NuiSliderFloatBuilder();
            sliderFloat(sliderFloatBuilder);

            Element.Children.Add(sliderFloatBuilder.Build());

            return this;
        }

        public NuiRowBuilder AddSpace(Action<NuiSpacerBuilder> space)
        {
            var spaceBuilder = new NuiSpacerBuilder();
            space(spaceBuilder);

            Element.Children.Add(spaceBuilder.Build());

            return this;
        }

        public NuiRowBuilder AddText(Action<NuiTextBuilder> text)
        {
            var textBuilder = new NuiTextBuilder();
            text(textBuilder);

            Element.Children.Add(textBuilder.Build());

            return this;
        }

        public NuiRowBuilder AddTextEdit(Action<NuiTextEditBuilder> textEdit)
        {
            var textEditBuilder = new NuiTextEditBuilder();
            textEdit(textEditBuilder);

            Element.Children.Add(textEditBuilder.Build());

            return this;
        }

        public NuiRowBuilder AddToggles(Action<NuiTogglesBuilder> toggles)
        {
            var togglesBuilder = new NuiTogglesBuilder();
            toggles(togglesBuilder);

            Element.Children.Add(togglesBuilder.Build());

            return this;
        }
    }

}
