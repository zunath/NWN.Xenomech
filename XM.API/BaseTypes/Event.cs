using NWN.Core;

namespace XM.API.BaseTypes
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
