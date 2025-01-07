using Anvil.API;
using XM.Shared.API.NUI;

namespace XM.UI.Builder.Component
{
    public class NuiSpacerBuilder<TViewModel> : NuiBuilderBase<NuiSpacerBuilder<TViewModel>, TViewModel>
        where TViewModel : IViewModel
    {
        public NuiSpacerBuilder(NuiEventCollection eventCollection)
            : base(eventCollection)
        {
        }

        public override Json BuildEntity()
        {
            return Nui.Spacer();
        }
    }
}