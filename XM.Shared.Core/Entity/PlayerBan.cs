using Anvil.Services;

namespace XM.Shared.Core.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class PlayerBan: EntityBase
    {
        public string CDKey { get; set; }
        public string Reason { get; set; }
    }
}
