using Anvil.Services;
using XM.Shared.Core.Localization;
using XM.UI;
using XM.UI.Builder;

namespace XM.Inventory.Durability.UI
{
    [ServiceBinding(typeof(IView))]
    internal class ItemRepairView: IView
    {
        private readonly NuiBuilder<ItemRepairViewModel> _builder = new();

        public IViewModel CreateViewModel()
        {
            return new ItemRepairViewModel();
        }

        public NuiBuiltWindow Build()
        {
            return _builder.CreateWindow(window =>
            {
                window
                    .IsCollapsible(WindowCollapsibleType.UserCollapsible)
                    .IsResizable(true)
                    .IsClosable(true)
                    .IsTransparent(false)
                    .Title(LocaleString.RepairItem)
                    .InitialGeometry(0, 0, 800, 400)
                    .Root(root =>
                    {

                    });
            }).Build();
        }
    }
}
