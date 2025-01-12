using Anvil.API;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using XM.Shared.API.NUI;
using XM.UI.Builder.DrawList;
using Action = System.Action;

namespace XM.UI.Builder.Component
{
    public abstract class NuiBuilderBase<TBuilder, TViewModel>
        : NuiBindable<TViewModel>, INuiComponentBuilder
        where TBuilder : NuiBuilderBase<TBuilder, TViewModel>
        where TViewModel: IViewModel
    {
        private float _aspect;

        private bool _isEnabled = true;
        private string _isEnabledBind;

        private Color? _foregroundColor;
        private string _foregroundColorBind;

        private float _height;

        private string _id;

        private float _margin = -1;

        private float _padding;

        private string _tooltipText;
        private string _tooltipTextBind;

        private bool _isVisible = true;
        private string _isVisibleBind;

        private float _width;

        private bool _useScissor;
        private string _scissorBind;

        private string _disabledTooltip;
        private string _disabledTooltipBind;

        private bool _isEncouraged;
        private string _isEncouragedBind;

        private NuiDrawListBuilder<TViewModel> _drawList;
        private string _drawListBind;

        protected NuiBuilderBase(NuiEventCollection eventCollection) 
            : base(eventCollection)
        {
            _drawList = new NuiDrawListBuilder<TViewModel>(eventCollection);
        }

        public TBuilder Aspect(float aspect)
        {
            _aspect = aspect;
            return (TBuilder)this;
        }

        public TBuilder IsEnabled(bool enabled)
        {
            _isEnabled = enabled;
            return (TBuilder)this;
        }

        public TBuilder ForegroundColor(byte red, byte green, byte blue, byte alpha = 255)
        {
            _foregroundColor = new Color(red, green, blue, alpha);
            return (TBuilder)this;
        }

        public TBuilder ForegroundColor(Color foregroundColor)
        {
            _foregroundColor = foregroundColor;
            return (TBuilder)this;
        }

        public TBuilder Height(float height)
        {
            _height = height;
            return (TBuilder)this;
        }

        public TBuilder Id(string id)
        {
            _id = id;
            return (TBuilder)this;
        }

        public TBuilder Margin(float margin)
        {
            _margin = margin;
            return (TBuilder)this;
        }

        public TBuilder Padding(float padding)
        {
            _padding = padding;
            return (TBuilder)this;
        }

        public TBuilder TooltipText(string tooltipText)
        {
            _tooltipText = tooltipText;
            return (TBuilder)this;
        }

        public TBuilder IsVisible(bool visible)
        {
            _isVisible = visible;
            return (TBuilder)this;
        }
        public TBuilder Width(float width)
        {
            _width = width;
            return (TBuilder)this;
        }

        public TBuilder Scissor(bool scissor)
        {
            _useScissor = scissor;
            return (TBuilder)this;
        }
        public TBuilder DrawList(Action<NuiDrawListBuilder<TViewModel>> drawList)
        {
            var drawListBuilder = new NuiDrawListBuilder<TViewModel>(RegisteredEvents);
            drawList(drawListBuilder);

            _drawList = drawListBuilder;

            return (TBuilder)this;
        }

        public TBuilder DrawList(Expression<Func<TViewModel, GuiBindingList<NuiDrawListItem>>> expression)
        {
            _drawListBind = GetBindName(expression);
            return (TBuilder)this;
        }

        public TBuilder DisabledTooltipText(string disabledTooltip)
        {
            _disabledTooltip = disabledTooltip;
            return (TBuilder)this;
        }

        public TBuilder IsEncouraged(bool isEncouraged)
        {
            _isEncouraged = isEncouraged;
            return (TBuilder)this;
        }

        public TBuilder IsEnabled(Expression<Func<TViewModel, bool>> expression)
        {
            _isEnabledBind = GetBindName(expression);
            return (TBuilder)this;
        }

        public TBuilder ForegroundColor(Expression<Func<TViewModel, Color>> expression)
        {
            _foregroundColorBind = GetBindName(expression);
            return (TBuilder)this;
        }

        public TBuilder TooltipText(Expression<Func<TViewModel, string>> expression)
        {
            _tooltipTextBind = GetBindName(expression);
            return (TBuilder)this;
        }

        public TBuilder IsVisible(Expression<Func<TViewModel, bool>> expression)
        {
            _isVisibleBind = GetBindName(expression);
            return (TBuilder)this;
        }


        public TBuilder Scissor(Expression<Func<TViewModel, bool>> expression)
        {
            var bindName = GetBindName(expression);
            _scissorBind = bindName;

            return (TBuilder)this;
        }

        public TBuilder DisabledTooltipText(Expression<Func<TViewModel, string>> expression)
        {
            _disabledTooltipBind = GetBindName(expression);
            return (TBuilder)this;
        }

        public TBuilder IsEncouraged(Expression<Func<TViewModel, bool>> expression)
        {
            _isEncouragedBind = GetBindName(expression);
            return (TBuilder)this;
        }

        private void BindAction(NuiEventType eventType, Expression<Func<TViewModel, Action>> expression)
        {
            if (!HasId())
                _id = Guid.NewGuid().ToString();

            RaisesNuiEvents = true;
            var methodName = GetBindName(expression);

            if (!RegisteredEvents.ContainsKey(_id))
                RegisteredEvents[_id] = new Dictionary<NuiEventType, string>();

            RegisteredEvents[_id][eventType] = methodName;
        }

        public TBuilder OnClick(Expression<Func<TViewModel, Action>> expression)
        {
            BindAction(NuiEventType.Click, expression);
            return (TBuilder)this;
        }

        public TBuilder OnBlur(Expression<Func<TViewModel, Action>> expression)
        {
            BindAction(NuiEventType.Blur, expression);
            return (TBuilder)this;
        }

        public TBuilder OnMouseDown(Expression<Func<TViewModel, Action>> expression)
        {
            BindAction(NuiEventType.MouseDown, expression);
            return (TBuilder)this;
        }

        public TBuilder OnMouseUp(Expression<Func<TViewModel, Action>> expression)
        {
            BindAction(NuiEventType.MouseUp, expression);
            return (TBuilder)this;
        }

        private bool HasId()
        {
            return !string.IsNullOrWhiteSpace(_id);
        }

        public abstract Json BuildEntity();

        public virtual Json Build()
        {
            // Events only get raised in NUI if the element has an Id.
            // Ensure one is assigned if it wasn't set previously.
            if (!HasId() && RaisesNuiEvents)
            {
                _id = Guid.NewGuid().ToString();
            }

            var element = BuildEntity();

            if(!string.IsNullOrWhiteSpace(_id))
                element = Nui.Id(element, _id);

            // Width
            if (_width > 0f)
            {
                element = Nui.Width(element, _width);
            }

            // Height
            if (_height > 0f)
            {
                element = Nui.Height(element, _height);
            }

            // Aspect Ratio
            if (_aspect > 0f)
            {
                element = Nui.Aspect(element, _aspect);
            }

            // Is Enabled (Can be bound)
            if (!string.IsNullOrWhiteSpace(_isEnabledBind))
            {
                var binding = Nui.Bind(_isEnabledBind);
                element = Nui.Enabled(element, binding);
            }
            else
            {
                element = Nui.Enabled(element, JsonBool(_isEnabled));
            }

            // Is Visible (Can be bound)
            if (!string.IsNullOrWhiteSpace(_isVisibleBind))
            {
                var binding = Nui.Bind(_isVisibleBind);
                element = Nui.Visible(element, binding);
            }
            else
            {
                element = Nui.Visible(element, JsonBool(_isVisible));
            }


            // Margin
            if (_margin > -1f)
            {
                element = Nui.Margin(element, _margin);
            }

            // Padding
            if (_padding > 0f)
            {
                element = Nui.Padding(element, _padding);
            }

            // Tooltip (Can be bound)
            if (!string.IsNullOrWhiteSpace(_tooltipTextBind))
            {
                var binding = Nui.Bind(_tooltipTextBind);
                element = Nui.Tooltip(element, binding);
            }
            else if (!string.IsNullOrWhiteSpace(_tooltipText))
            {
                element = Nui.Tooltip(element, JsonString(_tooltipText));
            }

            // Disabled Tooltip (Can be bound)
            if (!string.IsNullOrWhiteSpace(_disabledTooltipBind))
            {
                var binding = Nui.Bind(_disabledTooltipBind);
                element = Nui.DisabledTooltip(element, binding);
            }
            else if (!string.IsNullOrWhiteSpace(_disabledTooltip))
            {
                element = Nui.DisabledTooltip(element, JsonString(_disabledTooltip));
            }

            // Is Encouraged (Can be bound)
            if (!string.IsNullOrWhiteSpace(_isEncouragedBind))
            {
                var binding = Nui.Bind(_isEncouragedBind);
                element = Nui.Encouraged(element, binding);
            }
            else
            {
                element = Nui.Encouraged(element, JsonBool(_isEncouraged));
            }

            // Color
            if (!string.IsNullOrWhiteSpace(_foregroundColorBind))
            {
                var binding = Nui.Bind(_foregroundColorBind);
                element = Nui.StyleForegroundColor(element, binding);
            }
            else if (_foregroundColor != null)
            {
                var color = _foregroundColor.Value;
                element = Nui.StyleForegroundColor(
                    element, 
                    Nui.Color(
                        color.Red, 
                        color.Green, 
                        color.Blue, 
                        color.Alpha));
            }

            var scissor = string.IsNullOrWhiteSpace(_scissorBind)
                ? Nui.Bind(_scissorBind)
                : JsonBool(_useScissor);

            // Draw List
            if (!string.IsNullOrWhiteSpace(_drawListBind))
            {
                var binding = Nui.Bind(_drawListBind);
                element = Nui.DrawList(element, scissor, binding);
            }
            else if(_drawList != null)
            {
                element = _drawList.Build(element);
            }

            return element;
        }

    }
}
