using Anvil.Services;
using XM.Shared.Core.Data;

namespace XM.Plugin.Area.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    internal class PlayerLocation: EntityBase
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
