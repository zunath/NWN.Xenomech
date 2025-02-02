using Anvil.Services;
using System.Collections.Generic;
using XM.Shared.Core.Data;

namespace XM.Plugin.Area.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    internal class PlayerMapProgression: EntityBase
    {
        public PlayerMapProgression()
        {
            MapProgressions = new Dictionary<string, string>();
        }

        public PlayerMapProgression(string playerId)
        {
            Id = playerId;
            MapProgressions = new Dictionary<string, string>();
        }

        public Dictionary<string, string> MapProgressions { get; set; }
    }
}
