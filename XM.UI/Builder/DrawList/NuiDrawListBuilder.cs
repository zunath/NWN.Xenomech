using System;
using System.Collections.Generic;
using Anvil.API;

namespace XM.UI.Builder.DrawList
{
    public class NuiDrawListBuilder<TViewModel>
        where TViewModel: IViewModel
    {
        private readonly List<NuiDrawListItem> _drawItems = new();


        public NuiDrawListBuilder<TViewModel> AddArc(Action<NuiDrawListArcBuilder<TViewModel>> arc)
        {
            var builder = new NuiDrawListArcBuilder<TViewModel>();
            arc(builder);
            _drawItems.Add(builder.Build());

            return this;
        }

        public NuiDrawListBuilder<TViewModel> AddCircle(Action<NuiDrawListCircleBuilder<TViewModel>> circle)
        {
            var builder = new NuiDrawListCircleBuilder<TViewModel>();
            circle(builder);
            _drawItems.Add(builder.Build());

            return this;
        }

        public NuiDrawListBuilder<TViewModel> AddCurve(Action<NuiDrawListCurveBuilder<TViewModel>> curve)
        {
            var builder = new NuiDrawListCurveBuilder<TViewModel>();
            curve(builder);
            _drawItems.Add(builder.Build());

            return this;
        }

        public NuiDrawListBuilder<TViewModel> AddImage(Action<NuiDrawListImageBuilder<TViewModel>> image)
        {
            var builder = new NuiDrawListImageBuilder<TViewModel>();
            image(builder);
            _drawItems.Add(builder.Build());

            return this;
        }

        public NuiDrawListBuilder<TViewModel> AddLine(Action<NuiDrawListLineBuilder<TViewModel>> line)
        {
            var builder = new NuiDrawListLineBuilder<TViewModel>();
            line(builder);
            _drawItems.Add(builder.Build());

            return this;
        }

        public NuiDrawListBuilder<TViewModel> AddPolyLine(Action<NuiDrawListPolyLineBuilder<TViewModel>> polyLine)
        {
            var builder = new NuiDrawListPolyLineBuilder<TViewModel>();
            polyLine(builder);
            _drawItems.Add(builder.Build());

            return this;
        }

        public NuiDrawListBuilder<TViewModel> AddText(Action<NuiDrawListTextBuilder<TViewModel>> text)
        {
            var builder = new NuiDrawListTextBuilder<TViewModel>();
            text(builder);
            _drawItems.Add(builder.Build());

            return this;
        }


        public List<NuiDrawListItem> Build()
        {
            return _drawItems;
        }
    }
}
