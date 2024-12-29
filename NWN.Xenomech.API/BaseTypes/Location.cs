using NWN.Core;

namespace NWN.Xenomech.API.BaseTypes
{
    public partial class Location
    {
        public IntPtr Handle;
        public Location(IntPtr handle) => Handle = handle;
        ~Location() { VM.FreeGameDefinedStructure((int)EngineStructure.Location, Handle); }

        public static implicit operator IntPtr(Location effect) => effect.Handle;
        public static implicit operator Location(IntPtr intPtr) => new Location(intPtr);
    }
}
