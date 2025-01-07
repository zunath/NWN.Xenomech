using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Anvil.API;
using XM.Shared.API.NUI;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListBuilder<TViewModel>: NuiBindable<TViewModel>
        where TViewModel: IViewModel
    {
        private bool _isConstrained;
        private string _isConstrainedBind;

        private readonly List<INuiDrawListItemBuilder> _drawItems = new();

        public NuiDrawListBuilder(NuiEventCollection registeredEvents) 
            : base(registeredEvents)
        {
        }

        public NuiDrawListBuilder<TViewModel> IsConstrained(bool isConstrained)
        {
            _isConstrained = isConstrained;
            return this;
        }

        public NuiDrawListBuilder<TViewModel> IsConstrained(Expression<Func<TViewModel, bool>> expression)
        {
            _isConstrainedBind = GetBindName(expression);
            return this;
        }

        public NuiDrawListBuilder<TViewModel> AddArc(Action<NuiDrawListArcBuilder<TViewModel>> arc)
        {
            var builder = new NuiDrawListArcBuilder<TViewModel>(RegisteredEvents);
            arc(builder);
            _drawItems.Add(builder);

            return this;
        }

        public NuiDrawListBuilder<TViewModel> AddCircle(Action<NuiDrawListCircleBuilder<TViewModel>> circle)
        {
            var builder = new NuiDrawListCircleBuilder<TViewModel>(RegisteredEvents);
            circle(builder);
            _drawItems.Add(builder);

            return this;
        }

        public NuiDrawListBuilder<TViewModel> AddCurve(Action<NuiDrawListCurveBuilder<TViewModel>> curve)
        {
            var builder = new NuiDrawListCurveBuilder<TViewModel>(RegisteredEvents);
            curve(builder);
            _drawItems.Add(builder);

            return this;
        }

        public NuiDrawListBuilder<TViewModel> AddImage(Action<NuiDrawListImageBuilder<TViewModel>> image)
        {
            var builder = new NuiDrawListImageBuilder<TViewModel>(RegisteredEvents);
            image(builder);
            _drawItems.Add(builder);

            return this;
        }

        public NuiDrawListBuilder<TViewModel> AddLine(Action<NuiDrawListLineBuilder<TViewModel>> line)
        {
            var builder = new NuiDrawListLineBuilder<TViewModel>(RegisteredEvents);
            line(builder);
            _drawItems.Add(builder);

            return this;
        }

        public NuiDrawListBuilder<TViewModel> AddPolyLine(Action<NuiDrawListPolyLineBuilder<TViewModel>> polyLine)
        {
            var builder = new NuiDrawListPolyLineBuilder<TViewModel>(RegisteredEvents);
            polyLine(builder);
            _drawItems.Add(builder);

            return this;
        }

        public NuiDrawListBuilder<TViewModel> AddText(Action<NuiDrawListTextBuilder<TViewModel>> text)
        {
            var builder = new NuiDrawListTextBuilder<TViewModel>(RegisteredEvents);
            text(builder);
            _drawItems.Add(builder);

            return this;
        }

        public Json Build(Json targetElement)
        {
            var isConstrained = string.IsNullOrWhiteSpace(_isConstrainedBind)
                ? JsonBool(_isConstrained)
                : Nui.Bind(_isConstrainedBind);

            var drawList = JsonArray();
            foreach (var item in _drawItems)
            {
                drawList = JsonArrayInsert(drawList, item.Build());
            }

            return Nui.DrawList(targetElement, isConstrained, drawList);
        }

    }
}
