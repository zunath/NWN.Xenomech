using System.Collections.Generic;

namespace XM.Shared.Core.Data
{
    public class DBServerCommand
    {
        public DBServerCommandType CommandType { get; set; }
        public string EntityType { get; set; }
        public string Key { get; set; }
        public string EntitySingle { get; set; }
        public List<string> EntitiesList { get; set; }
        public string Message { get; set; }
        public List<IndexedProperty> IndexedProperties { get; set; }
        public Dictionary<string, object> IndexData { get; set; }

        public DBQuery<IDBEntity> Query { get; set; }

        public DBServerCommand()
        {
            IndexedProperties = new List<IndexedProperty>();
            EntitiesList = new List<string>();
        }
    }
}