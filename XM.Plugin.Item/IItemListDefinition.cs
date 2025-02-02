using System.Collections.Generic;

namespace XM.Plugin.Item
{
    public interface IItemListDefinition
    {
        public Dictionary<string, ItemDetail> BuildItems();
    }
}
