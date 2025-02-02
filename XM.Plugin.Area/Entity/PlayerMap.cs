using Anvil.Services;
using System.Collections.Generic;
using XM.Plugin.Area.Map;
using XM.Shared.Core.Data;

namespace XM.Plugin.Area.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    internal class PlayerMap: EntityBase
    {
        public PlayerMap()
        {
            MapProgressions = new Dictionary<string, string>();
            MapPins = new Dictionary<string, List<MapPin>>();
        }

        public PlayerMap(string playerId)
        {
            Id = playerId;
            MapProgressions = new Dictionary<string, string>();
            MapPins = new Dictionary<string, List<MapPin>>();
        }

        public Dictionary<string, string> MapProgressions { get; set; }
        public Dictionary<string, List<MapPin>> MapPins { get; set; }
    }
}
