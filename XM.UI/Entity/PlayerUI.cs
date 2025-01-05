using Anvil.Services;
using System.Collections.Generic;
using Anvil.API;
using XM.Data;
using XM.Data.Shared;

namespace XM.UI.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    internal class PlayerUI: EntityBase
    {
        public PlayerUI()
        {
            Init();
        }

        public PlayerUI(string playerId)
        {
            Id = playerId;
            Init();
        }

        private void Init()
        {
            WindowGeometries = new Dictionary<string, NuiRect>();
        }

        public Dictionary<string, NuiRect> WindowGeometries { get; set; }
    }
}
