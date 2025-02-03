﻿namespace XM.Shared.Core.Data
{
    public enum DBServerCommandType
    {
        Invalid = 0,
        Register = 1,
        Get = 2,
        Set = 3,
        Search = 4,
        Result = 5,
        Ok = 6,
        Error = 7,
        SearchCount = 8,
        Delete = 9,
        IndexingStatus = 10,
        Pending = 11,
    }
}
