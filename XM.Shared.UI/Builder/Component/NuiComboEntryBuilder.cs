using Anvil.API;
using XM.Shared.API.NUI;

namespace XM.UI.Builder.Component
{
    public class NuiComboEntryBuilder<TViewModel>
        : NuiBuilderBase<NuiComboEntryBuilder<TViewModel>, TViewModel>
        where TViewModel: IViewModel
    {
        private string _label;
        private int _value;

        public NuiComboEntryBuilder(NuiEventCollection eventCollection) 
            : base(eventCollection)
        {
        }

        public NuiComboEntryBuilder<TViewModel> Label(string label)
        {
            _label = label;
            return this;
        }

        public NuiComboEntryBuilder<TViewModel> Value(int value)
        {
            _value = value;
            return this;
        }

        public override Json BuildEntity()
        {
            return Nui.ComboEntry(_label, _value);
        }
    }
}
