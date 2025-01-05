using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using Anvil.API;
using XM.UI.Builder.Component;

namespace XM.UI.Builder
{
    public class NuiListBuilder<TViewModel> : NuiBuilderBase<NuiListBuilder<TViewModel>, NuiList, TViewModel>
        where TViewModel : IViewModel
    {
        private string _bindName;

        public NuiListBuilder(NuiEventCollection eventCollection)
            : base(new NuiList(new List<NuiListTemplateCell>(), 0), eventCollection)
        {
        }

        public NuiListBuilder<TViewModel> AddTemplate(
            Action<NuiListTemplateCellBuilder<TViewModel>> template,
            Expression<Func<TViewModel, IBindingList>> targetList)
        {
            var templateBuilder = new NuiListTemplateCellBuilder<TViewModel>(RegisteredEvents);
            template(templateBuilder);

            if (string.IsNullOrWhiteSpace(_bindName))
                _bindName = GetBindName(targetList);

            Element.RowTemplate.Add(templateBuilder.Build());

            return this;
        }
        public NuiListBuilder<TViewModel> RowHeight(float rowHeight)
        {
            Element.RowHeight = rowHeight;
            return this;
        }

        public NuiListBuilder<TViewModel> Scrollbars(NuiScrollbars scrollbars)
        {
            Element.Scrollbars = scrollbars;
            return this;
        }

        private void CreateListBind()
        {
            var bind = new NuiBind<int>(_bindName + "_" + nameof(NuiList.RowCount));
            Element.RowCount = bind;
        }

        public override NuiList Build()
        {
            CreateListBind();


            return base.Build();
        }
    }

}
