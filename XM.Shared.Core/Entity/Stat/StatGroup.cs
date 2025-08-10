using System.Collections.Generic;

using System.Text.Json.Serialization;
using XM.Shared.Core.Json;

namespace XM.Shared.Core.Entity.Stat
{
    [JsonConverter(typeof(StatGroupJsonConverter))]
    public class StatGroup
    {
        public Dictionary<int, int> Stats { get; set; } = new();
        public Dictionary<int, int> Resists { get; set; } = new();
    }
}


