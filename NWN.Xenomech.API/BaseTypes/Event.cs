using NWN.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWN.Xenomech.API.BaseTypes
{
    public partial class Event
    {
        public IntPtr Handle;
        public Event(IntPtr handle) => Handle = handle;
        ~Event() { VM.FreeGameDefinedStructure((int)EngineStructure.Event, Handle); }

        public static implicit operator IntPtr(Event effect) => effect.Handle;
        public static implicit operator Event(IntPtr intPtr) => new Event(intPtr);
    }
}
