using Anvil.Services;
using System.Collections.Generic;

namespace XM.Shared.Core.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerMap : EntityBase
    {
        public PlayerMap()
        {
            MapProgressions = new Dictionary<string, string>();
            MapPins = new Dictionary<string, List<MapPinEntry>>();
        }

        public PlayerMap(string playerId)
        {
            Id = playerId;
            MapProgressions = new Dictionary<string, string>();
            MapPins = new Dictionary<string, List<MapPinEntry>>();
        }

        public Dictionary<string, string> MapProgressions { get; set; }
        public Dictionary<string, List<MapPinEntry>> MapPins { get; set; }
    }
}



