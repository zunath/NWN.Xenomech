﻿using NWN.Core;

namespace XM.Shared.API.BaseTypes
{
    public partial class Event
    {
        public nint Handle;
        public Event(nint handle) => Handle = handle;
        ~Event() { VM.FreeGameDefinedStructure((int)EngineStructure.Event, Handle); }

        public static implicit operator nint(Event effect) => effect.Handle;
        public static implicit operator Event(nint intPtr) => new Event(intPtr);
    }
}