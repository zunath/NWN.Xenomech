using Anvil.Services;

namespace XM.Shared.Core.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerMarket : EntityBase
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



