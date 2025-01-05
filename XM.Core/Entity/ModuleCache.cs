using Anvil.Services;
using System.Collections.Generic;
using System.Numerics;
using XM.Core.Data;

namespace XM.Core.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class ModuleCache: EntityBase
    {
        public const string CacheIdName = "MOD_CACHE";

        public ModuleCache()
        {
            Id = CacheIdName;
            WalkmeshesByArea = new Dictionary<string, List<Vector3>>();
            ItemNamesByResref = new Dictionary<string, string>();
        }

        public int LastModuleMTime { get; set; }
        public Dictionary<string, List<Vector3>> WalkmeshesByArea { get; set; }
        public Dictionary<string, string> ItemNamesByResref { get; set; }
    }
}
