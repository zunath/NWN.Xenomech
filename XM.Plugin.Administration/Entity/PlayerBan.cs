using Anvil.Services;
using XM.Shared.Core.Data;

namespace XM.Plugin.Administration.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    internal class PlayerBan: EntityBase
    {
        public string CDKey { get; set; }
        public string Reason { get; set; }
    }
}
