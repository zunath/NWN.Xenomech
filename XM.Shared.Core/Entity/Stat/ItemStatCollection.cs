using System.Collections.Generic;
using System.Text.Json.Serialization;
using XM.Shared.Core.Json;

namespace XM.Shared.Core.Entity.Stat
{
    [JsonConverter(typeof(ItemStatCollectionJsonConverter))]
    public class ItemStatCollection : Dictionary<int, ItemStatGroup>
    {
    }
}


