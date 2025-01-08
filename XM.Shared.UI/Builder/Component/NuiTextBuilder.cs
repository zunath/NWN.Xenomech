using Anvil.API;
using System.Linq.Expressions;
using System;
using XM.Shared.API.NUI;
using NuiScrollbars = XM.Shared.API.NUI.NuiScrollbars;

namespace XM.UI.Builder.Component
{
    public class NuiTextBuilder<TViewModel> : NuiBuilderBase<NuiTextBuilder<TViewModel>, TViewModel>
        where TViewModel : IViewModel
    {
        private string _text;
        private string _textBind;

        private bool _border;
        private NuiScrollbars _scrollbars;

        public NuiTextBuilder(NuiEventCollection eventCollection)
            : base(eventCollection)
        {
        }

        public NuiTextBuilder<TViewModel> Text(string text)
        {
            _text = text;
            return this;
        }

        public NuiTextBuilder<TViewModel> Border(bool border)
        {
            _border = border;
            return this;
        }

        public NuiTextBuilder<TViewModel> Scrollbars(NuiScrollbars scrollbars)
        {
            _scrollbars = scrollbars;
            return this;
        }

        public NuiTextBuilder<TViewModel> Text(Expression<Func<TViewModel, string>> expression)
        {
            _textBind = GetBindName(expression);
            return this;
        }

        public override Json BuildEntity()
        {
            var text = string.IsNullOrWhiteSpace(_textBind)
                ? JsonString(_text)
                : Nui.Bind(_textBind);

            return Nui.Text(text, _border, _scrollbars);
        }
    }
}