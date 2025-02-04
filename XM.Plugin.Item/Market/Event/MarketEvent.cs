using XM.Shared.Core.EventManagement;

namespace XM.Plugin.Item.Market.Event
{
    public class MarketEvent
    {
        public struct ChangeMarketPrice : IXMEvent
        {
            public string RecordId { get; set; }
            public int Price { get; set; }

            public ChangeMarketPrice(string recordId, int price)
            {
                RecordId = recordId;
                Price = price;
            }
        }
    }
}
