﻿using System;

namespace XM.Shared.Core.Configuration
{
    [Flags]
    public enum ServerEnvironmentType
    {
        Invalid = 0,
        Development = 1,
        Production = 2,
        Test = 4,

        All = Development | Production | Test
    }
}
