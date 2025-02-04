using Anvil.Services;
using XM.Shared.Core.Data;

namespace XM.Plugin.Item.Market.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    internal class PlayerMarket: EntityBase
    {
        public PlayerMarket()
        {
            
        }

        public PlayerMarket(string playerId)
        {
            Id = playerId;
        }

        public int MarketTill { get; set; }
    }
}
