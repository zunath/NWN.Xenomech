using Anvil.Services;
using XM.Shared.Core.Data;

namespace XM.Shared.Core.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerRespawn: EntityBase
    {
        public float RespawnLocationX { get; set; }
        public float RespawnLocationY { get; set; }
        public float RespawnLocationZ { get; set; }
        public float RespawnLocationOrientation { get; set; }
        [Indexed]
        public string RespawnAreaResref { get; set; }

        public PlayerRespawn()
        {
            Init();
        }

        public PlayerRespawn(string id)
        {
            Init();
            Id = id;
        }

        private void Init()
        {
            RespawnAreaResref = string.Empty;
        }
    }
}



