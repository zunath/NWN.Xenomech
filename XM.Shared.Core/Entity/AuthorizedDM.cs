using Anvil.Services;
using XM.Shared.Core.Authorization;
using XM.Shared.Core.Data;

namespace XM.Shared.Core.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    public class AuthorizedDM : EntityBase
    {
        [Indexed]
        public string Name { get; set; }
        [Indexed]
        public string CDKey { get; set; }
        [Indexed]
        public AuthorizationLevel Authorization { get; set; }
    }
}
