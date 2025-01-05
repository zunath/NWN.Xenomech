using NWN.Core;
using XM.Shared.API;

namespace XM.Shared.API.BaseTypes
{
    public partial class Effect
    {
        public nint Handle;
        public Effect(nint handle) => Handle = handle;
        ~Effect() { VM.FreeGameDefinedStructure((int)EngineStructure.Effect, Handle); }

        public static implicit operator nint(Effect effect) => effect.Handle;
        public static implicit operator Effect(nint intPtr) => new Effect(intPtr);
    }
}
