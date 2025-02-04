namespace XM.Plugin.Item.Market.UI.PriceSelection
{
    internal class PriceSelectionPayload
    {
        public string RecordId { get; set; }
        public int CurrentPrice { get; set; }
        public string ItemName { get; set; }

        public PriceSelectionPayload(
            string recordId,
            int currentPrice,
            string itemName)
        {
            RecordId = recordId;
            CurrentPrice = currentPrice;
            ItemName = itemName;
        }
    }
}
