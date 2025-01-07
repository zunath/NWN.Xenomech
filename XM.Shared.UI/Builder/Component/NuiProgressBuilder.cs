using Anvil.API;
using System.Linq.Expressions;
using System;
using XM.Shared.API.NUI;

namespace XM.UI.Builder.Component
{
    public class NuiProgressBuilder<TViewModel> : NuiBuilderBase<NuiProgressBuilder<TViewModel>, TViewModel>
        where TViewModel : IViewModel
    {
        private float _value;
        private string _valueBind;

        public NuiProgressBuilder(NuiEventCollection eventCollection)
            : base(eventCollection)
        {
        }

        public NuiProgressBuilder<TViewModel> Value(float value)
        {
            _value = value;
            return this;
        }

        public NuiProgressBuilder<TViewModel> Value(Expression<Func<TViewModel, float>> expression)
        {
            _valueBind = GetBindName(expression);
            return this;
        }

        public override Json BuildEntity()
        {
            var value = string.IsNullOrWhiteSpace(_valueBind)
                ? JsonFloat(_value)
                : Nui.Bind(_valueBind);

            return Nui.Progress(value);
        }
    }
}