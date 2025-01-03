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
        public NuiListBuilder(NuiEventCollection eventCollection)
            : base(new NuiList(new List<NuiListTemplateCell>(), 0), eventCollection)
        {
        }

        public NuiListBuilder<TViewModel> AddTemplate(Action<NuiListTemplateCellBuilder<TViewModel>> template)
        {
            var templateBuilder = new NuiListTemplateCellBuilder<TViewModel>(RegisteredEvents);
            template(templateBuilder);

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

        public NuiListBuilder<TViewModel> RowCount(Expression<Func<TViewModel, IBindingList>> expression)
        {
            var bindName = GetBindName(expression);
            var bind = new NuiBind<int>(bindName + "_" + nameof(NuiList.RowCount));
            Element.RowCount = bind;

            return this;
        }


    }

}
