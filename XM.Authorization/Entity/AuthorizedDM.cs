using XM.Data;

namespace XM.Authorization.Entity
{
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
