using Anvil.Services;
using XM.Data.Shared;

namespace XM.Authorization.Entity
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
