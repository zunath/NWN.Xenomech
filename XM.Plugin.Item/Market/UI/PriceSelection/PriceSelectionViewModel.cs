using System;
using System.Text.RegularExpressions;
using XM.Plugin.Item.Market.Event;
using XM.Shared.Core.Localization;
using XM.UI;

namespace XM.Plugin.Item.Market.UI.PriceSelection
{
    internal class PriceSelectionViewModel: ViewModel<PriceSelectionViewModel>
    {
        private string _targetRecordId;

        public override void OnOpen()
        {
            var initialPayload = GetInitialData<PriceSelectionPayload>();

            _targetRecordId = initialPayload.RecordId;
            Price = initialPayload.CurrentPrice.ToString();
            ItemName = LocaleString.PriceForX.ToLocalizedString(initialPayload.ItemName);

            WatchOnClient(model => model.Price);
        }

        public override void OnClose()
        {

        }

        public string ItemName
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Price
        {
            get => Get<string>();
            set
            {
                var newPrice = Regex.Replace(value, "[^0-9]", string.Empty);
                newPrice = newPrice.TrimStart('0');

                // If nothing is entered, default to zero.
                if (newPrice.Length < 1)
                    newPrice = "0";

                // Ensure we can convert the number. If we can't, reduce it to zero.
                if (!int.TryParse(newPrice, out var result))
                {
                    newPrice = "0";
                }

                // Handle negative prices.
                if (result < 0)
                    newPrice = "0";

                // Handle max
                if (result > 9999999)
                    newPrice = "9999999";

                Set(newPrice);
            }
        }

        public Action OnClickSave() => () =>
        {
            var price = Convert.ToInt32(Price);
            var @event = new MarketEvent.ChangeMarketPrice(_targetRecordId, price);
            Event.PublishEvent(Player, @event);
            CloseWindow();
        };

        public Action OnClickCancel() => () =>
        {
            CloseWindow();
        };

    }
}
