using System.Collections.Generic;
using System.Text.Json.Serialization;
using XM.Shared.Core.Json;

namespace XM.Shared.Core.Entity.Stat
{
    [JsonConverter(typeof(AbilityStatCollectionJsonConverter))]
    public class AbilityStatCollection : Dictionary<int, StatGroup>
    {
    }
}


