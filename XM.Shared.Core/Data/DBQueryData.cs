using System.Collections.Generic;

namespace XM.Shared.Core.Data
{
    public class DBQueryData
    {
        public Dictionary<string, SearchCriteria> FieldSearches { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public string SortByField { get; set; }
        public bool IsAscending { get; set; }

        public DBQueryData()
        {
            FieldSearches = new Dictionary<string, SearchCriteria>();
        }
    }
}
