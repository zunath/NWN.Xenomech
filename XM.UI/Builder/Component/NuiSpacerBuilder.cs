using Anvil.API;

namespace XM.UI.Builder.Component
{
    public class NuiSpacerBuilder<TViewModel> : NuiBuilderBase<NuiSpacerBuilder<TViewModel>, NuiSpacer, TViewModel>
        where TViewModel: IViewModel
    {
        public NuiSpacerBuilder()
            : base(new NuiSpacer())
        {
        }
    }


}
