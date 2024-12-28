using System;

namespace NWN.Xenomech.Authorization
{
    [Flags]
    public enum AuthorizationLevel
    {
        None = 0,
        Player = 1,
        DM = 2,
        Admin = 4,

        All = Player | DM | Admin
    }

}
