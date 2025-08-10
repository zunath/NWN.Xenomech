using System.Collections.Generic;
using Anvil.API;
using Anvil.Services;

namespace XM.Shared.Core.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerUI : EntityBase
    {
        public PlayerUI()
        {
            WindowGeometries = new Dictionary<string, NuiRect>();
        }

        public PlayerUI(string playerId)
        {
            Id = playerId;
            WindowGeometries = new Dictionary<string, NuiRect>();
        }

        public Dictionary<string, NuiRect> WindowGeometries { get; set; }
    }
}


