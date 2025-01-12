using Anvil.API;
using XM.Shared.API.NUI;
using XM.Shared.Core.Localization;

namespace XM.UI.Builder.Component
{
    public class NuiComboEntryBuilder<TViewModel>
        : NuiBuilderBase<NuiComboEntryBuilder<TViewModel>, TViewModel>
        where TViewModel: IViewModel
    {
        private LocaleString _label;
        private int _value;

        public NuiComboEntryBuilder(NuiEventCollection eventCollection) 
            : base(eventCollection)
        {
        }

        public NuiComboEntryBuilder<TViewModel> Label(LocaleString label)
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
            var label = Locale.GetString(_label);
            return Nui.ComboEntry(label, _value);
        }
    }
}
