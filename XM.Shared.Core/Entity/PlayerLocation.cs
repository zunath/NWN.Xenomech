using Anvil.Services;

namespace XM.Shared.Core.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerLocation : EntityBase
    {
        public PlayerLocation()
        {
        }

        public PlayerLocation(string playerId)
        {
            Id = playerId;
        }

        public string LocationAreaResref { get; set; }
        public float LocationX { get; set; }
        public float LocationY { get; set; }
        public float LocationZ { get; set; }
        public float LocationOrientation { get; set; }
    }
}



