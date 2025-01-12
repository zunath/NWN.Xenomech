using System;
using System.Collections.Generic;
using Anvil.API;
using XM.Shared.API.NUI;
using XM.UI.Builder.Component;
using NuiScrollbars = XM.Shared.API.NUI.NuiScrollbars;

namespace XM.UI.Builder.Layout
{
    public class NuiRowBuilder<TViewModel> 
        : NuiBuilderBase<NuiRowBuilder<TViewModel>, TViewModel>
        where TViewModel : IViewModel
    {
        private readonly List<INuiComponentBuilder> _children = new();

        public NuiRowBuilder(NuiEventCollection registeredEvents) : base(registeredEvents)
        {
        }

        public NuiRowBuilder<TViewModel> AddColumn(Action<NuiColumnBuilder<TViewModel>> column)
        {
            var columnBuilder = new NuiColumnBuilder<TViewModel>(RegisteredEvents);
            column(columnBuilder);

            _children.Add(columnBuilder);

            return this;
        }

        public NuiRowBuilder<TViewModel> AddGroup(Action<NuiGroupBuilder<TViewModel>> group)
        {
            var groupBuilder = new NuiGroupBuilder<TViewModel>(RegisteredEvents);
            group(groupBuilder);

            _children.Add(groupBuilder);

            return this;
        }

        public NuiRowBuilder<TViewModel> AddPartialPlaceholder(string id)
        {
            var groupBuilder = new NuiGroupBuilder<TViewModel>(RegisteredEvents);
            groupBuilder
                .SetLayout(col => { })
                .Border(false)
                .Scrollbars(NuiScrollbars.None)
                .Id(id);

            _children.Add(groupBuilder);

            return this;
        }

        public NuiRowBuilder<TViewModel> AddButton(Action<NuiButtonBuilder<TViewModel>> button)
        {
            var buttonBuilder = new NuiButtonBuilder<TViewModel>(RegisteredEvents);
            button(buttonBuilder);

            _children.Add(buttonBuilder);

            return this;
        }

        public NuiRowBuilder<TViewModel> AddButtonImage(Action<NuiButtonImageBuilder<TViewModel>> buttonImage)
        {
            var buttonImageBuilder = new NuiButtonImageBuilder<TViewModel>(RegisteredEvents);
            buttonImage(buttonImageBuilder);

            _children.Add(buttonImageBuilder);

            return this;
        }

        public NuiRowBuilder<TViewModel> AddButtonSelect(Action<NuiButtonSelectBuilder<TViewModel>> buttonSelect)
        {
            var buttonSelectBuilder = new NuiButtonSelectBuilder<TViewModel>(RegisteredEvents);
            buttonSelect(buttonSelectBuilder);

            _children.Add(buttonSelectBuilder);

            return this;
        }

        public NuiRowBuilder<TViewModel> AddChart(Action<NuiChartBuilder<TViewModel>> chart)
        {
            var chartBuilder = new NuiChartBuilder<TViewModel>(RegisteredEvents);
            chart(chartBuilder);

            _children.Add(chartBuilder);

            return this;
        }

        public NuiRowBuilder<TViewModel> AddCheck(Action<NuiCheckBuilder<TViewModel>> check)
        {
            var checkBuilder = new NuiCheckBuilder<TViewModel>(RegisteredEvents);
            check(checkBuilder);

            _children.Add(checkBuilder);

            return this;
        }

        public NuiRowBuilder<TViewModel> AddColorPicker(Action<NuiColorPickerBuilder<TViewModel>> colorPicker)
        {
            var colorPickerBuilder = new NuiColorPickerBuilder<TViewModel>(RegisteredEvents);
            colorPicker(colorPickerBuilder);

            _children.Add(colorPickerBuilder);

            return this;
        }

        public NuiRowBuilder<TViewModel> AddComboBox(Action<NuiComboBuilder<TViewModel>> comboBox)
        {
            var comboBoxBuilder = new NuiComboBuilder<TViewModel>(RegisteredEvents);
            comboBox(comboBoxBuilder);

            _children.Add(comboBoxBuilder);

            return this;
        }

        public NuiRowBuilder<TViewModel> AddImage(Action<NuiImageBuilder<TViewModel>> image)
        {
            var imageBuilder = new NuiImageBuilder<TViewModel>(RegisteredEvents);
            image(imageBuilder);

            _children.Add(imageBuilder);

            return this;
        }

        public NuiRowBuilder<TViewModel> AddLabel(Action<NuiLabelBuilder<TViewModel>> label)
        {
            var labelBuilder = new NuiLabelBuilder<TViewModel>(RegisteredEvents);
            label(labelBuilder);

            _children.Add(labelBuilder);

            return this;
        }

        public NuiRowBuilder<TViewModel> AddOptions(Action<NuiOptionsBuilder<TViewModel>> options)
        {
            var optionsBuilder = new NuiOptionsBuilder<TViewModel>(RegisteredEvents);
            options(optionsBuilder);

            _children.Add(optionsBuilder);

            return this;
        }

        public NuiRowBuilder<TViewModel> AddProgress(Action<NuiProgressBuilder<TViewModel>> progress)
        {
            var progressBuilder = new NuiProgressBuilder<TViewModel>(RegisteredEvents);
            progress(progressBuilder);

            _children.Add(progressBuilder);

            return this;
        }

        public NuiRowBuilder<TViewModel> AddSlider(Action<NuiSliderBuilder<TViewModel>> slider)
        {
            var sliderBuilder = new NuiSliderBuilder<TViewModel>(RegisteredEvents);
            slider(sliderBuilder);

            _children.Add(sliderBuilder);

            return this;
        }

        public NuiRowBuilder<TViewModel> AddSliderFloat(Action<NuiSliderFloatBuilder<TViewModel>> sliderFloat)
        {
            var sliderFloatBuilder = new NuiSliderFloatBuilder<TViewModel>(RegisteredEvents);
            sliderFloat(sliderFloatBuilder);

            _children.Add(sliderFloatBuilder);

            return this;
        }

        public NuiRowBuilder<TViewModel> AddSpacer()
        {
            var spaceBuilder = new NuiSpacerBuilder<TViewModel>(RegisteredEvents);
            _children.Add(spaceBuilder);

            return this;
        }

        public NuiRowBuilder<TViewModel> AddText(Action<NuiTextBuilder<TViewModel>> text)
        {
            var textBuilder = new NuiTextBuilder<TViewModel>(RegisteredEvents);
            text(textBuilder);

            _children.Add(textBuilder);

            return this;
        }

        public NuiRowBuilder<TViewModel> AddTextEdit(Action<NuiTextEditBuilder<TViewModel>> textEdit)
        {
            var textEditBuilder = new NuiTextEditBuilder<TViewModel>(RegisteredEvents);
            textEdit(textEditBuilder);

            _children.Add(textEditBuilder);

            return this;
        }

        public NuiRowBuilder<TViewModel> AddToggles(Action<NuiTogglesBuilder<TViewModel>> toggles)
        {
            var togglesBuilder = new NuiTogglesBuilder<TViewModel>(RegisteredEvents);
            toggles(togglesBuilder);

            _children.Add(togglesBuilder);

            return this;
        }

        public NuiRowBuilder<TViewModel> AddList(Action<NuiListBuilder<TViewModel>> list)
        {
            var listBuilder = new NuiListBuilder<TViewModel>(RegisteredEvents);
            list(listBuilder);

            _children.Add(listBuilder);

            return this;
        }

        public override Json BuildEntity()
        {
            var row = JsonArray();
            foreach (var element in _children)
            {
                row = JsonArrayInsert(row, element.Build());
            }

            return Nui.Row(row);
        }
    }
}