using Anvil.Services;
using XM.Plugin.Item.Market.UI.MarketBuyMenu;
using XM.Plugin.Item.Market.UI.MarketListingMenu;
using XM.Shared.Core.Dialog;
using XM.Shared.Core;
using XM.Shared.Core.Localization;
using XM.UI;

namespace XM.Plugin.Item.Market.Dialog
{
    [ServiceBinding(typeof(IConversation))]
    internal class MarketDialog : DialogBase
    {
        private const string MainPageId = "MAIN_PAGE";

        [Inject]
        private GuiService Gui { get; set; }

        public override PlayerDialog SetUp(uint player)
        {
            var builder = new DialogBuilder()
                .AddPage(MainPageId, MainPageInit);

            return builder.Build();
        }

        private void MainPageInit(DialogPage page)
        {
            var player = GetPC();

            page.Header = ColorToken.Green(LocaleString.GlobalMarket.ToLocalizedString()) + "\n\n" +
                          LocaleString.MarketDescription.ToLocalizedString();

            page.AddResponse("Buy", () =>
            {
                Gui.ShowWindow<MarketBuyView>(player);
                EndConversation();
            });

            page.AddResponse("Sell", () =>
            {
                Gui.ShowWindow<MarketListingView>(player);
                EndConversation();
            });
        }
    }
}
