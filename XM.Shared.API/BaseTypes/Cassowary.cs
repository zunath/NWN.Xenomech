﻿using NWN.Core;
using XM.Shared.API;

namespace XM.Shared.API.BaseTypes
{
    public class Cassowary
    {
        public nint Handle;
        public Cassowary(nint handle) => Handle = handle;
        ~Cassowary() { VM.FreeGameDefinedStructure((int)EngineStructure.Cassowary, Handle); }

        public static implicit operator nint(Cassowary cassowary) => cassowary.Handle;
        public static implicit operator Cassowary(nint intPtr) => new Cassowary(intPtr);
    }
}
