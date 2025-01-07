using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Xml.Linq;
using Anvil.API;
using XM.Shared.API.NUI;
using XM.UI.Builder.Component;
using NuiScrollbars = XM.Shared.API.NUI.NuiScrollbars;
using NuiStyle = XM.Shared.API.NUI.NuiStyle;

namespace XM.UI.Builder
{
    public class NuiListBuilder<TViewModel> : NuiBindable<TViewModel>, INuiComponentBuilder
        where TViewModel : IViewModel
    {
        private readonly List<NuiListTemplateCellBuilder<TViewModel>> _template = new();
        private string _rowCountBind;

        private float _rowHeight = NuiStyle.RowHeight;
        private bool _border = true;
        private NuiScrollbars _scrollbars = NuiScrollbars.Y;

        public NuiListBuilder(NuiEventCollection registeredEvents) 
            : base(registeredEvents)
        {
        }

        public NuiListBuilder<TViewModel> AddTemplateCell(
            Action<NuiListTemplateCellBuilder<TViewModel>> templateCell,
            Expression<Func<TViewModel, IBindingList>> targetList)
        {
            if (_template.Count >= 16)
                throw new InvalidOperationException("Maximum of 16 template cells allowed.");

            var builder = new NuiListTemplateCellBuilder<TViewModel>(RegisteredEvents);
            templateCell(builder);
            _template.Add(builder);

            if (string.IsNullOrWhiteSpace(_rowCountBind))
                _rowCountBind = GetBindName(targetList);

            return this;
        }

        public NuiListBuilder<TViewModel> RowHeight(float rowHeight)
        {
            _rowHeight = rowHeight;
            return this;
        }

        public NuiListBuilder<TViewModel> Border(bool border)
        {
            _border = border;
            return this;
        }

        public NuiListBuilder<TViewModel> Scrollbars(NuiScrollbars scrollbars)
        {
            if (scrollbars == NuiScrollbars.Auto)
                throw new InvalidOperationException("Scrollbars cannot be set to AUTO for NuiList.");

            _scrollbars = scrollbars;
            return this;
        }

        public Json Build()
        {
            var template = JsonArray();

            foreach (var element in _template)
            {
                template = JsonArrayInsert(template, element.Build());
            }

            var rowCount = Nui.Bind(_rowCountBind + "_" + nameof(NuiList.RowCount));

            return Nui.List(
                template, 
                rowCount, 
                _rowHeight, 
                _border, 
                _scrollbars);
        }
    }
}
