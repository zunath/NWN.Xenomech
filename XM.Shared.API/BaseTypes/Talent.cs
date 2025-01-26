using NWN.Core;
using NWNX.NET;
using XM.Shared.API;

namespace XM.Shared.API.BaseTypes
{
    public partial class Talent
    {
        public nint Handle;
        public Talent(nint handle) => Handle = handle;
        ~Talent() { NWNXAPI.FreeGameDefinedStructure((int)EngineStructure.Talent, Handle); }

        public static implicit operator nint(Talent effect) => effect.Handle;
        public static implicit operator Talent(nint intPtr) => new Talent(intPtr);
    }
}
