﻿using Anvil.Services;
using XM.Shared.Core.Data;
using XM.Shared.Core.Entity;

namespace XM.Shared.Core.Authorization.Entity
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
