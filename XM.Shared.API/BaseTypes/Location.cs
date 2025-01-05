using NWN.Core;
using XM.Shared.API;

namespace XM.Shared.API.BaseTypes
{
    public partial class Location
    {
        public nint Handle;
        public Location(nint handle) => Handle = handle;
        ~Location() { VM.FreeGameDefinedStructure((int)EngineStructure.Location, Handle); }

        public static implicit operator nint(Location effect) => effect.Handle;
        public static implicit operator Location(nint intPtr) => new Location(intPtr);
    }
}
