using NWN.Core;

namespace XM.API.BaseTypes
{
    public class Cassowary
    {
        public IntPtr Handle;
        public Cassowary(IntPtr handle) => Handle = handle;
        ~Cassowary() { VM.FreeGameDefinedStructure((int)EngineStructure.Cassowary, Handle); }

        public static implicit operator IntPtr(Cassowary cassowary) => cassowary.Handle;
        public static implicit operator Cassowary(IntPtr intPtr) => new Cassowary(intPtr);
    }
}
